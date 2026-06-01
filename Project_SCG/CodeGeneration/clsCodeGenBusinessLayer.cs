using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    public class clsCodeGenBusinessLayer
    {
        public string ClassName { get; set; }
        public string Properties { get; set; } //(example: ID:int,Name:string,Email:string)
        public string TableName { get; set; }
        private StringBuilder sb { get; }

        private string _PropID;
        private string _nameMethod;

        private string[] props ;

        public clsCodeGenBusinessLayer(string ClassName, string Properties, string tableName)
        {
            this.ClassName = ClassName;
            this.Properties = Properties;
            this.TableName = tableName;

            sb = new StringBuilder();
            props = Properties.Split(',');

            _PropID = props[0].Split(':')[0];
            _nameMethod = _PropID.Substring(0, _PropID.LastIndexOf("ID"));
        }

        private void CreateClassAndProperties()
        {
            sb.AppendLine($"public class {ClassName}");
            sb.AppendLine("{");

            sb.AppendLine("\n    public enum enMode { AddNew = 0, Update = 1 };");
            sb.AppendLine("    private enMode Mode = enMode.AddNew; \n\n");

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    public {propType} {propName} {{ get; set; }}");
            }

        }

        private void CreateDefaultConstructor()
        {
            sb.AppendLine($"\n  public {ClassName}()");
            sb.AppendLine("  {");

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                string DefaultValue = propType != "string" && propType != "bool" && propType != "DateTime" ? "-1"
                     : propType == "string" ? "\"\"" : propType == "bool" ? "true" : propType == "DateTime" ? "DateTime.Now" : "";

                

                sb.AppendLine($"    this.{propName} = {DefaultValue} ;");
            }

            sb.AppendLine("\n    Mode = enMode.AddNew;");
            sb.AppendLine("  }");

        }

        private void CreateConstructor()
        {
            string Props = "";

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                string comma = prop == props.Last() ? "" : ",";

                Props += propType + " " + propName + comma;

            }

            sb.AppendLine($"\n  private {ClassName}({Props})");
            sb.AppendLine("  {");

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    this.{propName} = {propName} ;");
            }

            sb.AppendLine("\n    Mode = enMode.Update;");
            sb.AppendLine("  }");

        }

        private void CreateFindMethod()
        {
            string PropID = _PropID;

            sb.AppendLine($"\n  public static {ClassName} Find(int {PropID})");
            sb.AppendLine("  {");

            Dictionary<string, string> defaults = new Dictionary<string, string>
            {
                { "int", "-1" },
                { "float", "-1" },
                { "byte", "-1" },
                { "double", "-1" },
                { "decimal", "-1" },
                { "string", "\"\"" },
                { "bool", "true" },
                { "DateTime", "DateTime.Now" }

            };
           
            Dictionary<string, List<string>> grouped = new Dictionary<string, List<string>>();

            // Ignore the first parameter
            var filteredProps = props.Skip(1);

            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string name = parts[0];
                string type = parts[1];

                if (!defaults.ContainsKey(type))
                    continue;

                if (!grouped.ContainsKey(type))
                    grouped[type] = new List<string>();

                grouped[type].Add($"{name} = {defaults[type]}");
            }
            
            foreach (var group in grouped)
            {
                sb.AppendLine($"    {group.Key} {string.Join(", ", group.Value)};");
            }

            sb.AppendLine("");

            string nameMethod = _nameMethod;

            string PropsData = "";
            string PropsCon = "";

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];

                if (PropsData == "")
                    PropsData = propName ;
                else
                    PropsData += ", ref " + propName;

                if (PropsCon == "")
                    PropsCon = propName;
                else
                    PropsCon += ", " + propName;

            }

            sb.AppendLine($"    if({ClassName}Data.Get{nameMethod}InfoBy{PropID}({PropsData}))");
            sb.AppendLine("    {");
            sb.AppendLine($"     return new {ClassName}({PropsCon});");
            sb.AppendLine("    }");
            sb.AppendLine("    else");
            sb.AppendLine("      return null;");

            sb.AppendLine("  }");

        }

        private void CreateAddNewMethod()
        {
            string PropID = _PropID;
            string nameMethod = _nameMethod;

            string Props = "";
            var filteredProps = props.Skip(1);

            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];

                if (Props == "")
                    Props = "this." + propName;
                else
                    Props += ", " + "this." + propName;
            }


            sb.AppendLine($"\n  private bool _AddNew{nameMethod}()");
            sb.AppendLine("  {");

            sb.AppendLine($"   this.{PropID} = {ClassName}Data.AddNew{nameMethod}({Props});");

            sb.AppendLine($"   return ({PropID} != -1);");

            sb.AppendLine("  }");
        }

        private void CreateUpdateMethod()
        {
            string PropID = _PropID;
            string nameMethod = _nameMethod;

            string Props = "";
            
            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];

                if (Props == "")
                    Props = "this." + propName;
                else
                    Props += ", " +"this."+ propName;
            }


            sb.AppendLine($"\n  private bool _Update{nameMethod}()");
            sb.AppendLine("  {");

            sb.AppendLine($"   return {ClassName}Data.Update{nameMethod}({Props});");

            sb.AppendLine("  }");
        }

        private void CreateSaveMethod()
        {
            string PropID = _PropID;
            string nameMethod = _nameMethod;

            sb.AppendLine($"\n  public bool Save()");
            sb.AppendLine("  {");

            sb.AppendLine("    switch (Mode)");
            sb.AppendLine("    {");
            sb.AppendLine("        case enMode.AddNew:\n");
            sb.AppendLine($"            if (_AddNew{nameMethod}())");
            sb.AppendLine("            {");
            sb.AppendLine("                Mode = enMode.Update;");
            sb.AppendLine("                return true;");
            sb.AppendLine("            }");
            sb.AppendLine("            else");
            sb.AppendLine("            {");
            sb.AppendLine("                return false;");
            sb.AppendLine("            }\n");
            sb.AppendLine("        case enMode.Update:\n");
            sb.AppendLine($"            return (_Update{nameMethod}());\n");
            sb.AppendLine("    }\n");
            sb.AppendLine("    return false;");

            sb.AppendLine("  }");
        }

        private void CreateDeleteMethod()
        {
            string PropID = _PropID;
            string nameMethod = _nameMethod;

            sb.AppendLine($"\n  public bool Delete()");
            sb.AppendLine("  {");
            sb.AppendLine($"    return {ClassName}Data.Delete{nameMethod}(this.{PropID});");
            sb.AppendLine("  }");
        }

        private void CreateGetAllMethod()
        {
            string PropID = _PropID;
            
            sb.AppendLine($"\n  public static DataTable GetAll{TableName}()");
            sb.AppendLine("  {");
            sb.AppendLine($"    return {ClassName}Data.GetAll{TableName}();");
            sb.AppendLine("  }");
        }

        public string GenerateCode()
        {
            CreateClassAndProperties();
            CreateDefaultConstructor();
            CreateConstructor();
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
