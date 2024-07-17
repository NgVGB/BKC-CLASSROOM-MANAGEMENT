using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using N5PMQLPH.Models;

namespace N5PMQLPH
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        N5QLPHContextDB db = new N5QLPHContextDB();

        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void lbMoFormDK_Click(object sender, EventArgs e)
        {
            FormDangKy dangKi = new FormDangKy();
            dangKi.Show();
            this.Hide();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=WIN-IVULELHP93E\SQLEXPRESS;Initial Catalog=NEWN5QLPH;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter("select * from TaiKhoanUD where TenDangNhap = N'" + txtTK.Text + "' and MatKhau = N'" + txtMK.Text + "'", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Detect SQL Injection
            string input = txtTK.Text;
            string input1 = txtMK.Text;
            if (Regex.IsMatch(input, @"[\s;'\-]+|or|and|select|insert|update|delete|drop|create|alter|truncate|grant|revoke|exec|execute", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Tính phá hả? Nghĩ sao vậy ní");
            }
            else if (Regex.IsMatch(input1, @"[\s;'\-]+|or|and|select|insert|update|delete|drop|create|alter|truncate|grant|revoke|exec|execute", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Tính phá hả? Nghĩ sao vậy ní");
            } 
            else if (dt.Rows.Count > 0)
            {
                string tenDangNhap = txtTK.Text;
                FormMain frMain = new FormMain(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString());
                frMain.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bạn đã nhập sai mật khẩu hoặc chưa điền thông tin đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
