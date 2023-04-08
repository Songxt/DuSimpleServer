using System.Buffers;
using SuperSocket.ProtoBase;

namespace Du.SuperSocket.Server
{
    public class SimpleFilter : PipelineFilterBase<Package>
    {
        private IPipelineFilter<Package> _filterA;
        private IPipelineFilter<Package> _filterB;
        public SimpleFilter()
        {
            _filterA = new Filter376(this);
            _filterB = new Filter645(this);
        }

        public override Package Filter(ref SequenceReader<byte> reader)
        {
            if (!reader.TryRead(out byte flag))
                throw new ProtocolException("Flag byte cannot be read.");

            if (flag == 0x01)
                NextFilter = _filterA;
            else if (flag == 0x02)
                NextFilter = _filterB;
            else
                throw new ProtocolException($"Unknown flag at the first postion: {flag}.");

            RedisHelper.Set(flag.ToString(),true);
            reader.Rewind(1);
            return null;
        }
    }
}
