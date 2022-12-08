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
        private string maChiTietDP = "";

        private string maKH = "";
        public ChiTietDatPhong(string maPhieuDP)
        {
            InitializeComponent();
            this.maChiTietDP = maPhieuDP;
            
        }

        private void load_id() { }

        private void ChiTietDatPhong_Load(object sender, EventArgs e)
        {
            Load_Data();
            Load_KhachHang();
            load_id();
            maKH = modify.GetID("Select kh.MaKH From KhachHang kh, ChiTietDatPhong ct where kh.MaKH= ct.MaKH and ct.MaChiTietDatPhong = '" + maChiTietDP + "' ");
        }

        private void Load_KhachHang()
        {
           maKH = modify.GetID("Select kh.MaKH From KhachHang kh, ChiTietDatPhong ct where kh.MaKH= ct.MaKH and ct.MaChiTietDatPhong = '" + maChiTietDP + "' ");
           string squery_kh = "Select * From KhachHang where MaKH = '"+maKH+"'";
            DataTableReader reader = modify.GetDataTable(squery_kh).CreateDataReader();
           // MessageBox.Show(squery_kh);
            while (reader.Read())
            {
                
                txtHoTen.Text = reader.GetString(1);
                txtCMND.Text = reader.GetString(2);
                cbBoxLoaiKhachHang.Text = reader.GetString(3);
                txtSoDienThoai.Text = reader.GetString(4);
                dateSinh.Text = reader["NgaySinh"].ToString();
                txtDiaChi.Text = reader.GetString(6);
                cbBoxGioiTinh.Text = reader.GetString(7);
                cbBoxQuocTich.Text = reader.GetString(8);
            }
        }
        private void Load_Data()
        {
            string squery_loadDataThongTinPhong = "Select ct.MaChiTietDatPhong, ct.TenLoai,ct.NgayNhan, ct.NgayTra , ct.SoDem from ChiTietDatPhong ct where ct.MaChiTietDatPhong ='"+maChiTietDP+"'";
            DataTable data = modify.GetDataTable(squery_loadDataThongTinPhong);
            DataTableReader reader = data.CreateDataReader();
            txtMaDP.Text = maChiTietDP;
            string s = "";
            while (reader.Read())
            {
                cbBoxLoaiPhong.Text = reader.GetString(1);
                dateNhan.Text = reader["NgayNhan"].ToString();
                dateTra.Text = reader["NgayTra"].ToString();
                udSoDem.Value =  reader.GetInt32(4);
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            modify.Command("UPDATE ChiTietDatPhong Set NgayNhan ='" + dateNhan.Value.ToString("yyyy-MM-dd") + "' , NgayTra='" + dateTra.Value.ToString("yyyy-MM-dd") + "' , SoDem = '" + udSoDem.Value + "' , TenLoai ='"+cbBoxLoaiPhong.Text+"' Where MaChiTietDatPhong = '" + maChiTietDP + "'");
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
            // KT KH có hóa đơn chưa
            int ktHD = modify.GetInt32("select Count(MaHD) from HoaDon where MaKH = '" + maKH + "'");
            if(ktHD  > 0)
            {
                string maHD = modify.GetID("select MaHD from HoaDon where MaKH = '" + maKH + "'");
                // xóa các hóa đơn dịch vụ
                modify.Command("Delete HoaDonDV where MaHD = '" + maHD + "' ");
                // xóa Phiếu đặt phòng 
                modify.Command("Delete PhieuDatPhong Where MaChiTietDP = '" + maChiTietDP + "'");
        
                // xóa Hóa Đơn phòng
                modify.Command("Delete HoaDonPhong Where MaHD = '" + maHD + "'");
                // xóa Chi Tiết Đặt Phòng 
                modify.Command("Delete ChiTietDatPhong Where MaChiTietDatPhong = '" + maChiTietDP + "'");
                // xóa hóa đơn
                modify.Command("Delete HoaDon Where MaHD = '" + maHD + "' ");
                
                // xáo khách hàng
                string squery_delKH = " Delete From Khachhang  Where MaKH = '" + maKH + "' ";
                modify.Command(squery_delKH);
                
            }else
            {
                // xóa bản chi tiet dat phong
                modify.Command("Delete ChiTietDatPhong Where MaChiTietDatPhong = '" + maChiTietDP + "'");
                // xóa bản khách hàng 
                string squery_delKH = " Delete From Khachhang  Where MaKH = '" + maKH + "' ";
                modify.Command(squery_delKH);
            }

            MessageBox.Show("Đã xóa khách hàng: " + txtHoTen.Text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
