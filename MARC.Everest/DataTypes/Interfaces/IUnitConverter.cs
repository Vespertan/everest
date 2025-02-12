﻿/* 
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
 * Date: 02-09-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// Identifies a structure that the PQ data type
    /// can use to convert units to/from other units
    /// </summary>
    public interface IUnitConverter
    {

        /// <summary>
        /// Returns true if the <paramref name="from"/> can be converted
        /// to <paramref name="unitTo"/>
        /// </summary>
        bool CanConvert(PQ from, string unitTo);

        /// <summary>
        /// Converts <paramref name="original"/> to a new
        /// instance of PQ with the <paramref name="unitTo"/>
        /// unit
        /// </summary>
        /// <remarks>Implementers must create a new PQ during this process</remarks>
        PQ Convert(PQ original, string unitTo);
    }
}
