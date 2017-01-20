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
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
#pragma warning disable CC0033 // Dispose Fields Properly
        private readonly Connect conexao = new Connect();//add conexao.Dispose(); to the Dispose method on another file.
#pragma warning restore CC0033 // Dispose Fields Properly

        public FormRelatorio()
        {

            InitializeComponent();

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


        public int Op { get; set; }

        public string Aux { get; set; }

        public DateTime date { get; set; }

        private void FormRelatorio_Load(object sender, EventArgs e)
        {
            if (connectBanco())
            {
                try
                {
                    reportDataSource1.Name = "DataSet1";
                    switch (Op)
                    {
                        case 1:
                            reportDataSource1.Value = this.getdataprazoBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.NBaixa.rdlc";
                            var dataprazo = new Microsoft.Reporting.WinForms.ReportParameter("Data", date.ToString("dd/MM/yyyy"));
                            this.reportViewer1.LocalReport.SetParameters(dataprazo);
                            this.getdataprazoTableAdapter.Fill(this.dBars.Getdataprazo,date);
                            break;
                        case 2:
                            reportDataSource1.Value = this.getdatabaixaBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.BaixaHoje.rdlc";
                            var baixa = new Microsoft.Reporting.WinForms.ReportParameter("dataBaixa", Aux);
                            this.reportViewer1.LocalReport.SetParameters(baixa);
                            this.getdatabaixaTableAdapter.Fill(this.dBars.Getdatabaixa,Aux);
                            break;
                        case 3:
                            reportDataSource1.Value = this.getDataBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.BaixasPar.rdlc";
                            var get = new Microsoft.Reporting.WinForms.ReportParameter("Data", Aux);
                            this.reportViewer1.LocalReport.SetParameters(get);
                            this.getDataTableAdapter.Fill(this.dBars.GetData, Aux);
                            break;
                        case 4:
                            reportDataSource1.Value = this.EntradaHojeBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.TitBaixaHoje.rdlc";
                            this.entradaHojeTableAdapter.Fill(this.dBars.EntradaHoje);
                            break;
                        default:
                            this.Dispose();
                            this.Close();
                            break;
                    }

                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                //this.reportViewer1.

                var pg = new System.Drawing.Printing.PageSettings
                {
                    Margins = new System.Drawing.Printing.Margins(36, 2, 2, 2),
                    Landscape = true
                };

                this.reportViewer1.SetPageSettings(pg);
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                this.reportViewer1.RefreshReport();
            }
        }
    }
}
