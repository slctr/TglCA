
using System.Runtime.Serialization;

namespace TglCA.Bll.Interfaces.Entities.Chart
{
    [DataContract]
    public class ChartPoint<Tx,Ty>
    {
        [DataMember(Name = "x")]
        public Tx X { get; }
        [DataMember(Name = "y")]
        public Ty Y { get; }

        public ChartPoint(Tx x, Ty y)
        {
            X = x;
            Y = y;
        }
    }
}
