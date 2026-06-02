using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    public class clsCodeGenDataLayer
    {
        public string ClassName { get; set; }
        public string Properties { get; set; } //(example: ID:int,Name:string,Email:string)
        public string TableName { get; set; }

        public bool GenerateEventLogCode { get; set; } // Add WriteOnEventLog in code

        private string _sourceName; 
        
        public string SourceName
        {
            get
            {
                return _sourceName;
            }
            set
            {
                _sourceName = value;

                //GenerateEventLogCode = !string.IsNullOrWhiteSpace(value);
            }
        }


        private StringBuilder sb { get; }

        private string _PropID;
        private string _nameMethod;
        private string[] props;

        public clsCodeGenDataLayer(string ClassName, string Properties , string NameTable , string SourceName)
        {
            this.ClassName = ClassName + "Data";
            this.Properties = Properties;
            this.TableName = NameTable;
            this.SourceName = SourceName;

            sb = new StringBuilder();
            props = Properties.Split(',');

           _PropID = props[0].Split(':')[0];
           _nameMethod = ClassName.Replace("cls", "").Replace("Data", "");

        }

        private StringBuilder CreateDataSettingsClass()//clsDataSettings
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public class clsDataSettings");
            sb.AppendLine("{");
            sb.AppendLine("  static public string ConnectionString = \"Server=.;Database=NameDatabase;Trusted_Connection=True;\"");
            sb.AppendLine("}");

            return sb;
        }

        //WriteOnEventLog


        private void CreateClass()//cls[Name]sData
        {
            sb.AppendLine($"public class {ClassName}");
            sb.AppendLine("{");

        }

        private void CreateGetInfoByIDMethod()
        {
            string PropID = _PropID;

            string PropsData = "";

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                if (PropsData == "")
                    PropsData = propType + " " + propName;
                else
                    PropsData += ", ref " + propType + " " + propName;

            }

            string nameMethod = _nameMethod;

            sb.AppendLine($"\n  public static bool Get{nameMethod}InfoBy{PropID}({PropsData})");
            sb.AppendLine("  {");//bool isFound = false;
            sb.AppendLine("    bool isFound = false;");
            sb.AppendLine("");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"SELECT * FROM {TableName} WHERE {PropID} = @{PropID} ;\";");
            sb.AppendLine("");
            sb.AppendLine("    SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine($"    command.Parameters.AddWithValue(\"@{PropID}\", {PropID});");
            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        SqlDataReader reader = command.ExecuteReader();");
            sb.AppendLine("");
            sb.AppendLine("        if (reader.Read())");
            sb.AppendLine("        {");
            sb.AppendLine("            isFound = true;");
            sb.AppendLine("");

            var filteredProps = props.Skip(1);
            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"            {propName} = ({propType})reader[\"{propName}\"];");

            }

            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("      reader.Close();");
            sb.AppendLine("    }");

            //"clsUtil.WriteOnEventLog("DVLD", ex.Message, EventLogEntryType.Error);"

            if (GenerateEventLogCode)
            {
                sb.AppendLine("    catch(Exception ex)"); 
                sb.AppendLine("    {");
                sb.AppendLine("         isFound = false;");
                sb.AppendLine($"        clsLogger.WriteOnEventLog(\"{SourceName}\", ex.Message, EventLogEntryType.Error);   ");
                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine("    catch//(Exception ex)"); 
                sb.AppendLine("    {");
                sb.AppendLine("        isFound = false;");
                sb.AppendLine("    }");
            }

            sb.AppendLine("    finally");
            sb.AppendLine("    {");
            sb.AppendLine("         connection.Close();");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    return isFound;");
            sb.AppendLine("  }");

        }

        private void CreateAddNewMethod()
        {
            string PropID = _PropID;

            string PropsData = "" , Query = "";

            var filteredProps = props.Skip(1);
            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                if (PropsData == "")
                    PropsData = propType + " " + propName;
                else
                    PropsData += "," + propType + " " + propName;

                if (Query == "")
                    Query =  "@"+propName;
                else
                    Query += ", @" + propName;

            }

            string nameMethod = _nameMethod;

            sb.AppendLine($"\n  public static int AddNew{nameMethod} ({PropsData})");
            sb.AppendLine("  {");//bool isFound = false;
            sb.AppendLine("    int ID = -1;");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"INSERT INTO {TableName} VALUES ({Query}); \" +");
            sb.AppendLine("                    \"SELECT SCOPE_IDENTITY();\";");
            sb.AppendLine("");
            sb.AppendLine("    SqlCommand command = new SqlCommand(query, connection);");

            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    command.Parameters.AddWithValue(\"@{propName}\", {propName});");

            }

            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        object Result = command.ExecuteScalar();");
            sb.AppendLine("");
            sb.AppendLine("        if (Result != null && int.TryParse(Result.ToString(), out int insertedID))");
            sb.AppendLine("        {");
            sb.AppendLine("            ID = insertedID;");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("    }");

            if (GenerateEventLogCode)
            {
                sb.AppendLine("    catch(Exception ex)");
                sb.AppendLine("    {");
                sb.AppendLine("         ID = -1;");
                sb.AppendLine($"        clsLogger.WriteOnEventLog(\"{SourceName}\", ex.Message, EventLogEntryType.Error);   ");
                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine("    catch//(Exception ex)");
                sb.AppendLine("    { ID = -1; }");
            }

            sb.AppendLine("    finally");
            sb.AppendLine("    { connection.Close(); }");
            sb.AppendLine("");
            sb.AppendLine("    return ID;");
            sb.AppendLine("  }");
        }

        private void CreateUpdateMethod()
        {
            string nameMethod = ClassName.Replace("cls", "").Replace("Data", "");

            string PropsData = "", Query = "" , WhereQuery = "";

            
            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                if (PropsData == "")
                    PropsData = propType + " " + propName;
                else
                    PropsData += "," + propType + " " + propName;

                if (WhereQuery == "") 
                    WhereQuery = propName + " = @" + propName;
                else if(Query == "")
                    Query = propName + "=@" + propName;
                else
                    Query += " ," + propName + "=@" + propName;

            }


            sb.AppendLine($"\n  public static bool Update{nameMethod}({PropsData})");
            sb.AppendLine("  {");
            sb.AppendLine("    int RowAffected = 0;");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"UPDATE {TableName} SET {Query} \" +");
            sb.AppendLine($"                   \"WHERE {WhereQuery};\";");
            sb.AppendLine("");
            sb.AppendLine("    SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine("");

            foreach (var prop in props)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"    command.Parameters.AddWithValue(\"@{propName}\", {propName});");
            }

            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        RowAffected = command.ExecuteNonQuery();");     
            sb.AppendLine("    }");

            if (GenerateEventLogCode)
            {
                sb.AppendLine("    catch(Exception ex)");
                sb.AppendLine("    {");
                sb.AppendLine("         return false;");
                sb.AppendLine($"        clsLogger.WriteOnEventLog(\"{SourceName}\", ex.Message, EventLogEntryType.Error);   ");
                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine("    catch//(Exception ex)");
                sb.AppendLine("    { return false; }");
            }

            sb.AppendLine("    finally");
            sb.AppendLine("    { connection.Close(); }");
            sb.AppendLine("");
            sb.AppendLine("    return (RowAffected > 0);");
            sb.AppendLine("  }");
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
            sb.AppendLine($"    string query = \"DELETE FROM {TableName} WHERE {PropID + "= @" + PropID};\";");
           
            sb.AppendLine("");
            sb.AppendLine("     SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine("");
            sb.AppendLine($"    command.Parameters.AddWithValue(\"@{PropID}\", {PropID});");
            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        RowAffected = command.ExecuteNonQuery();");
            sb.AppendLine("    }");

            if (GenerateEventLogCode)
            {
                sb.AppendLine("    catch(Exception ex)");
                sb.AppendLine("    {");
                sb.AppendLine("         return false;");
                sb.AppendLine($"        clsLogger.WriteOnEventLog(\"{SourceName}\", ex.Message, EventLogEntryType.Error);   ");
                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine("    catch//(Exception ex)");
                sb.AppendLine("    { return false; }");
            }

            sb.AppendLine("    finally");
            sb.AppendLine("    { connection.Close(); }");
            sb.AppendLine("");
            sb.AppendLine("    return (RowAffected > 0);");
            sb.AppendLine("  }");
        }

        private void CreateGetAllMethod()
        {
            string PropID = _PropID;
            //string nameMethod = _nameMethod;

            sb.AppendLine($"\n  public static DataTable GetAll{TableName}()");
            sb.AppendLine("  {");
            sb.AppendLine("    DataTable dt = new DataTable();");
            sb.AppendLine("");
            sb.AppendLine("    SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);");
            sb.AppendLine("");
            sb.AppendLine($"    string query = \"SELECT * FROM {TableName} ;\";");

            sb.AppendLine("");
            sb.AppendLine("       SqlCommand command = new SqlCommand(query, connection);");
            sb.AppendLine("");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        connection.Open();");
            sb.AppendLine("        SqlDataReader reader = command.ExecuteReader();");
            sb.AppendLine("");
            sb.AppendLine("        if (reader.HasRows)");
            sb.AppendLine("        {");
            sb.AppendLine("           dt.Load(reader);");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        reader.Close();");
            sb.AppendLine("    }");

            if (GenerateEventLogCode)
            {
                sb.AppendLine("    catch(Exception ex)");
                sb.AppendLine("    {");
                sb.AppendLine($"        clsLogger.WriteOnEventLog(\"{SourceName}\", ex.Message, EventLogEntryType.Error);   ");
                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine("    catch//(Exception ex)");
                sb.AppendLine("    {  }");
            }

            sb.AppendLine("    finally");
            sb.AppendLine("    { connection.Close(); }");
            sb.AppendLine("");
            sb.AppendLine("    return dt;");
            sb.AppendLine("  }");
        }

        public string GenerateCode()
        {
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
