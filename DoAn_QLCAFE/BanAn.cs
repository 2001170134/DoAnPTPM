using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_QLCAFE
{
    public partial class BanAn : UserControl
    {
        public BanAn()
        {
            InitializeComponent();
        }
        void loadGVBan()
        {
            var bans = from b in qlcf.DANHSACHBANs select b;
            dataGridView1.DataSource = bans;
            dataGridView1.ReadOnly = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnluu.Enabled = false;
        }
        QLCAFEDataContext qlcf = new QLCAFEDataContext();
        private void BanAn_Load(object sender, EventArgs e)
        {
            loadGVBan();
            cbTrangThai.DataSource = qlcf.DANHSACHBANs;
            cbTrangThai.ValueMember = "TRANGTHAI";
          
            txtMaBan.Enabled = false;
            txtTenBan.Enabled = false;
            cbTrangThai.Enabled = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtMaBan.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTenBan.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbTrangThai.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            btnsua.Enabled = btnxoa.Enabled = true;
            txtMaBan.Enabled = false;
            txtTenBan.Enabled = false;
            cbTrangThai.Enabled = false;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            btnluu.Enabled = true;
            txtMaBan.Enabled = txtTenBan.Enabled =cbTrangThai.Enabled = true;
            txtMaBan.Clear();
            txtTenBan.Clear();
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            string maban = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            DANHSACHBAN ban_xoa = qlcf.DANHSACHBANs.Where(t => t.MABAN == maban).FirstOrDefault();
            qlcf.DANHSACHBANs.DeleteOnSubmit(ban_xoa);
            qlcf.SubmitChanges();
            var bans = from l in qlcf.DANHSACHBANs select l;
            dataGridView1.DataSource = bans;
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            btnluu.Enabled = true;
            txtTenBan.Enabled = true;
            txtMaBan.Enabled = false;
            cbTrangThai.Enabled = true;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (txtTenBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên bàn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenBan.Focus();
                return;
            }
            if (txtMaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã bàn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaBan.Focus();
                return;
            }

            if(kt_khoachinh(txtMaBan.Text))
            {
                if (txtMaBan.Enabled == false)
                {                  
                        string maban = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        DANHSACHBAN ban_sua = qlcf.DANHSACHBANs.FirstOrDefault(t => t.MABAN == maban);
                        ban_sua.TENBAN = txtTenBan.Text;
                        ban_sua.TRANGTHAI=cbTrangThai.SelectedValue.ToString();
                        qlcf.SubmitChanges();
                        loadGVBan();
                        MessageBox.Show("Sửa thành công");                
                                     
                }
                else
                {
                    MessageBox.Show("Trùng mã loại");
                } 
            }
                
            else
            {
                DANHSACHBAN ban_them = new DANHSACHBAN();
                ban_them.MABAN = txtMaBan.Text;
                ban_them.TENBAN = txtTenBan.Text;
                ban_them.TRANGTHAI=cbTrangThai.SelectedValue.ToString();
                qlcf.DANHSACHBANs.InsertOnSubmit(ban_them);
                qlcf.SubmitChanges();
                loadGVBan();
                MessageBox.Show("Thêm thành công");
            }
            btnluu.Enabled = false;
            txtMaBan.Enabled = txtTenBan.Enabled = cbTrangThai.Enabled=false;

        }

        bool kt_khoachinh(string ma)
        {
            DANHSACHBAN maban = qlcf.DANHSACHBANs.FirstOrDefault(t => t.MABAN == ma);
            if (maban != null)
                return true;          
                return false;

        
        }
    }
}
