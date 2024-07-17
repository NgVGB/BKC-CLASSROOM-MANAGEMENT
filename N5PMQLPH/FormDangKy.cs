using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using N5PMQLPH.Models;

namespace N5PMQLPH
{
    public partial class FormDangKy : Form
    {
        public FormDangKy()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        N5QLPHContextDB db = new N5QLPHContextDB();
        private void lbMoFormDN_Click(object sender, EventArgs e)
        {
            FormDangNhap dangNhap = new FormDangNhap();
            dangNhap.Show();
            this.Hide();
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {

        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTK.Text) ||
                string.IsNullOrWhiteSpace(txtHT.Text) ||
                string.IsNullOrWhiteSpace(txtMK.Text))
            {
                MessageBox.Show("Vui lòng điền thông tin vào form.");
                return;
            }

            var user = db.TaiKhoanUDs.FirstOrDefault(u => u.TenDangNhap == txtTK.Text);
            if (user != null)
            {
                MessageBox.Show("Người dùng này đã tồn tại vui lòng đăng kí lại");
                return;
            }

            var maxId = db.TaiKhoanUDs.Max(u => u.ID);
            int nextId = int.Parse(maxId.Substring(1)) + 1;
            string newId = nextId.ToString("D3"); 

            db.TaiKhoanUDs.Add(new TaiKhoanUD
            {
                ID = newId,
                TenDangNhap = txtTK.Text,
                TenNhanVien = txtHT.Text,
                MatKhau = txtMK.Text,
                Quyen = "User"
            });
            db.SaveChanges();

            MessageBox.Show("Đăng kí thành công. Giờ bạn có thể đăng nhập.");
            FormDangNhap frmDN = new FormDangNhap();
            frmDN.Show();
            this.Hide();
        }
    }
}
