using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TglCA.Bll.Interfaces.Entities
{
    public record ChartData
    {
        public DateTime Time { get; init; }
        public decimal Price { get; init; }
    }
}
