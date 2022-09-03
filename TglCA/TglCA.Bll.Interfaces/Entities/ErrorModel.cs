using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TglCA.Bll.Interfaces.Entities
{
    public class ErrorModel
    {
        public IEnumerable<ErrorDetail>? ErrorDetails { get; set; }

        public bool IsSuccess
        {
            get
            {
                if (ErrorDetails != null && ErrorDetails.Any())
                {
                    return false;
                }
                return true;
            }
        }
    }
}
