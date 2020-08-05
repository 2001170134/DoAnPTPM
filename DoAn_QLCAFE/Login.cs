using DoAn_QLCAFE;
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
    
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        QLCAFEDataContext qlcf = new QLCAFEDataContext();
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            List<QL_NguoiDung> ttk = getTenDN();
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập", "Thông báo", MessageBoxButtons.OK);
            else if (!kiemTraDangNhap())
            {
                return;
            }
            else
            {
                FrmMain main = new FrmMain();
                main.Show();
                this.Hide();
            }
        }
        public List<QL_NguoiDung> getTenDN()
        {
            var getTDN = from tdn in qlcf.QL_NguoiDungs select tdn;
            return getTDN.ToList<QL_NguoiDung>();
        }

        public bool kiemTraDangNhap()
        {
            List<QL_NguoiDung> listTK = getTenDN();
            string tendn = txtUsername.Text;
            string matkhau = txtPassword.Text;
            QL_NguoiDung taikhoan = null;
            foreach (QL_NguoiDung tk in listTK)
            {
                if (tk.TenDangNhap.Equals(tendn))
                {
                    taikhoan = new QL_NguoiDung();
                    taikhoan = tk;
                    break;
                }
            }
            if (taikhoan == null)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại.", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                if (taikhoan.MatKhau.Equals(matkhau))
                {
                    //loaiTaiKhoan = taikhoan.LoaiTaiKhoan;
                    return true;
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng.", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }
        }

        public bool kiemTraTaiKhoan(string tenDN)
        {
            QL_NguoiDung tk = qlcf.QL_NguoiDungs.Where(t => t.TenDangNhap == tenDN).FirstOrDefault();
            if (tk != null)
                return true;
            return false;
        }
    }
}
