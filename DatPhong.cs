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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cboxChuyenPhong_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnChiTietDatPhong_Click(object sender, EventArgs e)
        {
            ChiTietDatPhong chitiet = new ChiTietDatPhong();
            //chitiet.ShowDialog();
        }
    }
}
