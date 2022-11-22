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
    public partial class NhanPhong : Form
    {
        public NhanPhong()
        {
            InitializeComponent();
        }


        Modify modify = new Modify();
        private void NhanPhong_Load(object sender, EventArgs e)
        {
            Load_comboPhong();
            Load_GV();

        }

        private void Load_GV()
        {
            gvDatPhong.ReadOnly = true;
            gvNhanPhong.ReadOnly = true;
            gvDatPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvNhanPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            string squery = "Select ct.MaChiTietDatPhong, kh.HoTen, kh.CMND, ct.TenLoai, ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct , KhachHang kh where ct.MaKH  =kh.MaKH and ct.MaChiTietDatPhong not in (Select pdp.MaChiTietDP from PhieuDatPhong pdp )";
            gvDatPhong.DataSource = modify.GetDataTable(squery);

            string squery_Nhan = "Select ct.MaChiTietDatPhong, kh.HoTen, kh.CMND, ct.TenLoai, ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct , KhachHang kh where ct.MaKH  =kh.MaKH and ct.MaChiTietDatPhong  in (Select pdp.MaChiTietDP from PhieuDatPhong pdp )";
            gvNhanPhong.DataSource = modify.GetDataTable(squery_Nhan);
        }

        private void Load_comboPhong()
        {
            string squery = "Select DISTINCT  lp.TenLoai From ChiTietDatPhong ct, Phong p , LoaiPhong lp Where ct.TenLoai = lp.TenLoai and p.MaLoai = lp.Maloai and p.TrangThai = N'Trống'";
            cbLoaiPhong.DataSource = modify.GetDataTable(squery);
            cbLoaiPhong.DisplayMember = "TenLoai";
        }

        private void btnNhanPhong_Click(object sender, EventArgs e)
        {

        }

        private void gvDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(gvDatPhong.Rows.Count > 0)
            {
                DataGridViewRow row = gvDatPhong.SelectedRows[0];
                string maChiTietDP = row.Cells[0].Value.ToString();
                string squery = "Select kh.HoTen, kh.CMND,  ct.TenLoai, ct.NgayNhan, ct.NgayTra, lp.SoNguoi, lp.DonGia From KhachHang kh, ChiTietDatPhong ct , LoaiPhong lp Where kh.MaKH = ct.MaKH and lp.TenLoai = ct.TenLoai and  ct.MaChiTietDatPhong = '" + maChiTietDP + "'";
                DataTableReader reader = modify.GetDataTable(squery).CreateDataReader();
                while(reader.Read())
                {
                    txtCMND.Text = reader.GetString(0);
                    txtCMND.Text = reader.GetString(1);
                    cbTenLoaiPhong.Text = reader.GetString(2);
                    dateNhan.Text = reader["NgayNhan"].ToString();
                    dateTra.Text = reader["NgayTra"].ToString();
                    udSL.Value = reader.GetInt32(5);
                    txtDonGia.Text = reader.GetInt32(6).ToString();
                }




            }
        }
    }
}
