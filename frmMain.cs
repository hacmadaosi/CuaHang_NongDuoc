using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CuahangNongduoc
{
    public partial class frmMain : Form
    {
        Form currentForm = new frmSanPham();
        public frmMain()
        {
            InitializeComponent();
        }
        frmDonViTinh DonViTinh = null;

        private void mnuDonViTinh_Click(object sender, EventArgs e)
        {
            if (DonViTinh == null || DonViTinh.IsDisposed)
            {
                DonViTinh = new frmDonViTinh();
                DonViTinh.MdiParent = this;
                DonViTinh.Show();
                
            }
            else
                DonViTinh.Activate();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\CoolSoft\\CuahangNongduoc");

            //if (regKey == null)
            //{
            //    DataService.m_ConnectString = "";
            //}
            //else
            //{
            //    try
            //    {
            //        DataService.m_ConnectString = (String)regKey.GetValue("ConnectString");
            //    }
            //    catch
            //    {
            //    }
            //    finally
            //    {
            //        regKey.Close();
            //    }
            //}

            //if (DataService.OpenConnection() == false)
            //{
            //    MessageBox.Show("Không thể kết nối dữ liệu!", "Cua hang Nong duoc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}

            DataService.OpenConnection();

            changeForm(currentForm);
        }

        private void changeForm(Form f)
        {
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Visible = true;
            f.Dock = DockStyle.Fill;

            panel_ShowTable.Controls.Clear();
            panel_ShowTable.Controls.Add(f);
        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            changeForm(new frmSanPham());
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            changeForm(new frmKhachHang());
        }

        private void mnuDaiLy_Click(object sender, EventArgs e)
        {
            changeForm(new frmDaiLy());

        }

        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            changeForm(new frmDanhsachPhieuNhap());
        }

        private void mnuBanHangKH_Click(object sender, EventArgs e)
        {
            changeForm(new frmDanhsachPhieuBanLe());
        }
        private void mnuBanHangDL_Click(object sender, EventArgs e)
        {
            changeForm(new frmDanhsachPhieuBanSi());
        }

        private void mnuThanhCongCu_Click(object sender, EventArgs e)
        {
            mnuThanhCongCu.Checked = !mnuThanhCongCu.Checked;
            toolStrip.Visible = mnuThanhCongCu.Checked;
        }

        private void mnuThanhChucNang_Click(object sender, EventArgs e)
        {
            mnuThanhChucNang.Checked = !mnuThanhChucNang.Checked;
            taskPane.Visible = mnuThanhChucNang.Checked;
        }

        private void mnuThanhtoan_Click(object sender, EventArgs e)
        {
            changeForm(new frmThanhToan());
        }

        private void mnuTonghopDuno_Click(object sender, EventArgs e)
        {
            changeForm(new frmDunoKhachhang());
        }

        private void mnuBaocaoDoanhThu_Click(object sender, EventArgs e)
        {
            changeForm(new frmDoanhThu());
        }

        private void mnuBaocaoSoluongton_Click(object sender, EventArgs e)
        {
            changeForm(new frmSoLuongTon());
        }

        private void mnuSoLuongBan_Click(object sender, EventArgs e)
        {
            changeForm(new frmSoLuongBan());
        }
 
        private void mnuSanphamHethan_Click(object sender, EventArgs e)
        {
            changeForm(new frmSanphamHethan());
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuTuychinhThongtin_Click(object sender, EventArgs e)
        {
            changeForm(new frmThongtinCuahang());
        }

        private void mnuTrogiupLienhe_Click(object sender, EventArgs e)
        {
            changeForm(new frmThongtinLienhe());
        }

        private void mnuNhaCungCap_Click(object sender, EventArgs e)
        {
            changeForm(new frmNhaCungCap());
        }

        private void mnuLyDoChi_Click(object sender, EventArgs e)
        {
            changeForm(new frmLyDoChi());
        }

        private void mnuPhieuChi_Click(object sender, EventArgs e)
        {
            changeForm(new frmPhieuChi());
        }

        private void mnuTrogiupHuongdan_Click(object sender, EventArgs e)
        {
           // Help.ShowHelp(this, "CPP.CHM");
        }
    }
}