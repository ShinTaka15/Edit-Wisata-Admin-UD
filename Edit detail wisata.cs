﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitur_Homepage_admin_penginapan
{
    public partial class Edit_detail_wisata : Form
    {
        private string connectionString = "host=localhost;port=5432;database=Wisata Admin;user id=postgres;password=shofiyah774";

        public Edit_detail_wisata()
        {
            InitializeComponent();
        }

        private void Edit_detail_wisata_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand("SELECT t1.nama_wisata, t1.deskripsi_wisata, t1.alamat_wisata, t3.harga_tiket\r\nFROM wisata AS t1 \r\nJOIN detail_wisata AS t2 ON t1.id_wisata = t2.wisata_id_wisata \r\nJOIN tiket AS t3 ON t1.id_wisata = t3.wisata_id_wisata\r\nWHERE t1.id_wisata = 'A01'", connection);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    List<string> menupaket = new List<string>();

                    while (reader.Read())
                    {
                        Judul.Text = reader["nama_wisata"].ToString();
                        Keterangan.Text = reader["deskripsi_wisata"].ToString();
                        Hargatiket.Text = reader["harga_tiket"].ToString();
                        Lokasi.Text = reader["alamat_wisata"].ToString();
                        //string dataMenupaket = reader["nama_paketmakanan"].ToString();
                        //menupaket.Add(dataMenupaket);
                    }
                    //string kumpulanMenupaket = "";
                    //foreach (var item in menupaket)
                    //{
                    //    kumpulanMenupaket += item.ToString();
                    //    kumpulanMenupaket += ", ";
                    //}
                    //Menupaket.Text = kumpulanMenupaket;
                    reader.Close();

                    NpgsqlCommand command1 = new NpgsqlCommand("SELECT t1.nama_fasilitas FROM fasilitas_wisata AS t1\r\nJOIN detail_wisata AS t2 ON t1.fasilitas_wisata_id = t2.fasilitas_wisata_fasilitas_wisata_id\r\nJOIN wisata AS t3 ON t2.wisata_id_wisata = t3.id_wisata\r\nWHERE t3.id_wisata = 'A01'", connection);
                    NpgsqlDataReader reader1 = command1.ExecuteReader();
                    List<string> fasilitas = new List<string>();

                    while (reader1.Read()) 
                    {
                        string dataFasilitas = reader1["nama_fasilitas"].ToString();
                        fasilitas.Add(dataFasilitas);
                    }
                    string kumpulanFasilitas = "";
                    foreach (var item in fasilitas)
                    {
                        kumpulanFasilitas += item.ToString();
                        kumpulanFasilitas += ", ";
                    }
                    Fasilitas.Text = kumpulanFasilitas;
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
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string id = "A01";
                    string newJudul = Judul.Text;
                    string newDeskripsi = Keterangan.Text;
                    string newAlamat = Lokasi.Text;

                    NpgsqlCommand command = new NpgsqlCommand("UPDATE wisata SET nama_wisata = @Nama, deskripsi_wisata = @Deskripsi, alamat_wisata = @Alamat ", connection);
                    command.Parameters.AddWithValue("@Nama", newJudul);
                    command.Parameters.AddWithValue("@Deskripsi", newDeskripsi);
                    command.Parameters.AddWithValue("@Alamat", newAlamat);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Data baru tersimpan");
                    connection.Close();
                }

                LoadData();
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