namespace Banco_de_Dados.Relatorio
{
    partial class FormRelatorio
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
            
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dBars = new Banco_de_Dados.DBars();
            this.baixaHojeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.baixaHojeTableAdapter = new Banco_de_Dados.DBarsTableAdapters.BaixaHojeTableAdapter();
            this.entregadorComBaixaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entregadorComBaixaTableAdapter = new Banco_de_Dados.DBarsTableAdapters.EntregadorComBaixaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dBars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baixaHojeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entregadorComBaixaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            
            
            //reportDataSource1.Value = this.entregadorComBaixaBindingSource;
            //this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "Banco_de_Dados.Relatorio.NBaixa.rdlc";
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
            // baixaHojeBindingSource
            // 
            this.baixaHojeBindingSource.DataMember = "BaixaHoje";
            this.baixaHojeBindingSource.DataSource = this.dBars;
            // 
            // baixaHojeTableAdapter
            // 
            this.baixaHojeTableAdapter.ClearBeforeFill = true;
            // 
            // entregadorComBaixaBindingSource1
            // 
            this.entregadorComBaixaBindingSource.DataMember = "EntregadorComBaixa";
            this.entregadorComBaixaBindingSource.DataSource = this.dBars;
            // 
            // entregadorComBaixaTableAdapter
            // 
            this.entregadorComBaixaTableAdapter.ClearBeforeFill = true;
            // 
            // FormRelatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 675);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormRelatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRelatorio";
            this.Load += new System.EventHandler(this.FormRelatorio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dBars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baixaHojeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entregadorComBaixaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DBars dBars;
        private System.Windows.Forms.BindingSource baixaHojeBindingSource;
        
        private DBarsTableAdapters.BaixaHojeTableAdapter baixaHojeTableAdapter;
        private System.Windows.Forms.BindingSource entregadorComBaixaBindingSource;
        private DBarsTableAdapters.EntregadorComBaixaTableAdapter entregadorComBaixaTableAdapter;
    }
}