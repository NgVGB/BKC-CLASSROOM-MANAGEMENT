using N5PMQLPH.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N5PMQLPH
{
    public partial class FormPhanCongGV : Form
    {
        public FormPhanCongGV()
        {
            InitializeComponent();
        }

        N5QLPHContextDB db = new N5QLPHContextDB();

        private void FormPhanCongGV_Load(object sender, EventArgs e)
        {
            List<PhanCong> pcgv = db.PhanCongs.ToList();
            fillgrid_PCGV(pcgv);
            List<PhanCong> idpcgv = db.PhanCongs.ToList();
            fillcb_Search(idpcgv);
            List<PhanCong> idgv = db.PhanCongs.ToList();
            fillcb_IDGV(idgv);
            List<PhanCong> uuidgv = db.PhanCongs.ToList();
            fillcb_UUID(uuidgv);
        }

        private void fillgrid_PCGV(List<PhanCong> pcgv)
        {
            dgvPCGV.Rows.Clear();
            foreach (var item in pcgv)
            {
                int index = dgvPCGV.Rows.Add();
                dgvPCGV.Rows[index].Cells[0].Value = item.IDPC;
                dgvPCGV.Rows[index].Cells[1].Value = item.IDGV;
                dgvPCGV.Rows[index].Cells[2].Value = item.UUIDP;
                dgvPCGV.Rows[index].Cells[3].Value = item.NgayBatDau;
                dgvPCGV.Rows[index].Cells[4].Value = item.NgayKetThuc;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                PhanCong at = new PhanCong();
                at.IDPC = int.Parse(txtIDPC.Text);
                at.IDGV = cbIDGV.Text;
                at.UUIDP = cbUUID.Text;
                at.NgayBatDau = DateTime.Parse(dtpStart.Text);
                at.NgayKetThuc = DateTime.Parse(dtpEnd.Text);

                db.PhanCongs.Add(at);
                db.SaveChanges();

                List<PhanCong> add = db.PhanCongs.ToList();
                fillgrid_PCGV(add);

                txtIDPC.Clear();
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

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PhanCong> at = db.PhanCongs.ToList();
            var find = from sp in at
                       where sp.IDPC == int.Parse(cbSearch.Text)
                       select sp;
            fillgrid_PCGV(find.ToList());
        }
        private void fillcb_Search(List<PhanCong> idpcgv)
        {
            this.cbSearch.DataSource = idpcgv;
            this.cbSearch.DisplayMember = "IDPC";
            this.cbSearch.ValueMember = "IDPC";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void fillcb_IDGV(List<PhanCong> idgv)
        {
            this.cbIDGV.DataSource = idgv;
            this.cbIDGV.DisplayMember = "IDGV";
            this.cbIDGV.ValueMember = "IDGV";
        }

        private void fillcb_UUID(List<PhanCong> uuidgv)
        {
            this.cbUUID.DataSource = uuidgv;
            this.cbUUID.DisplayMember = "UUIDP";
            this.cbUUID.ValueMember = "UUIDP";
        }

        private void cbIDGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PhanCong> at = db.PhanCongs.ToList();
            var find = from sp in at
                       where sp.IDGV == cbIDGV.Text
                       select sp;
        }

        private void cbUUID_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PhanCong> at = db.PhanCongs.ToList();
            var find = from sp in at
                       where sp.UUIDP == cbUUID.Text
                       select sp;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPCGV.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa!", "Thông Báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (dr == DialogResult.OK)
                {
                    int index = dgvPCGV.SelectedRows[0].Index;
                    string idKey = dgvPCGV.Rows[index].Cells[0].Value.ToString();
                    var deleteKey = db.PhanCongs.Where(o => o.IDGV == idKey).FirstOrDefault();
                    if (deleteKey != null)
                    {
                        db.PhanCongs.Remove(deleteKey);
                        db.SaveChanges();
                    }
                    dgvPCGV.Rows.RemoveAt(index);
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

        private void dgvPCGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPCGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvPCGV.SelectedRows[0];
                txtIDPC.Text = selectedRow.Cells[0].Value.ToString();
                cbIDGV.Text = selectedRow.Cells[1].Value.ToString();
                cbUUID.Text = selectedRow.Cells[2].Value.ToString();
                dtpStart.Text = selectedRow.Cells[3].Value.ToString();
                dtpEnd.Text = selectedRow.Cells[4].Value.ToString();
            }
        }
    }
}
