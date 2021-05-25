using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace _323assignment
{
    class Database
    {
        private void Connect()
        {
            string oradb = "Data Source=ORCL;User Id=hr;Password=hr;";
            OracleConnection conn = new OracleConnection(oradb);  // C#
            conn.Open();
        }
    }
}
