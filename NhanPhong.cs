using Manager_Hotel.ClassLoin;
using System;
using System.Data;
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
            gvDatPhong.Columns[0].HeaderText = "Mã Đặt Phòng";
            gvDatPhong.Columns[1].HeaderText = "Họ Tên";
            gvDatPhong.Columns[2].HeaderText = "Số CMND";
            gvDatPhong.Columns[3].HeaderText = "Loại Phòng";
            gvDatPhong.Columns[4].HeaderText = "Ngày Nhận";
            gvDatPhong.Columns[5].HeaderText = "Ngày Trả";

            string squery_Nhan = "Select p.MaPhong, kh.HoTen, kh.CMND, ct.TenLoai, ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct , KhachHang kh, PhieuDatPhong pdp, Phong p  where ct.MaKH  =kh.MaKH and p.MaPhong = pdp.MaPhong and ct.MaChiTietDatPhong = pdp.MaChiTietDP  and ct.MaChiTietDatPhong  in (Select pdp.MaChiTietDP from PhieuDatPhong pdp )";
            gvNhanPhong.DataSource = modify.GetDataTable(squery_Nhan);
            gvNhanPhong.Columns[0].HeaderText = "Mã Phòng";
            gvNhanPhong.Columns[1].HeaderText = "Họ Tên";
            gvNhanPhong.Columns[2].HeaderText = "Số CMND";
            gvNhanPhong.Columns[3].HeaderText = "Loại Phòng";
            gvNhanPhong.Columns[4].HeaderText = "Ngày Nhận";
            gvNhanPhong.Columns[5].HeaderText = "Ngày Trả";

            cbPhong.Enabled = false;
        }
        private void Load_comboPhong()
        {
            cbLoaiPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPhong.DropDownStyle = ComboBoxStyle.DropDownList;

            string squery = "Select DISTINCT  lp.TenLoai From Phong p , LoaiPhong lp Where p.MaLoai = lp.Maloai and p.TrangThai = N'Trống'";
            cbLoaiPhong.DataSource = modify.GetDataTable(squery);
            cbLoaiPhong.DisplayMember = "TenLoai";
        }

        private void btnNhanPhong_Click(object sender, EventArgs e)
        {
            // lấy mã chitietdatphong hiện tại
            string maChiTietDP;
            string maPhong;
            string maPhieuDP;
            if (gvDatPhong.Rows.Count > 1 && cbPhong.Text != "")
            {
                // lấy mã 
                maChiTietDP = gvDatPhong.CurrentRow.Cells[0].Value.ToString();
                maPhong = cbPhong.Text;
                while (true)
                {
                    try
                    {
                        Random rd = new Random();
                        maPhieuDP = "B" + rd.Next(100, 1000);

                        string squery = "Insert Into PhieuDatPhong values( '" + maPhieuDP + "', '" + maChiTietDP + "', '" + maPhong + "' )";
                        modify.Command(squery);

                        string squery_update = "Update Phong Set TrangThai = N'Đã Đặt' Where MaPhong = '" + maPhong + "'";
                        modify.Command(squery_update);

                        MessageBox.Show("Đã Nhận Phòng ", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        break;
                    }
                    catch (Exception ex)
                    {
                        maPhong = "";
                        if (MessageBox.Show("Thử Lại", "Lỗi", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                            break;
                    }
                }
                Load_GV();
            }
        }

        private void gvDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvDatPhong.Rows.Count > 0)
            {
                DataGridViewRow row = gvDatPhong.SelectedRows[0];
                string maChiTietDP = row.Cells[0].Value.ToString();
                string squery = "Select kh.HoTen, kh.CMND,  ct.TenLoai, ct.NgayNhan, ct.NgayTra, lp.SoNguoi, lp.DonGia From KhachHang kh, ChiTietDatPhong ct , LoaiPhong lp Where kh.MaKH = ct.MaKH and lp.TenLoai = ct.TenLoai and  ct.MaChiTietDatPhong = '" + maChiTietDP + "'";
                DataTableReader reader = modify.GetDataTable(squery).CreateDataReader();
                while (reader.Read())
                {
                    txtHoTen.Text = reader.GetString(0);
                    txtCMND.Text = reader.GetString(1);
                    cbTenLoaiPhong.Text = reader.GetString(2);
                    cbLoaiPhong.Text = reader.GetString(2);
                    dateNhan.Text = reader["NgayNhan"].ToString();
                    dateTra.Text = reader["NgayTra"].ToString();
                    udSL.Value = reader.GetInt32(5);
                    txtDonGia.Text = reader.GetInt32(6).ToString();
                }
                cbPhong.Enabled = false;
            }
        }

        private void btnLayPhong_Click(object sender, EventArgs e)
        {
            if (cbLoaiPhong.Text != "")
            {
                string squery = "Select p.MaPhong From Phong p, LoaiPhong lp where p.MaLoai = lp.MaLoai and p.TrangThai = N'Trống' and lp.TenLoai = '" + cbLoaiPhong.Text + "' ";
                cbPhong.DataSource = modify.GetDataTable(squery);
                cbPhong.DisplayMember = "MaPhong";
                cbPhong.Enabled = true;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            string maPhong;
            string maPhieuDP;
            string maChiTietDP;
            if (gvNhanPhong.Rows.Count > 1 && gvNhanPhong.SelectedRows.Count > 0)
            {
                // lấy mã phòng 
                maPhong = gvNhanPhong.CurrentRow.Cells[0].Value.ToString();
                maPhieuDP = modify.GetID("Select  pdp.MaPhieuDP From Phong p , PhieuDatPhong pdp Where p.MaPhong  =pdp.MaPhong and  p.MaPhong =  '" + maPhong + "' ");
                maChiTietDP = modify.GetID("Select ct.MaChiTietDatPhong from PhieuDatPhong pdp, ChiTietDatPhong ct where pdp.MaChiTietDP  = ct.MaChiTietDatPhong and  pdp.MaPhieuDP = '" + maPhieuDP + "'");

                // xóa phiếu đặt phòng 
                string squery_del = "DELETE FROM PhieuDatPhong WHERE MaPhieuDP = '" + maPhieuDP + "'";
                modify.Command(squery_del);
                //
                string squery_update = "Update Phong Set TrangThai = N'Trống' Where MaPhong = '" + maPhong + "'";
                modify.Command(squery_update);
                Load_GV();
            }
        }

        private void btnSreach_Click(object sender, EventArgs e)
        {
            if (rdDanhSachDp.Checked == true)
            {
                string squery = "Select ct.MaChiTietDatPhong, kh.HoTen, kh.CMND, ct.TenLoai, ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct , KhachHang kh where ct.MaKH  =kh.MaKH and ct.MaChiTietDatPhong not in (Select pdp.MaChiTietDP from PhieuDatPhong pdp ) and (kh.HoTen like '%" + txtSearch.Text + "%' or kh.CMND like '%" + txtSearch.Text + "%')";
                gvDatPhong.DataSource = modify.GetDataTable(squery);
            }
            else if (rdDanhSachNP.Checked == true)
            {
                string squery_Nhan = "Select p.MaPhong, kh.HoTen, kh.CMND, ct.TenLoai, ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct , KhachHang kh, PhieuDatPhong pdp, Phong p  where ct.MaKH  =kh.MaKH and p.MaPhong = pdp.MaPhong and ct.MaChiTietDatPhong = pdp.MaChiTietDP  and ct.MaChiTietDatPhong  in (Select pdp.MaChiTietDP from PhieuDatPhong pdp ) and (kh.HoTen like '%" + txtSearch.Text + "%' or kh.CMND like '%" + txtSearch.Text + "%')";
                gvNhanPhong.DataSource = modify.GetDataTable(squery_Nhan);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
