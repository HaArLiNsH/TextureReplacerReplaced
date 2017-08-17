/*
 * Copyright © 2017 HaArLiNsH, Sigma88 
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

    /// <summary>
    /// Textures class. Here you find some lists of Textures needed by TRR
    /// </summary>
    internal static class Textures_Loader
    {
        /// <summary>
        /// Generates and returns the required Dictionary
        /// </summary>
        static Dictionary<Texture2D, string> Load(Dictionary<Texture2D, string> dictionary, List<string> folders)
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<Texture2D, string>();

                foreach (string folder in folders)
                {
                    foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture.Where(t => t.name.StartsWith(folder, StringComparison.Ordinal)))
                    {
                        if (texInfo.texture != null && !dictionary.ContainsKey(texInfo.texture))
                            dictionary.Add(texInfo.texture, texInfo.name.Substring(folder.Length));
                    }
                }
            }
            return dictionary;
        }


        static Dictionary<Texture2D, string> DefaultDictionary = null;
        /// <summary>
        /// Dictionary of Texture2D from Default folders with their originalName
        /// </summary>
        internal static Dictionary<Texture2D, string> DEFAULT()
        {
            return Load(DefaultDictionary, Folders.DEFAULT);
        }


        static Dictionary<Texture2D, string> EnvMapDictionary = null;
        /// <summary>
        /// Dictionary of Texture2D from Default folders with their originalName
        /// </summary>
        internal static Dictionary<Texture2D, string> ENVMAP()
        {
            return Load(EnvMapDictionary, Folders.ENVMAP);
        }

        /// <summary>
        /// Loads all Heads into a non gender-specific list and in two gender-specific lists
        /// </summary>
        /// <param name="FullList"></param>
        /// <param name="GenderList"></param>
        /// <param name="DefaultHead"></param>
        internal static void LoadHeads(List<Head_Set> FullList, List<Head_Set>[] GenderList, Head_Set[] DefaultHead)
        {
            string[] gender = { "Male", "Female" };
            var headDirs = new Dictionary<string, int>();
            //string lastTextureName = "";
                      
            for (int i = 0; i < 2; i++)
            {
               // Util.log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                //Util.log("i = : {0}", i);
                foreach (string HEADS_Folder in Folders.HEADS)
                {
                    //Util.log("++++++++++++++++++++++++++++++++++++++++++++++");
                   // Util.log("HEADS_Folder = : {0}", HEADS_Folder);

                    string genderFolder = (HEADS_Folder + gender[i] + "/");
                    //Util.log("genderFolder = : {0}", genderFolder);

                    foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture.Where(t => t.name.StartsWith(genderFolder)))
                    {
                        //Util.log("****************************");
                        Texture2D texture = texInfo.texture;
                        //Util.log("Texture full path = : {0}", texture.name);

                        int lastSlash_full = texture.name.LastIndexOf('/');
                        //Util.log("lastSlash_full = : {0}", lastSlash_full);

                        string headSetFolder_full = texInfo.name.Substring(genderFolder.Length);
                        //Util.log("headSetFolder_full = : {0}", headSetFolder_full);

                        int lastSlash_headSetFolder = headSetFolder_full.LastIndexOf('/');
                        //Util.log("lastSlash_headSetFolder = : {0}", lastSlash_headSetFolder);


                        if (lastSlash_headSetFolder < 1)
                        {
                            Util.log("Head texture should be inside a subdirectory: {0}", texture.name);
                            continue;
                        }
                        
                        string TextureFileName = texture.name.Substring(lastSlash_full + 1);                        

                        string headSetFolder = headSetFolder_full.Remove(headSetFolder_full.Length - (TextureFileName.Length+1));
                        //Util.log("headSetFolder = : {0}", headSetFolder);
                        //Util.log("TextureFileName = : {0}", TextureFileName);

                        if (texture == null)
                        {
                            Util.log("texture : {0} == null!! ", TextureFileName);
                            continue;
                        }
                        if (!texture.name.StartsWith(HEADS_Folder, StringComparison.Ordinal))
                        {
                            Util.log("texture : {0} DON'T start with {1}", TextureFileName, HEADS_Folder);
                            continue;
                        }
                        else
                        {
                            texture.wrapMode = TextureWrapMode.Clamp;
                            
                            int index;

                            if (!headDirs.TryGetValue(headSetFolder, out index))
                            {                                
                                index = FullList.Count;
                                
                                Head_Set head = new Head_Set
                                {
                                    headSetName = headSetFolder,
                                    isFemale = (i == 1)
                                };

                                FullList.Add(head);                                
                                headDirs.Add(headSetFolder, index);                               
                                GenderList[i].Add(head);
                                Util.log("HeadSet added : {0}", headSetFolder);
                            }                            
                            Head_Set headSet = FullList[index];
                            if (!headSet.setTexture(TextureFileName, texture))
                                //Util.log("Texture {0} properly loaded in {1}", TextureFileName, texture.name);
                            //else
                                Util.log("Unknown head texture name \"{0}\": {1}", TextureFileName, texture.name);

                        }  
                    }
                }
            }
            foreach (string defaultFolders in Folders.DEFAULT)
            {
                foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture)
                {
                    Texture2D texture = texInfo.texture;
                    if (texture == null || !texture.name.StartsWith(defaultFolders, StringComparison.Ordinal))
                        continue;

                    //Util.log("starting loading default head");

                    if (texture.name.StartsWith(defaultFolders, StringComparison.Ordinal))
                    {
                        int lastSlash = texture.name.LastIndexOf('/');
                        string originalName = texture.name.Substring(lastSlash + 1);
                        //Util.log("default folder = " +defaultFolders);
                        //Util.log("DEFAULT : texture name \"{0}\": {1}", originalName, texture.name);

                        if (originalName == "kerbalHead")
                        {
                            DefaultHead[0].setTexture(originalName, texture);
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }

                        else if (originalName == "kerbalHeadkerbalHeadNRM")
                        {
                            DefaultHead[0].setTexture(originalName, texture);
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }

                        /*else if (originalName == "Pupil_Left_Male_Default")
                        {
                            DefaultHead[0].setTexture(originalName, texture);
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }

                        else if (originalName == "Pupil_Right_Male_Default")
                        {
                            DefaultHead[0].setTexture(originalName, texture);
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }*/

                        else if (originalName == "kerbalGirl_06_BaseColor")
                        {
                            DefaultHead[1].setTexture(originalName, texture);
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }

                        else if (originalName == "kerbalGirl_06_BaseColorNRM")
                        {
                            DefaultHead[1].setTexture(originalName, texture);
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }

                       /* else if (originalName == "Pupil_Left_Female_Default")
                        {
                            DefaultHead[1].setTexture(originalName, texture);
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }

                        else if (originalName == "Pupil_Right_Female_Default")
                        {
                            DefaultHead[1].setTexture(originalName, texture);
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }*/

                    }
                    //lastTextureName = texture.name;
                }
            }



        }

        /// <summary>
        /// Loads the Default heads
        /// </summary>
        internal static void DefaultHeads(Head_Set[] heads)
        {
                foreach (KeyValuePair<Texture2D, string> texInfo in DEFAULT())
            {
                Texture2D texture = texInfo.Key;
                string originalName = texInfo.Value;

                if (originalName == "kerbalHead")
                {
                    for (int i = 0 ; i < 6; ++i)
                    {
                        heads[0].headTexture[i] = texture;
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }        
                }
                else if (originalName == "kerbalHeadNRM")
                {
                    for (int i = 0; i < 6; ++i)
                    {
                        heads[0].headTextureNRM[i] = texture;
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                }
                else if (originalName == "kerbalGirl_06_BaseColor")
                {
                    for (int i = 0; i < 6; ++i)
                    {
                        heads[1].headTexture[i] = texture;
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                }
                else if (originalName == "kerbalGirl_06_BaseColorNRM")
                {
                    for (int i = 0; i < 6; ++i)
                    {
                        heads[1].headTextureNRM[i] = texture;
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suitsList"></param>
        /// <param name="defaultSuit"></param>
        internal static void LoadSuits(List<Suit_Set> suitsList, Suit_Set defaultSuit)
        {
            var suitDirs = new Dictionary<string, int>();
            string lastTextureName = "";
            foreach (string suitSetFolder in Folders.SUITS)
            {
                foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture)
                {
                    Texture2D texture = texInfo.texture;
                    if (texture == null || !texture.name.StartsWith(suitSetFolder, StringComparison.Ordinal))
                        continue;

                    // Add a suit texture.
                    if (texture.name.StartsWith(suitSetFolder, StringComparison.Ordinal))
                    {
                        texture.wrapMode = TextureWrapMode.Clamp;

                        int lastSlash = texture.name.LastIndexOf('/');
                        int dirNameLength = lastSlash - suitSetFolder.Length;
                        string originalName = texture.name.Substring(lastSlash + 1);

                        if (dirNameLength < 1)
                        {
                            Util.log("Suit texture should be inside a subdirectory: {0}", texture.name);
                        }
                        else
                        {
                            string dirName = texture.name.Substring(suitSetFolder.Length, dirNameLength);

                            int index;
                            if (!suitDirs.TryGetValue(dirName, out index))
                            {
                                index = suitsList.Count;
                                suitsList.Add(new Suit_Set { suitSetName = dirName });
                                suitDirs.Add(dirName, index);
                            }

                            Suit_Set suit = suitsList[index];
                            if (!suit.setTexture(originalName, texture))
                                Util.log("Unknown suit texture name \"{0}\": {1}", originalName, texture.name);
                        }
                    }

                    lastTextureName = texture.name;
                }
            }
            foreach (string defaultFolders in Folders.DEFAULT)
            {
                foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture)
                {
                    Texture2D texture = texInfo.texture;
                    if (texture == null || !texture.name.StartsWith(defaultFolders, StringComparison.Ordinal))
                        continue;

                    if (texture.name.StartsWith(defaultFolders, StringComparison.Ordinal))
                    {
                        int lastSlash = texture.name.LastIndexOf('/');
                        string originalName = texture.name.Substring(lastSlash + 1);
                        //Util.log("default folder = " +defaultFolders);
                        //Util.log("DEFAULT : texture name \"{0}\": {1}", originalName, texture.name);
                        if (defaultSuit.setTexture(originalName, texture) || originalName == "kerbalMain")
                        {
                            texture.wrapMode = TextureWrapMode.Clamp;
                        }
                    }
                    lastTextureName = texture.name;
                }                
            }

        }

    }
}