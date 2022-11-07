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
    public partial class NhanPhong : Form
    {
        public NhanPhong()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void NhanPhong_Load(object sender, EventArgs e)
        {
            ltvNhanPhong.View = View.Details;
            int w = ltvNhanPhong.Width /6 ;
            ltvNhanPhong.GridLines = true;

            ltvNhanPhong.Columns.Add("Mã Nhận Phòng");
            ltvNhanPhong.Columns[0].Width = w;

            ltvNhanPhong.Columns.Add("Họp và Tên");
            ltvNhanPhong.Columns[1].Width = w;

            ltvNhanPhong.Columns.Add("CMND");
            ltvNhanPhong.Columns[2].Width = w;

            ltvNhanPhong.Columns.Add("Tên Phòng");
            ltvNhanPhong.Columns[3].Width = w;

            ltvNhanPhong.Columns.Add("Ngày Nhận");
            ltvNhanPhong.Columns[4].Width = w;

            ltvNhanPhong.Columns.Add("Ngày Trả");
            ltvNhanPhong.Columns[5].Width = w;
        }
    }
}
