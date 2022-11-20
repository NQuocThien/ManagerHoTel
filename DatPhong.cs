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
        DataSet ds;
       

        private void btnChiTietDatPhong_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewDSDatPhong.SelectedRows[0];
            string maPhieuDP = row.Cells[0].Value.ToString();
            ChiTietDatPhong chitiet = new ChiTietDatPhong(maPhieuDP);
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
            string maLoai = modify.GetID("select * from LoaiPhong lp where lp.TenLoai = '" + LoaiPhong + "'");

            string id_Phong = "R"; // Phong
            string id_pdp = "N"; // Phiếu

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


                    if (MessageBox.Show(" Loi KH Thử lại " + ex, "Lỗi", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                    id_kh = "C";
                }
            }

            while (true)// insert table Chi tiet dat Phong
            {
                try
                {
                    int id = rd.Next(100, 1000);
                    id_ctdp += +id;
                    string squeryChiTietDatPhong = "insert into ChiTietDatPhong values('" + id_ctdp + "', '" + NgayNhan + "', '" + NgayTra + "', '" + SoDem + "', '" + id_kh + "' )";
                    modify.Command(squeryChiTietDatPhong);
                    break;
                }
                catch (Exception ex)
                {

                    if (MessageBox.Show(" Loi Đat Phong Thử lại " + ex, "Lỗi", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                    id_ctdp = "A";
                }
            }



            try // lấy mã phòng
            {
                string squery_maPhong = "Select MaPhong from Phong where TrangThai=N'Trống' and Maloai='" + maLoai + "'";
                id_Phong = modify.GetID(squery_maPhong);
                MessageBox.Show("  mã phòng: " + id_Phong);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(" Loi Lấy Mã Phòng Thử lại " + ex, "Lỗi", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    this.Close();
                }

            }

            while (true) 
            {
                try
                {
                    int id = rd.Next(100, 1000);
                    id_pdp += +id;
                    string squery = "insert into PhieuDatPhong values('" + id_pdp + "', '" + id_ctdp + "', '" + id_Phong + "')";
                    modify.Command(squery);
                    break;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(" Loi PhieuDatPhong Thử lại " + ex, "Lỗi", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                    id_pdp = "N";
                }
            }
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
            string squery = "select pdp.MaPhieuDP, kh.HoTen, kh.CMND, lp.TenLoai, ctdp.NgayNhan, ctdp.NgayTra from KhachHang kh,  ChiTietDatPhong ctdp, PhieuDatPhong pdp, Phong p, LoaiPhong lp where kh.MaKH =ctdp.MaKH and pdp.MaPhong = p.MaPhong and p.MaLoai = lp.Maloai and ctdp.MaChiTietDatPhong = pdp.MaChiTietDP ";
            DataTable dt = modify.GetDataTable(squery);
            dataGridViewDSDatPhong.DataSource = dt;
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
         
            string squery_cmnd = "select * from KhachHang kh where kh.CMND = '" + socm + "'";
            dataGridViewDSDatPhong.DataSource = modify.GetDataTable(squery_cmnd);

        }

        private void btnHuyTK_Click(object sender, EventArgs e)
        {
            Load_gvDSDatPhong();
        }

        private void dataGridViewDSDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewDSDatPhong.SelectedRows[0];
            string maPhieuDP = row.Cells[0].Value.ToString();
            string squery_infor = "select lp.Maloai, lp.TenLoai, lp.SoNguoi, lp.DonGia from PhieuDatPhong pdp, Phong p, LoaiPhong lp where pdp.MaPhong = p.MaPhong and p.MaLoai = lp.Maloai and pdp.MaPhieuDP ='" + maPhieuDP + "'";
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
