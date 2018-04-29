/*
 * Copyright © 2017-2018 HaArLiNsH
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


        public GameObject suit_Vintage_Male_obj = new GameObject("suit_Vintage_Male");
//         public GameObject helmet_Vintage_Male_obj = new GameObject("helmet_Vintage_Male");
//         public GameObject jetpack_Vintage_Male_obj = new GameObject("jetpack_Vintage_Male");

        public SkinnedMeshRenderer suit_Vintage_Male_smrSRC = null;

        public SkinnedMeshRenderer helmet_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer visor_Vintage_Male_smrSRC = null;

        public SkinnedMeshRenderer jetpack_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer tank1_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer tank2_Vintage_Male_smrSRC = null;

        public SkinnedMeshRenderer arm_r_handle01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer arm_r_arm_b01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer arm_r_arm_a01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer arm_r_handleCtrl01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer arm_r_pivotA01_Vintage_Male_smrSRC = null;

        public SkinnedMeshRenderer arm_l_handle01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer arm_l_arm_b01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer arm_l_arm_a01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer arm_l_handleCtrl01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer arm_l_pivotA01_Vintage_Male_smrSRC = null;

        public SkinnedMeshRenderer thruster_r01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_r02_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_r03_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_r04_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_r05_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_r06_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_r07_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_r08_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_r09_Vintage_Male_smrSRC = null;

        public SkinnedMeshRenderer thruster_l01_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_l02_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_l03_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_l04_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_l05_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_l06_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_l07_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_l08_Vintage_Male_smrSRC = null;
        public SkinnedMeshRenderer thruster_l09_Vintage_Male_smrSRC = null;

        public GameObject suit_Hazmat_Male_obj = new GameObject("suit_Hazmat_Male");

        public SkinnedMeshRenderer suit_Hazmat_Male_smrSRC = null;
        public SkinnedMeshRenderer helmet_Hazmat_Male_smrSRC = null;
        public SkinnedMeshRenderer helmetSupport_Hazmat_Male_smrSRC = null;
        public SkinnedMeshRenderer visor_Hazmat_Male_smrSRC = null;
        public SkinnedMeshRenderer jetpack_Hazmat_Male_smrSRC = null;

        Dictionary<string, Transform> kerbalEVA_BonesCatalog = new Dictionary<string, Transform>();
        Dictionary<string, Transform> hazmat_BonesCatalog = new Dictionary<string, Transform>();

        public Transform hazmat_bones = null;
        public Transform kerbalEVA_bones = null;


        private void saveMeshes (Part maleEva)
        {
            Util.log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Util.log("List of objects");

            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
            {

                if (obj.name == "kbEVAVintage")
                {
                    Util.log("found kbEVAVintage +++");

                    suit_Vintage_Male_obj = obj;

                    var srcSuit_Transf = suit_Vintage_Male_obj.transform.Find("model01");
                    foreach (Transform t in srcSuit_Transf.transform)
                    {
                        //Util.log("{0} ===", t.name);
                        switch (t.name)
                        {
                            case "body01":// look for the body and name the new SkinnedMeshRenderer
                                suit_Vintage_Male_smrSRC = t.GetComponent<SkinnedMeshRenderer>();
                                suit_Vintage_Male_smrSRC.name = "suit_Vintage_Male";
                                Util.log("body ok ---");
                                break;

                            case "head02":// look for the head and destroy it (we already have a head)
                                t.gameObject.DestroyGameObject();
                                Util.log("head ok ---");
                                break;

                            case "helmet01":
                                // look for the helmet and name the new SkinnedMeshRenderer
                                var srcHelmet_Transf = t.transform.Find("helmet");
                                helmet_Vintage_Male_smrSRC = srcHelmet_Transf.GetComponent<SkinnedMeshRenderer>();
                                helmet_Vintage_Male_smrSRC.name = "helmet_Vintage_Male";
                                Util.log("helmet ok ---");

                                // look for the visor and name the new SkinnedMeshRenderer
                                var srcVisor_Transf = t.transform.Find("visor");
                                visor_Vintage_Male_smrSRC = srcVisor_Transf.GetComponent<SkinnedMeshRenderer>();
                                visor_Vintage_Male_smrSRC.name = "visor_Vintage_Male";
                                Util.log("visor ok ---");
                                break;

                             
                            case "jetpack01":// look for the jetpack and all his subcomponent and name the news SkinnedMeshRenderers

                                var tank1 = t.transform.Find("tank1");
                                tank1_Vintage_Male_smrSRC = tank1.GetComponent<SkinnedMeshRenderer>();
                                tank1_Vintage_Male_smrSRC.name = "tank1_Vintage_Male";
                                Util.log("tank1 ok ---");

                                var tank2 = t.transform.Find("tank2");
                                tank2_Vintage_Male_smrSRC = tank1.GetComponent<SkinnedMeshRenderer>();
                                tank2_Vintage_Male_smrSRC.name = "tank2_Vintage_Male";
                                Util.log("tank2 ok ---");

                                var jetpack_base01 = t.transform.Find("jetpack_base01");
                                jetpack_Vintage_Male_smrSRC = jetpack_base01.GetComponent<SkinnedMeshRenderer>();
                                jetpack_Vintage_Male_smrSRC.name = "jetpack_Vintage_Male";
                                Util.log("jetpack_base01 ok ---");

                                var grp_r_arm01 = t.transform.Find("grp_r_arm01");
                                foreach (Transform tr in grp_r_arm01.transform)
                                {
                                    switch (tr.name)
                                    {
                                        case "arm_r_handle01": 
                                            arm_r_handle01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_r_handle01_Vintage_Male_smrSRC.name = "arm_r_handle01_Vintage_Male";
                                            break;

                                        case "arm_r_arm_b01":
                                            arm_r_arm_b01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_r_arm_b01_Vintage_Male_smrSRC.name = "arm_r_arm_b01_Vintage_Male";
                                            break;                                       

                                        case "arm_r_arm_a01":
                                            arm_r_arm_a01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_r_arm_a01_Vintage_Male_smrSRC.name = "arm_r_arm_a01_Vintage_Male";
                                            break;

                                        case "arm_r_handleCtrl01":
                                            arm_r_handleCtrl01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_r_handleCtrl01_Vintage_Male_smrSRC.name = "arm_r_handleCtrl01_Vintage_Male";
                                            break;

                                        case "arm_r_pivotA01":
                                            arm_r_pivotA01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_r_pivotA01_Vintage_Male_smrSRC.name = "arm_r_pivotA01_Vintage_Male";
                                            break;
                                    }
                                }
                                Util.log("grp_r_arm01 ok ---");

                                var grp_l_arm01 = t.transform.Find("grp_l_arm01");
                                foreach (Transform tr in grp_l_arm01.transform)
                                {
                                    switch (tr.name)
                                    {
                                        case "arm_l_handle01":
                                            arm_l_handle01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_l_handle01_Vintage_Male_smrSRC.name = "arm_l_handle01_Vintage_Male";
                                            break;

                                        case "arm_l_arm_b01":
                                            arm_l_arm_b01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_l_arm_b01_Vintage_Male_smrSRC.name = "arm_l_arm_b01_Vintage_Male";
                                            break;

                                        case "arm_l_arm_a01":
                                            arm_l_arm_a01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_l_arm_a01_Vintage_Male_smrSRC.name = "arm_l_arm_a01_Vintage_Male";
                                            break;

                                        case "arm_l_handleCtrl01":
                                            arm_l_handleCtrl01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_l_handleCtrl01_Vintage_Male_smrSRC.name = "arm_l_handleCtrl01_Vintage_Male";
                                            break;

                                        case "arm_l_pivotA01":
                                            arm_l_pivotA01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            arm_l_pivotA01_Vintage_Male_smrSRC.name = "arm_l_pivotA01_Vintage_Male";
                                            break;
                                    }
                                }
                                Util.log("grp_l_arm01 ok ---");


                                var thrusters_r = t.transform.Find("thrusters_r");
                                foreach (Transform tr in thrusters_r.transform)
                                {
                                    switch (tr.name)
                                    {
                                        case "thruster_r01":
                                            thruster_r01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r01_Vintage_Male_smrSRC.name = "thruster_r01_Vintage_Male";
                                            break;

                                        case "thruster_r02":
                                            thruster_r02_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r02_Vintage_Male_smrSRC.name = "thruster_r02_Vintage_Male";
                                            break;

                                        case "thruster_r03":
                                            thruster_r03_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r03_Vintage_Male_smrSRC.name = "thruster_r03_Vintage_Male";
                                            break;

                                        case "thruster_r04":
                                            thruster_r04_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r04_Vintage_Male_smrSRC.name = "thruster_r04_Vintage_Male";
                                            break;

                                        case "thruster_r05":
                                            thruster_r05_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r05_Vintage_Male_smrSRC.name = "thruster_r05_Vintage_Male";
                                            break;

                                        case "thruster_r06":
                                            thruster_r06_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r06_Vintage_Male_smrSRC.name = "thruster_r06_Vintage_Male";
                                            break;

                                        case "thruster_r07":
                                            thruster_r07_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r07_Vintage_Male_smrSRC.name = "thruster_r07_Vintage_Male";
                                            break;

                                        case "thruster_r08":
                                            thruster_r08_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r08_Vintage_Male_smrSRC.name = "thruster_r08_Vintage_Male";
                                            break;

                                        case "thruster_r09":
                                            thruster_r09_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_r09_Vintage_Male_smrSRC.name = "thruster_r09_Vintage_Male";
                                            break;
                                    }
                                }
                                Util.log("thrusters_r ok ---");

                                var thrusters_l = t.transform.Find("thrusters_l");
                                foreach (Transform tr in thrusters_l.transform)
                                {
                                    switch (tr.name)
                                    {
                                        case "thruster_l01":
                                            thruster_l01_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l01_Vintage_Male_smrSRC.name = "thruster_l01_Vintage_Male";
                                            break;

                                        case "thruster_l02":
                                            thruster_l02_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l02_Vintage_Male_smrSRC.name = "thruster_l02_Vintage_Male";
                                            break;

                                        case "thruster_l03":
                                            thruster_l03_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l03_Vintage_Male_smrSRC.name = "thruster_l03_Vintage_Male";
                                            break;

                                        case "thruster_l04":
                                            thruster_l04_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l04_Vintage_Male_smrSRC.name = "thruster_l04_Vintage_Male";
                                            break;

                                        case "thruster_l05":
                                            thruster_l05_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l05_Vintage_Male_smrSRC.name = "thruster_l05_Vintage_Male";
                                            break;

                                        case "thruster_l06":
                                            thruster_l06_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l06_Vintage_Male_smrSRC.name = "thruster_l06_Vintage_Male";
                                            break;

                                        case "thruster_l07":
                                            thruster_l07_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l07_Vintage_Male_smrSRC.name = "thruster_l07_Vintage_Male";
                                            break;

                                        case "thruster_l08":
                                            thruster_l08_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l08_Vintage_Male_smrSRC.name = "thruster_l08_Vintage_Male";
                                            break;

                                        case "thruster_l09":
                                            thruster_l09_Vintage_Male_smrSRC = tr.GetComponent<SkinnedMeshRenderer>();
                                            thruster_l09_Vintage_Male_smrSRC.name = "thruster_l09_Vintage_Male";
                                            break;
                                    }
                                }
                                Util.log("thrusters_l ok ---");

                                break;


                        }
                    }

                    

                    // look for the body and name the new SkinnedMeshRenderer
//                     var srcBody_Transf = suit_Vintage_Male_obj.transform.Find("model01/body01"); 
//                     suit_Vintage_Male_smrSRC = srcBody_Transf.GetComponent<SkinnedMeshRenderer>();
//                     suit_Vintage_Male_smrSRC.name = "suit_Vintage_Male";
//                     Util.log("body ok ---");

                    // look for the head and destroy it (we already have a head)
//                     var srcHead_Transf = suit_Vintage_Male_obj.transform.Find("model01/head02");                    
//                     srcHead_Transf.gameObject.DestroyGameObject();
//                     Util.log("head ok ---");

                    // look for the helmet and name the new SkinnedMeshRenderer
//                     foreach (Transform t in suit_Vintage_Male_obj.transform)
//                     {
//                         Util.log("{0} ===",t.name);
//                     }
//                     var srcHelmet_Tansf = suit_Vintage_Male_obj.transform.Find("helmet01");
//                     helmet_Vintage_Male_smrSRC = srcHelmet_Tansf.GetComponent<SkinnedMeshRenderer>();
//                     helmet_Vintage_Male_smrSRC.name = "helmet_Vintage_Male";
//                     Util.log("helmet ok ---");

                    // look for the jetpack and name the new SkinnedMeshRenderer
//                     var srcJetpack_Tansf = suit_Vintage_Male_obj.transform.Find("jetpack01");
//                     jetpack_Vintage_Male_smrSRC = srcJetpack_Tansf.GetComponent<SkinnedMeshRenderer>();
//                     jetpack_Vintage_Male_smrSRC.name = "jetpack_Vintage_Male";
//                     Util.log("jetpack ok ---");
                }

                if (obj.name == " kbEVAFemaleVintage")
                {
                    Util.log("found  kbEVAFemaleVintage +++");
                }

                if (obj.name == "female01")
                {
                    Util.log("found female01 +++");
                }


                if (obj.name == "mortimer")
                {
                    Util.log("found mortimer (VIP) +++");
                }

                if (obj.name == "PR_Guy")
                {
                    Util.log("found PR_Guy (Hazmat) +++");

                    suit_Hazmat_Male_obj = obj;


                    hazmat_bones = obj.transform.Find("globalMove01");

                   // hazmat_bones.gameObject.DestroyGameObject();

                    hazmat_BonesCatalog = new Stitcher.TransformCatalog(hazmat_bones.transform);


                    var srcSuit_Transf = suit_Hazmat_Male_obj.transform.Find("model01");
                    //Util.log("found model01 ******* ");
                    var srcSuit_Transf_hazmat = srcSuit_Transf.transform.Find("hazmatmodel01");
                    //Util.log("found hazmatmodel01 ******* ");
                    var srcSuit_Transf_head = srcSuit_Transf.transform.Find("head02");
                    Util.log("found head02 ******* ");
                    srcSuit_Transf_head.gameObject.DestroyGameObject();
                    foreach (Transform t in srcSuit_Transf_hazmat.transform)
                    {
                        switch (t.name)
                        {
                            case "body01":
                                suit_Hazmat_Male_smrSRC = t.GetComponent<SkinnedMeshRenderer>();
                                suit_Hazmat_Male_smrSRC.name = "suit_Hazmat_Male";
                                Util.log("body01 ok ---");
                                break;

                            case "mesh_hazm_helmet":
                                helmet_Hazmat_Male_smrSRC = t.GetComponent<SkinnedMeshRenderer>();
                                helmet_Hazmat_Male_smrSRC.name = "helmet_Hazmat_Male";
                                Util.log("mesh_hazm_helmet ok ---");
                                break;

                            case "mesh_helmet_support":
                                helmetSupport_Hazmat_Male_smrSRC = t.GetComponent<SkinnedMeshRenderer>();
                                helmetSupport_Hazmat_Male_smrSRC.name = "helmetSupport_Hazmat_Male";
                                Util.log("mesh_helmet_support ok ---");
                                break;

                            case "mesh_hazm_visor":
                                visor_Hazmat_Male_smrSRC = t.GetComponent<SkinnedMeshRenderer>();
                                visor_Hazmat_Male_smrSRC.name = "visor_Hazmat_Male";
                                Util.log("mesh_hazm_visor ok ---");
                                break;

                            case "mesh_backpack":
                                jetpack_Hazmat_Male_smrSRC = t.GetComponent<SkinnedMeshRenderer>();
                                jetpack_Hazmat_Male_smrSRC.name = "jetpack_Hazmat_Male";
                                Util.log("mesh_backpack ok ---");
                                break;
                        }
                    }
                }

                if (obj.name == "Gus")
                {
                    Util.log("found Gus (worker) +++");
                }

                if (obj.name == "linus")
                {
                    Util.log("found linus (scientist) +++");
                }

                if (obj.name == "WernerVonKerman")
                {
                    Util.log("found WernerVonKerman +++");

                }

                if (obj.name == "GroundCrew01")
                {
                    Util.log("found GroundCrew01 +++");
                }

                if (obj.name == "Mechanic01")
                {
                    Util.log("found Mechanic01 +++");
                }

                if (obj.name == "kerbalEVA")
                {
                    Util.log("found KerbalEVA +++");

                    kerbalEVA_bones = obj.transform.Find("globalMove01");

                    kerbalEVA_BonesCatalog = new Stitcher.TransformCatalog(kerbalEVA_bones.transform);
                }

            }
           /* Util.log("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            Util.log("list of Hazmat bones :   ");
            foreach (String s in hazmat_BonesCatalog.Keys)
            {
                Util.log(s);
            }

            Util.log("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            Util.log("list of KerbalEVA bones :   ");
            foreach (String s in kerbalEVA_BonesCatalog.Keys)
            {
                Util.log(s);
            }

            Util.log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Util.log("bones diff :   ");
            var intersection = kerbalEVA_BonesCatalog.Keys.Intersect(hazmat_BonesCatalog.Keys);
            Util.log("intersection :");
            foreach (String s in intersection)
            {

                Util.log(s);
            }

            var extraKeysInA = hazmat_BonesCatalog.Keys.Except(intersection);
            var extraKeysInB = kerbalEVA_BonesCatalog.Keys.Except(intersection);
            Util.log("extra in Hazmat :  ");
            foreach (String s in extraKeysInA)
            {
               
                Util.log(s);
            }
            Util.log("extra in kerbalEVA :   ");
            foreach (String s in extraKeysInB)
            {
                
                Util.log(s);
            }*/

            //hazmat_bones.gameObject.transform = kerbalEVA_bones.transform;


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
                if (!material.HasProperty("_MainTex"))
                continue;

                Texture texture = material.mainTexture;

                if (texture == null || texture.name.Length == 0 || texture.name.StartsWith("Temp", StringComparison.Ordinal))
                    continue;

                if (logTextures)
                    Util.log("[{0}] {1}", material.name, texture.name);

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

                if (!material.HasProperty(Util.BUMPMAP_PROPERTY))
                    continue;

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

            saveMeshes(maleEva);            

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
        }
    }
}
