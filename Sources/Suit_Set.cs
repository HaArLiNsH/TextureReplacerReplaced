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
    /// Contain the configuration data and textures of the suit set
    /// <para>Here you will find all the textures for a suit set and their functions </para>
    /// </summary>
    /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class Suit_Set
    {
        /// <summary>
        /// The name of the suit set
        /// </summary>
        public string suitSetName;

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  This will disappear !!!!!!!!!!!!!!!!!!!!!!!
        /// <summary>
        /// Is the suit set made for female kerbal?
        /// </summary>
        public bool isFemale;        

        /// <summary>
        /// The texture list for the helmet_EvaGround_Badass_Female
        /// </summary>
        private Texture2D[] helmet_EvaGround_Badass_Female;

        /// <summary>
        /// Normal map list for the helmet_EvaGround_Badass_Female
        /// </summary>
        private Texture2D[] helmet_EvaGround_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaGround_Badass_Male
        /// </summary>
        private Texture2D[] helmet_EvaGround_Badass_Male;

        /// <summary>
        /// Normal map list for the helmet_EvaGround_Badass_Male
        /// </summary>
        private Texture2D[] helmet_EvaGround_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaGround_Standard_Female
        /// </summary>
        private Texture2D[] helmet_EvaGround_Standard_Female;

        /// <summary>
        /// Normal map list for the helmet_EvaGround_Standard_Female
        /// </summary>
        private Texture2D[] helmet_EvaGround_Standard_FemaleNRM;

        /// <summary>
        /// The texture list for the leveled EVA ground helmet
        /// </summary>
        private Texture2D[] helmet_EvaGround_Standard_Male;

        /// <summary>
        /// Normal map list for the leveled EVA ground helmet
        /// </summary>
        private Texture2D[] helmet_EvaGround_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaGround_VetBad_Female
        /// </summary>
        private Texture2D[] helmet_EvaGround_VetBad_Female;

        /// <summary>
        /// Normal map list for the helmet_EvaGround_VetBad_Female
        /// </summary>
        private Texture2D[] helmet_EvaGround_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaGround_VetBad_Male
        /// </summary>
        private Texture2D[] helmet_EvaGround_VetBad_Male;

        /// <summary>
        /// Normal map list for the helmet_EvaGround_VetBad_Male
        /// </summary>
        private Texture2D[] helmet_EvaGround_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaGround_Veteran_Female
        /// </summary>
        private Texture2D[] helmet_EvaGround_Veteran_Female;

        /// <summary>
        /// Normal map list for the helmet_EvaGround_Veteran_Female
        /// </summary>
        private Texture2D[] helmet_EvaGround_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaGround_Veteran_Male
        /// </summary>
        private Texture2D[] helmet_EvaGround_Veteran_Male;

        /// <summary>
        /// Normal map list for the helmet_EvaGround_Veteran_Male
        /// </summary>
        private Texture2D[] helmet_EvaGround_Veteran_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaSpace_Badass_Female
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Badass_Female;

        /// <summary>
        /// Normal map list for the helmet_EvaSpace_Badass_Female
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaSpace_Badass_Male
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Badass_Male;

        /// <summary>
        /// Normal map list for the helmet_EvaSpace_Badass_Male
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaSpace_Standard_Female
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Standard_Female;

        /// <summary>
        /// Normal map list for the helmet_EvaSpace_Standard_Female
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Standard_FemaleNRM;
        /// <summary>
        /// The texture list for the leveled EVA space helmet
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Standard_Male;

        /// <summary>
        /// Normal map list for the leveled EVA space helmet
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaSpace_VetBad_Female
        /// </summary>
        private Texture2D[] helmet_EvaSpace_VetBad_Female;

        /// <summary>
        /// Normal map list for the helmet_EvaSpace_VetBad_Female
        /// </summary>
        private Texture2D[] helmet_EvaSpace_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaSpace_VetBad_Male
        /// </summary>
        private Texture2D[] helmet_EvaSpace_VetBad_Male;

        /// <summary>
        /// Normal map list for the helmet_EvaSpace_VetBad_Male
        /// </summary>
        private Texture2D[] helmet_EvaSpace_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaSpace_Veteran_Female
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Veteran_Female;

        /// <summary>
        /// Normal map list for the helmet_EvaSpace_Veteran_Female
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_EvaSpace_Veteran_Male
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Veteran_Male;

        /// <summary>
        /// Normal map list for the helmet_EvaSpace_Veteran_Male
        /// </summary>
        private Texture2D[] helmet_EvaSpace_Veteran_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_Iva_Badass_Female
        /// </summary>
        private Texture2D[] helmet_Iva_Badass_Female;

        /// <summary>
        /// Normal map list for the helmet_Iva_Badass_Female
        /// </summary>
        private Texture2D[] helmet_Iva_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_Iva_Badass_Male
        /// </summary>
        private Texture2D[] helmet_Iva_Badass_Male;

        /// <summary>
        /// Normal map list for the helmet_Iva_Badass_Male
        /// </summary>
        private Texture2D[] helmet_Iva_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_Iva_Standard_Female
        /// </summary>
        private Texture2D[] helmet_Iva_Standard_Female;

        /// <summary>
        /// Normal map list for the helmet_Iva_Standard_Female
        /// </summary>
        private Texture2D[] helmet_Iva_Standard_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_Iva_Standard_Male
        /// </summary>
        private Texture2D[] helmet_Iva_Standard_Male;

        /// <summary>
        /// Normal map list for the helmet_Iva_Standard_Male
        /// </summary>
        private Texture2D[] helmet_Iva_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_Iva_VetBad_Female
        /// </summary>
        private Texture2D[] helmet_Iva_VetBad_Female;

        /// <summary>
        /// Normal map list for the helmet_Iva_VetBad_Female
        /// </summary>
        private Texture2D[] helmet_Iva_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_Iva_VetBad_Male
        /// </summary>
        private Texture2D[] helmet_Iva_VetBad_Male;

        /// <summary>
        /// Normal map list for the helmet_Iva_VetBad_Male
        /// </summary>
        private Texture2D[] helmet_Iva_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the helmet_Iva_Veteran_Female
        /// </summary>
        private Texture2D[] helmet_Iva_Veteran_Female;

        /// <summary>
        /// Normal map list for the helmet_Iva_Veteran_Female
        /// </summary>
        private Texture2D[] helmet_Iva_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the helmet_Iva_Veteran_Male
        /// </summary>
        private Texture2D[] helmet_Iva_Veteran_Male;

        /// <summary>
        /// Normal map list for the helmet_Iva_Veteran_Male
        /// </summary>
        private Texture2D[] helmet_Iva_Veteran_MaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaGround_Badass_Female
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Badass_Female;

        /// <summary>
        /// Normal map list for the jetpack_EvaGround_Badass_Female
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaGround_Badass_Male
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Badass_Male;

        /// <summary>
        /// Normal map list for the jetpack_EvaGround_Badass_Male
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaGround_Standard_Female
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Standard_Female;

        /// <summary>
        /// Normal map list for the jetpack_EvaGround_Standard_Female
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Standard_FemaleNRM;

        /// <summary>
        /// The texture list for the leveled EVA ground jetpack
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Standard_Male;

        /// <summary>
        /// Normal map list for the leveled EVA ground jetpack
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaGround_VetBad_Female
        /// </summary>
        private Texture2D[] jetpack_EvaGround_VetBad_Female;

        /// <summary>
        /// Normal map list for the jetpack_EvaGround_VetBad_Female
        /// </summary>
        private Texture2D[] jetpack_EvaGround_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaGround_VetBad_Male
        /// </summary>
        private Texture2D[] jetpack_EvaGround_VetBad_Male;

        /// <summary>
        /// Normal map list for the jetpack_EvaGround_VetBad_Male
        /// </summary>
        private Texture2D[] jetpack_EvaGround_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaGround_Veteran_Female
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Veteran_Female;

        /// <summary>
        /// Normal map list for the jetpack_EvaGround_Veteran_Female
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaGround_Veteran_Male
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Veteran_Male;

        /// <summary>
        /// Normal map list for the jetpack_EvaGround_Veteran_Male
        /// </summary>
        private Texture2D[] jetpack_EvaGround_Veteran_MaleNRM;       

        /// <summary>
        /// The texture list for the jetpack_EvaSpace_Badass_Female
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Badass_Female;

        /// <summary>
        /// Normal map list for the jetpack_EvaSpace_Badass_Female
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaSpace_Badass_Male
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Badass_Male;

        /// <summary>
        /// Normal map list for the jetpack_EvaSpace_Badass_Male
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaSpace_Standard_Female
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Standard_Female;

        /// <summary>
        /// Normal map list for the jetpack_EvaSpace_Standard_Female
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Standard_FemaleNRM;
        /// <summary>
        /// The texture list for the leveled EVA space jetpack
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Standard_Male;

        /// <summary>
        /// Normal map list for the leveled EVA space jetpack
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaSpace_VetBad_Female
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_VetBad_Female;

        /// <summary>
        /// Normal map list for the jetpack_EvaSpace_VetBad_Female
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaSpace_VetBad_Male
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_VetBad_Male;

        /// <summary>
        /// Normal map list for the jetpack_EvaSpace_VetBad_Male
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaSpace_Veteran_Female
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Veteran_Female;

        /// <summary>
        /// Normal map list for the jetpack_EvaSpace_Veteran_Female
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the jetpack_EvaSpace_Veteran_Male
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Veteran_Male;

        /// <summary>
        /// Normal map list for the jetpack_EvaSpace_Veteran_Male
        /// </summary>
        private Texture2D[] jetpack_EvaSpace_Veteran_MaleNRM;       
        
        /// <summary>
        /// The texture list for the suit_EvaGround_Badass_Female
        /// </summary>
        private Texture2D[] suit_EvaGround_Badass_Female;

        /// <summary>
        /// Normal map list for the suit_EvaGround_Badass_Female
        /// </summary>
        private Texture2D[] suit_EvaGround_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaGround_Badass_Male
        /// </summary>
        private Texture2D[] suit_EvaGround_Badass_Male;

        /// <summary>
        /// Normal map list for the suit_EvaGround_Badass_Male
        /// </summary>
        private Texture2D[] suit_EvaGround_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaGround_Standard_Female
        /// </summary>
        private Texture2D[] suit_EvaGround_Standard_Female;

        /// <summary>
        /// Normal map list for the suit_EvaGround_Standard_Female
        /// </summary>
        private Texture2D[] suit_EvaGround_Standard_FemaleNRM;

        /// <summary>
        /// The texture list for the leveled EVA ground suit
        /// </summary>
        private Texture2D[] suit_EvaGround_Standard_Male;

        /// <summary>
        /// Normal map list for the leveled EVA ground suit
        /// </summary>
        private Texture2D[] suit_EvaGround_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaGround_VetBad_Female
        /// </summary>
        private Texture2D[] suit_EvaGround_VetBad_Female;

        /// <summary>
        /// Normal map list for the suit_EvaGround_VetBad_Female
        /// </summary>
        private Texture2D[] suit_EvaGround_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaGround_VetBad_Male
        /// </summary>
        private Texture2D[] suit_EvaGround_VetBad_Male;

        /// <summary>
        /// Normal map list for the suit_EvaGround_VetBad_Male
        /// </summary>
        private Texture2D[] suit_EvaGround_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaGround_Veteran_Female
        /// </summary>
        private Texture2D[] suit_EvaGround_Veteran_Female;

        /// <summary>
        /// Normal map list for the suit_EvaGround_Veteran_Female
        /// </summary>
        private Texture2D[] suit_EvaGround_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaGround_Veteran_Male
        /// </summary>
        private Texture2D[] suit_EvaGround_Veteran_Male;

        /// <summary>
        /// Normal map list for the suit_EvaGround_Veteran_Male
        /// </summary>
        private Texture2D[] suit_EvaGround_Veteran_MaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaSpace_Badass_Female
        /// </summary>
        private Texture2D[] suit_EvaSpace_Badass_Female;

        /// <summary>
        /// Normal map list for the suit_EvaSpace_Badass_Female
        /// </summary>
        private Texture2D[] suit_EvaSpace_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaSpace_Badass_Male
        /// </summary>
        private Texture2D[] suit_EvaSpace_Badass_Male;

        /// <summary>
        /// Normal map list for the suit_EvaSpace_Badass_Male
        /// </summary>
        private Texture2D[] suit_EvaSpace_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaSpace_Standard_Female
        /// </summary>
        private Texture2D[] suit_EvaSpace_Standard_Female;

        /// <summary>
        /// Normal map list for the suit_EvaSpace_Standard_Female
        /// </summary>
        private Texture2D[] suit_EvaSpace_Standard_FemaleNRM;
        /// <summary>
        /// The texture list for the leveled EVA space suit
        /// </summary>
        private Texture2D[] suit_EvaSpace_Standard_Male;

        /// <summary>
        /// Normal map list for the leveled EVA space suit
        /// </summary>
        private Texture2D[] suit_EvaSpace_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaSpace_VetBad_Female
        /// </summary>
        private Texture2D[] suit_EvaSpace_VetBad_Female;

        /// <summary>
        /// Normal map list for the suit_EvaSpace_VetBad_Female
        /// </summary>
        private Texture2D[] suit_EvaSpace_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaSpace_VetBad_Male
        /// </summary>
        private Texture2D[] suit_EvaSpace_VetBad_Male;

        /// <summary>
        /// Normal map list for the suit_EvaSpace_VetBad_Male
        /// </summary>
        private Texture2D[] suit_EvaSpace_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaSpace_Veteran_Female
        /// </summary>
        private Texture2D[] suit_EvaSpace_Veteran_Female;

        /// <summary>
        /// Normal map list for the suit_EvaSpace_Veteran_Female
        /// </summary>
        private Texture2D[] suit_EvaSpace_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_EvaSpace_Veteran_Male
        /// </summary>
        private Texture2D[] suit_EvaSpace_Veteran_Male;

        /// <summary>
        /// Normal map list for the suit_EvaSpace_Veteran_Male
        /// </summary>
        private Texture2D[] suit_EvaSpace_Veteran_MaleNRM;

        /// <summary>
        /// The texture list for the suit_Iva_Badass_Female
        /// </summary>
        private Texture2D[] suit_Iva_Badass_Female;

        /// <summary>
        /// Normal map list for the suit_Iva_Badass_Female
        /// </summary>
        private Texture2D[] suit_Iva_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_Iva_Badass_Male
        /// </summary>
        private Texture2D[] suit_Iva_Badass_Male;

        /// <summary>
        /// Normal map list for the suit_Iva_Badass_Male
        /// </summary>
        private Texture2D[] suit_Iva_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the suit_Iva_Standard_Female
        /// </summary>
        private Texture2D[] suit_Iva_Standard_Female;

        /// <summary>
        /// Normal map list for the suit_Iva_Standard_Female
        /// </summary>
        private Texture2D[] suit_Iva_Standard_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_Iva_Standard_Male
        /// </summary>
        private Texture2D[] suit_Iva_Standard_Male;

        /// <summary>
        /// Normal map list for the suit_Iva_Standard_Male
        /// </summary>
        private Texture2D[] suit_Iva_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the suit_Iva_VetBad_Female
        /// </summary>
        private Texture2D[] suit_Iva_VetBad_Female;

        /// <summary>
        /// Normal map list for the suit_Iva_VetBad_Female
        /// </summary>
        private Texture2D[] suit_Iva_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_Iva_VetBad_Male
        /// </summary>
        private Texture2D[] suit_Iva_VetBad_Male;

        /// <summary>
        /// Normal map list for the suit_Iva_VetBad_Male
        /// </summary>
        private Texture2D[] suit_Iva_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the suit_Iva_Veteran_Female
        /// </summary>
        private Texture2D[] suit_Iva_Veteran_Female;

        /// <summary>
        /// Normal map list for the suit_Iva_Veteran_Female
        /// </summary>
        private Texture2D[] suit_Iva_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the suit_Iva_Veteran_Male
        /// </summary>
        private Texture2D[] suit_Iva_Veteran_Male;

        /// <summary>
        /// Normal map list for the suit_Iva_Veteran_Male
        /// </summary>
        private Texture2D[] suit_Iva_Veteran_MaleNRM;

        /// <summary>
        /// The texture list for the Visor_EvaGround_Badass_Female
        /// </summary>
        private Texture2D[] visor_EvaGround_Badass_Female;

        /// <summary>
        /// Normal map list for the visor_EvaGround_Badass_Female
        /// </summary>
        private Texture2D[] visor_EvaGround_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaGround_Badass_Male
        /// </summary>
        private Texture2D[] visor_EvaGround_Badass_Male;

        /// <summary>
        /// Normal map list for the visor_EvaGround_Badass_Male
        /// </summary>
        private Texture2D[] visor_EvaGround_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaGround_Standard_Female
        /// </summary>
        private Texture2D[] visor_EvaGround_Standard_Female;

        /// <summary>
        /// Normal map list for the visor_EvaGround_Standard_Female
        /// </summary>
        private Texture2D[] visor_EvaGround_Standard_FemaleNRM;

        /// <summary>
        /// The texture list for the leveled EVA ground visor
        /// </summary>
        private Texture2D[] visor_EvaGround_Standard_Male;

        /// <summary>
        /// Normal map list for the leveled EVA ground visor
        /// </summary>
        private Texture2D[] visor_EvaGround_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaGround_VetBad_Female
        /// </summary>
        private Texture2D[] visor_EvaGround_VetBad_Female;

        /// <summary>
        /// Normal map list for the visor_EvaGround_VetBad_Female
        /// </summary>
        private Texture2D[] visor_EvaGround_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaGround_VetBad_Male
        /// </summary>
        private Texture2D[] visor_EvaGround_VetBad_Male;

        /// <summary>
        /// Normal map list for the visor_EvaGround_VetBad_Male
        /// </summary>
        private Texture2D[] visor_EvaGround_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaGround_Veteran_Female
        /// </summary>
        private Texture2D[] visor_EvaGround_Veteran_Female;

        /// <summary>
        /// Normal map list for the visor_EvaGround_Veteran_Female
        /// </summary>
        private Texture2D[] visor_EvaGround_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaGround_Veteran_Male
        /// </summary>
        private Texture2D[] visor_EvaGround_Veteran_Male;

        /// <summary>
        /// Normal map list for the visor_EvaGround_Veteran_Male
        /// </summary>
        private Texture2D[] visor_EvaGround_Veteran_MaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaSpace_Badass_Female
        /// </summary>
        private Texture2D[] visor_EvaSpace_Badass_Female;

        /// <summary>
        /// Normal map list for the visor_EvaSpace_Badass_Female
        /// </summary>
        private Texture2D[] visor_EvaSpace_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaSpace_Badass_Male
        /// </summary>
        private Texture2D[] visor_EvaSpace_Badass_Male;

        /// <summary>
        /// Normal map list for the visor_EvaSpace_Badass_Male
        /// </summary>
        private Texture2D[] visor_EvaSpace_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaSpace_Standard_Female
        /// </summary>
        private Texture2D[] visor_EvaSpace_Standard_Female;

        /// <summary>
        /// Normal map list for the visor_EvaSpace_Standard_Female
        /// </summary>
        private Texture2D[] visor_EvaSpace_Standard_FemaleNRM;
        /// <summary>
        /// The texture list for the leveled EVA space visor
        /// </summary>
        private Texture2D[] visor_EvaSpace_Standard_Male;

        /// <summary>
        /// Normal map list for the leveled EVA space visor
        /// </summary>
        private Texture2D[] visor_EvaSpace_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaSpace_VetBad_Female
        /// </summary>
        private Texture2D[] visor_EvaSpace_VetBad_Female;

        /// <summary>
        /// Normal map list for the visor_EvaSpace_VetBad_Female
        /// </summary>
        private Texture2D[] visor_EvaSpace_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaSpace_VetBad_Male
        /// </summary>
        private Texture2D[] visor_EvaSpace_VetBad_Male;

        /// <summary>
        /// Normal map list for the visor_EvaSpace_VetBad_Male
        /// </summary>
        private Texture2D[] visor_EvaSpace_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaSpace_Veteran_Female
        /// </summary>
        private Texture2D[] visor_EvaSpace_Veteran_Female;

        /// <summary>
        /// Normal map list for the visor_EvaSpace_Veteran_Female
        /// </summary>
        private Texture2D[] visor_EvaSpace_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_EvaSpace_Veteran_Male
        /// </summary>
        private Texture2D[] visor_EvaSpace_Veteran_Male;

        /// <summary>
        /// Normal map list for the visor_EvaSpace_Veteran_Male
        /// </summary>
        private Texture2D[] visor_EvaSpace_Veteran_MaleNRM;

        /// <summary>
        /// The texture list for the visor_Iva_Badass_Female
        /// </summary>
        private Texture2D[] visor_Iva_Badass_Female;

        /// <summary>
        /// Normal map list for the visor_Iva_Badass_Female
        /// </summary>
        private Texture2D[] visor_Iva_Badass_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_Iva_Badass_Male
        /// </summary>
        private Texture2D[] visor_Iva_Badass_Male;

        /// <summary>
        /// Normal map list for the visor_Iva_Badass_Male
        /// </summary>
        private Texture2D[] visor_Iva_Badass_MaleNRM;

        /// <summary>
        /// The texture list for the visor_Iva_Standard_Female
        /// </summary>
        private Texture2D[] visor_Iva_Standard_Female;

        /// <summary>
        /// Normal map list for the visor_Iva_Standard_Female
        /// </summary>
        private Texture2D[] visor_Iva_Standard_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_Iva_Standard_Male
        /// </summary>
        private Texture2D[] visor_Iva_Standard_Male;

        /// <summary>
        /// Normal map list for the visor_Iva_Standard_Male
        /// </summary>
        private Texture2D[] visor_Iva_Standard_MaleNRM;

        /// <summary>
        /// The texture list for the visor_Iva_VetBad_Female
        /// </summary>
        private Texture2D[] visor_Iva_VetBad_Female;

        /// <summary>
        /// Normal map list for the visor_Iva_VetBad_Female
        /// </summary>
        private Texture2D[] visor_Iva_VetBad_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_Iva_VetBad_Male
        /// </summary>
        private Texture2D[] visor_Iva_VetBad_Male;

        /// <summary>
        /// Normal map list for the visor_Iva_VetBad_Male
        /// </summary>
        private Texture2D[] visor_Iva_VetBad_MaleNRM;

        /// <summary>
        /// The texture list for the visor_Iva_Veteran_Female
        /// </summary>
        private Texture2D[] visor_Iva_Veteran_Female;

        /// <summary>
        /// Normal map list for the visor_Iva_Veteran_Female
        /// </summary>
        private Texture2D[] visor_Iva_Veteran_FemaleNRM;

        /// <summary>
        /// The texture list for the visor_Iva_Veteran_Male
        /// </summary>
        private Texture2D[] visor_Iva_Veteran_Male;

        /// <summary>
        /// Normal map list for the visor_Iva_Veteran_Male
        /// </summary>
        private Texture2D[] visor_Iva_Veteran_MaleNRM;


        ////// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        ///// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        ///////// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!     

        ////// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        ///// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        ///////// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
/*
        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Badass_Female(int level)
        {
            return helmet_EvaGround_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Badass_FemaleNRM(int level)
        {
            return helmet_EvaGround_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Badass_Male(int level)
        {
            return helmet_EvaGround_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Badass_MaleNRM(int level)
        {
            return helmet_EvaGround_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Standard_Female(int level)
        {
            return helmet_EvaGround_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Standard_FemaleNRM(int level)
        {
            return helmet_EvaGround_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Standard_Male(int level)
        {
            return helmet_EvaGround_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Standard_MaleNRM(int level)
        {
            return helmet_EvaGround_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_VetBad_Female(int level)
        {
            return helmet_EvaGround_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_VetBad_FemaleNRM(int level)
        {
            return helmet_EvaGround_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_VetBad_Male(int level)
        {
            return helmet_EvaGround_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_VetBad_MaleNRM(int level)
        {
            return helmet_EvaGround_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Veteran_Female(int level)
        {
            return helmet_EvaGround_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Veteran_FemaleNRM(int level)
        {
            return helmet_EvaGround_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Veteran_Male(int level)
        {
            return helmet_EvaGround_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Veteran_MaleNRM(int level)
        {
            return helmet_EvaGround_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Badass_Female(int level)
        {
            return helmet_EvaSpace_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Badass_FemaleNRM(int level)
        {
            return helmet_EvaSpace_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Badass_Male(int level)
        {
            return helmet_EvaSpace_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Badass_MaleNRM(int level)
        {
            return helmet_EvaSpace_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Standard_Female(int level)
        {
            return helmet_EvaSpace_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Standard_FemaleNRM(int level)
        {
            return helmet_EvaSpace_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Standard_Male(int level)
        {
            return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Standard_MaleNRM(int level)
        {
            return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_VetBad_Female(int level)
        {
            return helmet_EvaSpace_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_VetBad_FemaleNRM(int level)
        {
            return helmet_EvaSpace_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_VetBad_Male(int level)
        {
            return helmet_EvaSpace_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_VetBad_MaleNRM(int level)
        {
            return helmet_EvaSpace_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Veteran_Female(int level)
        {
            return helmet_EvaSpace_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Veteran_FemaleNRM(int level)
        {
            return helmet_EvaSpace_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Veteran_Male(int level)
        {
            return helmet_EvaSpace_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Veteran_MaleNRM(int level)
        {
            return helmet_EvaSpace_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Badass_Female(int level)
        {
            return helmet_Iva_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Badass_FemaleNRM(int level)
        {
            return helmet_Iva_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Badass_Male(int level)
        {
            return helmet_Iva_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Badass_MaleNRM(int level)
        {
            return helmet_Iva_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Standard_Female(int level)
        {
            return helmet_Iva_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Standard_FemaleNRM(int level)
        {
            return helmet_Iva_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Standard_Male(int level)
        {
            return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Standard_MaleNRM(int level)
        {
            return helmet_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_VetBad_Female(int level)
        {
            return helmet_Iva_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_VetBad_FemaleNRM(int level)
        {
            return helmet_Iva_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_VetBad_Male(int level)
        {
            return helmet_Iva_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_VetBad_MaleNRM(int level)
        {
            return helmet_Iva_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Veteran_Female(int level)
        {
            return helmet_Iva_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Veteran_FemaleNRM(int level)
        {
            return helmet_Iva_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Veteran_Male(int level)
        {
            return helmet_Iva_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Veteran_MaleNRM(int level)
        {
            return helmet_Iva_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Badass_Female(int level)
        {
            return jetpack_EvaGround_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Badass_FemaleNRM(int level)
        {
            return jetpack_EvaGround_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Badass_Male(int level)
        {
            return jetpack_EvaGround_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Badass_MaleNRM(int level)
        {
            return jetpack_EvaGround_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Standard_Female(int level)
        {
            return jetpack_EvaGround_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Standard_FemaleNRM(int level)
        {
            return jetpack_EvaGround_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Standard_Male(int level)
        {
            return jetpack_EvaGround_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Standard_MaleNRM(int level)
        {
            return jetpack_EvaGround_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_VetBad_Female(int level)
        {
            return jetpack_EvaGround_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_VetBad_FemaleNRM(int level)
        {
            return jetpack_EvaGround_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_VetBad_Male(int level)
        {
            return jetpack_EvaGround_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_VetBad_MaleNRM(int level)
        {
            return jetpack_EvaGround_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Veteran_Female(int level)
        {
            return jetpack_EvaGround_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Veteran_FemaleNRM(int level)
        {
            return jetpack_EvaGround_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Veteran_Male(int level)
        {
            return jetpack_EvaGround_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Veteran_MaleNRM(int level)
        {
            return jetpack_EvaGround_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Badass_Female(int level)
        {
            return jetpack_EvaSpace_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Badass_FemaleNRM(int level)
        {
            return jetpack_EvaSpace_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Badass_Male(int level)
        {
            return jetpack_EvaSpace_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Badass_MaleNRM(int level)
        {
            return jetpack_EvaSpace_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Standard_Female(int level)
        {
            return jetpack_EvaSpace_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Standard_FemaleNRM(int level)
        {
            return jetpack_EvaSpace_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Standard_Male(int level)
        {
            return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Standard_MaleNRM(int level)
        {
            return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_VetBad_Female(int level)
        {
            return jetpack_EvaSpace_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_VetBad_FemaleNRM(int level)
        {
            return jetpack_EvaSpace_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_VetBad_Male(int level)
        {
            return jetpack_EvaSpace_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_VetBad_MaleNRM(int level)
        {
            return jetpack_EvaSpace_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Veteran_Female(int level)
        {
            return jetpack_EvaSpace_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Veteran_FemaleNRM(int level)
        {
            return jetpack_EvaSpace_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Veteran_Male(int level)
        {
            return jetpack_EvaSpace_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Veteran_MaleNRM(int level)
        {
            return jetpack_EvaSpace_Veteran_MaleNRM[level];
        }
        /*
        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Badass_Female(int level)
        {
            return jetpack_Iva_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Badass_FemaleNRM(int level)
        {
            return jetpack_Iva_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Badass_Male(int level)
        {
            return jetpack_Iva_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Badass_MaleNRM(int level)
        {
            return jetpack_Iva_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Standard_Female(int level)
        {
            return jetpack_Iva_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Standard_FemaleNRM(int level)
        {
            return jetpack_Iva_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Standard_Male(int level)
        {
            return jetpack_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Standard_MaleNRM(int level)
        {
            return jetpack_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_VetBad_Female(int level)
        {
            return jetpack_Iva_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_VetBad_FemaleNRM(int level)
        {
            return jetpack_Iva_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_VetBad_Male(int level)
        {
            return jetpack_Iva_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_VetBad_MaleNRM(int level)
        {
            return jetpack_Iva_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Veteran_Female(int level)
        {
            return jetpack_Iva_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Veteran_FemaleNRM(int level)
        {
            return jetpack_Iva_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Veteran_Male(int level)
        {
            return jetpack_Iva_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_Iva_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_Iva_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_Iva_Veteran_MaleNRM(int level)
        {
            return jetpack_Iva_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Badass_Female(int level)
        {
            return suit_EvaGround_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Badass_FemaleNRM(int level)
        {
            return suit_EvaGround_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Badass_Male(int level)
        {
            return suit_EvaGround_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Badass_MaleNRM(int level)
        {
            return suit_EvaGround_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Standard_Female(int level)
        {
            return suit_EvaGround_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Standard_FemaleNRM(int level)
        {
            return suit_EvaGround_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Standard_Male(int level)
        {
            return suit_EvaGround_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Standard_MaleNRM(int level)
        {
            return suit_EvaGround_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_VetBad_Female(int level)
        {
            return suit_EvaGround_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_VetBad_FemaleNRM(int level)
        {
            return suit_EvaGround_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_VetBad_Male(int level)
        {
            return suit_EvaGround_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_VetBad_MaleNRM(int level)
        {
            return suit_EvaGround_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Veteran_Female(int level)
        {
            return suit_EvaGround_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Veteran_FemaleNRM(int level)
        {
            return suit_EvaGround_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Veteran_Male(int level)
        {
            return suit_EvaGround_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Veteran_MaleNRM(int level)
        {
            return suit_EvaGround_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Badass_Female(int level)
        {
            return suit_EvaSpace_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Badass_FemaleNRM(int level)
        {
            return suit_EvaSpace_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Badass_Male(int level)
        {
            return suit_EvaSpace_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Badass_MaleNRM(int level)
        {
            return suit_EvaSpace_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Standard_Female(int level)
        {
            return suit_EvaSpace_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Standard_FemaleNRM(int level)
        {
            return suit_EvaSpace_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Standard_Male(int level)
        {
            return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Standard_MaleNRM(int level)
        {
            return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_VetBad_Female(int level)
        {
            return suit_EvaSpace_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_VetBad_FemaleNRM(int level)
        {
            return suit_EvaSpace_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_VetBad_Male(int level)
        {
            return suit_EvaSpace_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_VetBad_MaleNRM(int level)
        {
            return suit_EvaSpace_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Veteran_Female(int level)
        {
            return suit_EvaSpace_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Veteran_FemaleNRM(int level)
        {
            return suit_EvaSpace_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Veteran_Male(int level)
        {
            return suit_EvaSpace_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Veteran_MaleNRM(int level)
        {
            return suit_EvaSpace_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Badass_Female(int level)
        {
            return suit_Iva_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Badass_FemaleNRM(int level)
        {
            return suit_Iva_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Badass_Male(int level)
        {
            return suit_Iva_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Badass_MaleNRM(int level)
        {
            return suit_Iva_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Standard_Female(int level)
        {
            return suit_Iva_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Standard_FemaleNRM(int level)
        {
            return suit_Iva_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Standard_Male(int level)
        {
            return suit_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Standard_MaleNRM(int level)
        {
            return suit_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_VetBad_Female(int level)
        {
            return suit_Iva_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_VetBad_FemaleNRM(int level)
        {
            return suit_Iva_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_VetBad_Male(int level)
        {
            return suit_Iva_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_VetBad_MaleNRM(int level)
        {
            return suit_Iva_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Veteran_Female(int level)
        {
            return suit_Iva_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Veteran_FemaleNRM(int level)
        {
            return suit_Iva_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Veteran_Male(int level)
        {
            return suit_Iva_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Veteran_MaleNRM(int level)
        {
            return suit_Iva_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Badass_Female(int level)
        {
            return visor_EvaGround_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Badass_FemaleNRM(int level)
        {
            return visor_EvaGround_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Badass_Male(int level)
        {
            return visor_EvaGround_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Badass_MaleNRM(int level)
        {
            return visor_EvaGround_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Standard_Female(int level)
        {
            return visor_EvaGround_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Standard_FemaleNRM(int level)
        {
            return visor_EvaGround_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Standard_Male(int level)
        {
            return visor_EvaGround_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Standard_MaleNRM(int level)
        {
            return visor_EvaGround_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_VetBad_Female(int level)
        {
            return visor_EvaGround_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_VetBad_FemaleNRM(int level)
        {
            return visor_EvaGround_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_VetBad_Male(int level)
        {
            return visor_EvaGround_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_VetBad_MaleNRM(int level)
        {
            return visor_EvaGround_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Veteran_Female(int level)
        {
            return visor_EvaGround_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Veteran_FemaleNRM(int level)
        {
            return visor_EvaGround_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Veteran_Male(int level)
        {
            return visor_EvaGround_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Veteran_MaleNRM(int level)
        {
            return visor_EvaGround_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Badass_Female(int level)
        {
            return visor_EvaSpace_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Badass_FemaleNRM(int level)
        {
            return visor_EvaSpace_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Badass_Male(int level)
        {
            return visor_EvaSpace_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Badass_MaleNRM(int level)
        {
            return visor_EvaSpace_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Standard_Female(int level)
        {
            return visor_EvaSpace_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Standard_FemaleNRM(int level)
        {
            return visor_EvaSpace_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Standard_Male(int level)
        {
            return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Standard_MaleNRM(int level)
        {
            return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_VetBad_Female(int level)
        {
            return visor_EvaSpace_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_VetBad_FemaleNRM(int level)
        {
            return visor_EvaSpace_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_VetBad_Male(int level)
        {
            return visor_EvaSpace_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_VetBad_MaleNRM(int level)
        {
            return visor_EvaSpace_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Veteran_Female(int level)
        {
            return visor_EvaSpace_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Veteran_FemaleNRM(int level)
        {
            return visor_EvaSpace_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Veteran_Male(int level)
        {
            return visor_EvaSpace_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Veteran_MaleNRM(int level)
        {
            return visor_EvaSpace_Veteran_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Badass_Female(int level)
        {
            return visor_Iva_Badass_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Badass_FemaleNRM(int level)
        {
            return visor_Iva_Badass_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Badass_Male(int level)
        {
            return visor_Iva_Badass_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Badass_MaleNRM(int level)
        {
            return visor_Iva_Badass_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Standard_Female(int level)
        {
            return visor_Iva_Standard_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Standard_FemaleNRM(int level)
        {
            return visor_Iva_Standard_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Standard_Male(int level)
        {
            return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Standard_MaleNRM(int level)
        {
            return visor_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_VetBad_Female(int level)
        {
            return visor_Iva_VetBad_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_VetBad_FemaleNRM(int level)
        {
            return visor_Iva_VetBad_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_VetBad_Male(int level)
        {
            return visor_Iva_VetBad_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_VetBad_MaleNRM(int level)
        {
            return visor_Iva_VetBad_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Veteran_Female(int level)
        {
            return visor_Iva_Veteran_Female[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Veteran_FemaleNRM(int level)
        {
            return visor_Iva_Veteran_FemaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Veteran_Male(int level)
        {
            return visor_Iva_Veteran_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Veteran_MaleNRM(int level)
        {
            return visor_Iva_Veteran_MaleNRM[level];
        }*/

        //////////// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ////////     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ////// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //////////// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ////////     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ////// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            

        
        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Badass_Female(int level)
        {
            if (helmet_EvaGround_Badass_Female[level] != null)
                return helmet_EvaGround_Badass_Female[level];
            else if (helmet_EvaGround_Standard_Female[level] != null)
                return helmet_EvaGround_Standard_Female[level];
            else if (helmet_EvaGround_Badass_Male[level] != null)
                return helmet_EvaGround_Badass_Male[level];
            else if (helmet_EvaGround_Standard_Male[level] != null)
                return helmet_EvaGround_Standard_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Badass_FemaleNRM(int level)
        {
            if (helmet_EvaGround_Badass_FemaleNRM[level] != null)
                return helmet_EvaGround_Badass_FemaleNRM[level];
            else if (helmet_EvaGround_Standard_FemaleNRM[level] != null)
                return helmet_EvaGround_Standard_FemaleNRM[level];
            else if (helmet_EvaGround_Badass_MaleNRM[level] != null)
                return helmet_EvaGround_Badass_MaleNRM[level];
            else if (helmet_EvaGround_Standard_MaleNRM[level] != null)
                return helmet_EvaGround_Standard_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Badass_Male(int level)
        {
            if (helmet_EvaGround_Badass_Male[level] != null)
                return helmet_EvaGround_Badass_Male[level];
            else if (helmet_EvaGround_Standard_Male[level] != null)
                return helmet_EvaGround_Standard_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Badass_MaleNRM(int level)
        {
            if (helmet_EvaGround_Badass_MaleNRM[level] != null)
                return helmet_EvaGround_Badass_MaleNRM[level];
            else if (helmet_EvaGround_Standard_MaleNRM[level] != null)
                return helmet_EvaGround_Standard_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Standard_Female(int level)
        {
            if (helmet_EvaGround_Standard_Female[level] != null)
                return helmet_EvaGround_Standard_Female[level];
            else if (helmet_EvaGround_Standard_Male[level] != null)
                return helmet_EvaGround_Standard_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Standard_FemaleNRM(int level)
        {
            if (helmet_EvaGround_Standard_FemaleNRM[level] != null)
                return helmet_EvaGround_Standard_FemaleNRM[level];
            else if (helmet_EvaGround_Standard_MaleNRM[level] != null)
                return helmet_EvaGround_Standard_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Standard_Male(int level)
        {
            if (helmet_EvaGround_Standard_Male[level] != null)
                return helmet_EvaGround_Standard_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Standard_MaleNRM(int level)
        {
            if (helmet_EvaGround_Standard_MaleNRM[level] != null)
                return helmet_EvaGround_Standard_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_VetBad_Female(int level)
        {
            if (helmet_EvaGround_VetBad_Female[level] != null)
                return helmet_EvaGround_VetBad_Female[level];

            else if (helmet_EvaGround_Veteran_Female[level] != null)
                return helmet_EvaGround_Veteran_Female[level];

            else if (helmet_EvaGround_Standard_Female[level] != null)
                return helmet_EvaGround_Standard_Female[level];

            else if (helmet_EvaGround_VetBad_Male[level] != null)
                return helmet_EvaGround_VetBad_Male[level];

            else if (helmet_EvaGround_Veteran_Male[level] != null)
                return helmet_EvaGround_Veteran_Male[level];

            else if (helmet_EvaGround_Standard_Male[level] != null)
                return helmet_EvaGround_Standard_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_VetBad_FemaleNRM(int level)
        {
            if (helmet_EvaGround_VetBad_FemaleNRM[level] != null)
                return helmet_EvaGround_VetBad_FemaleNRM[level];

            else if (helmet_EvaGround_Veteran_FemaleNRM[level] != null)
                return helmet_EvaGround_Veteran_FemaleNRM[level];

            else if (helmet_EvaGround_Standard_FemaleNRM[level] != null)
                return helmet_EvaGround_Standard_FemaleNRM[level];

            else if (helmet_EvaGround_VetBad_MaleNRM[level] != null)
                return helmet_EvaGround_VetBad_MaleNRM[level];

            else if (helmet_EvaGround_Veteran_MaleNRM[level] != null)
                return helmet_EvaGround_Veteran_MaleNRM[level];
            else if (helmet_EvaGround_Standard_MaleNRM[level] != null)
                return helmet_EvaGround_Standard_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_VetBad_Male(int level)
        {
            if (helmet_EvaGround_VetBad_Male[level] != null)
                return helmet_EvaGround_VetBad_Male[level];

            else if (helmet_EvaGround_Veteran_Male[level] != null)
                return helmet_EvaGround_Veteran_Male[level];

            else if (helmet_EvaGround_Standard_Male[level] != null)
                return helmet_EvaGround_Standard_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_VetBad_MaleNRM(int level)
        {
            if (helmet_EvaGround_VetBad_MaleNRM[level] != null)
                return helmet_EvaGround_VetBad_MaleNRM[level];

            else if (helmet_EvaGround_Veteran_MaleNRM[level] != null)
                return helmet_EvaGround_Veteran_MaleNRM[level];
            else if (helmet_EvaGround_Standard_MaleNRM[level] != null)
                return helmet_EvaGround_Standard_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Veteran_Female(int level)
        {
            if (helmet_EvaGround_Veteran_Female[level] != null)
                return helmet_EvaGround_Veteran_Female[level];
            else if (helmet_EvaGround_Standard_Female[level] != null)
                return helmet_EvaGround_Standard_Female[level];
            else if (helmet_EvaGround_Veteran_Male[level] != null)
                return helmet_EvaGround_Veteran_Male[level];
            else if (helmet_EvaGround_Standard_Male[level] != null)
                return helmet_EvaGround_Standard_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Veteran_FemaleNRM(int level)
        {
            if (helmet_EvaGround_Veteran_FemaleNRM[level] != null)
                return helmet_EvaGround_Veteran_FemaleNRM[level];
            else if (helmet_EvaGround_Standard_FemaleNRM[level] != null)
                return helmet_EvaGround_Standard_FemaleNRM[level];
            else if (helmet_EvaGround_Veteran_MaleNRM[level] != null)
                return helmet_EvaGround_Veteran_MaleNRM[level];
            else if (helmet_EvaGround_Standard_MaleNRM[level] != null)
                return helmet_EvaGround_Standard_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Veteran_Male(int level)
        {
            if (helmet_EvaGround_Veteran_Male[level] != null)
                return helmet_EvaGround_Veteran_Male[level];
            else if (helmet_EvaGround_Standard_Male[level] != null)
                return helmet_EvaGround_Standard_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaGround_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaGround_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaGround_Veteran_MaleNRM(int level)
        {
            if (helmet_EvaGround_Veteran_MaleNRM[level] != null)
                return helmet_EvaGround_Veteran_MaleNRM[level];
            else if (helmet_EvaGround_Standard_MaleNRM[level] != null)
                return helmet_EvaGround_Standard_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Badass_Female(int level)
        {
            if (helmet_EvaSpace_Badass_Female[level] != null)
                return helmet_EvaSpace_Badass_Female[level];
            else if (helmet_EvaSpace_Standard_Female[level] != null)
                return helmet_EvaSpace_Standard_Female[level];
            else if (helmet_EvaSpace_Badass_Male[level] != null)
                return helmet_EvaSpace_Badass_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Badass_FemaleNRM(int level)
        {
            if (helmet_EvaSpace_Badass_FemaleNRM[level] != null)
                return helmet_EvaSpace_Badass_FemaleNRM[level];
            else if (helmet_EvaSpace_Standard_FemaleNRM[level] != null)
                return helmet_EvaSpace_Standard_FemaleNRM[level];
            else if (helmet_EvaSpace_Badass_MaleNRM[level] != null)
                return helmet_EvaSpace_Badass_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Badass_Male(int level)
        {
            if (helmet_EvaSpace_Badass_Male[level] != null)
                return helmet_EvaSpace_Badass_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Badass_MaleNRM(int level)
        {
            if (helmet_EvaSpace_Badass_MaleNRM[level] != null)
                return helmet_EvaSpace_Badass_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Standard_Female(int level)
        {
            if (helmet_EvaSpace_Standard_Female[level] != null)
                return helmet_EvaSpace_Standard_Female[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Standard_FemaleNRM(int level)
        {
            if (helmet_EvaSpace_Standard_FemaleNRM[level] != null)
                return helmet_EvaSpace_Standard_FemaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Standard_Male(int level)
        {
            return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Standard_MaleNRM(int level)
        {
            return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_VetBad_Female(int level)
        {
            if (helmet_EvaSpace_VetBad_Female[level] != null)
                return helmet_EvaSpace_VetBad_Female[level];

            else if (helmet_EvaSpace_Veteran_Female[level] != null)
                return helmet_EvaSpace_Veteran_Female[level];

            else if (helmet_EvaSpace_Standard_Female[level] != null)
                return helmet_EvaSpace_Standard_Female[level];

            else if (helmet_EvaSpace_VetBad_Male[level] != null)
                return helmet_EvaSpace_VetBad_Male[level];

            else if (helmet_EvaSpace_Veteran_Male[level] != null)
                return helmet_EvaSpace_Veteran_Male[level];

            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_VetBad_FemaleNRM(int level)
        {
            if (helmet_EvaSpace_VetBad_FemaleNRM[level] != null)
                return helmet_EvaSpace_VetBad_FemaleNRM[level];

            else if (helmet_EvaSpace_Veteran_FemaleNRM[level] != null)
                return helmet_EvaSpace_Veteran_FemaleNRM[level];

            else if (helmet_EvaSpace_Standard_FemaleNRM[level] != null)
                return helmet_EvaSpace_Standard_FemaleNRM[level];

            else if (helmet_EvaSpace_VetBad_MaleNRM[level] != null)
                return helmet_EvaSpace_VetBad_MaleNRM[level];

            else if (helmet_EvaSpace_Veteran_MaleNRM[level] != null)
                return helmet_EvaSpace_Veteran_MaleNRM[level];

            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_VetBad_Male(int level)
        {
            if (helmet_EvaSpace_VetBad_Male[level] != null)
                return helmet_EvaSpace_VetBad_Male[level];

            else if (helmet_EvaSpace_Veteran_Male[level] != null)
                return helmet_EvaSpace_Veteran_Male[level];

            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_VetBad_MaleNRM(int level)
        {
            if (helmet_EvaSpace_VetBad_MaleNRM[level] != null)
                return helmet_EvaSpace_VetBad_MaleNRM[level];

            else if (helmet_EvaSpace_Veteran_MaleNRM[level] != null)
                return helmet_EvaSpace_Veteran_MaleNRM[level];

            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Veteran_Female(int level)
        {
            if (helmet_EvaSpace_Veteran_Female[level] != null)
                return helmet_EvaSpace_Veteran_Female[level];
            else if (helmet_EvaSpace_Standard_Female[level] != null)
                return helmet_EvaSpace_Standard_Female[level];
            else if (helmet_EvaSpace_Veteran_Male[level] != null)
                return helmet_EvaSpace_Veteran_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Veteran_FemaleNRM(int level)
        {
            if (helmet_EvaSpace_Veteran_FemaleNRM[level] != null)
                return helmet_EvaSpace_Veteran_FemaleNRM[level];
            else if (helmet_EvaSpace_Standard_FemaleNRM[level] != null)
                return helmet_EvaSpace_Standard_FemaleNRM[level];
            else if (helmet_EvaSpace_Veteran_MaleNRM[level] != null)
                return helmet_EvaSpace_Veteran_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Veteran_Male(int level)
        {
            if (helmet_EvaSpace_Veteran_Male[level] != null)
                return helmet_EvaSpace_Veteran_Male[level];
            else
                return helmet_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_EvaSpace_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_EvaSpace_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_EvaSpace_Veteran_MaleNRM(int level)
        {
            if (helmet_EvaSpace_Veteran_MaleNRM[level] != null)
                return helmet_EvaSpace_Veteran_MaleNRM[level];
            else
                return helmet_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Badass_Female(int level)
        {
            if (helmet_Iva_Badass_Female[level] != null)
                return helmet_Iva_Badass_Female[level];
            else if (helmet_Iva_Standard_Female[level] != null)
                return helmet_Iva_Standard_Female[level];
            else if (helmet_Iva_Badass_Male[level] != null)
                return helmet_Iva_Badass_Male[level];
            else
                return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Badass_FemaleNRM(int level)
        {
            if (helmet_Iva_Badass_FemaleNRM[level] != null)
                return helmet_Iva_Badass_FemaleNRM[level];
            else if (helmet_Iva_Standard_FemaleNRM[level] != null)
                return helmet_Iva_Standard_FemaleNRM[level];
            else if (helmet_Iva_Badass_MaleNRM[level] != null)
                return helmet_Iva_Badass_MaleNRM[level];
            else
                return helmet_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Badass_Male(int level)
        {
            if (helmet_Iva_Badass_Male[level] != null)
                return helmet_Iva_Badass_Male[level];
            else
                return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Badass_MaleNRM(int level)
        {
            if (helmet_Iva_Badass_MaleNRM[level] != null)
                return helmet_Iva_Badass_MaleNRM[level];
            else
                return helmet_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Standard_Female(int level)
        {
            if (helmet_Iva_Standard_Female[level] != null)
                return helmet_Iva_Standard_Female[level];
            else
                return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Standard_FemaleNRM(int level)
        {
            if (helmet_Iva_Standard_FemaleNRM[level] != null)
                return helmet_Iva_Standard_FemaleNRM[level];
            else
                return helmet_Iva_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Standard_Male(int level)
        {
            return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Standard_MaleNRM(int level)
        {
            return helmet_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_VetBad_Female(int level)
        {
            if (helmet_Iva_VetBad_Female[level] != null)
                return helmet_Iva_VetBad_Female[level];

            else if (helmet_Iva_Veteran_Female[level] != null)
                return helmet_Iva_Veteran_Female[level];

            else if (helmet_Iva_Standard_Female[level] != null)
                return helmet_Iva_Standard_Female[level];

            else if (helmet_Iva_VetBad_Male[level] != null)
                return helmet_Iva_VetBad_Male[level];

            else if (helmet_Iva_Veteran_Male[level] != null)
                return helmet_Iva_Veteran_Male[level];

            else
                return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_VetBad_FemaleNRM(int level)
        {
            if (helmet_Iva_VetBad_FemaleNRM[level] != null)
                return helmet_Iva_VetBad_FemaleNRM[level];

            else if (helmet_Iva_Veteran_FemaleNRM[level] != null)
                return helmet_Iva_Veteran_FemaleNRM[level];

            else if (helmet_Iva_Standard_FemaleNRM[level] != null)
                return helmet_Iva_Standard_FemaleNRM[level];

            else if (helmet_Iva_VetBad_MaleNRM[level] != null)
                return helmet_Iva_VetBad_MaleNRM[level];

            else if (helmet_Iva_Veteran_MaleNRM[level] != null)
                return helmet_Iva_Veteran_MaleNRM[level];

            else
                return helmet_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_VetBad_Male(int level)
        {
            if (helmet_Iva_VetBad_Male[level] != null)
                return helmet_Iva_VetBad_Male[level];

            else if (helmet_Iva_Veteran_Male[level] != null)
                return helmet_Iva_Veteran_Male[level];

            else
                return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_VetBad_MaleNRM(int level)
        {
            if (helmet_Iva_VetBad_MaleNRM[level] != null)
                return helmet_Iva_VetBad_MaleNRM[level];

            else if (helmet_Iva_Veteran_MaleNRM[level] != null)
                return helmet_Iva_Veteran_MaleNRM[level];

            else
                return helmet_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Veteran_Female(int level)
        {
            if (helmet_Iva_Veteran_Female[level] != null)
                return helmet_Iva_Veteran_Female[level];
            else if (helmet_Iva_Standard_Female[level] != null)
                return helmet_Iva_Standard_Female[level];
            else if (helmet_Iva_Veteran_Male[level] != null)
                return helmet_Iva_Veteran_Male[level];
            else
                return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Veteran_FemaleNRM(int level)
        {
            if (helmet_Iva_Veteran_FemaleNRM[level] != null)
                return helmet_Iva_Veteran_FemaleNRM[level];
            else if (helmet_Iva_Standard_FemaleNRM[level] != null)
                return helmet_Iva_Standard_FemaleNRM[level];
            else if (helmet_Iva_Veteran_MaleNRM[level] != null)
                return helmet_Iva_Veteran_MaleNRM[level];
            else
                return helmet_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Veteran_Male(int level)
        {
            if (helmet_Iva_Veteran_Male[level] != null)
                return helmet_Iva_Veteran_Male[level];
            else
                return helmet_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the helmet_Iva_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The helmet_Iva_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_helmet_Iva_Veteran_MaleNRM(int level)
        {
            if (helmet_Iva_Veteran_MaleNRM[level] != null)
                return helmet_Iva_Veteran_MaleNRM[level];
            else
                return helmet_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Badass_Female(int level)
        {
            if (jetpack_EvaGround_Badass_Female[level] != null)
                return jetpack_EvaGround_Badass_Female[level];
            else if (jetpack_EvaGround_Standard_Female[level] != null)
                return jetpack_EvaGround_Standard_Female[level];
            else if (jetpack_EvaGround_Badass_Male[level] != null)
                return jetpack_EvaGround_Badass_Male[level];
            else if (jetpack_EvaGround_Standard_Male[level] != null)
                return jetpack_EvaGround_Standard_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Badass_FemaleNRM(int level)
        {
            if (jetpack_EvaGround_Badass_FemaleNRM[level] != null)
                return jetpack_EvaGround_Badass_FemaleNRM[level];
            else if (jetpack_EvaGround_Standard_FemaleNRM[level] != null)
                return jetpack_EvaGround_Standard_FemaleNRM[level];
            else if (jetpack_EvaGround_Badass_MaleNRM[level] != null)
                return jetpack_EvaGround_Badass_MaleNRM[level];
            else if (jetpack_EvaGround_Standard_MaleNRM[level] != null)
                return jetpack_EvaGround_Standard_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Badass_Male(int level)
        {
            if (jetpack_EvaGround_Badass_Male[level] != null)
                return jetpack_EvaGround_Badass_Male[level];
            else if (jetpack_EvaGround_Standard_Male[level] != null)
                return jetpack_EvaGround_Standard_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Badass_MaleNRM(int level)
        {
            if (jetpack_EvaGround_Badass_MaleNRM[level] != null)
                return jetpack_EvaGround_Badass_MaleNRM[level];
            else if (jetpack_EvaGround_Standard_MaleNRM[level] != null)
                return jetpack_EvaGround_Standard_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Standard_Female(int level)
        {
            if (jetpack_EvaGround_Standard_Female[level] != null)
                return jetpack_EvaGround_Standard_Female[level];
            else if (jetpack_EvaGround_Standard_Male[level] != null)
                return jetpack_EvaGround_Standard_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Standard_FemaleNRM(int level)
        {
            if (jetpack_EvaGround_Standard_FemaleNRM[level] != null)
                return jetpack_EvaGround_Standard_FemaleNRM[level];
            else if (jetpack_EvaGround_Standard_MaleNRM[level] != null)
                return jetpack_EvaGround_Standard_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Standard_Male(int level)
        {
            if (jetpack_EvaGround_Standard_Male[level] != null)
                return jetpack_EvaGround_Standard_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Standard_MaleNRM(int level)
        {
            if (jetpack_EvaGround_Standard_MaleNRM[level] != null)
                return jetpack_EvaGround_Standard_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_VetBad_Female(int level)
        {
            if (jetpack_EvaGround_VetBad_Female[level] != null)
                return jetpack_EvaGround_VetBad_Female[level];

            else if (jetpack_EvaGround_Veteran_Female[level] != null)
                return jetpack_EvaGround_Veteran_Female[level];

            else if (jetpack_EvaGround_Standard_Female[level] != null)
                return jetpack_EvaGround_Standard_Female[level];

            else if (jetpack_EvaGround_VetBad_Male[level] != null)
                return jetpack_EvaGround_VetBad_Male[level];

            else if (jetpack_EvaGround_Veteran_Male[level] != null)
                return jetpack_EvaGround_Veteran_Male[level];

            else if (jetpack_EvaGround_Standard_Male[level] != null)
                return jetpack_EvaGround_Standard_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_VetBad_FemaleNRM(int level)
        {
            if (jetpack_EvaGround_VetBad_FemaleNRM[level] != null)
                return jetpack_EvaGround_VetBad_FemaleNRM[level];

            else if (jetpack_EvaGround_Veteran_FemaleNRM[level] != null)
                return jetpack_EvaGround_Veteran_FemaleNRM[level];

            else if (jetpack_EvaGround_Standard_FemaleNRM[level] != null)
                return jetpack_EvaGround_Standard_FemaleNRM[level];

            else if (jetpack_EvaGround_VetBad_MaleNRM[level] != null)
                return jetpack_EvaGround_VetBad_MaleNRM[level];

            else if (jetpack_EvaGround_Veteran_MaleNRM[level] != null)
                return jetpack_EvaGround_Veteran_MaleNRM[level];
            else if (jetpack_EvaGround_Standard_MaleNRM[level] != null)
                return jetpack_EvaGround_Standard_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_VetBad_Male(int level)
        {
            if (jetpack_EvaGround_VetBad_Male[level] != null)
                return jetpack_EvaGround_VetBad_Male[level];

            else if (jetpack_EvaGround_Veteran_Male[level] != null)
                return jetpack_EvaGround_Veteran_Male[level];
            else if (jetpack_EvaGround_Standard_Male[level] != null)
                return jetpack_EvaGround_Standard_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_VetBad_MaleNRM(int level)
        {
            if (jetpack_EvaGround_VetBad_MaleNRM[level] != null)
                return jetpack_EvaGround_VetBad_MaleNRM[level];

            else if (jetpack_EvaGround_Veteran_MaleNRM[level] != null)
                return jetpack_EvaGround_Veteran_MaleNRM[level];
            else if (jetpack_EvaGround_Standard_MaleNRM[level] != null)
                return jetpack_EvaGround_Standard_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Veteran_Female(int level)
        {
            if (jetpack_EvaGround_Veteran_Female[level] != null)
                return jetpack_EvaGround_Veteran_Female[level];
            else if (jetpack_EvaGround_Standard_Female[level] != null)
                return jetpack_EvaGround_Standard_Female[level];
            else if (jetpack_EvaGround_Veteran_Male[level] != null)
                return jetpack_EvaGround_Veteran_Male[level];
            else if (jetpack_EvaGround_Standard_Male[level] != null)
                return jetpack_EvaGround_Standard_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Veteran_FemaleNRM(int level)
        {
            if (jetpack_EvaGround_Veteran_FemaleNRM[level] != null)
                return jetpack_EvaGround_Veteran_FemaleNRM[level];
            else if (jetpack_EvaGround_Standard_FemaleNRM[level] != null)
                return jetpack_EvaGround_Standard_FemaleNRM[level];
            else if (jetpack_EvaGround_Veteran_MaleNRM[level] != null)
                return jetpack_EvaGround_Veteran_MaleNRM[level];
            else if (jetpack_EvaGround_Standard_MaleNRM[level] != null)
                return jetpack_EvaGround_Standard_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Veteran_Male(int level)
        {
            if (jetpack_EvaGround_Veteran_Male[level] != null)
                return jetpack_EvaGround_Veteran_Male[level];
            else if (jetpack_EvaGround_Standard_Male[level] != null)
                return jetpack_EvaGround_Standard_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaGround_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaGround_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaGround_Veteran_MaleNRM(int level)
        {
            if (jetpack_EvaGround_Veteran_MaleNRM[level] != null)
                return jetpack_EvaGround_Veteran_MaleNRM[level];
            else if (jetpack_EvaGround_Standard_MaleNRM[level] != null)
                return jetpack_EvaGround_Standard_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Badass_Female(int level)
        {
            if (jetpack_EvaSpace_Badass_Female[level] != null)
                return jetpack_EvaSpace_Badass_Female[level];
            else if (jetpack_EvaSpace_Standard_Female[level] != null)
                return jetpack_EvaSpace_Standard_Female[level];
            else if (jetpack_EvaSpace_Badass_Male[level] != null)
                return jetpack_EvaSpace_Badass_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Badass_FemaleNRM(int level)
        {
            if (jetpack_EvaSpace_Badass_FemaleNRM[level] != null)
                return jetpack_EvaSpace_Badass_FemaleNRM[level];
            else if (jetpack_EvaSpace_Standard_FemaleNRM[level] != null)
                return jetpack_EvaSpace_Standard_FemaleNRM[level];
            else if (jetpack_EvaSpace_Badass_MaleNRM[level] != null)
                return jetpack_EvaSpace_Badass_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Badass_Male(int level)
        {
            if (jetpack_EvaSpace_Badass_Male[level] != null)
                return jetpack_EvaSpace_Badass_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Badass_MaleNRM(int level)
        {
            if (jetpack_EvaSpace_Badass_MaleNRM[level] != null)
                return jetpack_EvaSpace_Badass_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Standard_Female(int level)
        {
            if (jetpack_EvaSpace_Standard_Female[level] != null)
                return jetpack_EvaSpace_Standard_Female[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Standard_FemaleNRM(int level)
        {
            if (jetpack_EvaSpace_Standard_FemaleNRM[level] != null)
                return jetpack_EvaSpace_Standard_FemaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Standard_Male(int level)
        {
            return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Standard_MaleNRM(int level)
        {
            return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_VetBad_Female(int level)
        {
            if (jetpack_EvaSpace_VetBad_Female[level] != null)
                return jetpack_EvaSpace_VetBad_Female[level];

            else if (jetpack_EvaSpace_Veteran_Female[level] != null)
                return jetpack_EvaSpace_Veteran_Female[level];

            else if (jetpack_EvaSpace_Standard_Female[level] != null)
                return jetpack_EvaSpace_Standard_Female[level];

            else if (jetpack_EvaSpace_VetBad_Male[level] != null)
                return jetpack_EvaSpace_VetBad_Male[level];

            else if (jetpack_EvaSpace_Veteran_Male[level] != null)
                return jetpack_EvaSpace_Veteran_Male[level];

            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_VetBad_FemaleNRM(int level)
        {
            if (jetpack_EvaSpace_VetBad_FemaleNRM[level] != null)
                return jetpack_EvaSpace_VetBad_FemaleNRM[level];

            else if (jetpack_EvaSpace_Veteran_FemaleNRM[level] != null)
                return jetpack_EvaSpace_Veteran_FemaleNRM[level];

            else if (jetpack_EvaSpace_Standard_FemaleNRM[level] != null)
                return jetpack_EvaSpace_Standard_FemaleNRM[level];

            else if (jetpack_EvaSpace_VetBad_MaleNRM[level] != null)
                return jetpack_EvaSpace_VetBad_MaleNRM[level];

            else if (jetpack_EvaSpace_Veteran_MaleNRM[level] != null)
                return jetpack_EvaSpace_Veteran_MaleNRM[level];

            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_VetBad_Male(int level)
        {
            if (jetpack_EvaSpace_VetBad_Male[level] != null)
                return jetpack_EvaSpace_VetBad_Male[level];

            else if (jetpack_EvaSpace_Veteran_Male[level] != null)
                return jetpack_EvaSpace_Veteran_Male[level];

            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_VetBad_MaleNRM(int level)
        {
            if (jetpack_EvaSpace_VetBad_MaleNRM[level] != null)
                return jetpack_EvaSpace_VetBad_MaleNRM[level];

            else if (jetpack_EvaSpace_Veteran_MaleNRM[level] != null)
                return jetpack_EvaSpace_Veteran_MaleNRM[level];

            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Veteran_Female(int level)
        {
            if (jetpack_EvaSpace_Veteran_Female[level] != null)
                return jetpack_EvaSpace_Veteran_Female[level];
            else if (jetpack_EvaSpace_Standard_Female[level] != null)
                return jetpack_EvaSpace_Standard_Female[level];
            else if (jetpack_EvaSpace_Veteran_Male[level] != null)
                return jetpack_EvaSpace_Veteran_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Veteran_FemaleNRM(int level)
        {
            if (jetpack_EvaSpace_Veteran_FemaleNRM[level] != null)
                return jetpack_EvaSpace_Veteran_FemaleNRM[level];
            else if (jetpack_EvaSpace_Standard_FemaleNRM[level] != null)
                return jetpack_EvaSpace_Standard_FemaleNRM[level];
            else if (jetpack_EvaSpace_Veteran_MaleNRM[level] != null)
                return jetpack_EvaSpace_Veteran_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Veteran_Male(int level)
        {
            if (jetpack_EvaSpace_Veteran_Male[level] != null)
                return jetpack_EvaSpace_Veteran_Male[level];
            else
                return jetpack_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the jetpack_EvaSpace_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The jetpack_EvaSpace_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_jetpack_EvaSpace_Veteran_MaleNRM(int level)
        {
            if (jetpack_EvaSpace_Veteran_MaleNRM[level] != null)
                return jetpack_EvaSpace_Veteran_MaleNRM[level];
            else
                return jetpack_EvaSpace_Standard_MaleNRM[level];
        }       

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Badass_Female(int level)
        {
            if (suit_EvaGround_Badass_Female[level] != null)
                return suit_EvaGround_Badass_Female[level];
            else if (suit_EvaGround_Standard_Female[level] != null)
                return suit_EvaGround_Standard_Female[level];
            else if (suit_EvaGround_Badass_Male[level] != null)
                return suit_EvaGround_Badass_Male[level];
            else if (suit_EvaGround_Standard_Male[level] != null)
            {
                return suit_EvaGround_Standard_Male[level];
            }
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Badass_FemaleNRM(int level)
        {
            if (suit_EvaGround_Badass_FemaleNRM[level] != null)
                return suit_EvaGround_Badass_FemaleNRM[level];
            else if (suit_EvaGround_Standard_FemaleNRM[level] != null)
                return suit_EvaGround_Standard_FemaleNRM[level];
            else if (suit_EvaGround_Badass_MaleNRM[level] != null)
                return suit_EvaGround_Badass_MaleNRM[level];
            else if (suit_EvaGround_Standard_MaleNRM[level] != null)
            {
                return suit_EvaGround_Standard_MaleNRM[level];
            }
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Badass_Male(int level)
        {
            if (suit_EvaGround_Badass_Male[level] != null)
                return suit_EvaGround_Badass_Male[level];
            else if (suit_EvaGround_Standard_Male[level] != null)
            {
                return suit_EvaGround_Standard_Male[level];
            }
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Badass_MaleNRM(int level)
        {
            if (suit_EvaGround_Badass_MaleNRM[level] != null)
                return suit_EvaGround_Badass_MaleNRM[level];
            else if (suit_EvaGround_Standard_MaleNRM[level] != null)
            {
                return suit_EvaGround_Standard_MaleNRM[level];
            }
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Standard_Female(int level)
        {
            if (suit_EvaGround_Standard_Female[level] != null)
                return suit_EvaGround_Standard_Female[level];
            else if (suit_EvaGround_Standard_Male[level] != null)
            {
                return suit_EvaGround_Standard_Male[level];
            }
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Standard_FemaleNRM(int level)
        {
            if (suit_EvaGround_Standard_FemaleNRM[level] != null)
                return suit_EvaGround_Standard_FemaleNRM[level];
            else if (suit_EvaGround_Standard_MaleNRM[level] != null)
            {
                return suit_EvaGround_Standard_MaleNRM[level];
            }
            else
                return suit_EvaSpace_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Standard_Male(int level)
        {
            if (suit_EvaGround_Standard_Male[level] != null)
            {
                return suit_EvaGround_Standard_Male[level];
            }
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Standard_MaleNRM(int level)
        {
            if (suit_EvaGround_Standard_MaleNRM[level] != null)
            {
                return suit_EvaGround_Standard_MaleNRM[level];
            }
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }
        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_VetBad_Female(int level)
        {
            if (suit_EvaGround_VetBad_Female[level] != null)
                return suit_EvaGround_VetBad_Female[level];

            else if (suit_EvaGround_Veteran_Female[level] != null)
                return suit_EvaGround_Veteran_Female[level];

            else if (suit_EvaGround_Standard_Female[level] != null)
                return suit_EvaGround_Standard_Female[level];

            else if (suit_EvaGround_VetBad_Male[level] != null)
                return suit_EvaGround_VetBad_Male[level];

            else if (suit_EvaGround_Veteran_Male[level] != null)
                return suit_EvaGround_Veteran_Male[level];
            
                else if (suit_EvaGround_Standard_Male[level] != null)
            {
                return suit_EvaGround_Standard_Male[level];
            }
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_VetBad_FemaleNRM(int level)
        {
            if (suit_EvaGround_VetBad_FemaleNRM[level] != null)
                return suit_EvaGround_VetBad_FemaleNRM[level];

            else if (suit_EvaGround_Veteran_FemaleNRM[level] != null)
                return suit_EvaGround_Veteran_FemaleNRM[level];

            else if (suit_EvaGround_Standard_FemaleNRM[level] != null)
                return suit_EvaGround_Standard_FemaleNRM[level];

            else if (suit_EvaGround_VetBad_MaleNRM[level] != null)
                return suit_EvaGround_VetBad_MaleNRM[level];

            else if (suit_EvaGround_Veteran_MaleNRM[level] != null)
                return suit_EvaGround_Veteran_MaleNRM[level];

            else if (suit_EvaGround_Standard_MaleNRM[level] != null)
            {
                return suit_EvaGround_Standard_MaleNRM[level];
            }
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_VetBad_Male(int level)
        {
            if (suit_EvaGround_VetBad_Male[level] != null)
                return suit_EvaGround_VetBad_Male[level];

            else if (suit_EvaGround_Veteran_Male[level] != null)
                return suit_EvaGround_Veteran_Male[level];

            else if (suit_EvaGround_Standard_Male[level] != null)
            {
                return suit_EvaGround_Standard_Male[level];
            }
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_VetBad_MaleNRM(int level)
        {
            if (suit_EvaGround_VetBad_MaleNRM[level] != null)
                return suit_EvaGround_VetBad_MaleNRM[level];

            else if (suit_EvaGround_Veteran_MaleNRM[level] != null)
                return suit_EvaGround_Veteran_MaleNRM[level];

            else if (suit_EvaGround_Standard_MaleNRM[level] != null)
            {
                return suit_EvaGround_Standard_MaleNRM[level];
            }
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Veteran_Female(int level)
        {
            if (suit_EvaGround_Veteran_Female[level] != null)
                return suit_EvaGround_Veteran_Female[level];
            else if (suit_EvaGround_Standard_Female[level] != null)
                return suit_EvaGround_Standard_Female[level];
            else if (suit_EvaGround_Veteran_Male[level] != null)
                return suit_EvaGround_Veteran_Male[level];
            else if (suit_EvaGround_Standard_Male[level] != null)
            {
                return suit_EvaGround_Standard_Male[level];
            }
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Veteran_FemaleNRM(int level)
        {
            if (suit_EvaGround_Veteran_FemaleNRM[level] != null)
                return suit_EvaGround_Veteran_FemaleNRM[level];
            else if (suit_EvaGround_Standard_FemaleNRM[level] != null)
                return suit_EvaGround_Standard_FemaleNRM[level];
            else if (suit_EvaGround_Veteran_MaleNRM[level] != null)
                return suit_EvaGround_Veteran_MaleNRM[level];
            else if (suit_EvaGround_Standard_MaleNRM[level] != null)
            {
                return suit_EvaGround_Standard_MaleNRM[level];
            }
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Veteran_Male(int level)
        {
            if (suit_EvaGround_Veteran_Male[level] != null)
                return suit_EvaGround_Veteran_Male[level];
            else if (suit_EvaGround_Standard_Male[level] != null)
            {
                return suit_EvaGround_Standard_Male[level];
            }
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaGround_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaGround_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaGround_Veteran_MaleNRM(int level)
        {
            if (suit_EvaGround_Veteran_MaleNRM[level] != null)
                return suit_EvaGround_Veteran_MaleNRM[level];
            else if (suit_EvaGround_Standard_MaleNRM[level] != null)
            {
                return suit_EvaGround_Standard_MaleNRM[level];
            }
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Badass_Female(int level)
        {
            if (suit_EvaSpace_Badass_Female[level] != null)
                return suit_EvaSpace_Badass_Female[level];
            else if (suit_EvaSpace_Standard_Female[level] != null)
                return suit_EvaSpace_Standard_Female[level];
            else if (suit_EvaSpace_Badass_Male[level] != null)
                return suit_EvaSpace_Badass_Male[level];
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Badass_FemaleNRM(int level)
        {
            if (suit_EvaSpace_Badass_FemaleNRM[level] != null)
                return suit_EvaSpace_Badass_FemaleNRM[level];
            else if (suit_EvaSpace_Standard_FemaleNRM[level] != null)
                return suit_EvaSpace_Standard_FemaleNRM[level];
            else if (suit_EvaSpace_Badass_MaleNRM[level] != null)
                return suit_EvaSpace_Badass_MaleNRM[level];
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Badass_Male(int level)
        {
            if (suit_EvaSpace_Badass_Male[level] != null)
                return suit_EvaSpace_Badass_Male[level];
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Badass_MaleNRM(int level)
        {
            if (suit_EvaSpace_Badass_MaleNRM[level] != null)
                return suit_EvaSpace_Badass_MaleNRM[level];
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Standard_Female(int level)
        {
            if (suit_EvaSpace_Standard_Female[level] != null)
                return suit_EvaSpace_Standard_Female[level];
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Standard_FemaleNRM(int level)
        {
            if (suit_EvaSpace_Standard_FemaleNRM[level] != null)
                return suit_EvaSpace_Standard_FemaleNRM[level];
            else
                return suit_EvaSpace_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Standard_Male(int level)
        {
            return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Standard_MaleNRM(int level)
        {
            return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_VetBad_Female(int level)
        {
            if (suit_EvaSpace_VetBad_Female[level] != null)
                return suit_EvaSpace_VetBad_Female[level];

            else if (suit_EvaSpace_Veteran_Female[level] != null)
                return suit_EvaSpace_Veteran_Female[level];

            else if (suit_EvaSpace_Standard_Female[level] != null)
                return suit_EvaSpace_Standard_Female[level];

            else if (suit_EvaSpace_VetBad_Male[level] != null)
                return suit_EvaSpace_VetBad_Male[level];

            else if (suit_EvaSpace_Veteran_Male[level] != null)
                return suit_EvaSpace_Veteran_Male[level];

            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_VetBad_FemaleNRM(int level)
        {
            if (suit_EvaSpace_VetBad_FemaleNRM[level] != null)
                return suit_EvaSpace_VetBad_FemaleNRM[level];

            else if (suit_EvaSpace_Veteran_FemaleNRM[level] != null)
                return suit_EvaSpace_Veteran_FemaleNRM[level];

            else if (suit_EvaSpace_Standard_FemaleNRM[level] != null)
                return suit_EvaSpace_Standard_FemaleNRM[level];

            else if (suit_EvaSpace_VetBad_MaleNRM[level] != null)
                return suit_EvaSpace_VetBad_MaleNRM[level];

            else if (suit_EvaSpace_Veteran_MaleNRM[level] != null)
                return suit_EvaSpace_Veteran_MaleNRM[level];

            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_VetBad_Male(int level)
        {
            if (suit_EvaSpace_VetBad_Male[level] != null)
                return suit_EvaSpace_VetBad_Male[level];

            else if (suit_EvaSpace_Veteran_Male[level] != null)
                return suit_EvaSpace_Veteran_Male[level];

            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_VetBad_MaleNRM(int level)
        {
            if (suit_EvaSpace_VetBad_MaleNRM[level] != null)
                return suit_EvaSpace_VetBad_MaleNRM[level];

            else if (suit_EvaSpace_Veteran_MaleNRM[level] != null)
                return suit_EvaSpace_Veteran_MaleNRM[level];

            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Veteran_Female(int level)
        {
            if (suit_EvaSpace_Veteran_Female[level] != null)
                return suit_EvaSpace_Veteran_Female[level];
            else if (suit_EvaSpace_Standard_Female[level] != null)
                return suit_EvaSpace_Standard_Female[level];
            else if (suit_EvaSpace_Veteran_Male[level] != null)
                return suit_EvaSpace_Veteran_Male[level];
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Veteran_FemaleNRM(int level)
        {
            if (suit_EvaSpace_Veteran_FemaleNRM[level] != null)
                return suit_EvaSpace_Veteran_FemaleNRM[level];
            else if (suit_EvaSpace_Standard_FemaleNRM[level] != null)
                return suit_EvaSpace_Standard_FemaleNRM[level];
            else if (suit_EvaSpace_Veteran_MaleNRM[level] != null)
                return suit_EvaSpace_Veteran_MaleNRM[level];
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Veteran_Male(int level)
        {
            if (suit_EvaSpace_Veteran_Male[level] != null)
                return suit_EvaSpace_Veteran_Male[level];
            else
                return suit_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_EvaSpace_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_EvaSpace_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_EvaSpace_Veteran_MaleNRM(int level)
        {
            if (suit_EvaSpace_Veteran_MaleNRM[level] != null)
                return suit_EvaSpace_Veteran_MaleNRM[level];
            else
                return suit_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Badass_Female(int level)
        {
            if (suit_Iva_Badass_Female[level] != null)
                return suit_Iva_Badass_Female[level];
            else if (suit_Iva_Standard_Female[level] != null)
                return suit_Iva_Standard_Female[level];
            else if (suit_Iva_Badass_Male[level] != null)
                return suit_Iva_Badass_Male[level];
            else
                return suit_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Badass_FemaleNRM(int level)
        {
            if (suit_Iva_Badass_FemaleNRM[level] != null)
                return suit_Iva_Badass_FemaleNRM[level];
            else if (suit_Iva_Standard_FemaleNRM[level] != null)
                return suit_Iva_Standard_FemaleNRM[level];
            else if (suit_Iva_Badass_MaleNRM[level] != null)
                return suit_Iva_Badass_MaleNRM[level];
            else
                return suit_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Badass_Male(int level)
        {
            if (suit_Iva_Badass_Male[level] != null)
                return suit_Iva_Badass_Male[level];
            else
                return suit_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Badass_MaleNRM(int level)
        {
            if (suit_Iva_Badass_MaleNRM[level] != null)
                return suit_Iva_Badass_MaleNRM[level];
            else
                return suit_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Standard_Female(int level)
        {
            if (suit_Iva_Standard_Female[level] != null)                           
                return suit_Iva_Standard_Female[level];            
            else
                return suit_Iva_Standard_Male[level];
    }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Standard_FemaleNRM(int level)
        {
            if (suit_Iva_Standard_FemaleNRM[level] != null)
                return suit_Iva_Standard_FemaleNRM[level];
            else
                return suit_Iva_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Standard_Male(int level)
        {
            return suit_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Standard_MaleNRM(int level)
        {
            return suit_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_VetBad_Female(int level)
        {
            if (suit_Iva_VetBad_Female[level] != null)
                return suit_Iva_VetBad_Female[level];

            else if (suit_Iva_Veteran_Female[level] != null)
                return suit_Iva_Veteran_Female[level];

            else if (suit_Iva_Standard_Female[level] != null)
                return suit_Iva_Standard_Female[level];

            else if (suit_Iva_VetBad_Male[level] != null)
                return suit_Iva_VetBad_Male[level];

            else if (suit_Iva_Veteran_Male[level] != null)
                return suit_Iva_Veteran_Male[level];

            else
                return suit_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_VetBad_FemaleNRM(int level)
        {
            if (suit_Iva_VetBad_FemaleNRM[level] != null)
                return suit_Iva_VetBad_FemaleNRM[level];

            else if (suit_Iva_Veteran_FemaleNRM[level] != null)
                return suit_Iva_Veteran_FemaleNRM[level];

            else if (suit_Iva_Standard_FemaleNRM[level] != null)
                return suit_Iva_Standard_FemaleNRM[level];

            else if (suit_Iva_VetBad_MaleNRM[level] != null)
                return suit_Iva_VetBad_MaleNRM[level];

            else if (suit_Iva_Veteran_MaleNRM[level] != null)
                return suit_Iva_Veteran_MaleNRM[level];

            else
                return suit_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_VetBad_Male(int level)
        {
            if (suit_Iva_VetBad_Male[level] != null)
                return suit_Iva_VetBad_Male[level];

            else if (suit_Iva_Veteran_Male[level] != null)
                return suit_Iva_Veteran_Male[level];

            else
                return suit_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_VetBad_MaleNRM(int level)
        {
            if (suit_Iva_VetBad_MaleNRM[level] != null)
                return suit_Iva_VetBad_MaleNRM[level];

            else if (suit_Iva_Veteran_MaleNRM[level] != null)
                return suit_Iva_Veteran_MaleNRM[level];

            else
                return suit_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Veteran_Female(int level)
        {
            if (suit_Iva_Veteran_Female[level] != null)
                return suit_Iva_Veteran_Female[level];
            else if (suit_Iva_Standard_Female[level] != null)
                return suit_Iva_Standard_Female[level];
            else if (suit_Iva_Veteran_Male[level] != null)
                return suit_Iva_Veteran_Male[level];
            else
                return suit_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Veteran_FemaleNRM(int level)
        {
            if (suit_Iva_Veteran_FemaleNRM[level] != null)
                return suit_Iva_Veteran_FemaleNRM[level];
            else if (suit_Iva_Standard_FemaleNRM[level] != null)
                return suit_Iva_Standard_FemaleNRM[level];
            else if (suit_Iva_Veteran_MaleNRM[level] != null)
                return suit_Iva_Veteran_MaleNRM[level];
            else
                return suit_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Veteran_Male(int level)
        {
            if (suit_Iva_Veteran_Male[level] != null)
                return suit_Iva_Veteran_Male[level];
            else
                return suit_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the suit_Iva_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The suit_Iva_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_suit_Iva_Veteran_MaleNRM(int level)
        {
            if (suit_Iva_Veteran_MaleNRM[level] != null)
                return suit_Iva_Veteran_MaleNRM[level];
            else
                return suit_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Badass_Female(int level)
        {
            if (visor_EvaGround_Badass_Female[level] != null)
                return visor_EvaGround_Badass_Female[level];
            else if (visor_EvaGround_Standard_Female[level] != null)
                return visor_EvaGround_Standard_Female[level];
            else if (visor_EvaGround_Badass_Male[level] != null)
                return visor_EvaGround_Badass_Male[level];
            else if (visor_EvaGround_Standard_Male[level] != null)
                return visor_EvaGround_Standard_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Badass_FemaleNRM(int level)
        {
            if (visor_EvaGround_Badass_FemaleNRM[level] != null)
                return visor_EvaGround_Badass_FemaleNRM[level];
            else if (visor_EvaGround_Standard_FemaleNRM[level] != null)
                return visor_EvaGround_Standard_FemaleNRM[level];
            else if (visor_EvaGround_Badass_MaleNRM[level] != null)
                return visor_EvaGround_Badass_MaleNRM[level];
            else if (visor_EvaGround_Standard_MaleNRM[level] != null)
                return visor_EvaGround_Standard_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Badass_Male(int level)
        {
            if (visor_EvaGround_Badass_Male[level] != null)
                return visor_EvaGround_Badass_Male[level];
            else if (visor_EvaGround_Standard_Male[level] != null)
                return visor_EvaGround_Standard_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Badass_MaleNRM(int level)
        {
            if (visor_EvaGround_Badass_MaleNRM[level] != null)
                return visor_EvaGround_Badass_MaleNRM[level];
            else if (visor_EvaGround_Standard_MaleNRM[level] != null)
                return visor_EvaGround_Standard_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Standard_Female(int level)
        {
            if (visor_EvaGround_Standard_Female[level] != null)
                return visor_EvaGround_Standard_Female[level];
            else if (visor_EvaGround_Standard_Male[level] != null)
                return visor_EvaGround_Standard_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Standard_FemaleNRM(int level)
        {
            if (visor_EvaGround_Standard_FemaleNRM[level] != null)
                return visor_EvaGround_Standard_FemaleNRM[level];
            else if (visor_EvaGround_Standard_MaleNRM[level] != null)
                return visor_EvaGround_Standard_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Standard_Male(int level)
        {
            if (visor_EvaGround_Standard_Male[level] != null)
                return visor_EvaGround_Standard_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Standard_MaleNRM(int level)
        {
            if (visor_EvaGround_Standard_MaleNRM[level] != null)
                return visor_EvaGround_Standard_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_VetBad_Female(int level)
        {
            if (visor_EvaGround_VetBad_Female[level] != null)
                return visor_EvaGround_VetBad_Female[level];

            else if (visor_EvaGround_Veteran_Female[level] != null)
                return visor_EvaGround_Veteran_Female[level];

            else if (visor_EvaGround_Standard_Female[level] != null)
                return visor_EvaGround_Standard_Female[level];

            else if (visor_EvaGround_VetBad_Male[level] != null)
                return visor_EvaGround_VetBad_Male[level];

            else if (visor_EvaGround_Veteran_Male[level] != null)
                return visor_EvaGround_Veteran_Male[level];
            else if (visor_EvaGround_Standard_Male[level] != null)
                return visor_EvaGround_Standard_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_VetBad_FemaleNRM(int level)
        {
            if (visor_EvaGround_VetBad_FemaleNRM[level] != null)
                return visor_EvaGround_VetBad_FemaleNRM[level];

            else if (visor_EvaGround_Veteran_FemaleNRM[level] != null)
                return visor_EvaGround_Veteran_FemaleNRM[level];

            else if (visor_EvaGround_Standard_FemaleNRM[level] != null)
                return visor_EvaGround_Standard_FemaleNRM[level];

            else if (visor_EvaGround_VetBad_MaleNRM[level] != null)
                return visor_EvaGround_VetBad_MaleNRM[level];

            else if (visor_EvaGround_Veteran_MaleNRM[level] != null)
                return visor_EvaGround_Veteran_MaleNRM[level];
            else if (visor_EvaGround_Standard_MaleNRM[level] != null)
                return visor_EvaGround_Standard_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_VetBad_Male(int level)
        {
            if (visor_EvaGround_VetBad_Male[level] != null)
                return visor_EvaGround_VetBad_Male[level];

            else if (visor_EvaGround_Veteran_Male[level] != null)
                return visor_EvaGround_Veteran_Male[level];
            else if (visor_EvaGround_Standard_Male[level] != null)
                return visor_EvaGround_Standard_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_VetBad_MaleNRM(int level)
        {
            if (visor_EvaGround_VetBad_MaleNRM[level] != null)
                return visor_EvaGround_VetBad_MaleNRM[level];

            else if (visor_EvaGround_Veteran_MaleNRM[level] != null)
                return visor_EvaGround_Veteran_MaleNRM[level];
            else if (visor_EvaGround_Standard_MaleNRM[level] != null)
                return visor_EvaGround_Standard_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Veteran_Female(int level)
        {
            if (visor_EvaGround_Veteran_Female[level] != null)
                return visor_EvaGround_Veteran_Female[level];
            else if (visor_EvaGround_Standard_Female[level] != null)
                return visor_EvaGround_Standard_Female[level];
            else if (visor_EvaGround_Veteran_Male[level] != null)
                return visor_EvaGround_Veteran_Male[level];
            else if (visor_EvaGround_Standard_Male[level] != null)
                return visor_EvaGround_Standard_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Veteran_FemaleNRM(int level)
        {
            if (visor_EvaGround_Veteran_FemaleNRM[level] != null)
                return visor_EvaGround_Veteran_FemaleNRM[level];
            else if (visor_EvaGround_Standard_FemaleNRM[level] != null)
                return visor_EvaGround_Standard_FemaleNRM[level];
            else if (visor_EvaGround_Veteran_MaleNRM[level] != null)
                return visor_EvaGround_Veteran_MaleNRM[level];
            else if (visor_EvaGround_Standard_MaleNRM[level] != null)
                return visor_EvaGround_Standard_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Veteran_Male(int level)
        {
            if (visor_EvaGround_Veteran_Male[level] != null)
                return visor_EvaGround_Veteran_Male[level];
            else if (visor_EvaGround_Standard_Male[level] != null)
                return visor_EvaGround_Standard_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaGround_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaGround_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaGround_Veteran_MaleNRM(int level)
        {
            if (visor_EvaGround_Veteran_MaleNRM[level] != null)
                return visor_EvaGround_Veteran_MaleNRM[level];
            else if (visor_EvaGround_Standard_MaleNRM[level] != null)
                return visor_EvaGround_Standard_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Badass_Female(int level)
        {
            if (visor_EvaSpace_Badass_Female[level] != null)
                return visor_EvaSpace_Badass_Female[level];
            else if (visor_EvaSpace_Standard_Female[level] != null)
                return visor_EvaSpace_Standard_Female[level];
            else if (visor_EvaSpace_Badass_Male[level] != null)
                return visor_EvaSpace_Badass_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Badass_FemaleNRM(int level)
        {
            if (visor_EvaSpace_Badass_FemaleNRM[level] != null)
                return visor_EvaSpace_Badass_FemaleNRM[level];
            else if (visor_EvaSpace_Standard_FemaleNRM[level] != null)
                return visor_EvaSpace_Standard_FemaleNRM[level];
            else if (visor_EvaSpace_Badass_MaleNRM[level] != null)
                return visor_EvaSpace_Badass_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Badass_Male(int level)
        {
            if (visor_EvaSpace_Badass_Male[level] != null)
                return visor_EvaSpace_Badass_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Badass_MaleNRM(int level)
        {
            if (visor_EvaSpace_Badass_MaleNRM[level] != null)
                return visor_EvaSpace_Badass_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Standard_Female(int level)
        {
            if (visor_EvaSpace_Standard_Female[level] != null)
                return visor_EvaSpace_Standard_Female[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Standard_FemaleNRM(int level)
        {
            if (visor_EvaSpace_Standard_FemaleNRM[level] != null)
                return visor_EvaSpace_Standard_FemaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Standard_Male(int level)
        {
            return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Standard_MaleNRM(int level)
        {
            return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_VetBad_Female(int level)
        {
            if (visor_EvaSpace_VetBad_Female[level] != null)
                return visor_EvaSpace_VetBad_Female[level];

            else if (visor_EvaSpace_Veteran_Female[level] != null)
                return visor_EvaSpace_Veteran_Female[level];

            else if (visor_EvaSpace_Standard_Female[level] != null)
                return visor_EvaSpace_Standard_Female[level];

            else if (visor_EvaSpace_VetBad_Male[level] != null)
                return visor_EvaSpace_VetBad_Male[level];

            else if (visor_EvaSpace_Veteran_Male[level] != null)
                return visor_EvaSpace_Veteran_Male[level];

            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_VetBad_FemaleNRM(int level)
        {
            if (visor_EvaSpace_VetBad_FemaleNRM[level] != null)
                return visor_EvaSpace_VetBad_FemaleNRM[level];

            else if (visor_EvaSpace_Veteran_FemaleNRM[level] != null)
                return visor_EvaSpace_Veteran_FemaleNRM[level];

            else if (visor_EvaSpace_Standard_FemaleNRM[level] != null)
                return visor_EvaSpace_Standard_FemaleNRM[level];

            else if (visor_EvaSpace_VetBad_MaleNRM[level] != null)
                return visor_EvaSpace_VetBad_MaleNRM[level];

            else if (visor_EvaSpace_Veteran_MaleNRM[level] != null)
                return visor_EvaSpace_Veteran_MaleNRM[level];

            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_VetBad_Male(int level)
        {
            if (visor_EvaSpace_VetBad_Male[level] != null)
                return visor_EvaSpace_VetBad_Male[level];

            else if (visor_EvaSpace_Veteran_Male[level] != null)
                return visor_EvaSpace_Veteran_Male[level];

            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_VetBad_MaleNRM(int level)
        {
            if (visor_EvaSpace_VetBad_MaleNRM[level] != null)
                return visor_EvaSpace_VetBad_MaleNRM[level];

            else if (visor_EvaSpace_Veteran_MaleNRM[level] != null)
                return visor_EvaSpace_Veteran_MaleNRM[level];

            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Veteran_Female(int level)
        {
            if (visor_EvaSpace_Veteran_Female[level] != null)
                return visor_EvaSpace_Veteran_Female[level];
            else if (visor_EvaSpace_Standard_Female[level] != null)
                return visor_EvaSpace_Standard_Female[level];
            else if (visor_EvaSpace_Veteran_Male[level] != null)
                return visor_EvaSpace_Veteran_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Veteran_FemaleNRM(int level)
        {
            if (visor_EvaSpace_Veteran_FemaleNRM[level] != null)
                return visor_EvaSpace_Veteran_FemaleNRM[level];
            else if (visor_EvaSpace_Standard_FemaleNRM[level] != null)
                return visor_EvaSpace_Standard_FemaleNRM[level];
            else if (visor_EvaSpace_Veteran_MaleNRM[level] != null)
                return visor_EvaSpace_Veteran_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Veteran_Male(int level)
        {
            if (visor_EvaSpace_Veteran_Male[level] != null)
                return visor_EvaSpace_Veteran_Male[level];
            else
                return visor_EvaSpace_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_EvaSpace_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_EvaSpace_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_EvaSpace_Veteran_MaleNRM(int level)
        {
            if (visor_EvaSpace_Veteran_MaleNRM[level] != null)
                return visor_EvaSpace_Veteran_MaleNRM[level];
            else
                return visor_EvaSpace_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Badass_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Badass_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Badass_Female(int level)
        {
            if (visor_Iva_Badass_Female[level] != null)
                return visor_Iva_Badass_Female[level];
            else if (visor_Iva_Standard_Female[level] != null)
                return visor_Iva_Standard_Female[level];
            else if (visor_Iva_Badass_Male[level] != null)
                return visor_Iva_Badass_Male[level];
            else
                return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Badass_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Badass_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Badass_FemaleNRM(int level)
        {
            if (visor_Iva_Badass_FemaleNRM[level] != null)
                return visor_Iva_Badass_FemaleNRM[level];
            else if (visor_Iva_Standard_FemaleNRM[level] != null)
                return visor_Iva_Standard_FemaleNRM[level];
            else if (visor_Iva_Badass_MaleNRM[level] != null)
                return visor_Iva_Badass_MaleNRM[level];
            else
                return visor_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Badass_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Badass_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Badass_Male(int level)
        {
            if (visor_Iva_Badass_Male[level] != null)
                return visor_Iva_Badass_Male[level];
            else
                return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Badass_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Badass_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Badass_MaleNRM(int level)
        {
            if (visor_Iva_Badass_MaleNRM[level] != null)
                return visor_Iva_Badass_MaleNRM[level];
            else
                return visor_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Standard_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Standard_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Standard_Female(int level)
        {
            if (visor_Iva_Standard_Female[level] != null)
                return visor_Iva_Standard_Female[level];
            else
                return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Standard_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Standard_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Standard_FemaleNRM(int level)
        {
            if (visor_Iva_Standard_FemaleNRM[level] != null)
                return visor_Iva_Standard_FemaleNRM[level];
            else
                return visor_Iva_Standard_MaleNRM[level];

        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Standard_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Standard_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Standard_Male(int level)
        {
            return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Standard_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Standard_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Standard_MaleNRM(int level)
        {
            return visor_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_VetBad_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_VetBad_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_VetBad_Female(int level)
        {
            if (visor_Iva_VetBad_Female[level] != null)
                return visor_Iva_VetBad_Female[level];

            else if (visor_Iva_Veteran_Female[level] != null)
                return visor_Iva_Veteran_Female[level];

            else if (visor_Iva_Standard_Female[level] != null)
                return visor_Iva_Standard_Female[level];

            else if (visor_Iva_VetBad_Male[level] != null)
                return visor_Iva_VetBad_Male[level];

            else if (visor_Iva_Veteran_Male[level] != null)
                return visor_Iva_Veteran_Male[level];

            else
                return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_VetBad_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_VetBad_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_VetBad_FemaleNRM(int level)
        {
            if (visor_Iva_VetBad_FemaleNRM[level] != null)
                return visor_Iva_VetBad_FemaleNRM[level];

            else if (visor_Iva_Veteran_FemaleNRM[level] != null)
                return visor_Iva_Veteran_FemaleNRM[level];

            else if (visor_Iva_Standard_FemaleNRM[level] != null)
                return visor_Iva_Standard_FemaleNRM[level];

            else if (visor_Iva_VetBad_MaleNRM[level] != null)
                return visor_Iva_VetBad_MaleNRM[level];

            else if (visor_Iva_Veteran_MaleNRM[level] != null)
                return visor_Iva_Veteran_MaleNRM[level];

            else
                return visor_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_VetBad_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_VetBad_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_VetBad_Male(int level)
        {
            if (visor_Iva_VetBad_Male[level] != null)
                return visor_Iva_VetBad_Male[level];

            else if (visor_Iva_Veteran_Male[level] != null)
                return visor_Iva_Veteran_Male[level];

            else
                return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_VetBad_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_VetBad_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_VetBad_MaleNRM(int level)
        {
            if (visor_Iva_VetBad_MaleNRM[level] != null)
                return visor_Iva_VetBad_MaleNRM[level];

            else if (visor_Iva_Veteran_MaleNRM[level] != null)
                return visor_Iva_Veteran_MaleNRM[level];

            else
                return visor_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Veteran_Female for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Veteran_Female texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Veteran_Female(int level)
        {
            if (visor_Iva_Veteran_Female[level] != null)
                return visor_Iva_Veteran_Female[level];
            else if (visor_Iva_Standard_Female[level] != null)
                return visor_Iva_Standard_Female[level];
            else if (visor_Iva_Veteran_Male[level] != null)
                return visor_Iva_Veteran_Male[level];
            else
                return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Veteran_FemaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Veteran_Female Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Veteran_FemaleNRM(int level)
        {
            if (visor_Iva_Veteran_FemaleNRM[level] != null)
                return visor_Iva_Veteran_FemaleNRM[level];
            else if (visor_Iva_Standard_FemaleNRM[level] != null)
                return visor_Iva_Standard_FemaleNRM[level];
            else if (visor_Iva_Veteran_MaleNRM[level] != null)
                return visor_Iva_Veteran_MaleNRM[level];
            else
                return visor_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Veteran_Male for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Veteran_Male texture for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Veteran_Male(int level)
        {
            if (visor_Iva_Veteran_Male[level] != null)
                return visor_Iva_Veteran_Male[level];
            else
                return visor_Iva_Standard_Male[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Used to get the visor_Iva_Veteran_MaleNRM for the level of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns>The visor_Iva_Veteran_Male Normal map for the level of the kerbal</returns>
        /// ************************************************************************************
        public Texture2D get_visor_Iva_Veteran_MaleNRM(int level)
        {
            if (visor_Iva_Veteran_MaleNRM[level] != null)
                return visor_Iva_Veteran_MaleNRM[level];
            else
                return visor_Iva_Standard_MaleNRM[level];
        }

        /// ************************************************************************************
        /// <summary>
        /// Search for the name of the texture, then set the good one in the suit set.
        /// <para>Related to <see cref="Personaliser.Suit"/> class. </para> 
        /// </summary>
        /// <param name="originalName">The name of the texture file (like KerbalMain.dds) 
        /// we want to save in the suit set.</param>
        /// <param name="texture">The texture2D we want to save in the suit set.</param>
        /// <returns>True if the texture is found and saved and false if not.</returns>
        /// ************************************************************************************
        public bool setTexture(string originalName, Texture2D texture)
        {
            int level;

            helmet_EvaGround_Badass_Female = helmet_EvaGround_Badass_Female ?? new Texture2D[6];
            helmet_EvaGround_Badass_FemaleNRM = helmet_EvaGround_Badass_FemaleNRM ?? new Texture2D[6];
            helmet_EvaGround_Badass_Male = helmet_EvaGround_Badass_Male ?? new Texture2D[6];
            helmet_EvaGround_Badass_MaleNRM = helmet_EvaGround_Badass_MaleNRM ?? new Texture2D[6];
            helmet_EvaGround_Standard_Female = helmet_EvaGround_Standard_Female ?? new Texture2D[6];
            helmet_EvaGround_Standard_FemaleNRM = helmet_EvaGround_Standard_FemaleNRM ?? new Texture2D[6];
            helmet_EvaGround_Standard_Male = helmet_EvaGround_Standard_Male ?? new Texture2D[6];
            helmet_EvaGround_Standard_MaleNRM = helmet_EvaGround_Standard_MaleNRM ?? new Texture2D[6];
            helmet_EvaGround_VetBad_Female = helmet_EvaGround_VetBad_Female ?? new Texture2D[6];
            helmet_EvaGround_VetBad_FemaleNRM = helmet_EvaGround_VetBad_FemaleNRM ?? new Texture2D[6];
            helmet_EvaGround_VetBad_Male = helmet_EvaGround_VetBad_Male ?? new Texture2D[6];
            helmet_EvaGround_VetBad_MaleNRM = helmet_EvaGround_VetBad_MaleNRM ?? new Texture2D[6];
            helmet_EvaGround_Veteran_Female = helmet_EvaGround_Veteran_Female ?? new Texture2D[6];
            helmet_EvaGround_Veteran_FemaleNRM = helmet_EvaGround_Veteran_FemaleNRM ?? new Texture2D[6];
            helmet_EvaGround_Veteran_Male = helmet_EvaGround_Veteran_Male ?? new Texture2D[6];
            helmet_EvaGround_Veteran_MaleNRM = helmet_EvaGround_Veteran_MaleNRM ?? new Texture2D[6];
            helmet_EvaSpace_Badass_Female = helmet_EvaSpace_Badass_Female ?? new Texture2D[6];
            helmet_EvaSpace_Badass_FemaleNRM = helmet_EvaSpace_Badass_FemaleNRM ?? new Texture2D[6];
            helmet_EvaSpace_Badass_Male = helmet_EvaSpace_Badass_Male ?? new Texture2D[6];
            helmet_EvaSpace_Badass_MaleNRM = helmet_EvaSpace_Badass_MaleNRM ?? new Texture2D[6];
            helmet_EvaSpace_Standard_Female = helmet_EvaSpace_Standard_Female ?? new Texture2D[6];
            helmet_EvaSpace_Standard_FemaleNRM = helmet_EvaSpace_Standard_FemaleNRM ?? new Texture2D[6];
            helmet_EvaSpace_Standard_Male = helmet_EvaSpace_Standard_Male ?? new Texture2D[6];
            helmet_EvaSpace_Standard_MaleNRM = helmet_EvaSpace_Standard_MaleNRM ?? new Texture2D[6];
            helmet_EvaSpace_VetBad_Female = helmet_EvaSpace_VetBad_Female ?? new Texture2D[6];
            helmet_EvaSpace_VetBad_FemaleNRM = helmet_EvaSpace_VetBad_FemaleNRM ?? new Texture2D[6];
            helmet_EvaSpace_VetBad_Male = helmet_EvaSpace_VetBad_Male ?? new Texture2D[6];
            helmet_EvaSpace_VetBad_MaleNRM = helmet_EvaSpace_VetBad_MaleNRM ?? new Texture2D[6];
            helmet_EvaSpace_Veteran_Female = helmet_EvaSpace_Veteran_Female ?? new Texture2D[6];
            helmet_EvaSpace_Veteran_FemaleNRM = helmet_EvaSpace_Veteran_FemaleNRM ?? new Texture2D[6];
            helmet_EvaSpace_Veteran_Male = helmet_EvaSpace_Veteran_Male ?? new Texture2D[6];
            helmet_EvaSpace_Veteran_MaleNRM = helmet_EvaSpace_Veteran_MaleNRM ?? new Texture2D[6];
            helmet_Iva_Badass_Female = helmet_Iva_Badass_Female ?? new Texture2D[6];
            helmet_Iva_Badass_FemaleNRM = helmet_Iva_Badass_FemaleNRM ?? new Texture2D[6];
            helmet_Iva_Badass_Male = helmet_Iva_Badass_Male ?? new Texture2D[6];
            helmet_Iva_Badass_MaleNRM = helmet_Iva_Badass_MaleNRM ?? new Texture2D[6];
            helmet_Iva_Standard_Female = helmet_Iva_Standard_Female ?? new Texture2D[6];
            helmet_Iva_Standard_FemaleNRM = helmet_Iva_Standard_FemaleNRM ?? new Texture2D[6];
            helmet_Iva_Standard_Male = helmet_Iva_Standard_Male ?? new Texture2D[6];
            helmet_Iva_Standard_MaleNRM = helmet_Iva_Standard_MaleNRM ?? new Texture2D[6];
            helmet_Iva_VetBad_Female = helmet_Iva_VetBad_Female ?? new Texture2D[6];
            helmet_Iva_VetBad_FemaleNRM = helmet_Iva_VetBad_FemaleNRM ?? new Texture2D[6];
            helmet_Iva_VetBad_Male = helmet_Iva_VetBad_Male ?? new Texture2D[6];
            helmet_Iva_VetBad_MaleNRM = helmet_Iva_VetBad_MaleNRM ?? new Texture2D[6];
            helmet_Iva_Veteran_Female = helmet_Iva_Veteran_Female ?? new Texture2D[6];
            helmet_Iva_Veteran_FemaleNRM = helmet_Iva_Veteran_FemaleNRM ?? new Texture2D[6];
            helmet_Iva_Veteran_Male = helmet_Iva_Veteran_Male ?? new Texture2D[6];
            helmet_Iva_Veteran_MaleNRM = helmet_Iva_Veteran_MaleNRM ?? new Texture2D[6];
            jetpack_EvaGround_Badass_Female = jetpack_EvaGround_Badass_Female ?? new Texture2D[6];
            jetpack_EvaGround_Badass_FemaleNRM = jetpack_EvaGround_Badass_FemaleNRM ?? new Texture2D[6];
            jetpack_EvaGround_Badass_Male = jetpack_EvaGround_Badass_Male ?? new Texture2D[6];
            jetpack_EvaGround_Badass_MaleNRM = jetpack_EvaGround_Badass_MaleNRM ?? new Texture2D[6];
            jetpack_EvaGround_Standard_Female = jetpack_EvaGround_Standard_Female ?? new Texture2D[6];
            jetpack_EvaGround_Standard_FemaleNRM = jetpack_EvaGround_Standard_FemaleNRM ?? new Texture2D[6];
            jetpack_EvaGround_Standard_Male = jetpack_EvaGround_Standard_Male ?? new Texture2D[6];
            jetpack_EvaGround_Standard_MaleNRM = jetpack_EvaGround_Standard_MaleNRM ?? new Texture2D[6];
            jetpack_EvaGround_VetBad_Female = jetpack_EvaGround_VetBad_Female ?? new Texture2D[6];
            jetpack_EvaGround_VetBad_FemaleNRM = jetpack_EvaGround_VetBad_FemaleNRM ?? new Texture2D[6];
            jetpack_EvaGround_VetBad_Male = jetpack_EvaGround_VetBad_Male ?? new Texture2D[6];
            jetpack_EvaGround_VetBad_MaleNRM = jetpack_EvaGround_VetBad_MaleNRM ?? new Texture2D[6];
            jetpack_EvaGround_Veteran_Female = jetpack_EvaGround_Veteran_Female ?? new Texture2D[6];
            jetpack_EvaGround_Veteran_FemaleNRM = jetpack_EvaGround_Veteran_FemaleNRM ?? new Texture2D[6];
            jetpack_EvaGround_Veteran_Male = jetpack_EvaGround_Veteran_Male ?? new Texture2D[6];
            jetpack_EvaGround_Veteran_MaleNRM = jetpack_EvaGround_Veteran_MaleNRM ?? new Texture2D[6];
            jetpack_EvaSpace_Badass_Female = jetpack_EvaSpace_Badass_Female ?? new Texture2D[6];
            jetpack_EvaSpace_Badass_FemaleNRM = jetpack_EvaSpace_Badass_FemaleNRM ?? new Texture2D[6];
            jetpack_EvaSpace_Badass_Male = jetpack_EvaSpace_Badass_Male ?? new Texture2D[6];
            jetpack_EvaSpace_Badass_MaleNRM = jetpack_EvaSpace_Badass_MaleNRM ?? new Texture2D[6];
            jetpack_EvaSpace_Standard_Female = jetpack_EvaSpace_Standard_Female ?? new Texture2D[6];
            jetpack_EvaSpace_Standard_FemaleNRM = jetpack_EvaSpace_Standard_FemaleNRM ?? new Texture2D[6];
            jetpack_EvaSpace_Standard_Male = jetpack_EvaSpace_Standard_Male ?? new Texture2D[6];
            jetpack_EvaSpace_Standard_MaleNRM = jetpack_EvaSpace_Standard_MaleNRM ?? new Texture2D[6];
            jetpack_EvaSpace_VetBad_Female = jetpack_EvaSpace_VetBad_Female ?? new Texture2D[6];
            jetpack_EvaSpace_VetBad_FemaleNRM = jetpack_EvaSpace_VetBad_FemaleNRM ?? new Texture2D[6];
            jetpack_EvaSpace_VetBad_Male = jetpack_EvaSpace_VetBad_Male ?? new Texture2D[6];
            jetpack_EvaSpace_VetBad_MaleNRM = jetpack_EvaSpace_VetBad_MaleNRM ?? new Texture2D[6];
            jetpack_EvaSpace_Veteran_Female = jetpack_EvaSpace_Veteran_Female ?? new Texture2D[6];
            jetpack_EvaSpace_Veteran_FemaleNRM = jetpack_EvaSpace_Veteran_FemaleNRM ?? new Texture2D[6];
            jetpack_EvaSpace_Veteran_Male = jetpack_EvaSpace_Veteran_Male ?? new Texture2D[6];
            jetpack_EvaSpace_Veteran_MaleNRM = jetpack_EvaSpace_Veteran_MaleNRM ?? new Texture2D[6];
            suit_EvaGround_Badass_Female = suit_EvaGround_Badass_Female ?? new Texture2D[6];
            suit_EvaGround_Badass_FemaleNRM = suit_EvaGround_Badass_FemaleNRM ?? new Texture2D[6];
            suit_EvaGround_Badass_Male = suit_EvaGround_Badass_Male ?? new Texture2D[6];
            suit_EvaGround_Badass_MaleNRM = suit_EvaGround_Badass_MaleNRM ?? new Texture2D[6];
            suit_EvaGround_Standard_Female = suit_EvaGround_Standard_Female ?? new Texture2D[6];
            suit_EvaGround_Standard_FemaleNRM = suit_EvaGround_Standard_FemaleNRM ?? new Texture2D[6];
            suit_EvaGround_Standard_Male = suit_EvaGround_Standard_Male ?? new Texture2D[6];
            suit_EvaGround_Standard_MaleNRM = suit_EvaGround_Standard_MaleNRM ?? new Texture2D[6];
            suit_EvaGround_VetBad_Female = suit_EvaGround_VetBad_Female ?? new Texture2D[6];
            suit_EvaGround_VetBad_FemaleNRM = suit_EvaGround_VetBad_FemaleNRM ?? new Texture2D[6];
            suit_EvaGround_VetBad_Male = suit_EvaGround_VetBad_Male ?? new Texture2D[6];
            suit_EvaGround_VetBad_MaleNRM = suit_EvaGround_VetBad_MaleNRM ?? new Texture2D[6];
            suit_EvaGround_Veteran_Female = suit_EvaGround_Veteran_Female ?? new Texture2D[6];
            suit_EvaGround_Veteran_FemaleNRM = suit_EvaGround_Veteran_FemaleNRM ?? new Texture2D[6];
            suit_EvaGround_Veteran_Male = suit_EvaGround_Veteran_Male ?? new Texture2D[6];
            suit_EvaGround_Veteran_MaleNRM = suit_EvaGround_Veteran_MaleNRM ?? new Texture2D[6];
            suit_EvaSpace_Badass_Female = suit_EvaSpace_Badass_Female ?? new Texture2D[6];
            suit_EvaSpace_Badass_FemaleNRM = suit_EvaSpace_Badass_FemaleNRM ?? new Texture2D[6];
            suit_EvaSpace_Badass_Male = suit_EvaSpace_Badass_Male ?? new Texture2D[6];
            suit_EvaSpace_Badass_MaleNRM = suit_EvaSpace_Badass_MaleNRM ?? new Texture2D[6];
            suit_EvaSpace_Standard_Female = suit_EvaSpace_Standard_Female ?? new Texture2D[6];
            suit_EvaSpace_Standard_FemaleNRM = suit_EvaSpace_Standard_FemaleNRM ?? new Texture2D[6];
            suit_EvaSpace_Standard_Male = suit_EvaSpace_Standard_Male ?? new Texture2D[6];
            suit_EvaSpace_Standard_MaleNRM = suit_EvaSpace_Standard_MaleNRM ?? new Texture2D[6];
            suit_EvaSpace_VetBad_Female = suit_EvaSpace_VetBad_Female ?? new Texture2D[6];
            suit_EvaSpace_VetBad_FemaleNRM = suit_EvaSpace_VetBad_FemaleNRM ?? new Texture2D[6];
            suit_EvaSpace_VetBad_Male = suit_EvaSpace_VetBad_Male ?? new Texture2D[6];
            suit_EvaSpace_VetBad_MaleNRM = suit_EvaSpace_VetBad_MaleNRM ?? new Texture2D[6];
            suit_EvaSpace_Veteran_Female = suit_EvaSpace_Veteran_Female ?? new Texture2D[6];
            suit_EvaSpace_Veteran_FemaleNRM = suit_EvaSpace_Veteran_FemaleNRM ?? new Texture2D[6];
            suit_EvaSpace_Veteran_Male = suit_EvaSpace_Veteran_Male ?? new Texture2D[6];
            suit_EvaSpace_Veteran_MaleNRM = suit_EvaSpace_Veteran_MaleNRM ?? new Texture2D[6];
            suit_Iva_Badass_Female = suit_Iva_Badass_Female ?? new Texture2D[6];
            suit_Iva_Badass_FemaleNRM = suit_Iva_Badass_FemaleNRM ?? new Texture2D[6];
            suit_Iva_Badass_Male = suit_Iva_Badass_Male ?? new Texture2D[6];
            suit_Iva_Badass_MaleNRM = suit_Iva_Badass_MaleNRM ?? new Texture2D[6];
            suit_Iva_Standard_Female = suit_Iva_Standard_Female ?? new Texture2D[6];
            suit_Iva_Standard_FemaleNRM = suit_Iva_Standard_FemaleNRM ?? new Texture2D[6];
            suit_Iva_Standard_Male = suit_Iva_Standard_Male ?? new Texture2D[6];
            suit_Iva_Standard_MaleNRM = suit_Iva_Standard_MaleNRM ?? new Texture2D[6];
            suit_Iva_VetBad_Female = suit_Iva_VetBad_Female ?? new Texture2D[6];
            suit_Iva_VetBad_FemaleNRM = suit_Iva_VetBad_FemaleNRM ?? new Texture2D[6];
            suit_Iva_VetBad_Male = suit_Iva_VetBad_Male ?? new Texture2D[6];
            suit_Iva_VetBad_MaleNRM = suit_Iva_VetBad_MaleNRM ?? new Texture2D[6];
            suit_Iva_Veteran_Female = suit_Iva_Veteran_Female ?? new Texture2D[6];
            suit_Iva_Veteran_FemaleNRM = suit_Iva_Veteran_FemaleNRM ?? new Texture2D[6];
            suit_Iva_Veteran_Male = suit_Iva_Veteran_Male ?? new Texture2D[6];
            suit_Iva_Veteran_MaleNRM = suit_Iva_Veteran_MaleNRM ?? new Texture2D[6];
            visor_EvaGround_Badass_Female = visor_EvaGround_Badass_Female ?? new Texture2D[6];
            visor_EvaGround_Badass_FemaleNRM = visor_EvaGround_Badass_FemaleNRM ?? new Texture2D[6];
            visor_EvaGround_Badass_Male = visor_EvaGround_Badass_Male ?? new Texture2D[6];
            visor_EvaGround_Badass_MaleNRM = visor_EvaGround_Badass_MaleNRM ?? new Texture2D[6];
            visor_EvaGround_Standard_Female = visor_EvaGround_Standard_Female ?? new Texture2D[6];
            visor_EvaGround_Standard_FemaleNRM = visor_EvaGround_Standard_FemaleNRM ?? new Texture2D[6];
            visor_EvaGround_Standard_Male = visor_EvaGround_Standard_Male ?? new Texture2D[6];
            visor_EvaGround_Standard_MaleNRM = visor_EvaGround_Standard_MaleNRM ?? new Texture2D[6];
            visor_EvaGround_VetBad_Female = visor_EvaGround_VetBad_Female ?? new Texture2D[6];
            visor_EvaGround_VetBad_FemaleNRM = visor_EvaGround_VetBad_FemaleNRM ?? new Texture2D[6];
            visor_EvaGround_VetBad_Male = visor_EvaGround_VetBad_Male ?? new Texture2D[6];
            visor_EvaGround_VetBad_MaleNRM = visor_EvaGround_VetBad_MaleNRM ?? new Texture2D[6];
            visor_EvaGround_Veteran_Female = visor_EvaGround_Veteran_Female ?? new Texture2D[6];
            visor_EvaGround_Veteran_FemaleNRM = visor_EvaGround_Veteran_FemaleNRM ?? new Texture2D[6];
            visor_EvaGround_Veteran_Male = visor_EvaGround_Veteran_Male ?? new Texture2D[6];
            visor_EvaGround_Veteran_MaleNRM = visor_EvaGround_Veteran_MaleNRM ?? new Texture2D[6];
            visor_EvaSpace_Badass_Female = visor_EvaSpace_Badass_Female ?? new Texture2D[6];
            visor_EvaSpace_Badass_FemaleNRM = visor_EvaSpace_Badass_FemaleNRM ?? new Texture2D[6];
            visor_EvaSpace_Badass_Male = visor_EvaSpace_Badass_Male ?? new Texture2D[6];
            visor_EvaSpace_Badass_MaleNRM = visor_EvaSpace_Badass_MaleNRM ?? new Texture2D[6];
            visor_EvaSpace_Standard_Female = visor_EvaSpace_Standard_Female ?? new Texture2D[6];
            visor_EvaSpace_Standard_FemaleNRM = visor_EvaSpace_Standard_FemaleNRM ?? new Texture2D[6];
            visor_EvaSpace_Standard_Male = visor_EvaSpace_Standard_Male ?? new Texture2D[6];
            visor_EvaSpace_Standard_MaleNRM = visor_EvaSpace_Standard_MaleNRM ?? new Texture2D[6];
            visor_EvaSpace_VetBad_Female = visor_EvaSpace_VetBad_Female ?? new Texture2D[6];
            visor_EvaSpace_VetBad_FemaleNRM = visor_EvaSpace_VetBad_FemaleNRM ?? new Texture2D[6];
            visor_EvaSpace_VetBad_Male = visor_EvaSpace_VetBad_Male ?? new Texture2D[6];
            visor_EvaSpace_VetBad_MaleNRM = visor_EvaSpace_VetBad_MaleNRM ?? new Texture2D[6];
            visor_EvaSpace_Veteran_Female = visor_EvaSpace_Veteran_Female ?? new Texture2D[6];
            visor_EvaSpace_Veteran_FemaleNRM = visor_EvaSpace_Veteran_FemaleNRM ?? new Texture2D[6];
            visor_EvaSpace_Veteran_Male = visor_EvaSpace_Veteran_Male ?? new Texture2D[6];
            visor_EvaSpace_Veteran_MaleNRM = visor_EvaSpace_Veteran_MaleNRM ?? new Texture2D[6];
            visor_Iva_Badass_Female = visor_Iva_Badass_Female ?? new Texture2D[6];
            visor_Iva_Badass_FemaleNRM = visor_Iva_Badass_FemaleNRM ?? new Texture2D[6];
            visor_Iva_Badass_Male = visor_Iva_Badass_Male ?? new Texture2D[6];
            visor_Iva_Badass_MaleNRM = visor_Iva_Badass_MaleNRM ?? new Texture2D[6];
            visor_Iva_Standard_Female = visor_Iva_Standard_Female ?? new Texture2D[6];
            visor_Iva_Standard_FemaleNRM = visor_Iva_Standard_FemaleNRM ?? new Texture2D[6];
            visor_Iva_Standard_Male = visor_Iva_Standard_Male ?? new Texture2D[6];
            visor_Iva_Standard_MaleNRM = visor_Iva_Standard_MaleNRM ?? new Texture2D[6];
            visor_Iva_VetBad_Female = visor_Iva_VetBad_Female ?? new Texture2D[6];
            visor_Iva_VetBad_FemaleNRM = visor_Iva_VetBad_FemaleNRM ?? new Texture2D[6];
            visor_Iva_VetBad_Male = visor_Iva_VetBad_Male ?? new Texture2D[6];
            visor_Iva_VetBad_MaleNRM = visor_Iva_VetBad_MaleNRM ?? new Texture2D[6];
            visor_Iva_Veteran_Female = visor_Iva_Veteran_Female ?? new Texture2D[6];
            visor_Iva_Veteran_FemaleNRM = visor_Iva_Veteran_FemaleNRM ?? new Texture2D[6];
            visor_Iva_Veteran_Male = visor_Iva_Veteran_Male ?? new Texture2D[6];
            visor_Iva_Veteran_MaleNRM = visor_Iva_Veteran_MaleNRM ?? new Texture2D[6];

            switch (originalName)
            {
                case "Helmet_EvaGround_Badass_Female0":
                case "Helmet_EvaGround_Badass_Female1":
                case "Helmet_EvaGround_Badass_Female2":
                case "Helmet_EvaGround_Badass_Female3":
                case "Helmet_EvaGround_Badass_Female4":
                case "Helmet_EvaGround_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Badass_Female[i] = texture;
                    return true;

                case "Helmet_EvaGround_Badass_FemaleNRM0":
                case "Helmet_EvaGround_Badass_FemaleNRM1":
                case "Helmet_EvaGround_Badass_FemaleNRM2":
                case "Helmet_EvaGround_Badass_FemaleNRM3":
                case "Helmet_EvaGround_Badass_FemaleNRM4":
                case "Helmet_EvaGround_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaGround_Badass_Male0":
                case "Helmet_EvaGround_Badass_Male1":
                case "Helmet_EvaGround_Badass_Male2":
                case "Helmet_EvaGround_Badass_Male3":
                case "Helmet_EvaGround_Badass_Male4":
                case "Helmet_EvaGround_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Badass_Male[i] = texture;
                    return true;

                case "Helmet_EvaGround_Badass_MaleNRM0":
                case "Helmet_EvaGround_Badass_MaleNRM1":
                case "Helmet_EvaGround_Badass_MaleNRM2":
                case "Helmet_EvaGround_Badass_MaleNRM3":
                case "Helmet_EvaGround_Badass_MaleNRM4":
                case "Helmet_EvaGround_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Badass_MaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaGround_Standard_Female0":
                case "Helmet_EvaGround_Standard_Female1":
                case "Helmet_EvaGround_Standard_Female2":
                case "Helmet_EvaGround_Standard_Female3":
                case "Helmet_EvaGround_Standard_Female4":
                case "Helmet_EvaGround_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Standard_Female[i] = texture;
                    return true;

                case "Helmet_EvaGround_Standard_FemaleNRM0":
                case "Helmet_EvaGround_Standard_FemaleNRM1":
                case "Helmet_EvaGround_Standard_FemaleNRM2":
                case "Helmet_EvaGround_Standard_FemaleNRM3":
                case "Helmet_EvaGround_Standard_FemaleNRM4":
                case "Helmet_EvaGround_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaGround_Standard_Male0":
                case "Helmet_EvaGround_Standard_Male1":
                case "Helmet_EvaGround_Standard_Male2":
                case "Helmet_EvaGround_Standard_Male3":
                case "Helmet_EvaGround_Standard_Male4":
                case "Helmet_EvaGround_Standard_Male5":
                case "EVAgroundHelmet":
                case "EVAgroundHelmet0":
                case "EVAgroundHelmet1":
                case "EVAgroundHelmet2":
                case "EVAgroundHelmet3":
                case "EVAgroundHelmet4":
                case "EVAgroundHelmet5":                
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Standard_Male[i] = texture;
                    return true;

                case "Helmet_EvaGround_Standard_MaleNRM0":
                case "Helmet_EvaGround_Standard_MaleNRM1":
                case "Helmet_EvaGround_Standard_MaleNRM2":
                case "Helmet_EvaGround_Standard_MaleNRM3":
                case "Helmet_EvaGround_Standard_MaleNRM4":
                case "Helmet_EvaGround_Standard_MaleNRM5":
                case "EVAgroundHelmetNRM":
                case "EVAgroundHelmetNRM0":
                case "EVAgroundHelmetNRM1":
                case "EVAgroundHelmetNRM2":
                case "EVAgroundHelmetNRM3":
                case "EVAgroundHelmetNRM4":
                case "EVAgroundHelmetNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Standard_MaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaGround_VetBad_Female0":
                case "Helmet_EvaGround_VetBad_Female1":
                case "Helmet_EvaGround_VetBad_Female2":
                case "Helmet_EvaGround_VetBad_Female3":
                case "Helmet_EvaGround_VetBad_Female4":
                case "Helmet_EvaGround_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_VetBad_Female[i] = texture;
                    return true;

                case "Helmet_EvaGround_VetBad_FemaleNRM0":
                case "Helmet_EvaGround_VetBad_FemaleNRM1":
                case "Helmet_EvaGround_VetBad_FemaleNRM2":
                case "Helmet_EvaGround_VetBad_FemaleNRM3":
                case "Helmet_EvaGround_VetBad_FemaleNRM4":
                case "Helmet_EvaGround_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaGround_VetBad_Male0":
                case "Helmet_EvaGround_VetBad_Male1":
                case "Helmet_EvaGround_VetBad_Male2":
                case "Helmet_EvaGround_VetBad_Male3":
                case "Helmet_EvaGround_VetBad_Male4":
                case "Helmet_EvaGround_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_VetBad_Male[i] = texture;
                    return true;

                case "Helmet_EvaGround_VetBad_MaleNRM0":
                case "Helmet_EvaGround_VetBad_MaleNRM1":
                case "Helmet_EvaGround_VetBad_MaleNRM2":
                case "Helmet_EvaGround_VetBad_MaleNRM3":
                case "Helmet_EvaGround_VetBad_MaleNRM4":
                case "Helmet_EvaGround_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaGround_Veteran_Female0":
                case "Helmet_EvaGround_Veteran_Female1":
                case "Helmet_EvaGround_Veteran_Female2":
                case "Helmet_EvaGround_Veteran_Female3":
                case "Helmet_EvaGround_Veteran_Female4":
                case "Helmet_EvaGround_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Veteran_Female[i] = texture;
                    return true;

                case "Helmet_EvaGround_Veteran_FemaleNRM0":
                case "Helmet_EvaGround_Veteran_FemaleNRM1":
                case "Helmet_EvaGround_Veteran_FemaleNRM2":
                case "Helmet_EvaGround_Veteran_FemaleNRM3":
                case "Helmet_EvaGround_Veteran_FemaleNRM4":
                case "Helmet_EvaGround_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaGround_Veteran_Male0":
                case "Helmet_EvaGround_Veteran_Male1":
                case "Helmet_EvaGround_Veteran_Male2":
                case "Helmet_EvaGround_Veteran_Male3":
                case "Helmet_EvaGround_Veteran_Male4":
                case "Helmet_EvaGround_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Veteran_Male[i] = texture;
                    return true;

                case "Helmet_EvaGround_Veteran_MaleNRM0":
                case "Helmet_EvaGround_Veteran_MaleNRM1":
                case "Helmet_EvaGround_Veteran_MaleNRM2":
                case "Helmet_EvaGround_Veteran_MaleNRM3":
                case "Helmet_EvaGround_Veteran_MaleNRM4":
                case "Helmet_EvaGround_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaGround_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Badass_Female0":
                case "Helmet_EvaSpace_Badass_Female1":
                case "Helmet_EvaSpace_Badass_Female2":
                case "Helmet_EvaSpace_Badass_Female3":
                case "Helmet_EvaSpace_Badass_Female4":
                case "Helmet_EvaSpace_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Badass_Female[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Badass_FemaleNRM0":
                case "Helmet_EvaSpace_Badass_FemaleNRM1":
                case "Helmet_EvaSpace_Badass_FemaleNRM2":
                case "Helmet_EvaSpace_Badass_FemaleNRM3":
                case "Helmet_EvaSpace_Badass_FemaleNRM4":
                case "Helmet_EvaSpace_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Badass_Male0":
                case "Helmet_EvaSpace_Badass_Male1":
                case "Helmet_EvaSpace_Badass_Male2":
                case "Helmet_EvaSpace_Badass_Male3":
                case "Helmet_EvaSpace_Badass_Male4":
                case "Helmet_EvaSpace_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Badass_Male[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Badass_MaleNRM0":
                case "Helmet_EvaSpace_Badass_MaleNRM1":
                case "Helmet_EvaSpace_Badass_MaleNRM2":
                case "Helmet_EvaSpace_Badass_MaleNRM3":
                case "Helmet_EvaSpace_Badass_MaleNRM4":
                case "Helmet_EvaSpace_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Badass_MaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Standard_Female0":
                case "Helmet_EvaSpace_Standard_Female1":
                case "Helmet_EvaSpace_Standard_Female2":
                case "Helmet_EvaSpace_Standard_Female3":
                case "Helmet_EvaSpace_Standard_Female4":
                case "Helmet_EvaSpace_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Standard_Female[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Standard_FemaleNRM0":
                case "Helmet_EvaSpace_Standard_FemaleNRM1":
                case "Helmet_EvaSpace_Standard_FemaleNRM2":
                case "Helmet_EvaSpace_Standard_FemaleNRM3":
                case "Helmet_EvaSpace_Standard_FemaleNRM4":
                case "Helmet_EvaSpace_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Standard_Male0":
                case "Helmet_EvaSpace_Standard_Male1":
                case "Helmet_EvaSpace_Standard_Male2":
                case "Helmet_EvaSpace_Standard_Male3":
                case "Helmet_EvaSpace_Standard_Male4":
                case "Helmet_EvaSpace_Standard_Male5":
                case "EVAhelmet":
                case "EVAhelmet0":
                case "EVAhelmet1":
                case "EVAhelmet2":
                case "EVAhelmet3":
                case "EVAhelmet4":
                case "EVAhelmet5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Standard_Male[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Standard_MaleNRM0":
                case "Helmet_EvaSpace_Standard_MaleNRM1":
                case "Helmet_EvaSpace_Standard_MaleNRM2":
                case "Helmet_EvaSpace_Standard_MaleNRM3":
                case "Helmet_EvaSpace_Standard_MaleNRM4":
                case "Helmet_EvaSpace_Standard_MaleNRM5":
                case "EVAhelmetNRM":
                case "EVAhelmetNRM0":
                case "EVAhelmetNRM1":
                case "EVAhelmetNRM2":
                case "EVAhelmetNRM3":
                case "EVAhelmetNRM4":
                case "EVAhelmetNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Standard_MaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaSpace_VetBad_Female0":
                case "Helmet_EvaSpace_VetBad_Female1":
                case "Helmet_EvaSpace_VetBad_Female2":
                case "Helmet_EvaSpace_VetBad_Female3":
                case "Helmet_EvaSpace_VetBad_Female4":
                case "Helmet_EvaSpace_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_VetBad_Female[i] = texture;
                    return true;

                case "Helmet_EvaSpace_VetBad_FemaleNRM0":
                case "Helmet_EvaSpace_VetBad_FemaleNRM1":
                case "Helmet_EvaSpace_VetBad_FemaleNRM2":
                case "Helmet_EvaSpace_VetBad_FemaleNRM3":
                case "Helmet_EvaSpace_VetBad_FemaleNRM4":
                case "Helmet_EvaSpace_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaSpace_VetBad_Male0":
                case "Helmet_EvaSpace_VetBad_Male1":
                case "Helmet_EvaSpace_VetBad_Male2":
                case "Helmet_EvaSpace_VetBad_Male3":
                case "Helmet_EvaSpace_VetBad_Male4":
                case "Helmet_EvaSpace_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_VetBad_Male[i] = texture;
                    return true;

                case "Helmet_EvaSpace_VetBad_MaleNRM0":
                case "Helmet_EvaSpace_VetBad_MaleNRM1":
                case "Helmet_EvaSpace_VetBad_MaleNRM2":
                case "Helmet_EvaSpace_VetBad_MaleNRM3":
                case "Helmet_EvaSpace_VetBad_MaleNRM4":
                case "Helmet_EvaSpace_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Veteran_Female0":
                case "Helmet_EvaSpace_Veteran_Female1":
                case "Helmet_EvaSpace_Veteran_Female2":
                case "Helmet_EvaSpace_Veteran_Female3":
                case "Helmet_EvaSpace_Veteran_Female4":
                case "Helmet_EvaSpace_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Veteran_Female[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Veteran_FemaleNRM0":
                case "Helmet_EvaSpace_Veteran_FemaleNRM1":
                case "Helmet_EvaSpace_Veteran_FemaleNRM2":
                case "Helmet_EvaSpace_Veteran_FemaleNRM3":
                case "Helmet_EvaSpace_Veteran_FemaleNRM4":
                case "Helmet_EvaSpace_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Veteran_Male0":
                case "Helmet_EvaSpace_Veteran_Male1":
                case "Helmet_EvaSpace_Veteran_Male2":
                case "Helmet_EvaSpace_Veteran_Male3":
                case "Helmet_EvaSpace_Veteran_Male4":
                case "Helmet_EvaSpace_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Veteran_Male[i] = texture;
                    return true;

                case "Helmet_EvaSpace_Veteran_MaleNRM0":
                case "Helmet_EvaSpace_Veteran_MaleNRM1":
                case "Helmet_EvaSpace_Veteran_MaleNRM2":
                case "Helmet_EvaSpace_Veteran_MaleNRM3":
                case "Helmet_EvaSpace_Veteran_MaleNRM4":
                case "Helmet_EvaSpace_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_EvaSpace_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Helmet_Iva_Badass_Female0":
                case "Helmet_Iva_Badass_Female1":
                case "Helmet_Iva_Badass_Female2":
                case "Helmet_Iva_Badass_Female3":
                case "Helmet_Iva_Badass_Female4":
                case "Helmet_Iva_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Badass_Female[i] = texture;
                    return true;

                case "Helmet_Iva_Badass_FemaleNRM0":
                case "Helmet_Iva_Badass_FemaleNRM1":
                case "Helmet_Iva_Badass_FemaleNRM2":
                case "Helmet_Iva_Badass_FemaleNRM3":
                case "Helmet_Iva_Badass_FemaleNRM4":
                case "Helmet_Iva_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_Iva_Badass_Male0":
                case "Helmet_Iva_Badass_Male1":
                case "Helmet_Iva_Badass_Male2":
                case "Helmet_Iva_Badass_Male3":
                case "Helmet_Iva_Badass_Male4":
                case "Helmet_Iva_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Badass_Male[i] = texture;
                    return true;

                case "Helmet_Iva_Badass_MaleNRM0":
                case "Helmet_Iva_Badass_MaleNRM1":
                case "Helmet_Iva_Badass_MaleNRM2":
                case "Helmet_Iva_Badass_MaleNRM3":
                case "Helmet_Iva_Badass_MaleNRM4":
                case "Helmet_Iva_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Badass_MaleNRM[i] = texture;
                    return true;

                case "Helmet_Iva_Standard_Female0":
                case "Helmet_Iva_Standard_Female1":
                case "Helmet_Iva_Standard_Female2":
                case "Helmet_Iva_Standard_Female3":
                case "Helmet_Iva_Standard_Female4":
                case "Helmet_Iva_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Standard_Female[i] = texture;
                    return true;

                case "Helmet_Iva_Standard_FemaleNRM0":
                case "Helmet_Iva_Standard_FemaleNRM1":
                case "Helmet_Iva_Standard_FemaleNRM2":
                case "Helmet_Iva_Standard_FemaleNRM3":
                case "Helmet_Iva_Standard_FemaleNRM4":
                case "Helmet_Iva_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_Iva_Standard_Male0":
                case "Helmet_Iva_Standard_Male1":
                case "Helmet_Iva_Standard_Male2":
                case "Helmet_Iva_Standard_Male3":
                case "Helmet_Iva_Standard_Male4":
                case "Helmet_Iva_Standard_Male5":
                case "kerbalHelmetGrey":
                case "kerbalHelmetGrey0":
                case "kerbalHelmetGrey1":
                case "kerbalHelmetGrey2":
                case "kerbalHelmetGrey3":
                case "kerbalHelmetGrey4":
                case "kerbalHelmetGrey5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Standard_Male[i] = texture;
                    return true;

                case "Helmet_Iva_Standard_MaleNRM0":
                case "Helmet_Iva_Standard_MaleNRM1":
                case "Helmet_Iva_Standard_MaleNRM2":
                case "Helmet_Iva_Standard_MaleNRM3":
                case "Helmet_Iva_Standard_MaleNRM4":
                case "Helmet_Iva_Standard_MaleNRM5":
                case "kerbalHelmetGreyNRM":
                case "kerbalHelmetGreyNRM0":
                case "kerbalHelmetGreyNRM1":
                case "kerbalHelmetGreyNRM2":
                case "kerbalHelmetGreyNRM3":
                case "kerbalHelmetGreyNRM4":
                case "kerbalHelmetGreyNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Standard_MaleNRM[i] = texture;
                    return true;

                case "Helmet_Iva_VetBad_Female0":
                case "Helmet_Iva_VetBad_Female1":
                case "Helmet_Iva_VetBad_Female2":
                case "Helmet_Iva_VetBad_Female3":
                case "Helmet_Iva_VetBad_Female4":
                case "Helmet_Iva_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_VetBad_Female[i] = texture;
                    return true;

                case "Helmet_Iva_VetBad_FemaleNRM0":
                case "Helmet_Iva_VetBad_FemaleNRM1":
                case "Helmet_Iva_VetBad_FemaleNRM2":
                case "Helmet_Iva_VetBad_FemaleNRM3":
                case "Helmet_Iva_VetBad_FemaleNRM4":
                case "Helmet_Iva_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_Iva_VetBad_Male0":
                case "Helmet_Iva_VetBad_Male1":
                case "Helmet_Iva_VetBad_Male2":
                case "Helmet_Iva_VetBad_Male3":
                case "Helmet_Iva_VetBad_Male4":
                case "Helmet_Iva_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_VetBad_Male[i] = texture;
                    return true;

                case "Helmet_Iva_VetBad_MaleNRM0":
                case "Helmet_Iva_VetBad_MaleNRM1":
                case "Helmet_Iva_VetBad_MaleNRM2":
                case "Helmet_Iva_VetBad_MaleNRM3":
                case "Helmet_Iva_VetBad_MaleNRM4":
                case "Helmet_Iva_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Helmet_Iva_Veteran_Female0":
                case "Helmet_Iva_Veteran_Female1":
                case "Helmet_Iva_Veteran_Female2":
                case "Helmet_Iva_Veteran_Female3":
                case "Helmet_Iva_Veteran_Female4":
                case "Helmet_Iva_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Veteran_Female[i] = texture;
                    return true;

                case "Helmet_Iva_Veteran_FemaleNRM0":
                case "Helmet_Iva_Veteran_FemaleNRM1":
                case "Helmet_Iva_Veteran_FemaleNRM2":
                case "Helmet_Iva_Veteran_FemaleNRM3":
                case "Helmet_Iva_Veteran_FemaleNRM4":
                case "Helmet_Iva_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Helmet_Iva_Veteran_Male0":
                case "Helmet_Iva_Veteran_Male1":
                case "Helmet_Iva_Veteran_Male2":
                case "Helmet_Iva_Veteran_Male3":
                case "Helmet_Iva_Veteran_Male4":
                case "Helmet_Iva_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Veteran_Male[i] = texture;
                    return true;

                case "Helmet_Iva_Veteran_MaleNRM0":
                case "Helmet_Iva_Veteran_MaleNRM1":
                case "Helmet_Iva_Veteran_MaleNRM2":
                case "Helmet_Iva_Veteran_MaleNRM3":
                case "Helmet_Iva_Veteran_MaleNRM4":
                case "Helmet_Iva_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        helmet_Iva_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Badass_Female0":
                case "Jetpack_EvaGround_Badass_Female1":
                case "Jetpack_EvaGround_Badass_Female2":
                case "Jetpack_EvaGround_Badass_Female3":
                case "Jetpack_EvaGround_Badass_Female4":
                case "Jetpack_EvaGround_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Badass_Female[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Badass_FemaleNRM0":
                case "Jetpack_EvaGround_Badass_FemaleNRM1":
                case "Jetpack_EvaGround_Badass_FemaleNRM2":
                case "Jetpack_EvaGround_Badass_FemaleNRM3":
                case "Jetpack_EvaGround_Badass_FemaleNRM4":
                case "Jetpack_EvaGround_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Badass_Male0":
                case "Jetpack_EvaGround_Badass_Male1":
                case "Jetpack_EvaGround_Badass_Male2":
                case "Jetpack_EvaGround_Badass_Male3":
                case "Jetpack_EvaGround_Badass_Male4":
                case "Jetpack_EvaGround_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Badass_Male[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Badass_MaleNRM0":
                case "Jetpack_EvaGround_Badass_MaleNRM1":
                case "Jetpack_EvaGround_Badass_MaleNRM2":
                case "Jetpack_EvaGround_Badass_MaleNRM3":
                case "Jetpack_EvaGround_Badass_MaleNRM4":
                case "Jetpack_EvaGround_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Badass_MaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Standard_Female0":
                case "Jetpack_EvaGround_Standard_Female1":
                case "Jetpack_EvaGround_Standard_Female2":
                case "Jetpack_EvaGround_Standard_Female3":
                case "Jetpack_EvaGround_Standard_Female4":
                case "Jetpack_EvaGround_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Standard_Female[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Standard_FemaleNRM0":
                case "Jetpack_EvaGround_Standard_FemaleNRM1":
                case "Jetpack_EvaGround_Standard_FemaleNRM2":
                case "Jetpack_EvaGround_Standard_FemaleNRM3":
                case "Jetpack_EvaGround_Standard_FemaleNRM4":
                case "Jetpack_EvaGround_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Standard_Male0":
                case "Jetpack_EvaGround_Standard_Male1":
                case "Jetpack_EvaGround_Standard_Male2":
                case "Jetpack_EvaGround_Standard_Male3":
                case "Jetpack_EvaGround_Standard_Male4":
                case "Jetpack_EvaGround_Standard_Male5":
                case "EVAgroundJetpack":
                case "EVAgroundJetpack0":
                case "EVAgroundJetpack1":
                case "EVAgroundJetpack2":
                case "EVAgroundJetpack3":
                case "EVAgroundJetpack4":
                case "EVAgroundJetpack5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Standard_Male[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Standard_MaleNRM0":
                case "Jetpack_EvaGround_Standard_MaleNRM1":
                case "Jetpack_EvaGround_Standard_MaleNRM2":
                case "Jetpack_EvaGround_Standard_MaleNRM3":
                case "Jetpack_EvaGround_Standard_MaleNRM4":
                case "Jetpack_EvaGround_Standard_MaleNRM5":
                case "EVAgroundJetpackNRM":
                case "EVAgroundJetpackNRM0":
                case "EVAgroundJetpackNRM1":
                case "EVAgroundJetpackNRM2":
                case "EVAgroundJetpackNRM3":
                case "EVAgroundJetpackNRM4":
                case "EVAgroundJetpackNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Standard_MaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaGround_VetBad_Female0":
                case "Jetpack_EvaGround_VetBad_Female1":
                case "Jetpack_EvaGround_VetBad_Female2":
                case "Jetpack_EvaGround_VetBad_Female3":
                case "Jetpack_EvaGround_VetBad_Female4":
                case "Jetpack_EvaGround_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_VetBad_Female[i] = texture;
                    return true;

                case "Jetpack_EvaGround_VetBad_FemaleNRM0":
                case "Jetpack_EvaGround_VetBad_FemaleNRM1":
                case "Jetpack_EvaGround_VetBad_FemaleNRM2":
                case "Jetpack_EvaGround_VetBad_FemaleNRM3":
                case "Jetpack_EvaGround_VetBad_FemaleNRM4":
                case "Jetpack_EvaGround_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaGround_VetBad_Male0":
                case "Jetpack_EvaGround_VetBad_Male1":
                case "Jetpack_EvaGround_VetBad_Male2":
                case "Jetpack_EvaGround_VetBad_Male3":
                case "Jetpack_EvaGround_VetBad_Male4":
                case "Jetpack_EvaGround_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_VetBad_Male[i] = texture;
                    return true;

                case "Jetpack_EvaGround_VetBad_MaleNRM0":
                case "Jetpack_EvaGround_VetBad_MaleNRM1":
                case "Jetpack_EvaGround_VetBad_MaleNRM2":
                case "Jetpack_EvaGround_VetBad_MaleNRM3":
                case "Jetpack_EvaGround_VetBad_MaleNRM4":
                case "Jetpack_EvaGround_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Veteran_Female0":
                case "Jetpack_EvaGround_Veteran_Female1":
                case "Jetpack_EvaGround_Veteran_Female2":
                case "Jetpack_EvaGround_Veteran_Female3":
                case "Jetpack_EvaGround_Veteran_Female4":
                case "Jetpack_EvaGround_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Veteran_Female[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Veteran_FemaleNRM0":
                case "Jetpack_EvaGround_Veteran_FemaleNRM1":
                case "Jetpack_EvaGround_Veteran_FemaleNRM2":
                case "Jetpack_EvaGround_Veteran_FemaleNRM3":
                case "Jetpack_EvaGround_Veteran_FemaleNRM4":
                case "Jetpack_EvaGround_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Veteran_Male0":
                case "Jetpack_EvaGround_Veteran_Male1":
                case "Jetpack_EvaGround_Veteran_Male2":
                case "Jetpack_EvaGround_Veteran_Male3":
                case "Jetpack_EvaGround_Veteran_Male4":
                case "Jetpack_EvaGround_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Veteran_Male[i] = texture;
                    return true;

                case "Jetpack_EvaGround_Veteran_MaleNRM0":
                case "Jetpack_EvaGround_Veteran_MaleNRM1":
                case "Jetpack_EvaGround_Veteran_MaleNRM2":
                case "Jetpack_EvaGround_Veteran_MaleNRM3":
                case "Jetpack_EvaGround_Veteran_MaleNRM4":
                case "Jetpack_EvaGround_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaGround_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Badass_Female0":
                case "Jetpack_EvaSpace_Badass_Female1":
                case "Jetpack_EvaSpace_Badass_Female2":
                case "Jetpack_EvaSpace_Badass_Female3":
                case "Jetpack_EvaSpace_Badass_Female4":
                case "Jetpack_EvaSpace_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Badass_Female[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Badass_FemaleNRM0":
                case "Jetpack_EvaSpace_Badass_FemaleNRM1":
                case "Jetpack_EvaSpace_Badass_FemaleNRM2":
                case "Jetpack_EvaSpace_Badass_FemaleNRM3":
                case "Jetpack_EvaSpace_Badass_FemaleNRM4":
                case "Jetpack_EvaSpace_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Badass_Male0":
                case "Jetpack_EvaSpace_Badass_Male1":
                case "Jetpack_EvaSpace_Badass_Male2":
                case "Jetpack_EvaSpace_Badass_Male3":
                case "Jetpack_EvaSpace_Badass_Male4":
                case "Jetpack_EvaSpace_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Badass_Male[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Badass_MaleNRM0":
                case "Jetpack_EvaSpace_Badass_MaleNRM1":
                case "Jetpack_EvaSpace_Badass_MaleNRM2":
                case "Jetpack_EvaSpace_Badass_MaleNRM3":
                case "Jetpack_EvaSpace_Badass_MaleNRM4":
                case "Jetpack_EvaSpace_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Badass_MaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Standard_Female0":
                case "Jetpack_EvaSpace_Standard_Female1":
                case "Jetpack_EvaSpace_Standard_Female2":
                case "Jetpack_EvaSpace_Standard_Female3":
                case "Jetpack_EvaSpace_Standard_Female4":
                case "Jetpack_EvaSpace_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Standard_Female[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Standard_FemaleNRM0":
                case "Jetpack_EvaSpace_Standard_FemaleNRM1":
                case "Jetpack_EvaSpace_Standard_FemaleNRM2":
                case "Jetpack_EvaSpace_Standard_FemaleNRM3":
                case "Jetpack_EvaSpace_Standard_FemaleNRM4":
                case "Jetpack_EvaSpace_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Standard_Male0":
                case "Jetpack_EvaSpace_Standard_Male1":
                case "Jetpack_EvaSpace_Standard_Male2":
                case "Jetpack_EvaSpace_Standard_Male3":
                case "Jetpack_EvaSpace_Standard_Male4":
                case "Jetpack_EvaSpace_Standard_Male5":
                case "EVAjetpack":
                case "EVAjetpack0":
                case "EVAjetpack1":
                case "EVAjetpack2":
                case "EVAjetpack3":
                case "EVAjetpack4":
                case "EVAjetpack5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Standard_Male[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Standard_MaleNRM0":
                case "Jetpack_EvaSpace_Standard_MaleNRM1":
                case "Jetpack_EvaSpace_Standard_MaleNRM2":
                case "Jetpack_EvaSpace_Standard_MaleNRM3":
                case "Jetpack_EvaSpace_Standard_MaleNRM4":
                case "Jetpack_EvaSpace_Standard_MaleNRM5":
                case "EVAjetpackNRM":
                case "EVAjetpackNRM0":
                case "EVAjetpackNRM1":
                case "EVAjetpackNRM2":
                case "EVAjetpackNRM3":
                case "EVAjetpackNRM4":
                case "EVAjetpackNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Standard_MaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_VetBad_Female0":
                case "Jetpack_EvaSpace_VetBad_Female1":
                case "Jetpack_EvaSpace_VetBad_Female2":
                case "Jetpack_EvaSpace_VetBad_Female3":
                case "Jetpack_EvaSpace_VetBad_Female4":
                case "Jetpack_EvaSpace_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_VetBad_Female[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_VetBad_FemaleNRM0":
                case "Jetpack_EvaSpace_VetBad_FemaleNRM1":
                case "Jetpack_EvaSpace_VetBad_FemaleNRM2":
                case "Jetpack_EvaSpace_VetBad_FemaleNRM3":
                case "Jetpack_EvaSpace_VetBad_FemaleNRM4":
                case "Jetpack_EvaSpace_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_VetBad_Male0":
                case "Jetpack_EvaSpace_VetBad_Male1":
                case "Jetpack_EvaSpace_VetBad_Male2":
                case "Jetpack_EvaSpace_VetBad_Male3":
                case "Jetpack_EvaSpace_VetBad_Male4":
                case "Jetpack_EvaSpace_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_VetBad_Male[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_VetBad_MaleNRM0":
                case "Jetpack_EvaSpace_VetBad_MaleNRM1":
                case "Jetpack_EvaSpace_VetBad_MaleNRM2":
                case "Jetpack_EvaSpace_VetBad_MaleNRM3":
                case "Jetpack_EvaSpace_VetBad_MaleNRM4":
                case "Jetpack_EvaSpace_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Veteran_Female0":
                case "Jetpack_EvaSpace_Veteran_Female1":
                case "Jetpack_EvaSpace_Veteran_Female2":
                case "Jetpack_EvaSpace_Veteran_Female3":
                case "Jetpack_EvaSpace_Veteran_Female4":
                case "Jetpack_EvaSpace_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Veteran_Female[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Veteran_FemaleNRM0":
                case "Jetpack_EvaSpace_Veteran_FemaleNRM1":
                case "Jetpack_EvaSpace_Veteran_FemaleNRM2":
                case "Jetpack_EvaSpace_Veteran_FemaleNRM3":
                case "Jetpack_EvaSpace_Veteran_FemaleNRM4":
                case "Jetpack_EvaSpace_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Veteran_Male0":
                case "Jetpack_EvaSpace_Veteran_Male1":
                case "Jetpack_EvaSpace_Veteran_Male2":
                case "Jetpack_EvaSpace_Veteran_Male3":
                case "Jetpack_EvaSpace_Veteran_Male4":
                case "Jetpack_EvaSpace_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Veteran_Male[i] = texture;
                    return true;

                case "Jetpack_EvaSpace_Veteran_MaleNRM0":
                case "Jetpack_EvaSpace_Veteran_MaleNRM1":
                case "Jetpack_EvaSpace_Veteran_MaleNRM2":
                case "Jetpack_EvaSpace_Veteran_MaleNRM3":
                case "Jetpack_EvaSpace_Veteran_MaleNRM4":
                case "Jetpack_EvaSpace_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        jetpack_EvaSpace_Veteran_MaleNRM[i] = texture;
                    return true;                

                case "Suit_EvaGround_Badass_Female0":
                case "Suit_EvaGround_Badass_Female1":
                case "Suit_EvaGround_Badass_Female2":
                case "Suit_EvaGround_Badass_Female3":
                case "Suit_EvaGround_Badass_Female4":
                case "Suit_EvaGround_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Badass_Female[i] = texture;
                    return true;

                case "Suit_EvaGround_Badass_FemaleNRM0":
                case "Suit_EvaGround_Badass_FemaleNRM1":
                case "Suit_EvaGround_Badass_FemaleNRM2":
                case "Suit_EvaGround_Badass_FemaleNRM3":
                case "Suit_EvaGround_Badass_FemaleNRM4":
                case "Suit_EvaGround_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Suit_EvaGround_Badass_Male0":
                case "Suit_EvaGround_Badass_Male1":
                case "Suit_EvaGround_Badass_Male2":
                case "Suit_EvaGround_Badass_Male3":
                case "Suit_EvaGround_Badass_Male4":
                case "Suit_EvaGround_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Badass_Male[i] = texture;
                    return true;

                case "Suit_EvaGround_Badass_MaleNRM0":
                case "Suit_EvaGround_Badass_MaleNRM1":
                case "Suit_EvaGround_Badass_MaleNRM2":
                case "Suit_EvaGround_Badass_MaleNRM3":
                case "Suit_EvaGround_Badass_MaleNRM4":
                case "Suit_EvaGround_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Badass_MaleNRM[i] = texture;
                    return true;

                case "Suit_EvaGround_Standard_Female0":
                case "Suit_EvaGround_Standard_Female1":
                case "Suit_EvaGround_Standard_Female2":
                case "Suit_EvaGround_Standard_Female3":
                case "Suit_EvaGround_Standard_Female4":
                case "Suit_EvaGround_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Standard_Female[i] = texture;
                    return true;

                case "Suit_EvaGround_Standard_FemaleNRM0":
                case "Suit_EvaGround_Standard_FemaleNRM1":
                case "Suit_EvaGround_Standard_FemaleNRM2":
                case "Suit_EvaGround_Standard_FemaleNRM3":
                case "Suit_EvaGround_Standard_FemaleNRM4":
                case "Suit_EvaGround_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Suit_EvaGround_Standard_Male0":
                case "Suit_EvaGround_Standard_Male1":
                case "Suit_EvaGround_Standard_Male2":
                case "Suit_EvaGround_Standard_Male3":
                case "Suit_EvaGround_Standard_Male4":
                case "Suit_EvaGround_Standard_Male5":
                case "EVAgroundTexture":
                case "EVAgroundTexture0":
                case "EVAgroundTexture1":
                case "EVAgroundTexture2":
                case "EVAgroundTexture3":
                case "EVAgroundTexture4":
                case "EVAgroundTexture5":

                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Standard_Male[i] = texture;
                    return true;

                case "Suit_EvaGround_Standard_MaleNRM0":
                case "Suit_EvaGround_Standard_MaleNRM1":
                case "Suit_EvaGround_Standard_MaleNRM2":
                case "Suit_EvaGround_Standard_MaleNRM3":
                case "Suit_EvaGround_Standard_MaleNRM4":
                case "Suit_EvaGround_Standard_MaleNRM5":
                case "EVAgroundTextureNRM":
                case "EVAgroundTextureNRM0":
                case "EVAgroundTextureNRM1":
                case "EVAgroundTextureNRM2":
                case "EVAgroundTextureNRM3":
                case "EVAgroundTextureNRM4":
                case "EVAgroundTextureNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Standard_MaleNRM[i] = texture;
                    return true;

                case "Suit_EvaGround_VetBad_Female0":
                case "Suit_EvaGround_VetBad_Female1":
                case "Suit_EvaGround_VetBad_Female2":
                case "Suit_EvaGround_VetBad_Female3":
                case "Suit_EvaGround_VetBad_Female4":
                case "Suit_EvaGround_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_VetBad_Female[i] = texture;
                    return true;

                case "Suit_EvaGround_VetBad_FemaleNRM0":
                case "Suit_EvaGround_VetBad_FemaleNRM1":
                case "Suit_EvaGround_VetBad_FemaleNRM2":
                case "Suit_EvaGround_VetBad_FemaleNRM3":
                case "Suit_EvaGround_VetBad_FemaleNRM4":
                case "Suit_EvaGround_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Suit_EvaGround_VetBad_Male0":
                case "Suit_EvaGround_VetBad_Male1":
                case "Suit_EvaGround_VetBad_Male2":
                case "Suit_EvaGround_VetBad_Male3":
                case "Suit_EvaGround_VetBad_Male4":
                case "Suit_EvaGround_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_VetBad_Male[i] = texture;
                    return true;

                case "Suit_EvaGround_VetBad_MaleNRM0":
                case "Suit_EvaGround_VetBad_MaleNRM1":
                case "Suit_EvaGround_VetBad_MaleNRM2":
                case "Suit_EvaGround_VetBad_MaleNRM3":
                case "Suit_EvaGround_VetBad_MaleNRM4":
                case "Suit_EvaGround_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Suit_EvaGround_Veteran_Female0":
                case "Suit_EvaGround_Veteran_Female1":
                case "Suit_EvaGround_Veteran_Female2":
                case "Suit_EvaGround_Veteran_Female3":
                case "Suit_EvaGround_Veteran_Female4":
                case "Suit_EvaGround_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Veteran_Female[i] = texture;
                    return true;

                case "Suit_EvaGround_Veteran_FemaleNRM0":
                case "Suit_EvaGround_Veteran_FemaleNRM1":
                case "Suit_EvaGround_Veteran_FemaleNRM2":
                case "Suit_EvaGround_Veteran_FemaleNRM3":
                case "Suit_EvaGround_Veteran_FemaleNRM4":
                case "Suit_EvaGround_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Suit_EvaGround_Veteran_Male0":
                case "Suit_EvaGround_Veteran_Male1":
                case "Suit_EvaGround_Veteran_Male2":
                case "Suit_EvaGround_Veteran_Male3":
                case "Suit_EvaGround_Veteran_Male4":
                case "Suit_EvaGround_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Veteran_Male[i] = texture;
                    return true;

                case "Suit_EvaGround_Veteran_MaleNRM0":
                case "Suit_EvaGround_Veteran_MaleNRM1":
                case "Suit_EvaGround_Veteran_MaleNRM2":
                case "Suit_EvaGround_Veteran_MaleNRM3":
                case "Suit_EvaGround_Veteran_MaleNRM4":
                case "Suit_EvaGround_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_EvaGround_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Suit_EvaSpace_Badass_Female0":
                case "Suit_EvaSpace_Badass_Female1":
                case "Suit_EvaSpace_Badass_Female2":
                case "Suit_EvaSpace_Badass_Female3":
                case "Suit_EvaSpace_Badass_Female4":
                case "Suit_EvaSpace_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Badass_Female[i] = texture;
                    return true;

                case "Suit_EvaSpace_Badass_FemaleNRM0":
                case "Suit_EvaSpace_Badass_FemaleNRM1":
                case "Suit_EvaSpace_Badass_FemaleNRM2":
                case "Suit_EvaSpace_Badass_FemaleNRM3":
                case "Suit_EvaSpace_Badass_FemaleNRM4":
                case "Suit_EvaSpace_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Suit_EvaSpace_Badass_Male0":
                case "Suit_EvaSpace_Badass_Male1":
                case "Suit_EvaSpace_Badass_Male2":
                case "Suit_EvaSpace_Badass_Male3":
                case "Suit_EvaSpace_Badass_Male4":
                case "Suit_EvaSpace_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Badass_Male[i] = texture;
                    return true;

                case "Suit_EvaSpace_Badass_MaleNRM0":
                case "Suit_EvaSpace_Badass_MaleNRM1":
                case "Suit_EvaSpace_Badass_MaleNRM2":
                case "Suit_EvaSpace_Badass_MaleNRM3":
                case "Suit_EvaSpace_Badass_MaleNRM4":
                case "Suit_EvaSpace_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Badass_MaleNRM[i] = texture;
                    return true;

                case "Suit_EvaSpace_Standard_Female0":
                case "Suit_EvaSpace_Standard_Female1":
                case "Suit_EvaSpace_Standard_Female2":
                case "Suit_EvaSpace_Standard_Female3":
                case "Suit_EvaSpace_Standard_Female4":
                case "Suit_EvaSpace_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Standard_Female[i] = texture;
                    return true;

                case "Suit_EvaSpace_Standard_FemaleNRM0":
                case "Suit_EvaSpace_Standard_FemaleNRM1":
                case "Suit_EvaSpace_Standard_FemaleNRM2":
                case "Suit_EvaSpace_Standard_FemaleNRM3":
                case "Suit_EvaSpace_Standard_FemaleNRM4":
                case "Suit_EvaSpace_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Suit_EvaSpace_Standard_Male0":
                case "Suit_EvaSpace_Standard_Male1":
                case "Suit_EvaSpace_Standard_Male2":
                case "Suit_EvaSpace_Standard_Male3":
                case "Suit_EvaSpace_Standard_Male4":
                case "Suit_EvaSpace_Standard_Male5":
                case "EVAtexture":
                case "EVAtexture0":
                case "EVAtexture1":
                case "EVAtexture2":
                case "EVAtexture3":
                case "EVAtexture4":
                case "EVAtexture5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Standard_Male[i] = texture;
                    return true;

                case "Suit_EvaSpace_Standard_MaleNRM0":
                case "Suit_EvaSpace_Standard_MaleNRM1":
                case "Suit_EvaSpace_Standard_MaleNRM2":
                case "Suit_EvaSpace_Standard_MaleNRM3":
                case "Suit_EvaSpace_Standard_MaleNRM4":
                case "Suit_EvaSpace_Standard_MaleNRM5":
                case "EVAtextureNRM":
                case "EVAtextureNRM0":
                case "EVAtextureNRM1":
                case "EVAtextureNRM2":
                case "EVAtextureNRM3":
                case "EVAtextureNRM4":
                case "EVAtextureNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Standard_MaleNRM[i] = texture;
                    return true;

                case "Suit_EvaSpace_VetBad_Female0":
                case "Suit_EvaSpace_VetBad_Female1":
                case "Suit_EvaSpace_VetBad_Female2":
                case "Suit_EvaSpace_VetBad_Female3":
                case "Suit_EvaSpace_VetBad_Female4":
                case "Suit_EvaSpace_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_VetBad_Female[i] = texture;
                    return true;

                case "Suit_EvaSpace_VetBad_FemaleNRM0":
                case "Suit_EvaSpace_VetBad_FemaleNRM1":
                case "Suit_EvaSpace_VetBad_FemaleNRM2":
                case "Suit_EvaSpace_VetBad_FemaleNRM3":
                case "Suit_EvaSpace_VetBad_FemaleNRM4":
                case "Suit_EvaSpace_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Suit_EvaSpace_VetBad_Male0":
                case "Suit_EvaSpace_VetBad_Male1":
                case "Suit_EvaSpace_VetBad_Male2":
                case "Suit_EvaSpace_VetBad_Male3":
                case "Suit_EvaSpace_VetBad_Male4":
                case "Suit_EvaSpace_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_VetBad_Male[i] = texture;
                    return true;

                case "Suit_EvaSpace_VetBad_MaleNRM0":
                case "Suit_EvaSpace_VetBad_MaleNRM1":
                case "Suit_EvaSpace_VetBad_MaleNRM2":
                case "Suit_EvaSpace_VetBad_MaleNRM3":
                case "Suit_EvaSpace_VetBad_MaleNRM4":
                case "Suit_EvaSpace_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Suit_EvaSpace_Veteran_Female0":
                case "Suit_EvaSpace_Veteran_Female1":
                case "Suit_EvaSpace_Veteran_Female2":
                case "Suit_EvaSpace_Veteran_Female3":
                case "Suit_EvaSpace_Veteran_Female4":
                case "Suit_EvaSpace_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Veteran_Female[i] = texture;
                    return true;

                case "Suit_EvaSpace_Veteran_FemaleNRM0":
                case "Suit_EvaSpace_Veteran_FemaleNRM1":
                case "Suit_EvaSpace_Veteran_FemaleNRM2":
                case "Suit_EvaSpace_Veteran_FemaleNRM3":
                case "Suit_EvaSpace_Veteran_FemaleNRM4":
                case "Suit_EvaSpace_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Suit_EvaSpace_Veteran_Male0":
                case "Suit_EvaSpace_Veteran_Male1":
                case "Suit_EvaSpace_Veteran_Male2":
                case "Suit_EvaSpace_Veteran_Male3":
                case "Suit_EvaSpace_Veteran_Male4":
                case "Suit_EvaSpace_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Veteran_Male[i] = texture;
                    return true;

                case "Suit_EvaSpace_Veteran_MaleNRM0":
                case "Suit_EvaSpace_Veteran_MaleNRM1":
                case "Suit_EvaSpace_Veteran_MaleNRM2":
                case "Suit_EvaSpace_Veteran_MaleNRM3":
                case "Suit_EvaSpace_Veteran_MaleNRM4":
                case "Suit_EvaSpace_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_EvaSpace_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Suit_Iva_Badass_Female0":
                case "Suit_Iva_Badass_Female1":
                case "Suit_Iva_Badass_Female2":
                case "Suit_Iva_Badass_Female3":
                case "Suit_Iva_Badass_Female4":
                case "Suit_Iva_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Badass_Female[i] = texture;
                    return true;

                case "Suit_Iva_Badass_FemaleNRM0":
                case "Suit_Iva_Badass_FemaleNRM1":
                case "Suit_Iva_Badass_FemaleNRM2":
                case "Suit_Iva_Badass_FemaleNRM3":
                case "Suit_Iva_Badass_FemaleNRM4":
                case "Suit_Iva_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Suit_Iva_Badass_Male0":
                case "Suit_Iva_Badass_Male1":
                case "Suit_Iva_Badass_Male2":
                case "Suit_Iva_Badass_Male3":
                case "Suit_Iva_Badass_Male4":
                case "Suit_Iva_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Badass_Male[i] = texture;
                    return true;

                case "Suit_Iva_Badass_MaleNRM0":
                case "Suit_Iva_Badass_MaleNRM1":
                case "Suit_Iva_Badass_MaleNRM2":
                case "Suit_Iva_Badass_MaleNRM3":
                case "Suit_Iva_Badass_MaleNRM4":
                case "Suit_Iva_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Badass_MaleNRM[i] = texture;
                    return true;

                case "Suit_Iva_Standard_Female0":
                case "Suit_Iva_Standard_Female1":
                case "Suit_Iva_Standard_Female2":
                case "Suit_Iva_Standard_Female3":
                case "Suit_Iva_Standard_Female4":
                case "Suit_Iva_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Standard_Female[i] = texture;
                    return true;

                case "Suit_Iva_Standard_FemaleNRM0":
                case "Suit_Iva_Standard_FemaleNRM1":
                case "Suit_Iva_Standard_FemaleNRM2":
                case "Suit_Iva_Standard_FemaleNRM3":
                case "Suit_Iva_Standard_FemaleNRM4":
                case "Suit_Iva_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Suit_Iva_Standard_Male0":
                case "Suit_Iva_Standard_Male1":
                case "Suit_Iva_Standard_Male2":
                case "Suit_Iva_Standard_Male3":
                case "Suit_Iva_Standard_Male4":
                case "Suit_Iva_Standard_Male5":
                case "kerbalMainGrey":
                case "kerbalMainGrey0":
                case "kerbalMainGrey1":
                case "kerbalMainGrey2":
                case "kerbalMainGrey3":
                case "kerbalMainGrey4":
                case "kerbalMainGrey5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Standard_Male[i] = texture;
                    return true;

                case "Suit_Iva_Standard_MaleNRM0":
                case "Suit_Iva_Standard_MaleNRM1":
                case "Suit_Iva_Standard_MaleNRM2":
                case "Suit_Iva_Standard_MaleNRM3":
                case "Suit_Iva_Standard_MaleNRM4":
                case "Suit_Iva_Standard_MaleNRM5":
                case "kerbalMainNRM":
                case "kerbalMainNRM0":
                case "kerbalMainNRM1":
                case "kerbalMainNRM2":
                case "kerbalMainNRM3":
                case "kerbalMainNRM4":
                case "kerbalMainNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Standard_MaleNRM[i] = texture;
                    return true;

                case "Suit_Iva_VetBad_Female0":
                case "Suit_Iva_VetBad_Female1":
                case "Suit_Iva_VetBad_Female2":
                case "Suit_Iva_VetBad_Female3":
                case "Suit_Iva_VetBad_Female4":
                case "Suit_Iva_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_Iva_VetBad_Female[i] = texture;
                    return true;

                case "Suit_Iva_VetBad_FemaleNRM0":
                case "Suit_Iva_VetBad_FemaleNRM1":
                case "Suit_Iva_VetBad_FemaleNRM2":
                case "Suit_Iva_VetBad_FemaleNRM3":
                case "Suit_Iva_VetBad_FemaleNRM4":
                case "Suit_Iva_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Suit_Iva_VetBad_Male0":
                case "Suit_Iva_VetBad_Male1":
                case "Suit_Iva_VetBad_Male2":
                case "Suit_Iva_VetBad_Male3":
                case "Suit_Iva_VetBad_Male4":
                case "Suit_Iva_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_VetBad_Male[i] = texture;
                    return true;

                case "Suit_Iva_VetBad_MaleNRM0":
                case "Suit_Iva_VetBad_MaleNRM1":
                case "Suit_Iva_VetBad_MaleNRM2":
                case "Suit_Iva_VetBad_MaleNRM3":
                case "Suit_Iva_VetBad_MaleNRM4":
                case "Suit_Iva_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Suit_Iva_Veteran_Female0":
                case "Suit_Iva_Veteran_Female1":
                case "Suit_Iva_Veteran_Female2":
                case "Suit_Iva_Veteran_Female3":
                case "Suit_Iva_Veteran_Female4":
                case "Suit_Iva_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Veteran_Female[i] = texture;
                    return true;

                case "Suit_Iva_Veteran_FemaleNRM0":
                case "Suit_Iva_Veteran_FemaleNRM1":
                case "Suit_Iva_Veteran_FemaleNRM2":
                case "Suit_Iva_Veteran_FemaleNRM3":
                case "Suit_Iva_Veteran_FemaleNRM4":
                case "Suit_Iva_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Suit_Iva_Veteran_Male0":
                case "Suit_Iva_Veteran_Male1":
                case "Suit_Iva_Veteran_Male2":
                case "Suit_Iva_Veteran_Male3":
                case "Suit_Iva_Veteran_Male4":
                case "Suit_Iva_Veteran_Male5":
                case "kerbalMain":
                case "kerbalMain1":
                case "kerbalMain2":
                case "kerbalMain3":
                case "kerbalMain4":
                case "kerbalMain5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Veteran_Male[i] = texture;
                    return true;

                case "Suit_Iva_Veteran_MaleNRM0":
                case "Suit_Iva_Veteran_MaleNRM1":
                case "Suit_Iva_Veteran_MaleNRM2":
                case "Suit_Iva_Veteran_MaleNRM3":
                case "Suit_Iva_Veteran_MaleNRM4":
                case "Suit_Iva_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        suit_Iva_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Visor_EvaGround_Badass_Female0":
                case "Visor_EvaGround_Badass_Female1":
                case "Visor_EvaGround_Badass_Female2":
                case "Visor_EvaGround_Badass_Female3":
                case "Visor_EvaGround_Badass_Female4":
                case "Visor_EvaGround_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Badass_Female[i] = texture;
                    return true;

                case "Visor_EvaGround_Badass_FemaleNRM0":
                case "Visor_EvaGround_Badass_FemaleNRM1":
                case "Visor_EvaGround_Badass_FemaleNRM2":
                case "Visor_EvaGround_Badass_FemaleNRM3":
                case "Visor_EvaGround_Badass_FemaleNRM4":
                case "Visor_EvaGround_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Visor_EvaGround_Badass_Male0":
                case "Visor_EvaGround_Badass_Male1":
                case "Visor_EvaGround_Badass_Male2":
                case "Visor_EvaGround_Badass_Male3":
                case "Visor_EvaGround_Badass_Male4":
                case "Visor_EvaGround_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Badass_Male[i] = texture;
                    return true;

                case "Visor_EvaGround_Badass_MaleNRM0":
                case "Visor_EvaGround_Badass_MaleNRM1":
                case "Visor_EvaGround_Badass_MaleNRM2":
                case "Visor_EvaGround_Badass_MaleNRM3":
                case "Visor_EvaGround_Badass_MaleNRM4":
                case "Visor_EvaGround_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Badass_MaleNRM[i] = texture;
                    return true;

                case "Visor_EvaGround_Standard_Female0":
                case "Visor_EvaGround_Standard_Female1":
                case "Visor_EvaGround_Standard_Female2":
                case "Visor_EvaGround_Standard_Female3":
                case "Visor_EvaGround_Standard_Female4":
                case "Visor_EvaGround_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Standard_Female[i] = texture;
                    return true;

                case "Visor_EvaGround_Standard_FemaleNRM0":
                case "Visor_EvaGround_Standard_FemaleNRM1":
                case "Visor_EvaGround_Standard_FemaleNRM2":
                case "Visor_EvaGround_Standard_FemaleNRM3":
                case "Visor_EvaGround_Standard_FemaleNRM4":
                case "Visor_EvaGround_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Visor_EvaGround_Standard_Male0":
                case "Visor_EvaGround_Standard_Male1":
                case "Visor_EvaGround_Standard_Male2":
                case "Visor_EvaGround_Standard_Male3":
                case "Visor_EvaGround_Standard_Male4":
                case "Visor_EvaGround_Standard_Male5":
                case "EVAgroundVisor":
                case "EVAgroundVisor0":
                case "EVAgroundVisor1":
                case "EVAgroundVisor2":
                case "EVAgroundVisor3":
                case "EVAgroundVisor4":
                case "EVAgroundVisor5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Standard_Male[i] = texture;
                    return true;

                case "Visor_EvaGround_Standard_MaleNRM0":
                case "Visor_EvaGround_Standard_MaleNRM1":
                case "Visor_EvaGround_Standard_MaleNRM2":
                case "Visor_EvaGround_Standard_MaleNRM3":
                case "Visor_EvaGround_Standard_MaleNRM4":
                case "Visor_EvaGround_Standard_MaleNRM5":
                case "EVAgroundVisorNRM":
                case "EVAgroundVisorNRM0":
                case "EVAgroundVisorNRM1":
                case "EVAgroundVisorNRM2":
                case "EVAgroundVisorNRM3":
                case "EVAgroundVisorNRM4":
                case "EVAgroundVisorNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Standard_MaleNRM[i] = texture;
                    return true;

                case "Visor_EvaGround_VetBad_Female0":
                case "Visor_EvaGround_VetBad_Female1":
                case "Visor_EvaGround_VetBad_Female2":
                case "Visor_EvaGround_VetBad_Female3":
                case "Visor_EvaGround_VetBad_Female4":
                case "Visor_EvaGround_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_VetBad_Female[i] = texture;
                    return true;

                case "Visor_EvaGround_VetBad_FemaleNRM0":
                case "Visor_EvaGround_VetBad_FemaleNRM1":
                case "Visor_EvaGround_VetBad_FemaleNRM2":
                case "Visor_EvaGround_VetBad_FemaleNRM3":
                case "Visor_EvaGround_VetBad_FemaleNRM4":
                case "Visor_EvaGround_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Visor_EvaGround_VetBad_Male0":
                case "Visor_EvaGround_VetBad_Male1":
                case "Visor_EvaGround_VetBad_Male2":
                case "Visor_EvaGround_VetBad_Male3":
                case "Visor_EvaGround_VetBad_Male4":
                case "Visor_EvaGround_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_VetBad_Male[i] = texture;
                    return true;

                case "Visor_EvaGround_VetBad_MaleNRM0":
                case "Visor_EvaGround_VetBad_MaleNRM1":
                case "Visor_EvaGround_VetBad_MaleNRM2":
                case "Visor_EvaGround_VetBad_MaleNRM3":
                case "Visor_EvaGround_VetBad_MaleNRM4":
                case "Visor_EvaGround_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Visor_EvaGround_Veteran_Female0":
                case "Visor_EvaGround_Veteran_Female1":
                case "Visor_EvaGround_Veteran_Female2":
                case "Visor_EvaGround_Veteran_Female3":
                case "Visor_EvaGround_Veteran_Female4":
                case "Visor_EvaGround_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Veteran_Female[i] = texture;
                    return true;

                case "Visor_EvaGround_Veteran_FemaleNRM0":
                case "Visor_EvaGround_Veteran_FemaleNRM1":
                case "Visor_EvaGround_Veteran_FemaleNRM2":
                case "Visor_EvaGround_Veteran_FemaleNRM3":
                case "Visor_EvaGround_Veteran_FemaleNRM4":
                case "Visor_EvaGround_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Visor_EvaGround_Veteran_Male0":
                case "Visor_EvaGround_Veteran_Male1":
                case "Visor_EvaGround_Veteran_Male2":
                case "Visor_EvaGround_Veteran_Male3":
                case "Visor_EvaGround_Veteran_Male4":
                case "Visor_EvaGround_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Veteran_Male[i] = texture;
                    return true;

                case "Visor_EvaGround_Veteran_MaleNRM0":
                case "Visor_EvaGround_Veteran_MaleNRM1":
                case "Visor_EvaGround_Veteran_MaleNRM2":
                case "Visor_EvaGround_Veteran_MaleNRM3":
                case "Visor_EvaGround_Veteran_MaleNRM4":
                case "Visor_EvaGround_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        visor_EvaGround_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Visor_EvaSpace_Badass_Female0":
                case "Visor_EvaSpace_Badass_Female1":
                case "Visor_EvaSpace_Badass_Female2":
                case "Visor_EvaSpace_Badass_Female3":
                case "Visor_EvaSpace_Badass_Female4":
                case "Visor_EvaSpace_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Badass_Female[i] = texture;
                    return true;

                case "Visor_EvaSpace_Badass_FemaleNRM0":
                case "Visor_EvaSpace_Badass_FemaleNRM1":
                case "Visor_EvaSpace_Badass_FemaleNRM2":
                case "Visor_EvaSpace_Badass_FemaleNRM3":
                case "Visor_EvaSpace_Badass_FemaleNRM4":
                case "Visor_EvaSpace_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Visor_EvaSpace_Badass_Male0":
                case "Visor_EvaSpace_Badass_Male1":
                case "Visor_EvaSpace_Badass_Male2":
                case "Visor_EvaSpace_Badass_Male3":
                case "Visor_EvaSpace_Badass_Male4":
                case "Visor_EvaSpace_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Badass_Male[i] = texture;
                    return true;

                case "Visor_EvaSpace_Badass_MaleNRM0":
                case "Visor_EvaSpace_Badass_MaleNRM1":
                case "Visor_EvaSpace_Badass_MaleNRM2":
                case "Visor_EvaSpace_Badass_MaleNRM3":
                case "Visor_EvaSpace_Badass_MaleNRM4":
                case "Visor_EvaSpace_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Badass_MaleNRM[i] = texture;
                    return true;

                case "Visor_EvaSpace_Standard_Female0":
                case "Visor_EvaSpace_Standard_Female1":
                case "Visor_EvaSpace_Standard_Female2":
                case "Visor_EvaSpace_Standard_Female3":
                case "Visor_EvaSpace_Standard_Female4":
                case "Visor_EvaSpace_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Standard_Female[i] = texture;
                    return true;

                case "Visor_EvaSpace_Standard_FemaleNRM0":
                case "Visor_EvaSpace_Standard_FemaleNRM1":
                case "Visor_EvaSpace_Standard_FemaleNRM2":
                case "Visor_EvaSpace_Standard_FemaleNRM3":
                case "Visor_EvaSpace_Standard_FemaleNRM4":
                case "Visor_EvaSpace_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Visor_EvaSpace_Standard_Male0":
                case "Visor_EvaSpace_Standard_Male1":
                case "Visor_EvaSpace_Standard_Male2":
                case "Visor_EvaSpace_Standard_Male3":
                case "Visor_EvaSpace_Standard_Male4":
                case "Visor_EvaSpace_Standard_Male5":
                case "EVAvisor":
                case "EVAvisor0":
                case "EVAvisor1":
                case "EVAvisor2":
                case "EVAvisor3":
                case "EVAvisor4":
                case "EVAvisor5":

                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Standard_Male[i] = texture;
                    return true;

                case "Visor_EvaSpace_Standard_MaleNRM0":
                case "Visor_EvaSpace_Standard_MaleNRM1":
                case "Visor_EvaSpace_Standard_MaleNRM2":
                case "Visor_EvaSpace_Standard_MaleNRM3":
                case "Visor_EvaSpace_Standard_MaleNRM4":
                case "Visor_EvaSpace_Standard_MaleNRM5":
                case "EVAvisorNRM":
                case "EVAvisorNRM0":
                case "EVAvisorNRM1":
                case "EVAvisorNRM2":
                case "EVAvisorNRM3":
                case "EVAvisorNRM4":
                case "EVAvisorNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Standard_MaleNRM[i] = texture;
                    return true;

                case "Visor_EvaSpace_VetBad_Female0":
                case "Visor_EvaSpace_VetBad_Female1":
                case "Visor_EvaSpace_VetBad_Female2":
                case "Visor_EvaSpace_VetBad_Female3":
                case "Visor_EvaSpace_VetBad_Female4":
                case "Visor_EvaSpace_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_VetBad_Female[i] = texture;
                    return true;

                case "Visor_EvaSpace_VetBad_FemaleNRM0":
                case "Visor_EvaSpace_VetBad_FemaleNRM1":
                case "Visor_EvaSpace_VetBad_FemaleNRM2":
                case "Visor_EvaSpace_VetBad_FemaleNRM3":
                case "Visor_EvaSpace_VetBad_FemaleNRM4":
                case "Visor_EvaSpace_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Visor_EvaSpace_VetBad_Male0":
                case "Visor_EvaSpace_VetBad_Male1":
                case "Visor_EvaSpace_VetBad_Male2":
                case "Visor_EvaSpace_VetBad_Male3":
                case "Visor_EvaSpace_VetBad_Male4":
                case "Visor_EvaSpace_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_VetBad_Male[i] = texture;
                    return true;

                case "Visor_EvaSpace_VetBad_MaleNRM0":
                case "Visor_EvaSpace_VetBad_MaleNRM1":
                case "Visor_EvaSpace_VetBad_MaleNRM2":
                case "Visor_EvaSpace_VetBad_MaleNRM3":
                case "Visor_EvaSpace_VetBad_MaleNRM4":
                case "Visor_EvaSpace_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Visor_EvaSpace_Veteran_Female0":
                case "Visor_EvaSpace_Veteran_Female1":
                case "Visor_EvaSpace_Veteran_Female2":
                case "Visor_EvaSpace_Veteran_Female3":
                case "Visor_EvaSpace_Veteran_Female4":
                case "Visor_EvaSpace_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Veteran_Female[i] = texture;
                    return true;

                case "Visor_EvaSpace_Veteran_FemaleNRM0":
                case "Visor_EvaSpace_Veteran_FemaleNRM1":
                case "Visor_EvaSpace_Veteran_FemaleNRM2":
                case "Visor_EvaSpace_Veteran_FemaleNRM3":
                case "Visor_EvaSpace_Veteran_FemaleNRM4":
                case "Visor_EvaSpace_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Visor_EvaSpace_Veteran_Male0":
                case "Visor_EvaSpace_Veteran_Male1":
                case "Visor_EvaSpace_Veteran_Male2":
                case "Visor_EvaSpace_Veteran_Male3":
                case "Visor_EvaSpace_Veteran_Male4":
                case "Visor_EvaSpace_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Veteran_Male[i] = texture;
                    return true;

                case "Visor_EvaSpace_Veteran_MaleNRM0":
                case "Visor_EvaSpace_Veteran_MaleNRM1":
                case "Visor_EvaSpace_Veteran_MaleNRM2":
                case "Visor_EvaSpace_Veteran_MaleNRM3":
                case "Visor_EvaSpace_Veteran_MaleNRM4":
                case "Visor_EvaSpace_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_EvaSpace_Veteran_MaleNRM[i] = texture;
                    return true;

                case "Visor_Iva_Badass_Female0":
                case "Visor_Iva_Badass_Female1":
                case "Visor_Iva_Badass_Female2":
                case "Visor_Iva_Badass_Female3":
                case "Visor_Iva_Badass_Female4":
                case "Visor_Iva_Badass_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Badass_Female[i] = texture;
                    return true;

                case "Visor_Iva_Badass_FemaleNRM0":
                case "Visor_Iva_Badass_FemaleNRM1":
                case "Visor_Iva_Badass_FemaleNRM2":
                case "Visor_Iva_Badass_FemaleNRM3":
                case "Visor_Iva_Badass_FemaleNRM4":
                case "Visor_Iva_Badass_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Badass_FemaleNRM[i] = texture;
                    return true;

                case "Visor_Iva_Badass_Male0":
                case "Visor_Iva_Badass_Male1":
                case "Visor_Iva_Badass_Male2":
                case "Visor_Iva_Badass_Male3":
                case "Visor_Iva_Badass_Male4":
                case "Visor_Iva_Badass_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Badass_Male[i] = texture;
                    return true;

                case "Visor_Iva_Badass_MaleNRM0":
                case "Visor_Iva_Badass_MaleNRM1":
                case "Visor_Iva_Badass_MaleNRM2":
                case "Visor_Iva_Badass_MaleNRM3":
                case "Visor_Iva_Badass_MaleNRM4":
                case "Visor_Iva_Badass_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Badass_MaleNRM[i] = texture;
                    return true;

                case "Visor_Iva_Standard_Female0":
                case "Visor_Iva_Standard_Female1":
                case "Visor_Iva_Standard_Female2":
                case "Visor_Iva_Standard_Female3":
                case "Visor_Iva_Standard_Female4":
                case "Visor_Iva_Standard_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Standard_Female[i] = texture;
                    return true;

                case "Visor_Iva_Standard_FemaleNRM0":
                case "Visor_Iva_Standard_FemaleNRM1":
                case "Visor_Iva_Standard_FemaleNRM2":
                case "Visor_Iva_Standard_FemaleNRM3":
                case "Visor_Iva_Standard_FemaleNRM4":
                case "Visor_Iva_Standard_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Standard_FemaleNRM[i] = texture;
                    return true;

                case "Visor_Iva_Standard_Male0":
                case "Visor_Iva_Standard_Male1":
                case "Visor_Iva_Standard_Male2":
                case "Visor_Iva_Standard_Male3":
                case "Visor_Iva_Standard_Male4":
                case "Visor_Iva_Standard_Male5":
                case "kerbalVisor":
                case "kerbalVisor0":
                case "kerbalVisor1":
                case "kerbalVisor2":
                case "kerbalVisor3":
                case "kerbalVisor4":
                case "kerbalVisor5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Standard_Male[i] = texture;
                    return true;

                case "Visor_Iva_Standard_MaleNRM0":
                case "Visor_Iva_Standard_MaleNRM1":
                case "Visor_Iva_Standard_MaleNRM2":
                case "Visor_Iva_Standard_MaleNRM3":
                case "Visor_Iva_Standard_MaleNRM4":
                case "Visor_Iva_Standard_MaleNRM5":
                case "kerbalVisorNRM":
                case "kerbalVisorNRM0":
                case "kerbalVisorNRM1":
                case "kerbalVisorNRM2":
                case "kerbalVisorNRM3":
                case "kerbalVisorNRM4":
                case "kerbalVisorNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Standard_MaleNRM[i] = texture;
                    return true;

                case "Visor_Iva_VetBad_Female0":
                case "Visor_Iva_VetBad_Female1":
                case "Visor_Iva_VetBad_Female2":
                case "Visor_Iva_VetBad_Female3":
                case "Visor_Iva_VetBad_Female4":
                case "Visor_Iva_VetBad_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_VetBad_Female[i] = texture;
                    return true;

                case "Visor_Iva_VetBad_FemaleNRM0":
                case "Visor_Iva_VetBad_FemaleNRM1":
                case "Visor_Iva_VetBad_FemaleNRM2":
                case "Visor_Iva_VetBad_FemaleNRM3":
                case "Visor_Iva_VetBad_FemaleNRM4":
                case "Visor_Iva_VetBad_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_VetBad_FemaleNRM[i] = texture;
                    return true;

                case "Visor_Iva_VetBad_Male0":
                case "Visor_Iva_VetBad_Male1":
                case "Visor_Iva_VetBad_Male2":
                case "Visor_Iva_VetBad_Male3":
                case "Visor_Iva_VetBad_Male4":
                case "Visor_Iva_VetBad_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_VetBad_Male[i] = texture;
                    return true;

                case "Visor_Iva_VetBad_MaleNRM0":
                case "Visor_Iva_VetBad_MaleNRM1":
                case "Visor_Iva_VetBad_MaleNRM2":
                case "Visor_Iva_VetBad_MaleNRM3":
                case "Visor_Iva_VetBad_MaleNRM4":
                case "Visor_Iva_VetBad_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_VetBad_MaleNRM[i] = texture;
                    return true;

                case "Visor_Iva_Veteran_Female0":
                case "Visor_Iva_Veteran_Female1":
                case "Visor_Iva_Veteran_Female2":
                case "Visor_Iva_Veteran_Female3":
                case "Visor_Iva_Veteran_Female4":
                case "Visor_Iva_Veteran_Female5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                   
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Veteran_Female[i] = texture;
                    return true;

                case "Visor_Iva_Veteran_FemaleNRM0":
                case "Visor_Iva_Veteran_FemaleNRM1":
                case "Visor_Iva_Veteran_FemaleNRM2":
                case "Visor_Iva_Veteran_FemaleNRM3":
                case "Visor_Iva_Veteran_FemaleNRM4":
                case "Visor_Iva_Veteran_FemaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Veteran_FemaleNRM[i] = texture;
                    return true;

                case "Visor_Iva_Veteran_Male0":
                case "Visor_Iva_Veteran_Male1":
                case "Visor_Iva_Veteran_Male2":
                case "Visor_Iva_Veteran_Male3":
                case "Visor_Iva_Veteran_Male4":
                case "Visor_Iva_Veteran_Male5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Veteran_Male[i] = texture;
                    return true;

                case "Visor_Iva_Veteran_MaleNRM0":
                case "Visor_Iva_Veteran_MaleNRM1":
                case "Visor_Iva_Veteran_MaleNRM2":
                case "Visor_Iva_Veteran_MaleNRM3":
                case "Visor_Iva_Veteran_MaleNRM4":
                case "Visor_Iva_Veteran_MaleNRM5":
                    try
                    {
                        string temp = originalName.Substring(originalName.Length - 1);
                        level = Int32.Parse(temp);
                    }
                    catch (FormatException)
                    {
                        level = 0;
                    }
                    
                    for (int i = level; i < 6; ++i)
                        visor_Iva_Veteran_MaleNRM[i] = texture;
                    return true;


                default:
                    return false;
            }
        }
    }
}