using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_de_Dados
{

    class Connect : IDisposable
    {
        public static string query = (@"Data Source=.\SQLEXPRESS;Initial Catalog=db_ARS;User ID=sa;Password=Lucas123");
        //"Data Source=.\SQLEXPRESS;Initial Catalog=db_ARS;User ID=sa;Password=Lucas1234"
        SqlConnection sqlCon = new SqlConnection(query);

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
