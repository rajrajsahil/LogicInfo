using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Techdome.API.Model
{
    public class UnitMaster
    {
        [Key]
        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Group { get; set; }
        [StringLength(100)]
        public string Desc { get; set; }


    }
    public class UnitMasterInput
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Desc { get; set; }
    }
}
