using Fitur_Homepage_admin_penginapan.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitur_Homepage_admin_penginapan
{
    public partial class Edit_detail_wisata : Form
    {
        Models.WisataContext WisataContext;
        private string Id;
        private int IdFasilitas;
        private string imagelocation;
        byte[] imageData;

        public Edit_detail_wisata()
        {
            InitializeComponent();
            WisataContext = new Models.WisataContext();
        }

        private void Edit_detail_wisata_Load(object sender, EventArgs e)
        {

        }
        //public void LoadData(string Id)
        //{
        //    try
        //    {
        //        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            NpgsqlCommand command = new NpgsqlCommand($"SELECT t1.nama_wisata, t1.deskripsi_wisata, t1.alamat_wisata, t3.nama_fasilitas, t4.harga_tiket \r\nFROM wisata AS t1 JOIN detail_wisata AS t2 ON t1.id_wisata = t2.wisata_id_wisata\r\nJOIN fasilitas_wisata AS t3 ON t2.fasilitas_wisata_fasilitas_wisata_id = t3.fasilitas_wisata_id\r\nJOIN tiket AS t4 ON t1.id_wisata = t4.wisata_id_wisata\r\nWHERE t1.id_wisata = '{Id}'", connection);
        //            NpgsqlDataReader reader = command.ExecuteReader();
        //            List<string> fasilitas = new List<string>();


        //            while (reader.Read())
        //            {
        //                Judul.Text = reader["nama_wisata"].ToString();
        //                Keterangan.Text = reader["deskripsi_wisata"].ToString();
        //                Hargatiket.Text = reader["harga_tiket"].ToString();
        //                Lokasi.Text = reader["alamat_wisata"].ToString();
        //                string dataFasilitas = reader["nama_fasilitas"].ToString();
        //                fasilitas.Add(dataFasilitas);
        //            }

        //            string kumpulanFasilitas = "";
        //            foreach (var item in fasilitas)
        //            {
        //                kumpulanFasilitas += item.ToString();
        //                if (item != fasilitas.Last())
        //                {
        //                    kumpulanFasilitas += ", ";
        //                }
        //            }
        //            Fasilitas.Text = kumpulanFasilitas;
        //            reader.Close();

        //            NpgsqlCommand command1 = new NpgsqlCommand($"SELECT t1.nama_paketmakanan FROM paket_makanan AS t1\r\nJOIN wisata AS t2 ON t1.wisata_id_wisata = t2.id_wisata\r\nWHERE t2.id_wisata = '{Id}'", connection);
        //            NpgsqlDataReader reader1 = command1.ExecuteReader();
        //            List<string> menupaket = new List<string>();

        //            while (reader1.Read())
        //            {
        //                string dataMenupaket = reader1["nama_paketmakanan"].ToString();
        //                menupaket.Add(dataMenupaket);
        //            }

        //            string kumpulanMenupaket = "";
        //            foreach (var item in menupaket)
        //            {
        //                kumpulanMenupaket += item.ToString();
        //                if (item != menupaket.Last())
        //                {
        //                    kumpulanMenupaket += ", ";
        //                }
        //            }
        //            Menupaket.Text = kumpulanMenupaket;
        //            reader1.Close();
        //            connection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

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
            string id_wisata = "A01";
            //int id_fasilitas
            string nama_wisata = Judul.Text;
            string deskripsi_wisata = Keterangan.Text;
            string alamat_wisata = Lokasi.Text;
            decimal harga_tiket = decimal.Parse(Hargatiket.Text);
            string fasilitas = Fasilitas.Text;
            string menu_paket = Menupaket.Text;
            byte[] Image = imageData;
            //GetDataWisata();
            //string id_wisata = dataWisata.id_wisata;

            if (string.IsNullOrEmpty(Judul.Text) || string.IsNullOrEmpty(Keterangan.Text) || string.IsNullOrEmpty(Fasilitas.Text) || string.IsNullOrEmpty(Menupaket.Text) || string.IsNullOrEmpty(Lokasi.Text))
            {
                MessageBox.Show("Ada kolom yang kosong. Harap masukkan nilai yang di inginkan.", "Pemberitahuan");
                return;
            }
            if (!long.TryParse(Hargatiket.Text, out long harga))
            {
                MessageBox.Show("Harap mengisi kolom harga yang tersedia", "Pemberitahuan");
                return;
            }
            DataWisata wisata = new DataWisata()
            {
                id_wisata = id_wisata,
                nama_wisata = nama_wisata,
                deskripsi_wisata = deskripsi_wisata,
                alamat_wisata = alamat_wisata,
                harga_tiket = harga_tiket,
                fasilitas = fasilitas,
                menu_paket = menu_paket,
                Image = Image,
            };
            WisataContext.UpdateData(wisata);
        }

        private void Hapus_Click(object sender, EventArgs e)
        {
            string id_wisata = "A01";
            DataWisata dataWisata = new DataWisata
            {
                id_wisata = id_wisata
            };
            DialogResult dr = MessageBox.Show("Apa anda yakin ingin menghapus?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                WisataContext.DeleteData(dataWisata);
                MessageBox.Show("Data berhasil dihapus", "Konfirmasi");
            }
        }

        public void SelectPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "File Gambar|*.jpg;*.png;*.gif;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagelocation = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(imagelocation);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pictureBox1.Image.Save(memoryStream, ImageFormat.Jpeg);
                    imageData = memoryStream.ToArray();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SelectPicture();
        }

        public Models.DataWisata GetDataWisata()
        {
            Models.DataWisata newData = new Models.DataWisata();
            //newData.id_wisata = Id;
            newData.nama_wisata = Judul.Text;
            newData.deskripsi_wisata = Keterangan.Text;
            newData.alamat_wisata = Lokasi.Text;
            newData.harga_tiket = int.Parse(Hargatiket.Text);
            newData.fasilitas = Fasilitas.Text;
            newData.menu_paket = Menupaket.Text;
            //newData.Image = pictureBox1.Image;

            return newData;
        }

        //public Models.DataWisata GetDataFasilitas()
        //{
        //    Models.DataWisata dataFasilitas = new Models.DataWisata();
        //    dataFasilitas.id_fasilitas = IdFasilitas;
        //    dataFasilitas.fasilitas = Fasilitas.Text;

        //    return dataFasilitas;
        //}

        public void SetDataWisata(DataWisata wisata)
        {
            Id = wisata.id_wisata;
            Judul.Text = wisata.nama_wisata;
            Keterangan.Text = wisata.deskripsi_wisata;
            Lokasi.Text = wisata.alamat_wisata;
            Hargatiket.Text = wisata.harga_tiket.ToString();
            IdFasilitas = wisata.id_fasilitas;
            Fasilitas.Text = wisata.fasilitas;
            Menupaket.Text = wisata.menu_paket;
            //pictureBox1 = wisata.Image;
        }
    }
}