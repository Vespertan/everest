using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Primitives;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Interfaces;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using MARC.Everest.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ClinicalDocument cda = new ClinicalDocument();
            cda.MoodCode = null;
            cda.ClassCode = null;
            cda.TypeId = II.CreatePublic("2.16.840.1.113883.1.3", "POCD_HD000040");
            cda.TemplateId = LIST<II>.CreateList(
                new II("2.16.840.1.113883.3.4424.13.10.1.1"),
                new II("2.16.840.1.113883.3.4424.13.10.1.2"),
                new II("2.16.840.1.113883.3.4424.13.10.1.3", "1.1.1"));
            cda.Id = new II("2.16.840.1.113883.3.4424.7.2.1", "2345678") { Displayable = true };
            var pi = new PertinentInformation() { TemplateId = LIST<II>.CreateList(new II("1.13.2.3")) };
            pi.CoveragePlan = new CoveragePlan(new CD<String>("PUBLICPOL", "2.16.840.1.113883.11.19350"));
            pi.CoveragePlan.Code.Qualifier = LIST<CR<String>>.CreateList(
                new CR<String>(
                    new CV<String>("RLEKUD", "1.2.3.4", null, null, "Refundacja ...", null),
                    new CD<String>("IB", "1.2.3.4")
                )
            );
            //cda.DataEnterer = new MyDataEnterer();
            //cda.PertinentInformation.Add(pi);
            cda.RecordTarget.Add(new RecordTarget { PatientRole = new PatientRole() });
            cda.RecordTarget[0].PatientRole.Addr = SET<AD>.CreateSET(AD.CreateAD(new ADXPPL("Kiszka", AddressPartType.PostalCode) { PostCity = "PT"}));
            XmlIts1Formatter xftr = new XmlIts1Formatter();
            //xftr.AddFormatterAssembly(Assembly.GetExecutingAssembly());
            xftr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            xftr.ValidateConformance = false;
            xftr.RegisterXSITypeName("POCD_MT000040UV.ClinicalDocument", typeof(ClinicalDocument));
            xftr.Settings |= SettingsType.AlwaysCheckForOverrides;


            using (XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(Console.Out, new XmlWriterSettings() { Indent = true })))
            {
                xw.WriteStartElement("", "ClinicalDocument", "urn:hl7-org:v3");
                xw.WriteAttributeString("xmlns", "extPL", null, "http://www.csioz.gov.pl/xsd/extPL/r1");
                xw.WriteAttributeString("xmlns", "xsi", null, XmlIts1Formatter.NS_XSI);
                //xw.WriteAttributeString("xsi", "type", XmlIts1Formatter.NS_XSI, "extPL:ClinicalDocument");

                xftr.Graph(xw, cda);
                xw.WriteEndElement();
            }












            PatientData pat = new PatientData()
            {
                Address = "123 Main Street West",
                City = "Hamilton",
                DateOfBirth = new DateTime(1995, 04, 03),
                FamilyName = "Smith",
                Gender = "F",
                GivenName = "Sarah",
                Id = "102-30343",
                OtherIds = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("2.16.2.3.2.3.2.4", "123-231-435")
                },
                State = "ON"
            };

            PhysicianData aut = new PhysicianData()
            {
                AddressLine = " 35 King Street West ",
                City = " Hamilton ",
                OrgId = " 123 - 1221 ",
                OrgName = new[] { " Good Health Clinics " },
                PhysicianId = " 1023433 - ON ",
                PhysicianName = new string[] { " Dr.", " Francis ", " F ", " Family " },
                Postal = "L0R2A0"
            };

            // var o = Parse(@"C:\temp\schematron\rec.xml");
            // Create the CDA
            //ClinicalDocument doc = CreateAPSDocument(pat, null, aut, DateTime.Now);
            //PrintStructure(doc);

            Console.ReadKey();

        }

        /// <summary>
        /// Print a structure to the console
        /// </summary>
        public static void PrintStructure(IGraphable structure)
        {
            // Create a formatter, this takes a model in memory and outputs it in XML
            using (XmlIts1Formatter fmtr = new XmlIts1Formatter())
            {
                fmtr.Settings = SettingsType.DefaultUniprocessor;
                // We want to use CDA data types
                using (ClinicalDocumentDatatypeFormatter dtfmtr = new ClinicalDocumentDatatypeFormatter())
                {
                    // This is a good idea to prevent validation errors
                    fmtr.ValidateConformance = false;
                    // This instructs the XML ITS1 Formatter we want to use CDA datatypes
                    fmtr.GraphAides.Add(dtfmtr);

                    // Output in a nice indented manner
                    using (XmlWriter xw = XmlWriter.Create(Console.Out, new XmlWriterSettings() { Indent = true }))
                    {
                        fmtr.Graph(xw, structure);
                    }
                }
            }

        }

        public static object Parse(string fileName)
        {
            using (XmlIts1Formatter fmtr = new XmlIts1Formatter())
            {
                fmtr.Settings = SettingsType.DefaultUniprocessor;
                using (ClinicalDocumentDatatypeFormatter dtfmtr = new ClinicalDocumentDatatypeFormatter())
                {

                    // This is a good idea to prevent validation errors
                    fmtr.ValidateConformance = false;
                    // This instructs the XML ITS1 Formatter we want to use CDA datatypes
                    fmtr.GraphAides.Add(dtfmtr);

                    return fmtr.Parse(new FileStream(fileName, FileMode.Open));
                    // Output in a nice indented manner

                    return dtfmtr.Parse(new FileStream(fileName, FileMode.Open));
                }
            }

        }
        /// <summary>
        /// Translates a dictionary of oid/ids to a set of II
        /// </summary>
        public static SET<II> CreateIdList(List<KeyValuePair<string, string>> identifiers)
        {
            SET<II> retVal = new SET<II>();
            foreach (var id in identifiers)
                retVal.Add(new II(id.Key, id.Value));
            return retVal;

        }

        private static ClinicalDocument CreateCDA(LIST<II> templateIds, CE<String> code, ST title, TS docDate, PatientData recordTarget, PatientData motherOfRCT, PatientData fatherOfRCT, PhysicianData author, params Section[] sections)
        {
            // First, we need to create a clinical document
            ClinicalDocument doc = new ClinicalDocument()
            {
                TemplateId = templateIds, // Identifiers
                Id = Guid.NewGuid(), // A unique id for the document
                Code = code, // The code for the document
                Title = title, // 
                TypeId = new II("2.16.840.1.113883.1.3", "POCD_HD000040"), // This value is static and identifies the HL7 type
                RealmCode = SET<CS<BindingRealm>>.CreateSET(BindingRealm.UniversalRealmOrContextUsedInEveryInstance), // This is UV some profiles require US
                EffectiveTime = docDate,
                ConfidentialityCode = x_BasicConfidentialityKind.Normal, // Confidentiality of data within the CDA
                LanguageCode = "en-US", // Language of the CDA
            };

            // Next we need to append the RecordTarget to the CDA
            RecordTarget rctPatient = new RecordTarget()
            {
                ContextControlCode = ContextControl.OverridingPropagating,
                PatientRole = new PatientRole()
                {
                    Id = CreateIdList(recordTarget.OtherIds), // These are "other MRNs" we know about in the CDA
                    Addr = SET<AD>.CreateSET(AD.FromSimpleAddress(PostalAddressUse.HomeAddress, recordTarget.Address, null, recordTarget.City, recordTarget.State, "CA", null)), // We create the address from parts
                    Patient = new Patient()
                    {
                        Name = SET<PN>.CreateSET(PN.FromFamilyGiven(EntityNameUse.Legal, recordTarget.FamilyName, recordTarget.GivenName)), // PAtient name
                        AdministrativeGenderCode = recordTarget.Gender, // GEnder
                        BirthTime = new TS(recordTarget.DateOfBirth, DatePrecision.Day) // Day of birth
                    }
                }
            };
            // WE also need to add our local identifier (example: from our database/MRN) to the CDA
            // You will need to use your own OID
            rctPatient.PatientRole.Id.Add(new II("1.2.3.4.5.6", recordTarget.Id));

            doc.RecordTarget.Add(rctPatient);

            // We now want to create an author
            Author docAuthor = new Author()
            {
                ContextControlCode = ContextControl.AdditivePropagating,
                Time = docDate, // When the document was created
                AssignedAuthor = new AssignedAuthor()
                {
                    Id = SET<II>.CreateSET(new II("1.2.3.4.5.6.1", author.PhysicianId)), // Physician's identifiers (or how we know the physician)
                    Addr = new SET<AD>() { AD.FromSimpleAddress(PostalAddressUse.PhysicalVisit, author.AddressLine, null, author.City, "ON", "CA", author.Postal) }, // The author's address
                    AssignedAuthorChoice = AuthorChoice.CreatePerson(
                                SET<PN>.CreateSET(PN.FromFamilyGiven(EntityNameUse.Legal, author.PhysicianName[0], author.PhysicianName[1])) // The author's name
                            ),
                    RepresentedOrganization = new Organization()
                    {
                        Id = new SET<II>(new II("1.2.3.4.5.6.2", author.OrgId)), // Organization the physician represents
                        Name = SET<ON>.CreateSET(new ON(null, new ENXP[] { new ENXP(author.OrgName[0]) })) // The name of the organization
                    }
                }
            };
            doc.Author.Add(docAuthor);


            // The document custodian is the source of truth for the document, i.e. the organization 
            // that is responsible for storing/maintaining the document.
            Custodian docCustodian = new Custodian(
                    new AssignedCustodian(
                        new CustodianOrganization(
                            SET<II>.CreateSET(new II("2.3.4.5.6.7", "304930-3")),
                            new ON(null, new ENXP[] { new ENXP("ACME Medical Centres") }),
                            null,
                            AD.FromSimpleAddress(PostalAddressUse.PhysicalVisit, "123 Main St.", null, "Hamilton", "ON", "CA", "L0R2A0")
                        )
                    )
                );
            doc.Custodian = docCustodian;


            // If the "mother" of the patient is provided, let's add the mother's data
            if (motherOfRCT != null)
            {
                doc.Participant.Add(new Participant1(ParticipationType.IND, ContextControl.OverridingNonpropagating, null, new IVL<TS>(new TS(recordTarget.DateOfBirth, DatePrecision.Year)), null)
                {
                    AssociatedEntity = new AssociatedEntity(RoleClassAssociative.NextOfKin,
                            CreateIdList(motherOfRCT.OtherIds),
                            new CE<string>("MTH", "2.16.840.1.113883.5.111", null, null, "Mother", null),
                            SET<AD>.CreateSET(AD.FromSimpleAddress(PostalAddressUse.HomeAddress, motherOfRCT.Address, null, motherOfRCT.City, motherOfRCT.State, "CA", null)),
                            SET<TEL>.CreateSET(new TEL() { NullFlavor = NullFlavor.NoInformation }),
                            new Person(SET<PN>.CreateSET(PN.FromFamilyGiven(EntityNameUse.Legal, motherOfRCT.FamilyName, motherOfRCT.GivenName))),
                            null)
                });
            }

            if (fatherOfRCT != null)
            {
                doc.Participant.Add(new Participant1(ParticipationType.IND, ContextControl.OverridingNonpropagating, null, new IVL<TS>(new TS(recordTarget.DateOfBirth, DatePrecision.Year)), null)
                {
                    AssociatedEntity = new AssociatedEntity(RoleClassAssociative.NextOfKin,
                CreateIdList(fatherOfRCT.OtherIds),
                new CE<string>("FTH", "2.16.840.1.113883.5.111", null, null, "Father", null),
                SET<AD>.CreateSET(AD.FromSimpleAddress(PostalAddressUse.HomeAddress, fatherOfRCT.Address, null, fatherOfRCT.City, fatherOfRCT.State, "CA", null)),
                SET<TEL>.CreateSET(new TEL() { NullFlavor = NullFlavor.NoInformation }),
                new Person(SET<PN>.CreateSET(PN.FromFamilyGiven(EntityNameUse.Legal, fatherOfRCT.FamilyName, fatherOfRCT.GivenName))),
                null)
                });
            }


            // Next we want to setup the body of the document
            doc.Component = new Component2(
                ActRelationshipHasComponent.HasComponent,
                true,
                new StructuredBody() // This document will be structured document
            );

            // Now we add the sections passed to us
            foreach (var sct in sections)
                doc.Component.GetBodyChoiceIfStructuredBody().Component.Add(new Component3(
                    ActRelationshipHasComponent.HasComponent,
                    true,
                    sct));

            return doc;

        }

        public static ClinicalDocument CreateAPSDocument(PatientData recordTarget, PatientData father, PhysicianData author, DateTime docDate, params Section[] sections)
        {
            var doc = CreateCDA(
                LIST<II>.CreateList(new II("1.3.6.1.4.1.19376.1.5.3.1.1.2"), new II("1.3.6.1.4.1.19376.1.5.3.1.1.11.2")),
                new CE<String>("57055-6", "2.16.840.1.113883.6.1", "LOINC", null, "Antepartum Summary Note", null),
                "Antepartum Summary",
                docDate,
                recordTarget,
                null,
                null,
                author,
                sections
                );

            // Father of fetus
            if (father != null)
            {
                SET<II> fatherIds = new SET<II>();
                foreach (var id in father.OtherIds)
                    fatherIds.Add(new II(id.Key, id.Value));

                doc.Participant.Add(new Participant1(ParticipationType.IND, ContextControl.OverridingNonpropagating, null, new IVL<TS>(new TS(recordTarget.DateOfBirth, DatePrecision.Year)), null)
                {
                    AssociatedEntity = new AssociatedEntity(RoleClassAssociative.NextOfKin,
                        fatherIds,
                        new CE<string>("xx - fatherofbaby ", " 2.16.840.1.113883.6.96 ", null, null, " Father of fetus ", null),
            SET<AD>.CreateSET(AD.FromSimpleAddress(PostalAddressUse.HomeAddress, father.Address, null, father.City, father.State, " CA ", null)),
            SET<TEL>.CreateSET(new TEL() { NullFlavor = NullFlavor.NoInformation }),
            new Person(SET<PN>.CreateSET(PN.FromFamilyGiven(EntityNameUse.Legal, father.FamilyName, father.GivenName))),
            null)
                });
            }

            return doc;

        }
    }
}
