using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace YotsubaBestGirl.Database.Entities
{
    public abstract class UserOwnedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int Uid { get; set; }

        [JsonIgnore]
        [ForeignKey("Uid")]
        public UserDB User { get; set; }
        // above 3 are all required for all entities for the database to function
    }
}
