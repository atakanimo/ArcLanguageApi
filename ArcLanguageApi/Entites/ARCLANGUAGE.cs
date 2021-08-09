using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARCLanguageApi.Entites
{
    [Table("ARCLANGUAGE")]
    public class ARCLANGUAGE
    {
        public int ID { get; set; }
        public string KEY_NAME { get; set; }
        public string KEY_VALUE { get; set; }
        public string KEY_COMMENT { get; set; }
        public string KEY_VALUE_EN { get; set; }
        public string KEY_VALUE_RU { get; set; }
        public string KEY_VALUE_RO { get; set; }
        public string KEY_VALUE_TR { get; set; }
        public string KEY_VALUE_DE { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string CreaterApp { get; set; }
        public string CreatorAppVersion { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public string UpdaterUser { get; set; }
        public string UpdaterApp { get; set; }
        public string UpdaterAppVersion { get; set; }
    }
}
