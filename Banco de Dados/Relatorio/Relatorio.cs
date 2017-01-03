using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco_de_Dados.Relatorio
{
    public partial class Relatorio : Form
    {
        public Relatorio()
        {
            InitializeComponent();
        }

        private int op;

        public int Op
        {
            get
            {
                return op;
            }

            set
            {
                op = value;
            }
        }

        private void Relatorio_Load(object sender, EventArgs e)
        {
            try
            {
                if (Op == 1)
                {
                    // TODO: This line of code loads data into the 'dBars.EntregadorComBaixa' table. You can move, or remove it, as needed.

                    this.entregadorComBaixaTableAdapter.Fill(this.dBars.EntregadorComBaixa);
                    this.reportViewer1.RefreshReport();
                }
                else if (Op == 2)
                {

                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.BaixaHoje.rdlc";
                    this.baixaHojeBindingSource.DataMember = "BaixaHoje";
                    this.baixaHojeBindingSource.DataSource = this.dBars;
                    this.baixaHojeTableAdapter.ClearBeforeFill = true;
                    this.baixaHojeTableAdapter.Fill(this.dBars.BaixaHoje);


                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    this.Dispose();
                    this.Close();
                }
                //this.reportViewer1.
                System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
                pg.Landscape = true;

                //set paper size
                //System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize();
                pg.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);
                pg.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;

                this.reportViewer1.SetPageSettings(pg);
                //size.RawKind = (int)System.Drawing.Printing.PaperKind.A5;
                //pg.PaperSize = size;
                //reportViewer1.SetPageSettings(pg);

                this.reportViewer1.RefreshReport();
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.Normal);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
           
        }
    }
}
