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

namespace Report
{
    public partial class Sistema : Form
    {

#pragma warning disable CC0033 // Dispose Fields Properly
        private readonly Connect conexao = new Connect();//add conexao.Dispose(); to the Dispose method on another file.
#pragma warning restore CC0033 // Dispose Fields Properly
        //variáveis do sistema
        SqlTransaction tran;
        SqlCommand cmd;
        int error;
        int contadorError;
        int j;
        int flag_error;
        int cor;
        //variáveis do sistema

        //string de insirir valores
        const string queryInsert = "insert into dbo.tb_carta(cartorio, protocolo, dataprotocolo, digitoProtoloco, destinatario, docdestinatario, endereco, complemento, bairro, cidade, UF, CEP, nrointimacao, prazolimite,datachamada, dataentrada) values(@cartorio,@protocolo,@dataprotocolo,@digitoProtoloco,@destinatario,@docdestinatario,@endereco,@complemento,@bairro,@cidade,@UF,@CEP,@nrointimacao,@prazolimite,@datachamada,@dataentrada)";
        //string de insirir valores

        public Sistema()
        {
            InitializeComponent();
            //Pega nome dos entregadores e preencher combo box
            GetdatabaseList();
            //
        }
        //Botões da coluna da esquerda

        private void button1_Click_1(object sender, EventArgs e)
        {

            button1.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;
            BackImporta.Visible = true;
            Baixa.Visible = false;
            BackRelatorio.Visible = false;


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
            textBox2.Focus();
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

        //Botões da coluna da esquerda
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            progressBar1.Value = 0;
            j = 0;
            using (OpenFileDialog ofd = new OpenFileDialog { Title = "Escolha um Arquivo TXT", Filter = "Arquivo Texto (*.txt)|*.txt|All Files(*.*)|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    backgroundWorker1.RunWorkerAsync(ofd.FileName);
                }
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {  /// Connect database
            if (connectBanco())
            {
                tran = conexao.SqlCon.BeginTransaction();
                // Váriavel
                string s;
                var flag = 0;
                var contador = 2;
                var contador_Error = 0;
                var lengths = new[] { 2, 4 ,8, 2, 45, 14, 45, 20, 20, 20, 2, 8, 13, 8, 8};
                string dataentrada, erroTexto = null, fim = null;
                var parts = new string[lengths.Length];
                var files = File.ReadAllLines((string)e.Argument);


                using (var reader = new StreamReader((string)e.Argument, Encoding.UTF7))
                {
                    dataentrada = reader.ReadLine();
                    fim = dataentrada = dataentrada.Substring(0, 8);
                    while (!reader.EndOfStream)
                    {
                        s = reader.ReadLine();
                        AppendTextBox("Importando Linha: " + (contador));
                        var startPos = 0;
                        try
                        {
                            if (s.Length > 240)
                            {
                                flag_error = 3;
                                erroTexto = " Linha com caracteres acima de 240 ";
                                throw new Exception(erroTexto);
                            }
                            if(s.Length < 240)
                            {
                                flag_error = 3;
                                erroTexto = " Linha com caracteres abaixo de 240";
                                throw new Exception(erroTexto);
                            }
                            if (s.Trim().Length < 210 || s.Substring(9, 226).Equals("                                                                                                                                                                                                                                  ") || s.Substring(17, 218).Equals("                                                                                                                                                                                                                          "))
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
                            if (parts[0].Trim().Length < 2 && parts[12].Trim().Length < 13)
                            {
                                flag_error = 2;
                                erroTexto = " Dado inválido para chave Primária do Banco!";
                                throw new Exception(erroTexto);
                            }

                            cmd = new SqlCommand(queryInsert, conexao.SqlCon, tran);
                            cmd.Parameters.AddWithValue("@cartorio",                        parts[0]);
                            cmd.Parameters.AddWithValue("@protocolo",                       parts[1]);
                            cmd.Parameters.AddWithValue("@dataprotocolo",                   parts[2]);
                            cmd.Parameters.AddWithValue("@digitoProtoloco",                 parts[3]);
                            cmd.Parameters.AddWithValue("@destinatario",                    parts[4]);
                            cmd.Parameters.AddWithValue("@docdestinatario",                 parts[5]);
                            cmd.Parameters.AddWithValue("@endereco",                        parts[6]);
                            cmd.Parameters.AddWithValue("@complemento",                     parts[7]);
                            cmd.Parameters.AddWithValue("@bairro",                          parts[8]);
                            cmd.Parameters.AddWithValue("@cidade",                          parts[9]);
                            cmd.Parameters.AddWithValue("@UF",                              parts[10]);
                            cmd.Parameters.AddWithValue("@CEP",                             parts[11]);
                            cmd.Parameters.AddWithValue("@nrointimacao",                    parts[12]);
                            cmd.Parameters.AddWithValue("@datachamada", DateTime.ParseExact(parts[13], "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("g"));
                            cmd.Parameters.AddWithValue("@prazolimite", DateTime.ParseExact(parts[14], "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("g"));
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
                                }
                                erroTexto = "Valor repetido / Já consta no banco";
                                contador_Error++;
                            }
                            else
                            {
                                MessageBox.Show("Erro! " + exq, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                erroTexto = exq.Message;
                            }
                            AppendTextBox(" >>  " + erroTexto);
                        }
                        catch (Exception ex)
                        {
                            flag = 1;
                            switch (flag_error)
                            {
                                case 1:
                                    MessageBox.Show(ex.Message, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    contador_Error++;
                                    break;
                                case 2:
                                    MessageBox.Show(ex.Message, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    contador_Error++;
                                    break;
                                case 3:
                                    MessageBox.Show(ex.Message, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    contador_Error++;
                                    break;
                                default:
                                    MessageBox.Show("Erro! " + ex, "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    erroTexto = ex.Message;
                                    break;
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
                                        textBox2.SelectAll();
                                        throw new Exception("Valor já baixado");
                                            //MessageBox.Show("Valor já baixado", nameof(Baixa), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            
                                    }
                                }
                                else
                                {
                                    myCommand.Dispose();
                                    myReader.Close();
                                    using (var log = new SqlCommand("insert into dbo.Log (Descricao,Numerointimacao,Data) values('Dado não encontrado','"+textBox2.Text+"','"+DateTime.Now+"')",conexao.SqlCon))
                                    {
                                        log.ExecuteNonQuery();
                                        textBox2.SelectAll();
                                        throw new Exception("Dado não encontado");
                                        //MessageBox.Show("Dado não encontado", nameof(Baixa), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                                    }
                                }

                            }

                        }catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, nameof(Baixa), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

#pragma warning disable CC0068 // Unused Method
        private static void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
#pragma warning restore CC0068 // Unused Method
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
            if ((!radioButton1.Checked) && (!radioButton2.Checked) && (!radioButton3.Checked) && (!radioButton4.Checked))
            {
                MessageBox.Show("Escolha pelo menos uma opção!", "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                // : radioButton4.Checked ? dateTimePicker1.Value.Date.ToString("dd/MM/yyyy")
                var rel = new Report.Relatorio.FormRelatorio
                {
                    Aux = radioButton3.Checked ? textBox3.Text : radioButton2.Checked ? textBox3.Text : radioButton1.Checked ? textBox3.Text : null,
                    Op = radioButton1.Checked ? 1 : radioButton2.Checked ? 2 : radioButton3.Checked ? 3 : radioButton4.Checked ? 4 : 0,
                    Nome = radioButton2.Checked ? comboBox1.Text : null,
                    data = radioButton4.Checked ? dateTimePicker1.Value : dateTimePicker1.Value

                };
                if ((radioButton3.Checked && string.IsNullOrWhiteSpace(textBox3.Text)) || radioButton2.Checked && string.IsNullOrWhiteSpace(textBox3.Text) && string.IsNullOrWhiteSpace(comboBox1.Text) || radioButton2.Checked && string.IsNullOrWhiteSpace(textBox3.Text) || radioButton2.Checked && string.IsNullOrWhiteSpace(comboBox1.Text) || radioButton1.Checked && string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Coloque um dado valido", nameof(Baixa), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox3.Focus();
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
                label1.Text = "Data Protocolo:";
                label1.Visible = true;
                label4.Visible = true;
                comboBox1.Visible = true;
                textBox3.Visible = true;
                textBox3.Focus();
            }else
            {
                label4.Visible = false;
                comboBox1.Visible = false;
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                PanelRigGRoupBox.Padding = new Padding(0,0, 0, 0);
                label1.Text = "Data Protocolo :";
                label1.Visible = true;
                textBox3.Visible = true;
                textBox3.Focus();
            }
            else
            {
                label1.Visible = false;
                textBox3.Visible = false;
                textBox3.Clear();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var a = new Login();
            a.ShowDialog();
            a.Dispose();
            conexao.Dispose();
            
        }
        public void GetdatabaseList()
        {
            try
            {
                if (connectBanco())
                {
                    using (SqlCommand cmd = new SqlCommand("select NomeCompleto from cad_entregador", conexao.SqlCon))
                    {

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                comboBox1.Items.Add(dr[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "Fechando o Programa", "Preencher ComboBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.SqlCon.Close();
            }

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                PanelRigGRoupBox.Padding = new Padding(0, 170, 0, 0);
                label1.Text = "Data da Importação:";
                label1.Visible = true;
                dateTimePicker1.Visible = true;
            }else
            {
                label1.Visible = false;
                dateTimePicker1.Visible = false;
            }

        }
        
    }
}