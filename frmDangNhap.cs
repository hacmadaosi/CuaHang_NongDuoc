using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
using CuahangNongduoc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CuahangNongduoc
{
    public partial class frmDangNhap : Form
    {
        NhanVienController ctlNhanVien = new NhanVienController();
        string msg_ThongBao = "";
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            string user = txt_User.Text.Trim();
            string pass = txt_Password.Text.Trim();
            if(kiemTraTenDangNhap(user) && kiemTraMatKhau(pass))
            {
                DataTable nhanvien = ctlNhanVien.DangNhap(user, pass);
                if(nhanvien != null)
                {
                    UserSingleton.Instance.SetUser(
                        Convert.ToInt32(nhanvien.Rows[0]["ID"]),
                        nhanvien.Rows[0]["TEN_DANG_NHAP"].ToString(),
                        Convert.ToInt32(nhanvien.Rows[0]["LOAI_TAI_KHOAN"])
                    );
                    this.Hide();
                    (new frmMain()).Show();
                }
                else
                {
                    MessageBox.Show("Thông tin đăng nhập không chính xác.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show(msg_ThongBao, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                Application.Exit();
            }
        }

        private void lbe_ShowPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_Password.PasswordChar = txt_Password.PasswordChar == '•' ? '\0' : '•';
            lbe_ShowPass.Text = txt_Password.PasswordChar == '•' ? "Hiện mật khẩu" : "Ẩn mật khẩu";
        }

        private bool kiemTraTenDangNhap(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                msg_ThongBao = "Tên đăng nhập không được để trống.";
                return false;
            }
            return true;
        }
        private bool kiemTraMatKhau(string str) {
            if (string.IsNullOrEmpty(str))
            {
                msg_ThongBao = "Mật khẩu không được để trống.";
                return false; 
            }
            //if (str.Length < 6)
            //{
            //    msg_ThongBao = "Mật khẩu phải có ít nhất 6 kí tự.";
            //    return false;
            //}
            return true;
        }

        private void txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Submit_Click(sender, e);
            }
        }
    }
}
