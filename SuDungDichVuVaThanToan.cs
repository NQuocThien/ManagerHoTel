using Manager_Hotel.ClassLoin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Manager_Hotel
{
    public partial class SuDungDichVuVaThanToan : Form
    {

        string queryDV1 = "select DISTINCT LoaiDichVu from DichVu";
        string HoTenNhanVien = "";
        public SuDungDichVuVaThanToan(string HoTen)
        {
            InitializeComponent();
            this.HoTenNhanVien = HoTen;
        }
        ClassLoin.Modify modify = new ClassLoin.Modify();
        private void SuDungDichVuVaThanToan_Load(object sender, EventArgs e)
        {
            txtTongTien.Enabled = false;
            btnThemDV.Enabled = false;
            loaddatagirdview();
            loadComboBox();
        }
        private void Load_hvHoaDon(string maKH)
        {

        }
        public void loaddatagirdview()
        {
            string squery_loadPhong = "Select kh.MaKH,p.MaPhong, kh.HoTen, kh.CMND, ct.TenLoai, ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct , KhachHang kh, PhieuDatPhong pdp, Phong p  where ct.MaKH  =kh.MaKH and p.MaPhong = pdp.MaPhong and ct.MaChiTietDatPhong = pdp.MaChiTietDP  and ct.MaChiTietDatPhong  in (Select pdp.MaChiTietDP from PhieuDatPhong pdp )";
            dataGridViewPhong.DataSource = modify.GetDataTable(squery_loadPhong);
            dataGridViewPhong.ReadOnly = true;
            dataGridViewPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPhong.Columns[0].HeaderText = "Mã KH";
            dataGridViewPhong.Columns[1].HeaderText = "Phòng";
            dataGridViewPhong.Columns[2].HeaderText = "Họ Tên";
            dataGridViewPhong.Columns[3].HeaderText = "CMND";
            dataGridViewPhong.Columns[4].HeaderText = "Loại Phòng";
            dataGridViewPhong.Columns[5].HeaderText = "Ngày Nhận";
            dataGridViewPhong.Columns[6].HeaderText = "Ngày Trả";


        }
        public void loadComboBox()
        {
                comboBoxLoaiDV.DisplayMember = "LoaiDichVu";
                comboBoxLoaiDV.ValueMember = "MaDV";
                comboBoxLoaiDV.DataSource = modify.GetDataTable(queryDV1);
        }
              
     
        private void Load_gvHoaDonPhong(string maKH)
        {
            int n = dataGridViewHD.Width / 6;
            dataGridViewHD.ReadOnly = true;
            dataGridViewHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            string squery_test = "Select ct.MaKH from ChiTietDatPhong ct where ct.MaChiTietDatPhong in (Select MaChiTietDP from HoaDonPhong ) and ct.MaKH = '"+maKH+"'";
            string maKH_test = modify.GetID(squery_test);
             if(maKH == maKH_test)
            { }
            else
            {

                MessageBox.Show("Tọa mới hóa đơn phòng");
                string maHDPhong = "HP";
                Random rd = new Random();
                while(true)
                {
                    try // tạo hóa đơn phòng
                    {
                        string maChiTietDP = modify.GetID("Select MaChiTietDatPhong from ChiTietDatPhong where MaKH = '" + maKH + "'");
                        string maHD = modify.GetID("Select hd.MaHD from HoaDon hd where hd.maKH ='" + maKH + "'");
                        maHDPhong += rd.Next(100, 1000);
                        DataTableReader reader = modify.GetDataTable("Select ct.SoDem ,lp.DonGia from ChiTietDatPhong ct , LoaiPhong lp where ct.TenLoai = lp.TenLoai and ct.MaKH ='"+maKH+"'").CreateDataReader();

                        int tienPhong =0;
                        int soDem =0;

                        while(reader.Read())
                        {
                            soDem = reader.GetInt32(0);
                            tienPhong = reader.GetInt32(1);
                        }
                        int tongtien = soDem * tienPhong;
                        string squery_insert = "Insert into HoaDonPhong values('" + maHDPhong + "', '" + maChiTietDP + "', '" + tongtien + "', '"+maHD+"')";
                        modify.Command(squery_insert);
                        break;
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi Tạo Hóa Đơn Phòng");
                        maHDPhong = "HP";
                    }
                }
                

            }

            dataGridViewHD.DataSource = modify.GetDataTable("Select p.MaPhong , lp.DonGia, ct.NgayNhan, ct.NgayTra, hdp.ThanhTien, hd.TongTien From ChiTietDatPhong ct, LoaiPhong lp, Phong p, PhieuDatPhong pdp, HoaDonPhong hdp, HoaDon hd where ct.MaChiTietDatPhong = pdp.MaChiTietDP and p.MaLoai = lp.Maloai and pdp.MaPhong = p.MaPhong and hd.MaHD = hdp.MaHD  and hdp.MaChiTietDP =ct.MaChiTietDatPhong and  ct.MaKH ='" + maKH + "'");

            dataGridViewHD.Columns[0].HeaderText = "Tên Phòng";
            dataGridViewHD.Columns[0].Width = n ;

            dataGridViewHD.Columns[1].HeaderText = "Đơn Giá";
            dataGridViewHD.Columns[1].Width = n;

            dataGridViewHD.Columns[2].HeaderText = "Ngày Nhận";
            dataGridViewHD.Columns[3].Width = n;

            dataGridViewHD.Columns[3].HeaderText = "Ngày Trả";
            dataGridViewHD.Columns[3].Width = n;

            dataGridViewHD.Columns[4].HeaderText = "Tiền Phòng";
            dataGridViewHD.Columns[4].Width = n;

            dataGridViewHD.Columns[5].HeaderText = "Tổng Tiền";
            dataGridViewHD.Columns[5].Width = n;
            CapNhatTien(maKH);
        }

        private void comboBoxLoaiDV_SelectedIndexChanged(object sender, EventArgs e)
        {

            String queryLoaiDV = "select TenDV from DichVu where LoaiDichVu like N'%" + comboBoxLoaiDV.Text + "%'";
            comboBoxDV.DisplayMember = "TenDV";
            comboBoxDV.ValueMember = "MaDV";
            comboBoxDV.DataSource = modify.GetDataTable(queryLoaiDV);
       
         }

        private void comboBoxDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            String squeryDV= "select DonGia from DichVu where TenDV = N'" + comboBoxDV.Text+ "'";
            txtGia.Text = modify.loadtextBox(squeryDV).Rows[0]["DonGia"].ToString();
        }

        private void Load_gvThemDichVu(string maKH)
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

        private void KiemTraTaoMoiHD (string maKH)
        {
            

            // kiểm tra khách hàng có tạo hóa đơn chưa 
            string squery_test = "Select kh.MaKH from KhachHang kh where kh.MaKH in(Select MaKH From HoaDon ) and kh.MaKH = '" + maKH + "'";
            string maKH_test = modify.GetID(squery_test);
            if(maKH_test == maKH)
            {            }
            else // tạo Hóa Đơn
            {
                MessageBox.Show(" Chưa có hóa đơn tạo hóa đơn ");
                string maHD = "B";
                while(true)
                {
                    try
                    {
                        Random rd = new Random();
                        maHD += rd.Next(100, 1000); // tọa mã hóa đơn
                        string squery_insert = "insert into HoaDon values('" + maHD + "', '0', '" + maKH + "', N'Chưa Thanh Toán', '', '')";
                        modify.Command(squery_insert);

                        break;
                    }
                    catch
                    {
                        maHD = "B";
                        MessageBox.Show(" lỗi Chưa có hóa đơn");
                    }
                }
            }
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            string maHDDichVu = "HD";
            string maKH = dataGridViewPhong.CurrentRow.Cells[0].Value.ToString();
            string maHD = modify.GetID("Select MaHD from HoaDon where MaKH = '"+maKH+"'");
            string maDV = "";
            int dongia = 0;
           

            DataTableReader reader = modify.GetDataTable("Select MaDV ,DonGia from DichVu where TenDV =N'" + comboBoxDV.Text + "' and LoaiDichVu =N'" + comboBoxLoaiDV.Text + "'").CreateDataReader();
            while(reader.Read())
            {
                maDV = reader.GetString(0).Trim();
                dongia = reader.GetInt32(1);
            }
            int soluong = (int) udSoLuong.Value ;
            int thanhtien = soluong * dongia;
            while (true)
            {
                try
                {
                    Random rd = new Random();
                    maHDDichVu += rd.Next(100, 1000);

                    string query_insert = "Insert Into HoaDonDV values ('" + maHDDichVu + "','" + maDV + "','" + soluong + "', '" + thanhtien + "' ,'" + maHD + "')";
                    modify.Command(query_insert);
                    break;
                }
                catch
                {
                    MessageBox.Show("Lỗi thêm DV");
                    maHDDichVu = "HD";
                }
            }
            // cập nhật lại tổng tiền 
            CapNhatTien(maKH);
            Load_gvThemDichVu(maKH);
            Load_gvHoaDonPhong(maKH);
            }

        private void CapNhatTien(string maKH)
        {
            int tienPhong = 0;
            int tienDV = 0;
            DataTableReader reader1 = modify.GetDataTable("Select hdp.ThanhTien  from HoaDonPhong hdp, HoaDon hd  where  hd.MaHD = hdp.MaHD and hd.MaKH ='" + maKH + "'").CreateDataReader();
            while (reader1.Read())
            {
                tienPhong = reader1.GetInt32(0);
            }
            DataTableReader reader2 = modify.GetDataTable("Select dhdv.ThanhTien from HoaDonDV dhdv , HoaDon hd  where  hd.MaHD = dhdv.MaHD and hd.MaKH ='" + maKH + "'").CreateDataReader();
            while (reader2.Read())
            {
                tienDV += reader2.GetInt32(0);
            }
            int tong = tienPhong + tienDV;
            string squery_update = "Update HoaDon set TongTien = '" + tong + "' where MaKH ='" + maKH + "'"; ;

            modify.Command(squery_update);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void btnXoaDV_Click(object sender, EventArgs e)
        {
            string maHDDV = "";
            string maKH = dataGridViewPhong.CurrentRow.Cells[0].Value.ToString();
            if (dataGridViewAddDV.Rows.Count > 1)
            {
                DataGridViewRow row = dataGridViewAddDV.SelectedRows[0];
                maHDDV = row.Cells[0].Value.ToString();
                string squery_del = "Delete HoaDonDV Where MaHDDV = '" + maHDDV + "'";
                modify.Command(squery_del);
            }
            CapNhatTien(maKH);
            Load_gvThemDichVu(maKH);
        }

        private void DatagirdviewPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string maKH = dataGridViewPhong.SelectedRows[0].Cells[0].Value.ToString();
            string squery = "Select TongTien from HoaDon where MaKH= '" + maKH + "'";
            int tongtien = 0;
            DataTableReader reader = modify.GetDataTable(squery).CreateDataReader();
            while(reader.Read())
            {
                tongtien += reader.GetInt32(0); 
            }

            int i = dataGridViewPhong.CurrentRow.Index;
            KiemTraTaoMoiHD(maKH); // 
            Load_gvThemDichVu(maKH); //
            Load_gvHoaDonPhong(maKH); // 
            btnThemDV.Enabled = true;
            comboBoxDV.Enabled = true;
            comboBoxLoaiDV.Enabled = true;
            udSoLuong.Enabled = true;
            udGiamGia.Enabled = true;
            btnXoaDV.Enabled = true;
            btnThanhToan.Enabled = true;
            txtTongTien.Text = tongtien + "";
        }

        private void udGiamGia_ValueChanged(object sender, EventArgs e)
        {
            int tongtien = int.Parse(txtTongTien.Text);
            tongtien -= (int) udGiamGia.Value;
            tongtien = tongtien < 0 ? 0 : tongtien;
            string maKH = dataGridViewPhong.CurrentRow.Cells[0].Value.ToString();
            // 
            modify.Command("Update HoaDon set TongTien = '" + tongtien + "' where MaKH = '"+maKH+"' ");
            txtTongTien.Text = tongtien + "";

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string maKH = dataGridViewPhong.SelectedRows[0].Cells[0].Value.ToString();
            string maHD = modify.GetID("Select MaHD from HoaDon Where MaKH = '" + maKH + "'");
            string kt_thongtin_thanhtoan = modify.GetID("Select TrangThaiTT from HoaDon Where MaKH = '" + maKH + "'");
            if (kt_thongtin_thanhtoan == "Đã Thanh Toán")
            {
                MessageBox.Show("Hóa Đơn Này Đã Được Thanh Toán", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }

            InHoaDon hd = new InHoaDon(maKH, HoTenNhanVien, int.Parse(txtTongTien.Text), maHD, (int) udGiamGia.Value);
            hd.ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
