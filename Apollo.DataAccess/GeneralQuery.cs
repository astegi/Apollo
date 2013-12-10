using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.DataAccess
{
    class GeneralQuery : Query
    {
        string commandText;

        public override string CommandText
        {
            get { return commandText; }
        }

        public void SetQuery(string commandText)
        {
            this.commandText = commandText;
        }

        /// <summary>
        /// Kimeneti paramétert hoz létre
        /// </summary>
        public void AddParameter()
        {
            base.AddOutParameter();
        }

        /// <summary>
        /// Bemeneti paramétert hoz létre
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddParameter(string name, object value)
        {
            base.AddInParameter(name, value);
        }
    }
}
