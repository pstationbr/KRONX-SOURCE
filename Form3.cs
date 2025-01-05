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

namespace KRONX_BETA
{
    public partial class Form3 : Form
    {
        public string Usuario { get; set; }
        public string Foto { get; set; }

        public Form3()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = Usuario;
            if (!string.IsNullOrEmpty(Foto) && File.Exists(Foto))
            {
                pictureBox1.Image = Image.FromFile(Foto);
            }
            else
            {
                MessageBox.Show("Imagem de perfil não encontrada.");
            }

        }
    }
}
