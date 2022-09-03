using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TglCA.Dal.Interfaces.Entities.Base;

namespace TglCA.Dal.Interfaces.Entities
{
    public class Currency : BaseEntity
    {
        public string MarketId { get; set; }
        public byte[] Img { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
