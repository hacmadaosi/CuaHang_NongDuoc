using CuahangNongduoc.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmNhanVien : Form
    {
        NhanVienController ctrl = new NhanVienController();

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            hienThiDsLoaiTaiKhoan();
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator);
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
    }
}
