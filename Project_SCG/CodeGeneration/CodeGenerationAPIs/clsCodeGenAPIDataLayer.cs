using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    public class clsCodeGenAPIDataLayer 
    {
        public string ClassName { get; set; }
        public string Properties { get; set; } //(example: ID:int,Name:string,Email:string)
        public string TableName { get; set; }

        private StringBuilder sb { get; }

        private string _PropID;
        private string _nameMethod;
        private string[] props;
        private string _nameDTO;

        public clsCodeGenAPIDataLayer(string ClassName, string Properties, string NameTable)
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

        private StringBuilder CreateDataSettingsClass()//clsDataSettings
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public class clsDataSettings");
            sb.AppendLine("{");
            sb.AppendLine("  static public string ConnectionString = \"Server=.;Database=StudentsDB;Trusted_Connection=True;TrustServerCertificate=True;\";");
            sb.AppendLine("}");

            return sb;
        }

        //WriteOnEventLog

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



        private void CreateClass()//cls[Name]sData
        {
            sb.AppendLine("");
            sb.AppendLine($"public class {ClassName}");
            sb.AppendLine("{");

        }

        private void CreateGetInfoByIDMethod()
        {
            string PropID = _PropID;


            string nameMethod = _nameMethod;

            sb.AppendLine($"\n  public static {_nameMethod}DTO? Get{nameMethod}InfoBy{PropID}(int {PropID})");
            sb.AppendLine("  {");

            sb.AppendLine("");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"SP_Get{nameMethod}ByID\";");
            sb.AppendLine("");
            sb.AppendLine("    SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine("    command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine($"    command.Parameters.AddWithValue(\"@{nameMethod}ID\", {PropID});");
            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        SqlDataReader reader = command.ExecuteReader();");
            sb.AppendLine("");
            sb.AppendLine("        if (reader.Read())");
            sb.AppendLine("        {");

            StringBuilder ArgumentsDTO = new StringBuilder();

            var filteredProps = props.Skip(1);
            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                ArgumentsDTO.Append($", ({propType})reader[\"{propName}\"]");
            }

            sb.AppendLine($"            return new {_nameMethod}DTO({PropID} {ArgumentsDTO});");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("      reader.Close();");
            sb.AppendLine("    }");

            sb.AppendLine("    catch//(Exception ex)");
            sb.AppendLine("    {");
            sb.AppendLine("         return null;");
            sb.AppendLine("    }");


            sb.AppendLine("    finally");
            sb.AppendLine("    {");
            sb.AppendLine("         connection.Close();");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    return null;");
            sb.AppendLine("  }");

        }

        private void CreateAddNewMethod()
        {
            string PropID = _PropID;
            string nameMethod = _nameMethod;

            sb.AppendLine("");

            sb.AppendLine($"\n  public static int AddNew{nameMethod} ({_nameMethod}DTO {_nameDTO})");
            sb.AppendLine("  {");//bool isFound = false;
            sb.AppendLine("    int ID = -1;");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"SP_Add{nameMethod}\" ;");

            sb.AppendLine("");
            sb.AppendLine("    SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine("    command.CommandType = CommandType.StoredProcedure;");

            sb.AppendLine("");
            sb.AppendLine("");

            var filteredProps = props.Skip(1);
            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    command.Parameters.AddWithValue(\"@{propName}\", {_nameDTO}.{propName});");

            }

            sb.AppendLine("");

            sb.AppendLine($"    SqlParameter outputParameter = new SqlParameter(\"@New{_nameMethod}ID\", SqlDbType.Int)");
            sb.AppendLine("    {");
            sb.AppendLine("        Direction = ParameterDirection.Output");
            sb.AppendLine("    };");
            sb.AppendLine("    command.Parameters.Add(outputParameter);");

            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        command.ExecuteNonQuery();");
            sb.AppendLine("");
            sb.AppendLine("        if (outputParameter.Value != DBNull.Value)");
            sb.AppendLine("        {");
            sb.AppendLine("            ID = (int)outputParameter.Value;");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("    }");
            sb.AppendLine("    catch//(Exception ex)");
            sb.AppendLine("    { ID = -1; }");
            sb.AppendLine("    finally");
            sb.AppendLine("    { connection.Close(); }");
            sb.AppendLine("");
            sb.AppendLine("    return ID;");
            sb.AppendLine("  }");

            sb.AppendLine("");
        }

        private void CreateUpdateMethod()
        {
            string nameMethod = _nameMethod;
            

            sb.AppendLine($"\n  public static bool Update{nameMethod}({_nameMethod}DTO {_nameDTO})");
            sb.AppendLine("  {");
            sb.AppendLine("    int RowAffected = 0;");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"SP_Update{nameMethod}\";");
            sb.AppendLine("");
            sb.AppendLine("    SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine("    command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine("");

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    command.Parameters.AddWithValue(\"@{propName}\", {_nameDTO}.{propName});");
            }

            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        object result = command.ExecuteScalar();");
            sb.AppendLine("");
            sb.AppendLine("        if (result != null && int.TryParse(result.ToString(), out int affectedRows))");
            sb.AppendLine("        {");
            sb.AppendLine("              RowAffected = affectedRows;");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("    catch//(Exception ex)");
            sb.AppendLine("    { return false; }");
            sb.AppendLine("    finally");
            sb.AppendLine("    { connection.Close(); }");
            sb.AppendLine("");
            sb.AppendLine("    return (RowAffected > 0);");
            sb.AppendLine("  }");

            sb.AppendLine("");
        }

        private void CreateDeleteMethod()
        {
            string PropID = _PropID;
            string nameMethod = _nameMethod;

            sb.AppendLine($"\n  public static bool Delete{nameMethod}(int {PropID})");
            sb.AppendLine("  {");
            sb.AppendLine("    int RowAffected = 0;");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"SP_Delete{nameMethod}\";");
            sb.AppendLine("");
            sb.AppendLine("    SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine("    command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine("");
            sb.AppendLine($"    command.Parameters.AddWithValue(\"@{PropID}\", {PropID});");
            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        object result = command.ExecuteScalar();");
            sb.AppendLine("");
            sb.AppendLine("        if (result != null && int.TryParse(result.ToString(), out int affectedRows))");
            sb.AppendLine("        {");
            sb.AppendLine("              RowAffected = affectedRows;");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("    catch//(Exception ex)");
            sb.AppendLine("    { return false; }");
            sb.AppendLine("    finally");
            sb.AppendLine("    { connection.Close(); }");
            sb.AppendLine("");
            sb.AppendLine("    return (RowAffected > 0);");
            sb.AppendLine("  }");

            sb.AppendLine("");
        }

        private void CreateGetAllMethod()
        {
            string PropID = _PropID;
            
            sb.AppendLine($"\n  public static List<{_nameMethod}DTO> GetAll{TableName}()");
            sb.AppendLine("  {");
            sb.AppendLine($"    var {_nameMethod}List = new List<{_nameMethod}DTO>();");
            sb.AppendLine("");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"SP_GetAll{_nameMethod}s\";");

            sb.AppendLine("");
            sb.AppendLine("       SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine("       command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        SqlDataReader reader = command.ExecuteReader();");
            sb.AppendLine("");
            sb.AppendLine("        while (reader.Read())");
            sb.AppendLine("        {");
            sb.AppendLine("");


            StringBuilder ArgumentsDTO = new StringBuilder();

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"            {propType} {propName} = ({propType})reader[\"{propName}\"];");

                if (ArgumentsDTO.Length == 0)
                    ArgumentsDTO.Append($"{propName}");
                else
                    ArgumentsDTO.Append($", {propName}");


                //if (ArgumentsDTO.Length == 0)
                //    ArgumentsDTO.Append($"({propType})reader[\"{propName}\"]");
                //else
                //    ArgumentsDTO.Append($", ({propType})reader[\"{propName}\"]");

            }


            sb.AppendLine("");

            sb.AppendLine($"            {_nameMethod}List.Add(new {_nameMethod}DTO({ArgumentsDTO}));");

            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        reader.Close();");
            sb.AppendLine("    }");
            sb.AppendLine("    catch//(Exception ex)");
            sb.AppendLine("    {  }");
            sb.AppendLine("    finally");
            sb.AppendLine("    { connection.Close(); }");
            sb.AppendLine("");
            sb.AppendLine($"    return {_nameMethod}List;");
            sb.AppendLine("  }");
        }

        public string GenerateCode()
        {
            CreateClassDTO();
            CreateClass();
            CreateGetInfoByIDMethod();
            CreateAddNewMethod();
            CreateUpdateMethod();
            CreateDeleteMethod();
            CreateGetAllMethod();

            sb.AppendLine("}");

            return sb.ToString();
        }

        public string GenerateDataSettingsClass()
        {
            return CreateDataSettingsClass().ToString();
        }



    }

}
