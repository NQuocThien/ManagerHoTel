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
    public partial class QuanLyNhanVien : Form
    {
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            Load_ListView();
        }

        private void Load_ListView()
        {
            int n = ltvDSNhanVien.Width;
            ltvDSNhanVien.View = View.Details;
            ltvDSNhanVien.GridLines = true;

            this.ltvDSNhanVien.Columns.Add("Tên Đăng Nhập");
            this.ltvDSNhanVien.Columns[0].Width = (int)n / 6;

            this.ltvDSNhanVien.Columns.Add("Tên");
            this.ltvDSNhanVien.Columns[1].Width = (int)n / 6;

            this.ltvDSNhanVien.Columns.Add("Loại");
            this.ltvDSNhanVien.Columns[2].Width = (int)n / 6;

            this.ltvDSNhanVien.Columns.Add("CMND");
            this.ltvDSNhanVien.Columns[3].Width = (int)n / 6;

            this.ltvDSNhanVien.Columns.Add("SDT");
            this.ltvDSNhanVien.Columns[4].Width = (int)n / 6;

            this.ltvDSNhanVien.Columns.Add("Địa Chỉ");
            this.ltvDSNhanVien.Columns[5].Width = (int)n / 6;

        }
    }
}
