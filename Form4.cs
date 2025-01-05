using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KRONX_BETA
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "arp -a", "tracert", "ping", "netstat", "systeminfo", "nmap" });
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExecutarComando_Click_1(object sender, EventArgs e)
        {
            StringBuilder resultado = new StringBuilder();
            try
            {
                string comando = comboBox1.SelectedItem.ToString();
                string parametros = textBox1.Text;
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", $"/c {comando} {parametros}");
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                using (Process process = Process.Start(psi))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            resultado.AppendLine(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.AppendLine("Erro ao executar comando: " + ex.Message);
            }

            textBox2.Text = resultado.ToString();
        }
    }
}
