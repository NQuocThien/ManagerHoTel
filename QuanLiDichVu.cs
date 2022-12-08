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
    public partial class QuanLiDichVu : Form
    {
        public QuanLiDichVu()
        {
            InitializeComponent();
        }
        String querytableDV = "select * from DichVu";
        ClassLoin.Modify modify = new ClassLoin.Modify();
        public void loadGirdView()
        {
            dataGridViewDV.ReadOnly = true;
            dataGridViewDV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDV.DataSource = modify.GetDataTable(querytableDV);

            int n = dataGridViewDV.Width / 4;
            dataGridViewDV.Columns[0].HeaderText = "Mã Dịch Vụ";
            dataGridViewDV.Columns[0].Width = n;
            dataGridViewDV.Columns[1].HeaderText = "Loại Dịch Vụ";
            dataGridViewDV.Columns[1].Width = n;
            dataGridViewDV.Columns[2].HeaderText = "Tên Dịch Vụ";
            dataGridViewDV.Columns[2].Width = n;
            dataGridViewDV.Columns[3].HeaderText = "Đơn Giá";
            dataGridViewDV.Columns[3].Width = n;
        }
        private void btnCapNhatDichVu_Click(object sender, EventArgs e)
        {
            int i;
            i = dataGridViewDV.CurrentRow.Index;
            String query1 = "update DichVu set LoaiDichVu=N'" + txtLoaiDV.Text + "', TenDV=N'" + txtTenDV.Text + "', DonGia=N'" + txtDonGia.Text + "' where MaDV='" + txtMaDV.Text + "'";
            modify.Command(query1);
            loadGirdView();
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            String queryThemDV;

            try
            {
                queryThemDV = "insert into DichVu values('" + txtMaDV.Text + "',+ N'" + txtLoaiDV.Text + "', N'" + txtTenDV.Text + "', '" + txtDonGia.Text + "' )";
                modify.Command(queryThemDV);

            }
            catch
            {

            }
            loadGirdView();
        }

        private void btnXoaDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "DELETE FROM DichVu where MaDV = '" + txtMaDV.Text + "'";
                modify.Command(query);
                loadGirdView();
            }
            catch { }
        }

        private void btnTimKiemDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                String querySearch = "select * From DichVu Where MaDV like '%" + txtSearch.Text + "%'or TenDV like '%" + txtSearch.Text + "%' ";
                dataGridViewDV.DataSource = modify.GetDataTable(querySearch);
            }
            catch { }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QuanLiDichVu_Load(object sender, EventArgs e)
        {
            loadGirdView();
        }

        private void dataGridViewDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;

            i = dataGridViewDV.CurrentRow.Index;

            txtMaDV.Text = dataGridViewDV.Rows[i].Cells[0].Value.ToString();
            txtLoaiDV.Text = dataGridViewDV.Rows[i].Cells[1].Value.ToString();
            txtTenDV.Text = dataGridViewDV.Rows[i].Cells[2].Value.ToString();
            txtDonGia.Text = dataGridViewDV.Rows[i].Cells[3].Value.ToString();
        }
    }
}
