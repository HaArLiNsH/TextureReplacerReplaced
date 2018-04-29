/*
 * Copyright © 2017-2018 HaArLiNsH 
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
using System.Text;
using UnityEngine;

namespace TextureReplacerReplaced
{
    class Suit_Selector
    {

        Personaliser personaliser = Personaliser.instance;
        KerbalData kerbalData = new KerbalData();        
        int level = 0;
        Suit_Set suit = new Suit_Set();
        Suit_Filter suit_Filter = new Suit_Filter();


        public Suit_Selector(KerbalData kData, int lvl, Suit_Set suitSet)
        {
            kerbalData = kData;            
            level = lvl;
            suit = suitSet;

            suit_Filter.kerbalData = kerbalData;
            suit_Filter.gender = kerbalData.gender;
            suit_Filter.isVeteran = kerbalData.isVeteran;
            suit_Filter.isBadass = kerbalData.isBadass;
            suit_Filter.level = level;
            suit_Filter.suit = suit;

        }

        public void select_suit_EvaGround_NoAtmo(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.suit_EvaGround_NoAtmo)
            {
                case 0:
                    suit_Filter.get_suit_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_suit_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_suit_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_suit_EvaSpace(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.suit_EvaSpace)
            {
                case 0:
                    suit_Filter.get_suit_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_suit_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_suit_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_suit_EvaGround_Atmo(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.suit_EvaGround_Atmo)
            {
                case 0:
                    suit_Filter.get_suit_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_suit_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_suit_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }


        public void select_suit_IvaUnsafe(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.suit_Iva_Unsafe)
            {
                case 0:
                    suit_Filter.get_suit_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_suit_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_suit_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_suit_IvaSafe(out Texture2D texture, out Texture2D normalMap)
        {         
            switch (suit.suit_Iva_Safe)
            {
                case 0:
                    suit_Filter.get_suit_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_suit_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_suit_EvaSpace(out texture, out normalMap);
                    return;

                 default :
                    texture = null;
                    normalMap = null;
                    return;
            }       
        }

        public void select_helmet_EvaGround_NoAtmo(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.helmet_EvaGround_NoAtmo)
            {
                case 0:
                    suit_Filter.get_helmet_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_helmet_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_helmet_EvaSpace(out texture, out normalMap);
                    return;
                                    
                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_helmet_EvaSpace(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.helmet_EvaSpace)
            {
                case 0:
                    suit_Filter.get_helmet_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_helmet_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_helmet_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_helmet_EvaGround_Atmo(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.helmet_EvaGround_Atmo)
            {
                case 0:
                    suit_Filter.get_helmet_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_helmet_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_helmet_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_helmet_Iva_Unsafe(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.helmet_Iva_Unsafe)
            {
                case 0:
                    suit_Filter.get_helmet_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_helmet_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_helmet_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_helmet_Iva_Safe(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.helmet_Iva_Safe)
            {
                case 0:
                    suit_Filter.get_helmet_Iva(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_helmet_EvaGround(out texture, out normalMap);
                    return;
                case 2:
                    suit_Filter.get_helmet_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_visor_EvaGround_NoAtmo(out Texture2D texture, out Texture2D normalMap, out Color32 reflectionColor, out Color32 baseColor)
        {
            switch (suit.visor_EvaGround_NoAtmo)
            {
                case 0:
                    suit_Filter.get_visor_Iva(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 1:
                    suit_Filter.get_visor_EvaGround(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 2:
                    suit_Filter.get_visor_EvaSpace(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    Color32 col = new Color32(128, 128, 128, 255);
                    reflectionColor = col;
                    Color32 col2 = new Color32(255, 255, 255, 255);
                    baseColor = col2;
                    return;
            }
        }

        public void select_visor_EvaSpace(out Texture2D texture, out Texture2D normalMap, out Color32 reflectionColor, out Color32 baseColor)
        {
            switch (suit.visor_EvaSpace)
            {
                case 0:
                    suit_Filter.get_visor_Iva(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 1:
                    suit_Filter.get_visor_EvaGround(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 2:
                    suit_Filter.get_visor_EvaSpace(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    Color32 col = new Color32(128, 128, 128, 255);
                    reflectionColor = col;
                    Color32 col2 = new Color32(255, 255, 255, 255);
                    baseColor = col2;
                    return;
            }
        }

        public void select_visor_EvaGround_Atmo(out Texture2D texture, out Texture2D normalMap, out Color32 reflectionColor, out Color32 baseColor)
        {
            switch (suit.visor_EvaGround_Atmo)
            {
                case 0:
                    suit_Filter.get_visor_Iva(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 1:
                    suit_Filter.get_visor_EvaGround(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 2:
                    suit_Filter.get_visor_EvaSpace(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    Color32 col = new Color32(128, 128, 128, 255);
                    reflectionColor = col;
                    Color32 col2 = new Color32(255, 255, 255, 255);
                    baseColor = col2;
                    return;
            }
        }

        public void select_visor_Iva_Unsafe(out Texture2D texture, out Texture2D normalMap, out Color32 reflectionColor, out Color32 baseColor)
        {
            switch (suit.visor_Iva_Unsafe)
            {
                case 0:
                    suit_Filter.get_visor_Iva(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 1:
                    suit_Filter.get_visor_EvaGround(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 2:
                    suit_Filter.get_visor_EvaSpace(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    Color32 col = new Color32(128, 128, 128, 255);
                    reflectionColor = col;
                    Color32 col2 = new Color32(255, 255, 255, 255);
                    baseColor = col2;
                    return;
            }
        }

        public void select_visor_Iva_Safe(out Texture2D texture, out Texture2D normalMap, out Color32 reflectionColor, out Color32 baseColor)
        {
            switch (suit.visor_Iva_Safe)
            {
                case 0:
                    suit_Filter.get_visor_Iva(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 1:
                    suit_Filter.get_visor_EvaGround(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;
                case 2:
                    suit_Filter.get_visor_EvaSpace(out texture, out normalMap, out reflectionColor, out baseColor);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    Color32 col = new Color32(128, 128, 128, 255);
                    reflectionColor = col;
                    Color32 col2 = new Color32(255, 255, 255, 255);
                    baseColor = col2;
                    return;
            }
        }

        public void select_jetpack_EvaGround_NoAtmo(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.jetpack_EvaGround_NoAtmo)
            {                
                case 0:
                    suit_Filter.get_jetpack_EvaGround(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_jetpack_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_jetpack_EvaSpace(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.jetpack_EvaSpace)
            {
                case 0:
                    suit_Filter.get_jetpack_EvaGround(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_jetpack_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

        public void select_jetpack_EvaGround_Atmo(out Texture2D texture, out Texture2D normalMap)
        {
            switch (suit.jetpack_EvaGround_Atmo)
            {
                case 0:
                    suit_Filter.get_jetpack_EvaGround(out texture, out normalMap);
                    return;
                case 1:
                    suit_Filter.get_jetpack_EvaSpace(out texture, out normalMap);
                    return;

                default:
                    texture = null;
                    normalMap = null;
                    return;
            }
        }

    }
}
