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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TextureReplacerReplaced
{
    /// <summary>
    /// This class handle shaders and the reflections on visors and windows
    /// </summary>
    internal class Reflections
    {
        /// <summary>
        /// the different type of reflection we can use: NONE, STATIC, REAL
        /// </summary>
        public enum Type
        {
            NONE,
            STATIC,
            REAL
        }

        /// <summary>
        /// Reflective shader mapping.
        /// </summary>
        private static Dictionary<string, string> shaderMappingConfig = new Dictionary<string, string> {
            { "KSP/Diffuse", "Reflective/Bumped Diffuse" },
            { "KSP/Specular", "Reflective/Bumped Diffuse" },
            { "KSP/Bumped", "Reflective/Bumped Diffuse" },
            { "KSP/Bumped Specular", "Reflective/Bumped Diffuse" },
            { "KSP/Alpha/Translucent", "KSP/TRR/Visor" },
            { "KSP/Alpha/Translucent Specular", "KSP/TRR/Visor" }
        };

        /// <summary>
        /// The cull distances for the different parts
        /// <para>Render layers: </para>
        /// <para>0 - parts </para>
        /// <para>1 - RCS jets </para>
        /// <para>5 - engine exhaust </para>
        /// <para>9 - sky/atmosphere </para>
        /// <para>10 - scaled space bodies </para>
        /// <para>15 - buildings, terrain </para>
        /// <para>18 - skybox </para>
        /// <para>23 - sun </para>        
        /// </summary>
        private static readonly float[] CULL_DISTANCES = {
            1000.0f, 100.0f, 0.0f, 0.0f, 0.0f, 100.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
        };

        /// <summary>
        /// The basic transparent Specular Shader found in unity
        /// </summary>
        private static readonly Shader transparentSpecularShader = Shader.Find("Transparent/Specular");

        /// <summary>
        /// The list of all the shaders we use
        /// </summary>
        private readonly Dictionary<Shader, Shader> shaderMappings = new Dictionary<Shader, Shader>();

        /// <summary>
        /// Reflective shader material.
        /// </summary>
        private Material shaderMaterial = null;

        /// <summary>
        /// Reflection camera.
        /// </summary>
        private static Camera camera = null;

        /// <summary>
        /// Environment map textures.
        /// </summary>
        private Cubemap staticEnvMap = null;

        /// <summary>
        /// Reflection type.
        /// </summary>
        public Type reflectionType = Type.REAL;

        /// <summary>
        /// Real reflection resolution.
        /// </summary>
        private static int reflectionResolution = 128;

        /// <summary>
        /// Interval in frames for updating environment map faces.
        /// </summary>
        private static int reflectionInterval = 2;

        /// <summary>
        /// Reflection colour of the visor.
        /// </summary>
        private static Color visorReflectionColour = new Color(0.5f, 0.5f, 0.5f);

        /// <summary>
        /// Visor reflection feature.
        /// </summary>
        public bool isVisorReflectionEnabled = true;

        /// <summary>
        /// Print names of meshes and their shaders in parts with TRReflection module.
        /// </summary>
        public bool logReflectiveMeshes = true;

        /// <summary>
        /// Reflective shader.
        /// </summary>
        private Shader visorShader = null;

        /// <summary>
        /// Instance.
        /// </summary>
        public static Reflections instance = null;

        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// nooed some comments 
        /// a class within an class might need some rework. Maybe make it an own monobehavior which is attached to the modules as components
        /// remove this comment with something more explaining why this is good
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public class Script
        {
            /// <summary>
            /// List of all created reflection scripts.
            /// </summary>
            private static readonly List<Script> scripts = new List<Script>();

            private static int currentScript = 0;

            private readonly Cubemap envMap;
            private readonly Transform transform;
            private readonly bool isEva;
            private readonly int interval;
            private int counter;
            private int currentFace;
            private bool isActive = true;

            /// ************************************************************************************
            /// <summary>
            /// 
            /// </summary>
            /// <param name="part"></param>
            /// <param name="updateInterval"></param>
            /// ************************************************************************************
            public Script(Part part, int updateInterval)
            {
                envMap = new Cubemap(reflectionResolution, TextureFormat.ARGB32, true);
                envMap.hideFlags = HideFlags.HideAndDontSave;
                envMap.wrapMode = TextureWrapMode.Clamp;

                transform = part.transform;
                isEva = part.GetComponent<KerbalEVA>() != null;

                if (isEva)
                {
                    transform = transform.Find("model01");

                    SkinnedMeshRenderer visor = transform.GetComponentsInChildren<SkinnedMeshRenderer>(true)
                      .FirstOrDefault(m => m.name == "visor");

                    if (visor != null)
                    {
                        Material material = visor.material;

                        material.shader = instance.visorShader;
                        material.SetTexture(Util.CUBE_PROPERTY, envMap);
                        material.SetColor(Util.REFLECT_COLOR_PROPERTY, visorReflectionColour);
                    }

                    // TODO ICI ! 
                   /* SkinnedMeshRenderer eyeballLeft = transform.GetComponentsInChildren<SkinnedMeshRenderer>(true)
                      .FirstOrDefault(m => m.name == "eyeballLeft");

                    if (eyeballLeft != null)
                    {
                        Material material = eyeballLeft.material;

                        material.shader = instance.visorShader;
                        material.SetTexture(Util.CUBE_PROPERTY, envMap);
                        material.SetColor(Util.REFLECT_COLOR_PROPERTY, visorReflectionColour);
                    }*/
                }

                interval = updateInterval;
                counter = Util.random.Next(updateInterval);
                currentFace = Util.random.Next(6);

                ensureCamera();
                update(true);

                scripts.Add(this);
            }

            /// ************************************************************************************
            /// <summary>
            /// 
            /// </summary>
            /// <param name="material"></param>
            /// <param name="shader"></param>
            /// <param name="reflectionColour"></param>
            /// <returns></returns>
            /// ************************************************************************************
            public bool apply(Material material, Shader shader, Color reflectionColour)
            {
                Shader reflectiveShader = shader ?? instance.toReflective(material.shader);

                if (reflectiveShader != null)
                {
                    material.shader = reflectiveShader;
                    material.SetTexture(Util.CUBE_PROPERTY, envMap);
                    material.SetColor(Util.REFLECT_COLOR_PROPERTY, reflectionColour);
                    return true;
                }
                return false;
            }

            /// ************************************************************************************
            /// <summary>
            /// 
            /// </summary>
            /// ************************************************************************************
            public void destroy()
            {
                scripts.Remove(this);

                Object.DestroyImmediate(envMap);
            }

            /// ************************************************************************************
            /// <summary>
            /// 
            /// </summary>
            /// <param name="force"></param>
            /// ************************************************************************************
            private void update(bool force)
            {
                int faceMask = force ? 0x3f : 1 << currentFace;

                // Hide all meshes of the current part.
                Renderer[] meshes = transform.GetComponentsInChildren<Renderer>();
                bool[] meshStates = new bool[meshes.Length];

                for (int i = 0; i < meshes.Length; ++i)
                {
                    meshStates[i] = meshes[i].enabled;
                    meshes[i].enabled = false;
                }

                // Skybox.
                camera.transform.position = GalaxyCubeControl.Instance.transform.position;
                camera.farClipPlane = 100.0f;
                camera.cullingMask = 1 << 18;
                camera.RenderToCubemap(envMap, faceMask);

                // Scaled space.
                camera.transform.position = ScaledSpace.Instance.transform.position;
                camera.farClipPlane = 3.0e7f;
                camera.cullingMask = (1 << 10) | (1 << 23);
                camera.RenderToCubemap(envMap, faceMask);

                // Scene.
                camera.transform.position = isEva ? transform.position + 0.4f * transform.up : transform.position;
                camera.farClipPlane = 60000.0f;
                camera.cullingMask = (1 << 0) | (1 << 1) | (1 << 5) | (1 << 15);
                camera.RenderToCubemap(envMap, faceMask);

                // Restore mesh visibility.
                for (int i = 0; i < meshes.Length; ++i)
                    meshes[i].enabled = meshStates[i];

                currentFace = (currentFace + 1) % 6;
            }

            /// ************************************************************************************
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// ************************************************************************************
            public void setActive(bool value)
            {
                if (!isActive && value)
                    update(true);

                isActive = value;
            }

            /// ************************************************************************************
            /// <summary>
            /// 
            /// </summary>
            /// ************************************************************************************
            public static void updateScripts()
            {
                if (scripts.Count != 0 && Time.frameCount % reflectionInterval == 0)
                {
                    currentScript %= scripts.Count;

                    int startScript = currentScript;
                    do
                    {
                        Script script = scripts[currentScript];
                        currentScript = (currentScript + 1) % scripts.Count;

                        if (script.isActive)
                        {
                            script.counter = (script.counter + 1) % script.interval;
                            if (script.counter == 0)
                            {
                                script.update(false);
                                break;
                            }
                        }
                    }
                    while (currentScript != startScript);
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private static void ensureCamera()
        {
            if (camera == null)
            {
                camera = new GameObject("TRReflectionCamera", new[] { typeof(Camera) }).GetComponent<Camera>();
                camera.enabled = false;
                camera.clearFlags = CameraClearFlags.Depth;
                // Any smaller number and visors will refect internals of helmets.
                camera.nearClipPlane = 0.125f;
                camera.layerCullDistances = CULL_DISTANCES;
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get reflective version of a shader.
        /// </summary>
        /// <param name="shader"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public Shader toReflective(Shader shader)
        {
            Shader newShader;
            shaderMappings.TryGetValue(shader, out newShader);
            return newShader;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="shader"></param>
        /// <param name="reflectionColour"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public bool applyStatic(Material material, Shader shader, Color reflectionColour)
        {
            Shader reflectiveShader = shader ?? toReflective(material.shader);

            if (reflectiveShader != null)
            {
                material.shader = reflectiveShader;
                material.SetTexture(Util.CUBE_PROPERTY, staticEnvMap);
                material.SetColor(Util.REFLECT_COLOR_PROPERTY, reflectionColour);
                return true;
            }
            return false;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void setReflectionType(Type type)
        {
            if (type == Type.STATIC && staticEnvMap == null)
                type = Type.NONE;

            reflectionType = type;

            Part[] evas = {
                PartLoader.getPartInfoByName("kerbalEVA").partPrefab,
                PartLoader.getPartInfoByName("kerbalEVAfemale").partPrefab
            };

            for (int i = 0; i < 2; ++i)
            {
                // Set visor texture and reflection on proto-EVA Kerbal.
                SkinnedMeshRenderer visor = evas[i].GetComponentsInChildren<SkinnedMeshRenderer>(true)
                  .First(m => m.name == "visor");

                Material material = visor.sharedMaterial;
                bool enableStatic = isVisorReflectionEnabled && reflectionType == Type.STATIC;

                // We apply visor shader for real reflections later, through TREvaModule since we don't
                // want corrupted reflections in the main menu.
                material.shader = enableStatic ? visorShader : transparentSpecularShader;

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // In 1.2 visor texture some reason want load by default way
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //Texture visorTex = GameDatabase.Instance.GetTexture(Util.DIR + "Default/EVAVisor", false);
               // visorTex = GameDatabase.Instance.GetTexture();
                Texture visorTex = new Texture();
                foreach (string path in Folders.DEFAULT)
                {
                    Texture texTest = GameDatabase.Instance.GetTexture(path + "EVAVisor", false);
                    if (texTest != null)
                    {
                        visorTex = texTest;
                        continue;
                    }
                        
                }

                    if (visorTex != null)
                {
                    material.SetTexture("_MainTex", visorTex);
                    material.color = Color.white;
                }
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                material.SetTexture(Util.CUBE_PROPERTY, enableStatic ? staticEnvMap : null);
                material.SetColor(Util.REFLECT_COLOR_PROPERTY, visorReflectionColour);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Read configuration and perform pre-load initialisation.
        /// </summary>
        /// <param name="rootNode"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void readConfig(ConfigNode rootNode)
        {
            Util.parse(rootNode.GetValue("reflectionType"), ref reflectionType);
            Util.parse(rootNode.GetValue("reflectionResolution"), ref reflectionResolution);
            Util.parse(rootNode.GetValue("reflectionInterval"), ref reflectionInterval);
            Util.parse(rootNode.GetValue("isVisorReflectionEnabled"), ref isVisorReflectionEnabled);
            Util.parse(rootNode.GetValue("visorReflectionColour"), ref visorReflectionColour);
            Util.parse(rootNode.GetValue("logReflectiveMeshes"), ref logReflectiveMeshes);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Post-load initialisation.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void load()
        {
            Texture2D[] envMapFaces = new Texture2D[6];

            // Foreach non-null Texture2D in any of the EnvMap Folders
            foreach (KeyValuePair<Texture2D, string> EnvMapTexture in Textures_Loader.ENVMAP())
            {
                Texture2D texture = EnvMapTexture.Key;
                string originalName = EnvMapTexture.Value;

                switch (originalName)
                {
                    case "PositiveX":
                        envMapFaces[0] = texture;
                        break;

                    case "NegativeX":
                        envMapFaces[1] = texture;
                        break;

                    case "PositiveY":
                        envMapFaces[2] = texture;
                        break;

                    case "NegativeY":
                        envMapFaces[3] = texture;
                        break;

                    case "PositiveZ":
                        envMapFaces[4] = texture;
                        break;

                    case "NegativeZ":
                        envMapFaces[5] = texture;
                        break;

                    default:
                        Util.log("Invalid enironment map texture name {0}", texture.name);
                        break;
                }
            }

            // Generate generic reflection cube map texture.
            if (envMapFaces.Contains(null))
            {
                Util.log("Some environment map faces are missing. Static reflections disabled.");
            }
            else
            {
                int envMapSize = envMapFaces[0].width;

                if (envMapFaces.Any(t => t.width != envMapSize || t.height != envMapSize)
                    || envMapFaces.Any(t => !Util.isPow2(t.width) || !Util.isPow2(t.height)))
                {
                    Util.log("Invalid environment map faces. Static reflections disabled.");
                }
                else
                {
                    try
                    {
                        staticEnvMap = new Cubemap(envMapSize, TextureFormat.RGB24, true);
                        staticEnvMap.hideFlags = HideFlags.HideAndDontSave;
                        staticEnvMap.wrapMode = TextureWrapMode.Clamp;
                        staticEnvMap.SetPixels(envMapFaces[0].GetPixels(), CubemapFace.PositiveX);
                        staticEnvMap.SetPixels(envMapFaces[1].GetPixels(), CubemapFace.NegativeX);
                        staticEnvMap.SetPixels(envMapFaces[2].GetPixels(), CubemapFace.PositiveY);
                        staticEnvMap.SetPixels(envMapFaces[3].GetPixels(), CubemapFace.NegativeY);
                        staticEnvMap.SetPixels(envMapFaces[4].GetPixels(), CubemapFace.PositiveZ);
                        staticEnvMap.SetPixels(envMapFaces[5].GetPixels(), CubemapFace.NegativeZ);
                        staticEnvMap.Apply(true, false);

                        Util.log("Static environment map cube texture generated.");
                    }
                    catch (UnityException)
                    {
                        if (staticEnvMap != null)
                            Object.DestroyImmediate(staticEnvMap);

                        staticEnvMap = null;

                        Util.log("Failed to set up static reflections. Textures not readable?");
                    }
                }
            }
           
            // we now save the visor shader in the placeholder. The shader got loaded through the ksp asset bundle
            visorShader = TextureReplacerReplaced.GetShader("KSP/TRR/Visor");

            // fill the shaderMappings dict, if we find the right shader from the mapping config. 
            // we could have used names here, but it is not in the fast path, so it is ok to leave it this way
            foreach (string origShaderName in shaderMappingConfig.Keys)
            {
                Shader original = TextureReplacerReplaced.GetShader(origShaderName);
                Shader reflective = TextureReplacerReplaced.GetShader(shaderMappingConfig[origShaderName]);

                if (original == null || original.name == "Hidden/InternalErrorShader")
                    Util.log("Shader \"{0}\" missing", origShaderName);
                else if (reflective == null || reflective.name == "Hidden/InternalErrorShader")
                    Util.log("Shader \"{0}\" missing", shaderMappingConfig[origShaderName]);
                else
                    shaderMappings[original] = reflective;
            }

            setReflectionType(reflectionType);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Called when we need to clean up
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void destroy()
        {
            if (staticEnvMap != null)
                Object.DestroyImmediate(staticEnvMap);

            if (camera != null)
                Object.DestroyImmediate(camera.gameObject);

            if (shaderMaterial != null)
                Object.DestroyImmediate(shaderMaterial);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is used to load the reflection type saved in the .cfg and persistent save
        /// </summary>
        /// <param name="node"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void loadScenario(ConfigNode node)
        {
            Type type = reflectionType;
            Util.parse(node.GetValue("reflectionType"), ref type);

            if (type != reflectionType)
                setReflectionType(type);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is used to save the reflection type persistent save
        /// </summary>
        /// <param name="node"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void saveScenario(ConfigNode node)
        {
            node.AddValue("reflectionType", reflectionType);
        }
    }
}