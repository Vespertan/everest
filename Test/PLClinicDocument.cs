using MARC.Everest.Attributes;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.RMIM.UV.CDAr2.RIM;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    [Structure(Name = "ClinicalDocument", IsEntryPoint = true, Model = "POCD_MT000040UV")]
    public class PLClinicalDocument : MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument
    {
        /// <summary>
        /// Default ctor for clinical document
        /// </summary>
        public PLClinicalDocument() : base()
        {
            this.PertinentInformation = new List<PertinentInformation>();
        }

        /// <summary>
        /// Ctor for required elements
        /// </summary>
        public PLClinicalDocument(II id, CE<string> code, TS effectiveTime, CE<x_BasicConfidentialityKind> confidentialityCode, RecordTarget recordTarget, Author author, Custodian custodian, Component2 component) :
            base(id, code, effectiveTime, confidentialityCode, recordTarget, author, custodian, component)
        {
            this.PertinentInformation = new List<PertinentInformation>();

        }

        /// <summary>
        /// Ctor for all elements
        /// </summary>
        public PLClinicalDocument(II id, CE<string> code, ST title, TS effectiveTime, CE<x_BasicConfidentialityKind> confidentialityCode, CS<string> languageCode, II setId, INT versionNumber, TS copyTime, RecordTarget recordTarget, Author author, DataEnterer dataEnterer, Informant12 informant, Custodian custodian, InformationRecipient informationRecipient, LegalAuthenticator legalAuthenticator, Authenticator authenticator, Participant1 participant, InFulfillmentOf inFulfillmentOf, DocumentationOf documentationOf, RelatedDocument relatedDocument, Authorization authorization, Component1 componentOf, Component2 component)
            : base(id, code, title, effectiveTime, confidentialityCode, languageCode, setId, versionNumber, copyTime, recordTarget, author, dataEnterer, informant, custodian, informationRecipient, legalAuthenticator, authenticator, participant, inFulfillmentOf, documentationOf, relatedDocument, authorization, componentOf, component)
        {
            this.PertinentInformation = new List<PertinentInformation>();
        }


        /// <summary>
        /// Gets or sets the pertinent information class
        /// </summary>
        [Property(Name = "pertientInformations", NamespaceUri = "http://www.csioz.gov.pl/xsd/extPL/r1", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.Traversable, SortKey = 99)]
        public List<PertinentInformation> PertinentInformation { get; set; }
    }

    [Structure(Name = "CoveragePlan", NamespaceUri = "http://www.csioz.gov.pl/xsd/extPL/r1", Model = "POCD_MT000040PL")]
    public class CoveragePlan : InfrastructureRoot
    {
        /// <summary>
        /// Default CTOR for document
        /// </summary>
        public CoveragePlan()
        {
            this.ClassCode = "COV";
            this.MoodCode = ActMoodEventOccurrence.Eventoccurrence;
        }

        public CoveragePlan(CD<String> code)
            : this()
        {
            this.Code = code;
        }

        [Property(Name = "classCode", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.Structural, SortKey = 1, FixedValue = "COV")]
        public CS<String> ClassCode { get; set; }

        [Property(Name = "moodCode", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, PropertyType = PropertyAttribute.AttributeAttributeType.Structural, SortKey = 2, FixedValue = "EVN")]
        public CS<ActMoodEventOccurrence> MoodCode { get; set; }

        [Property(Name = "code", Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, NamespaceUri = "http://www.csioz.gov.pl/xsd/extPL/r1", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, SortKey = 4)]
        public CD<String> Code { get; set; }
    }

}
