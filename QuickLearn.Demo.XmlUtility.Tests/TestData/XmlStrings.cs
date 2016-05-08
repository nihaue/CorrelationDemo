using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLearn.Demo.XmlUtility.Tests.TestData
{
    public static class XmlStrings
    {
        public static string SCHEMA_STRING = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<xs:schema xmlns=\"http://schemas.quicklearn.com/correlation/demo\" xmlns:b=\"http://schemas.microsoft.com/BizTalk/2003\" xmlns:ns0=\"http://schemas.quicklearn.com/properties/demo\" targetNamespace=\"http://schemas.quicklearn.com/correlation/demo\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">\r\n<xs:annotation>\r\n<xs:appinfo>\r\n<b:imports>\r\n<b:namespace prefix=\"ns0\" uri=\"http://schemas.quicklearn.com/properties/demo\" location=\".\\psCorrelateOrderId.xsd\" />\r\n</b:imports>\r\n</xs:appinfo>\r\n</xs:annotation>\r\n<xs:element name=\"PrintJob\">\r\n<xs:annotation>\r\n<xs:appinfo>\r\n<b:properties>\r\n<b:property name=\"ns0:OrderId\" xpath=\"/*[local-name()='PrintJob' and namespace-uri()='http://schemas.quicklearn.com/correlation/demo']/*[local-name()='OrderId' and namespace-uri()='']\" />\r\n</b:properties>\r\n</xs:appinfo>\r\n</xs:annotation>\r\n<xs:complexType>\r\n<xs:sequence>\r\n<xs:element name=\"OrderId\" type=\"xs:string\" />\r\n</xs:sequence>\r\n</xs:complexType>\r\n</xs:element>\r\n</xs:schema>";
        public static string DOCUMENT_STRING = "<ns0:PrintJob xmlns:ns0=\"http://schemas.quicklearn.com/correlation/demo\">\r\n<OrderId>OrderId_0</OrderId>\r\n</ns0:PrintJob>";
        public static string SCHEMA_LISTING = "<?xml version=\"1.0\" encoding=\"utf-8\"?><EnumerationResults ContainerName=\"http://qlis.blob.core.windows.net/schemas\"><Blobs><Blob><Name>PrintJob.xsd</Name><Url>http://qlis.blob.core.windows.net/schemas/PrintJob.xsd</Url><Properties><Last-Modified>Sun, 24 Apr 2016 06:13:06 GMT</Last-Modified><Etag>0x8D36C07809DA3F7</Etag><Content-Length>2214</Content-Length><Content-Type>application/octet-stream</Content-Type><Content-Encoding /><Content-Language /><Content-MD5 /><Cache-Control /><BlobType>BlockBlob</BlobType><LeaseStatus>unlocked</LeaseStatus></Properties></Blob></Blobs><NextMarker /></EnumerationResults>";
    }
}
