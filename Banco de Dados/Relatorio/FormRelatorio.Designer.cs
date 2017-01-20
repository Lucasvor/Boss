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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorio));
            this.getdatabaixaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBars = new Banco_de_Dados.DBars();
            this.EntradaHojeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getDataTableAdapter = new Banco_de_Dados.DBarsTableAdapters.GetDataTableAdapter();
            this.entradaHojeTableAdapter = new Banco_de_Dados.DBarsTableAdapters.EntradaHojeTableAdapter();
            this.getdatabaixaTableAdapter = new Banco_de_Dados.DBarsTableAdapters.GetdatabaixaTableAdapter();
            this.getdataprazoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getdataprazoTableAdapter = new Banco_de_Dados.DBarsTableAdapters.GetdataprazoTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.getdatabaixaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntradaHojeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getdataprazoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // getdatabaixaBindingSource
            // 
            this.getdatabaixaBindingSource.DataMember = "Getdatabaixa";
            this.getdatabaixaBindingSource.DataSource = this.dBars;
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
            // getdatabaixaTableAdapter
            // 
            this.getdatabaixaTableAdapter.ClearBeforeFill = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.getdatabaixaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntradaHojeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getdataprazoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DBars dBars;
        private System.Windows.Forms.BindingSource getDataBindingSource;
        private DBarsTableAdapters.GetDataTableAdapter getDataTableAdapter;
        private System.Windows.Forms.BindingSource EntradaHojeBindingSource;
        private DBarsTableAdapters.EntradaHojeTableAdapter entradaHojeTableAdapter;
        private System.Windows.Forms.BindingSource getdatabaixaBindingSource;
        private DBarsTableAdapters.GetdatabaixaTableAdapter getdatabaixaTableAdapter;
        private System.Windows.Forms.BindingSource getdataprazoBindingSource;
        private DBarsTableAdapters.GetdataprazoTableAdapter getdataprazoTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}