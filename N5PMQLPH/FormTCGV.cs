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
    public partial class FormTCGV : Form
    {
        public FormTCGV()
        {
            InitializeComponent();
        }
        
        N5QLPHContextDB db = new N5QLPHContextDB();

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormTCGV_Load(object sender, EventArgs e)
        {
            List<GiangVien> gv = db.GiangViens.ToList();
            fillgrid_TCGV(gv);
            List<GiangVien> idgv = db.GiangViens.ToList();
            fillcb_Search(idgv);
        }

        private void fillgrid_TCGV(List<GiangVien> gv)
        {
            dgvTCGV.Rows.Clear();
            foreach (var item in gv)
            {
                int index = dgvTCGV.Rows.Add();
                dgvTCGV.Rows[index].Cells[0].Value = item.IDGV;
                dgvTCGV.Rows[index].Cells[1].Value = item.TenGV;
                dgvTCGV.Rows[index].Cells[2].Value = item.Mon;
            }
        }

        private void fillcb_Search(List<GiangVien> idgv)
        {
            this.cbSearch.DataSource = idgv;
            this.cbSearch.DisplayMember = "IDGV";
            this.cbSearch.ValueMember = "IDGV";
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<GiangVien> at = db.GiangViens.ToList();
            var find = from sp in at
                       where sp.IDGV == cbSearch.Text
                       select sp;
            fillgrid_TCGV(find.ToList());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                GiangVien at = new GiangVien();
                at.IDGV = txtIDGV.Text;
                at.TenGV = txtTenGV.Text;
                at.Mon = txtMon.Text;

                db.GiangViens.Add(at);
                db.SaveChanges();

                List<GiangVien> add = db.GiangViens.ToList();
                fillgrid_TCGV(add);

                txtIDGV.Clear();
                txtTenGV.Clear();
                txtMon.Clear();
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
            if (dgvTCGV.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa!", "Thông Báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (dr == DialogResult.OK)
                {
                    int index = dgvTCGV.SelectedRows[0].Index;
                    string idKey = dgvTCGV.Rows[index].Cells[0].Value.ToString();
                    var deleteKey = db.GiangViens.Where(o => o.IDGV == idKey).FirstOrDefault();
                    if (deleteKey != null)
                    {
                        db.GiangViens.Remove(deleteKey);
                        db.SaveChanges();
                    }
                    dgvTCGV.Rows.RemoveAt(index);
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
            if (dgvTCGV.SelectedRows.Count > 0)
            {
                int index = dgvTCGV.SelectedRows[0].Index;
                string idKey = dgvTCGV.Rows[index].Cells[0].Value.ToString();
                var updateKey = db.GiangViens.Where(o => o.IDGV == idKey).FirstOrDefault();
                if (updateKey != null)
                {
                    updateKey.IDGV = txtIDGV.Text;
                    updateKey.TenGV = txtTenGV.Text;
                    updateKey.Mon = txtMon.Text;

                    db.Entry(updateKey).State = EntityState.Modified;
                    db.SaveChanges();

                    List<GiangVien> upKey = db.GiangViens.ToList();
                    fillgrid_TCGV(upKey);

                    txtIDGV.Clear();
                    txtTenGV.Clear();
                    txtMon.Clear();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dữ liệu muốn cập nhật!", "Thông Báo",
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

        private void dgvTCGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTCGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvTCGV.SelectedRows[0];
                txtIDGV.Text = selectedRow.Cells[0].Value.ToString();
                txtTenGV.Text = selectedRow.Cells[1].Value.ToString();
                txtMon.Text = selectedRow.Cells[2].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormPhanCongGV phanCong = new FormPhanCongGV();
            phanCong.Show();
        }
    }
}
