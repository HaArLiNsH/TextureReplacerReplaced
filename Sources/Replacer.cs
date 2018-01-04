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


using KSP.UI.Screens.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TextureReplacerReplaced
{
    /// <summary>
    /// class used to replace a texture other than the suit or the head. Like the Navball or another custom texture
    /// </summary>
    internal class Replacer
    {
        /// <summary>
        /// HUD NavBall string name
        /// </summary>
        public static readonly string HUD_NAVBALL = "HUDNavBall";

        /// <summary>
        /// IVA NavBall string name
        /// </summary>
        public static readonly string IVA_NAVBALL = "IVANavBall";
        
        /// <summary>
        /// List of mapped Texture
        /// </summary>
        private readonly Dictionary<string, Texture2D> mappedTextures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// hud NavBall textures.
        /// </summary>
        private Texture2D hudNavBallTexture = null;

        /// <summary>
        /// IVA NavBall textures.
        /// </summary>
        private Texture2D ivaNavBallTexture = null;

        /// <summary>
        /// Change shinning quality.
        /// </summary>
        private SkinQuality skinningQuality = SkinQuality.Auto;

        /// <summary>
        /// Print material/texture names when performing texture replacement pass.(default false)
        /// </summary>
        private bool logTextures = false;

        /// <summary>
        /// Instance.
        /// </summary>
        public static Replacer instance = null;

        public GameObject[] bodies = new GameObject[0];
        public GameObject bodyTest = new GameObject();
        public GameObject SpaceSuit = new GameObject();
        public GameObject CivilShort = new GameObject();
        public GameObject CivilLong = new GameObject();

        public Transform scientist_transf = null;
        public Transform worker_transf = null;
        public Transform hazmat_transf = null;

        public SkinnedMeshRenderer scientist_mesh = null;
        public SkinnedMeshRenderer worker_mesh = null;
        public SkinnedMeshRenderer hazmat_mesh = null;


        private void saveCivilMeshes()
        {
            //GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            Util.log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Util.log("List of objects");
            int bodycount = 0;

            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                //Material material = obj
                
                //Util.log("{0} ", obj.name);

                if (obj.name == "model01")
                {
                   // bodies.AddUnique(obj);
                    bodycount++;

                    
                }

                if (obj.name == "mortimer")
                {
                    Util.log("pouet mortimer !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                    foreach (SkinnedMeshRenderer smr in obj.GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        if (smr.name.EndsWith("model01", StringComparison.Ordinal))
                        {
                            scientist_mesh = smr;
                            if (scientist_mesh != null)
                            {
                                Util.log("good  :)  ============================");

                            }
                        }
                           
                    }

                    scientist_transf = obj.transform.Find("model01");

                    if (scientist_transf != null)
                    {
                        Util.log("good transform ,,,,,,,,,");
                        foreach (Renderer renderer in scientist_transf.GetComponentsInChildren<Renderer>(true))
                        {
                            var smr = renderer as SkinnedMeshRenderer;

                            if (smr != null)
                            {
                                switch (smr.name)
                                {
                                    case "body01":
                                        scientist_mesh = smr;

                                        break;
                                }
                            }
                        }

                    }

                }

                if (obj.name == "PR_Guy")
                {
                    Util.log("pouet PR_Guy !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }

                if (obj.name == "Gus")
                {
                    Util.log("pouet Gus !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }

                if (obj.name == "linus")
                {
                    Util.log("pouet linus !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }

                if (obj.name == "WernerVonKerman")
                {
                    Util.log("pouet WernerVonKerman !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }


            }
            Util.log("bodycount = {0} ", bodycount);
        }


        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// General texture replacement step.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void replaceTextures()
        {
            foreach (Material material in Resources.FindObjectsOfTypeAll<Material>())
            {
                Texture texture = material.mainTexture;

                if (texture == null || texture.name.Length == 0 || texture.name.StartsWith("Temp", StringComparison.Ordinal))
                    continue;

                if (logTextures)
                    Util.log("[{0}] {1}", material.name, texture.name);

                findCivilMeshes(material, texture);

                Texture2D newTexture;
                mappedTextures.TryGetValue(texture.name, out newTexture);

                if (newTexture != null)
                {
                    if (newTexture != texture)
                    {
                        newTexture.anisoLevel = texture.anisoLevel;
                        newTexture.wrapMode = texture.wrapMode;

                        material.mainTexture = newTexture;
                        UnityEngine.Object.Destroy(texture);
                    }
                }

                Texture normalMap = material.GetTexture(Util.BUMPMAP_PROPERTY);
                if (normalMap == null)
                    continue;

                Texture2D newNormalMap;
                mappedTextures.TryGetValue(normalMap.name, out newNormalMap);

                if (newNormalMap != null)
                {
                    if (newNormalMap != normalMap)
                    {
                        newNormalMap.anisoLevel = normalMap.anisoLevel;
                        newNormalMap.wrapMode = normalMap.wrapMode;

                        material.SetTexture(Util.BUMPMAP_PROPERTY, newNormalMap);
                        UnityEngine.Object.Destroy(normalMap);
                    }
                }
            }
        }


        public void findCivilMeshes(Material material, Texture texture)
        {

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///  Replace NavBalls' textures.
        /// </summary>
        /// <param name="vessel"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void updateNavball(Vessel vessel)
        {
            if (hudNavBallTexture != null)
            {
                NavBall hudNavball = UnityEngine.Object.FindObjectOfType<NavBall>();

                if (hudNavball != null)
                    hudNavball.navBall.GetComponent<Renderer>().sharedMaterial.mainTexture = hudNavBallTexture;
            }

            if (ivaNavBallTexture != null && InternalSpace.Instance != null)
            {
                InternalNavBall ivaNavball = InternalSpace.Instance.GetComponentInChildren<InternalNavBall>();

                if (ivaNavball != null)
                    ivaNavball.navBall.GetComponent<Renderer>().sharedMaterial.mainTexture = ivaNavBallTexture;
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Read configuration and perform pre-load initialization.
        /// </summary>
        /// <param name="rootNode"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void readConfig(ConfigNode rootNode)
        {
            Util.parse(rootNode.GetValue("skinningQuality"), ref skinningQuality);
            Util.parse(rootNode.GetValue("logTextures"), ref logTextures);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Post-load initialization.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void load()
        {
            Util.log("++++ 'load()' ++++");
            foreach (SkinnedMeshRenderer smr in Resources.FindObjectsOfTypeAll<SkinnedMeshRenderer>())
            {
                if (skinningQuality != SkinQuality.Auto)
                    smr.quality = skinningQuality;
            }

            foreach (Texture texture in Resources.FindObjectsOfTypeAll<Texture>())
            {
                if (texture.filterMode == FilterMode.Bilinear)
                    texture.filterMode = FilterMode.Trilinear;
            }

            foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture)
            {
                Texture2D texture = texInfo.texture;
                if (texture == null)
                    continue;

                foreach (string path in Folders.DEFAULT)
                {
                    if (!texture.name.StartsWith(path, StringComparison.Ordinal))
                        continue;

                    string originalName = texture.name.Substring(path.Length);

                    // Since we are merging multiple directories, we must expect conflicts.
                    if (!mappedTextures.ContainsKey(originalName))
                    {
                        if (originalName.StartsWith("GalaxyTex_", StringComparison.Ordinal))
                            texture.wrapMode = TextureWrapMode.Clamp;

                        mappedTextures.Add(originalName, texture);
                    }
                    break;
                }
            }

            Shader headShader = Shader.Find("Bumped Diffuse");
            Shader suitShader = Shader.Find("Bumped Diffuse");

            Texture2D[] headNormalMaps = { null, null };
            Texture2D ivaVisorTexture = null;

            if (mappedTextures.TryGetValue("kerbalHeadNRM", out headNormalMaps[0]))
                mappedTextures.Remove("kerbalHeadNRM");

            if (mappedTextures.TryGetValue("kerbalGirl_06_BaseColorNRM", out headNormalMaps[1]))
                mappedTextures.Remove("kerbalGirl_06_BaseColorNRM");

            if (mappedTextures.TryGetValue("kerbalVisor", out ivaVisorTexture))
                mappedTextures.Remove("kerbalVisor");

            // Fix female shaders, set normal-mapped shader for head and visor texture on proto-IVA and -EVA Kerbals.
            Kerbal[] kerbals = Resources.FindObjectsOfTypeAll<Kerbal>();

            /*Util.log("++++++++++++++++++++++++++++++++++++ pouet+++++++++++++++++++++++++++++++++++++++++");
            foreach (Kerbal kerb in kerbals)
            {
                Util.log(kerb.name);
            }*/

            Kerbal maleIva = kerbals.First(k => k.transform.name == "kerbalMale");
            Kerbal femaleIva = kerbals.First(k => k.transform.name == "kerbalFemale");
            Part maleEva = PartLoader.getPartInfoByName("kerbalEVA").partPrefab;
            Part femaleEva = PartLoader.getPartInfoByName("kerbalEVAfemale").partPrefab;

            SkinnedMeshRenderer[][] maleMeshes = {
                maleIva.GetComponentsInChildren<SkinnedMeshRenderer>(true),
                maleEva.GetComponentsInChildren<SkinnedMeshRenderer>(true)
            };

            SkinnedMeshRenderer[][] femaleMeshes = {
                femaleIva.GetComponentsInChildren<SkinnedMeshRenderer>(true),
                femaleEva.GetComponentsInChildren<SkinnedMeshRenderer>(true)
            };

            // Male materials to be copied to females to fix tons of female issues (missing normal maps, non-bumpmapped
            // shaders, missing teeth texture ...)
            Material headMaterial = null;
            Material[] suitMaterials = { null, null };
            Material[] helmetMaterials = { null, null };
            Material[] visorMaterials = { null, null };
            Material jetpackMaterial = null;

            for (int i = 0; i < 2; ++i)
            {
                foreach (SkinnedMeshRenderer smr in maleMeshes[i])
                {
                    // Many meshes share material, so it suffices to enumerate only one mesh for each material.
                    switch (smr.name)
                    {
                        case "headMesh01":
                            // Replace with bump-mapped shader so normal maps for heads will work.
                            smr.sharedMaterial.shader = headShader;

                            if (headNormalMaps[0] != null)
                                smr.sharedMaterial.SetTexture(Util.BUMPMAP_PROPERTY, headNormalMaps[0]);

                            headMaterial = smr.sharedMaterial;
                            break;

                        case "eyeballLeft":
                            smr.sharedMaterial.shader = headShader;                                                        
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballLeft":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "eyeballRight":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballRight":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "pupilLeft":
                            smr.sharedMaterial.shader = headShader;                            
                            break;
                        case "pupilRight":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilLeft":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilRight":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "upTeeth01":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth01":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth02":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth02":
                            smr.sharedMaterial.shader = headShader;
                            break;
                        case "downTeeth01":
                            smr.sharedMaterial.shader = headShader;
                            break;

                        case "body01":
                            // Also replace shader on EVA suits to match the one on IVA suits and to enable heat effects.
                            smr.sharedMaterial.shader = suitShader;

                            suitMaterials[i] = smr.sharedMaterial;
                            break;
                            
                        case "helmet":
                            // Also replace shader on EVA suits to match the one on IVA suits and to enable heat effects.
                            smr.sharedMaterial.shader = suitShader;

                            helmetMaterials[i] = smr.sharedMaterial;
                            break;

                        case "jetpack_base01":
                            // Also replace shader on EVA suits to match the one on IVA suits and to enable heat effects.
                            smr.sharedMaterial.shader = suitShader;

                            jetpackMaterial = smr.sharedMaterial;
                            break;

                        case "visor":
                            if (smr.transform.root == maleIva.transform && ivaVisorTexture != null)
                            {
                                smr.sharedMaterial.mainTexture = ivaVisorTexture;
                                smr.sharedMaterial.color = Color.white;
                            }

                            visorMaterials[i] = smr.sharedMaterial;
                            break;
                    }
                }
            }

            for (int i = 0; i < 2; ++i)
            {
                foreach (SkinnedMeshRenderer smr in femaleMeshes[i])
                {
                    // Here we must enumerate all meshes wherever we are replacing the material.
                    switch (smr.name)
                    {
                        case "headMesh":
                            smr.sharedMaterial.shader = headShader;

                            if (headNormalMaps[1] != null)
                                smr.sharedMaterial.SetTexture(Util.BUMPMAP_PROPERTY, headNormalMaps[1]);
                            break;

                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth01":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth01":
                        case "upTeeth01":
                        case "downTeeth01":
                            // Females don't have textured teeth, they use the same material as for the eyeballs. Extending female
                            // head material/texture to their teeth is not possible since teeth overlap with some ponytail sub texture.
                            // However, female teeth map to the same texture coordinates as male teeth, so we fix this by applying
                            // male head & teeth material for female teeth.
                            smr.sharedMaterial = headMaterial;
                            break;

                        case "mesh_female_kerbalAstronaut01_body01":
                        case "body01":
                            smr.sharedMaterial = suitMaterials[i];
                            break;
                            
                        case "mesh_female_kerbalAstronaut01_helmet":
                        case "helmet":
                            smr.sharedMaterial = helmetMaterials[i];
                            break;

                        case "jetpack_base01":
                            smr.sharedMaterial = jetpackMaterial;
                            break;

                        case "mesh_female_kerbalAstronaut01_visor":
                        case "visor":
                            smr.sharedMaterial = visorMaterials[i];
                            break;
                    }
                }
            }

            // Find NavBall replacement textures if available.
            if (mappedTextures.TryGetValue(HUD_NAVBALL, out hudNavBallTexture))
            {
                mappedTextures.Remove(HUD_NAVBALL);

                if (hudNavBallTexture.mipmapCount != 1)
                    Util.log("HUDNavBall texture should not have mipmaps!");
            }

            if (mappedTextures.TryGetValue(IVA_NAVBALL, out ivaNavBallTexture))
            {
                mappedTextures.Remove(IVA_NAVBALL);

                if (ivaNavBallTexture.mipmapCount != 1)
                    Util.log("IVANavBall texture should not have mipmaps!");
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add an event handler to update the Navball texture
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void beginFlight()
        {
            if (hudNavBallTexture != null || ivaNavBallTexture != null)
            {
                updateNavball(FlightGlobals.ActiveVessel);
                GameEvents.onVesselChange.Add(updateNavball);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Remove the event handler that update the Navball texture
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void endFlight()
        {
            if (hudNavBallTexture != null || ivaNavBallTexture != null)
                GameEvents.onVesselChange.Remove(updateNavball);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method to start the texture replacement
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void beginScene()
        {
            replaceTextures();
            saveCivilMeshes();
        }
    }
}
