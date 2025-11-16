using System;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using System.Data;

namespace CuahangNongduoc.Controller
{
    public class TaiKhoanController
    {
        private TaiKhoanFactory factory = new TaiKhoanFactory();

        public TaiKhoan DangNhap(string tenTaiKhoan, string matKhau)
        {
        
            DataTable dataTable = factory.LayTaiKhoanTheoTen(tenTaiKhoan);
          
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                string storedPassword = row["MatKhau"].ToString();

                if (matKhau == storedPassword)
                {
                    TaiKhoan tk = new TaiKhoan();
                    tk.TenNhanVien = row["TenNhanVien"].ToString();
                    tk.ID = row["ID"].ToString();
                    // Load thông tin Admin để xác định vai trò
                    if (row.Table.Columns.Contains("Admin"))
                    {
                        tk.Admin = row["Admin"] != DBNull.Value && bool.Parse(row["Admin"].ToString());
                    }
                    return tk;
                }
            }
            return null;
        }
        public TaiKhoan LayTaiKhoanTheoID(string idTaiKhoan)
        {
            DataTable dataTable = factory.LayTaiKhoanTheoID(idTaiKhoan);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return factory.ConvertToTaiKhoan(row);
            }

            return null;
        }

        public void HienthiDataGridview(System.Windows.Forms.DataGridView dg, System.Windows.Forms.BindingNavigator bn)
        {
            System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
            bs.DataSource = factory.LayTatCaTaiKhoan();
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public DataRow NewRow()
        {
            return factory.NewRow();
        }

        public void Add(DataRow row)
        {
            factory.Add(row);
        }

        public bool Save()
        {
            return factory.Save();
        }

        public bool CapNhatTaiKhoan(TaiKhoan taiKhoan)
        {
            return factory.CapNhatTaiKhoan(taiKhoan);
        }
        public void TimHoTen(String hoten)
        {
            factory.TimHoTen(hoten);
        }
    }
}