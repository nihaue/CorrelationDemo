namespace QuickLearn.Demo.Messaging {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://schemas.quicklearn.com/properties/demo",@"PrintJobUpdate")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::QuickLearn.Demo.Messaging.OrderId), XPath = @"/*[local-name()='PrintJobUpdate' and namespace-uri()='http://schemas.quicklearn.com/properties/demo']/*[local-name()='OrderId' and namespace-uri()='']", XsdType = @"string")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"PrintJobUpdate"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"QuickLearn.Demo.Messaging.psCorrelateOrderId", typeof(global::QuickLearn.Demo.Messaging.psCorrelateOrderId))]
    public sealed class PrintJobUpdate : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:ns0=""http://schemas.quicklearn.com/properties/demo"" targetNamespace=""http://schemas.quicklearn.com/properties/demo"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:annotation>
    <xs:appinfo>
      <b:imports>
        <b:namespace prefix=""ns0"" uri=""http://schemas.quicklearn.com/properties/demo"" location=""QuickLearn.Demo.Messaging.psCorrelateOrderId"" />
      </b:imports>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""PrintJobUpdate"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property name=""ns0:OrderId"" xpath=""/*[local-name()='PrintJobUpdate' and namespace-uri()='http://schemas.quicklearn.com/properties/demo']/*[local-name()='OrderId' and namespace-uri()='']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""OrderId"" type=""xs:string"" />
        <xs:element name=""Body"" type=""xs:string"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public PrintJobUpdate() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "PrintJobUpdate";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
    }
}
