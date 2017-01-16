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
    public partial class FormRelatorio : Form
    {
        private int op;
        private string aux;
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
        
        public FormRelatorio()
        {

            InitializeComponent();
            
        }
        

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

        public string Aux
        {
            get
            {
                return aux;
            }

            set
            {
                aux = value;
            }
        }

        private void FormRelatorio_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBars.EntradaHoje' table. You can move, or remove it, as needed.
            this.entradaHojeTableAdapter.Fill(this.dBars.EntradaHoje);
            try
            {
                reportDataSource1.Name = "DataSet1";
                if (Op == 1)
                {
                    reportDataSource1.Value = this.entregadorComBaixaBindingSource;
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.NBaixa.rdlc";
                    this.entregadorComBaixaTableAdapter.Fill(this.dBars.EntregadorComBaixa);
                    
                }
                else if (Op == 2)
                {
                    reportDataSource1.Value = this.baixaHojeBindingSource;
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.BaixaHoje.rdlc";
                    this.baixaHojeTableAdapter.Fill(this.dBars.BaixaHoje);
                }else if(Op == 3)
                {
                    reportDataSource1.Value = this.getDataBindingSource;
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.BaixasPar.rdlc";
                    Microsoft.Reporting.WinForms.ReportParameter get = new Microsoft.Reporting.WinForms.ReportParameter("Data", Aux);
                    this.reportViewer1.LocalReport.SetParameters(get);
                    this.getDataTableAdapter.Fill(this.dBars.GetData, Aux);
                }else if(Op == 4)
                {
                    reportDataSource1.Value = this.EntradaHojeBindingSource;
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.TitBaixaHoje.rdlc";
                    this.entradaHojeTableAdapter.Fill(this.dBars.EntradaHoje);

                }
                else
                {
                    this.Dispose();
                    this.Close();
                }
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            //this.reportViewer1.
            
            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins = new System.Drawing.Printing.Margins(36, 2, 2, 2);
            pg.Landscape = true;
            //System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize();
            

            ////set paper size
            ////System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize();
            //// pg.PaperSize = new System.Drawing.Printing.PaperSize("A4",)
            //pg.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;

            //this.reportViewer1.SetPageSettings(pg);
            ////size.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            //pg.PaperSize = size;
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

            this.reportViewer1.RefreshReport();

            this.reportViewer1.RefreshReport();
        }
    }
}
