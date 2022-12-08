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
    public partial class ThongTinTaiKhoan : Form
    {
        private string tenTaiKhoan = "";
        Modify modify = new Modify();
        string maNV = "";
        public ThongTinTaiKhoan(string tenTaiKhoan)
        {
            InitializeComponent();
            this.tenTaiKhoan = tenTaiKhoan;

        }

        private void ThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            load_Data();
            load_MaNV();
            txtEmail.Enabled = false;
            txtTenDangNhap.Enabled =false;
            cbChucVu.Enabled = false;
            lblChucVu.Enabled = false;
            txtTenNhanVien.Enabled = false;
            lblHoTen.Enabled = false;
            txtSoCMND.Enabled = false;
            cbGioiTinh.Enabled = false;
            dateNgaySinh.Enabled = false;
            txtSDT.Enabled = false;
            txtDiaChi.Enabled = false;
            dateNgayVaoLam.Enabled = false;
        }

        private void load_MaNV()
        {
            string squery = " Select MaNV From NhanVien Where TenDangNhap = '" + tenTaiKhoan + "' ";
            DataTableReader reader = modify.GetDataTable(squery).CreateDataReader();
            while(reader.Read())
            {
                maNV = reader.GetString(0);
            }
        }

        private void load_Data()
        {
            string squery = "Select * From NhanVien where TenDangNhap ='" + tenTaiKhoan+"'";
            string squery_tk = "Select Email From TaiKhoan Where TenDangNhap = '" + tenTaiKhoan + "'";
            DataTableReader reader = modify.GetDataTable(squery).CreateDataReader();
            DataTableReader readerTK = modify.GetDataTable(squery_tk).CreateDataReader();
            while(reader.Read())
            {
                txtTenDangNhap.Text = reader.GetString(1);
                cbChucVu.Text = reader.GetString(2);
                lblChucVu.Text = reader.GetString(2);
                txtTenNhanVien.Text = reader.GetString(3);
                lblHoTen.Text = reader.GetString(3);
                txtSoCMND.Text = reader.GetString(4);
                cbGioiTinh.Text = reader.GetString(5);
                dateNgaySinh.Text = reader["NgaySinhNV"].ToString();
                txtSDT.Text = reader.GetString(7);
                txtDiaChi.Text = reader.GetString(8);
                dateNgayVaoLam.Text = reader["NgayVaoLam"].ToString();

            }
            while(readerTK.Read())
            {
                txtEmail.Text = readerTK.GetString(0);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // up data email
            try
            {
                string squery_e = "Update TaiKhoan set Email = '" + txtEmail.Text + "' Where TenDangNhap = '" + tenTaiKhoan + "' ";
                modify.Command(squery_e);
                string squery_nv = " Update NhanVien set ChucVu = '" + cbChucVu.Text + "' , HoTen = N'" + txtTenNhanVien.Text + "', CMNDNhanVien = '" + txtSoCMND.Text + "'  , GioiTinh = N'" + cbGioiTinh.Text + "' , NgaySinhNV = '" + dateNgaySinh.Value.ToString("yyyy-MM-dd") + "',  SDT = '" + txtSDT.Text + "', DiaChi = N'" + txtDiaChi.Text + "' , NgayVaoLam = '" + dateNgayVaoLam.Value.ToString("yyyy-MM-dd") + "' ";
                modify.Command(squery_nv);
                MessageBox.Show("Cập nhật thông tin nhân viên thành công", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information); ;

             }
            catch
            {
                MessageBox.Show("Cập nhật thông tin nhân viên Không thành công", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error); ;

            }
}

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
