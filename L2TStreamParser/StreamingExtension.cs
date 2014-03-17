using System.Linq;
using LinqToTwitter;

namespace L2TStreamParser
{
    public static class StreamingExtension
    {
        public static StreamingParser CreateParser(this IQueryable<Streaming> stream)
        {
            return StreamingParser.CreateParser(stream);
        }
    }
}
