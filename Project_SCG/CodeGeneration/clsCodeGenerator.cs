using CodeGenerationForBusinessLayer.CodeGenerationAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    public class clsCodeGenerator
    {
        private clsCodeGenBusinessLayer CodeGenBL;
        private clsCodeGenDataLayer CodeGenDL;
        private clsCodeGenAPIDataLayer CodeGenAPIDL;
        private clsCodeGenSQLStoredProcedures CodeGenSQLSP;
        private clsCodeGenAPIBusinessLayer CodeGenAPIBL;
        private clsCodeGenAPIsController CodeGenAPIsCtrlr;
        private clsCodeGenAPIDTO CodeGenAPIDTO;
        private bool _GenerateEventLogCode;

        public bool GenerateEventLogCode // Add WriteOnEventLog Method in code
        {
            get { return _GenerateEventLogCode; }

            set
            {
                _GenerateEventLogCode = value;
                CodeGenDL.GenerateEventLogCode = value;
            }
        }

        /// <param name="ClassName">The ClassName to be Generated.</param>
        /// <param name="Properties">The Properties of ClassName . example: ID:int,Name:string,Email:string.</param>
        /// <param name="NameTable">The NameTable to be Generated Querys.</param>
        /// <param name="SourceName">SourceName represents the identifier of the software.</param>
        public clsCodeGenerator(string ClassName, string Properties, string NameTable , string SourceName) 
        {
            CodeGenBL  = new clsCodeGenBusinessLayer(ClassName, Properties, NameTable);
            CodeGenDL  = new clsCodeGenDataLayer (ClassName, Properties, NameTable , SourceName);

            CodeGenAPIDL = new clsCodeGenAPIDataLayer(ClassName, Properties, NameTable);

            CodeGenSQLSP = new clsCodeGenSQLStoredProcedures (ClassName, Properties, NameTable);

            CodeGenAPIBL = new clsCodeGenAPIBusinessLayer (ClassName, Properties, NameTable);

            CodeGenAPIsCtrlr = new clsCodeGenAPIsController(ClassName, Properties, NameTable);

            CodeGenAPIDTO = new clsCodeGenAPIDTO(ClassName, Properties, NameTable);
        }

        /// <summary>
        /// Generate Business Layer Code .
        /// </summary>
        /// <returns>The Generate Business Layer of the Code.</returns>
        public string GenerateBLCode() 
        {
            return CodeGenBL.GenerateCode();
        }

        /// <summary>
        /// Generate Data Layer Code .
        /// </summary>
        /// <returns>The Generate Data Layer of the Code.</returns>
        public string GenerateDLCode() 
        {
            return CodeGenDL.GenerateCode();
        }

        /// <summary>
        /// Generate DataSettings Class Code .
        /// </summary>
        /// <returns>The Generate DataSettings Class of the Code.</returns>
        public string GenerateDSClassCode() 
        {
            return CodeGenDL.GenerateDataSettingsClass();
        }


        public string GenerateAPIDLCode()
        {
            return CodeGenAPIDL.GenerateCode();
        }

        public string GenerateSQLSPCode()
        {
            return CodeGenSQLSP.GenerateCode();
        }

        public string GenerateAPIBLCode()
        {
            return CodeGenAPIBL.GenerateCode();
        }

        public string GenerateAPIsCtrlrCode()
        {
            return CodeGenAPIsCtrlr.GenerateCode();
        }

        public string GenerateAPIDTOCode()
        {
            return CodeGenAPIDTO.GenerateCode();
        }

    }
}
