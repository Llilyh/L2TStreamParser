using System;
using System.Linq;
using LinqToTwitter;

namespace L2TStreamParser
{
    public class StreamingParser
    {

        #region attributes
        private readonly IQueryable<Streaming> _stream;
        private Boolean _close;
        #endregion

        #region constructor
        private StreamingParser(IQueryable<Streaming> stream)
        {
            _stream = stream;
        }

        public static StreamingParser CreateParser(IQueryable<Streaming> stream)
        {
            return new StreamingParser(stream);
        }
        #endregion

        #region methodes
        public void ParseContent()
        {

            _stream.StartAsync(async strm =>
                                     {
                                         if (ReceivedResult != null)
                                             ReceivedResult(this, new StreamingResult(strm.Content));
                                     });
        }
        #endregion

        #region Event Handler
        public event EventHandler<StreamingResult> ReceivedResult;
        #endregion
    }
}
