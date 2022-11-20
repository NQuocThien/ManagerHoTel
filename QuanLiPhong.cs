using Manager_Hotel.ClassLoin;
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
        String querytableDV = "select p.MaPhong, p.TrangThai, lp.TenLoai, lp.SoNguoi, lp.DonGia from Phong p, LoaiPhong lp where p.MaLoai = lp.MaLoai ";
        Modify modify = new Modify();
        

        private void QuanLiPhong_Load(object sender, EventArgs e)
        {
            Load_gvPhong();
        }

        private void Load_gvPhong()
        {
            dataGridViewPhong.ReadOnly = true;
            dataGridViewPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPhong.DataSource = modify.GetDataTable(querytableDV);

            int n = dataGridViewPhong.Width / 5;
            dataGridViewPhong.Columns[0].Width = n;
            dataGridViewPhong.Columns[1].Width = n;
            dataGridViewPhong.Columns[2].Width = n;
            dataGridViewPhong.Columns[3].Width = n;
            dataGridViewPhong.Columns[4].Width = n;
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;

            i = dataGridViewPhong.CurrentRow.Index;
            txtMaPhong.Text = dataGridViewPhong.Rows[i].Cells[0].Value.ToString();
            cbTrangThai.Text = dataGridViewPhong.Rows[i].Cells[1].Value.ToString();
            cbLoaiPhong.Text = dataGridViewPhong.Rows[i].Cells[2].Value.ToString();
            udSLNguoi.Value = Convert.ToInt32(dataGridViewPhong.Rows[i].Cells[3].Value);
            txtDonGia.Text = dataGridViewPhong.Rows[i].Cells[4].Value.ToString();
        }
    }
}
