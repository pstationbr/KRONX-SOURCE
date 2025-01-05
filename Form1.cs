using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KRONX_BETA
{
    public partial class Form1 : Form
    {
        public string Usuario { get; set; }
        public string Foto { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Usuario;
            if (!string.IsNullOrEmpty(Foto))
            {
                pictureBox1.Image = Image.FromFile(Foto);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "SERVER=199.167.144.250;DATABASE=xnsrgio1_kronxbd;UID=xnsrgio1_krinxbd;PASSWORD=Veuliah@#2057;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT usuario, foto FROM usuarios WHERE usuario = @usuario AND senha = @senha";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", textBox1.Text);
                        cmd.Parameters.AddWithValue("@senha", textBox2.Text);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string usuario = reader["usuario"].ToString();
                                string foto = reader["foto"].ToString();
                                MessageBox.Show("Login bem-sucedido!");
                                this.Hide();
                                Form3 painelForm = new Form3();
                                painelForm.Usuario = usuario;
                                painelForm.Foto = foto;
                                painelForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Usuário ou senha incorretos.");
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message);
                }
            }

        }
    }
}
