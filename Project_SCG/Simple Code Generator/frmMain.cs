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
        }

        clsGeneratorInput Input;
        clsCodeGenerator codeGen; //= new clsCodeGenerator(ClassName, Properties, TableName);

        public frmMain()
        {
            InitializeComponent();
        }

        private void butGenerated_Click(object sender, EventArgs e)
        {
            Input.ClassName = txtClassName.Text;
            Input.Properties = txtProperties.Text;
            Input.TableName = txtTableName.Text;

            codeGen = new clsCodeGenerator (Input.ClassName, Input.Properties, Input.TableName);

            if (rbutBusinessLayer.Checked)
                txtCodeGenerated.Text = codeGen.GenerateBLCode();
            else
                txtCodeGenerated.Text = codeGen.GenerateDLCode();

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

            txtClassName.Text  = Input.ClassName;
            txtProperties.Text = Input.Properties;
            txtTableName.Text  = Input.TableName;

        }


        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Input.ClassName = txtClassName.Text;
            Input.Properties = txtProperties.Text;
            Input.TableName = txtTableName.Text;

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
    }
}
