using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace L2TStreamParser
{
    internal static class Serializer
    {
        public static XElement JsonToXml(string json)
        {
            using (
                var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json),
                    XmlDictionaryReaderQuotas.Max))
                return XElement.Load(reader);
        }

        /// <summary>
        /// Deserializes from string.
        /// 
        /// </summary>
        /// <param name="type">The type.</param><param name="content">The content.</param>
        /// <returns/>
        public static object DeserializeFromString(Type type, string content)
        {
            try
            {
                using (StringReader stringReader = new StringReader(content))
                    return new XmlSerializer(type).Deserialize((TextReader)stringReader);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Cannot deserialize the given string: '{0}'.\r\n  {1}\r\n", (object)ex.Message, (object)ex.InnerException));
            }
        }

        /// <summary>
        /// Deserializes from string.
        /// 
        /// </summary>
        /// <typeparam name="T"/><param name="content">The content.</param>
        /// <returns/>
        public static T DeserializeFromString<T>(string content)
        {
            try
            {
                using (var stringReader = new StringReader(content))
                    return (T)new XmlSerializer(typeof(T)).Deserialize((TextReader)stringReader);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Cannot deserialize the given string: '{0}'.\r\n  {1}\r\n", (object)ex.Message, (object)ex.InnerException));
            }
        }

        /// <summary>
        /// Serializes to string.
        /// 
        /// </summary>
        /// <param name="type">The type.</param><param name="content">The content.</param>
        /// <returns/>
        public static string SerializeToString(Type type, object content)
        {
            return Serializer.InternalSerializer(type, content);
        }

        /// <summary>
        /// Serializes to string.
        /// 
        /// </summary>
        /// <typeparam name="T"/><param name="content">The content.</param>
        /// <returns/>
        public static string SerializeToString<T>(T content)
        {
            return Serializer.InternalSerializer(typeof(T), (object)content);
        }

        /// <summary>
        /// Internals the serializer.
        /// 
        /// </summary>
        /// <param name="type">The type.</param><param name="content">The content.</param>
        /// <returns/>
        private static string InternalSerializer(Type type, object content)
        {
            try
            {
                StringWriter stringWriter = new StringWriter();
                new XmlSerializer(type).Serialize((TextWriter)stringWriter, content);
                return stringWriter.ToString();
            }
            catch (Exception ex)
            {
                var newException = new Exception(string.Format("Cannot serialize the given object: '{0}'.\r\n  {1}\r\n", (object)ex.Message, (object)ex.InnerException));
                Debug.WriteLine(newException.ToString());
                throw newException;
            }
        }
    }
}
