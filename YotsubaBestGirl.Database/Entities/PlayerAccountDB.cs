using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_player_account")]
    public class PlayerAccountDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("uuid")]
        public string? Uuid { get; set; }
        
        // username? password? didnt pcap account login yet idk
    }
}
