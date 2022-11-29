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
    public partial class InHoaDon : Form
    {
        private string maKH = "";
        private string HoTenNV = "";
        private int ThanhTien = 0;
        private string maHD;
        private int GiamGia = 0;
        public InHoaDon(string maKH, string hoTen, int tongtien, string maHD, int giamGia)
        {
            InitializeComponent();
            this.maKH = maKH;
            this.HoTenNV = hoTen;
            this.ThanhTien = tongtien;
            this.maHD = maHD;
            this.GiamGia = giamGia;
        }
        Modify modify = new Modify();
        private void InHoaDon_Load(object sender, EventArgs e)
        {
            //
            Load_ThongTinHoaDon();
            Load_ThongTinKH();
            Load_ThongTinPhong();
            Load_DSDichVu();
            Load_ThongTinThanhToan();
        }

        private void Load_ThongTinThanhToan()
        {
            string squery = "Select dv.ThanhTien, hd.TongTien from HoaDon hd, HoaDonDV dv where hd.MaHD = dv.MaHD and hd.MaKH ='" + maKH + "' ";
            DataTableReader reader = modify.GetDataTable(squery).CreateDataReader();
            int tongtienDV = 0;
            int TonngTien = 0;
            while (reader.Read())
            {
                tongtienDV += reader.GetInt32(0) ;
                TonngTien = reader.GetInt32(1);
            }
            string squery1 = "Select hdp.ThanhTien from HoaDon hd, HoaDonPhong hdp where hd.MaHD = hdp.MaHD and hd.MaKH ='" + maKH + "'";
            DataTableReader reader1 = modify.GetDataTable(squery1).CreateDataReader();
            int TienPhong = 0;
            while(reader1.Read())
            {
                TienPhong = reader1.GetInt32(0);
            }

            lblTienPhong.Text = TienPhong + "";
            lblTongTien.Text = TonngTien + "";
            lblGiamGia.Text = GiamGia + "";
            lblTienDV.Text = tongtienDV + "";
            lblThanhTien.Text = ThanhTien + "";

        }

        private void Load_DSDichVu()
        {
            int n = dataGridViewAddDV.Width / 10;
            dataGridViewAddDV.ReadOnly = true;
            dataGridViewAddDV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAddDV.DataSource = modify.GetDataTable("Select hv.MaHDDV, dv.TenDV, dv.DonGia, hv.SoLuong, hv.ThanhTien From DichVu dv, HoaDonDV hv , HoaDon hd where dv.MaDV = hv.MaDV and hd.MaHD = hv.MaHD and hd.MaKH = '" + maKH + "'");
            dataGridViewAddDV.Columns[0].Visible = false;
            dataGridViewAddDV.Columns[1].HeaderText = "Tên Dịch Vụ";
            dataGridViewAddDV.Columns[1].Width = n * 5;
            dataGridViewAddDV.Columns[2].HeaderText = "Đơn Giá";
            dataGridViewAddDV.Columns[2].Width = n * 2;
            dataGridViewAddDV.Columns[3].HeaderText = "Số Lượng";
            dataGridViewAddDV.Columns[3].Width = n * 1;
            dataGridViewAddDV.Columns[4].HeaderText = "Thành Tiền";
            dataGridViewAddDV.Columns[4].Width = n * 2;
        }

        private void Load_ThongTinPhong()
        {
            string squery = "Select p.MaPhong, lp.TenLoai, lp.DonGia, ct.NgayNhan,ct.SoDem , lp.SoNguoi from ChiTietDatPhong ct, Phong p , PhieuDatPhong pdp, LoaiPhong lp , HoaDonPhong hdp,HoaDon hd  where ct.MaChiTietDatPhong = pdp.MaChiTietDP and pdp.MaPhong = p.MaPhong and p.MaLoai=  lp.Maloai and hd.MaHD =hdp.MaHD and hdp.MaChiTietDP = ct.MaChiTietDatPhong and hd.MaKH = '"+maKH+"' ";
            DataTableReader reader = modify.GetDataTable(squery).CreateDataReader();
            while(reader.Read())
            {
                lblTenPhong.Text = reader.GetString(0);
                lblLoaiPhong.Text = reader.GetString(1);
                lblDonGia.Text = reader.GetInt32(2) + "";
                lblNgayDen.Text = reader["NgayNhan"].ToString();
                lblSoDem.Text = reader.GetInt32(4) + "";
                lblSoNguoi.Text = reader.GetInt32(5) + "";
            }
        }

        private void Load_ThongTinKH()
        {
            string squery = "Select * From KhachHang Where MaKH = '" + maKH + "'";
            DataTableReader reader = modify.GetDataTable(squery).CreateDataReader();
            while(reader.Read())
            {
                lblTenKhachHang.Text = reader.GetString(1);
                lblCMND.Text = reader.GetString(2);
                lblLoaiKhachHang.Text = reader.GetString(3);
                lblSoDienThoai.Text = reader.GetString(4);
                lblDiaChi.Text = reader.GetString(6);
                lblQuocTich.Text = reader.GetString(8);
            }
        }

        private void Load_ThongTinHoaDon()
        {
            lblNgayLap.Text = DateTime.Now.ToString();
            lblNhanVienLap.Text = HoTenNV;
            lblMaHoaDon.Text = maHD;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Bitmap bmp;
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(maKH);
            modify.Command("Update HoaDon set TrangThaiTT = N'Đã Thanh Toán' , NgayThanhToan = '"+DateTime.Now.ToString("yyyy-MM-dd")+ "', NguoiThanhToan = N'"+HoTenNV+"'   where MaKH = '" + maKH + "'");
            //MessageBox.Show("Đã Thanh Toán Xong"); /// thay thế code in hoa đơn
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Size.Width, this.Size.Height));
            printPreviewDialog1.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 10);
        }

       
    }
}
