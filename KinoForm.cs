using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktiline_too_Kino
{
    public partial class KinoForm : Form
    {
        string filmiId;
        string seanss_start;
        string posterFile;
        string filmiNimetus;
        string posterPath;
        string postersDirectory = Path.Combine(Application.StartupPath, "../../Poster");
        private List<string> valitudKohad;


        SqlCommand cmd;
        SqlDataReader reader;
        Button btn1, btn2;
        PictureBox pictureBox;
        Label filmi_nimetus_lbl;
        List<Image> posters;
        List<string> filmiNimetuss;
        int praegune_indeks;

        public KinoForm()
        {
            this.Height = 600;
            this.Width = 550;
            this.Text = "Tere kasutaja";
            this.BackgroundImage = Image.FromFile(@"../../NightSky.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            ForeColor = Color.White;
            BackColor = Color.Black;

            btn1 = new Button();
            btn1.Text = "->";
            btn1.Size = new Size(85, 360);
            btn1.Location = new Point(360, 150);
            btn1.Font = new Font("Bauhaus 93", 18, FontStyle.Italic);
            btn1.Click += Btn1_Click;
            Controls.Add(btn1);
            valitudKohad = new List<string>();
            btn2 = new Button();
            btn2.Text = "Piletit ostes";
            btn2.Size = new Size(360, 90);
            btn2.Location = new Point(80, 60);
            btn2.Font = new Font("Bauhaus 93", 18, FontStyle.Italic);
            btn2.Click += Btn2_Click;
            Controls.Add(btn2);

            pictureBox = new PictureBox();
            pictureBox.Location = new Point(45, 150);
            pictureBox.Size = new Size(327, 359);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(pictureBox);

            filmi_nimetus_lbl = new Label();
            filmi_nimetus_lbl.Size = new Size(327, 50);
            filmi_nimetus_lbl.Location = new Point(35, 500);
            filmi_nimetus_lbl.Font = new Font("Bauhaus 93", 14, FontStyle.Italic);
            filmi_nimetus_lbl.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(filmi_nimetus_lbl);

            NaitaAndmedPoster();
        }

        private void NaitaAndmedPoster()
        {
            posters = new List<Image>();
            filmiNimetuss = new List<string>();  // Инициализируем список для названий фильмов

            // Проверяем, существует ли папка Poster
            if (Directory.Exists(postersDirectory))
            {
                try
                {
                    // Открываем соединение с базой данных
                    AppContext.conn.Open();
                    cmd = new SqlCommand("SELECT Id, Poster, Filmi_nimetus FROM Kinolaud", AppContext.conn);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        posterFile = reader["Poster"].ToString();
                        filmiNimetus = reader["Filmi_nimetus"].ToString();
                        posterPath = Path.Combine(postersDirectory, posterFile);

                        // Проверяем, существует ли файл изображения в папке Poster
                        if (File.Exists(posterPath))
                        {
                            posters.Add(Image.FromFile(posterPath));
                            filmiNimetuss.Add(filmiNimetus);  // Добавляем название фильма в список
                        }
                        else
                        {
                            MessageBox.Show($"Pilt '{posterFile}' ei leitud Poster kaustast");
                        }
                    }

                    reader.Close();
                    AppContext.conn.Close();

                    // Если изображения загружены, показываем первое
                    if (posters.Count > 0)
                    {
                        praegune_indeks = 0;
                        pictureBox.Image = posters[praegune_indeks];
                        filmi_nimetus_lbl.Text = filmiNimetuss[praegune_indeks];  // Обновляем название фильма
                    }
                    else
                    {
                        MessageBox.Show("Kinolaud'i tabelis ei ole ühtegi pilti");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Viga piltide üleslaadimisel: " + ex.Message);
                    if (AppContext.conn.State == System.Data.ConnectionState.Open)
                    {
                        AppContext.conn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Plakatikausta ei leitud");
            }
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            // Переключаем изображение и получаем данные фильма
            if (posters != null && posters.Count > 0)
            {
                praegune_indeks++;
                if (praegune_indeks >= posters.Count)
                {
                    praegune_indeks = 0; // Вернуться к первой картинке, если дошли до конца
                }

                pictureBox.Image = posters[praegune_indeks];
                filmi_nimetus_lbl.Text = filmiNimetuss[praegune_indeks];

                filmiNimetus = filmi_nimetus_lbl.Text;
                posterPath = Path.Combine(postersDirectory, $"{filmiNimetus}.jpg".Trim().Replace("\n", "").Replace("\r", ""));


                try
                {
                    AppContext.conn.Open();
                    // Запрос для получения ID фильма по названию
                    cmd = new SqlCommand("SELECT Id FROM Kinolaud WHERE Filmi_nimetus = @filmiNimetus", AppContext.conn);
                    cmd.Parameters.AddWithValue("@filmiNimetus", filmiNimetus);
                    filmiId = cmd.ExecuteScalar()?.ToString();

                    // Если ID фильма не найден, выводим ошибку
                    if (string.IsNullOrEmpty(filmiId))
                    {
                        Debug.WriteLine("Viga: Filmi ID-d ei leitud");
                        return;
                    }

                    // Запрос для получения данных сеанса
                    cmd = new SqlCommand("SELECT * FROM seansid WHERE Kinolaud_id = @filmiId", AppContext.conn);
                    cmd.Parameters.AddWithValue("@filmiId", filmiId);
                    reader = cmd.ExecuteReader();

                    if (reader.Read()) // Если есть хотя бы одна строка
                    {
                        // Чтение значения времени сеанса
                        if (reader["Start_time"] != DBNull.Value)
                        {
                            seanss_start = reader["Start_time"].ToString();
                            Debug.WriteLine($"Start_time (из базы данных): {seanss_start}");
                        }
                        else
                        {
                            seanss_start = "Andmed puuduvad";
                            Debug.WriteLine("Start_time andmebaasis on tühi");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Viga: Valitud filmi jaoks ei leitud seanssi");
                        MessageBox.Show("Valitud filmi jaoks ei ole seansse leitud");
                        return;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Viga andmete laadimisel andmebaasist: " + ex.Message);
                    Debug.WriteLine($"Viga: {ex.Message}");
                }
                finally
                {
                    AppContext.conn.Close();
                }


            }
        }

        private void KinoForm_Load(object sender, EventArgs e)
        {

        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            // Открываем форму выбора мест
            PiletiOstmiseForm ticketForm = new PiletiOstmiseForm(filmiNimetus, posterPath, seanss_start);
            ticketForm.Show();
        }
    }
}
