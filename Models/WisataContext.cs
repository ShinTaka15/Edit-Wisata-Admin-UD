using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitur_Homepage_admin_penginapan.Models
{
    public class WisataContext
    {
        private string connectionString = "host=localhost;port=5432;database=JT-Apps;username=postgres;password=12345";
        public List <DataWisata> WisataList = new List<DataWisata>();

        public bool ReadData()
        {
            bool isSucces = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("SELECT t1.id_wisata, t1.nama_wisata, t1.deskripsi_wisata, t1.alamat_wisata, t3.fasilitas_wisata_id, t3.nama_fasilitas, t4.harga_tiket, t5.nama_paketmakanan FROM wisata t1 JOIN detail_wisata t2 ON t1.id_wisata = t2.wisata_id_wisata JOIN fasilitas_wisata t3 ON t2.fasilitas_wisata_fasilitas_wisata_id = t3.fasilitas_wisata_id JOIN tiket t4 ON t1.id_wisata = t4.wisata_id_wisata JOIN paket_makanan t5 ON t1.id_wisata = t5.wisata_id_wisata", connection);
                NpgsqlDataReader reader = command.ExecuteReader();

                WisataList.Clear();
                while (reader.Read())
                {
                    DataWisata newData = new DataWisata();

                    newData.id_wisata = (string)reader["id_wisata"];
                    newData.nama_wisata = (string)reader["nama_wisata"];
                    newData.deskripsi_wisata = (string)reader["deskripsi_wisata"];
                    newData.alamat_wisata = (string)reader["alamat_wisata"];
                    newData.harga_tiket = (decimal)reader["harga_tiket"];
                    newData.id_fasilitas = (int)reader["fasilitas_wisata_id"];
                    newData.fasilitas = (string)reader["nama_fasilitas"];
                    newData.menu_paket = (string)reader["nama_paketmakanan"];

                    WisataList.Add(newData);
                }
            }

            return isSucces;
        }

        public bool UpdateData(DataWisata wisata)
        {
            bool isSucces = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("UPDATE wisata t1 ", connection);

                command.Parameters.AddWithValue("", wisata.id_wisata);
                command.Parameters.AddWithValue("", wisata.nama_wisata);
            }
            return isSucces;
        }
    }
}
