/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.

 * 
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Attributes;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// A character string that may have a type-tag signifying its role in the address.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ADXP")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    [Structure(Name = "ADXPPL", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("ADXPPL", Namespace = "urn:hl7-org:v3")]
    public class ADXPPL : ADXP
    {
        /// <summary>
        /// Create a new instance of ADXP
        /// </summary>
        public ADXPPL() : base() { }
        /// <summary>
        /// Create a new instance of ADXP using the values specified
        /// </summary>
        /// <param name="type">The address part type</param>
        /// <param name="value">The value of the type</param>
        public ADXPPL(String value, AddressPartType type)
            : base()
        {
            Type = type;
            Value = value;
        }
        /// <summary>
        /// Create a new instance of ADXP using the value specified
        /// </summary>
        /// <param name="value">The value of the ADXP</param>
        /// <remarks>Type is set to "AddressLine"</remarks>
        public ADXPPL(String value) : base()
        {
            Value = value;
        }

        /// <summary>
        /// Convert a string into an address part
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static implicit operator ADXPPL(string s)
        {
            return new ADXPPL(s);
        }

        /// <summary>
        /// Convert an ADXP into a string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public static implicit operator String(ADXPPL a)
        {
            return a.Value;
        }

        /// <summary>
        /// Post City
        /// </summary>
        [Property(Name = "postCity", PropertyType = PropertyAttribute.AttributeAttributeType.Structural,
    Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("postCity")]

        public string PostCity { get; set; }

        /// <summary>
        /// Represent this value as a string
        /// </summary>
        public override string ToString()
        {
            return (string)this;
        }

        /// <summary>
        /// Validate this type
        /// </summary>
        /// <remarks>An address part type is valid if:
        ///     <list type="bullet">
        ///         <item>NullFlavor is not assigned, XOR</item>          
        ///         <item>Value property has a value, AND</item>
        ///         <item>Code AND CodeSystem are Assigned OR Code is not assigned, AND</item>
        ///         <item>CodeSystemVersion AND CodeSystem are Assigned OR CodeSystemVersion is not assigned</item>             
        ///     </list>
        /// </remarks>
        public override bool Validate()
        {
            return (NullFlavor != null) ^ (Value != null && ((Code != null && CodeSystem != null) || Code == null) &&
                ((CodeSystemVersion != null && CodeSystem != null) || CodeSystemVersion == null));
        }

        /// <summary>
        /// Extended validation which returns the results of the validation
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());
            if (this.CodeSystemVersion != null && this.CodeSystem == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ADXP", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "CodeSystemVersion", "Code"), null));
            if (this.Code != null && this.CodeSystem == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ADXP", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Code", "CodeSystem"), null));
            if (this.NullFlavor != null && this.Value == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ADXP", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            if (this.Value == null && this.NullFlavor == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ADXP", ValidationMessages.MSG_NULLFLAVOR_MISSING, null));
            return retVal;
        }
        #region IEquatable<ADXP> Members

        /// <summary>
        /// Determines if this ADXP equals another
        /// </summary>
        public bool Equals(ADXPPL other)
        {
            if (other != null)
            {
                return base.Equals((ANY)other) &&
                    other.Code == this.Code &&
                    other.CodeSystem == this.CodeSystem &&
                    other.CodeSystemVersion == this.CodeSystemVersion &&
                    other.Value == this.Value &&
                    other.Type == this.Type;
            }
            return false;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ADXPPL)
                return Equals(obj as ADXPPL);
            return base.Equals(obj);
        }
        /// <summary>
        /// Override od base GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 1029258072 + EqualityComparer<string>.Default.GetHashCode(PostCity);
        }

        #endregion
    }
}
