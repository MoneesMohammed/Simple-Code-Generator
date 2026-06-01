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
            this.rbutBusinessLayer = new System.Windows.Forms.RadioButton();
            this.rbutDataLayer = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
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
            this.txtCodeGenerated.Location = new System.Drawing.Point(12, 164);
            this.txtCodeGenerated.Multiline = true;
            this.txtCodeGenerated.Name = "txtCodeGenerated";
            this.txtCodeGenerated.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCodeGenerated.Size = new System.Drawing.Size(1169, 805);
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
            this.butCopy.Location = new System.Drawing.Point(1055, 975);
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
            // rbutBusinessLayer
            // 
            this.rbutBusinessLayer.AutoSize = true;
            this.rbutBusinessLayer.Checked = true;
            this.rbutBusinessLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbutBusinessLayer.Location = new System.Drawing.Point(907, 118);
            this.rbutBusinessLayer.Name = "rbutBusinessLayer";
            this.rbutBusinessLayer.Size = new System.Drawing.Size(135, 24);
            this.rbutBusinessLayer.TabIndex = 10;
            this.rbutBusinessLayer.TabStop = true;
            this.rbutBusinessLayer.Text = "Business Layer";
            this.rbutBusinessLayer.UseVisualStyleBackColor = true;
            // 
            // rbutDataLayer
            // 
            this.rbutDataLayer.AutoSize = true;
            this.rbutDataLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbutDataLayer.Location = new System.Drawing.Point(1066, 118);
            this.rbutDataLayer.Name = "rbutDataLayer";
            this.rbutDataLayer.Size = new System.Drawing.Size(105, 24);
            this.rbutDataLayer.TabIndex = 11;
            this.rbutDataLayer.Text = "Data Layer";
            this.rbutDataLayer.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 1003);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "copyright © 2026";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 1028);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbutDataLayer);
            this.Controls.Add(this.rbutBusinessLayer);
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
        private System.Windows.Forms.RadioButton rbutBusinessLayer;
        private System.Windows.Forms.RadioButton rbutDataLayer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

