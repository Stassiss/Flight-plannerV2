using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
