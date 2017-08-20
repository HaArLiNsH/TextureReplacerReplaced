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
    /// Contain the configuration data and textures of the head set
    /// <para>Here you will find all the textures for a head set and their functions </para>
    /// </summary>
    /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class Head_Set
    {
        /// <summary>
        /// the name of the head texture
        /// </summary>
        public string name;

        /// <summary>
        /// Is the head texture for a female model ?
        /// </summary>
        public bool isFemale;

        /// <summary>
        /// Is the head texture made to be used without the eyes 3D meshes ?
        /// </summary>
        //public bool isEyeless;

        /// <summary>
        /// Do we use the left eyeball 3d mesh?
        /// </summary>
        public int lvlToHide_Eye_Left;

        /// <summary>
        /// Do we use the right eyeball 3d mesh?
        /// </summary>
        public int lvlToHide_Eye_Right;

        /// <summary>
        /// Do we use the left pupil?
        /// </summary>
        public int lvlToHide_Pupil_Left;

        /// <summary>
        /// Do we use the right pupil ?
        /// </summary>
        public int lvlToHide_Pupil_Right;

        /// <summary>
        /// Do we use the up teeth ? 
        /// </summary>
        public int lvlToHide_TeethUp;

        /// <summary>
        /// Do we use the down teeth ?
        /// </summary>
        public int lvlToHide_TeethDown;

        /// <summary>
        /// Do we use the ponytail ? 
        /// </summary>
        public int lvlToHide_Ponytail;

        /// <summary>
        /// The head texture itself
        /// </summary>
        public Texture2D[] headTexture;

        /// <summary>
        /// The head normal map
        /// </summary>
        public Texture2D[] headTextureNRM;
        
        /// <summary>
        /// The ponytail texture
        /// </summary>
        public Texture2D[] ponytail;

        /// <summary>
        /// The ponytail normal map
        /// </summary>
        public Texture2D[] ponytailNRM;
        
        /// <summary>
        /// The eyeballLeft texture
        /// </summary>
        public Texture2D[] eyeball_Left;

        /// <summary>
        /// The eyeballLeftNRM normal map
        /// </summary>
        public Texture2D[] eyeball_LeftNRM;

        /// <summary>
        /// The eyeballRight texture
        /// </summary>
        public Texture2D[] eyeball_Right;

        /// <summary>
        /// The eyeballRightNRM normal map
        /// </summary>
        public Texture2D[] eyeball_RightNRM;

        /// <summary>
        /// The pupilLeft texture
        /// </summary>
        public Texture2D[] pupil_Left;

        /// <summary>
        /// The ponytailpupilLeftNRM normal map
        /// </summary>
        public Texture2D[] pupil_LeftNRM;

        /// <summary>
        /// The pupilRight texture
        /// </summary>
        public Texture2D[] pupil_Right;

        /// <summary>
        /// The pupilRightNRM normal map
        /// </summary>
        public Texture2D[] pupil_RightNRM;

        /// <summary>
        /// The Color32 of the Left eyeBall 
        /// </summary>
        public Color32 eyeballColor_Left = new Color32(255, 255, 255, 255);

        /// <summary>
        /// The Color32 of the Right eyeBall 
        /// </summary>
        public Color32 eyeballColor_Right = new Color32(255, 255, 255, 255);

        /// <summary>
        /// The Color32 of the Left Pupil 
        /// </summary>
        public Color32 pupilColor_Left = new Color32(0, 0, 0, 255);

        /// <summary>
        /// The Color32 of the Right Pupil 
        /// </summary>
        public Color32 pupilColor_Right = new Color32(0, 0, 0, 255);
        
        /// <summary>
        /// The tongue texture
        /// </summary>
        public Texture2D[] tongue;

        /// <summary>
        /// The ponytailtongueNRM normal map
        /// </summary>
        public Texture2D[] tongueNRM;

        /// <summary>
        /// The upTeethLeft texture
        /// </summary>
        public Texture2D[] teeth_Up_Left;

        /// <summary>
        /// The ponytailupTeethLeftNRM normal map
        /// </summary>
        public Texture2D[] teeth_Up_LeftNRM;

        /// <summary>
        /// The upTeethRight texture
        /// </summary>
        public Texture2D[] teeth_Up_Right;

        /// <summary>
        /// The ponytailupTeethRightNRM normal map
        /// </summary>
        public Texture2D[] teeth_Up_RightNRM;

        /// <summary>
        /// The downTeethLeft texture
        /// </summary>
        public Texture2D[] teeth_Down_Left;

        /// <summary>
        /// The downTeethLeftNRM normal map
        /// </summary>
        public Texture2D[] teeth_Down_LeftNRM;

        /// <summary>
        /// The downTeethRight texture 
        /// </summary>
        public Texture2D[] teeth_Down_Right;

        /// <summary>
        /// The downTeethRightNRM normal map
        /// </summary>
        public Texture2D[] teeth_Down_RightNRM;

        /// <summary>
        /// Used to get the texture for the head of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns></returns>
        public Texture2D get_headTexture(int level)
        {            
          return headTexture[level];
        }

        /// <summary>
        /// Used to get the normal map for the head of the kerbal
        /// </summary>
        /// <param name="level">The level of the kerbal</param>
        /// <returns></returns>
        public Texture2D get_headTextureNRM(int level)
        {
            return headTextureNRM[level];
        }

        /// <summary>
        /// Used to get the texture for the eyeball_Left of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_eyeball_LeftTexture(int level)
        {
            return eyeball_Left[level];
        }

        /// <summary>
        /// Used to get the normal map for the eyeball_Left of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_eyeball_LeftTextureNRM(int level)
        {
            return eyeball_LeftNRM[level];
        }

        /// <summary>
        /// Used to get the texture for the eyeball_Right of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_eyeball_RightTexture(int level)
        {
            return eyeball_Right[level];
        }

        /// <summary>
        /// Used to get the normal map for the eyeball_Right of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_eyeball_RightTextureNRM(int level)
        {
            return eyeball_RightNRM[level];
        }

        /// <summary>
        /// Used to get the texture for the pupil_Left of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_pupil_LeftTexture(int level)
        {
            return pupil_Left[level];
        }

        /// <summary>
        /// Used to get the normal map for the pupil_Left of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_pupil_LeftTextureNRM(int level)
        {
            return pupil_LeftNRM[level];
        }

        /// <summary>
        /// Used to get the texture for the pupil_Right of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_pupil_RightTexture(int level)
        {
            return pupil_Right[level];
        }

        /// <summary>
        /// Used to get the normal map for the pupil_Right of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_pupil_RightTextureNRM(int level)
        {
            return pupil_RightNRM[level];
        }

        /// <summary>
        /// Used to get the color the Left eyeBall 
        /// </summary>
        /// <returns></returns>
        public Color32 get_EyeballColor_Left()
        {
            return eyeballColor_Left;
        }

        /// <summary>
        /// Used to get the color the Right eyeBall 
        /// </summary>
        /// <returns></returns>
        public Color32 get_EyeballColor_Right()
        {
            return eyeballColor_Right;
        }

        /// <summary>
        /// Used to get the color the Left Pupil 
        /// </summary>
        /// <returns></returns>
        public Color32 get_PupilColor_Left()
        {
            return pupilColor_Left;
        }

        /// <summary>
        /// Used to get the color the Right Pupil 
        /// </summary>
        /// <returns></returns>
        public Color32 get_PupilColor_Right()
        {
            return pupilColor_Right;
        }       

        /// <summary>
        /// Used to get the texture for the teeth_Down_Left of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_teeth_Down_LeftTexture(int level)
        {
            return teeth_Down_Left[level];
        }

        /// <summary>
        /// Used to get the normal map for the teeth_Down_Left of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_teeth_Down_LeftTextureNRM(int level)
        {
            return teeth_Down_LeftNRM[level];
        }

        /// <summary>
        /// Used to get the texture for the teeth_Down_Right of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_teeth_Down_RightTexture(int level)
        {
            return teeth_Down_Right[level];
        }

        /// <summary>
        /// Used to get the normal map for the teeth_Down_Right of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_teeth_Down_RightTextureNRM(int level)
        {
            return teeth_Down_RightNRM[level];
        }

        /// <summary>
        /// Used to get the texture for the teeth_Up_Left of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_teeth_Up_LeftTexture(int level)
        {
            return teeth_Up_Left[level];
        }

        /// <summary>
        /// Used to get the normal map for the teeth_Up_Left of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_teeth_Up_LeftTextureNRM(int level)
        {
            return teeth_Up_LeftNRM[level];
        }

        /// <summary>
        /// Used to get the texture for the teeth_Up_Right of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_teeth_Up_RightTexture(int level)
        {
            return teeth_Up_Right[level];
        }

        /// <summary>
        /// Used to get the normal map for the teeth_Up_Right of the kerbal
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Texture2D get_teeth_Up_RightTextureNRM(int level)
        {
            return teeth_Up_RightNRM[level];
        }

        /// <summary>
        /// Used to set the color the Left eyeBall 
        /// </summary>
        /// <returns></returns>
        public void set_EyeballColor_Left(Color32 wantedColor)
        {
            eyeballColor_Left = wantedColor;
        }

        /// <summary>
        /// Used to set the color the Right eyeBall 
        /// </summary>
        /// <returns></returns>
        public void set_EyeballColor_Right(Color32 wantedColor)
        {
            eyeballColor_Right = wantedColor;
        }
        /// <summary>
        /// Used to set the color the Left Pupil 
        /// </summary>
        /// <returns></returns>
        public void set_PupilColor_Left(Color32 wantedColor)
        {
            pupilColor_Left = wantedColor;
        }

        /// <summary>
        /// Used to set the color the Right Pupil 
        /// </summary>
        /// <returns></returns>
        public void set_PupilColor_Right(Color32 wantedColor)
        {
            pupilColor_Right = wantedColor;
        }

        /// <summary>
        /// Search for the name of the texture, then set the good one in the suit set.
        /// <para>Related to <see cref="Personaliser.Head_Set"/> class. </para> 
        /// </summary>
        /// <param name="originalName">The name of the texture file (like KerbalMain.dds) 
        /// we want to save in the suit set.</param>
        /// <param name="texture">The texture2D we want to save in the suit set.</param>
        /// <returns>True if the texture is found and saved and false if not.</returns>
        public bool setTexture(string originalName, Texture2D texture)
        {
            int level;

            headTexture = headTexture ?? new Texture2D[6];
            headTextureNRM = headTextureNRM ?? new Texture2D[6];
            eyeball_Left = eyeball_Left ?? new Texture2D[6];
            eyeball_LeftNRM = eyeball_LeftNRM ?? new Texture2D[6];
            eyeball_Right = eyeball_Right ?? new Texture2D[6];
            eyeball_RightNRM = eyeball_RightNRM ?? new Texture2D[6];
            pupil_Left = pupil_Left ?? new Texture2D[6];
            pupil_LeftNRM = pupil_LeftNRM ?? new Texture2D[6];
            pupil_Right = pupil_Right ?? new Texture2D[6];
            pupil_RightNRM = pupil_RightNRM ?? new Texture2D[6];
            teeth_Down_Left = teeth_Down_Left ?? new Texture2D[6];
            teeth_Down_LeftNRM = teeth_Down_LeftNRM ?? new Texture2D[6];
            teeth_Down_Right = teeth_Down_Right ?? new Texture2D[6];
            teeth_Down_RightNRM = teeth_Down_RightNRM ?? new Texture2D[6];
            teeth_Up_Left = teeth_Up_Left ?? new Texture2D[6];
            teeth_Up_LeftNRM = teeth_Up_LeftNRM ?? new Texture2D[6];
            teeth_Up_Right = teeth_Up_Right ?? new Texture2D[6];
            teeth_Up_RightNRM = teeth_Up_RightNRM ?? new Texture2D[6];


            switch (originalName)
            {
                case "kerbalHead":
                case "kerbalGirl_06_BaseColor":
                case "HeadTexture0":
                case "HeadTexture1":
                case "HeadTexture2":
                case "HeadTexture3":
                case "HeadTexture4":
                case "HeadTexture5":
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
                        headTexture[i] = texture;
                    return true;

                case "kerbalHeadNRM":
                case "kerbalGirl_06_BaseColorNRM":
                case "HeadTextureNRM0":
                case "HeadTextureNRM1":
                case "HeadTextureNRM2":
                case "HeadTextureNRM3":
                case "HeadTextureNRM4":
                case "HeadTextureNRM5":
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
                        headTextureNRM[i] = texture;
                    return true;

                case "Eyeball_Left0":
                case "Eyeball_Left1":
                case "Eyeball_Left2":
                case "Eyeball_Left3":
                case "Eyeball_Left4":
                case "Eyeball_Left5":
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
                        eyeball_Left[i] = texture;
                    return true;

                case "Eyeball_LeftNRM0":
                case "Eyeball_LeftNRM1":
                case "Eyeball_LeftNRM2":
                case "Eyeball_LeftNRM3":
                case "Eyeball_LeftNRM4":
                case "Eyeball_LeftNRM5":
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
                        eyeball_LeftNRM[i] = texture;
                    return true;

                case "Eyeball_Right0":
                case "Eyeball_Right1":
                case "Eyeball_Right2":
                case "Eyeball_Right3":
                case "Eyeball_Right4":
                case "Eyeball_Right5":
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
                        eyeball_Right[i] = texture;
                    return true;

                case "Eyeball_RightNRM0":
                case "Eyeball_RightNRM1":
                case "Eyeball_RightNRM2":
                case "Eyeball_RightNRM3":
                case "Eyeball_RightNRM4":
                case "Eyeball_RightNRM5":
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
                        eyeball_RightNRM[i] = texture;
                    return true;

                case "Pupil_Left_Male_Default":
                case "Pupil_Left_Female_Default":
                case "Pupil_Left0":
                case "Pupil_Left1":
                case "Pupil_Left2":
                case "Pupil_Left3":
                case "Pupil_Left4":
                case "Pupil_Left5":
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
                        pupil_Left[i] = texture;
                    return true;

                case "Pupil_LeftNRM0":
                case "Pupil_LeftNRM1":
                case "Pupil_LeftNRM2":
                case "Pupil_LeftNRM3":
                case "Pupil_LeftNRM4":
                case "Pupil_LeftNRM5":
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
                        pupil_LeftNRM[i] = texture;
                    return true;

                case "Pupil_Right_Male_Default":
                case "Pupil_Right_Female_Default":
                case "Pupil_Right0":
                case "Pupil_Right1":
                case "Pupil_Right2":
                case "Pupil_Right3":
                case "Pupil_Right4":
                case "Pupil_Right5":
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
                        pupil_Right[i] = texture;
                    return true;

                case "Pupil_RightNRM0":
                case "Pupil_RightNRM1":
                case "Pupil_RightNRM2":
                case "Pupil_RightNRM3":
                case "Pupil_RightNRM4":
                case "Pupil_RightNRM5":
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
                        pupil_RightNRM[i] = texture;
                    return true;

                case "Teeth_Down_Left0":
                case "Teeth_Down_Left1":
                case "Teeth_Down_Left2":
                case "Teeth_Down_Left3":
                case "Teeth_Down_Left4":
                case "Teeth_Down_Left5":
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
                        teeth_Down_Left[i] = texture;
                    return true;

                case "Teeth_Down_LeftNRM0":
                case "Teeth_Down_LeftNRM1":
                case "Teeth_Down_LeftNRM2":
                case "Teeth_Down_LeftNRM3":
                case "Teeth_Down_LeftNRM4":
                case "Teeth_Down_LeftNRM5":
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
                        teeth_Down_LeftNRM[i] = texture;
                    return true;

                case "Teeth_Down_Right0":
                case "Teeth_Down_Right1":
                case "Teeth_Down_Right2":
                case "Teeth_Down_Right3":
                case "Teeth_Down_Right4":
                case "Teeth_Down_Right5":
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
                        teeth_Down_Right[i] = texture;
                    return true;

                case "Teeth_Down_RightNRM0":
                case "Teeth_Down_RightNRM1":
                case "Teeth_Down_RightNRM2":
                case "Teeth_Down_RightNRM3":
                case "Teeth_Down_RightNRM4":
                case "Teeth_Down_RightNRM5":
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
                        teeth_Down_RightNRM[i] = texture;
                    return true;

                case "Teeth_Up_Left0":
                case "Teeth_Up_Left1":
                case "Teeth_Up_Left2":
                case "Teeth_Up_Left3":
                case "Teeth_Up_Left4":
                case "Teeth_Up_Left5":
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
                        teeth_Up_Left[i] = texture;
                    return true;

                case "Teeth_Up_LeftNRM0":
                case "Teeth_Up_LeftNRM1":
                case "Teeth_Up_LeftNRM2":
                case "Teeth_Up_LeftNRM3":
                case "Teeth_Up_LeftNRM4":
                case "Teeth_Up_LeftNRM5":
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
                        teeth_Up_LeftNRM[i] = texture;
                    return true;

                case "Teeth_Up_Right0":
                case "Teeth_Up_Right1":
                case "Teeth_Up_Right2":
                case "Teeth_Up_Right3":
                case "Teeth_Up_Right4":
                case "Teeth_Up_Right5":
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
                        teeth_Up_Right[i] = texture;
                    return true;

                case "Teeth_Up_RightNRM0":
                case "Teeth_Up_RightNRM1":
                case "Teeth_Up_RightNRM2":
                case "Teeth_Up_RightNRM3":
                case "Teeth_Up_RightNRM4":
                case "Teeth_Up_RightNRM5":
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
                        teeth_Up_RightNRM[i] = texture;
                    return true;
                    

                default:
                    return false;
            }            
        }
    }
}