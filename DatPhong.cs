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
    public partial class DatPhong : Form
    {
        public DatPhong()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();

        private void btnChiTietDatPhong_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewDSDatPhong.SelectedRows[0];
            string maChiTietDP = row.Cells[0].Value.ToString();
            ChiTietDatPhong chitiet = new ChiTietDatPhong(maChiTietDP);
            chitiet.ShowDialog();
            Load_gvDSDatPhong();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            Random rd = new Random();

            string LoaiPhong = cbLoaiPhong.Text;

            string NgayNhan = dateNhan.Value.ToString("yyyy-MM-dd");
            string NgayTra = dateTra.Value.ToString("yyyy-MM-dd");
            int SoDem = int.Parse(udSoDem.Value.ToString());

            string HoTen = txtHoTen.Text;
            string CMND = txtCMND.Text;
            string LoaiKH = cbBoxLoaiKhachHang.Text;
            string sdt = txtSoDienThoai.Text;
            string NgaySinh = dateSinh.Value.ToString("yyyy-MM-dd");
            string DiaChi = txtDiaChi.Text;
            string GioiTinh = cbBoxGioiTinh.Text;
            string QuocTich = cbBoxQuocTich.Text;

            // các khóa chính 
            string id_kh = "C"; // Khóa chính bảng khách hàng
            string id_ctdp = "A"; // chi tiết đặt phòng
            while (true) // insert table KhachHang
            {
                try
                {
                    int id = rd.Next(100, 1000);
                    id_kh += +id;
                    string squery = "insert into KhachHang values('" + id_kh + "', N'" + HoTen + "', '" + CMND + "', N'" + LoaiKH + "', '" + sdt + "', '" + NgaySinh + "', N'" + DiaChi + "', N'" + GioiTinh + "', N'" + QuocTich + "')";
                    modify.Command(squery);
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" Loi KH Thử lại " + ex, "Lỗi", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    id_kh = "C";
                    return;
                }
            }

            while (true)// insert table Chi tiet dat Phong
            {
                try
                {
                    int id = rd.Next(100, 1000);
                    id_ctdp += +id;
                    string squeryChiTietDatPhong = "insert into ChiTietDatPhong values('" + id_ctdp + "', '" + NgayNhan + "', '" + NgayTra + "', '" + SoDem + "', '" + id_kh + "', '"+LoaiPhong+"' )";
                    modify.Command(squeryChiTietDatPhong);
                    break;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(" Loi Đat Phong Thử lại " + ex, "Lỗi", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    id_ctdp = "A";
                    return;
                }
            }
            if(cboxChuyenPhong.Checked == true)
            {
                this.Close();
                NhanPhong np = new NhanPhong();
                np.ShowDialog();
            }
            xoatrang();
            Load_gvDSDatPhong();
        }

        private void DatPhong_Load(object sender, EventArgs e)
        {
            Load_cbLoaiPhong();
            Load_gvDSDatPhong();
        }
        private void Load_gvDSDatPhong()
        {
            dataGridViewDSDatPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDSDatPhong.ReadOnly = true;
            string squery = "Select ct.MaChiTietDatPhong, kh.HoTen, kh.CMND, ct.TenLoai,ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct, KhachHang kh where ct.MaKH = kh.Makh ";
            DataTable dt = modify.GetDataTable(squery);
            dataGridViewDSDatPhong.DataSource = dt;
            dataGridViewDSDatPhong.Columns[0].HeaderText = "Mã Phòng";
            dataGridViewDSDatPhong.Columns[1].HeaderText = "Họ Tên";
            dataGridViewDSDatPhong.Columns[2].HeaderText = "Số CMND";
            dataGridViewDSDatPhong.Columns[3].HeaderText = "Loại Phòng";
            dataGridViewDSDatPhong.Columns[4].HeaderText = "Ngày Nhận";
            dataGridViewDSDatPhong.Columns[5].HeaderText = "Ngày Trả";
        }

        private void Load_cbLoaiPhong()
        {
            try
            {
                string query = "Select * from LoaiPhong p";
                DataTable dt = modify.GetDataTable(query);
                cbLoaiPhong.DataSource = dt;
                cbLoaiPhong.DisplayMember = "TenLoai";
            }
            catch
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void xoatrang()
        {
            txtHoTen.Clear();
            txtCMND.Clear();
            cbBoxLoaiKhachHang.SelectedIndex = -1;
            txtSoDienThoai.Clear();
            cbBoxGioiTinh.SelectedIndex = -1;
            cbBoxQuocTich.SelectedIndex = -1;
            txtDiaChi.Clear();
            udSoDem.Value = 0;
            cbLoaiPhong.Focus();

        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            xoatrang();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Main m = new Main();
            m.Show();
        }

        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            string socm = txtTimKiemCMND_KH.Text;
         
            string squery_cmnd = "select * from KhachHang kh where kh.CMND like '%" + socm + "%'";
            dataGridViewDSDatPhong.DataSource = modify.GetDataTable(squery_cmnd);

        }

        private void btnHuyTK_Click(object sender, EventArgs e)
        {
            Load_gvDSDatPhong();
        }

        private void dataGridViewDSDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewDSDatPhong.SelectedRows[0];
            string maChiTietDP = row.Cells[0].Value.ToString();
            string squery_infor = "Select lp.Maloai, lp.TenLoai, lp.SoNguoi, lp.DonGia From ChiTietDatPhong ct , KhachHang kh, LoaiPhong lp Where ct.MaKH = kh.MaKH and ct.TenLoai = lp.TenLoai and ct.MaChiTietDatPhong = '"+maChiTietDP+"'";

            DataTable data = modify.GetDataTable(squery_infor);
            DataTableReader tableReader = data.CreateDataReader();
            while (tableReader.Read())
            {
                txtMaLoaiPhong.Text = tableReader.GetString(0);
                txtTenLoaiPhong.Text = tableReader.GetString(1);
                txtSoLuongNguoiToiDa.Text = tableReader.GetInt32(2) + "";
                txtGia.Text = tableReader.GetInt32(3) + "";
            }
        }
    }
}
