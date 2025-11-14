using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CuahangNongduoc.DataLayer
{
    internal class NhanVienFactory
    {
        DataService m_Ds = new DataService();

        public DataTable KiemTraTaiKhoan(string tenDangNhap, string matKhau)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHAN_VIEN WHERE TEN_DANG_NHAP = @tenDangNhap AND MAT_KHAU = @matKhau");
            cmd.Parameters.Add("tenDangNhap", SqlDbType.VarChar).Value = tenDangNhap;
            cmd.Parameters.Add("matKhau", SqlDbType.VarChar).Value = matKhau;
            m_Ds.Load(cmd);

            return m_Ds;
        }
        public DataTable DsNhanVien() {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHAN_VIEN");
            m_Ds.Load(cmd);
            return m_Ds; 
        }
    }
}
