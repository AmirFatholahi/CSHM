using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CSHM.Widget.XML;

public static class XmlWidget
{
    public static string ToXmlPaya<T>(T entity)
    {
        var settings = new XmlWriterSettings
        {
            Indent = false,
            Encoding = Encoding.UTF8,
        };

        var serializer = new XmlSerializer(typeof(T));
        using var stream = new MemoryStream();
        using var xtWriter = XmlWriter.Create(stream, settings);
        xtWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"");
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("", "");
        serializer.Serialize(xtWriter, entity, ns);
        var xml = Encoding.UTF8.GetString(stream.ToArray());
        return xml;
    }

    public static string ToXml<T>(T entity,bool indent,Encoding encoding)
    {
        var settings = new XmlWriterSettings
        {
            Indent = indent,
            Encoding = encoding,
        };

        var serializer = new XmlSerializer(typeof(T));
        using var stream = new MemoryStream();
        using var xtWriter = XmlWriter.Create(stream, settings);
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        serializer.Serialize(xtWriter, entity, ns);
        var xml =encoding.GetString(stream.ToArray());
        return xml;
    }
}