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

namespace Banco_de_Dados
{
    public partial class Consulta : Form
    {
        //SqlConnection sqlCon = new SqlConnection(Connect.query);
        private readonly Connect conexao = new Connect();//add conexao.Dispose(); to the Dispose method on another file.//add conexao.Dispose(); to the Dispose method on another file.
        DataTable dt;

        SqlDataAdapter da;
        SqlCommand cmd;
        SqlCommandBuilder scb;


        string query;
        string dado;
        int flagButton;


        public Consulta()
        {
            InitializeComponent();
            GetdatabaseList();
            this.button5.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Plus));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (connectBanco())
            {
                if ((!string.IsNullOrWhiteSpace(comboBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)) && string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    query = "select * from dbo.tb_carta where " + comboBox1.Text + " like '" + textBox1.Text + "%'";
                }
                else if ((!string.IsNullOrWhiteSpace(comboBox1.Text) && string.IsNullOrWhiteSpace(textBox1.Text)) && string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    query = "select " + comboBox1.Text + " from dbo.tb_carta";
                }
                else if ((!string.IsNullOrWhiteSpace(comboBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)) && !string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    query = "select * from dbo.tb_carta where " + comboBox1.Text + " = '" + textBox1.Text + "' and " + comboBox2.Text + " like '" + textBox2.Text + "%'";
                }
                /* if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
                 {
                     query = "select * from dbo.tb_carta where nrointimacao = '" + textBox1.Text + "' AND cartorio = '"+comboBox1.Text+"'";
                 }else if(string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
                 {
                     query = "select * from dbo.tb_carta where cartorio = '" + comboBox1.Text + "'";
                 }else if (string.IsNullOrWhiteSpace(comboBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                 {
                     query = "select * from dbo.tb_carta where nrointimacao = '" + textBox1.Text + "'";
                 } */
                else
                {
                    query = "select top(200) * from dbo.tb_carta";
                }
                try
                {
                    //sqlCon.Open();
                    cmd = new SqlCommand(query, conexao.SqlCon);
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count < 1)
                    {
                        throw new Exception("Dado não Encontrado!");
                    }
                    else
                    {

                        dataGridView1.DataSource = dt;
                        configuraDataGridView();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Consulta");
                }
                finally
                {
                    conexao.SqlCon.Close();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt.Clear();
        }

        public void configuraDataGridView()
        {
            dataGridView1.Columns[0].HeaderText = "Num. Cartório";
            dataGridView1.Columns[1].HeaderText = "Protocolo";
            dataGridView1.Columns[2].HeaderText = "Data Protocolo";
            dataGridView1.Columns[3].HeaderText = "Destinatário";
            dataGridView1.Columns[4].HeaderText = "CPF / CNPJ";
            dataGridView1.Columns[5].HeaderText = "Endereço";
            dataGridView1.Columns[6].HeaderText = "Complemento";
            dataGridView1.Columns[7].HeaderText = "Bairro";
            dataGridView1.Columns[8].HeaderText = "Cidade";
            dataGridView1.Columns[9].HeaderText = "UF";
            dataGridView1.Columns[10].HeaderText = "CEP";
            dataGridView1.Columns[11].HeaderText = "Num. Intimação";
            dataGridView1.Columns[12].HeaderText = "Prazo Limite";
            dataGridView1.Columns[13].HeaderText = "Baixa do Título";
            dataGridView1.Columns[14].HeaderText = "Arquivo Gerado Em";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            scb = new SqlCommandBuilder(da);
            if(MessageBox.Show("Tem Certeza ?","Confirmação",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                da.Update(dt);
            }
            else{
                dt.Clear();
                da.Fill(dt);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                if (MessageBox.Show("Tem Certeza ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("delete from dbo.tb_carta where nrointimacao = @nrointimacao",conexao.SqlCon);
                    cmd.Parameters.AddWithValue("@nrointimacao", dado);
                    try
                    {
                        conexao.SqlCon.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Operação feita com sucesso","Excluir");
                        dt.Clear();
                        da.Fill(dt);

                    }
                    catch(Exception ed)
                    {
                        MessageBox.Show("Error : " + ed, "Excluir");
                    }
                    finally
                    {
                        conexao.SqlCon.Close();
                    }
                }
            }else
            {
                MessageBox.Show("Viewer Vazio","Alteração");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                dado = selectedRow.Cells[11].Value.ToString();
            }
        }

        public void GetdatabaseList()
        {
            //conexao.SqlCon.Open();
            if (connectBanco())
            {

                using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tb_carta')", conexao.SqlCon))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            comboBox1.Items.Add(dr[0].ToString());
                            comboBox2.Items.Add(dr[0].ToString());
                        }
                    }
                }

                conexao.SqlCon.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (flagButton == 0)
            {
                comboBox2.Visible = true;
                textBox2.Visible = true;
                button5.BackgroundImage = (System.Drawing.Bitmap)(Properties.Resources.Minus);
                flagButton = 1;
            }else
            {
                comboBox2.Visible = false;
                textBox2.Visible = false;
                comboBox2.ResetText();
                flagButton = 0;
                textBox2.Clear();
                button5.BackgroundImage = (System.Drawing.Bitmap)(Properties.Resources.Plus);

            }
        }

        private void Consulta_Load(object sender, EventArgs e)
        {

        }
        public bool connectBanco()
        {

            try
            {

                conexao.SqlCon.Open();
                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Conexão com Banco de dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


    }

}

