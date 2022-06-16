using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DLL.Interfaces
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
