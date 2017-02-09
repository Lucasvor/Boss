using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report.Relatorio
{
    public partial class FormRelatorio : Form
    {
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2");
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

        public string Nome { get; set; }

        public DateTime data { get; set; }

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
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Report.Relatorio.NBaixa.rdlc";
                            var dataprazo = new Microsoft.Reporting.WinForms.ReportParameter("Data", Aux);
                            this.reportViewer1.LocalReport.SetParameters(dataprazo);
                            this.getdataprazoTableAdapter.Fill(this.dBars.Getdataprazo,Aux);
                            break;
                        case 2:
                            reportDataSource1.Value = this.getnomebaixaBindingSource;
                            reportDataSource2.Value = this.quantidadeBaixaBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Report.Relatorio.BaixaHoje.rdlc";
                            var baixa = new Microsoft.Reporting.WinForms.ReportParameter("Data", Aux);
                            var nome = new Microsoft.Reporting.WinForms.ReportParameter(nameof(Nome), Nome);
                            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { baixa, nome });
                            this.getnomebaixaTableAdapter.Fill(this.dBars.Getnomebaixa,Aux,Nome);
                            this.quantidadeBaixaTableAdapter.Fill(this.dBars.QuantidadeBaixa, Aux);
                            break;
                        case 3:
                            reportDataSource1.Value = this.getDataBindingSource;
                            reportDataSource2.Value = this.quantidadeBaixaBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Report.Relatorio.BaixasPar.rdlc";
                            var qnt = new Microsoft.Reporting.WinForms.ReportParameter("Quantidade", Aux);
                            var get = new Microsoft.Reporting.WinForms.ReportParameter("Data", Aux);
                            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { qnt,get});
                            this.getDataTableAdapter.Fill(this.dBars.GetData, Aux);
                            this.quantidadeBaixaTableAdapter.Fill(this.dBars.QuantidadeBaixa, Aux);
                            break;
                        case 4:
                            reportDataSource1.Value = this.getImportaBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Report.Relatorio.TitBaixaHoje.rdlc";
                            var Data = new Microsoft.Reporting.WinForms.ReportParameter("Data", data.ToShortDateString());
                            this.reportViewer1.LocalReport.SetParameters(Data);
                            this.getImportaTableAdapter.Fill(this.dBars.GetImporta,data);
                            break;
                        default:
                            this.Dispose();
                            this.Close();
                            break;
                    }

                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                var pg = new System.Drawing.Printing.PageSettings
                {
                    Margins = new System.Drawing.Printing.Margins(36, 2, 10, 2),
                    Landscape = true
                };
                this.reportViewer1.SetPageSettings(pg);
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
