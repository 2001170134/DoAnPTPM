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
    public partial class LoaiDoUong : UserControl
    {
        public LoaiDoUong()
        {
            InitializeComponent();
        }
        QLCAFEDataContext qlcf = new QLCAFEDataContext();
        private void LoaiDoUong_Load(object sender, EventArgs e)
        {
            var loais = from l in qlcf.LOAITHUCDONs select l;
            dataGridView1.DataSource = loais;
            dataGridView1.ReadOnly = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnluu.Enabled = false;

            txttenhang.Enabled = false;
            txtMaLoai.Enabled = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtMaLoai.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txttenhang.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            btnsua.Enabled = btnxoa.Enabled = true;
            txttenhang.Enabled = false;
            txtMaLoai.Enabled = false;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            btnluu.Enabled = true;
            txttenhang.Enabled = txtMaLoai.Enabled = true;
            txttenhang.Clear();
            txtMaLoai.Clear();
            btnsua.Enabled = false;
            btnxoa.Enabled = false;

            //LOAITHUCDON mh_them = new LOAITHUCDON();
            //mh_them.MALOAI = txtMaLoai.Text;
            //mh_them.TENLOAI = txttenhang.Text;
            //qlcf.LOAITHUCDONs.InsertOnSubmit(mh_them);
            //qlcf.SubmitChanges();
            //var loais = from l in qlcf.LOAITHUCDONs select l;
            //dataGridView1.DataSource = loais;
            //MessageBox.Show("Thêm thành công");
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            btnluu.Enabled = true;
            txttenhang.Enabled = true;
            txtMaLoai.Enabled = false;

            //string maloai = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //LOAITHUCDON loai_sua = qlcf.LOAITHUCDONs.Where(t => t.MALOAI == maloai).FirstOrDefault();

            //loai_sua.TENLOAI = txttenhang.Text;
            //qlcf.SubmitChanges();
            //var loais = from l in qlcf.LOAITHUCDONs select l;
            //dataGridView1.DataSource = loais;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            string maloai = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            LOAITHUCDON loai_xoa = qlcf.LOAITHUCDONs.Where(t => t.MALOAI == maloai).FirstOrDefault();
            qlcf.LOAITHUCDONs.DeleteOnSubmit(loai_xoa);
            qlcf.SubmitChanges();
            var loais = from l in qlcf.LOAITHUCDONs select l;
            dataGridView1.DataSource = loais;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (txttenhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên thực đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttenhang.Focus();
                return;
            }
            if (txtMaLoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoai.Focus();
                return;
            }

            if(kt_khoachinh(txtMaLoai.Text))
            {
                if (txtMaLoai.Enabled == false)
                {                  
                        string maloai = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        LOAITHUCDON loai_sua = qlcf.LOAITHUCDONs.FirstOrDefault(t => t.MALOAI == maloai);
                        loai_sua.TENLOAI = txttenhang.Text;
                        qlcf.SubmitChanges();
                        var loais = from l in qlcf.LOAITHUCDONs select l;
                        dataGridView1.DataSource = loais;
                        MessageBox.Show("Sửa thành công");                
                                     
                }
                else
                {
                    MessageBox.Show("Trùng mã loại");
                } 
            }
                
            else
            {
                LOAITHUCDON loai_them = new LOAITHUCDON();
                loai_them.MALOAI = txtMaLoai.Text;
                loai_them.TENLOAI = txttenhang.Text;
                qlcf.LOAITHUCDONs.InsertOnSubmit(loai_them);
                qlcf.SubmitChanges();
                var loais = from l in qlcf.LOAITHUCDONs select l;
                dataGridView1.DataSource = loais;
                MessageBox.Show("Thêm thành công");
            }
            btnluu.Enabled = false;
            txttenhang.Enabled = txtMaLoai.Enabled = false;

        }

        bool kt_khoachinh(string ma)
        {
            LOAITHUCDON maloai = qlcf.LOAITHUCDONs.FirstOrDefault(t => t.MALOAI == ma);
            if (maloai != null)
                return true;          
                return false;

        }
    }
}
