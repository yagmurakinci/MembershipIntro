using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.Entities
{
    public abstract class Base<T>
    {
        //Not: ClassAdı<T> generic class demektir.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order=1)]
        public T Id { get; set; }
        [Column(Order = 2)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public bool IsRemoved { get; set; }
    }

}
