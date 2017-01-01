namespace Banco_de_Dados.Relatorio
{
    partial class Relatorio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Relatorio));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dBars = new Banco_de_Dados.DBars();
            this.entregadorComBaixaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entregadorComBaixaTableAdapter = new Banco_de_Dados.DBarsTableAdapters.EntregadorComBaixaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dBars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entregadorComBaixaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.IsDocumentMapWidthFixed = true;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.entregadorComBaixaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.NBaixa.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(909, 675);
            this.reportViewer1.TabIndex = 0;
            // 
            // dBars
            // 
            this.dBars.DataSetName = "DBars";
            this.dBars.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // entregadorComBaixaBindingSource
            // 
            this.entregadorComBaixaBindingSource.DataMember = "EntregadorComBaixa";
            this.entregadorComBaixaBindingSource.DataSource = this.dBars;
            // 
            // entregadorComBaixaTableAdapter
            // 
            this.entregadorComBaixaTableAdapter.ClearBeforeFill = true;
            // 
            // Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 675);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Relatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio";
            this.Load += new System.EventHandler(this.Relatorio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dBars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entregadorComBaixaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DBars dBars;
        private System.Windows.Forms.BindingSource entregadorComBaixaBindingSource;
        private DBarsTableAdapters.EntregadorComBaixaTableAdapter entregadorComBaixaTableAdapter;

        // NOVO
        private System.Windows.Forms.BindingSource BaixaHojeBindingSource;
        private DBarsTableAdapters.BaixaHojeTableAdapter BaixaHojeTableAdapter;
    }
}