using Manager_Hotel.ClassLoin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Manager_Hotel
{
    public partial class QuanLyNhanVien : Form
    {
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }

        SqlCommand sqlCommand;
        String querytableNv = "select * from NhanVien";
        ClassLoin.Modify modi = new ClassLoin.Modify();

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dateNgaySinh.Text = dt.ToString("yyyy-MM-dd");
            dateTimePickerNgayVaoLam.Text= dt.ToString("yyyy-MM-dd");
            modi.OpenConnection();
            dataGirdViewDSNhanVien.ReadOnly = true;
            modi.loaddataTable(dataGirdViewDSNhanVien, querytableNv);
            txtTenDangNhap.Enabled = false;
            txtChucVu.Enabled = false;

        }



        private void btnQuyen_Click(object sender, EventArgs e)
        {
            QuyenTruyCap quyen = new QuyenTruyCap();
            quyen.ShowDialog();
        }

        private void dataGirdViewDSNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGirdViewDSNhanVien.CurrentRow.Index;
            
            txtTenDangNhap.Text = dataGirdViewDSNhanVien.Rows[i].Cells[1].Value.ToString();
            txtChucVu.Text = dataGirdViewDSNhanVien.Rows[i].Cells[2].Value.ToString();
            txtHoTen.Text = dataGirdViewDSNhanVien.Rows[i].Cells[3].Value.ToString();
            txtCMND.Text = dataGirdViewDSNhanVien.Rows[i].Cells[4].Value.ToString();
            comboBoxGioiTinh.Text = dataGirdViewDSNhanVien.Rows[i].Cells[5].Value.ToString();
            dateNgaySinh.Text = dataGirdViewDSNhanVien.Rows[i].Cells[6].Value.ToString();
            txtSDT.Text = dataGirdViewDSNhanVien.Rows[i].Cells[7].Value.ToString();
            txtDiaChi.Text = dataGirdViewDSNhanVien.Rows[i].Cells[8].Value.ToString();
            dateTimePickerNgayVaoLam.Text= dataGirdViewDSNhanVien.Rows[i].Cells[9].Value.ToString();
        }

        private void btnCapNhatNhanVien_Click(object sender, EventArgs e)
        {
          
            int i;
            i = dataGirdViewDSNhanVien.CurrentRow.Index;
           
           String query1= "update NhanVien Set TenDangNhap='" + txtTenDangNhap.Text + "',ChucVu=N'" + txtChucVu.Text + "',HoTen=N'" + txtHoTen.Text + "',CMNDNhanVien='" + txtCMND.Text + "',GioiTinh=N'" + comboBoxGioiTinh.Text + "',NgaySinhNV='" + dateNgaySinh.Text + "',SDT='" + txtSDT.Text + "',DiaChi=N'" + txtDiaChi.Text + "',NgayVaoLam='" + dateTimePickerNgayVaoLam.Text + "' where MaNV='" + dataGirdViewDSNhanVien.Rows[i].Cells[0].Value.ToString() + "'";
            
            modi.conTable(querytableNv, query1, dataGirdViewDSNhanVien);
        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            String manv = "3";//khóa chính không được trùng
            String tenDangNhap = "hoangthang";//tên đăng nhập để thêm
            String ChucVu = "chucvu";//Chức vụ nhân viên
            String queryThemNV = "insert into NhanVien values ('" + manv + "','" + tenDangNhap + "','" + ChucVu + "',N'" + txtHoTen.Text + "','" + txtCMND.Text + "',N'" + comboBoxGioiTinh.Text + "','" + dateNgaySinh.Text + "','" + txtSDT.Text + "',N'" + txtDiaChi.Text + "','" + dateTimePickerNgayVaoLam.Text + "')";
            modi.conTable(querytableNv, queryThemNV, dataGirdViewDSNhanVien);

        }

        private void btnDeleteNV_Click(object sender, EventArgs e)
        {
            int i;
            i = dataGirdViewDSNhanVien.CurrentRow.Index;
            int maDelete= int .Parse(dataGirdViewDSNhanVien.Rows[i].Cells[0].Value.ToString());
            String queryDeleteNV = "delete from NhanVien where MaNV='" + maDelete + "'";
            modi.conTable(querytableNv, queryDeleteNV, dataGirdViewDSNhanVien);
        }
    }
}
