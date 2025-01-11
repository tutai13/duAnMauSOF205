using BUS_QLBanHang;
using DTO_QLBanHang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormSanPham : Form
    {
        public FormSanPham(string usingEmail)
        {
            InitializeComponent();
            this.usingEmail = usingEmail;

        }
        private BUS_Hang bus_Hang = new BUS_Hang();
        private string usingEmail;


        private void FormSanPham_Load(object sender, EventArgs e)
        {

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadGridView();
            
        }
        void LoadGridView()
        {
            dataGridViewHang.DataSource = bus_Hang.DanhSachHang();
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridViewHang.DataSource = bus_Hang.searchHang(textBox4.Text);
            dataGridViewHang.Columns[0].HeaderText = "Mã sản phẩm";
            dataGridViewHang.Columns[1].HeaderText = "Tên Sản phẩm";
            dataGridViewHang.Columns[2].HeaderText = "Số lượng";
            dataGridViewHang.Columns[3].HeaderText = "Giá bán";
            dataGridViewHang.Columns[4].HeaderText = "Giá Nhập";
            dataGridViewHang.Columns[5].HeaderText = "Ghi chú";
            dataGridViewHang.Columns[6].HeaderText = "Mã Nhân viên";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridViewHang.Rows.Count > 0)
            {
                
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                string Directory = Application.StartupPath;

                txtMaHang.ReadOnly = true;
                txtMaHang.Text = dataGridViewHang.CurrentRow.Cells[0].Value.ToString();
                txtTenHang.Text = dataGridViewHang.CurrentRow.Cells[1].Value.ToString();
                txtSoLuong.Text = dataGridViewHang.CurrentRow.Cells[2].Value.ToString();
                txtDonGiaNhap.Text = dataGridViewHang.CurrentRow.Cells[3].Value.ToString();
                txtDonGiaBan.Text = dataGridViewHang.CurrentRow.Cells[4].Value.ToString();
                txtHinh.Text = dataGridViewHang.CurrentRow.Cells[5].Value.ToString();
                txtGhiChu.Text = dataGridViewHang.CurrentRow.Cells[6].Value.ToString();
            }
            loadhinh();
        }
        private bool IsBlank()
        {
            return txtTenHang == null || txtSoLuong == null || txtDonGiaBan == null || txtDonGiaNhap == null || txtGhiChu == null|| txtHinh == null;
        }
        void ClearInput()
        {
            txtMaHang.Clear();
            txtTenHang.Clear();
            txtSoLuong.Clear();
            txtDonGiaBan.Clear();
            txtDonGiaNhap.Clear();
            txtGhiChu.Clear();
            txtHinh.Clear();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!IsBlank())
            {
                DTO_Hang sp = new DTO_Hang()
                {
                    tenHang = txtTenHang.Text,
                    soLuong = Convert.ToInt16(txtSoLuong.Text),
                    donGiaBan = float.Parse(txtDonGiaBan.Text),
                    donGiaNhap = float.Parse(txtDonGiaNhap.Text),
                    hinhAnh = txtHinh.Text,
                    ghiChu = txtGhiChu.Text,
                    email = usingEmail
                };
                if (bus_Hang.insertHang(sp))
                {
                    MessageBox.Show("Thêm thành công");
                    LoadGridView();
                    ClearInput();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại , vui lòng kiểm tra lại");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra nhập liệu");
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaHang != null)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắc muốn xóa? ", "Cảnh báo", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bus_Hang.deleteHang(txtMaHang.Text))
                    {
                        MessageBox.Show("Xóa thành công");
                        LoadGridView();
                        ClearInput();
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn mã sản phẩm cần xóa ở bảng dưới");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!IsBlank())
            {
                DTO_Hang sp = new DTO_Hang()
                {
                    maHang = Convert.ToInt16(txtMaHang.Text),
                    tenHang = txtTenHang.Text,
                    soLuong = Convert.ToInt16(txtSoLuong.Text),
                    donGiaBan = float.Parse(txtDonGiaBan.Text),
                    donGiaNhap = float.Parse(txtDonGiaNhap.Text),
                    hinhAnh = txtHinh.Text,
                    ghiChu = txtGhiChu.Text,
                    email = usingEmail
                };
                if (bus_Hang.updateHang(sp))
                {
                    MessageBox.Show("Sửa thành công");
                    LoadGridView();
                    ClearInput();
                }
                else
                {
                    MessageBox.Show("Sửa  thất bại , vui lòng kiểm tra lại");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra nhập liệu");
            }
        }

        private void btnMoHinh_Click(object sender, EventArgs e)
        {
            moHinh();
        }
        void moHinh()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File anh|*.jpg; *git; *png; |all file|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pcbSanPham.Image= Image.FromFile(ofd.FileName);
                txtHinh.Text = ofd.FileName;
            }
        }
        void loadhinh()
        {
            string imagePath = txtHinh.Text; // Lấy đường dẫn từ TextBox

            if (!string.IsNullOrEmpty(imagePath)) // Kiểm tra xem đường dẫn có hợp lệ không
            {
                try
                {
                    pcbSanPham.Image = Image.FromFile(imagePath); // Hiển thị hình ảnh trong PictureBox
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải hình ảnh. Lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đường dẫn hình ảnh.");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
