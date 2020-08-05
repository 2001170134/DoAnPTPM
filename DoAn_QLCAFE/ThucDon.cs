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
    public partial class ThucDon : UserControl
    {
        public ThucDon()
        {
            InitializeComponent();
        }
        void loadGVThucDon()
        {
            var mons = from m in qlcf.THUCDONs join l in qlcf.LOAITHUCDONs on m.MALOAI equals l.MALOAI select new { m.MAMON, m.TENMON, m.GIA, l.TENLOAI, m.DVT, m.SOLUONG, m.MOTA };
            dataGridView1.DataSource = mons;
        }
        QLCAFEDataContext qlcf = new QLCAFEDataContext();
        private void ThucDon_Load(object sender, EventArgs e)
        {
            cbmaloai.DataSource = qlcf.LOAITHUCDONs;
            cbmaloai.DisplayMember = "TENLOAI";
            cbmaloai.ValueMember = "MALOAI";

            cbdvt.DataSource = qlcf.THUCDONs;
            cbdvt.DisplayMember = "DVT";
            cbdvt.ValueMember = "MAMON";

            //var mons = from m in qlcf.THUCDONs join l in qlcf.LOAITHUCDONs on m.MALOAI equals l.MALOAI select new { m.MAMON, m.TENMON, m.GIA, l.TENLOAI,m.DVT,m.SOLUONG,m.MOTA };
            //dataGridView1.DataSource = mons;
            loadGVThucDon();
            dataGridView1.ReadOnly = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnluu.Enabled = false;

            txtMaMon.Enabled =txttenmon.Enabled=txtdongia.Enabled=txtsoluong.Enabled=txtMoTa.Enabled=cbdvt.Enabled=cbmaloai.Enabled =false;
            

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtMaMon.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txttenmon.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtdongia.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cbmaloai.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cbdvt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtsoluong.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtMoTa.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            
            btnsua.Enabled = btnxoa.Enabled = true;
            txtMaMon.Enabled = false;
            txttenmon.Enabled = false;
            txtdongia.Enabled = false;
            txtsoluong.Enabled = false;
            txtMoTa.Enabled = false;
            cbdvt.Enabled = false;
            cbmaloai.Enabled = false;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            btnluu.Enabled = true;
            txtMaMon.Enabled = txttenmon.Enabled = txtdongia.Enabled = txtsoluong.Enabled = txtMoTa.Enabled = cbdvt.Enabled = cbmaloai.Enabled = true;
            txtMaMon.Clear();
            txttenmon.Clear();
            txtdongia.Clear();
            txtsoluong.Clear();
            txtMoTa.Clear();
                  
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            string mamon = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            THUCDON thucdon_xoa = qlcf.THUCDONs.Where(t => t.MAMON == mamon).FirstOrDefault();
            qlcf.THUCDONs.DeleteOnSubmit(thucdon_xoa);
            qlcf.SubmitChanges();
            var thucdons = from td in qlcf.THUCDONs select td;
            dataGridView1.DataSource = thucdons;
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            btnluu.Enabled = true;
            txtMaMon.Enabled = false;
            txttenmon.Enabled = txtdongia.Enabled = txtsoluong.Enabled = txtMoTa.Enabled = cbdvt.Enabled = cbmaloai.Enabled = true;

            //string maloai = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //LOAITHUCDON loai_sua = qlcf.LOAITHUCDONs.Where(t => t.MALOAI == maloai).FirstOrDefault();

            //loai_sua.TENLOAI = txttenhang.Text;
            //qlcf.SubmitChanges();
            //var loais = from l in qlcf.LOAITHUCDONs select l;
            //dataGridView1.DataSource = loais;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            

        }

        bool kt_khoachinh(string ma)
        {
            THUCDON mamons = qlcf.THUCDONs.FirstOrDefault(t => t.MAMON == ma);
            if (mamons != null)
                return true;           
                return false;

        }

        private void btnsua_Click_1(object sender, EventArgs e)
        {
            btnluu.Enabled = true;
            txtMaMon.Enabled = false;
            txttenmon.Enabled = txtdongia.Enabled = txtsoluong.Enabled = txtMoTa.Enabled = cbdvt.Enabled = cbmaloai.Enabled = true;
        }

        private void btnluu_Click_1(object sender, EventArgs e)
        {
            if (kt_khoachinh(txtMaMon.Text))
            {
                if (txtMaMon.Enabled == false)
                {
                    string mamon = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    THUCDON mon_sua = qlcf.THUCDONs.FirstOrDefault(t => t.MAMON == mamon);

                    mon_sua.TENMON = txttenmon.Text;
                    mon_sua.GIA = int.Parse(txtdongia.Text.Trim());
                    mon_sua.MOTA = txtMoTa.Text;
                    mon_sua.MALOAI = cbmaloai.SelectedValue.ToString();
                    mon_sua.DVT = cbdvt.Text;
                    mon_sua.SOLUONG = int.Parse(txtsoluong.Text.Trim());
                    qlcf.SubmitChanges();
                    //var mons = from m in qlcf.THUCDONs select m;
                    //dataGridView1.DataSource = mons;
                    loadGVThucDon();
                    MessageBox.Show("Sửa thành công");
                }
                else
                {
                    MessageBox.Show("Trùng mã loại");
                }
                
            }

            else
            {
                
                    THUCDON mon_them = new THUCDON();
                    mon_them.MAMON = txtMaMon.Text;
                    mon_them.TENMON = txttenmon.Text;
                    mon_them.GIA = int.Parse(txtdongia.Text.Trim());
                    mon_them.MOTA = txtMoTa.Text;
                    mon_them.MALOAI = cbmaloai.SelectedValue.ToString();
                    mon_them.DVT = cbdvt.Text;
                    mon_them.SOLUONG = int.Parse(txtsoluong.Text.Trim());


                    qlcf.THUCDONs.InsertOnSubmit(mon_them);
                    qlcf.SubmitChanges();
                    //var mons = from m in qlcf.THUCDONs select m;
                    //dataGridView1.DataSource = mons;
                    loadGVThucDon();
                    MessageBox.Show("Thêm thành công");
               
            }
            btnluu.Enabled = false;
            txtMaMon.Enabled = txttenmon.Enabled = txtdongia.Enabled = txtsoluong.Enabled = txtMoTa.Enabled = cbdvt.Enabled = cbmaloai.Enabled = false;
        }
    }
}
