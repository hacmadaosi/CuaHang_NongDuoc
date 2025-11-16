using System;
using System.Windows.Forms;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc.Controller
{
    /// <summary>
    /// Class hỗ trợ quản lý phân quyền cho Quản lý và Nhân viên
    /// </summary>
    public class PhanQuyenHelper
    {
        private TaiKhoan taiKhoan;

        public PhanQuyenHelper(TaiKhoan tk)
        {
            this.taiKhoan = tk;
        }

        /// <summary>
        /// Kiểm tra xem tài khoản có phải là Quản lý không
        /// </summary>
        public bool LaQuanLy()
        {
            return taiKhoan != null && taiKhoan.LaQuanLy();
        }

        /// <summary>
        /// Kiểm tra xem tài khoản có phải là Nhân viên không
        /// </summary>
        public bool LaNhanVien()
        {
            return taiKhoan != null && taiKhoan.LaNhanVien();
        }

        /// <summary>
        /// Ẩn hoặc hiện menu item dựa trên quyền
        /// </summary>
        public void ApDungPhanQuyen(ToolStripMenuItem menuItem, bool choPhepQuanLy, bool choPhepNhanVien)
        {
            if (menuItem == null) return;

            bool coQuyen = false;
            if (LaQuanLy())
            {
                coQuyen = choPhepQuanLy;
            }
            else if (LaNhanVien())
            {
                coQuyen = choPhepNhanVien;
            }

            menuItem.Visible = coQuyen;
            menuItem.Enabled = coQuyen;
        }

        /// <summary>
        /// Ẩn hoặc hiện tool strip button dựa trên quyền
        /// </summary>
        public void ApDungPhanQuyen(ToolStripButton toolButton, bool choPhepQuanLy, bool choPhepNhanVien)
        {
            if (toolButton == null) return;

            bool coQuyen = false;
            if (LaQuanLy())
            {
                coQuyen = choPhepQuanLy;
            }
            else if (LaNhanVien())
            {
                coQuyen = choPhepNhanVien;
            }

            toolButton.Visible = coQuyen;
            toolButton.Enabled = coQuyen;
        }

        /// <summary>
        /// Kiểm tra quyền trước khi thực hiện hành động
        /// </summary>
        public bool KiemTraQuyen(bool choPhepQuanLy, bool choPhepNhanVien)
        {
            if (LaQuanLy())
            {
                return choPhepQuanLy;
            }
            else if (LaNhanVien())
            {
                return choPhepNhanVien;
            }

            return false;
        }

        /// <summary>
        /// Hiển thị thông báo khi không có quyền
        /// </summary>
        public void HienThiThongBaoKhongQuyen()
        {
            MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

