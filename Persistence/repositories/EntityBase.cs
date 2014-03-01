using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.repositories
{
    public enum ObjectState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }

    public abstract class EntityBase
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
