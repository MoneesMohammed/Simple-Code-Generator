using CodeGeneration;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace Simple_Code_Generator
{
    public partial class frmMain : Form
    {
        [Serializable]
        private class clsGeneratorInput
        {
            public string ClassName;
            public string Properties;
            public string TableName;
            public string SourceName;
        }

        clsGeneratorInput Input = new clsGeneratorInput();
        clsCodeGenerator codeGen; //= new clsCodeGenerator(ClassName, Properties, TableName);

        public frmMain()
        {
            InitializeComponent();
        }

        private void butGenerated_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Input.ClassName = txtClassName.Text;
            Input.Properties = txtProperties.Text;
            Input.TableName = txtTableName.Text;
            Input.SourceName = txtSourceName.Text;

            codeGen = new clsCodeGenerator (Input.ClassName, Input.Properties, Input.TableName, Input.SourceName);

            codeGen.GenerateEventLogCode = chbAddWriteOnEventLog.Checked;

            if (!chbAPIs.Checked)
            {
                if (rbtnBusinessLayer.Checked)
                    txtCodeGenerated.Text = codeGen.GenerateBLCode();
                else if (rbtnDataLayer.Checked)
                    txtCodeGenerated.Text = codeGen.GenerateDLCode();

            }
            else
            {
                if (rbtnDataLayer.Checked)
                    txtCodeGenerated.Text = codeGen.GenerateAPIDLCode();
                else if (rbtnSQLStoredProcedures.Checked)
                    txtCodeGenerated.Text = codeGen.GenerateSQLSPCode();
                else if (rbtnBusinessLayer.Checked)
                    txtCodeGenerated.Text = codeGen.GenerateAPIBLCode();
                else
                    txtCodeGenerated.Text = codeGen.GenerateAPIsCtrlrCode();

            }
           
            


            MessageBox.Show("Generated!","Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void butCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCodeGenerated.Text);

            MessageBox.Show("Copied!","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Input = LoadGeneratorInput("Input.Json");

            txtClassName.Text = Input.ClassName;
            txtProperties.Text = Input.Properties;
            txtTableName.Text = Input.TableName;
            txtSourceName.Text = Input.SourceName;
        }


        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Input.ClassName  = txtClassName.Text;
            Input.Properties = txtProperties.Text;
            Input.TableName  = txtTableName.Text;
            Input.SourceName = txtSourceName.Text;

            string path = "Input.Json";

            SaveGeneratorInput(path, Input);
        }


        private clsGeneratorInput LoadGeneratorInput(string path)
        {
            if (!File.Exists(path))
                return new clsGeneratorInput();

            string json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
                return new clsGeneratorInput();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(clsGeneratorInput));

            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            {
                return (clsGeneratorInput)serializer.ReadObject(stream);
            }
        }

        private void SaveGeneratorInput(string path, clsGeneratorInput input)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(clsGeneratorInput));

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, input);
                string json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

                File.WriteAllText(path, json);
            }
        }

        private void txtProperties_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProperties.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtProperties, "Properties should have a Value!");
                return;
            }

            if (!clsValidatoin.ValidateProperties(txtProperties.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtProperties, "Invalid Properties Format!");
            }
            else
            {
                errorProvider1.SetError(txtProperties, null);
            }
        }

        private void txtClassName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtClassName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtClassName, "Class Name should have a Value!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtClassName, null);
            }
        }

        private void txtTableName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTableName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTableName, "Table Name should have a Value!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTableName, null);
            }
        }

        private void rbtnDataLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAPIs.Checked)
            {
                return;
            }

            chbAddWriteOnEventLog.Visible = rbtnDataLayer.Checked;

             chbAddWriteOnEventLog.Checked = false;
            
        }

        private void chbAddWriteOnEventLog_CheckedChanged(object sender, EventArgs e)
        {
            lblExSourceName.Visible = chbAddWriteOnEventLog.Checked;
            lblSourceName.Visible = lblExSourceName.Visible;
            txtSourceName.Visible = lblSourceName.Visible;

            if(chbAddWriteOnEventLog.Checked)
                txtSourceName.Focus();
        }

        private void txtSourceName_Validating(object sender, CancelEventArgs e)
        {
            if (!txtSourceName.Visible)
                return;

            if (string.IsNullOrEmpty(txtSourceName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSourceName, "Source Name should have a Value!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtSourceName, null);
            }
        }

        private void chbAPIs_CheckedChanged(object sender, EventArgs e)
        {
            rbtnAPIsController.Visible = chbAPIs.Checked;
            rbtnSQLStoredProcedures.Visible = rbtnAPIsController.Visible;

            rbtnBusinessLayer.Text = chbAPIs.Checked ? "API Business Layer": "Business Layer";
            rbtnDataLayer.Text = chbAPIs.Checked ? "API Data Layer" : "Data Layer";

            chbAddWriteOnEventLog.Visible = !chbAPIs.Checked && !rbtnBusinessLayer.Checked;
            chbAddWriteOnEventLog.Checked = false;
        }


    }
}
