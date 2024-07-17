using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using N5PMQLPH.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace N5PMQLPH
{
    public partial class FormTCPH : Form
    {
        public FormTCPH()
        {
            InitializeComponent();
        }

        N5QLPHContextDB db = new N5QLPHContextDB();

        private void FormTCPH_Load(object sender, EventArgs e)
        {
            List<Phong> ph = db.Phongs.ToList();
            fillgrid_TCPH(ph);
            List<Phong> idp = db.Phongs.ToList();
            fillcb_Search(idp);
            List<Phong> addlp = db.Phongs.ToList();
            fillcb_AddLP(addlp);
        }

        private void fillcb_Search(List<Phong> idp)
        {
            this.cbSearch.DataSource = idp;
            this.cbSearch.DisplayMember = "UUIDP";
            this.cbSearch.ValueMember = "UUIDP";
        }

        private void fillcb_AddLP(List<Phong> addlp)
        {
            this.cbIDP.DataSource = addlp;
            this.cbIDP.DisplayMember = "IDPhong";
            this.cbIDP.ValueMember = "IDPhong";
        }

        private void fillgrid_TCPH(List<Phong> ph)
        {
            dgvTCPH.Rows.Clear();
            foreach (var item in ph)
            {
                int index = dgvTCPH.Rows.Add();
                dgvTCPH.Rows[index].Cells[0].Value = item.UUIDP;
                dgvTCPH.Rows[index].Cells[1].Value = item.TenPhong;
                dgvTCPH.Rows[index].Cells[2].Value = item.IDPhong;
                dgvTCPH.Rows[index].Cells[3].Value = item.Khu;
                dgvTCPH.Rows[index].Cells[4].Value = item.Lau;
                dgvTCPH.Rows[index].Cells[5].Value = item.TrangThai;
            }
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Phong> at = db.Phongs.ToList();
            var find = from sp in at
                       where sp.UUIDP == cbSearch.Text
                       select sp;
            fillgrid_TCPH(find.ToList());
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Phong at = new Phong();
                at.UUIDP = txtUUID.Text;
                at.TenPhong = txtTenPhong.Text;
                at.IDPhong = int.Parse(cbIDP.Text);
                at.Khu = txtKhu.Text;
                at.Lau = txtLau.Text;
                at.TrangThai = txtTT.Text;

                db.Phongs.Add(at);
                db.SaveChanges();

                List<Phong> add = db.Phongs.ToList();
                fillgrid_TCPH(add);

                txtUUID.Clear();
                txtTenPhong.Clear();
                txtKhu.Clear();
                txtLau.Clear();
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTCPH.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa!", "Thông Báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (dr == DialogResult.OK)
                {
                    int index = dgvTCPH.SelectedRows[0].Index;
                    string idKey = dgvTCPH.Rows[index].Cells[0].Value.ToString();
                    var deleteKey = db.Phongs.Where(o => o.UUIDP == idKey).FirstOrDefault();
                    if (deleteKey != null)
                    {
                        db.Phongs.Remove(deleteKey);
                        db.SaveChanges();
                    }
                    dgvTCPH.Rows.RemoveAt(index);
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
            if (dgvTCPH.SelectedRows.Count > 0)
            {
                int index = dgvTCPH.SelectedRows[0].Index;
                string idKey = dgvTCPH.Rows[index].Cells[0].Value.ToString();
                var updateKey = db.Phongs.Where(o => o.UUIDP == idKey).FirstOrDefault();
                if (updateKey != null)
                {
                    updateKey.UUIDP = txtUUID.Text;
                    updateKey.TenPhong = txtTenPhong.Text;
                    updateKey.IDPhong = int.Parse(cbIDP.Text);
                    updateKey.Khu = txtKhu.Text;
                    updateKey.Lau = txtLau.Text;
                    updateKey.TrangThai = txtTT.Text;

                    db.Entry(updateKey).State = EntityState.Modified;
                    db.SaveChanges();

                    List<Phong> upKey = db.Phongs.ToList();
                    fillgrid_TCPH(upKey);

                    txtUUID.Clear();
                    txtTenPhong.Clear();
                    txtKhu.Clear();
                    txtLau.Clear();
                    txtTT.Clear();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dữ liệu muốn cập nhật!", "Thông Báo",
                MessageBoxButtons.OK);
            }
        }

        private void dgvTCPH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTCPH.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvTCPH.SelectedRows[0];
                txtUUID.Text = selectedRow.Cells[0].Value.ToString();
                txtTenPhong.Text = selectedRow.Cells[1].Value.ToString();
                cbIDP.Text = selectedRow.Cells[2].Value.ToString();
                txtKhu.Text = selectedRow.Cells[3].Value.ToString();
                txtLau.Text = selectedRow.Cells[4].Value.ToString();
                txtTT.Text = selectedRow.Cells[5].Value.ToString();
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

        private void cbIDP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
