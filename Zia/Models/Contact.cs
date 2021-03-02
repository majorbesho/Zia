using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string BodyMessage { get; set; }
        public DateTime DateTimeMssage { get; set; }

        public string MessageSubject { get; set; }
        public string CompanySection { get; set; }
        public enum EnumCompanySection { Marketing=0,DigitalMarketing=1,
            ContentCreation = 2, VideoProduction = 3, Campaign = 4, MediaPlanning = 5
            , PersonalBranding = 6, Branding = 7, WebDesignAndDevelopment = 8, PrintingAndPackaging = 9, Others = 10,
        }


    }
}
