namespace Report.Relatorio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorio));
            this.dBars = new Report.DBars();
            this.EntradaHojeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getDataTableAdapter = new Report.DBarsTableAdapters.GetDataTableAdapter();
            this.entradaHojeTableAdapter = new Report.DBarsTableAdapters.EntradaHojeTableAdapter();
            this.getdataprazoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getdataprazoTableAdapter = new Report.DBarsTableAdapters.GetdataprazoTableAdapter();
            this.quantidadeBaixaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.quantidadeBaixaTableAdapter = new Report.DBarsTableAdapters.QuantidadeBaixaTableAdapter();
            this.getnomebaixaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getnomebaixaTableAdapter = new Report.DBarsTableAdapters.GetnomebaixaTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dBars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntradaHojeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getdataprazoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quantidadeBaixaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getnomebaixaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dBars
            // 
            this.dBars.DataSetName = "DBars";
            this.dBars.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // EntradaHojeBindingSource
            // 
            this.EntradaHojeBindingSource.DataMember = "EntradaHoje";
            this.EntradaHojeBindingSource.DataSource = this.dBars;
            // 
            // getDataBindingSource
            // 
            this.getDataBindingSource.DataMember = "GetData";
            this.getDataBindingSource.DataSource = this.dBars;
            // 
            // getDataTableAdapter
            // 
            this.getDataTableAdapter.ClearBeforeFill = true;
            // 
            // entradaHojeTableAdapter
            // 
            this.entradaHojeTableAdapter.ClearBeforeFill = true;
            // 
            // getdataprazoBindingSource
            // 
            this.getdataprazoBindingSource.DataMember = "Getdataprazo";
            this.getdataprazoBindingSource.DataSource = this.dBars;
            // 
            // getdataprazoTableAdapter
            // 
            this.getdataprazoTableAdapter.ClearBeforeFill = true;
            // 
            // quantidadeBaixaBindingSource
            // 
            this.quantidadeBaixaBindingSource.DataMember = "QuantidadeBaixa";
            this.quantidadeBaixaBindingSource.DataSource = this.dBars;
            // 
            // quantidadeBaixaTableAdapter
            // 
            this.quantidadeBaixaTableAdapter.ClearBeforeFill = true;
            // 
            // getnomebaixaBindingSource
            // 
            this.getnomebaixaBindingSource.DataMember = "Getnomebaixa";
            this.getnomebaixaBindingSource.DataSource = this.dBars;
            // 
            // getnomebaixaTableAdapter
            // 
            this.getnomebaixaTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(909, 675);
            this.reportViewer1.TabIndex = 0;
            // 
            // FormRelatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 675);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRelatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRelatorio";
            this.Load += new System.EventHandler(this.FormRelatorio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dBars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntradaHojeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getdataprazoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quantidadeBaixaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getnomebaixaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DBars dBars;
        private System.Windows.Forms.BindingSource getDataBindingSource;
        private DBarsTableAdapters.GetDataTableAdapter getDataTableAdapter;
        private System.Windows.Forms.BindingSource EntradaHojeBindingSource;
        private DBarsTableAdapters.EntradaHojeTableAdapter entradaHojeTableAdapter;
        private System.Windows.Forms.BindingSource getdataprazoBindingSource;
        private DBarsTableAdapters.GetdataprazoTableAdapter getdataprazoTableAdapter;
        private System.Windows.Forms.BindingSource quantidadeBaixaBindingSource;
        private DBarsTableAdapters.QuantidadeBaixaTableAdapter quantidadeBaixaTableAdapter;
        private System.Windows.Forms.BindingSource getnomebaixaBindingSource;
        private DBarsTableAdapters.GetnomebaixaTableAdapter getnomebaixaTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}