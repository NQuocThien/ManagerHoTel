using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Manager_Hotel
{
    class Database_KS
    {
        public static DataSet ds;
        public static SqlConnection conn;
        public Database_KS()
        {
            ds = new DataSet("dsQLKS");
            conn = new SqlConnection(@"Data Source=QUOCTHIEN;Initial Catalog=QUANLYKHACHSAN;Persist Security Info=True;User ID=sa;Password=***********");
        }
       
    }
}
