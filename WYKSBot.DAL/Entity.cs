using System.ComponentModel.DataAnnotations;

namespace WYKSBot.DAL.Models
{
    public abstract class Entity 
    {
        [Key]
        public int Id { get; set; }
    }
}
