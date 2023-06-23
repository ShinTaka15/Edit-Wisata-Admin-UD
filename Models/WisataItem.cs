using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitur_Homepage_admin_penginapan.Models
{
    internal class WisataItem
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wisata));
        Panel panelWisata = new();
        Label labelNamaWisata = new();
        DataWisata dataWisata;

        public WisataItem(DataWisata dataWisata)
        {
            this.dataWisata = dataWisata;
            panelWisata.Controls.Add(labelNamaWisata);

            labelNamaWisata.AutoSize = true;
            labelNamaWisata.Font = new Font("Poppins Black", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            labelNamaWisata.Location = new Point(7, 47);
            labelNamaWisata.Name = "id_wisata";
            labelNamaWisata.Size = new Size(138, 44);
            labelNamaWisata.ForeColor = Color.White;
            labelNamaWisata.Text = dataWisata.nama_wisata;

            //panelWisata.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelWisata.BackColor = Color.Transparent;
            panelWisata.BackgroundImage = (Image)resources.GetObject("panel4.BackgroundImage");
            panelWisata.BackgroundImageLayout = ImageLayout.Stretch;
            panelWisata.Controls.Add(labelNamaWisata);
            panelWisata.Location = new Point(41, 200);
            panelWisata.Margin = new Padding(3, 4, 3, 4);
            panelWisata.Size = new Size(810, 133);
            panelWisata.Click += PanelWisata_Click;

        }

        private void PanelWisata_Click (object sender, EventArgs e)
        {
            using (Edit_detail_wisata edit_Detail_Wisata = new Edit_detail_wisata ())
            {
                WisataContext wisataContext = new WisataContext ();
                edit_Detail_Wisata.SetDataWisata(dataWisata);
                DialogResult dialogResult = edit_Detail_Wisata.ShowDialog();
            }
        }

        public Panel CreateItem()
        {
            return panelWisata;
        }
    }
}
