using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitur_Homepage_admin_penginapan.Models
{
    public class DataWisata
    {
        public string id_wisata { get; set; }
        public string nama_wisata { get; set; }
        public string deskripsi_wisata { get; set; }
        public string alamat_wisata { get; set;}
        public decimal harga_tiket { get; set; }
        public int id_fasilitas { get; set; }
        public string fasilitas { get; set; }
        public string menu_paket { get; set; }
    }
}
