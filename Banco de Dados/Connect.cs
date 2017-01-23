using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Report
{

    class Connect : IDisposable
    {
        //public static string query = (@"Data Source=.\SQLEXPRESS;Initial Catalog=db_ARS;User ID=sa;Password=Lucas123");
        //"Data Source=.\SQLEXPRESS;Initial Catalog=db_ARS;User ID=sa;Password=Lucas1234"
        SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[1].ToString().Replace("Provider=SQLNCLI11;",""));

        public void Dispose()
        {
            sqlCon.Dispose();
            GC.SuppressFinalize(this);
        }
        public SqlConnection SqlCon
        {
            get
            {
                return sqlCon;
            }

            set
            {
                sqlCon = value;
            }
        }
    }

}
