using System;
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
                /*Properties.Settings.Default.ConnectionString = @"Provider=SQLNCLI11;Data Source=.SQLEXPRESS;Password=" + textBox3.Text + @";User ID=" + textBox1.Text + @";Initial Catalog=db_ARS";
                Properties.Settings.Default.Save();*/

                //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //config.AppSettings.Settings["configuration"].Value = @"Provider=SQLNCLI11;Data Source=.SQLEXPRESS;Password=" + textBox3.Text + @";User ID=" + textBox1.Text + @";Initial Catalog=db_ARS";
                //config.Save(ConfigurationSaveMode.Modified);
                //ConfigurationManager.RefreshSection("appSettings");
                try
                {
                    //var XmlDoc = new XmlDocument();

                    //XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    //foreach(XmlElement xElement in XmlDoc.DocumentElement)
                    //{
                    //    if(xElement.Name  == "connectionStrings")
                    //    {
                    //        foreach(XmlNode xNode in xElement.ChildNodes)
                    //        {
                    //            if(xNode.Attributes[1].Name == "connectionString")
                    //            {
                    //                xNode.Attributes[1].Value = @"Provider=SQLNCLI11;Data Source=.SQLEXPRESS;Password=" + textBox3.Text + @";User ID=" + textBox1.Text + @";Initial Catalog=db_ARS";
                    //            }

                    //        }
                    //    }

                    //}
                    //XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    UpdateSetting("Report.Properties.Settings.ConnectionString", @"Provider=SQLNCLI11;Data Source=.SQLEXPRESS;Password=" + textBox3.Text + @";User ID=" + textBox1.Text + @";Initial Catalog=db_ARS");

                MessageBox.Show("Alterado com sucesso!", nameof(Login), MessageBoxButtons.OK, MessageBoxIcon.Information);

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, nameof(Login), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Campo Invalido", nameof(Login), MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

    }
}
