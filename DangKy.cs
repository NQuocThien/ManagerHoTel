using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Manager_Hotel.ClassLoin;

namespace Manager_Hotel
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        public bool checkAccount(string ac) // check mật khẩu và tài khoản 
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,24}$");
        }

        public bool checkEmail(string email) // kiểm tra email người dùng nhập có hợp lệ hong
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$");
        }
        Modify modify = new Modify();
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tentk = txtTenTk.Text;
            string makhau = txtMatKhau.Text;
            string xnmatkhau = txtXNMatKhau.Text;
            string email = txtEmail.Text;
            if(!checkEmail(email)) { MessageBox.Show("Nhập sai Email", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); return; }
            if(!checkAccount(tentk)) { MessageBox.Show("Tên Tài Khoản Không Hợp Lệ! Nhập Lại", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); return; }
            if(xnmatkhau != makhau) { MessageBox.Show("Vui Lòng Xác Nhận Mật Khẩu ", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); return; }
            if (modify.TaiKhoans("Select * from TaiKhoan where Email= '" + email + "'").Count != 0) { MessageBox.Show("Email đã được đăng ký vui lòng đổi email khác", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); return; }
            try
            {
                string squery = "Insert into TaiKhoan values ('" + tentk + "','" + makhau + "','" + email + "')";
                modify.Command(squery);

            }
            catch
            {
                MessageBox.Show("Tài Khoản Đã Tồn Tại, Vui Lòng Nhập Tên Tài Khoản Khác", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void btnHuyDK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
