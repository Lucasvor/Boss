using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Banco_de_Dados
{
    public partial class Sistema : Form
    {

        SqlConnection sqlCon = new SqlConnection(Connect.sqlCon);
        SqlTransaction tran;
        SqlCommand cmd;
        int error;
        int contadorError;
        int j;
        int flag_error;

        const string queryInsert = "insert into dbo.tb_carta(cartorio, protocolo, dataprotocolo, destinatario, docdestinatario, endereco, complemento, bairro, cidade, UF, CEP, nrointimacao, prazolimite, dataentrada) values(@cartorio,@protocolo,@dataprotocolo,@destinatario,@docdestinatario,@endereco,@complemento,@bairro,@cidade,@UF,@CEP,@nrointimacao,@prazolimite,@dataentrada)";


        public Sistema()
        {
            InitializeComponent();
            //panel13.BringToFront();



        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            button1.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;
            BackImporta.Visible = true;
            Baixa.Visible = false;


            //panel14.BringToFront();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;

            Consulta Consulta = new Consulta();
            Consulta.ShowDialog();
            Consulta.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            button4.BackColor = System.Drawing.Color.Transparent;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);

            BackImporta.Visible = false;
            Baixa.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;
            //panel13.BringToFront();
            BackImporta.Visible = false;
            textBox1.Clear();
            progressBar1.Value = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            progressBar1.Value = 0;
            j = 0;
            using (OpenFileDialog ofd = new OpenFileDialog { Title = "Escolha um Arquivo TXT", Filter = "All Files|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    backgroundWorker1.RunWorkerAsync(ofd.FileName);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {  /// Connect database
            sqlCon.Open();
            tran = sqlCon.BeginTransaction();

        
            // Váriavel
            string s;
            int flag = 0, contador = 2, contador_Error = 0;
            var lengths = new[] { 2, 4, 8, 45, 14, 45, 20, 20, 20, 2, 8, 13 , 8 };
            string dataentrada, erroTexto = null, fim = null;
            var parts = new string[lengths.Length];
            var files = File.ReadAllLines((string)e.Argument);
            //


            using (var reader = new StreamReader((string)e.Argument,Encoding.UTF7))
            {

               dataentrada = reader.ReadLine();
               dataentrada = dataentrada.Substring(0, 8);
               fim = dataentrada;
                while (!reader.EndOfStream)
                {
                    s = reader.ReadLine();
                    AppendTextBox("Importando Linha: " + (contador));
                    //textBox1.AppendText("Importando Linha: " + (contador++));
                    var startPos = 0;

                    try{
                        if (s.Trim().Length < 210)
                        {
                            if(fim == s.Substring(0, 8))
                            {
                                if (flag == 0)
                                {
                                    AppendTextBox("  Operação Concluída com Sucesso!");
                                    break;
                                }else
                                {
                                    AppendTextBox("  Operação Concluída com Falha!");
                                    break;
                                }
                            }
                            else{
                                flag_error = 1;
                                erroTexto = "Falta dados necessários para importação! Linha: " + contador;
                                throw new Exception(erroTexto); }
                        }

                         for (int i = 0; i < lengths.Length; i++){
                            parts[i] = s.Substring(startPos, lengths[i]);
                            startPos += lengths[i];
                        }
                        if (parts[0].Trim().Length < 2 && parts[11].Trim().Length < 13)
                        {
                            flag_error = 2;
                            erroTexto = "Dado inválido para chave Primária do Banco!";
                            throw new Exception(erroTexto);
                        }

                        cmd = new SqlCommand(queryInsert, sqlCon, tran);
                        cmd.Parameters.AddWithValue("@cartorio", parts[0]);
                        cmd.Parameters.AddWithValue("@protocolo", parts[1]);
                        cmd.Parameters.AddWithValue("@dataprotocolo", parts[2]);
                        cmd.Parameters.AddWithValue("@destinatario", parts[3]);
                        cmd.Parameters.AddWithValue("@docdestinatario", parts[4]);
                        cmd.Parameters.AddWithValue("@endereco", parts[5]);
                        cmd.Parameters.AddWithValue("@complemento", parts[6]);
                        cmd.Parameters.AddWithValue("@bairro", parts[7]);
                        cmd.Parameters.AddWithValue("@cidade", parts[8]);
                        cmd.Parameters.AddWithValue("@UF", parts[9]);
                        cmd.Parameters.AddWithValue("@CEP", parts[10]);
                        cmd.Parameters.AddWithValue("@nrointimacao", parts[11]);
                        cmd.Parameters.AddWithValue("@prazolimite", DateTime.ParseExact(parts[12], "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("g"));
                        cmd.Parameters.AddWithValue("@dataentrada", DateTime.ParseExact(dataentrada, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("g"));

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        flag = 1;
                        if (ex.HResult == -2146232060)
                        {
                            if (contador_Error < 1)
                            {
                                MessageBox.Show("Há Valores Repetidos nas chaves primarias ou Título já consta no banco. ", "Importação");
                                erroTexto = "Valor repetido / Já consta no banco";
                                // tran.Rollback();
                            }
                            contador_Error++;
                        }
                        else if (flag_error == 1)
                        {
                            MessageBox.Show(ex.Message ,"Importação");
                            contador_Error++;
                        }
                        else if(flag_error == 2)
                        {
                            MessageBox.Show(ex.Message, "Importação");
                            contador_Error++;
                        }
                        else
                        {
                            MessageBox.Show("Erro! " + ex, "Importação");
                            erroTexto = ex.Message;
                            // tran.Rollback();
                        }
                        AppendTextBox(" >>  "+erroTexto);
                     
                    }
                    AppendTextBox(System.Environment.NewLine);
                    contador++;
                    
                    this.backgroundWorker1.ReportProgress(j++ * 100 / files.Length);
                    if (backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker1.ReportProgress(0);
                        return;
                    }
                }
                reader.Close();
            }
            contadorError = contador_Error;
            error = flag;
            backgroundWorker1.ReportProgress(100);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            textBox1.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (error == 0)
            {
                MessageBox.Show("Arquivo importado com sucesso!", "Importação");
                progressBar1.Value = 100;
                tran.Commit();
                sqlCon.Close();
            }
            else
            {
                MessageBox.Show("Arquivo não importado! \nHá " + contadorError + " linhas inválidas", "Importação");
                tran.Rollback();
                sqlCon.Close();
            }

        }
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBox1.AppendText(value);
        }
    }
}