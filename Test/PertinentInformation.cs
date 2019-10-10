using MARC.Everest.Attributes;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;

namespace Test
{

    [Structure(Name = "PertinentInformation", NamespaceUri = "http://www.csioz.gov.pl/xsd/extPL/r1", Model = "POCD_MT000040PL")]
    public class PertinentInformation : PatientRole
    {
        [Property(Name = "coveragePlan", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.Structural, SortKey = 1)]

        public CoveragePlan CoveragePlan { get;  set; }
    }
}