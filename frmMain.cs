using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmMain : Form
    {
        private string userName;
        private frmDangNhap fDangNhap;
        private string idNhanVien;
        private TaiKhoan taiKhoan;
        private PhanQuyenHelper phanQuyen;

        public frmMain(TaiKhoan tk, frmDangNhap fDangNhap)
        {
            InitializeComponent();
            this.taiKhoan = tk;
            this.idNhanVien = tk.ID;
            this.userName = tk.TenNhanVien;
            this.phanQuyen = new PhanQuyenHelper(tk);
            tenToolStripMenuItem.Text = "Người dùng: " + userName + " (" + tk.VaiTro + ")";
            this.fDangNhap = fDangNhap;
            
            // Áp dụng phân quyền khi form được load
            ApDungPhanQuyen();
        }
        frmDonViTinh DonViTinh = null;

        private void mnuDonViTinh_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
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
            DataService.OpenConnection();
        }

        /// <summary>
        /// Áp dụng phân quyền cho các menu và chức năng
        /// Quản lý: có đầy đủ quyền
        /// Nhân viên: chỉ có quyền bán hàng, xem báo cáo cơ bản
        /// </summary>
        private void ApDungPhanQuyen()
        {
            bool laQuanLy = phanQuyen.LaQuanLy();
            bool laNhanVien = phanQuyen.LaNhanVien();

            // Menu Quản lý - Chỉ Quản lý mới có quyền
            mnuQuanLy.Visible = laQuanLy;
            mnuLyDoChi.Visible = laQuanLy;
            mnuDonViTinh.Visible = laQuanLy;
            mnuSanPham.Visible = laQuanLy;
            mnuKhachHang.Visible = laQuanLy;
            mnuDaiLy.Visible = laQuanLy;
            mnuNhaCungCap.Visible = laQuanLy;
            mnuQuanLyTaiKhoan.Visible = laQuanLy;

            // Toolbar Quản lý
            toolSanPham.Visible = laQuanLy;
            toolNhaCungCap.Visible = laQuanLy;
            toolKhachHang.Visible = laQuanLy;
            toolDaiLy.Visible = laQuanLy;
            toolNhanVien.Visible = laQuanLy;

            // TaskPane Quản lý
            itemSanPham.Visible = laQuanLy;
            itemNhaCungCap.Visible = laQuanLy;
            itemKhachHang.Visible = laQuanLy;
            itemDaiLy.Visible = laQuanLy;

            // Menu Nghiệp vụ
            // Nhập hàng - Chỉ Quản lý
            mnuNhapHang.Visible = laQuanLy;
            toolNhapHang.Visible = laQuanLy;
            itemNhapHang.Visible = laQuanLy;

            // Bán hàng - Cả Quản lý và Nhân viên đều có quyền
            mnuBanHang.Visible = true;
            mnuBanHangKH.Visible = true;
            mnuBanHangDL.Visible = true;
            toolBanLe.Visible = true;
            toolBanSi.Visible = true;
            itemBanLe.Visible = true;
            itemBanSi.Visible = true;

            // Phiếu chi - Chỉ Quản lý
            mnuPhieuChi.Visible = laQuanLy;
            toolPhieuChi.Visible = laQuanLy;
            itemPhieuChi.Visible = laQuanLy;

            // Phiếu thu (Thanh toán) - Cả Quản lý và Nhân viên
            mnuThanhtoan.Visible = true;
            toolThanhtoan.Visible = true;
            itemThanhToan.Visible = true;

            // Tổng hợp dư nợ - Chỉ Quản lý
            mnuTonghopDuno.Visible = laQuanLy;

            // Menu Báo cáo
            // Số lượng tồn - Cả Quản lý và Nhân viên
            mnuBaocaoSoluongton.Visible = true;
            toolTonKho.Visible = true;
            itemTonKho.Visible = true;

            // Số lượng bán - Cả Quản lý và Nhân viên
            mnuSoLuongBan.Visible = true;
            itemTonghopDoanhthu.Visible = true;

            // Thống kê - Chỉ Quản lý
            mnuThongKe.Visible = laQuanLy;

            // Sản phẩm hết hạn - Cả Quản lý và Nhân viên
            mnuSanphamHethan.Visible = true;
            taskItem1.Visible = true;

            // Menu Tùy chỉnh - Chỉ Quản lý
            mnuTuychinh.Visible = laQuanLy;
            mnuTuychinhThongtin.Visible = laQuanLy;

            // Ẩn các separator nếu không cần thiết
            if (!laQuanLy)
            {
                toolStripSeparator2.Visible = false;
            }
        }
        frmSanPham SanPham = null;
        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (SanPham == null || SanPham.IsDisposed)
            {
                SanPham = new frmSanPham();
                SanPham.MdiParent = this;
                SanPham.Show();
            }
            else
                SanPham.Activate();
        }
        frmKhachHang KhachHang = null;
        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (KhachHang == null || KhachHang.IsDisposed)
            {
                KhachHang = new frmKhachHang();
                KhachHang.MdiParent = this;
                KhachHang.Show();
            }
            else
                KhachHang.Activate();
        }
        frmDaiLy DaiLy = null;
        private void mnuDaiLy_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (DaiLy == null || DaiLy.IsDisposed)
            {
                DaiLy = new frmDaiLy();
                DaiLy.MdiParent = this;
                DaiLy.Show();
            }
            else
                DaiLy.Activate();

        }
        frmDanhsachPhieuNhap NhapHang = null;
        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (NhapHang == null || NhapHang.IsDisposed)
            {
                NhapHang = new frmDanhsachPhieuNhap();
                NhapHang.MdiParent = this;
                NhapHang.Show();
            }
            else
                NhapHang.Activate();
        }
        frmDanhsachPhieuBanLe BanLe = null;
        private void mnuBanHangKH_Click(object sender, EventArgs e)
        {
            if (BanLe == null || BanLe.IsDisposed)
            {
                BanLe = new frmDanhsachPhieuBanLe(userName);
                BanLe.MdiParent = this;
                BanLe.Show();
            }
            else
                BanLe.Activate();
        }
        frmDanhsachPhieuBanSi BanSi = null;
        private void mnuBanHangDL_Click(object sender, EventArgs e)
        {
            if (BanSi == null || BanSi.IsDisposed)
            {
                BanSi = new frmDanhsachPhieuBanSi(userName);
                BanSi.MdiParent = this;
                BanSi.Show();
            }
            else
                BanSi.Activate();
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
        frmThanhToan ThanhToan = null;
        private void mnuThanhtoan_Click(object sender, EventArgs e)
        {
            if (ThanhToan == null || ThanhToan.IsDisposed)
            {
                ThanhToan = new frmThanhToan();
                ThanhToan.MdiParent = this;
                ThanhToan.Show();
            }
            else
                ThanhToan.Activate();
        }
        frmDunoKhachhang DunoKhachhang = null;
        private void mnuTonghopDuno_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (DunoKhachhang == null || DunoKhachhang.IsDisposed)
            {
                DunoKhachhang = new frmDunoKhachhang();
                DunoKhachhang.MdiParent = this;
                DunoKhachhang.Show();
            }
            else
                DunoKhachhang.Activate();
        }
        frmDoanhThu DoanhThu = null;
        private void mnuBaocaoDoanhThu_Click(object sender, EventArgs e)
        {
            if (DoanhThu == null || DoanhThu.IsDisposed)
            {
                DoanhThu = new frmDoanhThu();
                DoanhThu.MdiParent = this;
                DoanhThu.Show();
            }
            else
                DoanhThu.Activate();

        }

        frmSoLuongTon SoLuongTon = null;
        private void mnuBaocaoSoluongton_Click(object sender, EventArgs e)
        {

            if (SoLuongTon == null || SoLuongTon.IsDisposed)
            {
                SoLuongTon = new frmSoLuongTon();
                SoLuongTon.MdiParent = this;
                SoLuongTon.Show();
            }
            else
                SoLuongTon.Activate();

        }
        frmSoLuongBan SoLuongBan = null;
        private void mnuSoLuongBan_Click(object sender, EventArgs e)
        {
            if (SoLuongBan == null || SoLuongBan.IsDisposed)
            {
                SoLuongBan = new frmSoLuongBan();
                SoLuongBan.MdiParent = this;
                SoLuongBan.Show();
            }
            else
                SoLuongBan.Activate();
        }
        frmSanphamHethan SanphamHethan = null;
        private void mnuSanphamHethan_Click(object sender, EventArgs e)
        {
            if (SanphamHethan == null || SanphamHethan.IsDisposed)
            {
                SanphamHethan = new frmSanphamHethan();
                SanphamHethan.MdiParent = this;
                SanphamHethan.Show();
            }
            else
                SanphamHethan.Activate();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        frmThongtinCuahang ThongtinCuahang = null;
        private void mnuTuychinhThongtin_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (ThongtinCuahang == null || ThongtinCuahang.IsDisposed)
            {
                ThongtinCuahang = new frmThongtinCuahang();
                ThongtinCuahang.MdiParent = this;
                ThongtinCuahang.Show();
            }
            else
                ThongtinCuahang.Activate();
        }
        frmThongtinLienhe ThongtinLienhe = null;
        private void mnuTrogiupLienhe_Click(object sender, EventArgs e)
        {
            if (ThongtinLienhe == null || ThongtinLienhe.IsDisposed)
            {
                ThongtinLienhe = new frmThongtinLienhe();
                ThongtinLienhe.MdiParent = this;
                ThongtinLienhe.Show();
            }
            else
                ThongtinLienhe.Activate();
        }

        frmNhaCungCap NhaCungCap = null;
        private void mnuNhaCungCap_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (NhaCungCap == null || NhaCungCap.IsDisposed)
            {
                NhaCungCap = new frmNhaCungCap();
                NhaCungCap.MdiParent = this;
                NhaCungCap.Show();
            }
            else
                NhaCungCap.Activate();
        }
        frmLyDoChi LyDoChi = null;
        private void mnuLyDoChi_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (LyDoChi == null || LyDoChi.IsDisposed)
            {
                LyDoChi = new frmLyDoChi();
                LyDoChi.MdiParent = this;
                LyDoChi.Show();
            }
            else
                LyDoChi.Activate();
        }

        frmQuanLyTaiKhoan QuanLyTaiKhoan = null;
        private void mnuQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (QuanLyTaiKhoan == null || QuanLyTaiKhoan.IsDisposed)
            {
                // Truyền thông tin tài khoản hiện tại để không cho xóa
                QuanLyTaiKhoan = new frmQuanLyTaiKhoan();
                QuanLyTaiKhoan.MdiParent = this;
                QuanLyTaiKhoan.Show();
            }
            else
                QuanLyTaiKhoan.Activate();
        }

        frmPhieuChi PhieuChi = null;
        private void mnuPhieuChi_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (PhieuChi == null || PhieuChi.IsDisposed)
            {
                PhieuChi = new frmPhieuChi();
                PhieuChi.MdiParent = this;
                PhieuChi.Show();
            }
            else
                PhieuChi.Activate();
        }

        private void mnuTrogiupHuongdan_Click(object sender, EventArgs e)
        {
           // Help.ShowHelp(this, "CPP.CHM");
        }

        frmThongKe ThongKe = null;

        private void mnuThongKe_Click(object sender, EventArgs e)
        {
            if (!phanQuyen.LaQuanLy())
            {
                phanQuyen.HienThiThongBaoKhongQuyen();
                return;
            }
            if (ThongKe == null || ThongKe.IsDisposed)
            {
                ThongKe = new frmThongKe(idNhanVien);
                ThongKe.MdiParent = this;
                ThongKe.Show();
            }
            else
                ThongKe.Activate(); }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            
            fDangNhap.Show();
            //fDangNhap.txtMatKhau.Clear();
            //fDangNhap.txtMatKhau.Clear();

        }

        frmQuanLyTaiKhoanCaNhan QuanLyTaiKhoanCaNhan = null;
        private void mnuQuanLyTaiKhoanCaNhan_Click(object sender, EventArgs e)
        {
            if (QuanLyTaiKhoanCaNhan == null || QuanLyTaiKhoanCaNhan.IsDisposed)
            {
                QuanLyTaiKhoanCaNhan = new frmQuanLyTaiKhoanCaNhan(taiKhoan);
                QuanLyTaiKhoanCaNhan.MdiParent = this;
                QuanLyTaiKhoanCaNhan.Show();
            }
            else
                QuanLyTaiKhoanCaNhan.Activate();
        }

        frmQuanLyTaiKhoan NhanVien = null;
        private void toolNhanVien_Click(object sender, EventArgs e)
        {
            if (NhanVien == null || NhanVien.IsDisposed)
            {
                NhanVien = new frmQuanLyTaiKhoan();
                NhanVien.MdiParent = this;
                NhanVien.Show();
            }
            else
                NhanVien.Activate();
        }
    }
}