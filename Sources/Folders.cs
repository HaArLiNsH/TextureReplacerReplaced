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

//#define TR_LOG_HIERARCHY

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TextureReplacerReplaced
{
    /// <summary>
    /// Folders class. Here you find all the folders needed by TRR
    /// </summary>
    internal static class Folders
    {
        /// <summary>
        /// name of the install directory of TextureReplacerReplaced
        /// </summary>
        static string DIR = "TextureReplacerReplaced/";

        /// <summary>
        /// path of the Default folder (from <see cref="DIR"/> + Default/ )
        /// </summary>
        internal static string DIR_DEFAULT = DIR + "Default/";

        /// <summary>
        /// path of the EnvMap folder (from <see cref="DIR"/> + EnvMap/ )
        /// </summary>
        internal static string DIR_ENVMAP = DIR + "EnvMap/";

        /// <summary>
        /// path of the Heads folder (from <see cref="DIR"/> + Heads/ )
        /// </summary>
        static string DIR_HEADS = DIR + "Heads/";

        /// <summary>
        /// path of the Suits folder (from <see cref="DIR"/> + Suits/ )
        /// </summary>
        static string DIR_SUITS = DIR + "Suits/";


        /// <summary>
        /// The list of paths for the "Default/" folders
        /// </summary>
        internal static readonly List<string> DEFAULT = new List<string> { DIR_DEFAULT };

        /// <summary>
        /// The list of paths for the "EnvMap/" folders
        /// </summary>
        internal static readonly List<string> ENVMAP = new List<string> { DIR_ENVMAP };

        /// <summary>
        /// The list of paths for the "Heads/" folders
        /// </summary>
        internal static readonly List<string> HEADS = new List<string> { DIR_HEADS };

        /// <summary>
        /// The list of paths for the "Suits/" folders
        /// </summary>
        internal static readonly List<string> SUITS = new List<string> { DIR_SUITS };

        /// <summary>
        /// The list of paths for the "KeepLoaded/" folders
        /// </summary>
        internal static readonly List<string> KEEPLOADED = new List<string>(ENVMAP);

        /// <summary>
        /// Load the paths to all folders used in TRR
        /// </summary>
        internal static void LoadFolders()
        {
            UnityEngine.Debug.Log("SigmaLog: Loading Folders...");
            foreach (ConfigNode TRR_NODE in TextureReplacerReplaced.SETTINGS.Where(n => n.HasNode("Folders")))
            {
                UnityEngine.Debug.Log("SigmaLog: Found NODE: " + TRR_NODE.name);

                UnityEngine.Debug.Log("SigmaLog: Loading Default Folders...");
                DEFAULT.AddRange(TRR_NODE.GetNode("Folders").GetValues("Default"));
                UnityEngine.Debug.Log("SigmaLog: Loading EnvMap Folders...");
                ENVMAP.AddRange(TRR_NODE.GetNode("Folders").GetValues("EnvMap"));
                UnityEngine.Debug.Log("SigmaLog: Loading Heads Folders...");
                HEADS.AddRange(TRR_NODE.GetNode("Folders").GetValues("Heads"));
                UnityEngine.Debug.Log("SigmaLog: Loading Suits Folders...");
                SUITS.AddRange(TRR_NODE.GetNode("Folders").GetValues("Suits"));
                UnityEngine.Debug.Log("SigmaLog: Loading KeepLoaded Folders...");
                KEEPLOADED.AddRange(TRR_NODE.GetNode("Folders").GetValues("KeepLoaded"));
            }
            // DEBUG
            UnityEngine.Debug.Log("SigmaLog: List of Loaded Folders:");
            UnityEngine.Debug.Log("SigmaLog: >>> DEFAULT <<<");
            foreach (string s in DEFAULT)
            {
                UnityEngine.Debug.Log("SigmaLog:     > " + s);
            }
            UnityEngine.Debug.Log("SigmaLog: >>> ENVMAP <<<");
            foreach (string s in ENVMAP)
            {
                UnityEngine.Debug.Log("SigmaLog:     > " + s);
            }
            UnityEngine.Debug.Log("SigmaLog: >>> HEADS <<<");
            foreach (string s in HEADS)
            {
                UnityEngine.Debug.Log("SigmaLog:     > " + s);
            }
            UnityEngine.Debug.Log("SigmaLog: >>> SUITS <<<");
            foreach (string s in SUITS)
            {
                UnityEngine.Debug.Log("SigmaLog:     > " + s);
            }
            UnityEngine.Debug.Log("SigmaLog: >>> KEEPLOADED <<<");
            foreach (string s in KEEPLOADED)
            {
                UnityEngine.Debug.Log("SigmaLog:     > " + s);
            }
            UnityEngine.Debug.Log("SigmaLog: End Debug.");
        }
    }

    /// <summary>
    /// Textures class. Here you find some lists of Textures needed by TRR
    /// </summary>
    internal static class Textures
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
        internal static void LoadHeads(List<Personaliser.Head> FullList, List<Personaliser.Head>[] GenderList)
        {
            string[] gender = { "Male", "Female" };

            for (int i = 0; i < 2; i++)
            {
                foreach (string folder in Folders.HEADS)
                {
                    foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture.Where(t => t.texture != null && t.name.StartsWith(folder + gender[i] + "/", StringComparison.Ordinal) && !t.name.EndsWith("NRM")))
                    {
                        string headName = texInfo.name.Substring(folder.Length);
                        if (!FullList.Any(t => t.name == headName)) continue;

                        Texture2D texture = texInfo.texture;
                        Texture2D normal = GameDatabase.Instance.databaseTexture?.FirstOrDefault(t => t.texture != null && t.name == (texInfo.name + "NRM"))?.texture;
                        
                        texture.wrapMode = TextureWrapMode.Clamp;
                        normal.wrapMode = TextureWrapMode.Clamp;

                        Personaliser.Head head = new Personaliser.Head { name = headName, head = texture, isFemale = (i == 1) };
                        if (normal != null) head.headNRM = normal;

                        FullList.Add(head);
                        GenderList[i].Add(head);
                    }
                }
            }
        }

        /// <summary>
        /// Loads the Default heads
        /// </summary>
        internal static void DefaultHeads(Personaliser.Head[] heads)
        {
            foreach (KeyValuePair<Texture2D, string> texInfo in DEFAULT())
            {
                Texture2D texture = texInfo.Key;
                string originalName = texInfo.Value;

                if (originalName == "kerbalHead")
                {
                    heads[0].head = texture;
                    texture.wrapMode = TextureWrapMode.Clamp;
                }
                else if (originalName == "kerbalHeadNRM")
                {
                    heads[0].headNRM = texture;
                    texture.wrapMode = TextureWrapMode.Clamp;
                }
                else if (originalName == "kerbalGirl_06_BaseColor")
                {
                    heads[1].head = texture;
                    texture.wrapMode = TextureWrapMode.Clamp;
                }
                else if (originalName == "kerbalGirl_06_BaseColorNRM")
                {
                    heads[1].headNRM = texture;
                    texture.wrapMode = TextureWrapMode.Clamp;
                }
            }
        }
    }
}
