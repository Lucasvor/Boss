using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Report
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                /*Properties.Settings.Default.ConnectionString = @"Provider=SQLNCLI11;Data Source=.SQLEXPRESS;Password=" + textBox3.Text + @";User ID=" + textBox1.Text + @";Initial Catalog=db_ARS";
                Properties.Settings.Default.Save();*/

                //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //config.AppSettings.Settings["configuration"].Value = @"Provider=SQLNCLI11;Data Source=.SQLEXPRESS;Password=" + textBox3.Text + @";User ID=" + textBox1.Text + @";Initial Catalog=db_ARS";
                //config.Save(ConfigurationSaveMode.Modified);
                //ConfigurationManager.RefreshSection("appSettings");
                try
                {
                    var xml = XDocument.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    var root = xml.Elements("configuration").Single();
                    var sb = root.FirstNode.NextNode;
                   
                    
                    root.Value = "<configSections> </configSections> <connectionStrings> <add name=\"Report.Properties.Settings.ConnectionString\" connectionString =\"Provider=SQLNCLI11;Data Source=.\\SQLEXPRESS;Password=" + textBox3.Text + ";User ID=" + textBox1.Text + ";Initial Catalog=db_ARS\" providerName =\"System.Data.SqlClient\" /> </ connectionStrings >";
                    xml.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    MessageBox.Show("Alterado com sucesso!", nameof(Login), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, nameof(Login), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Campo Invalido", nameof(Login), MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
