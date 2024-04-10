using System.Text.Json.Serialization;

namespace BaseLibrary;

public abstract class Relationship
{
   [JsonIgnore]
   public List<Employee>? Employees { get; set; }
}
