using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.Entities
{
    [Table("MembersPhones")]
    public class MemberPhone : Base<int>
    {
        public byte PhoneTypeId { get; set; } //FK
        //0 (53X) XXX YY ZZ
        //0 (21X) AAA BB CC
        [StringLength(17, MinimumLength =17,
            ErrorMessage = "Telefon numarası 17 karakter olmalıdır. ÖRNEK: 0 (XXX) XXX YY ZZ ")]
        public string Phone { get; set; }
        public string MemberId { get; set; } //FK

        // ilişkileri belirtelim
        [ForeignKey("PhoneTypeId")]
        public virtual PhoneType PhoneType { get; set; }
        
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}
