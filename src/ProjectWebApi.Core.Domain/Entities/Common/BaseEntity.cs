using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Domain.Entities.Common
{
    /// <summary>
    /// Project base entity
    /// </summary>
    public abstract class BaseEntity : Entity, IHasCreationTime, IHasModificationTime
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
