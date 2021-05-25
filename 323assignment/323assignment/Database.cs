using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace _323assignment
{
    class OracleDB
    {
        public Boolean Connect()
        {
            try
            {
                string oradb = "Data Source=oracle.cms.waikato.ac.nz:1521/teaching;User Id=COMPX323_05;Password=qVfsntckJ9;";
                OracleConnection conn = new OracleConnection(oradb);
                conn.Open();
                return true;
            }
            catch
            {
                Console.WriteLine("An Error Occured connecting to the database");
                return false;
            }
        }
    }
}
