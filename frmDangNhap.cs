using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmDangNhap : Form
    {
        private TaiKhoanController taiKhoanController = new TaiKhoanController();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;

            TaiKhoan tk = taiKhoanController.DangNhap(tenTaiKhoan, matKhau);
            bool loginSuccessful = tk != null;

            if (loginSuccessful)
            {
              
                this.Hide();

                using (var frmMain = new frmMain(tk, this))
                {
                    frmMain.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra thông tin đăng nhập.");
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lbe_ShowPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtMatKhau.PasswordChar = txtMatKhau.PasswordChar == '•' ? '\0' : '•';
            lbe_ShowPass.Text = txtMatKhau.PasswordChar == '•' ? "Hiện mật khẩu" : "Ẩn mật khẩu";
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                btnDangNhap_Click(sender, e);
            }
        }
    }
}
