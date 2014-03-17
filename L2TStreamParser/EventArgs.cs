using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace L2TStreamParser
{
    public class StreamingResultEventArgs : EventArgs
    {
        public string Json { get; private set; }

        public StreamingResultEventArgs(string json)
        {
            Json = json;
        }
    }

    public class Geo
    {
        [XmlArray("coordinates")]
        [XmlArrayItem("item")]
        public List<string> coordinates { get; set; }

        public double latitude
        {
            get
            {
                if (coordinates != null && coordinates.Count > 0)
                {
                    return double.Parse(coordinates[0]);
                }
                else
                {
                    return -200;
                }
            }
        }

        public double longitude
        {
            get
            {
                if (coordinates != null && coordinates.Count > 0)
                {
                    return double.Parse(coordinates[1]);
                }
                else
                {
                    return -200;
                }
            }
        }

    }

    public class User
    {
        [XmlElement("id")]
        public string id_long { get; set; }

        [XmlElement("id_str")]
        public string id_str { get; set; }

        [XmlElement("name")]
        public string name { get; set; }

        [XmlElement("screen_name")]
        public string screen_name { get; set; }

        [XmlElement("profile_image_url")]
        public string avatar { get; set; }
    }

    [XmlRoot("root")]
    public class Tweet
    {
        [XmlElement("created_at")]
        public string CreatedAt { get; set; }

        [XmlIgnore]
        public DateTime CreatedAtTime
        {
            get
            {
                return DateTime.Parse(this.CreatedAt);
            }
        }

        [XmlElement("id")]
        public long id_long { get; set; }

        [XmlElement("id_str")]
        public string id_str { get; set; }

        [XmlElement("text")]
        public string text { get; set; }

        [XmlElement("source")]
        public string source { get; set; }

        [XmlElement("user")]
        public User User { get; set; }

        [XmlElement("geo")]
        public Geo Geo { get; set; }
    }

    public class StreamingResult : StreamingResultEventArgs
    {
        public Tweet Tweet;

        public StreamingResult(string json)
            : base(json)
        {
            var parsed = Serializer.JsonToXml(json);

            try
            {
                Tweet = Serializer.DeserializeFromString<Tweet>(parsed.ToString());
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }

}