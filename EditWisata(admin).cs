using Fitur_Homepage_admin_penginapan.Models;
using System.Security.Cryptography.X509Certificates;

namespace Fitur_Homepage_admin_penginapan
{
    public partial class Wisata : Form
    {
        Models.WisataContext WisataContext;

        public Wisata()
        {
            InitializeComponent();
            WisataContext = new Models.WisataContext();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadItem();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //form.Show();
            //form.LoadData("A01");
            ////form.ShowPicture("A01");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //form.Show();
            //form.LoadData("A02");
            ////form.ShowPicture("A02");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //form.Show();
            //form.LoadData("A03");
            ////form.ShowPicture("A03");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //form.Show();
            //form.LoadData("A04");
            ////form.ShowPicture("A04");
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            //form.Show();
            //form.LoadData("A01");
            ////form.ShowPicture("A01");
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            //form.Show();
            //form.LoadData("A02");
            ////form.ShowPicture("A02");
        }

        private void LoadItem()
        {
            WisataContext.ReadData();
            List<DataWisata> datas;
            datas = WisataContext.WisataList;

            flowLayoutPanel1.Controls.Clear();
            foreach (var wisata in datas)
            {
                string namaWisata = wisata.nama_wisata;
                Panel panelWisata = new WisataItem(wisata).CreateItem();
                flowLayoutPanel1.Controls.Add(panelWisata);
            }
        }
    }
}