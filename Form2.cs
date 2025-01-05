using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace KRONX_BETA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

 

        private void button1_Click(object sender, EventArgs e)
        {
            if (logopcbox.Tag != null)
            {
                string connectionString = "SERVER=199.167.144.250;DATABASE=xnsrgio1_kronxbd;UID=xnsrgio1_krinxbd;PASSWORD=Veuliah@#2057;";
                string targetDir = Path.Combine(Application.StartupPath, "uploads");
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                string fileName = Path.GetFileName(logopcbox.Tag.ToString());
                string targetPath = Path.Combine(targetDir, fileName);
                logopcbox.Image.Save(targetPath);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO usuarios (usuario, senha, foto) VALUES (@usuario, @senha, @foto)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@usuario", textBox1.Text);
                            cmd.Parameters.AddWithValue("@senha", textBox2.Text);
                            cmd.Parameters.AddWithValue("@foto", targetPath);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Usuário cadastrado com sucesso!");
                        }
                        this.Close();
                        Form1 form1 = new Form1();
                        form1.ShowDialog();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma imagem de perfil.");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                logopcbox.Image = new Bitmap(open.FileName);
                logopcbox.Tag = open.FileName; // Armazenar o caminho da imagem
            }

        }
    }
}
