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

        String querytableNv = "select tk.TenDangNhap, nv.HoTen, nv.ChucVu, nv.CMNDNhanVien, nv.SDT, nv.DiaChi  from TaiKhoan tk,NhanVien nv where tk.TenDangNhap=nv.TenDangNhap";
        ClassLoin.Modify modi = new ClassLoin.Modify();

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            loadGirdView();
            txtTenTK.Enabled = false;
        }
        public void loadGirdView()
        {
            DateTime dt = DateTime.Now;
            dateNgaySinh.Text = dt.ToString("yyyy/MM/dd");
            dateTimePickerNgayVaoLam.Text = dt.ToString("yyyy/MM/dd");

            int n = dataGirdViewDSNhanVien.Width/ 10;
            dataGirdViewDSNhanVien.ReadOnly = true;
            dataGirdViewDSNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGirdViewDSNhanVien.DataSource=modi.GetDataTable(querytableNv);

            dataGirdViewDSNhanVien.Columns[0].HeaderText = "Tên Tài Khoản";
            dataGirdViewDSNhanVien.Columns[0].Width = n*2 ;

            dataGirdViewDSNhanVien.Columns[1].HeaderText = "Tên Nhân Viên";
            dataGirdViewDSNhanVien.Columns[1].Width = n*2;


            dataGirdViewDSNhanVien.Columns[2].HeaderText = "Chức Vụ";
            dataGirdViewDSNhanVien.Columns[2].Width = n*2;

            dataGirdViewDSNhanVien.Columns[3].HeaderText = "Số CMDN";
            dataGirdViewDSNhanVien.Columns[3].Width = n;

            dataGirdViewDSNhanVien.Columns[4].HeaderText = "Số Điện Thoại";
            dataGirdViewDSNhanVien.Columns[4].Width = n;

            dataGirdViewDSNhanVien.Columns[5].HeaderText = "Địa Chỉ ";
            dataGirdViewDSNhanVien.Columns[5].Width = n;
        }

        private void dataGirdViewDSNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int i;
                i = dataGirdViewDSNhanVien.CurrentRow.Index;
                string tenDangNhap = dataGirdViewDSNhanVien.Rows[i].Cells[0].Value.ToString();
                string squery = "select *  from TaiKhoan tk,NhanVien nv where tk.TenDangNhap=nv.TenDangNhap and tk.TenDangNhap = '" + tenDangNhap + "'";
                DataTableReader reader = modi.GetDataTable(squery).CreateDataReader();
                while(reader.Read())
                {
                    txtTenTK.Text = reader.GetString(0);
                    txtPass.Text = reader.GetString(1);
                    txtEmail.Text = reader.GetString(2);
                    //3,4
                    comboBoxChucVu.Text = reader.GetString(5);
                    txtHoTen.Text = reader.GetString(6);
                    txtCMND.Text = reader.GetString(7);
                    comboBoxGioiTinh.Text = reader.GetString(8);
                    dateNgaySinh.Text = reader["NgaySinhNV"].ToString();
                    txtSDT.Text = reader.GetString(10);
                    txtDiaChi.Text = reader.GetString(11);
                    dateTimePickerNgayVaoLam.Text = reader["NgayVaoLam"].ToString();
                }
            }
            catch { }
        }
        Random rd = new Random();
        String idNhanVien = "E";

        private void btnDeleteNV_Click(object sender, EventArgs e)
        {
            int i;
            i = dataGirdViewDSNhanVien.CurrentRow.Index;
            String maDelete = dataGirdViewDSNhanVien.Rows[i].Cells[0].Value.ToString();
            String queryDeleteNV = "delete from NhanVien where TenDangNhap='" + maDelete + "'";
            String queryDeleteTK = "delete from TaiKhoan where TenDangNhap='" + maDelete + "'";
            modi.Command(queryDeleteNV);
            modi.Command(queryDeleteTK);
            loadGirdView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            String querySearch = "select * From NhanVien Where MaNV like '%" + txtSearch.Text + "%'or HoTen like '%" + txtSearch.Text + "%'or CMNDNhanVien like '%" + txtSearch.Text + "%'or SDT like '%" + txtSearch.Text + "%' ";
            
            dataGirdViewDSNhanVien.DataSource = modi.GetDataTable(querySearch);
        }

        private void btnbtnThucHien_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioBtcapnhatNV.Checked == true)
                {
                    int i;
                    i = dataGirdViewDSNhanVien.CurrentRow.Index;

                    String query1 = "update NhanVien Set ChucVu= N'" + comboBoxChucVu.Text + "',HoTen=N'" + txtHoTen.Text + "',CMNDNhanVien='" + txtCMND.Text + "',GioiTinh=N'" + comboBoxGioiTinh.Text + "',NgaySinhNV='" + dateNgaySinh.Text + "',SDT='" + txtSDT.Text + "',DiaChi=N'" + txtDiaChi.Text + "',NgayVaoLam='" + dateTimePickerNgayVaoLam.Text + "' where TenDangNhap='" + dataGirdViewDSNhanVien.Rows[i].Cells[0].Value.ToString() + "'";
                    String query2 = "update TaiKhoan set MatKhau='" + txtPass.Text + "',Email='" + txtEmail.Text + "'where TenDangNhap='" + dataGirdViewDSNhanVien.Rows[i].Cells[0].Value.ToString() + "'";
                    modi.Command(query2);
                    modi.Command(query1);
                    loadGirdView();
                }
                else if (radioBtAddNV.Checked == true)
                {
                    String queryThemNV;
                    try
                    {
                        string query_tk = "insert into TaiKhoan values ('" + txtTenTK.Text + "','" + txtPass.Text + "','" + txtEmail.Text + "')";
                        modi.Command(query_tk);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Thêm tài khoản thất bại ");
                    }
                    while (true)
                    {
                        try
                        {
                            Random r = new Random();
                            int ID = r.Next(100, 1000);
                            idNhanVien += ID;
                            queryThemNV = "insert into NhanVien values ('" + idNhanVien + "','" + txtTenTK.Text + "','" + comboBoxChucVu.Text + "',N'" + txtHoTen.Text + "','" + txtCMND.Text + "',N'" + comboBoxGioiTinh.Text + "','" + dateNgaySinh.Text + "','" + txtSDT.Text + "',N'" + txtDiaChi.Text + "','" + dateTimePickerNgayVaoLam.Text + "')";
                            modi.Command(queryThemNV);
                            break;
                        }
                        catch (Exception ex)
                        {
                            idNhanVien = "E";
                        }
                    }
                    loadGirdView();
                }
            }
            catch { MessageBox.Show("Có Lỗi!!!!"); }
        }

        private void radioBtcapnhatNV_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtcapnhatNV.Checked == true)
            {
                txtTenTK.Enabled = false;
            }
        }

        private void radioBtAddNV_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtAddNV.Checked == true)
            {
                txtTenTK.Clear();
                txtPass.Clear();
                txtEmail.Clear();
                txtHoTen.Clear();
                txtCMND.Clear();
                txtSDT.Clear();
                txtDiaChi.Clear();

                txtTenTK.Enabled = true;
            }
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
