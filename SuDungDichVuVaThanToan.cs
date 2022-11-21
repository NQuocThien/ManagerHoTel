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
        String queryDV1 = "select LoaiDichVu From DichVu";


        ClassLoin.Modify modify = new ClassLoin.Modify();
        private void SuDungDichVuVaThanToan_Load(object sender, EventArgs e)
        {
            Load_LvHD();
            Load_lvServiece();
            Load_lvPhuThu();
            loaddatagirdview();
            loadComboBox();
        }
        public void loaddatagirdview()
        {
            dataGridViewDV.DataSource = modify.GetDataTable(queryDV);
        }
        public void loadComboBox()
        {
                comboBoxLoaiDV.DisplayMember = "LoaiDichVu";
                comboBoxLoaiDV.ValueMember = "MaDV";
                comboBoxLoaiDV.DataSource = modify.GetDataTable(queryDV); 
            
               
        }

        private void Load_LvHD()
        {
            ltvHD.View = View.Details;
            int n = ltvHD.Width;
            ltvHD.GridLines = true;


            ltvHD.Columns.Add("Tên Phòng");
            ltvHD.Columns[0].Width = (int)n / 7;

            ltvHD.Columns.Add("Đơn Giá");
            ltvHD.Columns[1].Width = (int)n / 7;

            ltvHD.Columns.Add("Ngày Nhận");
            ltvHD.Columns[2].Width = (int)n / 7;

            ltvHD.Columns.Add("Ngày Trả");
            ltvHD.Columns[3].Width = (int)n / 7;

            ltvHD.Columns.Add("Tiền Phòng");
            ltvHD.Columns[4].Width = (int)n / 7;

            ltvHD.Columns.Add("Phụ Thu");
            ltvHD.Columns[5].Width = (int)n / 7;

            ltvHD.Columns.Add("Thành Tiền");
            ltvHD.Columns[6].Width = (int)n / 7;
        }

        private void Load_lvServiece()
        {
            ltvService.View = View.Details;
            int n = ltvService.Width / 10;
            ltvService.GridLines = true;

            ltvService.Columns.Add("STT");
            ltvService.Columns[0].Width = (int)n * 1;

            ltvService.Columns.Add("Tên Dịch Vụ");
            ltvService.Columns[1].Width = (int)n *4;

            ltvService.Columns.Add("Đơn Giá");
            ltvService.Columns[2].Width = (int)n *2;

            ltvService.Columns.Add("Số Lượng");
            ltvService.Columns[3].Width = (int)n *1;

            ltvService.Columns.Add("Thành Tiền");
            ltvService.Columns[4].Width = (int)n *2;
        }

        private void Load_lvPhuThu()
        {
            LtvPhuThu.View = View.Details;
            int n = LtvPhuThu.Width / 10;
            LtvPhuThu.GridLines = true;

            LtvPhuThu.Columns.Add("STT");
            LtvPhuThu.Columns[0].Width = (int) n ;

            LtvPhuThu.Columns.Add("Tên Quy Định");
            LtvPhuThu.Columns[1].Width = (int) n * 3 ;

            LtvPhuThu.Columns.Add("Hệ số");
            LtvPhuThu.Columns[2].Width = (int) n *2;

            LtvPhuThu.Columns.Add("Mô Tả");
            LtvPhuThu.Columns[3].Width = (int) n *4;
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

        }
    }
}
