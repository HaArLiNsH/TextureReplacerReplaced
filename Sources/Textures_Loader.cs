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
        internal static void LoadHeads(List<Head_Set> FullList, List<Head_Set>[] GenderList)
        {
            string[] gender = { "Male", "Female" };

            for (int i = 0; i < 2; i++)
            {
                foreach (string folder in Folders.HEADS)
                {
                    foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture.Where(t => t.texture != null && t.name.StartsWith((folder + gender[i] + "/"), StringComparison.Ordinal) && !t.name.EndsWith("NRM")))
                    {
                        string headName = texInfo.name.Substring((folder + gender[i] + "/").Length);
                        if (FullList.Any(t => t.headName == headName)) continue;

                        Texture2D texture = texInfo.texture;
                        texture.wrapMode = TextureWrapMode.Clamp;

                        Head_Set head = new Head_Set
                        {
                            headName = headName,
                            headTexture = texture,
                            isFemale = (i == 1)
                        };

                        Texture2D normal = GameDatabase.Instance.databaseTexture.FirstOrDefault(t => t.name == (texInfo.name + "NRM"))?.texture;

                        if (normal != null)
                        {
                            normal.wrapMode = TextureWrapMode.Clamp;
                            head.headNRM = normal;
                        }

                        FullList.Add(head);
                        GenderList[i].Add(head);
                    }
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
                    heads[0].headTexture = texture;
                    texture.wrapMode = TextureWrapMode.Clamp;
                }
                else if (originalName == "kerbalHeadNRM")
                {
                    heads[0].headNRM = texture;
                    texture.wrapMode = TextureWrapMode.Clamp;
                }
                else if (originalName == "kerbalGirl_06_BaseColor")
                {
                    heads[1].headTexture = texture;
                    texture.wrapMode = TextureWrapMode.Clamp;
                }
                else if (originalName == "kerbalGirl_06_BaseColorNRM")
                {
                    heads[1].headNRM = texture;
                    texture.wrapMode = TextureWrapMode.Clamp;
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