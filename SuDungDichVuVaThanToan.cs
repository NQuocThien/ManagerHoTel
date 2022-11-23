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
            string squery_loadPhong = "Select kh.MaKH,p.MaPhong, kh.HoTen, kh.CMND, ct.TenLoai, ct.NgayNhan, ct.NgayTra from ChiTietDatPhong ct , KhachHang kh, PhieuDatPhong pdp, Phong p  where ct.MaKH  =kh.MaKH and p.MaPhong = pdp.MaPhong and ct.MaChiTietDatPhong = pdp.MaChiTietDP  and ct.MaChiTietDatPhong  in (Select pdp.MaChiTietDP from PhieuDatPhong pdp )";
            dataGridViewDV.DataSource = modify.GetDataTable(squery_loadPhong);
            dataGridViewDV.ReadOnly = true;
            dataGridViewDV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void loadComboBox()
        {
                comboBoxLoaiDV.DisplayMember = "LoaiDichVu";
                comboBoxLoaiDV.ValueMember = "MaDV";
                comboBoxLoaiDV.DataSource = modify.GetDataTable(queryDV1);
            

        }

       
        private void DatagirdviewDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //load
            int i = dataGridViewDV.CurrentRow.Index;
            dataGridViewAddDV.DataSource = modify.GetDataTable("select dv.LoaiDichVu,dv.TenDV,hd.SoLuong,dv.DonGia,hd.ThanhTien from DichVu dv,HoaDonDV hd where dv.MaDV = hd.MaDV and hd.MaKH='" + dataGridViewDV.Rows[i].Cells[0].Value.ToString() +"'");
            /*int sum = 0;
            foreach (DataGridViewRow row in dataGridViewAddDV.Rows)
            {
                sum += Convert.ToInt32(row.Cells[4].Value.ToString());
                
            }
            
            txtTongTien.Text = sum.ToString();*/

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

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            String maHDDV = "HD";
            //Lấy mã dịch vụ
            String squeryMaDV = "select MaDV from DichVu where TenDV=N'"+ comboBoxDV.Text + "' and LoaiDichVu=N'"+ comboBoxLoaiDV .Text+ "'";
            String MaDV= modify.loadtextBox(squeryMaDV).Rows[0]["MaDV"].ToString().Trim();
            // lấy vị trí mã khách hàng
            
            int i = dataGridViewDV.CurrentRow.Index;
            if(txtSL.Text=="")
            {
                MessageBox.Show("Chưa nhập số lượng, vui lòng nhập lại", "Thông bóa");
            }
            else
            {
                while (true)
                {
                    try
                    {
                        Random r = new Random();
                        
                        maHDDV += r.Next(100, 1000);
                        int thanhtien=Convert.ToInt32(txtGia.Text)*Convert.ToInt32(txtSL.Text);
                        
                        String queryThemDuLieuHDDV = "insert into HoaDonDV values('"+ maHDDV + "','"+ MaDV + "','" + txtSL.Text + "','" + dataGridViewDV.Rows[i].Cells[0].Value.ToString() +"','"+ thanhtien+ "') ";
                       
                        modify.Command(queryThemDuLieuHDDV);
                        dataGridViewAddDV.DataSource = modify.GetDataTable("select dv.LoaiDichVu,dv.TenDV,hd.SoLuong,dv.DonGia,hd.ThanhTien from DichVu dv,HoaDonDV hd where dv.MaDV = hd.MaDV and hd.MaKH='" + dataGridViewDV.Rows[i].Cells[0].Value.ToString() + "'");

                        break;
                    }
                    catch
                    {
                        maHDDV = "HD";
                    }
                }
            }

           
            
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewAddDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
