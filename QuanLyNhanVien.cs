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

        //SqlCommand sqlCommand;
        String querytableNv = "select * from NhanVien";
        ClassLoin.Modify modi = new ClassLoin.Modify();

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            loadGirdView();
            //txtTenDangNhap.Enabled = false;
            //txtChucVu.Enabled = false;
            modi.OpenConnection();

        }
        public void loadGirdView()
        {
            DateTime dt = DateTime.Now;
            dateNgaySinh.Text = dt.ToString("yyyy/MM/dd");
            dateTimePickerNgayVaoLam.Text = dt.ToString("yyyy/MM/dd");
            
            dataGirdViewDSNhanVien.ReadOnly = true;
            dataGirdViewDSNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGirdViewDSNhanVien.DataSource=modi.GetDataTable(querytableNv);
            //modi.loaddataTable(dataGirdViewDSNhanVien, querytableNv);
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

            txtTenTK.Text = dataGirdViewDSNhanVien.Rows[i].Cells[1].Value.ToString();
            comboBoxChucVu.Text = dataGirdViewDSNhanVien.Rows[i].Cells[2].Value.ToString();
            txtHoTen.Text = dataGirdViewDSNhanVien.Rows[i].Cells[3].Value.ToString();
            txtCMND.Text = dataGirdViewDSNhanVien.Rows[i].Cells[4].Value.ToString();
            comboBoxGioiTinh.Text = dataGirdViewDSNhanVien.Rows[i].Cells[5].Value.ToString();
            dateNgaySinh.Text = dataGirdViewDSNhanVien.Rows[i].Cells[6].Value.ToString();
            txtSDT.Text = dataGirdViewDSNhanVien.Rows[i].Cells[7].Value.ToString();
            txtDiaChi.Text = dataGirdViewDSNhanVien.Rows[i].Cells[8].Value.ToString();
            dateTimePickerNgayVaoLam.Text = dataGirdViewDSNhanVien.Rows[i].Cells[9].Value.ToString();
        }

        private void btnCapNhatNhanVien_Click(object sender, EventArgs e)
        {


            int i;
            i = dataGirdViewDSNhanVien.CurrentRow.Index;

            String query1 = "update NhanVien Set TenDangNhap='" + txtTenTK.Text + "',ChucVu=N'" + txtPass.Text + "',HoTen=N'" + txtHoTen.Text + "',CMNDNhanVien='" + txtCMND.Text + "',GioiTinh=N'" + comboBoxGioiTinh.Text + "',NgaySinhNV='" + dateNgaySinh.Text + "',SDT='" + txtSDT.Text + "',DiaChi=N'" + txtDiaChi.Text + "',NgayVaoLam='" + dateTimePickerNgayVaoLam.Text + "' where MaNV='" + dataGirdViewDSNhanVien.Rows[i].Cells[0].Value.ToString() + "'";
            modi.Command(query1);
            loadGirdView();
            //modi.conTable(querytableNv, query1, dataGirdViewDSNhanVien);
        }
        Random rd = new Random();
        String idNhanVien = "E";
        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {

            String queryThemNV;
            try
            {
                string query_tk = "insert into TaiKhoan values ('" + txtTenTK.Text + "','" + txtPass.Text + "','" + txtEmail.Text + "')";
                modi.Command(query_tk);
            }
            catch (Exception ex)
            {

            }
            while (true)
            {

                try
                {
                    int ID = rd.Next(100, 1000);
                    idNhanVien += ID;
                    String ChucVu = comboBoxGioiTinh.Text;//Chức vụ nhân viên
                    queryThemNV = "insert into NhanVien values ('" + idNhanVien + "','" + txtTenTK.Text + "','" + ChucVu + "',N'" + txtHoTen.Text + "','" + txtCMND.Text + "',N'" + comboBoxGioiTinh.Text + "','" + dateNgaySinh.Text + "','" + txtSDT.Text + "',N'" + txtDiaChi.Text + "','" + dateTimePickerNgayVaoLam.Text + "')";
                    modi.Command(queryThemNV);
                    break;

                }
                catch (Exception ex)
                {
                    idNhanVien = "E";
                }

            }
            loadGirdView();
            //modi.conTable(querytableNv, queryThemNV, dataGirdViewDSNhanVien);


        }

        private void btnDeleteNV_Click(object sender, EventArgs e)
        {



            int i;
            i = dataGirdViewDSNhanVien.CurrentRow.Index;
            String maDelete = dataGirdViewDSNhanVien.Rows[i].Cells[1].Value.ToString();
            String queryDeleteNV = "delete from NhanVien where TenDangNhap='" + maDelete + "'";
            String queryDeleteTK = "delete from TaiKhoan where TenDangNhap='" + maDelete + "'";
            modi.Command(queryDeleteTK);
            modi.Command(queryDeleteNV);
            loadGirdView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            String querySearch = "select * From NhanVien Where MaNV like '%" + txtSearch.Text + "%'or HoTen like '%" + txtSearch.Text + "%'or CMNDNhanVien like '%" + txtSearch.Text + "%'or SDT like '%" + txtSearch.Text + "%' ";
            
            dataGirdViewDSNhanVien.DataSource = modi.GetDataTable(querySearch);
        }
    }
}
