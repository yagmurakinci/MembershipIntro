using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.ResultModels
{
    public interface IDataResult<T> :IResult
    {
        T Data { get; set; }
    }
}
