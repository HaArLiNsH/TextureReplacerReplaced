/*
 * Copyright © 2017 HaArLiNsH
 * Copyright © 2013-2017 Davorin Učakar, RangeMachine
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TextureReplacerReplaced
{
    /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// The data of a kerbal
    /// <para>Contain the head and suit texture used by the kerbal</para>   
    /// </summary>
    /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class KerbalData
    {
        /// <summary>
        /// The hash code of the kerbal
        /// </summary>
        public int hash;

        /// <summary>
        /// The gender of the kerbal
        /// </summary>
        public int gender;

        /// <summary>
        /// Is the kerbal a veteran?
        /// </summary>
        public bool isVeteran;

        /// <summary>
        /// Is the kerbal a badass ? 
        /// </summary>
        public bool isBadass;

        /// <summary>
        /// The head set of the kerbal
        /// </summary>
        public Head_Set head;

        /// <summary>
        /// The suit set of the kerbal
        /// </summary>
        public Suit_Set suit;

        /// <summary>
        /// The forced cabin suit (IVA) of the kerbal
        /// </summary>
        //public Suit_Set cabinSuit;
    }


}