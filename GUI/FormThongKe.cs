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
    public partial class FormThongKe : Form
    {
        public FormThongKe()
        {
            InitializeComponent();
        }

        private void sảnPhẩmNhậpKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BUS_QLBanHang.BUS_Hang busHang = new BUS_QLBanHang.BUS_Hang();
            dataGridView1.DataSource = busHang.thongKeSP();
            designView();
        }
        void designView()
        {
            dataGridView1.Columns[0].HeaderText = "Mã nhân viên";
            dataGridView1.Columns[1].HeaderText = "Tên nhân viên";
            dataGridView1.Columns[2].HeaderText = "Số lượng sản phẩm nhập";
         

           
           

        }

        private void sảnPhẩmTồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BUS_QLBanHang.BUS_Hang busHang = new BUS_QLBanHang.BUS_Hang();
            dataGridView1.DataSource = busHang.thongKeTonKho();
            designView2();
        }
        void designView2()
        {
            dataGridView1.Columns[0].HeaderText = "Tên hàng";
            dataGridView1.Columns[1].HeaderText = "Số lượng";
           
            
        }
    }
}
