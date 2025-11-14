using CuahangNongduoc.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmNhanVien : Form
    {
        NhanVienController ctrl = new NhanVienController();
        int _idSelected = -1;
        string msg_Notify = "";
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            hienThiDsLoaiTaiKhoan(); 
            setEnableWidget(false);
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator, txt_TenDangNhap, txt_MatKhau, cbb_LoaiTaiKhoan, txt_HoTen, txt_SDT, txt_DiaChi);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                _idSelected = int.Parse(row.Cells[0].Value.ToString());
            }
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            setEnableWidget(true);
            txt_TenDangNhap.Focus();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            setEnableWidget(true);
            refreshInput();
            txt_TenDangNhap.Focus();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_HuyBo_Click(object sender, EventArgs e)
        {
            frmNhanVien_Load(sender, e);
            _idSelected = -1;
        }

        private void hienThiDsLoaiTaiKhoan()
        {
            cbb_LoaiTaiKhoan.DataSource = DsLoaiTaiKhoan();
            cbb_LoaiTaiKhoan.DisplayMember = "Display";
            cbb_LoaiTaiKhoan.ValueMember = "Value";
        }

        private DataTable DsLoaiTaiKhoan()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));
            dt.Columns.Add("Display", typeof(string));
            dt.Rows.Add(0, "Nhân viên");
            dt.Rows.Add(1, "Quản lý");
            return dt;
        }
        private void setEnableWidget(bool state)
        {
            txt_DiaChi.Enabled = state;
            txt_HoTen.Enabled = state;
            txt_SDT.Enabled = state;
            txt_TenDangNhap.Enabled = state;
            txt_MatKhau.Enabled = state;
            cbb_LoaiTaiKhoan.Enabled = state;
            btn_Them.Enabled = !state;
            btn_CapNhat.Enabled = !state;
            btn_Xoa.Enabled = !state;
            btn_HuyBo.Enabled = state;
            btn_Luu.Enabled = state;
            dataGridView.Enabled = !state;
        }
        private void refreshInput()
        {
            txt_DiaChi.Text = "";
            txt_HoTen.Text = "";
            txt_SDT.Text = "";
            txt_TenDangNhap.Text = "";
            txt_MatKhau.Text = "";
            cbb_LoaiTaiKhoan.SelectedIndex = 0;
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (kiemTraDauVao())
            {
                bool state = ctrl.xulyNhanVien(
                                _idSelected,
                                txt_TenDangNhap.Text,
                                txt_MatKhau.Text,
                                Convert.ToInt32(cbb_LoaiTaiKhoan.SelectedValue),
                                txt_HoTen.Text,
                                txt_SDT.Text,
                                txt_DiaChi.Text);

                if (state == false)
                {
                    MessageBox.Show("Phát sinh lỗi hệ thống vui lòng thử lại sau!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frmNhanVien_Load(sender, e);
            }
            else
            {
                MessageBox.Show(msg_Notify, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng với id = " + _idSelected + "?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool state = ctrl.xoaNhanVien(_idSelected);
                if (state == false) {
                    MessageBox.Show("Phát sinh lỗi vui lòng thử lại sau!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                frmNhanVien_Load(sender, e);
            }
        }

        private void txt_TimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                ctrl.TimTenNhanVien(txt_TimKiem.Text);
            }
        }

        private void txt_TimKiem_Enter(object sender, EventArgs e)
        {
            txt_TimKiem.Text = "";
            txt_TimKiem.ForeColor = Color.Black;
        }

        private void txt_TimKiem_Leave(object sender, EventArgs e)
        {
            ctrl.TimTenNhanVien("");
        }

        private bool kiemTraDauVao()
        {
            msg_Notify = "";

            if (string.IsNullOrEmpty(txt_TenDangNhap.Text))
            {
                msg_Notify = "Tên Đăng Nhập không được để trống.";
                return false;
            }

            if (string.IsNullOrEmpty(txt_MatKhau.Text))
            {
                msg_Notify = "Mật Khẩu không được để trống.";
                return false;
            }

            if (string.IsNullOrEmpty(txt_HoTen.Text))
            {
                msg_Notify = "Họ Tên không được để trống.";
                return false;
            }

            if (string.IsNullOrEmpty(txt_SDT.Text))
            {
                msg_Notify = "Số Điện Thoại không được để trống.";
                return false;
            }

            if (txt_SDT.Text.Length > 10)
            {
                msg_Notify = "Số Điện Thoại không được vượt quá 10 ký tự.";
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txt_SDT.Text, @"^\d+$"))
            {
                msg_Notify = "Số Điện Thoại chỉ được chứa ký tự số.";
                return false;
            }

            if (string.IsNullOrEmpty(txt_DiaChi.Text))
            {
                msg_Notify = "Địa Chỉ không được để trống.";
                return false;
            }

            return true;
        }
    }
}
