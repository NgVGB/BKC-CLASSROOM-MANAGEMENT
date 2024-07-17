using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using N5PMQLPH.Models;

namespace N5PMQLPH
{
    public partial class FormQLTK : Form
    {
        public FormQLTK()
        {
            InitializeComponent();
        }

        N5QLPHContextDB db = new N5QLPHContextDB();

        private void FormQLTK_Load(object sender, EventArgs e)
        {
            List<TaiKhoanUD> tk = db.TaiKhoanUDs.ToList();
            fillgrid_PQ(tk);
        }

        private void fillgrid_PQ(List<TaiKhoanUD> PQtable)
        {
            dgvPQ.Rows.Clear();
            foreach (var item in PQtable)
            {
                int index = dgvPQ.Rows.Add();
                dgvPQ.Rows[index].Cells[0].Value = item.ID;
                dgvPQ.Rows[index].Cells[1].Value = item.TenDangNhap;
                dgvPQ.Rows[index].Cells[2].Value = item.TenNhanVien;
                dgvPQ.Rows[index].Cells[3].Value = item.MatKhau;
                dgvPQ.Rows[index].Cells[4].Value = item.Quyen;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                TaiKhoanUD tk = new TaiKhoanUD();
                tk.ID = txtID.Text;
                tk.TenDangNhap = txtTK.Text;
                tk.TenNhanVien = txtNV.Text;
                tk.MatKhau = txtMK.Text;
                tk.Quyen = txtQuyen.Text;

                db.TaiKhoanUDs.Add(tk);
                db.SaveChanges();

                List<TaiKhoanUD> addtk = db.TaiKhoanUDs.ToList();
                fillgrid_PQ(addtk);

                txtID.Clear();
                txtTK.Clear();
                txtNV.Clear();
                txtMK.Clear();
                txtQuyen.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu nhập không đúng định dạng.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvPQ.SelectedRows.Count > 0)
            {
                int index = dgvPQ.SelectedRows[0].Index;
                string idTK = dgvPQ.Rows[index].Cells[0].Value.ToString();
                var updateTK = db.TaiKhoanUDs.Where(o => o.ID == idTK).FirstOrDefault();
                if (updateTK != null)
                {
                    updateTK.ID = txtID.Text;
                    updateTK.TenDangNhap = txtTK.Text;
                    updateTK.TenNhanVien = txtNV.Text;
                    updateTK.MatKhau = txtMK.Text;
                    updateTK.Quyen = txtQuyen.Text;

                    db.Entry(updateTK).State = EntityState.Modified;
                    db.SaveChanges();

                    List<TaiKhoanUD> upTK = db.TaiKhoanUDs.ToList();
                    fillgrid_PQ(upTK);

                    txtTK.Clear();
                    txtNV.Clear();
                    txtMK.Clear();
                    txtQuyen.Clear();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dữ liệu muốn cập nhật!", "Thông Báo",
                MessageBoxButtons.OK);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPQ.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa!", "Thông Báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (dr == DialogResult.OK)
                {
                    int index = dgvPQ.SelectedRows[0].Index;
                    string idTK = dgvPQ.Rows[index].Cells[0].Value.ToString();
                    var deleteTK = db.TaiKhoanUDs.Where(o => o.ID == idTK).FirstOrDefault();
                    if (deleteTK != null)
                    {
                        db.TaiKhoanUDs.Remove(deleteTK);
                        db.SaveChanges();
                    }
                    dgvPQ.Rows.RemoveAt(index);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dữ liệu muốn xóa!", "Thông Báo",
                MessageBoxButtons.OK);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông Báo",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (dr == DialogResult.OK)
            {
                Close();
            }
        }

        private void dgvPQ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPQ.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvPQ.SelectedRows[0];
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtTK.Text = selectedRow.Cells[1].Value.ToString();
                txtNV.Text = selectedRow.Cells[2].Value.ToString();
                txtMK.Text = selectedRow.Cells[3].Value.ToString();
                txtQuyen.Text = selectedRow.Cells[4].Value.ToString();
            }
        }
    }
}
