using System.ComponentModel.DataAnnotations;

namespace DLL.Interfaces
{
    /// <summary>
    /// IEntity Intarface used for Entity in DB, has only one key Id.
    /// </summary>
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
