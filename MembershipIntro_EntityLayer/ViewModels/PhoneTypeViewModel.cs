using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.ViewModels
{
    public class PhoneTypeViewModel
    {
        public byte Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRemoved { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Telefon türü min 2 mak 50 karakterdir!")]
        public string Name { get; set; }

    }
}
