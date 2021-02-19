using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Marck7JR.Core.Data
{
    public abstract class Entity : ObservableObject, IEntity
    {
        private DateTime? createdAt = DateTime.UtcNow;
        private Guid? guid;
        private DateTime? updatedAt;

        public Entity()
        {
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(UpdatedAt))
                {
                    UpdatedAt = DateTime.UtcNow;
                }
            };
        }

        public virtual DateTime? CreatedAt { get => GetValue(ref createdAt); set => SetValue(ref createdAt, value); }
        public virtual Guid? Guid { get => GetValue(ref guid); set => SetValue(ref guid, value); }
        public virtual DateTime? UpdatedAt { get => GetValue(ref updatedAt); set => SetValue(ref updatedAt, value); }

        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Guid), guid);
            info.AddValue(nameof(CreatedAt), createdAt);
            info.AddValue(nameof(UpdatedAt), updatedAt);
        }
    }
}
