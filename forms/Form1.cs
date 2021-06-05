using Framework_Aplicacao.classes;
using System;
using System.Windows.Forms;

namespace Framework_Aplicacao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDiretorioProjetoConvertido.Text = Parametros.DiretorioProjetoConvertido = @"C:\Temp\ClassFactoryConvertido";
            txtDiretorioProjetoLegado.Text = Parametros.DiretorioProjetoLegado = @"C:\Temp\ClassFactory";
        }

        private void btnDiretorioProjetoLegado_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
                txtDiretorioProjetoLegado.Text = Parametros.DiretorioProjetoLegado = folder.SelectedPath;
            else
                txtDiretorioProjetoLegado.Text = Parametros.DiretorioProjetoLegado = "";
        }

        private void btnDiretorioProjetoConvertido_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
                txtDiretorioProjetoConvertido.Text = Parametros.DiretorioProjetoConvertido = folder.SelectedPath;
            else
                txtDiretorioProjetoConvertido.Text = Parametros.DiretorioProjetoConvertido = "";
        }

        private void btnConverter_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Parametros.DiretorioProjetoLegado))
                {
                    MessageBox.Show("Informe o diretório do projeto legado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(Parametros.DiretorioProjetoConvertido))
                {
                    MessageBox.Show("Informe o diretório de gravação do novo projeto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                lblStatus.Visible = true;
                Application.DoEvents();

                Copia.ProjetoModelo();

                Copia.Estrutura();

                Referencias.Localizar();

                Converter.Forms();

                MessageBox.Show("Conversão realizada com sucesso!\n\nConfira em " + Parametros.DiretorioProjetoConvertido, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                lblStatus.Visible = false;
            }
        }
    }
}
