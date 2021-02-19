using System;

namespace Marck7JR.Core.Data
{
    public interface IEntity
    {
        public Guid? Guid { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
