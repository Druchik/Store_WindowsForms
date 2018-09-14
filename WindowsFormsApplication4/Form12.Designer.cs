namespace WindowsFormsApplication4
{
    partial class Form12
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Магазин2DataSet = new WindowsFormsApplication4.Магазин2DataSet();
            this.ПродажиBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ПродажиTableAdapter = new WindowsFormsApplication4.Магазин2DataSetTableAdapters.ПродажиTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Магазин2DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ПродажиBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.ПродажиBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApplication4.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1065, 535);
            this.reportViewer1.TabIndex = 0;
            // 
            // Магазин2DataSet
            // 
            this.Магазин2DataSet.DataSetName = "Магазин2DataSet";
            this.Магазин2DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ПродажиBindingSource
            // 
            this.ПродажиBindingSource.DataMember = "Продажи";
            this.ПродажиBindingSource.DataSource = this.Магазин2DataSet;
            // 
            // ПродажиTableAdapter
            // 
            this.ПродажиTableAdapter.ClearBeforeFill = true;
            // 
            // Form12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 559);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form12";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчет по продажам";
            this.Load += new System.EventHandler(this.Form12_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Магазин2DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ПродажиBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ПродажиBindingSource;
        private Магазин2DataSet Магазин2DataSet;
        private Магазин2DataSetTableAdapters.ПродажиTableAdapter ПродажиTableAdapter;
    }
}