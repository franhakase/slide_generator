using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace slide_generator
{
    public partial class FrmPrincipal : Form
    {
        private string sPastaSelecionada;
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog f = new FolderBrowserDialog())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if(Directory.Exists(f.SelectedPath))
                    {
                        sPastaSelecionada = f.SelectedPath;
                        textBox1.Text = sPastaSelecionada;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!Directory.Exists(sPastaSelecionada))
            {
                MessageBox.Show("Necessário especificar uma pasta existente.", "Aviso.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string[] arquivos = Directory.GetFiles(sPastaSelecionada);


            string[] extensoes_img = { "jpg", "jpeg", "webp", "gif", "png" };
            StringBuilder sb = new StringBuilder();
            foreach(string sNomeArquivo in arquivos)
            {
                if(extensoes_img.Contains(Path.GetExtension(sNomeArquivo).ToLower()))
                {
                    sb.AppendLine(string.Format("<div class='mySlides fade'><img src='{0}'></div>", sNomeArquivo));
                }
            }

            string sArquivoFinal = Properties.Resources.dummy;

            sArquivoFinal = sArquivoFinal.Replace("@DIV_IMG_AQUI", sb.ToString());
            sArquivoFinal = sArquivoFinal.Replace("@NRO_SEGUNDOS", numericUpDown1.Value.ToString());

            string sNomeArquivoFinal = string.Format("{0}\\index.htm", sPastaSelecionada);
            File.WriteAllText(sNomeArquivoFinal, sArquivoFinal);
            if (File.Exists(sNomeArquivoFinal))
            {
                MessageBox.Show("Processo finalizado com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
