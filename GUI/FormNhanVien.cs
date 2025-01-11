using BUS_QLBanHang;
using DTO;
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
    public partial class FormNhanVien : Form
    {
        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private BUS_NhanVien bus_nv = new BUS_NhanVien();
        private void btnThem_Click(object sender, EventArgs e)
        {

            if (!isBlank())
            {
                DTO_NhanVien nv = new DTO_NhanVien()
                {
                    EmailNV = txtEmail.Text,
                    TenNhanVien = txtTenNhanVien.Text,
                    DiaChi = txtDiaChi.Text,
                    VaiTro = rdoQuanLy.Checked ? 1 : 0,
                    TinhTrang = rdoHoatDong.Checked ? 1 : 0,
                    MatKhau = bus_nv.getPassword()

                };
                if (bus_nv.insertNhanVien(nv))
                {
                    MessageBox.Show("Thêm thành công");
                    dataGridViewNhanVien.DataSource = bus_nv.danhSachNV();
                    ClearInput();
                }
                else
                    MessageBox.Show("Thêm thất bại , kiểm tra dữ kiệu");
            }
            else
                MessageBox.Show("Thêm thất bại, thieu du lieu ");
        }
        void ClearInput()
        {

            txtDiaChi.Clear();
            txtEmail.Clear();
            txtTenNhanVien.Clear();
            rdoQuanLy.Checked = false;
            rdoNhanVien.Checked = false;
            rdoHoatDong.Checked = false;
            rdoNgungHoatDong.Checked = false;

        }
        void Load_Gridview()
        {
            dataGridViewNhanVien.DataSource = bus_nv.danhSachNV();
            dataGridViewNhanVien.Columns[0].HeaderText = "Email";
            dataGridViewNhanVien.Columns[1].HeaderText = "Tên Nhân Viên";
            dataGridViewNhanVien.Columns[2].HeaderText = "Địa chỉ";
            dataGridViewNhanVien.Columns[3].HeaderText = "Vai trò";
            dataGridViewNhanVien.Columns[4].HeaderText = "Tình trạng";
        }
        private bool isBlank()
        {
            return txtDiaChi.Text == null
            || txtEmail.Text == null
            || txtTenNhanVien.Text == null
            || (rdoNhanVien.Checked == false && rdoQuanLy.Checked == false)
            || (rdoHoatDong.Checked == false && rdoNgungHoatDong.Checked == false);

        }


        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            showNhanVien();        }

        private void loadGridView()
        {
            dataGridViewNhanVien.Columns[0].HeaderText = "Email";
            dataGridViewNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dataGridViewNhanVien.Columns[2].HeaderText = "Địa chỉ";
            dataGridViewNhanVien.Columns[3].HeaderText = "Vai trò";
            dataGridViewNhanVien.Columns[4].HeaderText = "Tình trạng";
            dataGridViewNhanVien.Columns[0].Width = 170;
            dataGridViewNhanVien.Columns[0].DividerWidth = 2;
            dataGridViewNhanVien.Columns[1].DividerWidth = 2;
            dataGridViewNhanVien.Columns[2].DividerWidth = 2;
            dataGridViewNhanVien.Columns[3].DividerWidth = 2;
            dataGridViewNhanVien.Columns[4].DividerWidth = 2;
        }
        private void showNhanVien()
        {
            dataGridViewNhanVien.DataSource = bus_nv.danhSachNV();
            loadGridView();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                string email = txtEmail.Text;
                DialogResult result = MessageBox.Show(" Không thể khôi phục sau khi xóa. Bạn có chắc chắn muốn xóa? ", "Cảnh báo", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bus_nv.deleteNhanVien(email))
                    {
                        MessageBox.Show("Xóa thành công");
                        Load_Gridview();
                        ClearInput();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập email của nhân viên cần xóa cần xóa");
            }
        }
        

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txttimkiem.Text != "")
            {
                DataTable data = bus_nv.searchNhanVien(txttimkiem.Text);
                if (data.Rows.Count > 0)
                {
                    dataGridViewNhanVien.DataSource = data;
                    loadGridView();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào!");
                }
            }
            else MessageBox.Show("Vui lòng nhập tên cần tìm kiếm");
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void dataGridViewNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            rdoNgungHoatDong.Enabled = true;
            rdoHoatDong.Enabled = true;
            rdoNhanVien.Enabled = true;
            rdoQuanLy.Enabled = true;
           
            txtEmail.Text = dataGridViewNhanVien.CurrentRow.Cells[0].Value.ToString();
            txtTenNhanVien.Text = dataGridViewNhanVien.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dataGridViewNhanVien.CurrentRow.Cells[2].Value.ToString();
            int vaitro = int.Parse(dataGridViewNhanVien.CurrentRow.Cells[3].Value.ToString());
            int tinhtrang = int.Parse(dataGridViewNhanVien.CurrentRow.Cells[4].Value.ToString());
            if (vaitro == 1)
                rdoQuanLy.Checked = true;
            else
                rdoNhanVien.Checked = true;
            if (tinhtrang == 1)
                rdoHoatDong.Checked = true;
            else
                rdoNgungHoatDong.Checked = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isBlank())
            {
                DTO_NhanVien nv = new DTO_NhanVien()
                {
                    EmailNV = txtEmail.Text,
                    TenNhanVien = txtTenNhanVien.Text,
                    DiaChi = txtDiaChi.Text,
                    VaiTro = rdoQuanLy.Checked ? 1 : 0,
                    TinhTrang = rdoHoatDong.Checked ? 1 : 0,
                    

                };
                if (bus_nv.updateNhanVien(nv))
                {
                    MessageBox.Show("Cập nhật thành công");
                    dataGridViewNhanVien.DataSource = bus_nv.danhSachNV();
                    ClearInput();
                }
                else
                    MessageBox.Show("Cập nhật thất bại , kiểm tra dữ kiệu");
            }
            else
                MessageBox.Show("Cập nhật thất bại, thieu du lieu ");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           

        }
    }
}
