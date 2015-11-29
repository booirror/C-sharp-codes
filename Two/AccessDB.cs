using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace DBTest
{
    class AccessDB
    {
        private static string accessDB = System.Configuration.ConfigurationManager.AppSettings["accessdb"];
        private static string dbConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"D:\programming_langage_csharp\db" + "\\" + accessDB;

        public static string DBConnString
        {
            get
            {
                return dbConnString;
            }
        }

        public static DataSet GetDataSet(string sql)
        {
            if (DBConnString == null || dbConnString == "") return null;
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, DBConnString);
            DataSet dsMyData = new DataSet();
            adapter.Fill(dsMyData);
            return dsMyData;
        }

        public static int ExecuteNonQuery(string sql)
        {
            if (DBConnString == null || DBConnString == "") return 0;

            OleDbConnection con = new OleDbConnection(DBConnString);
            OleDbCommand cmd = new OleDbCommand(sql, con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            return n;
        }

        public static string ExecuteScalar(string sql)
        {
            if (DBConnString == null || DBConnString == "") return null;

            OleDbConnection myConn = new OleDbConnection(DBConnString);
            OleDbCommand myCommand = new OleDbCommand(sql, myConn);
            myConn.Open();
            object ret = myCommand.ExecuteScalar();
            myConn.Close();
            if (ret == null) return null;
            return ret.ToString();
        }
    }
}
