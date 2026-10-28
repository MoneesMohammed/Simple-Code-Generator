using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeGeneration
{
    public class clsCodeGenSQLStoredProcedures
    {
        public string ClassName { get; set; }
        public string Properties { get; set; } //(example: ID:int,Name:string,Email:string)
        public string TableName { get; set; }

        private StringBuilder sb { get; }

        private string _PropID;
        private string _nameMethod;
        private string[] props;
        
        Dictionary<string, string> dataType = new Dictionary<string, string>
        {
          { "int", "INT" },
          { "float", "REAL" }, 
          { "byte", "TINYINT" },
          { "double", "FLOAT" }, 
          { "decimal", "DECIMAL(18, 2)" }, 
          { "string", "NVARCHAR(250)" },
          { "bool", "BIT" },
          { "DateTime", "DATETIME2" } 

        };

        public clsCodeGenSQLStoredProcedures(string ClassName, string Properties, string NameTable)
        {
            this.ClassName = ClassName ;
            this.Properties = Properties;
            this.TableName = NameTable;

            sb = new StringBuilder();
            props = Properties.Split(',');

            _PropID = props[0].Split(':')[0];
            _nameMethod = ClassName.Replace("cls", "");

           

        }

        private void CreateSP_AddNew()
        {
            sb.AppendLine("");

            sb.AppendLine($"CREATE PROCEDURE SP_Add{_nameMethod}");
            
            string t1 = "";
            string t2 = "";

            var filteredProps = props.Skip(1);
            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    @{propName} {dataType[propType]},");

                string comma = prop == props.Last() ? "" : ",";

                t1 += $"{propName} {comma}";
                t2 += $"@{propName} {comma}";
            }

            sb.AppendLine($"    @New{_nameMethod}ID INT OUTPUT");

            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("     SET NOCOUNT ON;");
            sb.AppendLine("");
            sb.AppendLine($"    INSERT INTO {TableName} ({t1}) VALUES ({t2});");
            sb.AppendLine($"    SET @New{_nameMethod}ID = SCOPE_IDENTITY();");
            sb.AppendLine("");
            sb.AppendLine("END;");
            sb.AppendLine("GO");

            sb.AppendLine("");
        }

        private void CreateSP_GetInfoByID()
        {
            sb.AppendLine("");

            sb.AppendLine($"CREATE PROCEDURE SP_Get{_nameMethod}ByID");

            string t1 = "";
            
            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                string comma = prop == props.Last() ? "" : ",";

                t1 += $"{propName} {comma}";
            }

            sb.AppendLine($"    @{_PropID} INT ");

            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("");
            sb.AppendLine($"    SELECT {t1} ");
            sb.AppendLine($"    FROM {TableName} ");
            sb.AppendLine($"    WHERE {_PropID} = @{_PropID};");
            sb.AppendLine("");
            sb.AppendLine("END;");
            sb.AppendLine("GO");

            sb.AppendLine("");
        }

        private void CreateSP_Update()
        {
            sb.AppendLine("");

            sb.AppendLine($"CREATE PROCEDURE SP_Update{_nameMethod}");

            sb.AppendLine($"    @{_PropID} {dataType["int"]},");

            string t1 = "";

            var filteredProps = props.Skip(1);
            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                string comma = prop == props.Last() ? "" : ",";

                sb.AppendLine($"    @{propName} {dataType[propType]}{comma}");


                t1 += $"{propName} = @{propName} {comma}";
               
            }

            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("     SET NOCOUNT ON;");
            sb.AppendLine("");
            sb.AppendLine("     DECLARE @RowsAffected INT;");
            sb.AppendLine("");
            sb.AppendLine($"     UPDATE {TableName} SET {t1}");
            sb.AppendLine($"     WHERE {_PropID} = @{_PropID};");
            sb.AppendLine("");
            sb.AppendLine("     SET @RowsAffected = @@ROWCOUNT;");
            sb.AppendLine("     SELECT @RowsAffected AS RowsAffected;");
            sb.AppendLine("END;");
            sb.AppendLine("GO");

            sb.AppendLine("");
        }

        private void CreateSP_GetAll()
        {
            sb.AppendLine("");

            sb.AppendLine($"CREATE PROCEDURE SP_GetAll{_nameMethod}s");

            string t1 = "";

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                string comma = prop == props.Last() ? "" : ",";

                t1 += $"{propName} {comma}";
            }

            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("");
            sb.AppendLine($"    SELECT {t1} ");
            sb.AppendLine($"    FROM {TableName} ;");
            sb.AppendLine("");
            sb.AppendLine("END;");
            sb.AppendLine("GO");

            sb.AppendLine("");
        }

        private void CreateSP_Delete()
        {
            sb.AppendLine("");

            sb.AppendLine($"CREATE PROCEDURE SP_Delete{_nameMethod}");
            sb.AppendLine($"    @{_PropID} INT ");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("     SET NOCOUNT ON;");
            sb.AppendLine("");
            sb.AppendLine("     DECLARE @RowsAffected INT;");
            sb.AppendLine("");
            sb.AppendLine($"     DELETE FROM {TableName}");
            sb.AppendLine($"     WHERE {_PropID} = @{_PropID};");
            sb.AppendLine("");
            sb.AppendLine("     SET @RowsAffected = @@ROWCOUNT;");
            sb.AppendLine("     SELECT @RowsAffected AS RowsAffected;");
            sb.AppendLine("");
            sb.AppendLine("END;");
            sb.AppendLine("GO");

            sb.AppendLine("");
        }




        public string GenerateCode()
        {
            CreateSP_AddNew();
            CreateSP_GetInfoByID();
            CreateSP_Update();
            CreateSP_GetAll();
            CreateSP_Delete();

            return sb.ToString();
        }


    }
}
