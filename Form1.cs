using System.Data;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public DataTable table;
        public DataGridViewRow row;

        private KaryawanRepository karyawanRepository;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            karyawanRepository = new KaryawanRepository(dgvData);
            karyawanRepository.Load();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            karyawanRepository.Add(tbNama, cbDepartemen);
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                row = dgvData.Rows[e.RowIndex];
                tbNama.Text = row.Cells["_nama"].Value.ToString();
                cbDepartemen.Text = row.Cells["_id_dep"].Value.ToString();
                karyawanRepository.Row = row;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            karyawanRepository.Edit(tbNama, cbDepartemen);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            karyawanRepository.Delete(tbNama, cbDepartemen);
        }
    }
}