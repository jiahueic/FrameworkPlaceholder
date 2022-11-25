using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Shared
{
    public interface IEntity
    {
        /// <summary>
        /// Unique Identifier of the entity
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Created On of the entity
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Modified On of the entity
        /// </summary>
        DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Created By of the entity
        /// </summary>
        Guid CreatedBy { get; set; }

        /// <summary>
        /// Modified By of the entity
        /// </summary>
        Guid ModifiedBy { get; set; }

        /// <summary>
        /// Status of the entity
        /// </summary>
        int Status { get; set; }
    }
}
