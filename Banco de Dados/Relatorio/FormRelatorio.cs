﻿using System;
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

        private void FormRelatorio_Load(object sender, EventArgs e)
        {
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
            pg.Margins = new System.Drawing.Printing.Margins(2, 2, 2, 2);
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


        }
    }
}