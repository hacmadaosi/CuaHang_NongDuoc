using CuahangNongduoc.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc.Controller
{
    internal class NhanVienController
    {
        NhanVienFactory _factory = new NhanVienFactory();

        public DataTable DangNhap(string tenDangNhap, string matKhau)
        {
            DataTable data = _factory.KiemTraTaiKhoan(tenDangNhap, matKhau);
            return data.Rows.Count > 0 ? data : null;
        }
        public void HienthiDataGridview(System.Windows.Forms.DataGridView dg, System.Windows.Forms.BindingNavigator bn, 
            TextBox tenDangNhap, TextBox matKhau, ComboBox loaiTaiKhoan, TextBox hoTen, TextBox soDienThoai, TextBox diaChi)
        {
            System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
            bs.DataSource = _factory.DsNhanVien();

            // Bliding
            tenDangNhap.DataBindings.Clear();
            tenDangNhap.DataBindings.Add("Text", bs, "TEN_DANG_NHAP");

            matKhau.DataBindings.Clear();
            matKhau.DataBindings.Add("Text", bs, "MAT_KHAU");

            loaiTaiKhoan.DataBindings.Clear();
            loaiTaiKhoan.DataBindings.Add("SelectedValue", bs, "LOAI_TAI_KHOAN");

            hoTen.DataBindings.Clear();
            hoTen.DataBindings.Add("Text", bs, "HO_TEN");

            soDienThoai.DataBindings.Clear();
            soDienThoai.DataBindings.Add("Text", bs, "DIEN_THOAI");

            diaChi.DataBindings.Clear();
            diaChi.DataBindings.Add("Text", bs, "DIA_CHI");

            bn.BindingSource = bs;
            dg.DataSource = bs;

            foreach (DataGridViewColumn col in dg.Columns)
            {
                if (col.HeaderText == "TEN_DANG_NHAP" || col.HeaderText == "MAT_KHAU")
                {
                    col.Visible = false;
                }
            }
        }

        public bool xulyNhanVien(int _id, string tenDangNhap, string matKhau, int loaiTaiKhoan, string hoTen, string dienThoai, string diaChi)
        {
            return _factory.xuLyNhanVien(_id, tenDangNhap, matKhau, loaiTaiKhoan, hoTen, dienThoai, diaChi);
        }

        public bool xoaNhanVien(int _id)
        {
            return _factory.xoaNhanVien(_id);
        }
        public void TimTenNhanVien(String ten)
        {
            _factory.TimTenNhanVien(ten);
        }


    }
}
