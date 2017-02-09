﻿using System;
using System.Configuration;
using System.Windows.Forms;

namespace Report
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }
        private static void UpdateSetting(string key, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
            configuration.AppSettings.Settings[key].Value = value;

            ConfigurationManager.RefreshSection("connectionStrings");
            configuration.Save();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                
                try
                {
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                    connectionStringsSection.ConnectionStrings[1].ConnectionString = "Provider=SQLNCLI11;Data Source=.\\SQLEXPRESS;Initial Catalog=db_ARS;User ID=" + textBox3.Text + ";Password=" + textBox1.Text;
                    //config.ConnectionStrings.SectionInformation.ProtectSection(); Provider=SQLNCLI11;Data Source=.\SQLEXPRESS;Initial Catalog=db_ARS;User ID=sa;Password=Lucas123
                    config.Save();
                    ConfigurationManager.RefreshSection("connectionStrings");

                    //UpdateSetting("Report.Properties.Settings.ConnectionString", @"Provider=SQLNCLI11;Data Source=.SQLEXPRESS;Password=" + textBox3.Text + @";User ID=" + textBox1.Text + @";Initial Catalog=db_ARS");

                    MessageBox.Show("Alterado com sucesso!", nameof(Login), MessageBoxButtons.OK, MessageBoxIcon.Information);

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, nameof(Login), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Campo Invalido", nameof(Login), MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == nameof(Login))
            {
                textBox1.Text = "";
                textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }
       private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0)
            {
                textBox1.Text = nameof(Login);
                textBox1.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Senha")
            {
                textBox3.Text = "";
                textBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {
                textBox3.Text = "Senha";
                textBox3.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
    }
}
