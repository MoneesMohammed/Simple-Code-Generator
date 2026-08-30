namespace Simple_Code_Generator
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.txtProperties = new System.Windows.Forms.TextBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.txtCodeGenerated = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.butCopy = new System.Windows.Forms.Button();
            this.butGenerate = new System.Windows.Forms.Button();
            this.rbtnBusinessLayer = new System.Windows.Forms.RadioButton();
            this.rbtnDataLayer = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chbAddWriteOnEventLog = new System.Windows.Forms.CheckBox();
            this.txtSourceName = new System.Windows.Forms.TextBox();
            this.lblSourceName = new System.Windows.Forms.Label();
            this.lblExSourceName = new System.Windows.Forms.Label();
            this.chbAPIs = new System.Windows.Forms.CheckBox();
            this.rbtnSQLStoredProcedures = new System.Windows.Forms.RadioButton();
            this.rbtnAPIsController = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtClassName
            // 
            this.txtClassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClassName.Location = new System.Drawing.Point(151, 12);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(271, 26);
            this.txtClassName.TabIndex = 0;
            this.txtClassName.Validating += new System.ComponentModel.CancelEventHandler(this.txtClassName_Validating);
            // 
            // txtProperties
            // 
            this.txtProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProperties.Location = new System.Drawing.Point(151, 62);
            this.txtProperties.Name = "txtProperties";
            this.txtProperties.Size = new System.Drawing.Size(711, 26);
            this.txtProperties.TabIndex = 1;
            this.txtProperties.Validating += new System.ComponentModel.CancelEventHandler(this.txtProperties_Validating);
            // 
            // txtTableName
            // 
            this.txtTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableName.Location = new System.Drawing.Point(591, 12);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(271, 26);
            this.txtTableName.TabIndex = 2;
            this.txtTableName.Validating += new System.ComponentModel.CancelEventHandler(this.txtTableName_Validating);
            // 
            // txtCodeGenerated
            // 
            this.txtCodeGenerated.AcceptsTab = true;
            this.txtCodeGenerated.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeGenerated.Location = new System.Drawing.Point(12, 207);
            this.txtCodeGenerated.Multiline = true;
            this.txtCodeGenerated.Name = "txtCodeGenerated";
            this.txtCodeGenerated.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCodeGenerated.Size = new System.Drawing.Size(1169, 816);
            this.txtCodeGenerated.TabIndex = 4;
            this.txtCodeGenerated.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "ClassName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Properties";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(460, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "TableName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(24, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(379, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "example:           ID:int,Name:string,Email:string";
            // 
            // butCopy
            // 
            this.butCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butCopy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.butCopy.Image = global::Simple_Code_Generator.Properties.Resources.copy_1;
            this.butCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butCopy.Location = new System.Drawing.Point(1045, 1029);
            this.butCopy.Name = "butCopy";
            this.butCopy.Size = new System.Drawing.Size(126, 44);
            this.butCopy.TabIndex = 9;
            this.butCopy.Text = "Copy";
            this.butCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butCopy.UseVisualStyleBackColor = true;
            this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
            // 
            // butGenerate
            // 
            this.butGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butGenerate.ForeColor = System.Drawing.Color.Indigo;
            this.butGenerate.Image = global::Simple_Code_Generator.Properties.Resources.ai_technology;
            this.butGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butGenerate.Location = new System.Drawing.Point(952, 15);
            this.butGenerate.Name = "butGenerate";
            this.butGenerate.Size = new System.Drawing.Size(216, 73);
            this.butGenerate.TabIndex = 3;
            this.butGenerate.Text = "GENERATE";
            this.butGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butGenerate.UseVisualStyleBackColor = true;
            this.butGenerate.Click += new System.EventHandler(this.butGenerated_Click);
            // 
            // rbtnBusinessLayer
            // 
            this.rbtnBusinessLayer.AutoSize = true;
            this.rbtnBusinessLayer.Checked = true;
            this.rbtnBusinessLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnBusinessLayer.Location = new System.Drawing.Point(852, 118);
            this.rbtnBusinessLayer.Name = "rbtnBusinessLayer";
            this.rbtnBusinessLayer.Size = new System.Drawing.Size(135, 24);
            this.rbtnBusinessLayer.TabIndex = 10;
            this.rbtnBusinessLayer.Text = "Business Layer";
            this.rbtnBusinessLayer.UseVisualStyleBackColor = true;
            // 
            // rbtnDataLayer
            // 
            this.rbtnDataLayer.AutoSize = true;
            this.rbtnDataLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDataLayer.Location = new System.Drawing.Point(1040, 118);
            this.rbtnDataLayer.Name = "rbtnDataLayer";
            this.rbtnDataLayer.Size = new System.Drawing.Size(105, 24);
            this.rbtnDataLayer.TabIndex = 11;
            this.rbtnDataLayer.Text = "Data Layer";
            this.rbtnDataLayer.UseVisualStyleBackColor = true;
            this.rbtnDataLayer.CheckedChanged += new System.EventHandler(this.rbtnDataLayer_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 1057);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "copyright © 2026";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chbAddWriteOnEventLog
            // 
            this.chbAddWriteOnEventLog.AutoSize = true;
            this.chbAddWriteOnEventLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbAddWriteOnEventLog.Location = new System.Drawing.Point(972, 94);
            this.chbAddWriteOnEventLog.Name = "chbAddWriteOnEventLog";
            this.chbAddWriteOnEventLog.Size = new System.Drawing.Size(173, 22);
            this.chbAddWriteOnEventLog.TabIndex = 13;
            this.chbAddWriteOnEventLog.Text = "Add WriteOnEventLog";
            this.chbAddWriteOnEventLog.UseVisualStyleBackColor = true;
            this.chbAddWriteOnEventLog.Visible = false;
            this.chbAddWriteOnEventLog.CheckedChanged += new System.EventHandler(this.chbAddWriteOnEventLog_CheckedChanged);
            // 
            // txtSourceName
            // 
            this.txtSourceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSourceName.Location = new System.Drawing.Point(151, 132);
            this.txtSourceName.Name = "txtSourceName";
            this.txtSourceName.Size = new System.Drawing.Size(271, 26);
            this.txtSourceName.TabIndex = 14;
            this.txtSourceName.Visible = false;
            this.txtSourceName.Validating += new System.ComponentModel.CancelEventHandler(this.txtSourceName_Validating);
            // 
            // lblSourceName
            // 
            this.lblSourceName.AutoSize = true;
            this.lblSourceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourceName.Location = new System.Drawing.Point(23, 132);
            this.lblSourceName.Name = "lblSourceName";
            this.lblSourceName.Size = new System.Drawing.Size(122, 24);
            this.lblSourceName.TabIndex = 15;
            this.lblSourceName.Text = "SourceName";
            this.lblSourceName.Visible = false;
            // 
            // lblExSourceName
            // 
            this.lblExSourceName.AutoSize = true;
            this.lblExSourceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExSourceName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblExSourceName.Location = new System.Drawing.Point(147, 161);
            this.lblExSourceName.Name = "lblExSourceName";
            this.lblExSourceName.Size = new System.Drawing.Size(147, 20);
            this.lblExSourceName.TabIndex = 16;
            this.lblExSourceName.Text = "example: \"DVLD\"";
            this.lblExSourceName.Visible = false;
            // 
            // chbAPIs
            // 
            this.chbAPIs.AutoSize = true;
            this.chbAPIs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbAPIs.Location = new System.Drawing.Point(889, 17);
            this.chbAPIs.Name = "chbAPIs";
            this.chbAPIs.Size = new System.Drawing.Size(57, 22);
            this.chbAPIs.TabIndex = 17;
            this.chbAPIs.Text = "APIs";
            this.chbAPIs.UseVisualStyleBackColor = true;
            this.chbAPIs.CheckedChanged += new System.EventHandler(this.chbAPIs_CheckedChanged);
            // 
            // rbtnSQLStoredProcedures
            // 
            this.rbtnSQLStoredProcedures.AutoSize = true;
            this.rbtnSQLStoredProcedures.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSQLStoredProcedures.Location = new System.Drawing.Point(473, 118);
            this.rbtnSQLStoredProcedures.Name = "rbtnSQLStoredProcedures";
            this.rbtnSQLStoredProcedures.Size = new System.Drawing.Size(196, 24);
            this.rbtnSQLStoredProcedures.TabIndex = 19;
            this.rbtnSQLStoredProcedures.Text = "SQL Stored Procedures";
            this.rbtnSQLStoredProcedures.UseVisualStyleBackColor = true;
            this.rbtnSQLStoredProcedures.Visible = false;
            // 
            // rbtnAPIsController
            // 
            this.rbtnAPIsController.AutoSize = true;
            this.rbtnAPIsController.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnAPIsController.Location = new System.Drawing.Point(686, 118);
            this.rbtnAPIsController.Name = "rbtnAPIsController";
            this.rbtnAPIsController.Size = new System.Drawing.Size(133, 24);
            this.rbtnAPIsController.TabIndex = 18;
            this.rbtnAPIsController.Text = "APIs Controller";
            this.rbtnAPIsController.UseVisualStyleBackColor = true;
            this.rbtnAPIsController.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 1085);
            this.Controls.Add(this.rbtnSQLStoredProcedures);
            this.Controls.Add(this.rbtnAPIsController);
            this.Controls.Add(this.chbAPIs);
            this.Controls.Add(this.lblExSourceName);
            this.Controls.Add(this.lblSourceName);
            this.Controls.Add(this.txtSourceName);
            this.Controls.Add(this.chbAddWriteOnEventLog);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbtnDataLayer);
            this.Controls.Add(this.rbtnBusinessLayer);
            this.Controls.Add(this.butCopy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCodeGenerated);
            this.Controls.Add(this.butGenerate);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.txtProperties);
            this.Controls.Add(this.txtClassName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.TextBox txtProperties;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Button butGenerate;
        private System.Windows.Forms.TextBox txtCodeGenerated;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button butCopy;
        private System.Windows.Forms.RadioButton rbtnBusinessLayer;
        private System.Windows.Forms.RadioButton rbtnDataLayer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox chbAddWriteOnEventLog;
        private System.Windows.Forms.Label lblSourceName;
        private System.Windows.Forms.TextBox txtSourceName;
        private System.Windows.Forms.Label lblExSourceName;
        private System.Windows.Forms.CheckBox chbAPIs;
        private System.Windows.Forms.RadioButton rbtnSQLStoredProcedures;
        private System.Windows.Forms.RadioButton rbtnAPIsController;
    }
}

