using Manager_Hotel.ClassLoin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manager_Hotel
{
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
        }
        public bool checkEmail(string email) // kiểm tra email người dùng nhập có hợp lệ hong
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$");
        }
        Modify modify = new Modify();
        private void btnLayLaiPass_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (!checkEmail(email)) { MessageBox.Show("Nhập sai Email", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); return; }
            else
            {
                string squery = "Select * from TaiKhoan where Email='" + email + "'";
                if(modify.TaiKhoans(squery).Count != 0)
                {
                    MessageBox.Show("Mật Khẩu: " + modify.TaiKhoans(squery)[0].MatKhau, "Lấy Lại Mật Khẩu", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }else
                {
                    MessageBox.Show("Email chưa đăng ký", "Lấy Lại Mật Khẩu", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
