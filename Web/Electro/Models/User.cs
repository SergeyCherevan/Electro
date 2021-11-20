using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Electro.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        [JsonIgnore]
        [InverseProperty("Owner")]
        public virtual ICollection<Electrocar> Electrocars { get; set; }
    }
}
