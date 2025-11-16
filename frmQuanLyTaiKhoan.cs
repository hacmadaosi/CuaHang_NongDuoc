using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmQuanLyTaiKhoan : Form
    {
        CuahangNongduoc.Controller.TaiKhoanController ctrl = new CuahangNongduoc.Controller.TaiKhoanController();
        string msg = string.Empty;
        int _id = 0;
        Dictionary<string, string> danhSachTenDangNhap = new Dictionary<string, string>();

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator);

            if(danhSachTenDangNhap.Count == 0)
            {
                DataTable dt = (DataTable)this.bindingNavigator.BindingSource.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    danhSachTenDangNhap.Add(row["TenTaiKhoan"].ToString(), row["ID"].ToString());
                }
            }
            
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _id = dataGridView.Rows.Count > 0 ? Convert.ToInt32(dataGridView.Rows[dataGridView.Rows.Count - 1].Cells["colID"].Value) + 1 : 1;
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.AddNew();
            row["ID"] = _id;
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            ctrl.Save();
            MessageBox.Show("Đã lưu những thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;

            if (MessageBox.Show($"Bạn có chắc chắn xóa nhân viên {row["TenNhanVien"]}", "Xác nhận xóa?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
                toolLuu_Click(sender, e);
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void toolTimTaiKhoan_Enter(object sender, EventArgs e)
        {
            toolTimTaiKhoan.Text = "";
            toolTimTaiKhoan.ForeColor = Color.Black;
        }


        private void toolTimTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ctrl.TimHoTen(toolTimTaiKhoan.Text);
            }
        }

        private void toolTimTaiKhoan_Leave(object sender, EventArgs e)
        {
            toolTimTaiKhoan.Text = "Tìm theo tên";
            toolTimTaiKhoan.ForeColor = Color.FromArgb(224, 224, 224);

        }

        private void toolStripLamMoi_Click(object sender, EventArgs e)
        {
            frmQuanLyTaiKhoan_Load(sender, e);
        }

        private void ErrorAndRefresh(string msg, object sender, EventArgs e)
        {
            MessageBox.Show(msg, "Xảy ra lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            frmQuanLyTaiKhoan_Load(sender, e);
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "colTenTaiKhoan")
            {
                string taiKhoan = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (string.IsNullOrEmpty(taiKhoan))
                {
                    ErrorAndRefresh("Tên đăng nhập không được bỏ trống!", sender, e);
                    return;
                }
                if (danhSachTenDangNhap.ContainsKey(taiKhoan))
                {
                    ErrorAndRefresh("Tên tài khoản đã tồn tại, hãy thử với tên khác!", sender, e);
                    frmQuanLyTaiKhoan_Load(sender, e);
                    return;
                }

            }
            if(dataGridView.Columns[e.ColumnIndex].Name == "colMatKhau")
            {
                string matKhau = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (string.IsNullOrEmpty(matKhau))
                {
                    ErrorAndRefresh("Mật khẩu không được bỏ trống!", sender, e);
                    return;
                }
                if (matKhau.Length < 6)
                {
                    ErrorAndRefresh("Độ dài mật khẩu phải chứa ít nhất 6 kí tự!", sender, e);
                    frmQuanLyTaiKhoan_Load(sender, e);
                    return;
                }
            }
            if(dataGridView.Columns[e.ColumnIndex].Name == "colID")
            {
                ErrorAndRefresh("Đây là trường mặc định không thể thay đổi!", sender, e);
                frmQuanLyTaiKhoan_Load(sender, e);
            }
        }
    }
}
