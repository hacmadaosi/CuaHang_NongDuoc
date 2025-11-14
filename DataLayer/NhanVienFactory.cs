using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
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
        public bool xoaNhanVien(int _id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM NHAN_VIEN WHERE ID = @_id;");
            cmd.Parameters.Add("@_id", SqlDbType.Int).Value = _id;
            return m_Ds.ExecuteEffect(cmd);
        }
        public bool xuLyNhanVien(int _id, string tenDangNhap, string matKhau, int loaiTaiKhoan, string hoTen, string dienThoai, string diaChi)
        {
            SqlCommand cmd;
            string query;
            try
            {
                if (_id == -1)
                {
                    query = "INSERT INTO NHAN_VIEN (TEN_DANG_NHAP, MAT_KHAU, LOAI_TAI_KHOAN, HO_TEN, DIA_CHI, DIEN_THOAI) VALUES (@tenDangNhap, @matKhau, @loaiTaiKhoan, @hoTen, @diaChi, @dienThoai)";
                    cmd = new SqlCommand(query);
                }
                else
                {
                    query = "UPDATE NHAN_VIEN SET TEN_DANG_NHAP = @tenDangNhap, MAT_KHAU = @matKhau, LOAI_TAI_KHOAN = @loaiTaiKhoan, HO_TEN = @hoTen, DIA_CHI = @diaChi, DIEN_THOAI = @dienThoai WHERE ID = @_id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.Add("@_id", SqlDbType.Int).Value = _id;
                }

                cmd.Parameters.Add("@tenDangNhap", SqlDbType.VarChar).Value = tenDangNhap;
                cmd.Parameters.Add("@matKhau", SqlDbType.VarChar).Value = matKhau;
                cmd.Parameters.Add("@loaiTaiKhoan", SqlDbType.Int).Value = loaiTaiKhoan;
                cmd.Parameters.Add("@hoTen", SqlDbType.VarChar).Value = hoTen;
                cmd.Parameters.Add("@diaChi", SqlDbType.VarChar).Value = diaChi;
                cmd.Parameters.Add("@dienThoai", SqlDbType.VarChar).Value = dienThoai;
                return m_Ds.ExecuteEffect(cmd);

            }
            catch (Exception ex) {
                Debug.WriteLine("LỖI KHI XỬ LÝ NHÂN VIÊN: " + ex.ToString());
                return false;
            }
        }
        public DataTable TimTenNhanVien(String ten)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHAN_VIEN WHERE HO_TEN LIKE '%' + @ten + '%'");
            cmd.Parameters.Add("ten", SqlDbType.VarChar).Value = ten;
            m_Ds.Load(cmd);

            return m_Ds;
        }
    }
}
