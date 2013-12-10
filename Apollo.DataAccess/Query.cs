using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Apollo.DataAccess
{
    /// <summary>
    /// Egy adatbázisbeli parancsot reprezentáló osztály
    /// </summary>
    public abstract class Query
    {
        private readonly List<DbParameter> parameters;
        private DbParameter returnParameter;

        public virtual CommandType CommandType
        {
            get { return CommandType.Text; }
        }

        /// <summary>
        /// Az adatbázisbeli parancs szövege
        /// </summary>
        public abstract string CommandText { get; }

        /// <summary>
        /// Az adatbázisbeli parancs paraméterei
        /// </summary>
        public List<DbParameter> Parameters
        {
            get { return parameters; }
        }

        /// <summary>
        /// Az adatbázisbeli parancs visszatérési értéke, ha volt
        /// </summary>
        public object ReturnValue
        {
            get { return returnParameter == null ? null : returnParameter.Value; }
        }


        /// <summary>
        /// Létrehoz egy új adatbázis parancsot
        /// </summary>
        public Query()
        {
            parameters = new List<DbParameter>();

            AddParameters();
        }

        /// <summary>
        /// Felülírva elérhetővé teszi a paraméterek hozzáadását
        /// </summary>
        protected virtual void AddParameters()
        {
        }

        /// <summary>
        /// Bemeneti paramétert hoz létre
        /// </summary>
        /// <param name="name">A paraméter neve</param>
        /// <param name="value">A paraméter értéke</param>
        protected void AddInParameter(string name, object value)
        {
            DbParameter parameter = DataConnector.factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;

            parameters.Add(parameter);
        }
        
        /// <summary>
        /// Kimeneti paramétert hoz létre
        /// </summary>
        protected void AddOutParameter()
        {
            if (returnParameter != null)
                return;

            returnParameter = DataConnector.factory.CreateParameter();
            returnParameter.ParameterName = "return";
            returnParameter.Direction = ParameterDirection.ReturnValue;

            parameters.Add(returnParameter);
        }


        public static Query Create(string commandText)
        {
            GeneralQuery query = new GeneralQuery();
            query.SetQuery(commandText);
            return query;
        }
    }
}
