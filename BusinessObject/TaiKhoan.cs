using System;
using System.Collections.Generic;
using System.Text;

namespace CuahangNongduoc.BusinessObject
{
    public class TaiKhoan
    {
        private string m_ID;
        public string ID
        {
            get { return m_ID; }
            set { m_ID = value; }

        }
        private string m_TenTaiKhoan;

        public string TenTaiKhoan
        {
            get { return m_TenTaiKhoan; }
            set { m_TenTaiKhoan = value; }
        }
        private string m_MatKhau;

        public string MatKhau
        {
            get { return m_MatKhau; }
            set { m_MatKhau = value; }
        }
        private string m_TenNhanVien;

        public string TenNhanVien
        {
            get { return m_TenNhanVien; }
            set { m_TenNhanVien = value; }
        }
        private string m_SoDienThoai;
        public string SoDienThoai
        {
            get { return m_SoDienThoai; }
            set { m_SoDienThoai = value; }
        }
        private bool m_Admin;
        public bool Admin
        {
            get { return m_Admin; }
            set { m_Admin = value; }
        }

        private string m_VaiTro;
        public string VaiTro
        {
            get 
            { 
                // Nếu Admin = true thì là Quản lý, ngược lại là Nhân viên
                if (m_Admin)
                    return "Quản lý";
                return "Nhân viên";
            }
            set 
            { 
                m_VaiTro = value;
                // Cập nhật Admin dựa trên VaiTro
                m_Admin = (value == "Quản lý");
            }
        }

        public bool LaQuanLy()
        {
            return m_Admin || (m_VaiTro == "Quản lý");
        }

        public bool LaNhanVien()
        {
            return !m_Admin && (m_VaiTro != "Quản lý");
        }
    }
}
