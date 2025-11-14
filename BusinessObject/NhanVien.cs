using System;
using System.Collections.Generic;
using System.Text;

namespace CuahangNongduoc.BusinessObject
{
    internal class NhanVien
    { 
        string m_Id, m_HoTen, m_TenDangNhap, m_MatKhau, m_DiaChi, m_LoaiTaiKhoan;
        public int Id { get; set; }

        public string TenDangNhap { get; set; }

        public string MatKhau { get; set; }

        public string HoTen { get; set; }

        public string DiaChi { get; set; }

        public int LoaiTaiKhoan { get; set; }
    }
}
