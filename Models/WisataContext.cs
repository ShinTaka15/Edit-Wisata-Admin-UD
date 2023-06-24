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
        //string id_wisata = "A01";

        // Fitur Read belum fix
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

                    newData.id_wisata = (string)reader["id_wisata"];
                    newData.nama_wisata = (string)reader["nama_wisata"];
                    newData.deskripsi_wisata = (string)reader["deskripsi_wisata"];
                    newData.alamat_wisata = (string)reader["alamat_wisata"];
                    newData.harga_tiket = (decimal)reader["harga_tiket"];
                    newData.id_fasilitas = (int)reader["fasilitas_wisata_id"];
                    newData.fasilitas = (string)reader["nama_fasilitas"];
                    newData.menu_paket = (string)reader["nama_paketmakanan"];

                    //byte[] imageBytes = (byte[])reader["foto_wisata"];
                    //using (MemoryStream ms = new MemoryStream(imageBytes))
                    //{
                    //    Image image = Image.FromStream(ms);
                    //    newData.Image = image;
                    //}

                    WisataList.Add(newData);
                }
                reader.Close();
            }

            return isSucces;
        }

        public bool UpdateData(DataWisata wisata)
        {
            bool isSucces = false;
            int update1 = 0;
            int update2 = 0;
            int update3 = 0;
            byte[] foto = wisata.Image;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("UPDATE wisata SET nama_wisata = @Nama, deskripsi_wisata = @Deskripsi, alamat_wisata = @Alamat, foto_wisata = @Foto WHERE id_wisata = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", wisata.id_wisata);
                    command.Parameters.AddWithValue("@Nama", wisata.nama_wisata);
                    command.Parameters.AddWithValue("@Deskripsi", wisata.deskripsi_wisata);
                    command.Parameters.AddWithValue("@Alamat", wisata.alamat_wisata);
                    command.Parameters.AddWithValue("@Foto", wisata.Image);

                    update1 = command.ExecuteNonQuery();
                }

                using (NpgsqlCommand command2 = new NpgsqlCommand("UPDATE tiket SET harga_tiket = @Harga WHERE wisata_id_wisata = @Id", connection))
                {
                    command2.Parameters.AddWithValue("@Id", wisata.id_wisata);
                    command2.Parameters.AddWithValue("@Harga", wisata.harga_tiket);

                    update2 = command2.ExecuteNonQuery();
                }

                //foreach (var nama in namaFasilitas)
                //{
                //    using (NpgsqlCommand command1 = new NpgsqlCommand("UPDATE detail_wisata SET fasilitas_wisata_fasilitas_wisata_id = @Fasilitas WHERE wisata_id_wisata = @Id", connection))
                //    {
                //        command1.Parameters.AddWithValue("@Id", id_wisata);
                //        command1.Parameters.AddWithValue("@Fasilitas", wisata.fasilitas);
                //    }
                //}


                //using (NpgsqlCommand command3 = new NpgsqlCommand("INSERT INTO detail_wisata(wisata_id_wisata, fasilitas_wisata_fasilitas_wisata_id) VALUES (@Id,@IdFasilitas)", connection))
                //{
                //    command3.Parameters.AddWithValue("@Id", id_wisata);
                //    command3.Parameters.AddWithValue("@IdFasilitas", wisata.id_fasilitas);

                //    command3.ExecuteNonQuery();
                //}

                using (NpgsqlCommand command4 = new NpgsqlCommand("UPDATE paket_makanan SET nama_paketmakanan = @Menupaket WHERE wisata_id_wisata = @Id", connection))
                {
                    command4.Parameters.AddWithValue("@Id", wisata.id_wisata);
                    command4.Parameters.AddWithValue("@Menupaket", wisata.menu_paket);

                    update3= command4.ExecuteNonQuery();
                }
                if (update1 > 0 && update2 > 0 && update3 > 0)
                {
                    MessageBox.Show("Edit data berhasil", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Edit data gagal", "Error", MessageBoxButtons.OK);
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

                //NpgsqlCommand command = new NpgsqlCommand("DELETE FROM wisata WHERE id_wisata = 'A01'", connection);
                //command.Parameters.AddWithValue("@Id", wisata.id_wisata);
                //int jumlahData = command.ExecuteNonQuery();

                NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM detail_wisata WHERE wisata_id_wisata = @Id", connection);
                command1.Parameters.AddWithValue("@Id", wisata.id_wisata);
                int jumlahData1 = command1.ExecuteNonQuery();

                //NpgsqlCommand command2 = new NpgsqlCommand("DELETE FROM tiket WHERE wisata_id_wisata = @Id", connection);
                //command2.Parameters.AddWithValue("@Id", wisata.id_wisata);
                //int jumlahData2 = command2.ExecuteNonQuery();

                //NpgsqlCommand command3 = new NpgsqlCommand("DELETE FROM paket_makanan WHERE wisata_id_wisata = @Id", connection);
                //command3.Parameters.AddWithValue("@Id", wisata.id_wisata);
                //int jumlahData3 = command3.ExecuteNonQuery();

                if (jumlahData1 > 0 )
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
