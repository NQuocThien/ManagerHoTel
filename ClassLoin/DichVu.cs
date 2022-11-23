using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Hotel.ClassLoin
{
    internal class DichVu
    {
        private String MaDV, LoaiDV, TenDV, MaHDDV, MaKH;
        private int SoLuong, ThanhTienDV, DonGiaDV;

        public DichVu(string maDV, string loaiDV, string tenDV, string maHDDV, string maKH, int soLuong, int thanhTienDV, int donGiaDV)
        {
            MaDV = maDV;
            LoaiDV = loaiDV;
            TenDV = tenDV;
            MaHDDV = maHDDV;
            MaKH = maKH;
            SoLuong = soLuong;
            ThanhTienDV = thanhTienDV;
            DonGiaDV = donGiaDV;
        }

        public string MaDV1 { get => MaDV; set => MaDV = value; }
        public string LoaiDV1 { get => LoaiDV; set => LoaiDV = value; }
        public string TenDV1 { get => TenDV; set => TenDV = value; }
        public string MaHDDV1 { get => MaHDDV; set => MaHDDV = value; }
        public string MaKH1 { get => MaKH; set => MaKH = value; }
        public int SoLuong1 { get => SoLuong; set => SoLuong = value; }
        public int ThanhTienDV1 { get => ThanhTienDV; set => ThanhTienDV = value; }
        public int DonGiaDV1 { get => DonGiaDV; set => DonGiaDV = value; }
    }
}
