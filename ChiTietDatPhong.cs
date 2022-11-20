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
    public partial class ChiTietDatPhong : Form
    {

        Modify modify = new Modify();
        private string maPhieuDP = "";
        private string maChiTietDP = "";
        private string maKH = "";
        public ChiTietDatPhong(string maPhieuDP)
        {
            InitializeComponent();
            this.maPhieuDP = maPhieuDP;
            
        }

        private void load_id()
        {
            // lấy mã Chi Tiết Đặt Phòng
            DataTableReader reader = modify.GetDataTable("Select MaChiTietDP From PhieuDatPhong Where MaPhieuDP='" + maPhieuDP + "'").CreateDataReader();
            while (reader.Read())
            {
                maChiTietDP = reader.GetString(0);
            }
            // lấy mã KH
            
            DataTableReader readerkh = modify.GetDataTable("Select ctdp.MaKH From PhieuDatPhong pdp, ChiTietDatPhong ctdp where pdp.MaChiTietDP = ctdp.MaChiTietDatPhong and pdp.MaPhieuDP ='" + maPhieuDP + "' ").CreateDataReader();
            while (readerkh.Read())
            {
                maKH = readerkh.GetString(0);
            }
        }

        private void ChiTietDatPhong_Load(object sender, EventArgs e)
        {
            Load_Data();
            Load_KhachHang();
            load_id();
        }

        private void Load_KhachHang()
        {
            string squery_kh = "select  kh.HoTen, kh.CMND, kh.LoaiKH, kh.SDT, kh.NgaySinh, kh.DiaChi, kh.GioiTinh, QuocTich from KhachHang kh, ChiTietDatPhong ctdp, PhieuDatPhong pdp where ctdp.MaChiTietDatPhong = pdp.MaChiTietDP and ctdp.MaKH = kh.MaKH and pdp.MaPhieuDP ='" + maPhieuDP + "'";
            DataTable data = modify.GetDataTable(squery_kh);
            DataTableReader reader = data.CreateDataReader();
            while (reader.Read())
            {

                txtHoTen.Text = reader.GetString(0);
                txtCMND.Text = reader.GetString(1);
                cbBoxLoaiKhachHang.Text = reader.GetString(2);
                txtSoDienThoai.Text = reader.GetString(3);
                dateSinh.Text = reader["NgaySinh"].ToString();
                txtDiaChi.Text = reader.GetString(5);
                cbBoxGioiTinh.Text = reader.GetString(6);
                cbBoxQuocTich.Text = reader.GetString(7);
            }
        }

        private void Load_Data()
        {
            string squery_loadDataThongTinPhong = "select lp.TenLoai, ctdp.NgayNhan, ctdp.NgayTra, ctdp.SoDem from PhieuDatPhong pdp, Phong p, LoaiPhong lp , ChiTietDatPhong ctdp where pdp.MaPhong = p.MaPhong and p.MaLoai = lp.Maloai and pdp.MaPhieuDP ='" + maPhieuDP + "' and ctdp.MaChiTietDatPhong = pdp.MaChiTietDP";
            DataTable data = modify.GetDataTable(squery_loadDataThongTinPhong);
            DataTableReader reader = data.CreateDataReader();
            txtMaDP.Text = maPhieuDP;
            string s = "";
            while (reader.Read())
            {
                cbBoxLoaiPhong.Text = reader.GetString(0);
                dateNhan.Text = reader["NgayNhan"].ToString();
                dateTra.Text = reader["NgayTra"].ToString();
                /* dateNhan. = reader.GetString(1);
                 dateTra.Text = reader.GetString(2);*/
                udSoDem.Value = reader.GetInt32(3);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            modify.Command("UPDATE ChiTietDatPhong Set NgayNhan ='" + dateNhan.Value.ToString("yyyy-MM-dd") + "' , NgayTra='" + dateTra.Value.ToString("yyyy-MM-dd") + "' , SoDem = '" + udSoDem.Value + "' Where MaChiTietDatPhong = '" + maChiTietDP + "'");
            MessageBox.Show("Đã Lưu Lại Thông Tin Đặt Phòng: ", "Lưu Lại", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhatKH_Click(object sender, EventArgs e)
        {
            string squery = "Update KhachHang set HoTen = N'"+txtHoTen.Text+ "' , CMND = '"+txtCMND.Text+ "' , LoaiKH = N'"+cbBoxLoaiPhong.Text+ "' , SDT = '"+txtSoDienThoai.Text+ "' , NgaySinh = '"+dateSinh.Value.ToString("yyyy-MM-dd") + "', DiaChi=N'"+txtDiaChi.Text+ "', GioiTinh = N'"+cbBoxGioiTinh.Text+ "', QuocTich = N'"+cbBoxQuocTich.Text+ "' where MaKH = '"+maKH+"'";
            modify.Command(squery);
            MessageBox.Show("Đã Cập Nhật Khách Hàng: " + txtHoTen.Text, "Cập Nhật", MessageBoxButtons.OK, MessageBoxIcon.Information  );
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            // xóa phiếu đặt phòng
            string squery_delPDP = " Delete From PhieuDatPhong  Where MaPhieuDP = '" + maPhieuDP + "' ";
            modify.Command(squery_delPDP);
            // xóa phiếu chi tiết đặt phòng
            string squery_delCTDP = " Delete From ChiTietDatPhong  Where MaChiTietDatPhong = '" + maChiTietDP + "' ";
            modify.Command(squery_delCTDP);
            // xóa Khách hàng
            string squery_delKH = " Delete From Khachhang  Where MaKH = '" + maKH + "' ";
            modify.Command(squery_delKH);
            MessageBox.Show("Đã xóa khách hàng: " + txtHoTen.Text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
