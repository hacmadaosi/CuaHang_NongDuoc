using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc
{
    public partial class frmQuanLyTaiKhoanCaNhan : Form
    {
        private TaiKhoanController ctrl = new TaiKhoanController();
        private TaiKhoan currentUser;
        private bool isChanged = false;

        public frmQuanLyTaiKhoanCaNhan(TaiKhoan user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            try
            {
                // Load thông tin tài khoản hiện tại
                TaiKhoan tk = ctrl.LayTaiKhoanTheoID(currentUser.ID);
                if (tk != null)
                {
                    txtTenTaiKhoan.Text = tk.TenTaiKhoan;
                    txtTenNhanVien.Text = tk.TenNhanVien;
                    txtSoDienThoai.Text = tk.SoDienThoai;
                    lblVaiTro.Text = tk.VaiTro;
                    
                    // Disable tên tài khoản (không cho đổi)
                    txtTenTaiKhoan.ReadOnly = true;
                    txtTenTaiKhoan.BackColor = Color.FromArgb(240, 240, 240);
                    
                    // Cập nhật currentUser với thông tin đầy đủ
                    currentUser.TenTaiKhoan = tk.TenTaiKhoan;
                    currentUser.TenNhanVien = tk.TenNhanVien;
                    currentUser.SoDienThoai = tk.SoDienThoai;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin tài khoản: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(txtMatKhauCu.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauCu.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMatKhauMoi.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                if (txtMatKhauMoi.Text.Length < 6)
                {
                    MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text)
                {
                    MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtXacNhanMatKhau.Focus();
                    return;
                }

                // Kiểm tra mật khẩu cũ
                TaiKhoan tk = ctrl.DangNhap(currentUser.TenTaiKhoan, txtMatKhauCu.Text);
                if (tk == null)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhauCu.Focus();
                    txtMatKhauCu.SelectAll();
                    return;
                }

                // Cập nhật mật khẩu mới
                tk.MatKhau = txtMatKhauMoi.Text;
                if (ctrl.CapNhatTaiKhoan(tk))
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Clear các ô mật khẩu
                    txtMatKhauCu.Clear();
                    txtMatKhauMoi.Clear();
                    txtXacNhanMatKhau.Clear();
                    
                    // Cập nhật currentUser
                    currentUser.MatKhau = tk.MatKhau;

                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhatThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(txtTenNhanVien.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNhanVien.Focus();
                    return;
                }

                // Cập nhật thông tin
                TaiKhoan tk = ctrl.LayTaiKhoanTheoID(currentUser.ID);
                if (tk != null)
                {
                    tk.TenNhanVien = txtTenNhanVien.Text.Trim();
                    tk.SoDienThoai = txtSoDienThoai.Text.Trim();
                    
                    if (ctrl.CapNhatTaiKhoan(tk))
                    {
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Cập nhật currentUser
                        currentUser.TenNhanVien = tk.TenNhanVien;
                        currentUser.SoDienThoai = tk.SoDienThoai;
                        isChanged = false;
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin thất bại!", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTenNhanVien_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
        }

        private void frmQuanLyTaiKhoanCaNhan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isChanged)
            {
                DialogResult result = MessageBox.Show("Có thay đổi chưa được lưu. Bạn có muốn lưu trước khi thoát không?", 
                    "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    btnCapNhatThongTin_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmQuanLyTaiKhoanCaNhan_Load(object sender, EventArgs e)
        {
            isChanged = false;
        }
    }
}

