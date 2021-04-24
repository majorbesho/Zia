using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class VideoUploader
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }
        public bool IsActive { get; set; }

        

    }
}
