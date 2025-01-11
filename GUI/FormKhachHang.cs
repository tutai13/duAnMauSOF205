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
using System.Xml.Linq;

namespace GUI
{
    public partial class FormKhachHang : Form
    {
        public FormKhachHang(string email)
        {
            InitializeComponent();
            this.usingEmail = email;

        }
        private BUS_KhachHang bus_kh = new BUS_KhachHang();
        private string usingEmail;

        private void dataGridViewKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewKhachHang.Rows.Count > 0)
            {
               
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                rdoNam.Enabled = true;
                rdoNu.Enabled = true;

                txtDienThoai.Text = dataGridViewKhachHang.CurrentRow.Cells[0].Value.ToString();
                txtTenKhach.Text = dataGridViewKhachHang.CurrentRow.Cells[1].Value.ToString();
                txtDiaChi.Text = dataGridViewKhachHang.CurrentRow.Cells[2].Value.ToString();
                string gioitinh = dataGridViewKhachHang.CurrentRow.Cells[3].Value.ToString();
                if (gioitinh == "Nam")
                    rdoNam.Checked = true;
                else
                    rdoNu.Checked = true;
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            showKhachHang();
        }
        private void loadGridView()
        {
            dataGridViewKhachHang.Columns[0].HeaderText = "Số điện thoại";
            dataGridViewKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            dataGridViewKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dataGridViewKhachHang.Columns[3].HeaderText = "Giới tính";
            dataGridViewKhachHang.Columns[0].DividerWidth = 2;
            dataGridViewKhachHang.Columns[1].DividerWidth = 2;
            dataGridViewKhachHang.Columns[2].DividerWidth = 2;
            dataGridViewKhachHang.Columns[3].DividerWidth = 2;
        }
        private void showKhachHang()
        {
            dataGridViewKhachHang.DataSource = bus_kh.DanhSachKH();
            loadGridView();
           

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "")
            {
                DataTable data = bus_kh.searchKhachHang(txtTimKiem.Text);
                if (data.Rows.Count > 0)
                {
                    dataGridViewKhachHang.DataSource = data;
                    loadGridView();
                }
                else MessageBox.Show("Không tìm thấy khách hàng nào");
            }
        }
        private bool isBlank()
        {
            return txtDiaChi == null || txtDienThoai == null || txtTenKhach == null || (rdoNam.Checked ==false && rdoNu.Checked==false);
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!isBlank())
            {
                DTO_KhachHang kh = new DTO_KhachHang()
                {
                    soDienThoai = txtDienThoai.Text,
                    tenKhach = txtTenKhach.Text,
                    diaChi = txtDiaChi.Text,
                    phai = rdoNam.Checked ? "Nam" : "Nữ",
                    emailNV = usingEmail
                };
                if (bus_kh.insertKhachHang(kh))
                {
                    MessageBox.Show("Thêm thành công");
                    dataGridViewKhachHang.DataSource = bus_kh.DanhSachKH();
                    ClearInput();
                }
                else
                    MessageBox.Show("Thêm thất bại , kiểm tra dữ kiệu");
            }
            else
                MessageBox.Show("Thêm thất bại ");
        }

        void ClearInput()
        {

            txtDiaChi.Clear();
            txtTenKhach.Clear();
            txtDienThoai.Clear();
            rdoNam.Checked = false;
            rdoNu.Checked = false;
        }
        private void FormKhachHang_Load_1(object sender, EventArgs e)
        {
            showKhachHang();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (txtDienThoai.Text != "")
            {
                DialogResult result = MessageBox.Show("Chắc chắn muốn xóa ?", "Cảnh báo", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bus_kh.deleteKhachHang(txtDienThoai.Text))
                    {
                        MessageBox.Show("Xóa thành công ");
                        dataGridViewKhachHang.DataSource = bus_kh.DanhSachKH();
                        ClearInput();
                    }
                    else
                        MessageBox.Show("Không thể xóa khách hàng  này");
                }
            }
            else
                MessageBox.Show("Nhập số điện thoại cần xóa");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isBlank())
            {
                DTO_KhachHang kh = new DTO_KhachHang()
                {
                    soDienThoai = txtDienThoai.Text,
                    diaChi = txtDiaChi.Text,
                    tenKhach = txtTenKhach.Text,
                    phai = rdoNam.Checked ? "Nam" : "Nữ"
                };
                if (bus_kh.updateKhachHang(kh))
                {
                    MessageBox.Show("Sửa thành công");
                    dataGridViewKhachHang.DataSource = bus_kh.DanhSachKH();
                    ClearInput();
                }
                else MessageBox.Show("Sửa thất bại");
            }
            else MessageBox.Show("Nhập thông tin trước khi sửa");
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
    }
}
