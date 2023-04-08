using System.Buffers;
using SuperSocket.ProtoBase;

namespace Du.SuperSocket.Server
{
    public class Filter376:PipelineFilterBase<Package>
    {
        private readonly IPipelineFilter<Package> _switchFilter;

        public Filter376(IPipelineFilter<Package> switcher)
        {
            _switchFilter = switcher;
        }

        public override Package Filter(ref SequenceReader<byte> reader)
        {
            var pack = reader.Sequence.Slice(0, reader.Length);
            try
            {
                var ret = DecodePackage(ref pack);
                return ret;
            }
            finally
            {
                reader.Advance(reader.Length);
            }
        }

        protected override Package DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            return new Package() { Key = $"376_{"01"}" };
        }
    }
}
