using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manager_Hotel
{
    public partial class QuanLiPhong : Form
    {
        public QuanLiPhong()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void QuanLiPhong_Load(object sender, EventArgs e)
        {
            Load_ltvNhanVien();
        }

        private void Load_ltvNhanVien()
        {
            int n = ltvNhanVien.Width /4;
            ltvNhanVien.View = View.Details;

            ltvNhanVien.Columns.Add("Mã Phòng");
            ltvNhanVien.Columns[0].Width = n ;

            ltvNhanVien.Columns.Add("Loại Phòng");
            ltvNhanVien.Columns[1].Width = n ;

            ltvNhanVien.Columns.Add("Trạng Thái");
            ltvNhanVien.Columns[2].Width = n;

            ltvNhanVien.Columns.Add("Đơn Giá");
            ltvNhanVien.Columns[3].Width = n;
        }
    }
}
