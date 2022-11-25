using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.ElmahLog
{
    [Serializable]
    [DataContract]
    public class Elmah_Error
    {
        [Key]
        [DataMember]
        public Guid ErrorId { get; set; }

        [MaxLength(60)]
        [DataMember]
        public string Application { get; set; }

        [MaxLength(50)]
        [DataMember]
        public string Host { get; set; }

        [MaxLength(100)]
        [DataMember]
        public string Type { get; set; }

        [MaxLength(60)]
        [DataMember]
        public string Source { get; set; }

        [MaxLength(500)]
        [DataMember]
        public string Message { get; set; }

        [MaxLength(50)]
        [DataMember]
        public string User { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public DateTime TimeUtc { get; set; }

        [DataMember]
        public int Sequence { get; set; }

        [Column(TypeName = "ntext")]
        [DataMember]
        public string AllXml { get; set; }
    }
}
