using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using N5PMQLPH.Models;

namespace N5PMQLPH
{
    public partial class FormMain : Form
    {
        string ID = "", TenDangNhap = "", TenNhanVien = "", MatKhau = "", Quyen = "";
        string ketnoi = (@"Data Source=WIN-IVULELHP93E\SQLEXPRESS;Initial Catalog=N5QLPH;Integrated Security=True");
        string SQL;
        SqlConnection cn;
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;

        private string _tenDangNhap;
        private string _quyen;

        N5QLPHContextDB db = new N5QLPHContextDB();
        public FormMain(string MaID, string TenDangNhap, string TenNhanVien, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ID = MaID;
            this.TenDangNhap = TenDangNhap;
            this.TenNhanVien = TenNhanVien;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            _tenDangNhap = TenNhanVien;
            _quyen = Quyen;
        }

        private Form currentFormChild;

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelBody.Controls.Add(childForm);
            panelBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void panelL_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTCGV_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTCGV());
            labelNAME.Text = "TRA CỨU GIẢNG VIÊN";
        }

        private void btnTCPH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTCPH());
            labelNAME.Text = "TRA CỨU PHÒNG HỌC";
        }

        private void btnKTRATBI_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLTB());
            labelNAME.Text = "KIỂM TRA THIẾT BỊ";
        }

        private void btnPCGV_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormPhanCongGV());
            labelNAME.Text = "PHÂN CÔNG GIẢNG VIÊN";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn đăng xuất!", "Thông Báo",
              MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (dr == DialogResult.OK)
            {
                FormDangNhap dangNhap = new FormDangNhap();
                dangNhap.Show();
                this.Close();
            }
        }

        private void btnQLTK_Click(object sender, EventArgs e)
        {
            if (Quyen == "Admin")
            {
                OpenChildForm(new FormQLTK());
                labelNAME.Text = "QUẢN LÍ TÀI KHOẢN";
            }
            else
            {
                MessageBox.Show("Bạn không có quyền xem mục này!", "Thông Báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
        }

        private void panelTopR_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void labelTENUSER_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            labelTENUSER.Text = _tenDangNhap;
            labelPERM.Text = _quyen;
        }
    }
}
