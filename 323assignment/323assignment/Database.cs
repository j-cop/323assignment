using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace _323assignment
{
    class OracleDB
    {
        OracleConnection conn;
        public Boolean Connect()
        {
            try
            {
                string oradb = "Data Source=oracle.cms.waikato.ac.nz:1521/teaching;User Id=COMPX323_05;Password=qVfsntckJ9;";
                conn = new OracleConnection(oradb);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public OracleDataReader Query(String query)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                return dr;

            }
            catch
            {
                return null;
            }
        }


    }

}
