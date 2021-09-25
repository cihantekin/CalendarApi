using System;

namespace Calendar.Core
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
