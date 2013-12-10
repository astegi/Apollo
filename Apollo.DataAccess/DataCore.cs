
using System;
namespace Apollo.DataAccess
{
    /// <summary>
    /// Adatbázis tábla őselem
    /// </summary>
    [Serializable]
    public class DataCore
    {
        [DataField("pkID", true, false, true)]
        private int id;

        /// <summary>
        /// Elsődleges kulcs
        /// </summary>
        public int PrimaryID
        {
            get { return id; }
            private set { id = value; }
        }

        /// <summary>
        /// Frissíti az adatbázisban az entitást
        /// </summary>
        public void Update()
        {
            DataConnector.UpdateDataEntity(this);
        }

        /// <summary>
        /// Beszúrja az adatbázisba az entitást
        /// </summary>
        public void Insert()
        {
            DataConnector.InsertDataEntity(this);
        }

        /// <summary>
        /// Törli az adatbázisból az entitást
        /// </summary>
        public void Delete()
        {
            DataConnector.DeleteDataEntity(this);
        }

    }
}
