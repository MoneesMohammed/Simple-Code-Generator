using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeGenerationForBusinessLayer.CodeGenerationAPIs
{
    public class clsCodeGenAPIDTO
    {
        public string ClassName { get; set; }
        public string Properties { get; set; } //(example: ID:int,Name:string,Email:string)
        public string TableName { get; set; }

        private StringBuilder sb { get; }

        private string _PropID;
        private string _nameMethod;
        private string[] props;
        private string _nameDTO;


        public clsCodeGenAPIDTO(string ClassName, string Properties, string NameTable)
        {
            this.ClassName = ClassName + "Data";
            this.Properties = Properties;
            this.TableName = NameTable;

            sb = new StringBuilder();
            props = Properties.Split(',');

            _PropID = props[0].Split(':')[0];
            _nameMethod = ClassName.Replace("cls", "").Replace("Data", "");

            _nameDTO = _nameMethod[0].ToString().ToUpper() + "DTO";
        }

        private void CreateClassDTO()//cls[Name]sData
        {
            sb.AppendLine($"public class {_nameMethod}DTO");
            sb.AppendLine("{");

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    public {propType} {propName} {{ get; set; }}");
            }

            string Props = "";

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                string comma = prop == props.Last() ? "" : ",";

                Props += propType + " " + propName + comma;

            }

            sb.AppendLine("");

            sb.AppendLine($"  public {_nameMethod}DTO({Props})");
            sb.AppendLine("  {");

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    this.{propName} = {propName} ;");
            }


            sb.AppendLine("  }");

            sb.AppendLine("}");
            sb.AppendLine("");
        }


        public string GenerateCode()
        {
            CreateClassDTO();
            
            return sb.ToString();
        }

    }
}
