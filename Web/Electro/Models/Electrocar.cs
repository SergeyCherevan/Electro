using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Electro.Models
{
    public class Electrocar
    {
        [Key]
        public string VinCode { get; set; }

        [ForeignKey("User")]
        public virtual string OwnerId { get; set; }

        public string Name { get; set; }
        public string Model { get; set; }
        public int BtteryCapacity { get; set; }
        public int NumberOfRechargeCylcles { get; set; }

        [JsonIgnore]
        public virtual User Owner{ get; set; }
    }
}
