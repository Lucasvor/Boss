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

#pragma warning disable CC0033 // Dispose Fields Properly
        private readonly Connect conexao = new Connect();//add conexao.Dispose(); to the Dispose method on another file.
#pragma warning restore CC0033 // Dispose Fields Properly
        SqlTransaction tran;
        SqlCommand cmd;
        int error;
        int contadorError;
        int j;
        int flag_error;
        int cor;

        const string queryInsert = "insert into dbo.tb_carta(cartorio, protocolo, dataprotocolo, destinatario, docdestinatario, endereco, complemento, bairro, cidade, UF, CEP, nrointimacao, prazolimite,datachamada, dataentrada) values(@cartorio,@protocolo,@dataprotocolo,@destinatario,@docdestinatario,@endereco,@complemento,@bairro,@cidade,@UF,@CEP,@nrointimacao,@prazolimite,@datachamada,@dataentrada)";

        //public SqlConnection SqlCon
        //{
        //    get
        //    {
        //        return sqlCon;
        //    }

        //    set
        //    {
        //        sqlCon = value;
        //    }
        //}

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
            BackRelatorio.Visible = false;

            //panel14.BringToFront();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;

            var Consulta = new Consulta();
            Consulta.ShowDialog();
            Consulta.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            button4.BackColor = System.Drawing.Color.Transparent;
            BackImporta.Visible = false;
            Baixa.Visible = false;
            BackRelatorio.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);

            richTextBox1.Clear();
            BackImporta.Visible = false;
            Baixa.Visible = true;
            BackRelatorio.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;
            //panel13.BringToFront();
            BackImporta.Visible = false;
            Baixa.Visible = false;
            BackRelatorio.Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            progressBar1.Value = 0;
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
            //sqlCon.Open();
            if (connectBanco())
            {

                tran = conexao.SqlCon.BeginTransaction();
                // Váriavel
                string s;
                var flag = 0;
                var contador = 2;
                var contador_Error = 0;
                var lengths = new[] { 2, 4, 8, 45, 14, 45, 20, 20, 20, 2, 8, 13, 8, 8};
                string dataentrada, erroTexto = null, fim = null;
                var parts = new string[lengths.Length];
                var files = File.ReadAllLines((string)e.Argument);
                //


                using (var reader = new StreamReader((string)e.Argument, Encoding.UTF7))
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

                        try
                        {
                            if (s.Trim().Length < 210 || s.Substring(9, 226).Equals("                                                                                                                                                                                                                                  "))
                            {
                                if (fim == s.Substring(0, 8))
                                {
                                    if (flag == 0)
                                    {
                                        AppendTextBox("  Operação Concluída com Sucesso!");
                                        break;
                                    }
                                    else
                                    {
                                        AppendTextBox("  Operação Concluída com Falha!");
                                        break;
                                    }
                                }
                                else
                                {
                                    flag_error = 1;
                                    erroTexto = " Falta dados necessários para importação! Linha: " + contador;
                                    throw new Exception(erroTexto);
                                }
                            }

                            for (int i = 0; i < lengths.Length; i++)
                            {
                                parts[i] = s.Substring(startPos, lengths[i]);
                                startPos += lengths[i];
                            }
                            if (parts[0].Trim().Length < 2 && parts[11].Trim().Length < 13)
                            {
                                flag_error = 2;
                                erroTexto = " Dado inválido para chave Primária do Banco!";
                                throw new Exception(erroTexto);
                            }

                            cmd = new SqlCommand(queryInsert, conexao.SqlCon, tran);
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
                            cmd.Parameters.AddWithValue("@datachamada", DateTime.ParseExact(parts[13], "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("g"));
                            cmd.Parameters.AddWithValue("@dataentrada", DateTime.ParseExact(dataentrada, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("g"));

                            cmd.ExecuteNonQuery();
                        }
                        catch(SqlException exq)
                        {
                            flag = 1;
                            if (exq.Number == 2627)
                            {
                                if (contador_Error < 1)
                                {
                                    MessageBox.Show("Há Valores Repetidos nas chaves primarias ou AR já consta no banco. ", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    erroTexto = "Valor repetido / Já consta no banco";
                                    // tran.Rollback();
                                }
                                erroTexto = "Valor repetido / Já consta no banco";
                                contador_Error++;
                            }
                            else
                            {
                                MessageBox.Show("Erro! " + exq, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                erroTexto = exq.Message;
                                // tran.Rollback();
                            }
                            AppendTextBox(" >>  " + erroTexto);
                        }
                        catch (Exception ex)
                        {
                            flag = 1;
                                if (flag_error == 1)
                                {
                                    MessageBox.Show(ex.Message, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    contador_Error++;
                                }
                                else if (flag_error == 2)
                                {
                                    MessageBox.Show(ex.Message, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    contador_Error++;
                                }
                                else
                                {
                                    MessageBox.Show("Erro! " + ex, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    erroTexto = ex.Message;
                                    // tran.Rollback();
                                }
                                AppendTextBox(" >>  " + erroTexto);

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
                MessageBox.Show("Arquivo importado com sucesso!", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressBar1.Value = 100;
                tran.Commit();
                conexao.SqlCon.Close();
            }
            else if(error == 1)
            {
                MessageBox.Show("Arquivo não importado! \nHá " + contadorError + " linhas inválidas", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback();
                conexao.SqlCon.Close();
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

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (connectBanco())
                {
                    if (string.IsNullOrWhiteSpace(textBox2.Text) || textBox2.Text.Length > 14)
                    {
                        MessageBox.Show("Campo não pode ser vazio ou conter mais que 13 Caracteres", nameof(Baixa), MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        textBox2.Clear();
                        conexao.SqlCon.Close();
                    }
                    else
                    {
                        try
                        {
                            //richTextBox1.Text += textBox2.Text+System.Environment.NewLine;
                            //richTextBox1.AppendText(textBox2.Text + System.Environment.NewLine);

                            using (var myCommand = new SqlCommand("select baixa from db_ARS.dbo.tb_carta where nrointimacao = '" + textBox2.Text + "'", conexao.SqlCon))
                            {
                                myCommand.ExecuteNonQuery();
                                var myReader = myCommand.ExecuteReader();
                                if (myReader.HasRows)
                                {

                                    if (myReader.Read() && myReader.IsDBNull(0))
                                    {
                                        myCommand.Dispose();
                                        myReader.Close();
                                        using (var mycommand2 = new SqlCommand("update dbo.tb_carta set baixa = '" + DateTime.Now + "' where nrointimacao = '" + textBox2.Text + "'", conexao.SqlCon))
                                        {
                                            mycommand2.ExecuteNonQuery();
                                            richTextBox1.Select(0, 0);
                                            richTextBox1.SelectedText = textBox2.Text + "  " + DateTime.Now + System.Environment.NewLine;
                                            richTextBox1.Update();
                                            //richTextBox1.SelectAll();
                                            //richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                                            textBox2.Clear();
                                            mycommand2.Dispose();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Valor já baixado", nameof(Baixa), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        textBox2.SelectAll();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Dado não encontado", nameof(Baixa), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    textBox2.SelectAll();
                                }

                            }

                        }catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            conexao.SqlCon.Close();
                        }

                    }
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.Update();
            if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                switch (cor)
                {
                    case 0:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.WhiteSmoke;
                        cor++;
                        break;
                    case 1:

                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.Gainsboro;
                        cor++;
                        break;
                    case 2:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.LightGray;
                        cor++;
                        break;
                    case 3:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.Silver;
                        cor++;
                        break;
                    case 4:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.DarkGray;
                        cor++;
                        break;
                    case 5:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.Gray;
                        cor++;
                        break;
                    case 6:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.DimGray;
                        cor++;
                        break;
                    case 7:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.Gray;
                        cor++;
                        break;
                    case 8:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.DarkGray;
                        cor++;
                        break;
                    case 9:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.Silver;
                        cor++;
                        break;
                    case 10:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.LightGray;
                        cor++;
                        break;
                    case 11:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.LightGray;
                        cor++;
                        break;
                    case 12:
                        //richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 1), richTextBox1.Lines[richTextBox1.Lines.Length - 1].Length);
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(0), richTextBox1.Lines[0].Length);
                        richTextBox1.SelectionColor = Color.WhiteSmoke;
                        cor++;
                        break;
                    default:
                        cor = 0;
                        break;
                }
            }
        }

        private static void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
        public bool connectBanco()
        {

            try
            {

                conexao.SqlCon.Open();
                return true;

            }catch(Exception e)
            {
                MessageBox.Show(e.Message, "Conexão com Banco de dados",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }

        private void Sistema_FormClosed(object sender, FormClosedEventArgs e)
        {
            conexao.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            //var checkedButton = groupBox1.Controls.OfType<RadioButton>()
            //                          .FirstOrDefault(r => r.Checked);
            //switch (checkedButton.Text)
            //{
            //    case "Baixas não realizadas":
            //        rel.Op = 1;
            //        break;
            //    case "Segunda Opção":
            //        rel.Op = 2;
            //        break;
            //}

            if ((!radioButton1.Checked) && (!radioButton2.Checked) && (!radioButton3.Checked) && (!radioButton4.Checked))
            {
                MessageBox.Show("Escolha pelo menos uma opção!", "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                var rel = new Banco_de_Dados.Relatorio.FormRelatorio
                {
                    Aux = radioButton3.Checked ? textBox3.Text : radioButton2.Checked ? textBox3.Text : null,
                    Op = radioButton1.Checked ? 1 : radioButton2.Checked ? 2 : radioButton3.Checked ? 3 : radioButton4.Checked ? 4 : 0
                };
                if ((radioButton3.Checked && string.IsNullOrWhiteSpace(textBox3.Text)) || radioButton2.Checked && string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Coloque um dado valido", nameof(Baixa), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    rel.ShowDialog();
                    rel.Dispose();
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                //0; 55; 0; 0
                PanelRigGRoupBox.Padding = new Padding(0, 110, 0, 0);
                label1.Text = "Data Protocolo :";
                label1.Visible = true;
                textBox3.Visible = true;
                textBox3.Focus();
            }else
            {
                label1.Visible = false;
                textBox3.Visible = false;
                textBox3.Clear();
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                PanelRigGRoupBox.Padding = new Padding(0, 55, 0, 0);
                label1.Text = "Data Baixa :";
                label1.Visible = true;
                textBox3.Visible = true;
                textBox3.Focus();
            }else
            {
                label1.Visible = false;
                textBox3.Visible = false;
                textBox3.Clear();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {

                    button6.PerformClick();
                    textBox3.Clear();
                    button6.Focus();
                }
            }
        }


    }
}