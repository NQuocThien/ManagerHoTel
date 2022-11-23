using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Data.Entity.Infrastructure;

namespace Manager_Hotel.ClassLoin
{
    class Modify
    {
        public Modify()
        {
        }
        SqlCommand sqlCommand;
        SqlConnection connection;
        SqlDataAdapter sqlDataAdapter=new SqlDataAdapter();
        DataTable table = new DataTable();
        SqlDataReader dataReader; // doc dL trong ban

        public List<TaiKhoan> TaiKhoans(string query)
        {
            List<TaiKhoan> taiKhoans = new List<TaiKhoan>();

            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while(dataReader.Read())
                {
                    taiKhoans.Add(new TaiKhoan(dataReader.GetString(0), dataReader.GetString(1)) ) ;
                }
                sqlConnection.Close();
            }
            return taiKhoans;
        }
        public List<DichVu> DichVus(string query)
        {
            List<DichVu> DichVus = new List<DichVu>();

            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    DichVus.Add(new DichVu(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetString(5), dataReader.GetInt32(6), dataReader.GetInt32(7), dataReader.GetInt32(8)));
                }
                sqlConnection.Close();
            }
            return DichVus;
        }

        public string GetID(string squery)
        {
            string id = "";
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(squery, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    id = dataReader.GetString(0);
                }
                sqlConnection.Close();
            }
            return id;

        }
        public int GetInt32(string squery)
        {
            int id=0 ;
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(squery, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    id = dataReader.GetInt32(0);
                }
                sqlConnection.Close();
            }
            return id;

        }
        public void Command(string squery) // thêm xóa sửa
        {
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(squery, sqlConnection);
                sqlCommand.ExecuteNonQuery(); // thực thi câu truy vấn
                sqlConnection.Close();
            }
        }
        public DataTable GetDataTable(string squery) // 
        {
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(squery, sqlConnection);
                DataTable dt = new DataTable();
                dt.Load(sqlCommand.ExecuteReader());
                return dt;
            }
        }
        public void loaddataTable(DataGridView BangNV,String query)
        {
            sqlCommand = connection.CreateCommand();//load dữ liệu lên ,tạo xử lý kết nối
            sqlCommand.CommandText = query;//liên kết from nhân viên
            sqlDataAdapter.SelectCommand = sqlCommand;
            table.Clear();
            sqlDataAdapter.Fill(table);
            BangNV.DataSource = table;
            //sqlCommand.ExecuteNonQuery(); // thực thi câu truy vấn

        }
        //Dùng hiển thị trên comboBox từ sql
        public DataTable loadtextBox(String query)
        {
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, sqlConnection);//SQL là câu truy vấn bảng trong cơ sở dữ liệu, cn là connection đến cơ sở dữ liệu
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }


        }
        public void OpenConnection()
        {
            connection=Connection.GetSqlConnection();
            connection.Open();
        }
    }
    
  
}
