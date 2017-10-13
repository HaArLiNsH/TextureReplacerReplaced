/*
 * Copyright © 2017 HaArLiNsH 
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

using KSP.UI.Screens;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextureReplacerReplaced
{
    public class Suit_Filter
    {
        Personaliser personaliser = Personaliser.instance;
        public KerbalData kerbalData = new KerbalData();
        public int gender = 0;
        public bool isVeteran = false;
        public bool isBadass = false;
        public int level = 0;
        public Suit_Set suit = new Suit_Set();

        public Suit_Filter()
        {
            
        }

        public Suit_Filter(KerbalData kData, int lvl, Suit_Set suitSet)
        {
            kerbalData = kData;
            gender = kerbalData.gender;
            isVeteran = kerbalData.isVeteran;
            isBadass = kerbalData.isBadass;
            level = lvl;
            suit = suitSet;

        }

        public void get_suit_EvaGround(out Texture2D texture, out Texture2D normalMap)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_EvaGround_VetBad_Male(level);
                    normalMap = suit.get_suit_EvaGround_VetBad_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_EvaGround_VetBad_Female(level);
                    normalMap = suit.get_suit_EvaGround_VetBad_FemaleNRM(level);
                    return;
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_EvaGround_Veteran_Male(level);
                    normalMap = suit.get_suit_EvaGround_Veteran_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_EvaGround_Veteran_Female(level);
                    normalMap = suit.get_suit_EvaGround_Veteran_FemaleNRM(level);
                    return;
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_EvaGround_Badass_Male(level);
                    normalMap = suit.get_suit_EvaGround_Badass_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_EvaGround_Badass_Female(level);
                    normalMap = suit.get_suit_EvaGround_Badass_FemaleNRM(level);
                    return;
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_EvaGround_Standard_Male(level);
                    normalMap = suit.get_suit_EvaGround_Standard_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_EvaGround_Standard_Female(level);
                    normalMap = suit.get_suit_EvaGround_Standard_FemaleNRM(level);
                    return;
                }
            }

        }

        public void get_suit_EvaSpace(out Texture2D texture, out Texture2D normalMap)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_EvaSpace_VetBad_Male(level);
                    normalMap = suit.get_suit_EvaSpace_VetBad_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_EvaSpace_VetBad_Female(level);
                    normalMap = suit.get_suit_EvaSpace_VetBad_FemaleNRM(level);
                    return;
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_EvaSpace_Veteran_Male(level);
                    normalMap = suit.get_suit_EvaSpace_Veteran_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_EvaSpace_Veteran_Female(level);
                    normalMap = suit.get_suit_EvaSpace_Veteran_FemaleNRM(level);
                    return;
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_EvaSpace_Badass_Male(level);
                    normalMap = suit.get_suit_EvaSpace_Badass_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_EvaSpace_Badass_Female(level);
                    normalMap = suit.get_suit_EvaSpace_Badass_FemaleNRM(level);
                    return;
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_EvaSpace_Standard_Male(level);
                    normalMap = suit.get_suit_EvaSpace_Standard_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_EvaSpace_Standard_Female(level);
                    normalMap = suit.get_suit_EvaSpace_Standard_FemaleNRM(level);
                    return;
                }
            }
        }

        public void get_suit_Iva(out Texture2D texture, out Texture2D normalMap)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_Iva_VetBad_Male(level);
                    normalMap = suit.get_suit_Iva_VetBad_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_Iva_VetBad_Female(level);
                    normalMap = suit.get_suit_Iva_VetBad_FemaleNRM(level);
                    return;
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_Iva_Veteran_Male(level);
                    normalMap = suit.get_suit_Iva_Veteran_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_Iva_Veteran_Female(level);
                    normalMap = suit.get_suit_Iva_Veteran_FemaleNRM(level);
                    return;
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_Iva_Badass_Male(level);
                    normalMap = suit.get_suit_Iva_Badass_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_Iva_Badass_Female(level);
                    normalMap = suit.get_suit_Iva_Badass_FemaleNRM(level);
                    return;
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_suit_Iva_Standard_Male(level);
                    normalMap = suit.get_suit_Iva_Standard_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_suit_Iva_Standard_Female(level);
                    normalMap = suit.get_suit_Iva_Standard_FemaleNRM(level);
                    return;
                }
            }

        }

        public void get_helmet_EvaGround(out Texture2D texture, out Texture2D normalMap)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_EvaGround_VetBad_Male(level);
                    normalMap = suit.get_helmet_EvaGround_VetBad_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_EvaGround_VetBad_Female(level);
                    normalMap = suit.get_helmet_EvaGround_VetBad_FemaleNRM(level);
                    return;
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_EvaGround_Veteran_Male(level);
                    normalMap = suit.get_helmet_EvaGround_Veteran_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_EvaGround_Veteran_Female(level);
                    normalMap = suit.get_helmet_EvaGround_Veteran_FemaleNRM(level);
                    return;
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_EvaGround_Badass_Male(level);
                    normalMap = suit.get_helmet_EvaGround_Badass_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_EvaGround_Badass_Female(level);
                    normalMap = suit.get_helmet_EvaGround_Badass_FemaleNRM(level);
                    return;
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_EvaGround_Standard_Male(level);
                    normalMap = suit.get_helmet_EvaGround_Standard_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_EvaGround_Standard_Female(level);
                    normalMap = suit.get_helmet_EvaGround_Standard_FemaleNRM(level);
                    return;
                }
            }

        }

        public void get_helmet_EvaSpace(out Texture2D texture, out Texture2D normalMap)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_EvaSpace_VetBad_Male(level);
                    normalMap = suit.get_helmet_EvaSpace_VetBad_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_EvaSpace_VetBad_Female(level);
                    normalMap = suit.get_helmet_EvaSpace_VetBad_FemaleNRM(level);
                    return;
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_EvaSpace_Veteran_Male(level);
                    normalMap = suit.get_helmet_EvaSpace_Veteran_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_EvaSpace_Veteran_Female(level);
                    normalMap = suit.get_helmet_EvaSpace_Veteran_FemaleNRM(level);
                    return;
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_EvaSpace_Badass_Male(level);
                    normalMap = suit.get_helmet_EvaSpace_Badass_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_EvaSpace_Badass_Female(level);
                    normalMap = suit.get_helmet_EvaSpace_Badass_FemaleNRM(level);
                    return;
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_EvaSpace_Standard_Male(level);
                    normalMap = suit.get_helmet_EvaSpace_Standard_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_EvaSpace_Standard_Female(level);
                    normalMap = suit.get_helmet_EvaSpace_Standard_FemaleNRM(level);
                    return;
                }
            }
        }

        public void get_helmet_Iva(out Texture2D texture, out Texture2D normalMap)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_Iva_VetBad_Male(level);
                    normalMap = suit.get_helmet_Iva_VetBad_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_Iva_VetBad_Female(level);
                    normalMap = suit.get_helmet_Iva_VetBad_FemaleNRM(level);
                    return;
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_Iva_Veteran_Male(level);
                    normalMap = suit.get_helmet_Iva_Veteran_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_Iva_Veteran_Female(level);
                    normalMap = suit.get_helmet_Iva_Veteran_FemaleNRM(level);
                    return;
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_Iva_Badass_Male(level);
                    normalMap = suit.get_helmet_Iva_Badass_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_Iva_Badass_Female(level);
                    normalMap = suit.get_helmet_Iva_Badass_FemaleNRM(level);
                    return;
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_helmet_Iva_Standard_Male(level);
                    normalMap = suit.get_helmet_Iva_Standard_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_helmet_Iva_Standard_Female(level);
                    normalMap = suit.get_helmet_Iva_Standard_FemaleNRM(level);
                    return;
                }
            }

        }

        public void get_visor_EvaGround(out Texture2D texture, out Texture2D normalMap, out Color32 reflectionColor, out Color32 baseColor)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_EvaGround_VetBad_Male(level);
                    normalMap = suit.get_visor_EvaGround_VetBad_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_EvaGround_VetBad_Female(level);
                    normalMap = suit.get_visor_EvaGround_VetBad_FemaleNRM(level);
                    
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_EvaGround_Veteran_Male(level);
                    normalMap = suit.get_visor_EvaGround_Veteran_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_EvaGround_Veteran_Female(level);
                    normalMap = suit.get_visor_EvaGround_Veteran_FemaleNRM(level);
                    
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_EvaGround_Badass_Male(level);
                    normalMap = suit.get_visor_EvaGround_Badass_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_EvaGround_Badass_Female(level);
                    normalMap = suit.get_visor_EvaGround_Badass_FemaleNRM(level);
                    
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_EvaGround_Standard_Male(level);
                    normalMap = suit.get_visor_EvaGround_Standard_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_EvaGround_Standard_Female(level);
                    normalMap = suit.get_visor_EvaGround_Standard_FemaleNRM(level);
                    
                }
            }
            reflectionColor = suit.get_EvaGround_VisorReflectionColor(level);
            baseColor = suit.get_EvaGround_VisorBaseColor(level);
        }

        public void get_visor_EvaSpace(out Texture2D texture, out Texture2D normalMap, out Color32 reflectionColor, out Color32 baseColor)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_EvaSpace_VetBad_Male(level);
                    normalMap = suit.get_visor_EvaSpace_VetBad_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_EvaSpace_VetBad_Female(level);
                    normalMap = suit.get_visor_EvaSpace_VetBad_FemaleNRM(level);
                    
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_EvaSpace_Veteran_Male(level);
                    normalMap = suit.get_visor_EvaSpace_Veteran_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_EvaSpace_Veteran_Female(level);
                    normalMap = suit.get_visor_EvaSpace_Veteran_FemaleNRM(level);
                    
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_EvaSpace_Badass_Male(level);
                    normalMap = suit.get_visor_EvaSpace_Badass_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_EvaSpace_Badass_Female(level);
                    normalMap = suit.get_visor_EvaSpace_Badass_FemaleNRM(level);
                    
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_EvaSpace_Standard_Male(level);
                    normalMap = suit.get_visor_EvaSpace_Standard_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_EvaSpace_Standard_Female(level);
                    normalMap = suit.get_visor_EvaSpace_Standard_FemaleNRM(level);
                    
                }
            }
            reflectionColor = suit.get_EvaSpace_VisorReflectionColor(level);
            baseColor = suit.get_EvaSpace_VisorBaseColor(level);
        }

        public void get_visor_Iva(out Texture2D texture, out Texture2D normalMap, out Color32 reflectionColor, out Color32 baseColor)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_Iva_VetBad_Male(level);
                    normalMap = suit.get_visor_Iva_VetBad_MaleNRM(level);                 
                    
                }
                else
                {
                    texture = suit.get_visor_Iva_VetBad_Female(level);
                    normalMap = suit.get_visor_Iva_VetBad_FemaleNRM(level);
                    
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_Iva_Veteran_Male(level);
                    normalMap = suit.get_visor_Iva_Veteran_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_Iva_Veteran_Female(level);
                    normalMap = suit.get_visor_Iva_Veteran_FemaleNRM(level);
                    
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_Iva_Badass_Male(level);
                    normalMap = suit.get_visor_Iva_Badass_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_Iva_Badass_Female(level);
                    normalMap = suit.get_visor_Iva_Badass_FemaleNRM(level);
                    
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_visor_Iva_Standard_Male(level);
                    normalMap = suit.get_visor_Iva_Standard_MaleNRM(level);
                    
                }
                else
                {
                    texture = suit.get_visor_Iva_Standard_Female(level);
                    normalMap = suit.get_visor_Iva_Standard_FemaleNRM(level);
                    
                }
            }
            reflectionColor = suit.get_Iva_VisorReflectionColor(level);
            baseColor = suit.get_Iva_VisorBaseColor(level);
        }

        public void get_jetpack_EvaGround(out Texture2D texture, out Texture2D normalMap)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_jetpack_EvaGround_VetBad_Male(level);
                    normalMap = suit.get_jetpack_EvaGround_VetBad_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_jetpack_EvaGround_VetBad_Female(level);
                    normalMap = suit.get_jetpack_EvaGround_VetBad_FemaleNRM(level);
                    return;
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_jetpack_EvaGround_Veteran_Male(level);
                    normalMap = suit.get_jetpack_EvaGround_Veteran_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_jetpack_EvaGround_Veteran_Female(level);
                    normalMap = suit.get_jetpack_EvaGround_Veteran_FemaleNRM(level);
                    return;
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_jetpack_EvaGround_Badass_Male(level);
                    normalMap = suit.get_jetpack_EvaGround_Badass_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_jetpack_EvaGround_Badass_Female(level);
                    normalMap = suit.get_jetpack_EvaGround_Badass_FemaleNRM(level);
                    return;
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_jetpack_EvaGround_Standard_Male(level);
                    normalMap = suit.get_jetpack_EvaGround_Standard_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_jetpack_EvaGround_Standard_Female(level);
                    normalMap = suit.get_jetpack_EvaGround_Standard_FemaleNRM(level);
                    return;
                }
            }

        }

        public void get_jetpack_EvaSpace(out Texture2D texture, out Texture2D normalMap)
        {
            if (isBadass && isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_jetpack_EvaSpace_VetBad_Male(level);
                    normalMap = suit.get_jetpack_EvaSpace_VetBad_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_jetpack_EvaSpace_VetBad_Female(level);
                    normalMap = suit.get_jetpack_EvaSpace_VetBad_FemaleNRM(level);
                    return;
                }
            }
            else if (isVeteran)
            {
                if (gender == 0)
                {
                    texture = suit.get_jetpack_EvaSpace_Veteran_Male(level);
                    normalMap = suit.get_jetpack_EvaSpace_Veteran_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_jetpack_EvaSpace_Veteran_Female(level);
                    normalMap = suit.get_jetpack_EvaSpace_Veteran_FemaleNRM(level);
                    return;
                }
            }
            else if (isBadass)
            {
                if (gender == 0)
                {
                    texture = suit.get_jetpack_EvaSpace_Badass_Male(level);
                    normalMap = suit.get_jetpack_EvaSpace_Badass_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_jetpack_EvaSpace_Badass_Female(level);
                    normalMap = suit.get_jetpack_EvaSpace_Badass_FemaleNRM(level);
                    return;
                }
            }
            else
            {
                if (gender == 0)
                {
                    texture = suit.get_jetpack_EvaSpace_Standard_Male(level);
                    normalMap = suit.get_jetpack_EvaSpace_Standard_MaleNRM(level);
                    return;
                }
                else
                {
                    texture = suit.get_jetpack_EvaSpace_Standard_Female(level);
                    normalMap = suit.get_jetpack_EvaSpace_Standard_FemaleNRM(level);
                    return;
                }
            }
        }  
    }
}
