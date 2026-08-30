using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    public class clsCodeGenAPIsController
    {
        public string ClassName { get; set; }
        public string Properties { get; set; } //(example: ID:int,Name:string,Email:string)
        public string TableName { get; set; }

        private StringBuilder sb { get; }

        private string _PropID;
        private string _nameMethod;
        private string[] props;
        private string _nameDTO;

        public clsCodeGenAPIsController(string ClassName, string Properties, string NameTable)
        {
            this.ClassName = ClassName;
            this.Properties = Properties;
            this.TableName = NameTable;

            sb = new StringBuilder();
            props = Properties.Split(',');

            _PropID = props[0].Split(':')[0];
            _nameMethod = ClassName.Replace("cls", "");

            _nameDTO = _nameMethod[0].ToString().ToUpper() + "DTO";

        }


        private void CreateClass()
        {
            sb.AppendLine("");
            sb.AppendLine("//[Route(\"api/[controller]\")]");
            sb.AppendLine($"[Route(\"api/{_nameMethod}s\")]");
            sb.AppendLine("[ApiController]");
            sb.AppendLine($"public class {_nameMethod}sController : ControllerBase");
            sb.AppendLine("{");
            sb.AppendLine("");

        }

        private void CreateGetAllMethod()
        {
            sb.AppendLine($"    [HttpGet(\"All\",Name = \"GetAll{_nameMethod}s\")]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status200OK)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status404NotFound)]");
            sb.AppendLine("");
            sb.AppendLine($"    public ActionResult<IEnumerable<{_nameMethod}DTO>> GetAll{_nameMethod}s()");
            sb.AppendLine("    {");
            sb.AppendLine($"      List<{_nameMethod}DTO> {_nameMethod}List = {ClassName}.GetAll{_nameMethod}s();");
            sb.AppendLine("");
            sb.AppendLine($"      if({_nameMethod}List.Count == 0)");
            sb.AppendLine($"          return NotFound(\"No {_nameMethod}s Found!\");");
            sb.AppendLine("");
            sb.AppendLine($"      return Ok({_nameMethod}List);");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
        }

        private void CreateGetInfoByIDMethod()
        {
            sb.AppendLine($"    [HttpGet(\"{{ID}}\" , Name = \"Get{_nameMethod}ByID\")]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status200OK)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status404NotFound)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
            sb.AppendLine("");
            sb.AppendLine($"    public ActionResult<{_nameMethod}DTO> Get{_nameMethod}ByID(int ID)");
            sb.AppendLine("    {");
            sb.AppendLine($"      if (ID < 1)");
            sb.AppendLine("       {");
            sb.AppendLine("         return BadRequest($\"Not Accepted ID: {ID}\");");
            sb.AppendLine("       }");
            sb.AppendLine("");
            sb.AppendLine($"       var {_nameMethod} = cls{_nameMethod}.Find(ID);");
            sb.AppendLine("");
            sb.AppendLine($"       if ({_nameMethod} == null)");
            sb.AppendLine("       {");
            sb.AppendLine($"         return NotFound($\"{_nameMethod} with ID: {{ID}} not found.\");");
            sb.AppendLine("       }");
            sb.AppendLine("");
            sb.AppendLine($"       {_nameMethod}DTO {_nameDTO} = {_nameMethod}.{_nameDTO};");
            sb.AppendLine("");
            sb.AppendLine($"       return Ok({_nameDTO});");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
        }

        private void CreateAddNewMethod()
        {
            sb.AppendLine($"    [HttpPost(Name = \"Add{_nameMethod}\")]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status201Created)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
            sb.AppendLine($"    public ActionResult<{_nameMethod}DTO> Add{_nameMethod}({_nameMethod}DTO New{_nameMethod}DTO)");
            sb.AppendLine("    {");
            sb.AppendLine($"      if (New{_nameMethod}DTO == null )//you will add all validation here ");
            sb.AppendLine("      {");
            sb.AppendLine($"         return BadRequest(\"Invalid {_nameMethod} Data.\");");
            sb.AppendLine("      }");
            sb.AppendLine("");
            sb.AppendLine($"      var {_nameMethod} = new cls{_nameMethod}(New{_nameMethod}DTO);");
            sb.AppendLine("");
            sb.AppendLine($"      if(!{_nameMethod}.Save())");
            sb.AppendLine("      {");
            sb.AppendLine($"         return StatusCode(500, new {{ Message = \"Error : Adding {_nameMethod}.\" }});");
            sb.AppendLine("      }");
            sb.AppendLine("");
            sb.AppendLine($"      New{_nameMethod}DTO.{_PropID} = {_nameMethod}.{_PropID};");
            sb.AppendLine("");
            sb.AppendLine($"      return CreatedAtRoute(\"Get{_nameMethod}ByID\" , new {{ID = New{_nameMethod}DTO.{_PropID}}} , New{_nameMethod}DTO);");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
        }

        private void CreateUpdateMethod()
        {
            sb.AppendLine($"    [HttpPut(\"{{ID}}\", Name = \"Update{_nameMethod}\")]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status200OK)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status404NotFound)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
            sb.AppendLine($"    public ActionResult<{_nameMethod}DTO> Update{_nameMethod}(int ID, {_nameMethod}DTO updated{_nameMethod})");
            sb.AppendLine("    {");
            sb.AppendLine($"      if (ID < 1 || updated{_nameMethod} == null ) //you will add all validation here ");
            sb.AppendLine("      {");
            sb.AppendLine($"         return BadRequest(\"Invalid {_nameMethod} data.\");");
            sb.AppendLine("      }");
            sb.AppendLine("");
            sb.AppendLine($"      var {_nameMethod} = {ClassName}.Find(ID);");
            sb.AppendLine("");
            sb.AppendLine($"       if ({_nameMethod} == null)");
            sb.AppendLine("      {");
            sb.AppendLine($"         return NotFound($\"{_nameMethod} with ID {{ID}} not found.\");");
            sb.AppendLine("      }");
            sb.AppendLine("");

            var filteredProps = props.Skip(1);
            foreach (var prop in filteredProps)
            {
                var parts = prop.Split(':');
                string propName = parts[0];
                string propType = parts[1];

                sb.AppendLine($"      {_nameMethod}.{propName} = updated{_nameMethod}.{propName};");
            }

            sb.AppendLine("");
            sb.AppendLine($"      if (!{_nameMethod}.Save())");
            sb.AppendLine("      {");
            sb.AppendLine($"         return StatusCode(500 ,new {{ Message = \"Error : Updating {_nameMethod}.\" }});");
            sb.AppendLine("      }");
            sb.AppendLine("");
            sb.AppendLine($"      return Ok({_nameMethod}.{_nameDTO});");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
          
        }
        private void CreateDeleteMethod()
        {
            sb.AppendLine($"    [HttpDelete(\"{{ID}}\",Name = \"Delete{_nameMethod}\")]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status200OK)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status404NotFound)]");
            sb.AppendLine("    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
            sb.AppendLine($"    public ActionResult Delete{_nameMethod}(int ID)");
            sb.AppendLine("    {");
            sb.AppendLine("      if (ID < 1)");
            sb.AppendLine("      {");
            sb.AppendLine("         return BadRequest($\"Not Accepted ID: {ID}\");");
            sb.AppendLine("      }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine($"      if ({ClassName}.Delete{_nameMethod}(ID))");
            sb.AppendLine("      {");
            sb.AppendLine($"         return Ok($\"{_nameMethod} with ID: {{ID}} has been Deleted.\");");
            sb.AppendLine("      }");
            sb.AppendLine("      else");
            sb.AppendLine("      {");
            sb.AppendLine($"         return NotFound($\"{_nameMethod} with ID {{ID}} not found. no rows deleted!\");");
            sb.AppendLine("      }");
            sb.AppendLine("");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            
        }


        public string GenerateCode()
        {
            CreateClass();
            CreateGetAllMethod();
            CreateGetInfoByIDMethod();
            CreateAddNewMethod();
            CreateUpdateMethod();
            CreateDeleteMethod();

            sb.AppendLine("}");

            return sb.ToString();
        }

    }
}
