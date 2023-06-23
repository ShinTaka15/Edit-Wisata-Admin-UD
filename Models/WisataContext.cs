using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitur_Homepage_admin_penginapan.Models
{
    public class WisataContext
    {
        private string connectionString = "host=localhost;port=5432;database=JT-Apps;username=postgres;password=Memew001";
        public List <DataWisata> WisataList = new List<DataWisata>();
        //public List <IdFasilitas> idFasilitas1 = new List<IdFasilitas>();

        public bool ReadData()
        {
            bool isSucces = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("SELECT t1.id_wisata, t1.nama_wisata, t1.deskripsi_wisata, t1.alamat_wisata, t1.foto_wisata, t3.fasilitas_wisata_id, t3.nama_fasilitas, t4.harga_tiket, t5.nama_paketmakanan FROM wisata t1 JOIN detail_wisata t2 ON t1.id_wisata = t2.wisata_id_wisata JOIN fasilitas_wisata t3 ON t2.fasilitas_wisata_fasilitas_wisata_id = t3.fasilitas_wisata_id JOIN tiket t4 ON t1.id_wisata = t4.wisata_id_wisata JOIN paket_makanan t5 ON t1.id_wisata = t5.wisata_id_wisata", connection);
                NpgsqlDataReader reader = command.ExecuteReader();

                WisataList.Clear();
                while (reader.Read())
                {
                    DataWisata newData = new DataWisata();
                    //IdFasilitas idFasilitas = new IdFasilitas();

                    newData.id_wisata = (string)reader["id_wisata"];
                    newData.nama_wisata = (string)reader["nama_wisata"];
                    newData.deskripsi_wisata = (string)reader["deskripsi_wisata"];
                    newData.alamat_wisata = (string)reader["alamat_wisata"];
                    newData.harga_tiket = (decimal)reader["harga_tiket"];
                    newData.id_fasilitas = (int)reader["fasilitas_wisata_id"];
                    newData.fasilitas = (string)reader["nama_fasilitas"];
                    newData.menu_paket = (string)reader["nama_paketmakanan"];

                    byte[] imageBytes = (byte[])reader["foto_wisata"];
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(ms);
                        newData.Image = image;
                    }

                    WisataList.Add(newData);
                }
                reader.Close();
            }

            return isSucces;
        }

        public bool UpdateData(DataWisata wisata)
        {
            bool isSucces = false;
            Edit_detail_wisata edit_Detail_Wisata = new Edit_detail_wisata();
            List<DataWisata> namaFasilitas = new List<DataWisata>();
            namaFasilitas.Add(edit_Detail_Wisata.GetDataFasilitas());

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("UPDATE wisata t1 JOIN tiket t2 SET t1.nama_wisata = @Nama, t1.deskripsi_wisata = @Deskripsi, t1.alamat_wisata = @Alamat, t1.foto_wisata = @Foto, t2.harga_tiket = @Harga WHERE t1.id_wisata = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", wisata.id_wisata);
                    command.Parameters.AddWithValue("@Nama", wisata.nama_wisata);
                    command.Parameters.AddWithValue("@Deskripsi", wisata.deskripsi_wisata);
                    command.Parameters.AddWithValue("@Alamat", wisata.alamat_wisata);
                    command.Parameters.AddWithValue("@Foto", wisata.Image);
                    command.Parameters.AddWithValue("@Harga", wisata.harga_tiket);

                    command.ExecuteNonQuery();
                }

                foreach (var nama in namaFasilitas)
                {
                    using (NpgsqlCommand command1 = new NpgsqlCommand("SELECT fasilitas_wisata_id FROM fasilitas_wisata ilike @Fasilitas", connection))
                    {
                        command1.Parameters.AddWithValue("@Fasilitas", nama);
                        NpgsqlDataReader reader = command1.ExecuteReader();
                        while (reader.Read())
                        {
                            DataWisata Id = new DataWisata();
                            Id.id_fasilitas = (int)reader["fasilitas_wisata_id"];
                            namaFasilitas.Add(Id);
                        }
                        reader.Close();
                    }
                }

                using (NpgsqlCommand command2 = new NpgsqlCommand("DELETE FROM detail_wisata WHERE wisata_id_wisata = @Id", connection))
                {
                    command2.Parameters.AddWithValue("@Id", wisata.id_wisata);

                    command2.ExecuteNonQuery();
                }

                using (NpgsqlCommand command3 = new NpgsqlCommand("INSERT INTO detail_wisata(wisata_id_wisata, fasilitas_wisata_fasilitas_wisata_id) VALUES (@Id,@IdFasilitas)", connection))
                {
                    command3.Parameters.AddWithValue("@Id", wisata.id_wisata);
                    command3.Parameters.AddWithValue("@IdFasilitas", wisata.id_fasilitas);

                    command3.ExecuteNonQuery();
                }

                using (NpgsqlCommand command4 = new NpgsqlCommand("UPDATE FROM paket_makanan SET nama_paketmakanan = @Menupaket", connection))
                {
                    command4.Parameters.AddWithValue("@Menupaket", wisata.menu_paket);

                    command4.ExecuteNonQuery();
                }
            }

            return isSucces;
        }

        public bool DeleteData(DataWisata wisata)
        {
            bool isSucces = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM wisata WHERE id_wisata = @Id", connection);
                command.Parameters.AddWithValue("@Id", wisata.id_wisata);
                int jumlahData = command.ExecuteNonQuery();

                NpgsqlCommand command1 = new NpgsqlCommand($"DELETE FROM detail_wisata WHERE id_wisata = @Id", connection);
                command1.Parameters.AddWithValue("@Id", wisata.id_wisata);
                int jumlahData1 = command1.ExecuteNonQuery();

                NpgsqlCommand command2 = new NpgsqlCommand($"DELETE FROM tiket WHERE id_wisata = @Id", connection);
                command2.Parameters.AddWithValue("@Id", wisata.id_wisata);
                int jumlahData2 = command2.ExecuteNonQuery();

                NpgsqlCommand command3 = new NpgsqlCommand($"DELETE FROM paket_makanan WHERE id_wisata = @Id", connection);
                command3.Parameters.AddWithValue("@Id", wisata.id_wisata);
                int jumlahData3 = command3.ExecuteNonQuery();

                if ( jumlahData > 0 && jumlahData1 > 0 && jumlahData2 > 0 && jumlahData3 > 0)
                {
                    isSucces = true;
                    var itemToRemove = WisataList.Single(rec => rec.id_wisata == wisata.id_wisata);
                    WisataList.Remove(itemToRemove);
                }
            }

            return isSucces;
        }
    }
}
