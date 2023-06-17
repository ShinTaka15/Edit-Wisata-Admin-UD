using System.Security.Cryptography.X509Certificates;

namespace Fitur_Homepage_admin_penginapan
{
    public partial class Wisata : Form
    {
        public Wisata()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Edit_detail_wisata form = new Edit_detail_wisata();
            form.Show();
            form.LoadData("A01");
        }
    }
}