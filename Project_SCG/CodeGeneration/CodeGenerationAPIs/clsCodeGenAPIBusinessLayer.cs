using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{ 
    public class clsCodeGenAPIBusinessLayer
    {
        public string ClassName { get; set; }
        public string Properties { get; set; } //(example: ID:int,Name:string,Email:string)
        public string TableName { get; set; }
        private StringBuilder sb { get; }

        private string _PropID;
        private string _nameMethod;
        private string _nameDTO;

        private string[] props;

        public clsCodeGenAPIBusinessLayer(string ClassName, string Properties, string tableName)
        {
            this.ClassName = ClassName;
            this.Properties = Properties;
            this.TableName = tableName;

            sb = new StringBuilder();
            props = Properties.Split(',');

            _PropID = props[0].Split(':')[0];
            _nameMethod  = ClassName.Replace("cls", "");

            _nameDTO = _nameMethod[0].ToString().ToUpper() + "DTO";
        }

        private void CreateClassAndProperties()
        {
            sb.AppendLine($"public class {ClassName}");
            sb.AppendLine("{");
            sb.AppendLine("");
            sb.AppendLine("\n    public enum enMode { AddNew = 0, Update = 1 };");
            sb.AppendLine("    private enMode Mode = enMode.AddNew; \n\n");
            sb.AppendLine("");
            sb.AppendLine("");

            string propDTO = "";

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    public {propType} {propName} {{ get; set; }}");

                string comma = prop == props.Last() ? "" : ",";

                propDTO += $"this.{propName}{comma} ";
            }

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine($"    public {_nameMethod}DTO {_nameDTO}");
            sb.AppendLine("    {");
            sb.AppendLine($"        get {{ return new {_nameMethod}DTO({propDTO}); }}");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");

        }

        private void CreateDefaultConstructor()
        {
            

            sb.AppendLine($"    public {ClassName}({_nameMethod}DTO {_nameMethod[0]}DTO , enMode cMode = enMode.AddNew)");
            sb.AppendLine("    {");

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];


                sb.AppendLine($"        this.{propName} = {_nameDTO}.{propName} ;");
            }

            sb.AppendLine("");
            sb.AppendLine("        this.Mode = cMode;");
            sb.AppendLine("    }");

            sb.AppendLine("");
            sb.AppendLine("");

        }

        private void CreateFindMethod()
        {
            string PropID = _PropID;

            sb.AppendLine($"    public static {ClassName}? Find(int {PropID})");
            sb.AppendLine("    {");


            sb.AppendLine($"      {_nameMethod}DTO? {_nameDTO} = {ClassName}Data.Get{_nameMethod}InfoBy{PropID}({PropID});");

            sb.AppendLine("");

            sb.AppendLine($"      if({_nameDTO} != null)");
            sb.AppendLine("      {");
            sb.AppendLine($"        return new {ClassName}({_nameDTO}, enMode.Update);");
            sb.AppendLine("      }");
            sb.AppendLine("      else");
            sb.AppendLine("        return null;");

            sb.AppendLine("    }");

            sb.AppendLine("");
            sb.AppendLine("");
        }

        private void CreateAddNewMethod()
        {
            sb.AppendLine($"    private bool _AddNew{_nameMethod}()");
            sb.AppendLine("    {");
            sb.AppendLine($"      this.{_PropID} = {ClassName}Data.AddNew{_nameMethod}({_nameDTO});");
            sb.AppendLine($"      return ({_PropID} != -1);");
            sb.AppendLine("    }");

            sb.AppendLine("");
            sb.AppendLine("");
        }

        private void CreateUpdateMethod()
        {
            sb.AppendLine($"    private bool _Update{_nameMethod}()");
            sb.AppendLine("    {");
            sb.AppendLine($"      return {ClassName}Data.Update{_nameMethod}({_nameDTO});");
            sb.AppendLine("    }");

            sb.AppendLine("");
            sb.AppendLine("");
        }

        private void CreateSaveMethod()
        {
            sb.AppendLine($"    public bool Save()");
            sb.AppendLine("    {");
            sb.AppendLine("       switch (Mode)");
            sb.AppendLine("       {");
            sb.AppendLine("           case enMode.AddNew:\n");
            sb.AppendLine($"            if (_AddNew{_nameMethod}())");
            sb.AppendLine("              {");
            sb.AppendLine("                  Mode = enMode.Update;");
            sb.AppendLine("                  return true;");
            sb.AppendLine("              }");
            sb.AppendLine("              else");
            sb.AppendLine("              {");
            sb.AppendLine("                  return false;");
            sb.AppendLine("              }");
            sb.AppendLine("           case enMode.Update:\n");
            sb.AppendLine($"                 return (_Update{_nameMethod}());\n");
            sb.AppendLine("       }");
            sb.AppendLine("");
            sb.AppendLine("     return false;");

            sb.AppendLine("    }");

            sb.AppendLine("");
            sb.AppendLine("");
        }

        private void CreateDeleteMethod()
        {
            sb.AppendLine($"    public static bool Delete{_nameMethod}(int {_PropID})");
            sb.AppendLine("    {");
            sb.AppendLine($"      return {ClassName}Data.Delete{_nameMethod}({_PropID});");
            sb.AppendLine("    }");

            sb.AppendLine("");
            sb.AppendLine("");
        }

        private void CreateGetAllMethod()
        {
            sb.AppendLine($"    public static List<{_nameMethod}DTO> GetAll{TableName}()");
            sb.AppendLine("    {");
            sb.AppendLine($"      return {ClassName}Data.GetAll{TableName}();");
            sb.AppendLine("    }");

            sb.AppendLine("");
            sb.AppendLine("");
        }

        public string GenerateCode()
        {
            CreateClassAndProperties();
            CreateDefaultConstructor();
            CreateFindMethod();
            CreateAddNewMethod();
            CreateUpdateMethod();
            CreateSaveMethod();
            CreateDeleteMethod();
            CreateGetAllMethod();
            sb.AppendLine("}");

            return sb.ToString();
        }




    }
}
