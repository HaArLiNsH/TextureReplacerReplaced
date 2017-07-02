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
using System.Linq;
using System.Collections.Generic;

namespace TextureReplacerReplaced
{
    // delay the initialization to the MainMenu, so we have everything (ModuleManager, GameDatabase) loaded
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class TextureReplacerReplaced : MonoBehaviour
    {
        /// <summary>
        /// Status of the loading
        /// </summary>
        public static bool isLoaded = false;

        /// <summary>
        /// shader database
        /// </summary>
        private static Dictionary<string, Shader> allShaders = new Dictionary<string, Shader>();

        /// <summary>
        /// User Settings
        /// </summary>
        internal static ConfigNode[] SETTINGS = new ConfigNode[] { };

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Unity MoneBehaviour Awake call, this is when all the modules wake up and get loaded
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void Awake()
        {
            DontDestroyOnLoad(this);

            // initialize the shader database
            LoadShaders();

            // this will never happen, but I leave this here for now.
            if (Reflections.instance != null)
                Reflections.instance.destroy();

            Util.log("Started V{0}", Assembly.GetExecutingAssembly().GetName().Version);

            Loader.instance = new Loader();
            Replacer.instance = new Replacer();
            Reflections.instance = new Reflections();
            Personaliser.instance = new Personaliser();

            SETTINGS = GameDatabase.Instance.GetConfigNodes("TextureReplacerReplaced");
            Debug.Log("SigmaLog: SETTINGS NODES = " + SETTINGS.Length);

            Folders.LoadFolders();

            /*
            foreach (UrlDir.UrlConfig file in )
            {
                Loader.instance.readConfig(file.config);
                Replacer.instance.readConfig(file.config);
                Reflections.instance.readConfig(file.config);
                Personaliser.instance.readConfig(file.config);
            }*/

            Loader.instance.configure();
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Unity MoneBehaviour start call, all module assemblies are present 
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        void Start()
        {

            Loader.instance.processTextures();

            Loader.instance.initialise();

            Replacer.instance.load();
            Reflections.instance.load();
            Personaliser.instance.load();

            isLoaded = true;

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Load all shaders into the system and fill our shader database.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        internal void LoadShaders()
        {
            // the most important call: Loads all shaders into the memory, even, when they are not used by any GameObject
            //Shader.WarmupAllShaders();
            foreach (var shader in Resources.FindObjectsOfTypeAll<Shader>())
            {
                if (!allShaders.ContainsKey(shader.name))
                {
                    allShaders.Add(shader.name, shader);
                    Util.log("Loaded shader: " + shader.name);
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Replacement for Shader.Find() function, as we return also shaders, that are through KSP asset bundles (with autoload on)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        internal static Shader GetShader(string name)
        {
            if (allShaders.ContainsKey(name))
            {
                return allShaders[name];
            }
            else
            {
                Util.log("Error: Shader not found: " + name);
                // return the error Shader, if we have one
                if (allShaders.ContainsKey("Hidden/InternalErrorShader"))
                {
                    return allShaders["Hidden/InternalErrorShader"];
                } else
                {
                    return null;
                }
            }
        }

    }
}