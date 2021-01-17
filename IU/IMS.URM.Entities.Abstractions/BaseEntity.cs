using System;

namespace IMS.URM.Entities.Abstractions
{
    public abstract class BaseEntity
    {
        public object Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public bool IsArchived { get; set; }
    }
}
