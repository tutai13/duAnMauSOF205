using BUS_QLBanHang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }
        private bool isSuccess = false;
        public bool getSuccess
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEmail != null && txtPass != null) 
            {
                if (bus_nv.checkEmail(txtEmail.Text))
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    isSuccess = true;
                    Close();

                }
                else
                {
                    MessageBox.Show("Sai email hoặc mật khẩu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isSuccess = false;
                    txtEmail.Text = "";
                    txtPass.Text = "";
                    txtEmail.Focus();
                }
            }
        }
        public string getEmail
        {
            get { return txtEmail.Text; }
        }
        public bool isAdmin
        {
            get { return bus_nv.LayVaiTro(getEmail); }
        }
        private void llblQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string userEmail = txtEmail.Text.Trim();
            string pass = bus_nv.getPassword();
            if (userEmail != null)
            {
                if (bus_nv.checkEmail(userEmail))
                {
                    
                    SendMail(userEmail, pass ); //Gửi mật khẩu mới tới mail người dùng

                    //Lưu mật khẩu mới vào database
                    bus_nv.updateMatKhau(userEmail, pass);
                }
                else
                {
                    MessageBox.Show("Email không tôn tại");
                    txtEmail.Clear();
                    txtEmail.Focus();
                }
            }

        }
        public void SendMail(string mail, string matKhau)
        {
            try
            {

                MailMessage msg = new MailMessage();
                msg.To.Add(mail);
                msg.From = new MailAddress("duongvpd10563@gmail.com");
                msg.Subject = "Chức năng quên mật khẩu";
                msg.Body = "Mật khẩu mới của bạn là : " + matKhau;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("duongnvpd10563@gmail.com", "jipazvuqvjhktwxi");
                smtp.Send(msg);

                MessageBox.Show("Mail khôi phục mật khẩu đã được gửi tới mail của bạn ");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
