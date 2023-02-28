using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.Entities
{
    [Table("PhoneTypes")]
    public class PhoneType: Base<byte> 
    {
        [Required]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Telefon türü min 2 mak 50 karakterdir!")]
        public string Name { get; set; }

        //İlişkiler "n"
        //buraya hiç bir şey yazmayabilirsiniz
        // yazmak isterseniz
        public virtual List<MemberPhone> MemberPhones { get; set; }
    }
}
