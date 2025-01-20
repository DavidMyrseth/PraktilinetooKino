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
    public partial class RegistreerimineForm : Form
    {
        private Button btnLogin;
        private Label lblRole;
        private ComboBox cbRole;

        public RegistreerimineForm()
        {
            InitializeComponent();
            ConfigureForm();
            AddControls();
        }

        private void ConfigureForm()
        {
            this.Height = 300;
            this.Width = 300;
            this.Text = "Registreerimine";
            this.BackgroundImage = Image.FromFile(@"../../NightSky.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void AddControls()
        {
            btnLogin = new Button
            {
                Text = "Logi sisse",
                Size = new Size(185, 50),
                Location = new Point(40, 180),
                Font = new Font("Bauhaus 93", 18, FontStyle.Italic),
                ForeColor = Color.White,
                BackColor = Color.Black,
            };
            btnLogin.Click += BtnLogin_Click;
            Controls.Add(btnLogin);

            lblRole = new Label
            {
                AutoSize = true,
                Text = "Kasutajad",
                Font = new Font("Bauhaus 93", 25, FontStyle.Italic),
                Location = new Point(44, 40),
                ForeColor = Color.White,
                BackColor = Color.Black,
            };
            Controls.Add(lblRole);

            cbRole = new ComboBox
            {
                Location = new Point(40, 100),
                Font = new Font("Bauhaus 93", 15),
                Width = 180,
                ForeColor = Color.White,
                BackColor = Color.Black,
            };
            cbRole.Items.AddRange(new[] { "Admin", "Kasutaja" });
            Controls.Add(cbRole);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (cbRole.SelectedItem != null)
            {
                string selectedRole = cbRole.SelectedItem.ToString();

                switch (selectedRole)
                {
                    case "Admin":
                        OpenAdminForm();
                        break;

                    case "Kasutaja":
                        OpenUserForm();
                        break;

                    default:
                        MessageBox.Show("Valitud roll ei toetatud.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Valige roll.", "Hoiatus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OpenAdminForm()
        {
            TabelidForm adminForm = new TabelidForm();
            adminForm.Show();
        }

        private void OpenUserForm()
        {
            KinoForm userForm = new KinoForm();
            userForm.Show();
        }

        private void RegistreerimineForm_Load(object sender, EventArgs e)
        {
        }
    }
}