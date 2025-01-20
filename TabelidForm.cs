using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktiline_too_Kino
{
    public partial class TabelidForm : Form
    {
        Button Kinolaud_btn, Kohad_btn, Piletid_btn, Saal_btn, Seansid_btn;

        public TabelidForm()
        {
            this.Height = 175;
            this.Width = 300;
            this.Text = "Tabelid";
            this.BackgroundImage = Image.FromFile(@"../../NightSky.jpg");

            ForeColor = Color.White;
            BackColor = Color.Black;

            // Button - Kinolaud
            Kinolaud_btn = new Button();
            Kinolaud_btn.Font = new Font("Bauhaus 93", 12, FontStyle.Bold);
            Kinolaud_btn.Text = "Kinolaud";
            Kinolaud_btn.Size = new Size(120, 40);
            Kinolaud_btn.Location = new Point(150, 30);
            Controls.Add(Kinolaud_btn);
            Kinolaud_btn.Click += Kinolaud_btn_Click;

            // Button - Kohad
            Kohad_btn = new Button();
            Kohad_btn.Font = new Font("Bauhaus 93", 12, FontStyle.Bold);
            Kohad_btn.Text = "Tugitoolid";
            Kohad_btn.Size = new Size(120, 40);
            Kohad_btn.Location = new Point(20, 30);
            Controls.Add(Kohad_btn);
            Kohad_btn.Click += Kohad_btn_Click;

            // Button - Saal
            Saal_btn = new Button();
            Saal_btn.Font = new Font("Bauhaus 93", 12, FontStyle.Bold);
            Saal_btn.Text = "Saal";
            Saal_btn.Size = new Size(120, 40);
            Saal_btn.Location = new Point(20, 90);
            Controls.Add(Saal_btn);
            Saal_btn.Click += Saal_btn_Click;

            // Button - Seansid
            Seansid_btn = new Button();
            Seansid_btn.Font = new Font("Bauhaus 93", 12, FontStyle.Bold);
            Seansid_btn.Text = "Seansid";
            Seansid_btn.Size = new Size(120, 40);
            Seansid_btn.Location = new Point(150, 90);
            Controls.Add(Seansid_btn);
            Seansid_btn.Click += Seansid_btn_Click;
        }

        private void TabelidForm_Load(object sender, EventArgs e)
        {

        }

        private void Kinolaud_btn_Click(object sender, EventArgs e)
        {
            KinolaudForm kinolaud = new KinolaudForm();
            kinolaud.Show();
        }

        private void Kohad_btn_Click(object sender, EventArgs e)
        {
            KohadForm kohad = new KohadForm();
            kohad.Show();
        }

        private void Saal_btn_Click(object sender, EventArgs e)
        {
            LauasaalForm saal = new LauasaalForm();
            saal.Show();
        }

        private void Seansid_btn_Click(object sender, EventArgs e)
        {
            Seanside_laudForm seansid = new Seanside_laudForm();
            seansid.Show();
        }
    }
}
