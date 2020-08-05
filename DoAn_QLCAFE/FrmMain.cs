using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_QLCAFE
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btn_loaimon_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            LoaiDoUong tq = new LoaiDoUong();
            panel1.Controls.Add(tq);
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_monan_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            ThucDon td = new ThucDon();
            panel1.Controls.Add(td);
        }

        private void btn_exits_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đóng?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btn_BanAn_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            BanAn td = new BanAn();
            panel1.Controls.Add(td);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
