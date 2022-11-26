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
    public partial class QuanLyHoaDon : Form
    {
        public QuanLyHoaDon()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        private void button1_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        private void QuanLyHoaDon_Load(object sender, EventArgs e)
        {
            Load_Gridview();
        }

        private void Load_Gridview()
        {
            int n = gvHoaDon.Width / 10;
            string squery = "Select hd.MaHD, hd.TongTien, kh.HoTen , hd.TrangThaiTT, hd.NguoiThanhToan, hd.NgayThanhToan from HoaDon hd, KhachHang kh where hd.MaKH = kh.MaKH ";
            gvHoaDon.DataSource = modify.GetDataTable(squery);
            gvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvHoaDon.ReadOnly = true;

            gvHoaDon.Columns[0].HeaderText = "MaHD";
            gvHoaDon.Columns[0].Width = n;

            gvHoaDon.Columns[1].HeaderText = "Tổng Tiền";
            gvHoaDon.Columns[1].Width = n * 2;

            gvHoaDon.Columns[2].HeaderText = "Họ Tên";
            gvHoaDon.Columns[2].Width = n * 2;

            gvHoaDon.Columns[3].HeaderText = "Trạng Thái";
            gvHoaDon.Columns[3].Width = n;

            gvHoaDon.Columns[4].HeaderText = "Người Thanh Toán";
            gvHoaDon.Columns[4].Width = n * 2;

            gvHoaDon.Columns[5].HeaderText = "Ngày Thanh toán";
            gvHoaDon.Columns[5].Width = n * 2;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int n = gvHoaDon.Width / 10;
            string squery = "Select hd.MaHD, hd.TongTien, kh.HoTen , hd.TrangThaiTT, hd.NguoiThanhToan, hd.NgayThanhToan from HoaDon hd, KhachHang kh where hd.MaKH =kh.MaKH and( kh.HoTen like N'%" + txtSearch.Text+"%' or hd.NguoiThanhToan like N'%"+txtSearch.Text+"%') ";
            gvHoaDon.DataSource = modify.GetDataTable(squery);
            gvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvHoaDon.ReadOnly = true;

            gvHoaDon.Columns[0].HeaderText = "MaHD";
            gvHoaDon.Columns[0].Width = n;

            gvHoaDon.Columns[1].HeaderText = "Tổng Tiền";
            gvHoaDon.Columns[1].Width = n * 2;

            gvHoaDon.Columns[2].HeaderText = "Họ Tên";
            gvHoaDon.Columns[2].Width = n * 2;

            gvHoaDon.Columns[3].HeaderText = "Trạng Thái";
            gvHoaDon.Columns[3].Width = n;

            gvHoaDon.Columns[4].HeaderText = "Người Thanh Toán";
            gvHoaDon.Columns[4].Width = n * 2;

            gvHoaDon.Columns[5].HeaderText = "Ngày Thanh toán";
            gvHoaDon.Columns[5].Width = n * 2;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
