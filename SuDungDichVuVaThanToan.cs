using Manager_Hotel.ClassLoin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
        public SuDungDichVuVaThanToan()
        {
            InitializeComponent();
        }
        String queryDV = "select *from DichVu";
        String queryDV1 = "select DISTINCT LoaiDichVu from DichVu";


        ClassLoin.Modify modify = new ClassLoin.Modify();
        private void SuDungDichVuVaThanToan_Load(object sender, EventArgs e)
        {

            loaddatagirdview();
            loadComboBox();
        }
        public void loaddatagirdview()
        {
            string squery_loadPhong = "Select p.MaPhong, kh.HoTen, kh.CMND, ct.TenLoai, ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct , KhachHang kh, PhieuDatPhong pdp, Phong p  where ct.MaKH  =kh.MaKH and p.MaPhong = pdp.MaPhong and ct.MaChiTietDatPhong = pdp.MaChiTietDP  and ct.MaChiTietDatPhong  in (Select pdp.MaChiTietDP from PhieuDatPhong pdp )";
            dataGridViewDV.DataSource = modify.GetDataTable(squery_loadPhong);
        }
        public void loadComboBox()
        {
                comboBoxLoaiDV.DisplayMember = "LoaiDichVu";
                comboBoxLoaiDV.ValueMember = "MaDV";
                comboBoxLoaiDV.DataSource = modify.GetDataTable(queryDV1); 
            
               
        }

       
        private void DatagirdviewDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxLoaiDV_SelectedIndexChanged(object sender, EventArgs e)
        {

            String queryLoaiDV = "select TenDV from DichVu where LoaiDichVu like '%" + comboBoxLoaiDV.Text + "%'";
            //MessageBox.Show(comboBoxDV.SelectedText, "Thong bao");
            comboBoxDV.DisplayMember = "TenDV";
            comboBoxDV.ValueMember = "MaDV";
            comboBoxDV.DataSource = modify.GetDataTable(queryLoaiDV);
            
           /* txtGia.DataBindings.Clear();
            txtGia.DataBindings.Add("DichVu", comboBoxDV.DataSource, "DonGia");*/
            



         }

        private void comboBoxDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            String squeryDV= "select DonGia from DichVu where TenDV=N'" + comboBoxDV.Text+ "'";
            
            txtGia.Text = modify.loadtextBox(squeryDV).Rows[0]["DonGia"].ToString().Trim();
           
            
        }

        private void dataGridViewDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            String squeryAddDV = "select ";
        }
    }
}
