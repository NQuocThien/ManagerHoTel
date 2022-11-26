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
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            Load_chartPhong();
            Load_ChartTong();
        }

        private void Load_ChartTong()
        {
            int tongDT = 0;
            DataTableReader reader = modify.GetDataTable("Select TongTien From HoaDon").CreateDataReader();
            while(reader.Read())
            {
                tongDT += reader.GetInt32(0);
            }
            chartDV.Series["ChartTong "].Points.Add(tongDT);
            chartDV.Series["ChartTong "].Points[0].Label = "" + tongDT;
            chartDV.Series["ChartTong "].Points[0].Color = Color.Blue;
            chartDV.Series["ChartTong "].Points[0].AxisLabel = "Tiền Phòng";
        }

        Modify modify = new Modify();
        private void Load_chartPhong()
        {
            int tongTienPhongDeluxe = 0;
            DataTableReader reader = modify.GetDataTable("Select hdp.ThanhTien from PhieuDatPhong pdp , Phong p, ChiTietDatPhong ct, HoaDonPhong hdp where hdp.MaChiTietDP = ct.MaChiTietDatPhong  and ct.MaChiTietDatPhong =pdp.MaChiTietDP and  pdp.MaPhong = p.MaPhong and p.MaLoai = 'DLX'").CreateDataReader();
            while(reader.Read())
            {
                tongTienPhongDeluxe += reader.GetInt32(0);
            }

            int tongTienPhongStandard = 0;
            DataTableReader reader1 = modify.GetDataTable("Select hdp.ThanhTien from PhieuDatPhong pdp , Phong p, ChiTietDatPhong ct, HoaDonPhong hdp where hdp.MaChiTietDP = ct.MaChiTietDatPhong  and ct.MaChiTietDatPhong =pdp.MaChiTietDP and  pdp.MaPhong = p.MaPhong and p.MaLoai = 'STD'").CreateDataReader();
            while (reader1.Read())
            {
                tongTienPhongStandard += reader1.GetInt32(0);
            }

            int tongTienPhongSuperior = 0;
            DataTableReader reader2 = modify.GetDataTable("Select hdp.ThanhTien from PhieuDatPhong pdp , Phong p, ChiTietDatPhong ct, HoaDonPhong hdp where hdp.MaChiTietDP = ct.MaChiTietDatPhong  and ct.MaChiTietDatPhong =pdp.MaChiTietDP and  pdp.MaPhong = p.MaPhong and p.MaLoai = 'SUP'").CreateDataReader();
            while (reader2.Read())
            {
                tongTienPhongSuperior += reader2.GetInt32(0);
            }

            int tongTienPhongSulte = 0;
            DataTableReader reader3 = modify.GetDataTable("Select hdp.ThanhTien from PhieuDatPhong pdp , Phong p, ChiTietDatPhong ct, HoaDonPhong hdp where hdp.MaChiTietDP = ct.MaChiTietDatPhong  and ct.MaChiTietDatPhong =pdp.MaChiTietDP and  pdp.MaPhong = p.MaPhong and p.MaLoai = 'SUT'").CreateDataReader();
            while (reader3.Read())
            {
                tongTienPhongSulte += reader3.GetInt32(0);
            }

            chartPhong.Series["ChartPhong"].Points.Add(tongTienPhongDeluxe);
            chartPhong.Series["ChartPhong"].Points[0].Label = ""+tongTienPhongDeluxe;
            chartPhong.Series["ChartPhong"].Points[0].Color = Color.Blue;
            chartPhong.Series["ChartPhong"].Points[0].AxisLabel = "Deluxe";

            chartPhong.Series["ChartPhong"].Points.Add(tongTienPhongStandard);
            chartPhong.Series["ChartPhong"].Points[1].Label = ""+tongTienPhongStandard;
            chartPhong.Series["ChartPhong"].Points[1].Color = Color.Blue;
            chartPhong.Series["ChartPhong"].Points[1].AxisLabel = "Standard";

            chartPhong.Series["ChartPhong"].Points.Add(tongTienPhongSuperior);
            chartPhong.Series["ChartPhong"].Points[2].Label = ""+tongTienPhongSuperior;
            chartPhong.Series["ChartPhong"].Points[2].Color = Color.Blue;
            chartPhong.Series["ChartPhong"].Points[2].AxisLabel = "Superior";

            chartPhong.Series["ChartPhong"].Points.Add(tongTienPhongSulte);
            chartPhong.Series["ChartPhong"].Points[3].Label = ""+tongTienPhongSulte;
            chartPhong.Series["ChartPhong"].Points[3].Color = Color.Blue;
            chartPhong.Series["ChartPhong"].Points[3].AxisLabel = "Sulte";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
