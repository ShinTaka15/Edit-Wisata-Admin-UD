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
        //private string imagelocation;

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

                    NpgsqlCommand command = new NpgsqlCommand($"SELECT t1.nama_wisata, t1.deskripsi_wisata, t1.alamat_wisata, t3.nama_fasilitas, t4.harga_tiket \r\nFROM wisata AS t1 JOIN detail_wisata AS t2 ON t1.id_wisata = t2.wisata_id_wisata\r\nJOIN fasilitas_wisata AS t3 ON t2.fasilitas_wisata_fasilitas_wisata_id = t3.fasilitas_wisata_id\r\nJOIN tiket AS t4 ON t1.id_wisata = t4.wisata_id_wisata\r\nWHERE t1.id_wisata = '{Id}'", connection);
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
                        if (item != fasilitas.Last())
                        {
                            kumpulanFasilitas += ", ";
                        }
                    }
                    Fasilitas.Text = kumpulanFasilitas;
                    reader.Close();

                    NpgsqlCommand command1 = new NpgsqlCommand($"SELECT t1.nama_paketmakanan FROM paket_makanan AS t1\r\nJOIN wisata AS t2 ON t1.wisata_id_wisata = t2.id_wisata\r\nWHERE t2.id_wisata = '{Id}'", connection);
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
                        if (item != menupaket.Last())
                        {
                            kumpulanMenupaket += ", ";
                        }
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
            UpdateData($"{Id}");
        }

        private void UpdateData(string Id)
        {
            try
            {
                //byte[] images = null;


                //FileStream stream = new FileStream(imagelocation, FileMode.Open, FileAccess.Read);
                //BinaryReader brs = new BinaryReader(stream);
                //images = brs.ReadBytes((int)stream.Length);

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string newFasilitas = Fasilitas.Text;
                    string newMenupaket = Menupaket.Text;

                    // UPDATE database ke Tabel Wisata
                    NpgsqlCommand command = new NpgsqlCommand($"UPDATE wisata SET nama_wisata = @Nama, deskripsi_wisata = @Deskripsi, alamat_wisata = @Alamat WHERE id_wisata = '{Id}'", connection);
                    command.Parameters.AddWithValue("@Nama", Judul.Text);
                    command.Parameters.AddWithValue("@Deskripsi", Keterangan.Text);
                    command.Parameters.AddWithValue("@Alamat", Lokasi.Text);
                    command.ExecuteNonQuery();

                    // UPDATE database ke Tabel Tiket
                    NpgsqlCommand command1 = new NpgsqlCommand($"UPDATE tiket SET harga_tiket = @Harga WHERE wisata_id_wisata = '{Id}'", connection);
                    string newHarga = Hargatiket.Text;
                    if (Int64.TryParse(newHarga, out long newHargaBigInt))
                    {
                        command1.Parameters.AddWithValue("@Harga", newHargaBigInt);
                    }
                    else
                    {
                        command1.Parameters.AddWithValue("@Harga", DBNull.Value);
                    }
                    command1.ExecuteNonQuery();

                    // UPDATE database ke Tabel Detail Wisata
                    //List<string> fasilitas = new List<string>();
                    //fasilitas.Add(Fasilitas.Text);
                    //List<int> id_fasilitas = new List<int>();

                    //foreach (var nama_fasilitas in fasilitas)
                    //{
                    //    NpgsqlCommand command2 = new NpgsqlCommand($"SELECT fasilitas_wisata.fasilitas_wisata_id FROM fasilitas_wisata WHERE fasilitas_wisata.nama_fasilitas ILIKE '@NamaFasilitas'", connection);
                    //    command2.Parameters.AddWithValue("@NamaFasilitas", nama_fasilitas);
                    //    NpgsqlDataReader reader = command2.ExecuteReader();
                    //    while (reader.Read())
                    //    {
                    //        int id = reader.GetInt32("id");
                    //        id_fasilitas.Add(id);
                    //    }
                    //}

                    //NpgsqlCommand command3 = new NpgsqlCommand($"DELETE FROM detail_wisata WHERE id_wisata = '{Id}'", connection);
                    //command3.ExecuteNonQuery();

                    //foreach (var newId in id_fasilitas)
                    //{
                    //    NpgsqlCommand command4 = new NpgsqlCommand($"INSERT INTO detail_wisata (wisata_id_wisata, fasilitas_wisata_fasilitas_wisata_id) VALUES('{Id}', @id_fasilitas)", connection);
                    //    command4.Parameters.AddWithValue("@id_fasilitas", newId);
                    //    command4.ExecuteNonQuery();
                    //}

                    // UPDATE database ke tabel Paket Makanan


                    MessageBox.Show("Data baru tersimpan");
                    connection.Close();
                }

                LoadData($"A01");
            }

            catch (Exception ex)

            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Hapus_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apa anda yakin ingin menghapus?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Data berhasil dihapus", "Konfirmasi");
                DeleteData($"{Id}");
            }
        }

        private void DeleteData(string Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM wisata WHERE id_wisata = '{Id}'", connection);
                    NpgsqlCommand command1 = new NpgsqlCommand($"DELETE FROM detail_wisata WHERE wisata_id_wisata = '{Id}'", connection);
                    NpgsqlCommand command2 = new NpgsqlCommand($"DELETE FROM tiket WHERE id_wisata = '{Id}'", connection);
                    NpgsqlCommand command3 = new NpgsqlCommand($"DELETE FROM paket_makanan WHERE id_wisata = '{Id}'", connection);

                    command.ExecuteNonQuery();
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();

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

        //public void ShowPicture(string Id)
        //{
        //    try
        //    {
        //        using (NpgsqlConnection connection = new NpgsqlConnection (connectionString))
        //        {
        //            connection.Open();
        //            NpgsqlCommand command = new NpgsqlCommand($"SELECT image FROM wisata WHERE id_wisata = {Id}", connection);
        //            DataTable dt = new DataTable();
        //            dt.Load(command.ExecuteReader());
        //            connection.Close();

        //            if (dt.Rows.Count > 0 && dt.Rows[0]["image"] != DBNull.Value)
        //            {
        //                byte[] imageData = (byte[])dt.Rows[0]["image"];
        //                using (MemoryStream ms = new MemoryStream(imageData))
        //                {
        //                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
        //                }
        //            }
        //            else
        //            {
        //                pictureBox1.Image = null;
        //                LoadData($"{Id}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    imagelocation = dialog.FileName.ToString();
            //    pictureBox1.ImageLocation = imagelocation;
            //}
        }
    }
}