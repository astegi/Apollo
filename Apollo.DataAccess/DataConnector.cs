using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Apollo.DataAccess
{
    /// <summary>
    /// Adatbázis kapcsolattartó osztály
    /// </summary>
    public static class DataConnector
    {
        internal static DbProviderFactory factory;
        private static readonly string connectionString;

        static DataConnector()
        {
            string provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.ConnectionStrings[provider].ConnectionString;

            factory = DbProviderFactories.GetFactory(provider);
        }


        #region <--- DML --->
        /// <summary>
        /// Lekérdezés futtatás
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<T> ListDataEntities<T>(Query query)
            where T: DataCore
        {
            DataTable table = ExecuteQuery(query);
            return (from DataRow row in table.Rows select FillAllFields<T>(row)).ToList();
        }

        /// <summary>
        /// Entitás frissítés
        /// </summary>
        /// <param name="entity"></param>
        public static void UpdateDataEntity(DataCore entity)
        {
            var sb = new StringBuilder();
            string primaryKey = String.Empty;

            Type t = entity.GetType();
            object[] attributes = t.GetCustomAttributes(typeof(DataTableAttribute), false);
            if (attributes.Length != 1)
                return;

            var query = new GeneralQuery();
            Attribute attribute = (DataTableAttribute)attributes[0];

            sb.AppendFormat("UPDATE [{0}].[{1}] SET ", ((DataTableAttribute)attribute).SchemaName, ((DataTableAttribute)attribute).TableName);
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo field in fields)
            {
                attributes = field.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (attributes.Length != 1)
                    continue;

                attribute = (DataFieldAttribute)attributes[0];

                bool isPrimaryKey = ((DataFieldAttribute)attribute).IsPrimaryKey;
                string fieldName = ((DataFieldAttribute)attribute).FieldName;

                if (!isPrimaryKey)
                {
                    sb.AppendFormat("{0} = @{0}, ", fieldName);
                }
                else primaryKey = String.Format("{0} = @{0}", fieldName);

                query.AddParameter("@" + fieldName, field.GetValue(entity));
            }
            sb.Remove(sb.Length - 2, 2);
            sb.AppendFormat(" WHERE {0}", primaryKey);

            if (String.IsNullOrEmpty(primaryKey))
                return;

            ExecuteNonQuery(query);
        }

        /// <summary>
        /// Entitás törlés
        /// </summary>
        /// <param name="entity"></param>
        public static void DeleteDataEntity(DataCore entity)
        {
            var sb = new StringBuilder();
            string primaryKey = String.Empty;

            Type t = entity.GetType();
            object[] attributes = t.GetCustomAttributes(typeof(DataTableAttribute), false);
            if (attributes.Length != 1)
                return;

            var query = new GeneralQuery();
            Attribute attribute = (DataTableAttribute)attributes[0];

            sb.AppendFormat("DELETE FROM [{0}].[{1}] ", ((DataTableAttribute)attribute).SchemaName, ((DataTableAttribute)attribute).TableName);
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo field in fields)
            {
                attributes = field.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (attributes.Length != 1)
                    continue;

                attribute = (DataFieldAttribute)attributes[0];

                bool isPrimaryKey = ((DataFieldAttribute)attribute).IsPrimaryKey;
                string fieldName = ((DataFieldAttribute)attribute).FieldName;

                if (isPrimaryKey)
                {
                    query.AddParameter("@" + fieldName, field.GetValue(entity));
                    primaryKey = String.Format("{0} = @{0}", fieldName);
                    break;
                }

            }
            sb.AppendFormat("WHERE {0}", primaryKey);
            if (String.IsNullOrEmpty(primaryKey))
                return;

            ExecuteNonQuery(query);
        }

        /// <summary>
        /// Entitás beszúrás
        /// </summary>
        /// <param name="entity"></param>
        public static void InsertDataEntity(DataCore entity)
        {
            StringBuilder sb = new StringBuilder();

            Type t = entity.GetType();
            object[] attributes = t.GetCustomAttributes(typeof(DataTableAttribute), false);
            if (attributes.Length != 1)
                return;

            GeneralQuery query = new GeneralQuery();
            Attribute attribute = (DataTableAttribute)attributes[0];

            sb.AppendFormat("INSERT INTO [{0}].[{1}] (", ((DataTableAttribute)attribute).SchemaName, ((DataTableAttribute)attribute).TableName);
            FieldInfo[] fields = t.GetFields();

            StringBuilder paramValues = new StringBuilder();
            foreach (FieldInfo field in fields)
            {
                attributes = field.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (attributes.Length != 1)
                    continue;

                attribute = (DataFieldAttribute)attributes[0];

                string fieldName = ((DataFieldAttribute)attribute).FieldName;

                sb.AppendFormat("{0}, ", fieldName);
                paramValues.AppendFormat("@{0}, ", fieldName);

                query.AddParameter("@" + fieldName, field.GetValue(entity));
            }
            sb.Remove(sb.Length - 2, 2);
            paramValues.Remove(paramValues.Length - 2, 2);

            sb.AppendFormat(") VALUES ({0})", paramValues);
            ExecuteNonQuery(query);
        }
        #endregion

        #region <--- DDL --->
        /// <summary>
        /// Eldobja az adatbázistáblát
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Drop<T>()
            where T : DataCore
        {
            Type t = typeof(T);
            object[] attributes = t.GetCustomAttributes(typeof(DataTableAttribute), false);
            if (attributes.Length != 1)
                return;

            DataTableAttribute dataTableAttribute = (DataTableAttribute)attributes[0];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DROP TABLE [{0}].[{1}]", dataTableAttribute.SchemaName, dataTableAttribute.TableName);

            ExecuteNonQuery(Query.Create(sb.ToString()));
        }
        #endregion

        private static void ExecuteNonQuery(Query query)
        {
            using (DbConnection connection = CreateConnection())
            {
                DbCommand command = CreateCommand(connection, query.CommandText, query.Parameters);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private static DataTable ExecuteQuery(Query query)
        {
            using (DbConnection connection = CreateConnection())
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                DataTable ds = new DataTable();
                if (adapter != null)
                {
                    adapter.SelectCommand = CreateCommand(connection, query.CommandText, query.Parameters);

                    adapter.Fill(ds);
                }
                return ds;
            }
        }

        private static DbCommand CreateCommand(DbConnection connection, string commandText, IEnumerable<DbParameter> parameters)
        {
            DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            foreach (DbParameter parameter in parameters)
                command.Parameters.Add(parameter);

            return command;
        }
        private static DbConnection CreateConnection()
        {
            DbConnection connection = factory.CreateConnection();
            if (connection != null)
            {
                connection.ConnectionString = connectionString;
            }
            return connection;
        }

        private static void FillFields(object item, Type type, DataRow row)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object[] attributes = field.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (attributes.Length != 1)
                    continue;

                DataFieldAttribute attribute = (DataFieldAttribute)attributes[0];
                if (!row.Table.Columns.Contains(attribute.FieldName))
                {
                    if (attribute.Required)
                        throw new ArgumentException();
                    continue;
                }

                if (!attribute.AllowNull && Convert.IsDBNull(row[attribute.FieldName]))
                    throw new ArgumentNullException();

                field.SetValue(item, row[attribute.FieldName]);
            }
        }
        private static T FillAllFields<T>(DataRow row)
        {
            Type t = typeof(T);
            T item = (T)Activator.CreateInstance(t);
            Type itemType = t;
            while (itemType != null && itemType.FullName != typeof(Object).FullName)
            {
                FillFields(item, itemType, row);
                itemType = itemType.BaseType;
            }

            return item;
        }
    }
}
