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
        public void HienthiDataGridview(System.Windows.Forms.DataGridView dg, System.Windows.Forms.BindingNavigator bn)
        {
            System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
            DataTable tbl = _factory.DsNhanVien();
            bs.DataSource = tbl;
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

       
    }
}
