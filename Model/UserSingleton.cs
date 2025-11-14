using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;

namespace CuahangNongduoc.Model
{
    internal class UserSingleton
    {
        private static readonly UserSingleton instance = new UserSingleton();

        public int ID { get; private set; }

        public int LoaiTaiKhoan { get; private set; }

        public string HoTen { get; private set; }

        public static UserSingleton Instance
        {
            get
            {
                return instance;
            }
        }
        private UserSingleton()
        {
            this.ID = -1;
            this.HoTen = null;
            this.LoaiTaiKhoan = -1;
        }

        public void SetUser(int id, string hoTen, int loaiTaiKhoan)
        {
            this.ID = id;
            this.LoaiTaiKhoan = loaiTaiKhoan;
            this.HoTen = hoTen;
        }

        public void DangXuat()
        {
            this.ID = -1;
            this.LoaiTaiKhoan = -1;
            this.HoTen = null;
        }
    }

}
