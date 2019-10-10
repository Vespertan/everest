using MARC.Everest.Attributes;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;

namespace Test
{
    [Structure(Model = "POCD_MT000040", Name = "Patient", StructureType = StructureAttribute.StructureAttributeType.MessageType)]
    public class MyDataEnterer : DataEnterer
    {
        [Property(Name = "testName", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.Traversable, SortKey = 1, NamespaceUri = "http://www.csioz.gov.pl/xsd/extPL/r1")]
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.csioz.gov.pl/xsd/extPL/r1")]
        //[Realization(Name = "testName")]
        public string Test { get; set; } = "xxx";
    }
}