using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string usingEmail;
        private bool isAdmin;

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDangNhap formlogin = new FormDangNhap();
            formlogin.ShowDialog();
            if (formlogin.getSuccess)
            {
                usingEmail = formlogin.getEmail;
                isAdmin = formlogin.isAdmin;
                LoggedIn(true);
                formlogin.Close();
            }   
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForm(new FormNhanVien());
        }
        void ChangeForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panel1.Controls.Clear();
            panel1.Controls.Add(form);
            form.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForm(new FormKhachHang(usingEmail));
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForm(new FormSanPham(usingEmail));

        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau formDoiMK = new FormDoiMatKhau();
            formDoiMK.ShowDialog();
            if (formDoiMK.getSuccess)
            {
                LoggedIn(false);
                formDoiMK.Close();
            }

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn đăng xuất ?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                LoggedIn(false);
            }

        }
        private void LoggedIn(bool check)
        {
            menuThongKe.Visible = check;
            menuDanhMuc.Visible = check;
            menu_login.Enabled = !check;
            menu_Logout.Visible = check;
            if (!check)
            {
                panel1.Controls.Clear();
            }
            if (check)
            {
                menuNhanVien.Visible = isAdmin;
                menuThongKe.Visible = isAdmin;
            }
        }

        private void menuThongKe_Click(object sender, EventArgs e)
        {
            ChangeForm(new FormThongKe());
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sd 
            //vsdvvsdvvsvd
        }
    }
}
