using System.ComponentModel.DataAnnotations.Schema;
using WYKSBot.DAL.Profiles;

namespace WYKSBot.DAL.Models.Items
{
    public class ProfileItem : Entity
    {
        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
    }
}
