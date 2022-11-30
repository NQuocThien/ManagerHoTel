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
    public partial class QuanLiPhong : Form
    {
        public QuanLiPhong()
        {
            InitializeComponent();
        }
        String querytableDV = "select p.MaPhong, p.TrangThai, lp.TenLoai, lp.SoNguoi, lp.DonGia from Phong p, LoaiPhong lp where p.MaLoai = lp.MaLoai ";
        Modify modify = new Modify();
        

        private void QuanLiPhong_Load(object sender, EventArgs e)
        {
            Load_gvPhong();
        }

        private void Load_gvPhong()
        {
            dataGridViewPhong.ReadOnly = true;
            dataGridViewPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPhong.DataSource = modify.GetDataTable(querytableDV);

            int n = dataGridViewPhong.Width / 5;
            dataGridViewPhong.Columns[0].HeaderText = "Mã Phòng";
            dataGridViewPhong.Columns[0].Width = n;
            dataGridViewPhong.Columns[1].HeaderText = "Trạng Thái";
            dataGridViewPhong.Columns[1].Width = n;
            dataGridViewPhong.Columns[2].HeaderText = "Loại Phòng";
            dataGridViewPhong.Columns[2].Width = n;
            dataGridViewPhong.Columns[3].HeaderText = "Số Người";
            dataGridViewPhong.Columns[3].Width = n;
            dataGridViewPhong.Columns[4].HeaderText = "Đơn Giá";
            dataGridViewPhong.Columns[4].Width = n;
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {

            try
            {// lấy mã loại

                string maLoai = modify.GetID("Select MaLoai From LoaiPhong Where TenLoai = '" + cbLoaiPhong.Text + "'");
                // nhập bảng Phòng
                string squery = "Insert Into Phong Values('" + txtMaPhong.Text + "' , N'" + cbTrangThai.Text + "' , '" + maLoai + "')";
                modify.Command(squery);
            }catch
            {
                MessageBox.Show("Thông tin phòng không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Load_gvPhong();
        }

        private void dataGridViewPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;

            try
            {
                i = dataGridViewPhong.CurrentRow.Index;
                txtMaPhong.Text = dataGridViewPhong.Rows[i].Cells[0].Value.ToString();
                cbTrangThai.Text = dataGridViewPhong.Rows[i].Cells[1].Value.ToString();
                cbLoaiPhong.Text = dataGridViewPhong.Rows[i].Cells[2].Value.ToString();
                udSLNguoi.Value = Convert.ToInt32(dataGridViewPhong.Rows[i].Cells[3].Value);
                txtDonGia.Text = dataGridViewPhong.Rows[i].Cells[4].Value.ToString();
            }
            catch { }
           
        }

        private void btnCapNhatPhong_Click(object sender, EventArgs e)
        {
            try
            {
                 // lấy mã loại            
                string maLoai = modify.GetID("Select MaLoai From LoaiPhong Where TenLoai = '" + cbLoaiPhong.Text + "'");
                // cập nhật thông tin phòng
                string squery = "Update Phong Set TrangThai = N'" + cbTrangThai.Text + "' , MaLoai = '" + maLoai + "' where MaPhong = '"+txtMaPhong.Text+"' ";
                modify.Command(squery);
                Load_gvPhong();
            }
            catch
            {                
                if (MessageBox.Show("Thông tin không hợp lệ", "Lỗi ", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK )
                {
                    this.Close();
                    
                }

            }
          

        }

        private void btnXoaPhong_Click(object sender, EventArgs e)
        {
            // 
            string maPhong = "";
            if(dataGridViewPhong.SelectedRows.Count >0)
            {
                DataGridViewRow row = dataGridViewPhong.SelectedRows[0];
                maPhong = row.Cells[0].Value.ToString();
                string squery = "DELETE FROM Phong WHERE MaPhong = '" + maPhong + "' ";
                modify.Command(squery);
                Load_gvPhong();
            }
        }

        private void btnDongDSPhong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiemPhong_Click(object sender, EventArgs e)
        {
            
          /*  try
            {*/
                String querySearch = "select p.MaPhong, p.TrangThai, lp.TenLoai, lp.SoNguoi, lp.DonGia from Phong p, LoaiPhong lp where p.MaLoai = lp.MaLoai and ( p.MaPhong like '%" + txtSearch.Text + "%' or lp.TenLoai like '"+txtSearch.Text+"' )";
                dataGridViewPhong.DataSource = modify.GetDataTable(querySearch);
         /*   }
            catch { }*/
        }
    }
}
