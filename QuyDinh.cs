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
    public partial class QuyDinh : Form
    {
        public QuyDinh()
        {
            InitializeComponent();
        }

        public void inQuyDinh()
        {
            lblQuyDinh.Text =
                "1. Quý khách vui lòng xuất trình hộ chiếu hoặc chứng minh nhân dân để làm thủ tục nhận phòng tại Lễ tân." + "\r\n\r\n" +
                "2. Khách sạn chỉ chịu trách nhiệm với những tài sản hoặc tiền được gửi tại quầy Lễ tân.\r\n\r\n" +
                "3. Không mang súng đạn, chất cháy nổ, chất độc hại, các chất gây nghiện, vật nuôi hoặc thực phẩm có mùi tanh hôi vào phòng nghỉ. Không nấu nướng, giặt là trong phòng nghỉ.\r\n\r\n" +
                "4. Không thay đổi, di chuyển đồ đạc trong phòng hoặc từ phòng này sang phòng khác. Trường hợp tài sản, đồ dùng trong phòng bị mất, hỏng do chủ quan Quý khách sẽ phải bồi thường 100% giá trị.\r\n\r\n" +
                "5. Xin vui lòng không thay đổi phòng hoặc đưa thêm người vào phòng khi chưa đăng ký trước với Lễ tân.\r\n\r\n" +
                "6. Nếu có người thân đến thăm, xin quý khách vui lòng liên hệ với Lễ tân để bổ trí nơi tiếp đón.\r\n\r\n" +
                "7. Khi ra khỏi phòng, Quý khách vui lòng rút thẻ chìa khoá ra khỏi ổ điện và gửi tại quầy lễ tân. Điện trong phòng sẽ tự động ngắt khi cửa đã được khép.\r\n\r\n" +
                "8. Nếu Quý khách phát hiện có hiện tượng cháy trong Khách sạn, xin khẩn trương tìm cách thông báo cho người ở khu vực gần nhất và bình tĩnh làm theo chỉ dẫn phòng chống cháy nổ.\r\n\r\n" +
                "9. Thời gian trả phòng là 12h30, nếu muộn hơn sẽ phải thanh toán thêm phụ phí tương ứng. Trong trường hợp cần thiết, xin vui lòng liên hệ với Lễ tân.\r\n\r\n" +
                "10. Trước khi rời khỏi khách sạn, xin Quý khách vui lòng thanh toán toàn bộ các hoá đơn và trả lại chìa khoá phòng cho Lễ tân.\r\n\r\n" + "\r\nChúc Quý khách một kỳ nghỉ vui vẻ!\r\n\r\n";
        }
        private void QuyDinh_Load(object sender, EventArgs e)
        {
            inQuyDinh();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
