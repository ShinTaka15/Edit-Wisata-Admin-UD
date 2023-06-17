using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitur_Homepage_admin_penginapan
{
    public partial class Edit_detail_wisata : Form
    {
        private string connectionString = "host=localhost;port=5432;database=Wisata Admin;user id=postgres;password=shofiyah774";
        private string Id;

        public Edit_detail_wisata(string Id)
        {
            this.Id = Id;
        }


        public Edit_detail_wisata()
        {
            InitializeComponent();
        }

        private void Edit_detail_wisata_Load(object sender, EventArgs e)
        {

        }
        public void LoadData(string Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"SELECT t1.nama_wisata, t1.deskripsi_wisata, t1.alamat_wisata, t3.nama_fasilitas, t4.harga_tiket" +
                        $"FROM wisata AS t1 JOIN detail_wisata AS t2 ON t1.id_wisata = t2.wisata_id_wisata" +
                        $"JOIN fasilitas_wisata AS t3 ON t2.fasilitas_wisata_fasilitas_wisata_id = t3.fasilitas_wisata_id" +
                        $"JOIN tiket AS t4 ON t1.id_wisata = t4.wisata_id_wisata" +
                        $"WHERE t1.id_wisata = '{Id}'", connection);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    List<string> fasilitas = new List<string>();


                    while (reader.Read())
                    {
                        Judul.Text = reader["nama_wisata"].ToString();
                        Keterangan.Text = reader["deskripsi_wisata"].ToString();
                        Hargatiket.Text = reader["harga_tiket"].ToString();
                        Lokasi.Text = reader["alamat_wisata"].ToString();
                        string dataFasilitas = reader["nama_fasilitas"].ToString();
                        fasilitas.Add(dataFasilitas);
                    }

                    string kumpulanFasilitas = "";
                    foreach (var item in fasilitas)
                    {
                        kumpulanFasilitas += item.ToString();
                        kumpulanFasilitas += ", ";
                    }
                    Fasilitas.Text = kumpulanFasilitas;
                    reader.Close();

                    NpgsqlCommand command1 = new NpgsqlCommand($"SELECT t1.nama_paketmakanan FROM paket_makanan AS t1" +
                        $"JOIN wisata AS t2 ON t1.wisata_id_wisata = t2.id_wisata" +
                        $"WHERE t2.id_wisata = '{Id}'", connection);
                    NpgsqlDataReader reader1 = command1.ExecuteReader();
                    List<string> menupaket = new List<string>();

                    while (reader1.Read()) 
                    {
                        string dataMenupaket = reader1["nama_paketmakanan"].ToString();
                        menupaket.Add(dataMenupaket);
                    }

                    string kumpulanMenupaket = "";
                    foreach (var item in menupaket)
                    {
                        kumpulanMenupaket += item.ToString();
                        kumpulanMenupaket += ", ";
                    }
                    Menupaket.Text = kumpulanMenupaket;
                    reader1.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Kapasitas_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void Fasilitas_TextChanged(object sender, EventArgs e)
        {

        }

        private void Simpan_Click(object sender, EventArgs e)
        {
            UpdateData("");
        }

        private void UpdateData(string Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string newJudul = Judul.Text;
                    string newDeskripsi = Keterangan.Text;
                    string newAlamat = Lokasi.Text;
                    int newHarga = int.Parse(Hargatiket.Text);
                    string newFasilitas = Fasilitas.Text;
                    string newMenupaket = Menupaket.Text;

                    NpgsqlCommand command = new NpgsqlCommand($"UPDATE wisata SET nama_wisata = @Nama, deskripsi_wisata = @Deskripsi, alamat_wisata = @Alamat WHERE id_wisata = '{Id}'", connection);
                    command.Parameters.AddWithValue("@Nama", newJudul);
                    command.Parameters.AddWithValue("@Deskripsi", newDeskripsi);
                    command.Parameters.AddWithValue("@Alamat", newAlamat);
                    command.ExecuteNonQuery();

                    NpgsqlCommand command1 = new NpgsqlCommand($"UPDATE tiket SET harga_tiket = @Harga WHERE wisata_id_wisata = '{Id}'", connection);
                    command1.Parameters.AddWithValue("@Harga", newHarga);
                    command1.ExecuteNonQuery();

                    //NpgsqlCommand command2 = new NpgsqlCommand($"UPDATE fasilitas_wisata SET nama_fasilitas = @Fasilitas WHERE id_wisata = '{Id}'", connection);
                    //command.Parameters.AddWithValue("@Fasilitas", newFasilitas);
                    //command.ExecuteNonQuery();

                    //NpgsqlCommand command3 = new NpgsqlCommand($"UPDATE paket_makanan SET nama_paketmakanan = @Menupaket ", connection);
                    //command.Parameters.AddWithValue("@Menupaket", newMenupaket);
                    //command.ExecuteNonQuery();

                    MessageBox.Show("Data baru tersimpan");
                    connection.Close();
                }

                LoadData("");
            }

            catch (Exception ex)

            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Hapus_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apa anda yakin ingin menghapus?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Data berhasil dihapus", "Konfirmasi");
                DeleteData();
            }
        }

        private void DeleteData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM wisata WHERE id_wisata = 'A01'", connection);
                    NpgsqlCommand command2 = new NpgsqlCommand("DELETE FROM detail_wisata WHERE id_wisata = 'A01'", connection);

                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();

                    connection.Close();
                    Edit_detail_wisata form = new Edit_detail_wisata();
                    form.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}