using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WinFormsApp1
{
    internal class KaryawanRepository:Karyawan
    {
        private const string conn = "Host=localhost;Port=5432;Username=postgres;Password=informatika;Database=ResponsiJunproAria";
        private static NpgsqlConnection connection;
        private static NpgsqlCommand cmd;
        private static DataTable table;
        private DataGridView dgvData;
        private DataGridViewRow _row;

        public DataGridViewRow Row { set { _row = value; } }

        public KaryawanRepository(DataGridView _dgvData)
        {
            dgvData = _dgvData;
        }

        public void Load()
        {
            connection = new NpgsqlConnection(conn);
            try
            {
                connection.Open();
                dgvData.DataSource = null;
                table = new DataTable();
                string sql = "SELECT * FROM select_emp()";
                cmd = new NpgsqlCommand(sql, connection);

                NpgsqlDataReader r = cmd.ExecuteReader();
                table.Load(r);
                dgvData.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { connection.Close(); }
        }

        public void Add(TextBox tbNama, ComboBox cbDep)
        {
            connection = new NpgsqlConnection(conn);
            try
            {
                connection.Open();
                string sql = @"SELECT * FROM insert_emp(:_nama, :_id_dep)";
                cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("_nama", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", cbDep.Text);

                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data berhasil ditambahkan");
                    tbNama.Text = null;
                    cbDep.Text = null;
                    Load();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { connection.Close(); }
        }

        public void Edit(TextBox tbNama, ComboBox cbDep)
        {
            connection = new NpgsqlConnection(conn);
            if (_row == null)
            {
                MessageBox.Show("Pilih baris data yang akan diubah");
            }

            try
            {
                connection.Open();
                string sql = @"SELECT * FROM update_emp(:_id, :_nama, :_id_dep)";
                cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("_id", _row.Cells["_id_karyawan"].Value.ToString());
                cmd.Parameters.AddWithValue("_nama", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", cbDep.Text);

                if((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data berhasil diedit");
                    tbNama.Text = null;
                    cbDep.Text = null;
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { connection.Close(); }
        }

        public void Delete(TextBox tbNama, ComboBox cbDep)
        {
            connection= new NpgsqlConnection(conn);
            if (_row == null)
            {
                MessageBox.Show("Mohon Pilih baris data yang ingin dihapus");
            }
            try
            {
                connection.Open();
                string sql = @"SELECT * FROM delete_emp(:_id)";
                cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("_id", _row.Cells["_id_karyawan"].Value.ToString());

                if((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data berhasil dihapus");
                    tbNama.Text = null;
                    cbDep.Text = null;
                    Load();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { connection.Close(); }
        }
    }
}
