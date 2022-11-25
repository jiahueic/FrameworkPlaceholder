using AGTIV.Framework.MVC.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Shared
{
    public class Image : Entity
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Extension { get; set; }

        [DataMember]
        [NotMapped]
        public string TitleWithExtension
        {
            get
            {
                return Title + Extension;
            }
        }

        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public byte[] FileBytes { get; set; }

        [DataMember]
        public Guid UserProfileId { get; set; }

        [ForeignKey("UserProfileId")]
        public virtual UserProfile UserProfile { get; set; }
    }
}
