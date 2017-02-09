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
            textBox1.Text = nameof(Login);
            textBox2.Text = "Senha";
            textBox3.Text = "Data Source";
            
    }

    
        protected override void OnLoad(EventArgs e)
        {
            var btn = new PictureBox();
            btn.Size = new System.Drawing.Size(25, textBox1.ClientSize.Height + 2);
            btn.Location = new System.Drawing.Point(textBox1.ClientSize.Width - btn.Width, -1);
            btn.Dock = DockStyle.Right;
            btn.Cursor = Cursors.Default;
            btn.BackColor = System.Drawing.SystemColors.ControlDark;
            btn.Image = (Properties.Resources.User_48);
            textBox1.Controls.Add(btn);
            SendMessage(textBox1.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn.Width << 16));
            base.OnLoad(e);
            Two(e);
            
        }
       

        protected void Two(EventArgs e)
        {

            var btn = new PictureBox();
            btn.Size = new System.Drawing.Size(25, textBox1.ClientSize.Height + 2);
            btn.Location = new System.Drawing.Point(textBox1.ClientSize.Width - btn.Width, -1);
            btn.Dock = DockStyle.Right;
            btn.Cursor = Cursors.Default;
            btn.Padding = new Padding(2, 2, 0, 0);
            btn.BackColor = System.Drawing.SystemColors.ControlDark;
            btn.Image = (Properties.Resources.Key_48);
            textBox2.Controls.Add(btn);
            SendMessage(textBox1.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn.Width << 16));
            base.OnLoad(e);
            Three(e);

        }
        protected void Three (EventArgs e)
        {
            var btn = new PictureBox();
            btn.Size = new System.Drawing.Size(25, textBox1.ClientSize.Height + 2);
            btn.Location = new System.Drawing.Point(textBox1.ClientSize.Width - btn.Width, -1);
            btn.Dock = DockStyle.Right;
            btn.Cursor = Cursors.Default;
            //btn.Padding = new Padding(2, 2, 0, 0);
            btn.BackColor = System.Drawing.SystemColors.ControlDark;
            btn.Image = (Properties.Resources.database_icon);
            textBox3.Controls.Add(btn);
            SendMessage(textBox1.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn.Width << 16));
            base.OnLoad(e);
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void button6_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                
                try
                {
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                    if(textBox3.Text.Length == 0 || textBox3.Text.Equals("Data Source"))
                    {
                        textBox3.Text = ".\\SQLEXPRESS";
                    }
                    connectionStringsSection.ConnectionStrings[1].ConnectionString = "Provider=SQLNCLI11;Data Source="+textBox3.Text+";Initial Catalog=db_ARS;User ID=" + textBox1.Text + ";Password=" + textBox2.Text;
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

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Senha")
            {
                textBox2.Text = "";
                textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                textBox2.Text = "Senha";
                textBox2.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Data Source")
            {
                textBox3.Text = "";
                textBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {
                textBox3.Text = "Data Source";
                textBox3.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }
    }
}
