using System.ComponentModel.DataAnnotations;

namespace DLL.Interfaces
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
