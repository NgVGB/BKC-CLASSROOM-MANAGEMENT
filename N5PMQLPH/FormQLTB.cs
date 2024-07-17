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
    public partial class FormQLTB : Form
    {
        public FormQLTB()
        {
            InitializeComponent();
        }
        
        N5QLPHContextDB db = new N5QLPHContextDB();

        private void FormQLTB_Load(object sender, EventArgs e)
        {
            List<ThietBi> tb = db.ThietBis.ToList();
            fillgrid_QLTB(tb);
            List<ThietBi> search = db.ThietBis.ToList();
            fillcb_Search(search);
            List<ThietBi> add = db.ThietBis.ToList();
            fillcb_Add(add);
        }

        private void fillcb_Search(List<ThietBi> search)
        {
            this.cbSearch.DataSource = search;
            this.cbSearch.DisplayMember = "ID";
            this.cbSearch.ValueMember = "ID";
        }

        private void fillcb_Add(List<ThietBi> add)
        {
            this.cbAdd.DataSource = add;
            this.cbAdd.DisplayMember = "UUIDP";
            this.cbAdd.ValueMember = "UUIDP";
        }

        private void fillgrid_QLTB(List<ThietBi> tb)
        {
            dgvQLTB.Rows.Clear();
            foreach (var item in tb)
            {
                int index = dgvQLTB.Rows.Add();
                dgvQLTB.Rows[index].Cells[0].Value = item.ID;
                dgvQLTB.Rows[index].Cells[1].Value = item.UUIDP;
                dgvQLTB.Rows[index].Cells[2].Value = item.TenThietBi;
                dgvQLTB.Rows[index].Cells[3].Value = item.LoaiThietBi;
                dgvQLTB.Rows[index].Cells[4].Value = item.TrangThai;
            }
        }

        private void dgvQLTB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvQLTB.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvQLTB.SelectedRows[0];
                txtIDTB.Text = selectedRow.Cells[0].Value.ToString();
                cbAdd.Text = selectedRow.Cells[1].Value.ToString();
                txtTenTB.Text = selectedRow.Cells[2].Value.ToString();
                txtLoaiTB.Text = selectedRow.Cells[3].Value.ToString();
                txtTT.Text = selectedRow.Cells[4].Value.ToString();
            }
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ThietBi> at = db.ThietBis.ToList();
            var find = from sp in at
                       where sp.ID == int.Parse(cbSearch.Text)
                       select sp;
            fillgrid_QLTB(find.ToList());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                ThietBi at = new ThietBi();
                at.ID = int.Parse(txtIDTB.Text);
                at.UUIDP = cbAdd.Text;
                at.TenThietBi = txtTenTB.Text;
                at.LoaiThietBi = txtLoaiTB.Text;
                at.TrangThai = txtTT.Text;

                db.ThietBis.Add(at);
                db.SaveChanges();

                List<ThietBi> add = db.ThietBis.ToList();
                fillgrid_QLTB(add);

                txtIDTB.Clear();
                txtTenTB.Clear();
                txtLoaiTB.Clear();
                txtTT.Clear();
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvQLTB.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa!", "Thông Báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (dr == DialogResult.OK)
                {
                    int index = dgvQLTB.SelectedRows[0].Index;
                    string idKey = dgvQLTB.Rows[index].Cells[0].Value.ToString();
                    int key;
                    if (int.TryParse(idKey, out key))
                    {
                        var deleteKey = db.ThietBis.Where(o => o.ID == key).FirstOrDefault();
                        if (deleteKey != null)
                        {
                            db.ThietBis.Remove(deleteKey);
                            db.SaveChanges();
                        }
                        dgvQLTB.Rows.RemoveAt(index);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dữ liệu muốn xóa!", "Thông Báo",
                MessageBoxButtons.OK);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvQLTB.SelectedRows.Count > 0)
            {
                int index = dgvQLTB.SelectedRows[0].Index;
                string idKey = dgvQLTB.Rows[index].Cells[0].Value.ToString();
                int key;
                if (int.TryParse(idKey, out key))
                {
                    var updateKey = db.ThietBis.Where(o => o.ID == key).FirstOrDefault();
                    if (updateKey != null)
                    {
                        updateKey.ID = int.Parse(txtIDTB.Text);
                        updateKey.UUIDP = cbAdd.Text;
                        updateKey.TenThietBi = txtTenTB.Text;
                        updateKey.LoaiThietBi = txtLoaiTB.Text;
                        updateKey.TrangThai = txtTT.Text;

                        db.Entry(updateKey).State = EntityState.Modified;
                        db.SaveChanges();

                        List<ThietBi> upKey = db.ThietBis.ToList();
                        fillgrid_QLTB(upKey);

                        txtIDTB.Clear();
                        txtTenTB.Clear();
                        txtLoaiTB.Clear();
                        txtTT.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dữ liệu muốn cập nhật!", "Thông Báo",
                    MessageBoxButtons.OK);
                }
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
    }
}
