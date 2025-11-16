using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class TaiKhoanFactory
    {
        private DataService m_Ds = new DataService();

        private DataTable QueryTaiKhoan(string query, params object[] parameters)
        {
            // Tạo DataService mới cho query tìm kiếm (không ảnh hưởng đến m_Ds chính)
            DataService ds = new DataService();
            var cmd = new OleDbCommand(query);
            for (int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.Add("param" + i, OleDbType.VarChar, 50).Value = parameters[i];
            }
            ds.Load(cmd);
            return ds;
        }



        public DataTable LayTaiKhoanTheoID(string idTaiKhoan)
        {
            // Dùng ? thay vì @id cho OleDb
            string query = "SELECT * FROM TAI_KHOAN WHERE ID = ?";
            return QueryTaiKhoan(query, idTaiKhoan);
        }

        public DataTable LayTaiKhoanTheoTen(string tenTaiKhoan)
        {
            // Dùng ? thay vì @tenTaiKhoan cho OleDb
            string query = "SELECT * FROM TAI_KHOAN WHERE TenTaiKhoan = ?";
            return QueryTaiKhoan(query, tenTaiKhoan);
        }

        public DataTable LayTatCaTaiKhoan()
        {
            // Dùng m_Ds chính để bind với DataGridView, giống DonViTinhFactory
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM TAI_KHOAN");
            m_Ds.Load(cmd);
            // Set PrimaryKey để có thể dùng Find()
            if (m_Ds.Columns["ID"] != null && m_Ds.PrimaryKey.Length == 0)
            {
                m_Ds.PrimaryKey = new DataColumn[] { m_Ds.Columns["ID"] };
            }
            // Convert ID sang string sau khi load (nếu cần)
            if (m_Ds.Columns["ID"] != null && m_Ds.Rows.Count > 0)
            {
                foreach (DataRow row in m_Ds.Rows)
                {
                    if (row["ID"] != null && row["ID"] != DBNull.Value)
                    {
                        // Đảm bảo ID là string
                        row["ID"] = row["ID"].ToString();
                    }
                }
            }
            return m_Ds;
        }

        public bool ThemTaiKhoan(TaiKhoan taiKhoan)
        {
            DataRow row = m_Ds.NewRow();
            row["ID"] = taiKhoan.ID;
            row["TenTaiKhoan"] = taiKhoan.TenTaiKhoan;
            row["MatKhau"] = taiKhoan.MatKhau;
            row["TenNhanVien"] = taiKhoan.TenNhanVien;
            row["SoDienThoai"] = taiKhoan.SoDienThoai;
            row["Admin"] = taiKhoan.Admin;

            m_Ds.Rows.Add(row);

            return m_Ds.ExecuteNoneQuery() > 0;
        }

        public bool CapNhatTaiKhoan(TaiKhoan taiKhoan)
        {
            // Đảm bảo m_Ds đã được load dữ liệu
            if (m_Ds.Rows.Count == 0 || m_Ds.Columns.Count == 0)
            {
                // Load dữ liệu từ database
                LayTatCaTaiKhoan();
            }
            
            // Đảm bảo PrimaryKey được set
            if (m_Ds.Columns["ID"] != null && m_Ds.PrimaryKey.Length == 0)
            {
                m_Ds.PrimaryKey = new DataColumn[] { m_Ds.Columns["ID"] };
            }
            
            // Tìm row bằng Find hoặc duyệt qua rows
            DataRow row = null;
            if (m_Ds.PrimaryKey.Length > 0)
            {
                try
                {
                    row = m_Ds.Rows.Find(taiKhoan.ID);
                }
                catch
                {
                    // Nếu Find không hoạt động, tìm bằng cách duyệt
                    row = null;
                }
            }
            
            // Nếu không tìm được bằng Find, duyệt qua rows
            if (row == null)
            {
                foreach (DataRow r in m_Ds.Rows)
                {
                    if (r["ID"] != null && r["ID"].ToString() == taiKhoan.ID)
                    {
                        row = r;
                        break;
                    }
                }
            }
            
            if (row != null)
            {
                row["TenTaiKhoan"] = taiKhoan.TenTaiKhoan;
                row["MatKhau"] = taiKhoan.MatKhau;
                row["TenNhanVien"] = taiKhoan.TenNhanVien;
                row["SoDienThoai"] = taiKhoan.SoDienThoai;
                row["Admin"] = taiKhoan.Admin;

                return m_Ds.ExecuteNoneQuery() > 0;
            }

            return false;
        }

        public bool XoaTaiKhoan(string idTaiKhoan)
        {
            DataRow row = m_Ds.Rows.Find(idTaiKhoan);
            if (row != null)
            {
                row.Delete();
                return m_Ds.ExecuteNoneQuery() > 0;
            }

            return false;
        }

        public TaiKhoan ConvertToTaiKhoan(DataRow row)
        {
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.ID = row["ID"].ToString();
            taiKhoan.TenTaiKhoan = row["TenTaiKhoan"].ToString();
            taiKhoan.TenNhanVien = row["TenNhanVien"].ToString();
            taiKhoan.MatKhau = row["MatKhau"].ToString();
            taiKhoan.SoDienThoai = row["SoDienThoai"].ToString();
            taiKhoan.Admin = bool.Parse(row["Admin"].ToString());
            return taiKhoan;
        }

        public void LoadSchema()
        {
            // Chỉ load schema nếu m_Ds chưa có dữ liệu
            if (m_Ds.Rows.Count == 0 && m_Ds.Columns.Count == 0)
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM TAI_KHOAN WHERE ID='-1'");
                m_Ds.Load(cmd);
            }
            // Đảm bảo cột ID là string type
            if (m_Ds.Columns["ID"] != null)
            {
                m_Ds.Columns["ID"].DataType = typeof(string);
            }
        }

        public DataRow NewRow()
        {
            // Đảm bảo schema đã được load
            if (m_Ds.Columns.Count == 0)
            {
                LoadSchema();
            }
            return m_Ds.NewRow();
        }

        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }

        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }
        public DataTable TimHoTen(string hoten)
        {
            string query = "SELECT * FROM TAI_KHOAN WHERE TenNhanVien LIKE '%' + @hoten + '%'";

            var cmd = new OleDbCommand(query);
            cmd.Parameters.Add("@hoten", OleDbType.VarChar, 50).Value = hoten;

            m_Ds.Load(cmd);
            return m_Ds;

        }
    }
}
