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

using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace TextureReplacer
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class TextureReplacer : MonoBehaviour
    {
        // Status.
        public static bool isInitialised = false;

        public static bool isLoaded = false;
        private static Dictionary<string, Shader> allShaders = new Dictionary<string, Shader>();

        public void Start()
        {
            Util.log("Started {0}", Assembly.GetExecutingAssembly().GetName().Version);

            DontDestroyOnLoad(this);

            isInitialised = false;
            isLoaded = false;
            LoadShaders();

            if (Reflections.instance != null)
                Reflections.instance.destroy();

            Loader.instance = new Loader();
            Replacer.instance = new Replacer();
            Reflections.instance = new Reflections();
            Personaliser.instance = new Personaliser();

            foreach (UrlDir.UrlConfig file in GameDatabase.Instance.GetConfigs("TextureReplacer"))
            {
                Loader.instance.readConfig(file.config);
                Replacer.instance.readConfig(file.config);
                Reflections.instance.readConfig(file.config);
                Personaliser.instance.readConfig(file.config);
            }

            Loader.instance.configure();
        }

        public void LateUpdate()
        {
            if (!isInitialised)
            {
                // Compress textures, generate mipmaps, convert DXT5 -> DXT1 if necessary etc.
                Loader.instance.processTextures();

                if (GameDatabase.Instance.IsReady())
                {
                    Loader.instance.initialise();
                    isInitialised = true;
                }
            }
            else if (PartLoader.Instance.IsReady())
            {
                Replacer.instance.load();
                Reflections.instance.load();
                Personaliser.instance.load();

                isLoaded = true;
                Destroy(this);
            }
        }

        /// <summary>
        /// Load all known shaders into the internal lookup table
        /// </summary>
        internal static void LoadShaders()
        {
            // force unity to load up any shader, even they are not used by any material on any asset
            Shader.WarmupAllShaders();
            foreach (var shader in Resources.FindObjectsOfTypeAll<Shader>())
            {
                if (!allShaders.ContainsKey(shader.name))
                {
                    allShaders.Add(shader.name, shader);
                    //Util.log("loaded shader: " + shader.name);
                }
            }
        }

        /// <summary>
        /// Drop in replacement for Shader.find(). Shader.find() cannot see not buildin shaders
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static Shader GetShader(string name)
        {
            if (allShaders.ContainsKey(name))
            {
                return allShaders[name];
            }
            else
            {
                Util.log("shader: " + name + " not found: ");
                return null;
            }
        }

    }
}