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
    /// <summary>
    /// This class is used to personalize the head and the suit of your kerbal
    /// </summary>
    public class Personaliser
    {

        /// <summary>
        /// Default Male and Female head set (from `Default/`).
        /// </summary>
        public readonly Head_Set[] defaulMaleAndFemaleHeads = { new Head_Set { name = "DEFAULT_MALE" }, new Head_Set { name = "DEFAULT_FEMALE" } };

        /// <summary>
        /// Default suit textures (from `Default/`).
        /// </summary>
        public readonly Suit_Set defaultSuit = new Suit_Set { name = "DEFAULT_SUIT" };
        
        /// <summary>
        /// Heads textures, including excluded by configuration.
        /// </summary>
        public readonly List<Head_Set> KerbalHeadsDB_full = new List<Head_Set>();

        /// <summary>
        /// Suits textures, including excluded by configuration.
        /// </summary>
        public readonly List<Suit_Set> KerbalSuitsDB_full = new List<Suit_Set>();

        /// <summary>
        /// ordered list of the male and female heads textures, including excluded by configuration.
        /// </summary>
        public readonly List<Head_Set>[] maleAndfemaleHeadsDB_full = { new List<Head_Set>(), new List<Head_Set>() };

        /// <summary>
        /// Male and female heads textures (minus excluded).
        /// </summary>
        public readonly List<Head_Set>[] maleAndfemaleHeadsDB_cleaned = { new List<Head_Set>(), new List<Head_Set>() };

        /// <summary>
        /// Male and female suits textures (minus excluded).  
        /// </summary>
        private readonly List<Suit_Set>[] maleAndfemaleSuitsDB_cleaned = { new List<Suit_Set>(), new List<Suit_Set>() };

        /// <summary>
        /// the list of exclusive heads
        /// </summary>
        public List<string> excludedHeads = new List<string>();

        /// <summary>
        /// the list of exclusive suits
        /// </summary>
        public List<string> excludedSuits = new List<string>();


        /// <summary>
        /// List of the suit set (minus excluded). 
        /// </summary>
        private readonly List<Suit_Set> KerbalSuitsDB_cleaned = new List<Suit_Set>();

        /// <summary>
        /// Here we have the list of all the kerbal and the head set each one uses.
        /// </summary>  
        public Dictionary<string, string> KerbalAndTheirHeadsDB = new Dictionary<string, string>();

        /// <summary>
        /// List of your personalized Kerbals with their KerbalData
        /// </summary>
        private readonly Dictionary<string, KerbalData> gameKerbalsDB = new Dictionary<string, KerbalData>();

        /// <summary>
        /// Backed-up personalized textures from main configuration files.
        /// <para>These are used to initialise kerbals if a saved game doesn't contain `TRR_Scenario`. </para>
        /// </summary>
        private ConfigNode customKerbalsNode = new ConfigNode();
                
        /// <summary>
        /// List of class specific suits
        /// <para> This list is first loaded from the file @default.cfg</para>
        /// <para>Then this is saved and loaded in the persistent.sfs save</para>
        /// </summary>
        public readonly Dictionary<string, Suit_Set> classSuitsDB = new Dictionary<string, Suit_Set>();

        /// <summary>
        /// List of the default class specific suits
        /// </summary>
        public readonly Dictionary<string, Suit_Set> defaultClassSuits = new Dictionary<string, Suit_Set>();

        /// <summary>
        /// Used for the helmet removal
        /// /// this is the saved mesh
        /// </summary>
        private Mesh[] helmetMesh = { null, null };

        /// <summary>
        /// Used for the helmet removal
        /// this is the saved mesh
        /// </summary>
        private Mesh[] visorMesh = { null, null };

        /// <summary>
        /// Used for the jetpack removal
        /// this is the saved mesh
        /// </summary>
        private Mesh[] jetpackMesh = { null, null };

        /// <summary>
        /// Does the kerbal needs his helmet?
        /// </summary>
        public bool needHelmet = true;

        /// <summary>
        /// Convert all females to males but apply female textures on them to emulate pre-1.0 females. 
        /// Disabling this feature should restore "legacy" females back to real females.
        /// <para>used in the @default.cfg file </para>
        /// </summary>
        private bool forceLegacyFemales = false;
        
        /// <summary>
        /// Spawn a Kerbal on EVA ground suit when on ground and no atmosphere
        /// /// <para>This is only initial setting for new games! Use the GUI to change it later. </para>
        /// </summary>
        public bool isNewSuitStateEnabled = true;

        /// <summary>
        /// Minimum air pressure required for Kerbals to wear their IVA suits on EVA.
        /// </summary>
        private double atmSuitPressure = 50.0;

        /// <summary>
        /// List of planets/moons with breathable atmospheres where Kerbals can wear their IVA suits on EVA.
        /// </summary>
        private readonly HashSet<string> atmSuitBodies = new HashSet<string>();

        /// <summary>
        /// Instance of Personaliser
        /// </summary>
        public static Personaliser instance = null;

        private bool isMortimer_Body_loaded = false;



        /* =========================================================================================
         * general TRR options
         * used in the main TRR configuration Gui
         * =========================================================================================
         */

        /*/// <summary>
        /// Do we remove the helmet in safe situation ?
        /// </summary>
        public bool isSafeHelmetRemovalEnabled = true;

        /// <summary>
        /// Do we use Atmospheric IVA suit ?
        /// </summary>
        public bool isAtmoIVAsuitEnabled = true;

        /// <summary>
        /// Do we use EVA ground suit ?
        /// </summary>
        public bool isEVAgroundSuitEnabled = true;

        // This one will disappear I think
        /// <summary>
        /// Do we use the automatic suit state switcher ? 
        /// </summary>
        public bool isAutomaticSuitSwitchEnabled = true;

        /// <summary>
        /// remove collar on IVA suits ? (for later)
        /// </summary>
        public bool isCollarRemovalEnabled = false;*/

        public bool useKspSkin = true;   
                
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Component bound to internal models that triggers Kerbal texture personalization
        /// when the internal model changes.
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private class TRR_IvaModule : MonoBehaviour
        {
            /// <summary>
            /// Called at the Start() 
            /// </summary>
            public void Start()
            {
                //Util.log("++++ 'Start()' ++++");
                bool hasVisor = true;
                Personaliser.instance.personaliseIva(GetComponent<Kerbal>(), out hasVisor);
                //Destroy(this);
            }

            public void Update()
            {
                //Util.log("++++ 'Update()' ++++");
                bool hasVisor = true;
                Personaliser.instance.personaliseIva(GetComponent<Kerbal>(), out hasVisor);
            }

        }

        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// This is the module used when in EVA        
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private class TRR_EvaModule : PartModule
        {
            /// <summary>
            /// The script for the part that handle the reflections and shaders
            /// </summary>
            private Reflections.Script reflectionScript = null;

            /// <summary>
            /// To check if the PartModule has been initialized
            /// </summary>
            [KSPField(isPersistant = true)]
            private bool isInitialised = false;


            /// <summary>
            /// To check if your kerbal need his EVA space suit
            /// </summary>
            [KSPField(isPersistant = true)]
            public bool hasEvaSuit = false;

            /// <summary>
            /// To check if your kerbal need his EVA ground suit.
            /// </summary>
            [KSPField(isPersistant = true)]
            public bool hasEvaGroundSuit = false;

            /// <summary>
            /// The actual selection of the suit. The suit selection goes like this : 
            /// <para>0 = IVA suit</para>
            /// <para>1 = EVA ground suit</para>
            /// <para>2 = EVA space suit</para>
            /// </summary>
            [KSPField(isPersistant = true)]
            public int actualSuitState = 0;

            /// ************************************************************************************
            /// <summary>
            /// Used when you press the "Toggle EVA Suit" button on the Gui 
            /// when you Right-click on the kerbal
            /// </summary>
            /// ************************************************************************************
            [KSPEvent(guiActive = true, guiName = "Toggle EVA Suit Situation")]
            public void toggleEvaSuit()
            {

                Personaliser personaliser = Personaliser.instance;
                bool useVisor = true;
                Color32 visorReflectioncolor = new Color32(128, 128, 128, 255);

                // new suit State for TRR
                if (personaliser.isNewSuitStateEnabled)
                {
                    if (actualSuitState < 2)
                    {
                        actualSuitState++;
                    }
                    else
                    {
                        actualSuitState = 0;
                    }                    
                }
                else
                {
                    if (actualSuitState == 0)
                    {
                        actualSuitState = 2;
                    }
                    else if (actualSuitState == 1)
                    {
                        actualSuitState = 2;
                    }
                    else if (actualSuitState >= 2)
                    {
                        actualSuitState = 0;
                    }
                }
                switch (personaliser.personaliseEva(part, actualSuitState, out useVisor, out visorReflectioncolor, false))
                {
                    case 0:     //IVA suit, if no air switch to state 1 : EVAground
                        actualSuitState = 0;
                        hasEvaSuit = false;
                        hasEvaGroundSuit = false;
//                         if (reflectionScript != null)
//                             reflectionScript.setActive(useVisor);
                        //ScreenMessages.PostScreenMessage("IVA wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                    case 1:     //EVAground suit
                        actualSuitState = 1;
                        hasEvaSuit = true;
                        hasEvaGroundSuit = true;
//                         if (reflectionScript != null)
//                             reflectionScript.setActive(useVisor);
                        //ScreenMessages.PostScreenMessage("EVA ground wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                    case 2:     //EVA suit
                        actualSuitState = 2;
                        hasEvaSuit = true;
                        hasEvaGroundSuit = false;
//                         if (reflectionScript != null)
//                             reflectionScript.setActive(useVisor);
                        //ScreenMessages.PostScreenMessage("EVA space wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                }

                
            }

        /// ************************************************************************************
        /// <summary>
        /// Override the OnStart(). <see cref="Personaliser.TRR_EvaModule"/>
        /// </summary>
        /// ************************************************************************************
        public override void OnStart(StartState state)
            {
                Util.log("++++ 'OnStart()' ++++");
                //Util.log("+++++ '{0}' +++++", state);
                Personaliser personaliser = Personaliser.instance;
                bool useVisor = true;
                Color32 visorReflectioncolor = new Color32(128, 128, 128, 255);





                if (!isInitialised)
                {                    
                    isInitialised = true;
                }

                if (personaliser.personaliseEva(part, actualSuitState, out useVisor, out visorReflectioncolor, true) == 2)
                {
                    actualSuitState = 2;
                    hasEvaSuit = true;
                }

                if (Reflections.instance.isVisorReflectionEnabled
                && Reflections.instance.reflectionType == Reflections.Type.REAL)
                {
                    reflectionScript = new Reflections.Script(part, 2, visorReflectioncolor);
                    reflectionScript.setActive(useVisor);
                }
            }

            /// ************************************************************************************
            /// <summary>
            /// Update() <see cref="Personaliser.TRR_EvaModule"/>
            /// </summary>
            /// ************************************************************************************
            public void Update()
            {
                   
                Personaliser personaliser = Personaliser.instance;
                bool useVisor = true;
                Color32 visorReflectioncolor = new Color32(128, 128, 128, 255);

                switch (personaliser.personaliseEva(part, actualSuitState, out useVisor, out visorReflectioncolor, false))
                {
                    case 0:     //IVA suit, if no air switch to state 1 : EVAground
                        actualSuitState = 0;
                        hasEvaSuit = false;
                        hasEvaGroundSuit = false;
                        if (reflectionScript != null)
                        {
                            reflectionScript.setActive(useVisor);
                            reflectionScript.updateReflectioncolor(part, visorReflectioncolor);
                        }
                        //ScreenMessages.PostScreenMessage("IVA wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                    case 1:     //EVAground suit
                        actualSuitState = 1;
                        hasEvaSuit = true;
                        hasEvaGroundSuit = true;
                        if (reflectionScript != null)
                        {
                            reflectionScript.setActive(useVisor);
                            reflectionScript.updateReflectioncolor(part, visorReflectioncolor);
                        }
                        //ScreenMessages.PostScreenMessage("EVA ground wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                    case 2:     //EVA suit
                        actualSuitState = 2;
                        hasEvaSuit = true;
                        hasEvaGroundSuit = false;
                        if (reflectionScript != null)
                        {
                            reflectionScript.setActive(useVisor);
                            reflectionScript.updateReflectioncolor(part, visorReflectioncolor);
                        }
                        //ScreenMessages.PostScreenMessage("EVA space wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                }
            }          
            
        /// ************************************************************************************
        /// <summary>
        /// OnDestroy() <see cref="Personaliser.TRR_EvaModule"/>
        /// </summary>
        /// ************************************************************************************
        public void OnDestroy()
            {
                if (reflectionScript != null)
                    reflectionScript.destroy();
            }
        }                     

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Whether a vessel is in a "safe" situation, so Kerbals don't need helmets (landed/splashed or in orbit).
        /// </summary>
        /// <param name="vessel">The vessel you want to test</param>
        /// <returns>True if the vessel is not in flying or suborbital situation</returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private static bool isSituationSafe(Vessel vessel)
        {
            return vessel.situation != Vessel.Situations.FLYING && vessel.situation != Vessel.Situations.SUB_ORBITAL;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Whether atmosphere is breathable.
        /// </summary>
        /// <returns>True if the actual air pressure is superior to <see cref="Personaliser.atmSuitPressure"/>
        /// and were are on a planet listed in <see cref="Personaliser.atmSuitBodies"/></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public bool isAtmBreathable()
        {
            bool value = !HighLogic.LoadedSceneIsFlight
                         || (FlightGlobals.getStaticPressure() >= atmSuitPressure
                         && atmSuitBodies.Contains(FlightGlobals.currentMainBody.bodyName));
           
            //if (value == false) ScreenMessages.PostScreenMessage("Atmosphere non breathable !", 5.0f, ScreenMessageStyle.UPPER_CENTER);
            return value;
        }
        
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Whether we are under sub orbit.
        /// <para>Used to change automatically suits.</para>        
        /// </summary>        
        /// <param name="vessel">The vessel we want to test</param>
        /// <returns>True if the vessel is under suborbital situation</returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public bool isUnderSubOrbit(Vessel vessel)
        {
            bool value = false;
            
            if (vessel == null)
            {
                return value;
            }
                
            switch (vessel.situation)
            {
                case Vessel.Situations.PRELAUNCH:
                    value = true;
                    break;
                case Vessel.Situations.LANDED:
                    value = true;
                    break;
                case Vessel.Situations.SPLASHED:
                    value = true;
                    break;
                case Vessel.Situations.FLYING:
                    value = true;
                    break;
                case Vessel.Situations.SUB_ORBITAL:
                    value = true;
                    break;
            }

            return value;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Search the classSuits list and return the suit set selected for this class
        /// <para> if no suit set selected for this class, return the default one</para>
        /// </summary>
        /// <param name="kerbal">The kerbal we want to get his class suit</param>
        /// <returns>The suit set selected for this kerbal class 
        /// or the default one if no suit select for this class</returns>     
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public Suit_Set getClassSuit(ProtoCrewMember kerbal)
        {
            Suit_Set suit;
            classSuitsDB.TryGetValue(kerbal.experienceTrait.Config.Name, out suit);
            return suit;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Return the <see cref="KerbalData"/> of a kerbal
        /// <para> </para>
        /// </summary>
        /// <param name="kerbal">The kerbal we want the data</param>
        /// <returns>the <see cref="KerbalData"/> of the kerbal</returns>     
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public KerbalData getKerbalData(ProtoCrewMember kerbal)
        {
            KerbalData kerbalData;

            if (!gameKerbalsDB.TryGetValue(kerbal.name, out kerbalData))
            {
                kerbalData = new KerbalData
                {
                    hash = kerbal.name.GetHashCode(),
                    gender = (int)kerbal.gender,
                    isVeteran = kerbal.veteran,
                    isBadass = kerbal.isBadass
                };
                gameKerbalsDB.Add(kerbal.name, kerbalData);

                if (forceLegacyFemales)
                    kerbal.gender = ProtoCrewMember.Gender.Male;
            }
            return kerbalData;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Return the <see cref="Personaliser.KerbalData.head"/> chosen for the kerbal (with gender)
        /// <para> If no head already chosen, pick a random one from <see cref="Personaliser.maleAndfemaleHeadsDB_cleaned"/></para>
        /// <para>If nothing in the db, return <see cref="Personaliser.defaulMaleAndFemaleHeads"/></para>
        /// </summary>
        /// <param name="kerbal">The kerbal we want the head</param>
        /// <param name="kerbalData">The <see cref="KerbalData"/> of the kerbal we want the head</param>
        /// <returns>the <see cref="Personaliser.KerbalData.head"/> chosen for the kerbal (with gender)
        /// If none, select a random one from <see cref="Personaliser.maleAndfemaleHeadsDB_cleaned"/>
        /// If nothing in the db, return <see cref="Personaliser.defaulMaleAndFemaleHeads"/></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public Head_Set getKerbalHead(ProtoCrewMember kerbal, KerbalData kerbalData)
        {

            Personaliser personaliser = Personaliser.instance;

            Randomizer random = new Randomizer();

            
            List<Head_Set> genderHeads = maleAndfemaleHeadsDB_cleaned[kerbalData.gender];

            if (kerbalData.head != null)
            {
                if (kerbalData.head.name == "DEFAULT_MALE" || kerbalData.head.name == "DEFAULT_FEMALE")
                {
                    return defaulMaleAndFemaleHeads[(int)kerbal.gender];
                }
                else
                    return kerbalData.head;
            }
                          

            if (genderHeads.Count == 0)
            {               
                return defaulMaleAndFemaleHeads[(int)kerbal.gender];
            }
            
            // if the kerbal had no head set , choose one randomly.    
            kerbalData.head = random.randomize((int)kerbal.gender);

            string value = "";

            if (KerbalAndTheirHeadsDB.TryGetValue(kerbal.name, out value))
            {
                KerbalAndTheirHeadsDB[kerbal.name] = kerbalData.head.name;
            }
            else
            {
                KerbalAndTheirHeadsDB.Add(kerbal.name, kerbalData.head.name);
            }
            //Util.log("{0} use this head set : {1}", kerbal.name, kerbalData.head.headSetName);

            return kerbalData.head;


        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Search the KerbalData of a kerbal and return his suit set
        /// <para>If no suit set for this kerbal, search <see cref="classSuitsDB"/> and return the suit set for his class </para>
        /// <para>If no suit set for his class, give the kerbal a random suit from from <see cref="maleAndfemaleSuitsDB_cleaned"/></para>
        /// <para>If <see cref="maleAndfemaleSuitsDB_cleaned"/> is empty, return <see cref="defaultSuit"/> </para>
        /// </summary>
        /// <param name="kerbal">The kerbal we want the suit set</param>
        /// <param name="kerbalData">The <see cref="KerbalData"/> of the kerbal we want the suit set</param>
        /// <returns>The suit set of the kerbal saved in his <see cref="KerbalData"/>
        /// If none, select a random one from <see cref="maleAndfemaleSuitsDB_cleaned"/>
        /// If nothing in the db, return <see cref="defaultSuit"/></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public Suit_Set getKerbalSuit(ProtoCrewMember kerbal, KerbalData kerbalData)
        {
            Suit_Set suit = kerbalData.suit ?? getClassSuit(kerbal);
            if (suit != null)
                return suit;

            if (KerbalSuitsDB_cleaned.Count == 0)
                return defaultSuit;

            // We must use a different prime here to increase randomization so that the same head is not always combined with
            // the same suit.
            int number = ((kerbalData.hash + kerbal.name.Length) * 2053) & 0x7fffffff;
            return KerbalSuitsDB_cleaned[number % KerbalSuitsDB_cleaned.Count];
        }




        /// <summary>
        /// This is the main method that personalize and change the texture of your kerbal. 
        /// </summary>
        /// <param name="component">The kerbal we want to personalize, in term of <see cref="Component"/></param>
        /// <param name="protoKerbal">The <see cref="ProtoCrewMember"/> data of the kerbal we want to personalize</param>
        /// <param name="cabin">A handle to the part that contains this Kerbal</param>
        /// <param name="needsEVASuit">Does the kerbal need a EVA suit? (space or ground)</param>
        /// <param name="needsEVAgroundSuit">Does the kerbal need a EVA ground suit ?</param>
        /// /// ////////////////////////////////////////////////////////////////////////////////////////



        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is the main method that personalize and change the texture of your kerbal. 
        /// </summary>
        /// <param name="component">The kerbal we want to personalize, in term of <see cref="Component"/></param>
        /// <param name="protoKerbal">The <see cref="ProtoCrewMember"/> data of the kerbal we want to personalize</param>
        /// <param name="cabin">A handle to the part that contains this Kerbal</param>
        /// <param name="needsEVASuit">Does the kerbal need a EVA suit? (space or ground)</param>
        /// <param name="needsEVAgroundSuit">Does the kerbal need a EVA ground suit ?</param>
        /// <param name="suitState"></param>
        /// <param name="hasVisor">Does the kerbal use a visor?</param>
        /// <param name="visorReflection_Color">the color wanted for the reflection on the visor</param>
        /// <param name="initialisation"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void personaliseKerbal(Component component, ProtoCrewMember protoKerbal, Part cabin, bool needsEVASuit, 
            bool needsEVAgroundSuit, int suitState, out bool hasVisor, out Color32 visorReflection_Color, bool initialisation)
        {
            Personaliser personaliser = Personaliser.instance;

            Replacer replacer = Replacer.instance;

            Stitcher stitcher = new Stitcher();

            KerbalData kerbalData = getKerbalData(protoKerbal);

            int level = protoKerbal.experienceLevel;

            bool isEva = cabin == null;

            int gender = kerbalData.gender;
            bool isVeteran = kerbalData.isVeteran;
            bool isBadass = kerbalData.isBadass;

            bool useVisor = true;

            Color32 visorReflectionColor = new Color32(128, 128, 128, 255);

            Color32 visorBasecolor = new Color32(255, 255, 255, 255);

            Head_Set personaliseKerbal_Head = getKerbalHead(protoKerbal, kerbalData);
           
            Suit_Set personaliseKerbal_Suit = getKerbalSuit(protoKerbal, kerbalData);

            Suit_Filter suit_Filter = new Suit_Filter(kerbalData,level, personaliseKerbal_Suit);
            Suit_Selector suit_Selector = new Suit_Selector(kerbalData, level, personaliseKerbal_Suit);
            

            Transform model = isEva ? component.transform.Find("model01") : component.transform.Find("kbIVA@idle/model01");
            Transform flag = isEva ? component.transform.Find("model/kbEVA_flagDecals") : null;

            if (isEva)
                // remove the flag from the jetpack to "fix" it
                //flag.GetComponent<Renderer>().enabled = needsEVASuit;
                flag.GetComponent<Renderer>().enabled = false;

            // model = replacer.scientist_body_transf;

            //component.transform.

            GameObject baseModel = component.transform.gameObject;

            if (initialisation == true)
            {
                Util.log("Mortimer_Body is loading +++");

              //  var mortimer_body01_smr = baseModel.AddComponent<SkinnedMeshRenderer>();
             //   mortimer_body01_smr.name = "mortimer_body";

                GameObject morty = GameObject.Instantiate(replacer.mortimer_obj);

                

               //Stitch(morty, baseModel);

                stitcher.Stitch(morty, baseModel);

              //  mortimer_body01_smr.transform.parent = baseModel.transform;

              //  Util.log("parent = {0}", mortimer_body01_smr.transform.parent);

                //mortimer_body01_smr.sharedMaterial = replacer.mortimer_body01_smr.sharedMaterial;
              //  mortimer_body01_smr.sharedMesh = replacer.mortimer_body01_mesh;
               // mortimer_body01_smr.bones = baseModel.GetComponent<SkinnedMeshRenderer>().bones;
               // mortimer_body01_smr.transform.SetParent(baseModel.transform, true);

//                 mortimer_body01_smr.transform.position = baseModel.transform.position;
//                 mortimer_body01_smr.transform.rotation = baseModel.transform.rotation;
//                 mortimer_body01_smr.gameObject.layer = baseModel.gameObject.layer;
//                 mortimer_body01_smr.transform.parent = baseModel.transform.parent;

                Util.log("Mortimer_Body is loaded !!! ");
                isMortimer_Body_loaded = true;
            }

            



            // We must include hidden meshes, since flares are hidden when light is turned off.
            // All other meshes are always visible, so no performance hit here.
            //foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>(true))
            foreach (Renderer renderer in baseModel.GetComponentsInChildren<Renderer>(true))
            {
                //Util.log("pouet +++ {0}" , renderer.name);

                var smr = renderer as SkinnedMeshRenderer;

                // Thruster jets, flag decals and headlight flares.
                if (smr == null)
                {
                    if (renderer.name != "screenMessage")
                        renderer.enabled = needsEVASuit;
                }
                else
                {
                    Material material = renderer.material;
                    Texture2D newTexture = null;
                    Texture2D newNormalMap = null;

                    switch (smr.name)
                    {
                        case "eyeballLeft":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballLeft":
                            if (personaliseKerbal_Head != null)
                            {
                                
                               if (personaliseKerbal_Head.lvlToHide_Eye_Left <= protoKerbal.experienceLevel)
                                {
                                   // Util.log("*** HIDE for {0}",protoKerbal.name);                                    
                                    smr.GetComponentInChildren<Renderer>().enabled = false; 
                                }
                                else
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = true;
                                    newTexture = personaliseKerbal_Head.get_eyeball_LeftTexture(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Head.get_eyeball_LeftTextureNRM(protoKerbal.experienceLevel);
                                    if (newTexture != null)                                    
                                        smr.sharedMaterial.color = Color.white;                                    
                                    else
                                        smr.sharedMaterial.color = personaliseKerbal_Head.get_EyeballColor_Left(protoKerbal.experienceLevel);                                    
                                }
                            }
                            break;
                            
                        case "eyeballRight":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballRight":
                            if (personaliseKerbal_Head != null)
                            {
                                if (personaliseKerbal_Head.lvlToHide_Eye_Right <= protoKerbal.experienceLevel)
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = false;
                                }
                                else
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = true;
                                    newTexture = personaliseKerbal_Head.get_eyeball_RightTexture(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Head.get_eyeball_RightTextureNRM(protoKerbal.experienceLevel);
                                    if (newTexture != null)
                                        smr.sharedMaterial.color = Color.white;
                                    else
                                        smr.sharedMaterial.color = personaliseKerbal_Head.get_EyeballColor_Right(protoKerbal.experienceLevel);                                    
                                }
                            }
                            break;

                        case "pupilLeft":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilLeft":                            
                            if (personaliseKerbal_Head != null)
                            {
                                if (personaliseKerbal_Head.lvlToHide_Pupil_Left <= protoKerbal.experienceLevel)
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = false;
                                }
                                else
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = true;
                                    newTexture = personaliseKerbal_Head.get_pupil_LeftTexture(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Head.get_pupil_LeftTextureNRM(protoKerbal.experienceLevel);
                                    if (newTexture != null)
                                        smr.sharedMaterial.color = Color.white;
                                    else
                                        smr.sharedMaterial.color = personaliseKerbal_Head.get_PupilColor_Left(protoKerbal.experienceLevel);
                                }
                            }
                            break;

                        case "pupilRight":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilRight":                           
                            if (personaliseKerbal_Head != null)
                            {
                                if (personaliseKerbal_Head.lvlToHide_Pupil_Right <= protoKerbal.experienceLevel)
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = false;
                                }
                                else
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = true;
                                    newTexture = personaliseKerbal_Head.get_pupil_RightTexture(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Head.get_pupil_RightTextureNRM(protoKerbal.experienceLevel);
                                    if (newTexture != null)
                                        smr.sharedMaterial.color = Color.white;
                                    else
                                        smr.sharedMaterial.color = personaliseKerbal_Head.get_PupilColor_Right(protoKerbal.experienceLevel);
                                    
                                }
                            }
                            break;

                        case "headMesh01":                       
                        case "headMesh":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_polySurface51":
                            if (personaliseKerbal_Head != null)
                            {
                                newTexture = personaliseKerbal_Head.get_headTexture(protoKerbal.experienceLevel);
                                newNormalMap = personaliseKerbal_Head.get_headTextureNRM(protoKerbal.experienceLevel);                                
                            }
                            break;

                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pCube1":                        
                        case "ponytail":
                            if (personaliseKerbal_Head != null)
                            {
                                if (personaliseKerbal_Head.lvlToHide_Ponytail <= protoKerbal.experienceLevel)
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = false;
                                }
                                else
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = true;
                                    newTexture = personaliseKerbal_Head.get_headTexture(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Head.get_headTextureNRM(protoKerbal.experienceLevel);                                    
                                }
                            }
                            break;
                        case "tongue":
                           // Util.log("+++++++++++++++++++++++  TONGUE of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            /*smr.GetComponentInChildren<Renderer>().enabled = true;
                            if (personaliseKerbal_Head != null)
                            {
                                if ((int)protoKerbal.gender == 1)
                                {
                                    newTexture = defaulMaleAndFemaleHeads[0].get_headTexture(protoKerbal.experienceLevel);
                                    newNormalMap = defaulMaleAndFemaleHeads[0].get_headTextureNRM(protoKerbal.experienceLevel);
                                }
                                else
                                {
                                    newTexture = personaliseKerbal_Head.get_headTexture(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Head.get_headTextureNRM(protoKerbal.experienceLevel);
                                }
                            }*/
                            break;

                        case "upTeeth01":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth01":
                           // Util.log("+++++++++++++++++++++++  upTeeth01 of {0} ++++++++++++++++++++++++", protoKerbal.name);  
                            if (personaliseKerbal_Head != null)
                            {
                                if (personaliseKerbal_Head.lvlToHide_TeethUp <= protoKerbal.experienceLevel)
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = false;
                                }
                                else
                                {
                                    smr.GetComponentInChildren<Renderer>().enabled = true;
                                    if ((int)protoKerbal.gender == 1)
                                    {
                                        newTexture = defaulMaleAndFemaleHeads[0].get_headTexture(protoKerbal.experienceLevel);
                                        newNormalMap = defaulMaleAndFemaleHeads[0].get_headTextureNRM(protoKerbal.experienceLevel);
                                    }
                                    else
                                    {
                                        newTexture = personaliseKerbal_Head.get_headTexture(protoKerbal.experienceLevel);
                                        newNormalMap = personaliseKerbal_Head.get_headTextureNRM(protoKerbal.experienceLevel);
                                    }
                                }                                
                            }
                            break;
                        case "upTeeth02":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth02":
                            //Util.log("+++++++++++++++++++++++  upTeeth02 of {0} ++++++++++++++++++++++++", protoKerbal.name);   
                            if (personaliseKerbal_Head.lvlToHide_TeethUp <= protoKerbal.experienceLevel)
                            {
                                smr.GetComponentInChildren<Renderer>().enabled = false;
                            }
                            else
                            {
                                smr.GetComponentInChildren<Renderer>().enabled = true;
                                if ((int)protoKerbal.gender == 1)
                                {
                                    newTexture = defaulMaleAndFemaleHeads[0].get_headTexture(protoKerbal.experienceLevel);
                                    newNormalMap = defaulMaleAndFemaleHeads[0].get_headTextureNRM(protoKerbal.experienceLevel);
                                }
                                else
                                {
                                    newTexture = personaliseKerbal_Head.get_headTexture(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Head.get_headTextureNRM(protoKerbal.experienceLevel);
                                }
                            }
                            break;
                                                                    
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth01":                           
                        case "downTeeth01":
                            //Util.log("+++++++++++++++++++++++  downTeeth01 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            if (personaliseKerbal_Head.lvlToHide_TeethDown <= protoKerbal.experienceLevel)
                            {
                                smr.GetComponentInChildren<Renderer>().enabled = false;
                            }
                            else
                            {
                                smr.GetComponentInChildren<Renderer>().enabled = true;
                                if ((int)protoKerbal.gender == 1)
                                {
                                    newTexture = defaulMaleAndFemaleHeads[0].get_headTexture(protoKerbal.experienceLevel);
                                    newNormalMap = defaulMaleAndFemaleHeads[0].get_headTextureNRM(protoKerbal.experienceLevel);
                                }
                                else
                                {
                                    newTexture = personaliseKerbal_Head.get_headTexture(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Head.get_headTextureNRM(protoKerbal.experienceLevel);
                                }
                            }
                            break;

                        case "grp_mesh_bowTie01":
                        case "mesh_bowTie01":
                            Util.log(" pouet grp_mesh_bowTie01");
                            smr.transform.position = baseModel.transform.position;
                            smr.transform.rotation = baseModel.transform.rotation;
                            smr.GetComponentInChildren<Renderer>().enabled = true;
                            break;

                        case "hand_right01":
                            Util.log(" pouet hand_right01");
                            smr.transform.position = baseModel.transform.position;
                            smr.transform.rotation = baseModel.transform.rotation;
                            smr.GetComponentInChildren<Renderer>().enabled = true;
                            break;

                        case "hand_left01":
                            Util.log(" pouet handleft01");
                            smr.transform.position = baseModel.transform.position;
                            smr.transform.rotation = baseModel.transform.rotation;
                            smr.GetComponentInChildren<Renderer>().enabled = true;
                            break;

                        case "mortimer_body":
                        case "mortimerTRR":
                           // Util.log(" pouet mortimer_body ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            smr.transform.position = baseModel.transform.position;
                            smr.transform.rotation = baseModel.transform.rotation;
                            smr.GetComponentInChildren<Renderer>().enabled = true;

                            break;

                        case "body01":
                        case "mesh_female_kerbalAstronaut01_body01":

                            smr.GetComponentInChildren<Renderer>().enabled = false;

                            //smr = replacer.mortimer_body01_smr;

                            //                             Util.log(" body01 Root bone name : {0} +++", smr.rootBone.name);
                            // 
                            //                             foreach (var bone in smr.bones)
                            //                             {
                            //                                 Util.log("body01 bone name : {0}", bone.name);
                            //                             }

                            //smr.sharedMesh = replacer.scientist_mesh;
                            // Mesh mesh = smr.sharedMesh;
                            // mesh = replacer.scientist_mesh;
                            //smr.transform.position = replacer.mortimer_body01_smr.transform.position;
                            //smr.transform.rotation = replacer.mortimer_body01_smr.transform.rotation;


                            //smr.bones = replacer.mortimer_body01_smr.bones;


                            //                             smr.sharedMesh.vertices = replacer.scientist_smr.sharedMesh.vertices;
                            //                             smr.sharedMesh.uv = replacer.scientist_smr.sharedMesh.uv;
                            //                             smr.sharedMesh.triangles = replacer.scientist_smr.sharedMesh.triangles;
                            //                             smr.sharedMesh.boneWeights = replacer.scientist_smr.sharedMesh.boneWeights;
                            //                             smr.sharedMesh.normals = replacer.scientist_smr.sharedMesh.normals;


                            if (personaliseKerbal_Suit != null)
                            {   
                                if (isEva) // if out of the vehicle
                                {
                                    if (needsEVASuit && needsEVAgroundSuit) // if on the ground without atmo
                                    {                                        
                                        suit_Selector.select_suit_EvaGround_NoAtmo(out newTexture, out newNormalMap);
                                        break;  
                                    }
                                    else if (needsEVASuit) // if in space
                                    {
                                        suit_Selector.select_suit_EvaSpace(out newTexture, out newNormalMap);
                                        break;                                        
                                    }
                                    else // if on the ground with atmo
                                    {
                                        suit_Selector.select_suit_EvaGround_Atmo(out newTexture, out newNormalMap);
                                        break;                                        
                                    }
                                }
                                else // if in vehicle
                                {
                                    if (needsEVASuit) // if in vehicle unsafe
                                    {
                                        suit_Selector.select_suit_IvaUnsafe(out newTexture, out newNormalMap);
                                        break;
                                    }
                                    else // if in vehicle safe
                                    {
                                        suit_Selector.select_suit_IvaSafe(out newTexture, out newNormalMap);
                                        break;
                                    }                                                                      
                                }
                            }
                            break;

                        case "helmet":
                        case "mesh_female_kerbalAstronaut01_helmet":
                            
                            // Textures have to be replaced even when hidden since it may become visible later on situation change.
                            if (personaliseKerbal_Suit != null)
                            {
                                if (isEva) // if out of the vehicle
                                {
                                    if (needsEVASuit && needsEVAgroundSuit) // if on the ground without atmo
                                    {     
                                        if (personaliseKerbal_Suit.helmet_EvaGround_NoAtmo > 2) // hide the helmet 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            break;
                                        }                                            
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = helmetMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_helmet_EvaGround_NoAtmo(out newTexture, out newNormalMap);
                                            break;
                                        }  
                                        
                                    }
                                    else if (needsEVASuit) // if in space
                                    {
                                        if (personaliseKerbal_Suit.helmet_EvaSpace > 2)// hide the helmet 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            break;
                                        }
                                        else
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = helmetMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_helmet_EvaSpace(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                    else // if on the ground with atmo
                                    {
                                        if (personaliseKerbal_Suit.helmet_EvaGround_Atmo > 2) // hide the helmet 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = helmetMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_helmet_EvaGround_Atmo(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                }
                                else // if in vehicle
                                {
                                    if (needsEVASuit) // if in vehicle unsafe
                                    {
                                        if (personaliseKerbal_Suit.helmet_Iva_Unsafe > 2) // hide the helmet 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh =  null;                                            
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = helmetMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_helmet_Iva_Unsafe(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                    else // if in vehicle safe
                                    {
                                        if (personaliseKerbal_Suit.helmet_Iva_Safe > 2) // hide the helmet 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = helmetMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_helmet_Iva_Safe(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                }                
                            }
                            break;

                        case "visor":
                        case "mesh_female_kerbalAstronaut01_visor":

                            if (personaliseKerbal_Suit != null)
                            {
                                if (isEva) // if out of the vehicle
                                {
                                    if (needsEVASuit && needsEVAgroundSuit) // if on the ground without atmo
                                    {
                                        if (personaliseKerbal_Suit.visor_EvaGround_NoAtmo > 2) // hide the visor 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            useVisor = false;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = visorMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_visor_EvaGround_NoAtmo(out newTexture, out newNormalMap, out visorReflectionColor, out visorBasecolor);
                                            smr.sharedMaterial.color = visorBasecolor;                                                                                    
                                            useVisor = true;
                                            break;
                                        }

                                    }
                                    else if (needsEVASuit) // if in space
                                    {
                                        if (personaliseKerbal_Suit.visor_EvaSpace > 2)
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            useVisor = false;
                                            break;
                                        }
                                        else
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = visorMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_visor_EvaSpace(out newTexture, out newNormalMap, out visorReflectionColor, out visorBasecolor);
                                            smr.sharedMaterial.color = visorBasecolor;
                                            useVisor = true;
                                            break;
                                        }
                                    }
                                    else // if on the ground with atmo
                                    {
                                        if (personaliseKerbal_Suit.visor_EvaGround_Atmo > 2) // hide the visor 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            useVisor = false;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = visorMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_visor_EvaGround_Atmo(out newTexture, out newNormalMap, out visorReflectionColor, out visorBasecolor);
                                            smr.sharedMaterial.color = visorBasecolor;
                                            useVisor = true;
                                            break;
                                        }
                                    }
                                }
                                else // if in vehicle
                                {
                                    if (needsEVASuit) // if in vehicle unsafe
                                    {
                                        if (personaliseKerbal_Suit.visor_Iva_Unsafe > 2) // hide the visor 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            useVisor = false;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = visorMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_visor_Iva_Unsafe(out newTexture, out newNormalMap, out visorReflectionColor, out visorBasecolor);
                                            smr.sharedMaterial.color = visorBasecolor;
                                            useVisor = true;
                                            break;
                                        }
                                    }
                                    else // if in vehicle safe
                                    {
                                        if (personaliseKerbal_Suit.visor_Iva_Safe > 2) // hide the visor 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            useVisor = false;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = visorMesh[(int)protoKerbal.gender];                                            
                                            suit_Selector.select_visor_Iva_Safe(out newTexture, out newNormalMap, out visorReflectionColor, out visorBasecolor);
                                            smr.sharedMaterial.color = visorBasecolor;
                                            useVisor = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        

                        case "jetpack":
                        case "mesh_female_kerbalAstronaut01_jetpack":

                            if (personaliseKerbal_Suit != null)
                            {
                                if (isEva) // if out of the vehicle
                                {
                                    if (needsEVASuit && needsEVAgroundSuit) // if on the ground without atmo
                                    {
                                        if (personaliseKerbal_Suit.jetpack_EvaGround_NoAtmo > 1) // hide the jetpack 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = jetpackMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_jetpack_EvaGround_NoAtmo(out newTexture, out newNormalMap);
                                            break;
                                        }

                                    }
                                    else if (needsEVASuit) // if in space
                                    {
                                        if (personaliseKerbal_Suit.jetpack_EvaSpace > 1)
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            break;
                                        }
                                        else
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = jetpackMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_jetpack_EvaSpace(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                    else // if on the ground with atmo
                                    {
                                        if (personaliseKerbal_Suit.jetpack_EvaGround_Atmo > 1) // hide the jetpack 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = jetpackMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_jetpack_EvaGround_Atmo(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                }                                
                            }
                            break;

                            

                        default: // Jetpack.

                            if (personaliseKerbal_Suit != null)
                            {
                                if (isEva) // if out of the vehicle
                                {
                                    if (needsEVASuit && needsEVAgroundSuit) // if on the ground without atmo
                                    {
                                        if (personaliseKerbal_Suit.jetpack_EvaGround_NoAtmo > 1) // hide the jetpack 
                                        {
                                            smr.enabled = false;
                                            //smr.sharedMesh = null;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            suit_Selector.select_jetpack_EvaGround_NoAtmo(out newTexture, out newNormalMap);
                                            break;
                                        }

                                    }
                                    else if (needsEVASuit) // if in space
                                    {
                                        if (personaliseKerbal_Suit.jetpack_EvaSpace > 1)
                                        {
                                            smr.enabled = false;
                                            //smr.sharedMesh = null;
                                            break;
                                        }
                                        else
                                        {
                                            smr.enabled = true;
                                            suit_Selector.select_jetpack_EvaSpace(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                    else // if on the ground with atmo
                                    {
                                        if (personaliseKerbal_Suit.jetpack_EvaGround_Atmo > 1) // hide the jetpack 
                                        {
                                            smr.enabled = false;
                                            //smr.sharedMesh = null;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            suit_Selector.select_jetpack_EvaGround_Atmo(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                            
                    }

                    if (newTexture != null)
                        material.mainTexture = newTexture;

                    if (newNormalMap != null)
                        material.SetTexture(Util.BUMPMAP_PROPERTY, newNormalMap);
                }
            }

            hasVisor = useVisor;
            visorReflection_Color = visorReflectionColor;
        }
        
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Personalize Kerbals in an internal space of a vessel. Used by <see cref="TRR_IvaModule"/>
        /// </summary>
        /// <param name="kerbal">The kerbal we want to personalize</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void personaliseIva(Kerbal kerbal, out bool hasVisor)
        {            
            bool needsSuit = !isSituationSafe(kerbal.InVessel);

            Personaliser personaliser = Personaliser.instance;
            Color32 visorReflectionColor = new Color32(128, 128, 128, 255);

            personaliseKerbal(kerbal, kerbal.protoCrewMember, kerbal.InPart, needsSuit, false, 0, out hasVisor, out visorReflectionColor, false);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method to show or hide the helmet and visor. Called on vessel state change event.
        /// </summary>
        /// <param name="action">The game event handler for the change of situation of the vessel</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void updateHelmets(GameEvents.HostedFromToAction<Vessel, Vessel.Situations> action)
        {
            Vessel vessel = action.host;
            
            if (vessel == null)
                return;

            foreach (Part part in vessel.parts.Where(p => p.internalModel != null))
            {
                Kerbal[] kerbals = part.internalModel.GetComponentsInChildren<Kerbal>();
                             

                if (kerbals.Length != 0)
                {
                    bool hideHelmets = isSituationSafe(vessel);

                    foreach (Kerbal kerbal in kerbals.Where(k => k.showHelmet))
                    {

                        KerbalData kerbalData = getKerbalData(kerbal.protoCrewMember);
                        Suit_Set suit = getKerbalSuit(kerbal.protoCrewMember, kerbalData);

                        // `Kerbal.ShowHelmet(false)` irreversibly removes a helmet while
                        // `Kerbal.ShowHelmet(true)` has no effect at all. We need the following workaround.
                        foreach (SkinnedMeshRenderer smr in kerbal.helmetTransform.GetComponentsInChildren<SkinnedMeshRenderer>())
                        {
                            if (smr.name.EndsWith("helmet", StringComparison.Ordinal))
                                smr.sharedMesh = hideHelmets ? null : helmetMesh[(int)kerbal.protoCrewMember.gender];
                            else if (smr.name.EndsWith("visor", StringComparison.Ordinal))
                                smr.sharedMesh = hideHelmets ? null : visorMesh[(int)kerbal.protoCrewMember.gender];
                        }
                    }
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Set external IVA//EVAground/EVAspace suits by first testing the logic and <see cref="isAtmBreathable"/> . 
        /// Then call <see cref="personaliseKerbal(Component, ProtoCrewMember, Part, bool, bool)"/> and return the selected suit number.
        /// <para>The different codes send are used to maintain the logic to switch suits. (IVA->EVAground->EVAspace)</para>
        /// <para>Does a loop between EVA->EVAground suits outside of breathable atmosphere.</para>
        /// This function is used by <see cref="Personaliser.TRR_EvaModule"/>.
        /// </summary>
        /// <param name="evaPart">The <see cref="Part"/> to which this <see cref="PartModule"/> is attached. Use this to reference the part from your module code. </param>
        /// <param name="suitSelection">The actual selection of the suit before the test and configuration. The suit selection goes like this : 
        /// <para>0 = IVA suit</para>
        /// <para>1 = EVA ground suit</para>
        /// <para>2 = EVA space suit</para></param>
        /// <returns>The selected suit set after the <see cref="isAtmBreathable"/> test</returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private int personaliseEva(Part evaPart, int suitSelection, out bool hasVisor, out Color32 visorReflectionColor, bool initialisation)
        {
            int selection = suitSelection;
            bool evaSuit = false;
            bool evaGroundSuit = false;
            bool useVisor = true;
            Color32 reflectionColor = new Color32(128, 128, 128, 255);
            Personaliser personaliser = Personaliser.instance;

            List<ProtoCrewMember> crew = evaPart.protoModuleCrew;         
           
            if (crew.Count != 0)
            {   
                switch (selection)
                {
                    case 0:  //IVA suit, if no air switch to state 1 : EVAground
                        if (!personaliser.isAtmBreathable())                            
                        {
                            if (isNewSuitStateEnabled)
                            {
                                if (personaliser.isUnderSubOrbit(evaPart.vessel)) // if no air and under suborbit : state 1 : EVA ground
                                {
                                    evaSuit = true;
                                    evaGroundSuit = true;
                                    selection = 1;
                                    break;
                                }
                                else // if no air and space : sate : 2 EVA space
                                {
                                    evaSuit = true;
                                    evaGroundSuit = false;
                                    selection = 2;
                                    break;
                                }
                            }
                            else // if no air and space : sate : 2 EVA space
                            {
                                evaSuit = true;
                                evaGroundSuit = false;
                                selection = 2;
                                break;
                            }                            
                        }
                        break;
                    case 1: // EVA ground
                        if (isNewSuitStateEnabled)
                        {
                            if (personaliser.isUnderSubOrbit(evaPart.vessel))
                            {
                                evaSuit = true;
                                evaGroundSuit = true;
                                break;
                            }
                            else
                            {
                                evaSuit = true;
                                evaGroundSuit = false;
                                selection = 2;
                                break;
                            }

                        }
                        else
                        {
                            evaSuit = true;
                            evaGroundSuit = true;
                            selection = 2;
                            break;
                        }                        
                    case 2: // EVA
                        evaSuit = true;
                        evaGroundSuit = false;                        
                        break;

                }                
                personaliseKerbal(evaPart, crew[0], null, evaSuit, evaGroundSuit,selection, out useVisor, out reflectionColor, initialisation);
            }
            hasVisor = useVisor;
            visorReflectionColor = reflectionColor;
            return selection;
        }        

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Load per-game custom kerbals mapping.
        /// <para> This list is first loaded from the file @default.cfg</para>
        /// <para>Then this loaded in the persistent.sfs save</para>
        /// </summary>
        /// <param name="node">The <see cref="ConfigNode"/> that contain our saved data</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void loadKerbals(ConfigNode node)
        {
            node = node ?? customKerbalsNode;

            KerbalRoster roster = HighLogic.CurrentGame.CrewRoster;
            
            foreach (ProtoCrewMember ProtoKerbal in roster.Crew.Concat(roster.Tourist).Concat(roster.Unowned))
            {
                if (ProtoKerbal.rosterStatus == ProtoCrewMember.RosterStatus.Dead
                    && ProtoKerbal.type != ProtoCrewMember.KerbalType.Unowned)
                {
                    continue;
                }
                                

                KerbalData kerbalData = getKerbalData(ProtoKerbal);

                string value = node.GetValue(ProtoKerbal.name);
                if (value != null)
                {
                    string[] tokens = Util.splitConfigValue(value);
                    string genderName = tokens.Length >= 1 ? tokens[0] : null;
                    string headName = tokens.Length >= 2 ? tokens[1] : null;
                    string suitName = tokens.Length >= 3 ? tokens[2] : null;

                    if (genderName != null)
                        kerbalData.gender = genderName == "F" ? 1 : 0;
                                        
                    if (headName != null )
                    {                        
                        if (headName != "GENERIC")
                        {
                            if (headName == "DEFAULT_MALE" || headName == "DEFAULT_FEMALE")
                            {
                                kerbalData.head = defaulMaleAndFemaleHeads[(int)ProtoKerbal.gender];                                
                            }
                            else
                            {                                
                                bool headIsInTheDB = KerbalHeadsDB_full.Exists(h => h.name == headName);

                                if (headIsInTheDB)
                                {
                                    kerbalData.head = KerbalHeadsDB_full.Find(h => h.name == headName);
                                }
                                else
                                {
                                    kerbalData.head = getKerbalHead(ProtoKerbal, kerbalData);
                                    headName = kerbalData.head.name;
                                }
                            }
                        }
                        else
                        {
                            kerbalData.head = getKerbalHead(ProtoKerbal, kerbalData);
                            headName = kerbalData.head.name;
                        }
                    }
                    else
                    {
                        kerbalData.head = getKerbalHead(ProtoKerbal, kerbalData);
                        headName = kerbalData.head.name;
                    }
                    
                    if (!KerbalAndTheirHeadsDB.ContainsKey(ProtoKerbal.name))
                        KerbalAndTheirHeadsDB.Add(ProtoKerbal.name, headName);
                    else
                    {
                        KerbalAndTheirHeadsDB.Remove(ProtoKerbal.name);
                        KerbalAndTheirHeadsDB.Add(ProtoKerbal.name, headName);
                    }
                     
                    if (suitName != null)
                    {
                        if (suitName != "GENERIC")
                        {
                            if (suitName == "DEFAULT_SUIT")
                            {
                                kerbalData.suit = defaultSuit;
                            }
                            else
                            {
                                bool suitIsInTheDB = KerbalSuitsDB_full.Exists(s => s.name == suitName);

                                if (suitIsInTheDB)
                                {
                                    kerbalData.suit = KerbalSuitsDB_full.Find(s => s.name == suitName);
                                }
                                else
                                {
                                    kerbalData.suit = getKerbalSuit(ProtoKerbal, kerbalData);
                                    suitName = kerbalData.suit.name;
                                }
                            }
                        }
                        else
                        {
                            kerbalData.suit = getKerbalSuit(ProtoKerbal, kerbalData);
                            suitName = kerbalData.suit.name;
                        }
                    }
                    else
                    {
                        kerbalData.suit = getKerbalSuit(ProtoKerbal, kerbalData);
                        suitName = kerbalData.suit.name;
                    }

                    ProtoKerbal.gender = forceLegacyFemales ? ProtoCrewMember.Gender.Male : (ProtoCrewMember.Gender)kerbalData.gender;                    
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Save per-game custom Kerbals mapping.        
        /// <para>Then this is saved in the persistent.sfs save</para>
        /// </summary>
        /// <param name="node">The <see cref="ConfigNode"/> that contain our saved data</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void saveKerbals(ConfigNode node)
        {
            KerbalRoster roster = HighLogic.CurrentGame.CrewRoster;

            foreach (ProtoCrewMember kerbal in roster.Crew.Concat(roster.Tourist).Concat(roster.Unowned))
            {
                if (kerbal.rosterStatus == ProtoCrewMember.RosterStatus.Dead
                    && kerbal.type != ProtoCrewMember.KerbalType.Unowned)
                {
                    continue;
                }

                KerbalData kerbalData = getKerbalData(kerbal);
                Head_Set headSet = getKerbalHead(kerbal, kerbalData);
                Suit_Set suitSet = getKerbalSuit(kerbal, kerbalData);

                string genderName = kerbalData.gender == 0 ? "M" : "F";

                node.AddValue(kerbal.name, genderName + " " + headSet.name + " " + suitSet.name);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used to load the mapping of our suit databases
        /// </summary>
        /// <param name="node">The <see cref="ConfigNode"/> that contain our saved data</param>
        /// <param name="map">The database we want to map</param>
        /// <param name="defaultMap">If this one is not empty, map the value from here to the other parameter</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void loadSuitMap(ConfigNode node, IDictionary<string, Suit_Set> map, IDictionary<string, Suit_Set> defaultMap = null)
        {
            if (node == null)
            {
                if (defaultMap != null)
                {
                    foreach (var entry in defaultMap)
                        map[entry.Key] = entry.Value;
                }
            }
            else
            {
                foreach (ConfigNode.Value entry in node.values)
                {
                    map.Remove(entry.name);

                    string suitName = entry.value;

                    if (suitName != null && suitName != "GENERIC")
                    {
                        if (suitName == "DEFAULT_SUIT")
                        {
                            map[entry.name] = defaultSuit;
                        }
                        else
                        {
                            Suit_Set suit = KerbalSuitsDB_full.Find(s => s.name == suitName);
                            if (suit != null)
                                map[entry.name] = suit;
                        }
                    }
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used to save the mapping of our suit databases
        /// </summary>
        /// <param name="map">The database we want to map</param>
        /// <param name="node">The <see cref="ConfigNode"/> that contain our saved data</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private static void saveSuitMap(Dictionary<string, Suit_Set> map, ConfigNode node)
        {
            foreach (var entry in map)
            {
                string suitName = entry.Value == null ? "GENERIC" : entry.Value.name;

                node.AddValue(entry.Key, suitName);
            }
        }

        public void loadSuitConfig(ConfigNode node, List<Suit_Set> map, Suit_Set defaultMap, bool reset)
        {
            //Util.log("loadSuitConfig");
            bool hasCfgEntry = false;

            ConfigNode defaultNode = new ConfigNode();
            if (node.TryGetNode("DEFAULT_SUIT", ref defaultNode))
            {
                Util.log("Settings found for {0}, using them", defaultMap.name);
                Color32 nodeColor = new Color32(255, 255, 255, 255);
                int nodeInt = 0;
                bool nodebool = true;

                if (defaultNode.TryGetValue("suit_Iva_Safe", ref nodeInt))
                    defaultMap.suit_Iva_Safe = nodeInt;
                if (defaultNode.TryGetValue("suit_Iva_Unsafe", ref nodeInt))
                    defaultMap.suit_Iva_Unsafe = nodeInt;
                if (defaultNode.TryGetValue("suit_EvaGround_Atmo", ref nodeInt))
                    defaultMap.suit_EvaGround_Atmo = nodeInt;
                if (defaultNode.TryGetValue("suit_EvaGround_NoAtmo", ref nodeInt))
                    defaultMap.suit_EvaGround_NoAtmo = nodeInt;
                if (defaultNode.TryGetValue("suit_EvaSpace", ref nodeInt))
                    defaultMap.suit_EvaSpace = nodeInt;

                if (defaultNode.TryGetValue("helmet_Iva_Safe", ref nodeInt))
                    defaultMap.helmet_Iva_Safe = nodeInt;
                if (defaultNode.TryGetValue("helmet_Iva_Unsafe", ref nodeInt))
                    defaultMap.helmet_Iva_Unsafe = nodeInt;
                if (defaultNode.TryGetValue("helmet_EvaGround_Atmo", ref nodeInt))
                    defaultMap.helmet_EvaGround_Atmo = nodeInt;
                if (defaultNode.TryGetValue("helmet_EvaGround_NoAtmo", ref nodeInt))
                    defaultMap.helmet_EvaGround_NoAtmo = nodeInt;
                if (defaultNode.TryGetValue("helmet_EvaSpace", ref nodeInt))
                    defaultMap.helmet_EvaSpace = nodeInt;

                if (defaultNode.TryGetValue("visor_Iva_Safe", ref nodeInt))
                    defaultMap.visor_Iva_Safe = nodeInt;
                if (defaultNode.TryGetValue("visor_Iva_Unsafe", ref nodeInt))
                    defaultMap.visor_Iva_Unsafe = nodeInt;
                if (defaultNode.TryGetValue("visor_EvaGround_Atmo", ref nodeInt))
                    defaultMap.visor_EvaGround_Atmo = nodeInt;
                if (defaultNode.TryGetValue("visor_EvaGround_NoAtmo", ref nodeInt))
                    defaultMap.visor_EvaGround_NoAtmo = nodeInt;
                if (defaultNode.TryGetValue("visor_EvaSpace", ref nodeInt))
                    defaultMap.visor_EvaSpace = nodeInt;

                if (defaultNode.TryGetValue("jetpack_EvaGround_Atmo", ref nodeInt))
                    defaultMap.jetpack_EvaGround_Atmo = nodeInt;
                if (defaultNode.TryGetValue("jetpack_EvaGround_NoAtmo", ref nodeInt))
                    defaultMap.jetpack_EvaGround_NoAtmo = nodeInt;
                if (defaultNode.TryGetValue("jetpack_EvaSpace", ref nodeInt))
                    defaultMap.jetpack_EvaSpace = nodeInt;

                if (defaultNode.TryGetValue("visor_Iva_ReflectionAdaptive", ref nodebool))
                    defaultMap.visor_Iva_ReflectionAdaptive = nodebool;
                if (defaultNode.TryGetValue("visor_EvaGround_ReflectionAdaptive", ref nodebool))
                    defaultMap.visor_EvaGround_ReflectionAdaptive = nodebool;
                if (defaultNode.TryGetValue("visor_EvaSpace_ReflectionAdaptive", ref nodebool))
                    defaultMap.visor_EvaSpace_ReflectionAdaptive = nodebool;

                if (defaultNode.TryGetValue("visor_Iva_BaseColor[0]", ref nodeColor))
                    defaultMap.visor_Iva_BaseColor[0] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_BaseColor[1]", ref nodeColor))
                    defaultMap.visor_Iva_BaseColor[1] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_BaseColor[2]", ref nodeColor))
                    defaultMap.visor_Iva_BaseColor[2] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_BaseColor[3]", ref nodeColor))
                    defaultMap.visor_Iva_BaseColor[3] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_BaseColor[4]", ref nodeColor))
                    defaultMap.visor_Iva_BaseColor[4] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_BaseColor[5]", ref nodeColor))
                    defaultMap.visor_Iva_BaseColor[5] = nodeColor;

                if (defaultNode.TryGetValue("visor_EvaGround_BaseColor[0]", ref nodeColor))
                    defaultMap.visor_EvaGround_BaseColor[0] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_BaseColor[1]", ref nodeColor))
                    defaultMap.visor_EvaGround_BaseColor[1] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_BaseColor[2]", ref nodeColor))
                    defaultMap.visor_EvaGround_BaseColor[2] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_BaseColor[3]", ref nodeColor))
                    defaultMap.visor_EvaGround_BaseColor[3] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_BaseColor[4]", ref nodeColor))
                    defaultMap.visor_EvaGround_BaseColor[4] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_BaseColor[5]", ref nodeColor))
                    defaultMap.visor_EvaGround_BaseColor[5] = nodeColor;

                if (defaultNode.TryGetValue("visor_EvaSpace_BaseColor[0]", ref nodeColor))
                    defaultMap.visor_EvaSpace_BaseColor[0] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_BaseColor[1]", ref nodeColor))
                    defaultMap.visor_EvaSpace_BaseColor[1] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_BaseColor[2]", ref nodeColor))
                    defaultMap.visor_EvaSpace_BaseColor[2] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_BaseColor[3]", ref nodeColor))
                    defaultMap.visor_EvaSpace_BaseColor[3] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_BaseColor[4]", ref nodeColor))
                    defaultMap.visor_EvaSpace_BaseColor[4] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_BaseColor[5]", ref nodeColor))
                    defaultMap.visor_EvaSpace_BaseColor[5] = nodeColor;


                if (defaultNode.TryGetValue("visor_Iva_ReflectionColor[0]", ref nodeColor))
                    defaultMap.visor_Iva_ReflectionColor[0] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_ReflectionColor[1]", ref nodeColor))
                    defaultMap.visor_Iva_ReflectionColor[1] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_ReflectionColor[2]", ref nodeColor))
                    defaultMap.visor_Iva_ReflectionColor[2] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_ReflectionColor[3]", ref nodeColor))
                    defaultMap.visor_Iva_ReflectionColor[3] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_ReflectionColor[4]", ref nodeColor))
                    defaultMap.visor_Iva_ReflectionColor[4] = nodeColor;
                if (defaultNode.TryGetValue("visor_Iva_ReflectionColor[5]", ref nodeColor))
                    defaultMap.visor_Iva_ReflectionColor[5] = nodeColor;

                if (defaultNode.TryGetValue("visor_EvaGround_ReflectionColor[0]", ref nodeColor))
                    defaultMap.visor_EvaGround_ReflectionColor[0] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_ReflectionColor[1]", ref nodeColor))
                    defaultMap.visor_EvaGround_ReflectionColor[1] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_ReflectionColor[2]", ref nodeColor))
                    defaultMap.visor_EvaGround_ReflectionColor[2] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_ReflectionColor[3]", ref nodeColor))
                    defaultMap.visor_EvaGround_ReflectionColor[3] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_ReflectionColor[4]", ref nodeColor))
                    defaultMap.visor_EvaGround_ReflectionColor[4] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaGround_ReflectionColor[5]", ref nodeColor))
                    defaultMap.visor_EvaGround_ReflectionColor[5] = nodeColor;

                if (defaultNode.TryGetValue("visor_EvaSpace_ReflectionColor[0]", ref nodeColor))
                    defaultMap.visor_EvaSpace_ReflectionColor[0] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_ReflectionColor[1]", ref nodeColor))
                    defaultMap.visor_EvaSpace_ReflectionColor[1] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_ReflectionColor[2]", ref nodeColor))
                    defaultMap.visor_EvaSpace_ReflectionColor[2] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_ReflectionColor[3]", ref nodeColor))
                    defaultMap.visor_EvaSpace_ReflectionColor[3] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_ReflectionColor[4]", ref nodeColor))
                    defaultMap.visor_EvaSpace_ReflectionColor[4] = nodeColor;
                if (defaultNode.TryGetValue("visor_EvaSpace_ReflectionColor[5]", ref nodeColor))
                    defaultMap.visor_EvaSpace_ReflectionColor[5] = nodeColor;

                defaultMap.hasLoadedFromConfig = true;

            }

            foreach (Suit_Set suitSet in map)
            {
                if (reset)
                {

                    continue;
                }

                ConfigNode savedNode = new ConfigNode();
                // if the suit set has an entry in the .cfg, try to load the settings, if empty, load the default settings
                if (node.TryGetNode(suitSet.name, ref savedNode))
                {
                    Util.log("Settings found for Suit Set {0}, using them", suitSet.name);
                    hasCfgEntry = true;

                    bool nodebool = true;
                    Color32 nodeColor = new Color32(255, 255, 255, 255);                    
                    int nodeInt = 0;

                    // suit settings
                    /*if (savedNode.TryGetValue("isExclusive", ref nodebool))
                        suitSet.isExclusive = nodebool;
                    else
                        suitSet.isExclusive = defaultMap.isExclusive;*/

                    if (savedNode.TryGetValue("suit_Iva_Safe", ref nodeInt))
                        suitSet.suit_Iva_Safe = nodeInt;
                    else
                        suitSet.suit_Iva_Safe = defaultMap.suit_Iva_Safe;

                    if (savedNode.TryGetValue("suit_Iva_Unsafe", ref nodeInt))
                        suitSet.suit_Iva_Unsafe = nodeInt;
                    else
                        suitSet.suit_Iva_Unsafe = defaultMap.suit_Iva_Unsafe;

                    if (savedNode.TryGetValue("suit_EvaGround_Atmo", ref nodeInt))
                        suitSet.suit_EvaGround_Atmo = nodeInt;
                    else
                        suitSet.suit_EvaGround_Atmo = defaultMap.suit_EvaGround_Atmo;

                    if (savedNode.TryGetValue("suit_EvaGround_NoAtmo", ref nodeInt))
                    {
                        suitSet.suit_EvaGround_NoAtmo = nodeInt;
                        //Util.log("suit_EvaGround_NoAtmo for {0} = {1}", suitSet.name, suitSet.suit_EvaGround_NoAtmo);
                    }
                    else
                        
                    {
                        suitSet.suit_EvaGround_NoAtmo = defaultMap.suit_EvaGround_NoAtmo;
                        //Util.log("USING DEFAULT suit_EvaGround_NoAtmo for {0} = {1}", suitSet.name, suitSet.suit_EvaGround_NoAtmo);
                    }
                    

                    if (savedNode.TryGetValue("suit_EvaSpace", ref nodeInt))
                        suitSet.suit_EvaSpace = nodeInt;
                    else
                        suitSet.suit_EvaSpace = defaultMap.suit_EvaSpace;

                    //helmet settings
                    if (savedNode.TryGetValue("helmet_Iva_Safe", ref nodeInt))
                        suitSet.helmet_Iva_Safe = nodeInt;
                    else
                        suitSet.helmet_Iva_Safe = defaultMap.helmet_Iva_Safe;

                    if (savedNode.TryGetValue("helmet_Iva_Unsafe", ref nodeInt))
                        suitSet.helmet_Iva_Unsafe = nodeInt;
                    else
                        suitSet.helmet_Iva_Unsafe = defaultMap.helmet_Iva_Unsafe;

                    if (savedNode.TryGetValue("helmet_EvaGround_Atmo", ref nodeInt))
                        suitSet.helmet_EvaGround_Atmo = nodeInt;
                    else
                        suitSet.helmet_EvaGround_Atmo = defaultMap.helmet_EvaGround_Atmo;

                    if (savedNode.TryGetValue("helmet_EvaGround_NoAtmo", ref nodeInt))
                        suitSet.helmet_EvaGround_NoAtmo = nodeInt;
                    else
                        suitSet.helmet_EvaGround_NoAtmo = defaultMap.helmet_EvaGround_NoAtmo;

                    if (savedNode.TryGetValue("helmet_EvaSpace", ref nodeInt))
                        suitSet.helmet_EvaSpace = nodeInt;
                    else
                        suitSet.helmet_EvaSpace = defaultMap.helmet_EvaSpace;

                    //visor settings
                    if (savedNode.TryGetValue("visor_Iva_Safe", ref nodeInt))
                        suitSet.visor_Iva_Safe = nodeInt;
                    else
                        suitSet.visor_Iva_Safe = defaultMap.visor_Iva_Safe;

                    if (savedNode.TryGetValue("visor_Iva_Unsafe", ref nodeInt))
                        suitSet.visor_Iva_Unsafe = nodeInt;
                    else
                        suitSet.visor_Iva_Unsafe = defaultMap.visor_Iva_Unsafe;

                    if (savedNode.TryGetValue("visor_EvaGround_Atmo", ref nodeInt))
                        suitSet.visor_EvaGround_Atmo = nodeInt;
                    else
                        suitSet.visor_EvaGround_Atmo = defaultMap.visor_EvaGround_Atmo;

                    if (savedNode.TryGetValue("visor_EvaGround_NoAtmo", ref nodeInt))
                        suitSet.visor_EvaGround_NoAtmo = nodeInt;
                    else
                        suitSet.visor_EvaGround_NoAtmo = defaultMap.visor_EvaGround_NoAtmo;

                    if (savedNode.TryGetValue("visor_EvaSpace", ref nodeInt))
                        suitSet.visor_EvaSpace = nodeInt;
                    else
                        suitSet.visor_EvaSpace = defaultMap.visor_EvaSpace;

                    // jetpack settings
                    if (savedNode.TryGetValue("jetpack_EvaGround_Atmo", ref nodeInt))
                        suitSet.jetpack_EvaGround_Atmo = nodeInt;
                    else
                        suitSet.jetpack_EvaGround_Atmo = defaultMap.jetpack_EvaGround_Atmo;

                    if (savedNode.TryGetValue("jetpack_EvaGround_NoAtmo", ref nodeInt))
                        suitSet.jetpack_EvaGround_NoAtmo = nodeInt;
                    else
                        suitSet.jetpack_EvaGround_NoAtmo = defaultMap.jetpack_EvaGround_NoAtmo;

                    if (savedNode.TryGetValue("jetpack_EvaSpace", ref nodeInt))
                        suitSet.jetpack_EvaSpace = nodeInt;
                    else
                        suitSet.jetpack_EvaSpace = defaultMap.jetpack_EvaSpace;

                    // visor reflection settings
                    if (savedNode.TryGetValue("visor_Iva_ReflectionAdaptive", ref nodebool))
                        suitSet.visor_Iva_ReflectionAdaptive = nodebool;
                    else
                        suitSet.visor_Iva_ReflectionAdaptive = defaultMap.visor_Iva_ReflectionAdaptive;

                    if (savedNode.TryGetValue("visor_EvaGround_ReflectionAdaptive", ref nodebool))
                        suitSet.visor_EvaGround_ReflectionAdaptive = nodebool;
                    else
                        suitSet.visor_EvaGround_ReflectionAdaptive = defaultMap.visor_EvaGround_ReflectionAdaptive;

                    if (savedNode.TryGetValue("visor_EvaSpace_ReflectionAdaptive", ref nodebool))
                        suitSet.visor_EvaSpace_ReflectionAdaptive = nodebool;
                    else
                        suitSet.visor_EvaSpace_ReflectionAdaptive = defaultMap.visor_EvaSpace_ReflectionAdaptive;

                    // visor base color settings
                    if (savedNode.TryGetValue("visor_Iva_BaseColor[0]", ref nodeColor))
                        suitSet.visor_Iva_BaseColor[0] = nodeColor;
                    else
                        suitSet.visor_Iva_BaseColor[0] = defaultMap.visor_Iva_BaseColor[0];

                    if (savedNode.TryGetValue("visor_Iva_BaseColor[1]", ref nodeColor))
                        suitSet.visor_Iva_BaseColor[1] = nodeColor;
                    else
                        suitSet.visor_Iva_BaseColor[1] = defaultMap.visor_Iva_BaseColor[1];

                    if (savedNode.TryGetValue("visor_Iva_BaseColor[2]", ref nodeColor))
                        suitSet.visor_Iva_BaseColor[2] = nodeColor;
                    else
                        suitSet.visor_Iva_BaseColor[2] = defaultMap.visor_Iva_BaseColor[2];

                    if (savedNode.TryGetValue("visor_Iva_BaseColor[3]", ref nodeColor))
                        suitSet.visor_Iva_BaseColor[3] = nodeColor;
                    else
                        suitSet.visor_Iva_BaseColor[3] = defaultMap.visor_Iva_BaseColor[3];

                    if (savedNode.TryGetValue("visor_Iva_BaseColor[4]", ref nodeColor))
                        suitSet.visor_Iva_BaseColor[4] = nodeColor;
                    else
                        suitSet.visor_Iva_BaseColor[4] = defaultMap.visor_Iva_BaseColor[4];

                    if (savedNode.TryGetValue("visor_Iva_BaseColor[5]", ref nodeColor))
                        suitSet.visor_Iva_BaseColor[5] = nodeColor;
                    else
                        suitSet.visor_Iva_BaseColor[5] = defaultMap.visor_Iva_BaseColor[5];


                    if (savedNode.TryGetValue("visor_EvaGround_BaseColor[0]", ref nodeColor))
                        suitSet.visor_EvaGround_BaseColor[0] = nodeColor;
                    else
                        suitSet.visor_EvaGround_BaseColor[0] = defaultMap.visor_EvaGround_BaseColor[0];

                    if (savedNode.TryGetValue("visor_EvaGround_BaseColor[1]", ref nodeColor))
                        suitSet.visor_EvaGround_BaseColor[1] = nodeColor;
                    else
                        suitSet.visor_EvaGround_BaseColor[1] = defaultMap.visor_EvaGround_BaseColor[1];

                    if (savedNode.TryGetValue("visor_EvaGround_BaseColor[2]", ref nodeColor))
                        suitSet.visor_EvaGround_BaseColor[2] = nodeColor;
                    else
                        suitSet.visor_EvaGround_BaseColor[2] = defaultMap.visor_EvaGround_BaseColor[2];

                    if (savedNode.TryGetValue("visor_EvaGround_BaseColor[3]", ref nodeColor))
                        suitSet.visor_EvaGround_BaseColor[3] = nodeColor;
                    else
                        suitSet.visor_EvaGround_BaseColor[3] = defaultMap.visor_EvaGround_BaseColor[3];

                    if (savedNode.TryGetValue("visor_EvaGround_BaseColor[4]", ref nodeColor))
                        suitSet.visor_EvaGround_BaseColor[4] = nodeColor;
                    else
                        suitSet.visor_EvaGround_BaseColor[4] = defaultMap.visor_EvaGround_BaseColor[4];

                    if (savedNode.TryGetValue("visor_EvaGround_BaseColor[5]", ref nodeColor))
                        suitSet.visor_EvaGround_BaseColor[5] = nodeColor;
                    else
                        suitSet.visor_EvaGround_BaseColor[5] = defaultMap.visor_EvaGround_BaseColor[5];


                    if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[0]", ref nodeColor))
                        suitSet.visor_EvaSpace_BaseColor[0] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_BaseColor[0] = defaultMap.visor_EvaSpace_BaseColor[0];

                    if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[1]", ref nodeColor))
                        suitSet.visor_EvaSpace_BaseColor[1] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_BaseColor[1] = defaultMap.visor_EvaSpace_BaseColor[1];

                    if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[2]", ref nodeColor))
                        suitSet.visor_EvaSpace_BaseColor[2] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_BaseColor[2] = defaultMap.visor_EvaSpace_BaseColor[2];

                    if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[3]", ref nodeColor))
                        suitSet.visor_EvaSpace_BaseColor[3] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_BaseColor[3] = defaultMap.visor_EvaSpace_BaseColor[3];

                    if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[4]", ref nodeColor))
                        suitSet.visor_EvaSpace_BaseColor[4] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_BaseColor[4] = defaultMap.visor_EvaSpace_BaseColor[4];

                    if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[5]", ref nodeColor))
                        suitSet.visor_EvaSpace_BaseColor[5] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_BaseColor[5] = defaultMap.visor_EvaSpace_BaseColor[5];



                    // visor reflection color settings
                    if (savedNode.TryGetValue("visor_Iva_ReflectionColor[0]", ref nodeColor))
                        suitSet.visor_Iva_ReflectionColor[0] = nodeColor;
                    else
                        suitSet.visor_Iva_ReflectionColor[0] = defaultMap.visor_Iva_ReflectionColor[0];

                    if (savedNode.TryGetValue("visor_Iva_ReflectionColor[1]", ref nodeColor))
                        suitSet.visor_Iva_ReflectionColor[1] = nodeColor;
                    else
                        suitSet.visor_Iva_ReflectionColor[1] = defaultMap.visor_Iva_ReflectionColor[1];

                    if (savedNode.TryGetValue("visor_Iva_ReflectionColor[2]", ref nodeColor))
                        suitSet.visor_Iva_ReflectionColor[2] = nodeColor;
                    else
                        suitSet.visor_Iva_ReflectionColor[2] = defaultMap.visor_Iva_ReflectionColor[2];

                    if (savedNode.TryGetValue("visor_Iva_ReflectionColor[3]", ref nodeColor))
                        suitSet.visor_Iva_ReflectionColor[3] = nodeColor;
                    else
                        suitSet.visor_Iva_ReflectionColor[3] = defaultMap.visor_Iva_ReflectionColor[3];

                    if (savedNode.TryGetValue("visor_Iva_ReflectionColor[4]", ref nodeColor))
                        suitSet.visor_Iva_ReflectionColor[4] = nodeColor;
                    else
                        suitSet.visor_Iva_ReflectionColor[4] = defaultMap.visor_Iva_ReflectionColor[4];

                    if (savedNode.TryGetValue("visor_Iva_ReflectionColor[5]", ref nodeColor))
                        suitSet.visor_Iva_ReflectionColor[5] = nodeColor;
                    else
                        suitSet.visor_Iva_ReflectionColor[5] = defaultMap.visor_Iva_ReflectionColor[5];


                    if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[0]", ref nodeColor))
                        suitSet.visor_EvaGround_ReflectionColor[0] = nodeColor;
                    else
                        suitSet.visor_EvaGround_ReflectionColor[0] = defaultMap.visor_EvaGround_ReflectionColor[0];

                    if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[1]", ref nodeColor))
                        suitSet.visor_EvaGround_ReflectionColor[1] = nodeColor;
                    else
                        suitSet.visor_EvaGround_ReflectionColor[1] = defaultMap.visor_EvaGround_ReflectionColor[1];

                    if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[2]", ref nodeColor))
                        suitSet.visor_EvaGround_ReflectionColor[2] = nodeColor;
                    else
                        suitSet.visor_EvaGround_ReflectionColor[2] = defaultMap.visor_EvaGround_ReflectionColor[2];

                    if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[3]", ref nodeColor))
                        suitSet.visor_EvaGround_ReflectionColor[3] = nodeColor;
                    else
                        suitSet.visor_EvaGround_ReflectionColor[3] = defaultMap.visor_EvaGround_ReflectionColor[3];

                    if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[4]", ref nodeColor))
                        suitSet.visor_EvaGround_ReflectionColor[4] = nodeColor;
                    else
                        suitSet.visor_EvaGround_ReflectionColor[4] = defaultMap.visor_EvaGround_ReflectionColor[4];

                    if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[5]", ref nodeColor))
                        suitSet.visor_EvaGround_ReflectionColor[5] = nodeColor;
                    else
                        suitSet.visor_EvaGround_ReflectionColor[5] = defaultMap.visor_EvaGround_ReflectionColor[5];


                    if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[0]", ref nodeColor))
                        suitSet.visor_EvaSpace_ReflectionColor[0] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_ReflectionColor[0] = defaultMap.visor_EvaSpace_ReflectionColor[0];

                    if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[1]", ref nodeColor))
                        suitSet.visor_EvaSpace_ReflectionColor[1] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_ReflectionColor[1] = defaultMap.visor_EvaSpace_ReflectionColor[1];

                    if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[2]", ref nodeColor))
                        suitSet.visor_EvaSpace_ReflectionColor[2] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_ReflectionColor[2] = defaultMap.visor_EvaSpace_ReflectionColor[2];

                    if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[3]", ref nodeColor))
                        suitSet.visor_EvaSpace_ReflectionColor[3] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_ReflectionColor[3] = defaultMap.visor_EvaSpace_ReflectionColor[3];

                    if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[4]", ref nodeColor))
                        suitSet.visor_EvaSpace_ReflectionColor[4] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_ReflectionColor[4] = defaultMap.visor_EvaSpace_ReflectionColor[4];

                    if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[5]", ref nodeColor))
                        suitSet.visor_EvaSpace_ReflectionColor[5] = nodeColor;
                    else
                        suitSet.visor_EvaSpace_ReflectionColor[5] = defaultMap.visor_EvaSpace_ReflectionColor[5];

                    suitSet.hasLoadedFromConfig = true;
                }
                // if the suit set has no entry in the .cfg, load the default settings
                if (hasCfgEntry == false)
                {
                    if (suitSet.hasLoadedFromConfig == false)
                    {
                        //Util.log("Settings NOT found for {0}, using default", suitSet.name);
                        //suitSet.isExclusive = defaultMap.isExclusive;

                        // suit settings
                        suitSet.suit_Iva_Safe = defaultMap.suit_Iva_Safe;
                        suitSet.suit_Iva_Unsafe = defaultMap.suit_Iva_Unsafe;
                        suitSet.suit_EvaGround_Atmo = defaultMap.suit_EvaGround_Atmo;
                        suitSet.suit_EvaGround_NoAtmo = defaultMap.suit_EvaGround_NoAtmo;
                       // Util.log("suit_EvaGround_NoAtmo for {0} = {1}", suitSet.name, suitSet.suit_EvaGround_NoAtmo);
                        suitSet.suit_EvaSpace = defaultMap.suit_EvaSpace;

                        //helmet settings
                        suitSet.helmet_Iva_Safe = defaultMap.helmet_Iva_Safe;
                        suitSet.helmet_Iva_Unsafe = defaultMap.helmet_Iva_Unsafe;
                        suitSet.helmet_EvaGround_Atmo = defaultMap.helmet_EvaGround_Atmo;
                        suitSet.helmet_EvaGround_NoAtmo = defaultMap.helmet_EvaGround_NoAtmo;
                        suitSet.helmet_EvaSpace = defaultMap.helmet_EvaSpace;

                        //visor settings
                        suitSet.visor_Iva_Safe = defaultMap.visor_Iva_Safe;
                        suitSet.visor_Iva_Unsafe = defaultMap.visor_Iva_Unsafe;
                        suitSet.visor_EvaGround_Atmo = defaultMap.visor_EvaGround_Atmo;
                        suitSet.visor_EvaGround_NoAtmo = defaultMap.visor_EvaGround_NoAtmo;
                        suitSet.visor_EvaSpace = defaultMap.visor_EvaSpace;

                        // jetpack settings
                        suitSet.jetpack_EvaGround_Atmo = defaultMap.jetpack_EvaGround_Atmo;
                        suitSet.jetpack_EvaGround_NoAtmo = defaultMap.jetpack_EvaGround_NoAtmo;
                        suitSet.jetpack_EvaSpace = defaultMap.jetpack_EvaSpace;

                        // visor reflection settings
                        suitSet.visor_Iva_ReflectionAdaptive = defaultMap.visor_Iva_ReflectionAdaptive;
                        suitSet.visor_EvaGround_ReflectionAdaptive = defaultMap.visor_EvaGround_ReflectionAdaptive;
                        suitSet.visor_EvaSpace_ReflectionAdaptive = defaultMap.visor_EvaSpace_ReflectionAdaptive;


                        // visor Base color settings
                        suitSet.visor_Iva_BaseColor[0] = defaultMap.visor_Iva_BaseColor[0];
                        suitSet.visor_Iva_BaseColor[1] = defaultMap.visor_Iva_BaseColor[1];
                        suitSet.visor_Iva_BaseColor[2] = defaultMap.visor_Iva_BaseColor[2];
                        suitSet.visor_Iva_BaseColor[3] = defaultMap.visor_Iva_BaseColor[3];
                        suitSet.visor_Iva_BaseColor[4] = defaultMap.visor_Iva_BaseColor[4];
                        suitSet.visor_Iva_BaseColor[5] = defaultMap.visor_Iva_BaseColor[5];


                        suitSet.visor_EvaGround_BaseColor[0] = defaultMap.visor_EvaGround_BaseColor[0];
                        suitSet.visor_EvaGround_BaseColor[1] = defaultMap.visor_EvaGround_BaseColor[1];
                        suitSet.visor_EvaGround_BaseColor[2] = defaultMap.visor_EvaGround_BaseColor[2];
                        suitSet.visor_EvaGround_BaseColor[3] = defaultMap.visor_EvaGround_BaseColor[3];
                        suitSet.visor_EvaGround_BaseColor[4] = defaultMap.visor_EvaGround_BaseColor[4];
                        suitSet.visor_EvaGround_BaseColor[5] = defaultMap.visor_EvaGround_BaseColor[5];


                        suitSet.visor_EvaSpace_BaseColor[0] = defaultMap.visor_EvaSpace_BaseColor[0];
                        suitSet.visor_EvaSpace_BaseColor[1] = defaultMap.visor_EvaSpace_BaseColor[1];
                        suitSet.visor_EvaSpace_BaseColor[2] = defaultMap.visor_EvaSpace_BaseColor[2];
                        suitSet.visor_EvaSpace_BaseColor[3] = defaultMap.visor_EvaSpace_BaseColor[3];
                        suitSet.visor_EvaSpace_BaseColor[4] = defaultMap.visor_EvaSpace_BaseColor[4];
                        suitSet.visor_EvaSpace_BaseColor[5] = defaultMap.visor_EvaSpace_BaseColor[5];

                        // visor Reflection color settings
                        suitSet.visor_Iva_ReflectionColor[0] = defaultMap.visor_Iva_ReflectionColor[0];
                        suitSet.visor_Iva_ReflectionColor[1] = defaultMap.visor_Iva_ReflectionColor[1];
                        suitSet.visor_Iva_ReflectionColor[2] = defaultMap.visor_Iva_ReflectionColor[2];
                        suitSet.visor_Iva_ReflectionColor[3] = defaultMap.visor_Iva_ReflectionColor[3];
                        suitSet.visor_Iva_ReflectionColor[4] = defaultMap.visor_Iva_ReflectionColor[4];
                        suitSet.visor_Iva_ReflectionColor[5] = defaultMap.visor_Iva_ReflectionColor[5];


                        suitSet.visor_EvaGround_ReflectionColor[0] = defaultMap.visor_EvaGround_ReflectionColor[0];
                        suitSet.visor_EvaGround_ReflectionColor[1] = defaultMap.visor_EvaGround_ReflectionColor[1];
                        suitSet.visor_EvaGround_ReflectionColor[2] = defaultMap.visor_EvaGround_ReflectionColor[2];
                        suitSet.visor_EvaGround_ReflectionColor[3] = defaultMap.visor_EvaGround_ReflectionColor[3];
                        suitSet.visor_EvaGround_ReflectionColor[4] = defaultMap.visor_EvaGround_ReflectionColor[4];
                        suitSet.visor_EvaGround_ReflectionColor[5] = defaultMap.visor_EvaGround_ReflectionColor[5];


                        suitSet.visor_EvaSpace_ReflectionColor[0] = defaultMap.visor_EvaSpace_ReflectionColor[0];
                        suitSet.visor_EvaSpace_ReflectionColor[1] = defaultMap.visor_EvaSpace_ReflectionColor[1];
                        suitSet.visor_EvaSpace_ReflectionColor[2] = defaultMap.visor_EvaSpace_ReflectionColor[2];
                        suitSet.visor_EvaSpace_ReflectionColor[3] = defaultMap.visor_EvaSpace_ReflectionColor[3];
                        suitSet.visor_EvaSpace_ReflectionColor[4] = defaultMap.visor_EvaSpace_ReflectionColor[4];
                        suitSet.visor_EvaSpace_ReflectionColor[5] = defaultMap.visor_EvaSpace_ReflectionColor[5];
                    }

                    
                }
            }

        }

        private static void saveSuitConfig(ConfigNode node, List<Suit_Set> map, Suit_Set defaultMap)
        {
            ConfigNode defaultNode = new ConfigNode();
            node.AddNode("DEFAULT_SUIT", defaultNode);

            //defaultNode.AddValue("isExclusive", defaultMap.isExclusive);

            defaultNode.AddValue("suit_Iva_Safe", defaultMap.suit_Iva_Safe);
            defaultNode.AddValue("suit_Iva_Unsafe", defaultMap.suit_Iva_Unsafe);
            defaultNode.AddValue("suit_EvaGround_Atmo", defaultMap.suit_EvaGround_Atmo);
            defaultNode.AddValue("suit_EvaGround_NoAtmo", defaultMap.suit_EvaGround_NoAtmo);
            defaultNode.AddValue("suit_EvaSpace", defaultMap.suit_EvaSpace);

            defaultNode.AddValue("helmet_Iva_Safe", defaultMap.helmet_Iva_Safe);
            defaultNode.AddValue("helmet_Iva_Unsafe", defaultMap.helmet_Iva_Unsafe);
            defaultNode.AddValue("helmet_EvaGround_Atmo", defaultMap.helmet_EvaGround_Atmo);
            defaultNode.AddValue("helmet_EvaGround_NoAtmo", defaultMap.helmet_EvaGround_NoAtmo);
            defaultNode.AddValue("helmet_EvaSpace", defaultMap.helmet_EvaSpace);

            defaultNode.AddValue("visor_Iva_Safe", defaultMap.visor_Iva_Safe);
            defaultNode.AddValue("visor_Iva_Unsafe", defaultMap.visor_Iva_Unsafe);
            defaultNode.AddValue("visor_EvaGround_Atmo", defaultMap.visor_EvaGround_Atmo);
            defaultNode.AddValue("visor_EvaGround_NoAtmo", defaultMap.visor_EvaGround_NoAtmo);
            defaultNode.AddValue("visor_EvaSpace", defaultMap.visor_EvaSpace);

            defaultNode.AddValue("jetpack_EvaGround_Atmo", defaultMap.jetpack_EvaGround_Atmo);
            defaultNode.AddValue("jetpack_EvaGround_NoAtmo", defaultMap.jetpack_EvaGround_NoAtmo);
            defaultNode.AddValue("jetpack_EvaSpace", defaultMap.jetpack_EvaSpace);

            defaultNode.AddValue("visor_Iva_ReflectionAdaptive", defaultMap.visor_Iva_ReflectionAdaptive);
            defaultNode.AddValue("visor_EvaGround_ReflectionAdaptive", defaultMap.visor_EvaGround_ReflectionAdaptive);
            defaultNode.AddValue("visor_EvaSpace_ReflectionAdaptive", defaultMap.visor_EvaSpace_ReflectionAdaptive);

            defaultNode.AddValue("visor_Iva_BaseColor[0]", defaultMap.visor_Iva_BaseColor[0]);
            defaultNode.AddValue("visor_Iva_BaseColor[1]", defaultMap.visor_Iva_BaseColor[1]);
            defaultNode.AddValue("visor_Iva_BaseColor[2]", defaultMap.visor_Iva_BaseColor[2]);
            defaultNode.AddValue("visor_Iva_BaseColor[3]", defaultMap.visor_Iva_BaseColor[3]);
            defaultNode.AddValue("visor_Iva_BaseColor[4]", defaultMap.visor_Iva_BaseColor[4]);
            defaultNode.AddValue("visor_Iva_BaseColor[5]", defaultMap.visor_Iva_BaseColor[5]);

            defaultNode.AddValue("visor_EvaGround_BaseColor[0]", defaultMap.visor_EvaGround_BaseColor[0]);
            defaultNode.AddValue("visor_EvaGround_BaseColor[1]", defaultMap.visor_EvaGround_BaseColor[1]);
            defaultNode.AddValue("visor_EvaGround_BaseColor[2]", defaultMap.visor_EvaGround_BaseColor[2]);
            defaultNode.AddValue("visor_EvaGround_BaseColor[3]", defaultMap.visor_EvaGround_BaseColor[3]);
            defaultNode.AddValue("visor_EvaGround_BaseColor[4]", defaultMap.visor_EvaGround_BaseColor[4]);
            defaultNode.AddValue("visor_EvaGround_BaseColor[5]", defaultMap.visor_EvaGround_BaseColor[5]);

            defaultNode.AddValue("visor_EvaSpace_BaseColor[0]", defaultMap.visor_EvaSpace_BaseColor[0]);
            defaultNode.AddValue("visor_EvaSpace_BaseColor[1]", defaultMap.visor_EvaSpace_BaseColor[1]);
            defaultNode.AddValue("visor_EvaSpace_BaseColor[2]", defaultMap.visor_EvaSpace_BaseColor[2]);
            defaultNode.AddValue("visor_EvaSpace_BaseColor[3]", defaultMap.visor_EvaSpace_BaseColor[3]);
            defaultNode.AddValue("visor_EvaSpace_BaseColor[4]", defaultMap.visor_EvaSpace_BaseColor[4]);
            defaultNode.AddValue("visor_EvaSpace_BaseColor[5]", defaultMap.visor_EvaSpace_BaseColor[5]);



            defaultNode.AddValue("visor_Iva_ReflectionColor[0]", defaultMap.visor_Iva_ReflectionColor[0]);
            defaultNode.AddValue("visor_Iva_ReflectionColor[1]", defaultMap.visor_Iva_ReflectionColor[1]);
            defaultNode.AddValue("visor_Iva_ReflectionColor[2]", defaultMap.visor_Iva_ReflectionColor[2]);
            defaultNode.AddValue("visor_Iva_ReflectionColor[3]", defaultMap.visor_Iva_ReflectionColor[3]);
            defaultNode.AddValue("visor_Iva_ReflectionColor[4]", defaultMap.visor_Iva_ReflectionColor[4]);
            defaultNode.AddValue("visor_Iva_ReflectionColor[5]", defaultMap.visor_Iva_ReflectionColor[5]);

            defaultNode.AddValue("visor_EvaGround_ReflectionColor[0]", defaultMap.visor_EvaGround_ReflectionColor[0]);
            defaultNode.AddValue("visor_EvaGround_ReflectionColor[1]", defaultMap.visor_EvaGround_ReflectionColor[1]);
            defaultNode.AddValue("visor_EvaGround_ReflectionColor[2]", defaultMap.visor_EvaGround_ReflectionColor[2]);
            defaultNode.AddValue("visor_EvaGround_ReflectionColor[3]", defaultMap.visor_EvaGround_ReflectionColor[3]);
            defaultNode.AddValue("visor_EvaGround_ReflectionColor[4]", defaultMap.visor_EvaGround_ReflectionColor[4]);
            defaultNode.AddValue("visor_EvaGround_ReflectionColor[5]", defaultMap.visor_EvaGround_ReflectionColor[5]);

            defaultNode.AddValue("visor_EvaSpace_ReflectionColor[0]", defaultMap.visor_EvaSpace_ReflectionColor[0]);
            defaultNode.AddValue("visor_EvaSpace_ReflectionColor[1]", defaultMap.visor_EvaSpace_ReflectionColor[1]);
            defaultNode.AddValue("visor_EvaSpace_ReflectionColor[2]", defaultMap.visor_EvaSpace_ReflectionColor[2]);
            defaultNode.AddValue("visor_EvaSpace_ReflectionColor[3]", defaultMap.visor_EvaSpace_ReflectionColor[3]);
            defaultNode.AddValue("visor_EvaSpace_ReflectionColor[4]", defaultMap.visor_EvaSpace_ReflectionColor[4]);
            defaultNode.AddValue("visor_EvaSpace_ReflectionColor[5]", defaultMap.visor_EvaSpace_ReflectionColor[5]);


            foreach (Suit_Set suitSet in map)
            {
                ConfigNode subNode = new ConfigNode();
                node.AddNode(suitSet.name, subNode);

                //subNode.AddValue("isExclusive", suitSet.isExclusive);

                subNode.AddValue("suit_Iva_Safe", suitSet.suit_Iva_Safe);
                subNode.AddValue("suit_Iva_Unsafe", suitSet.suit_Iva_Unsafe);
                subNode.AddValue("suit_EvaGround_Atmo", suitSet.suit_EvaGround_Atmo);
                subNode.AddValue("suit_EvaGround_NoAtmo", suitSet.suit_EvaGround_NoAtmo);
                subNode.AddValue("suit_EvaSpace", suitSet.suit_EvaSpace);

                subNode.AddValue("helmet_Iva_Safe", suitSet.helmet_Iva_Safe);
                subNode.AddValue("helmet_Iva_Unsafe", suitSet.helmet_Iva_Unsafe);
                subNode.AddValue("helmet_EvaGround_Atmo", suitSet.helmet_EvaGround_Atmo);
                subNode.AddValue("helmet_EvaGround_NoAtmo", suitSet.helmet_EvaGround_NoAtmo);
                subNode.AddValue("helmet_EvaSpace", suitSet.helmet_EvaSpace);

                subNode.AddValue("visor_Iva_Safe", suitSet.visor_Iva_Safe);
                subNode.AddValue("visor_Iva_Unsafe", suitSet.visor_Iva_Unsafe);
                subNode.AddValue("visor_EvaGround_Atmo", suitSet.visor_EvaGround_Atmo);
                subNode.AddValue("visor_EvaGround_NoAtmo", suitSet.visor_EvaGround_NoAtmo);
                subNode.AddValue("visor_EvaSpace", suitSet.visor_EvaSpace);

                subNode.AddValue("jetpack_EvaGround_Atmo", suitSet.jetpack_EvaGround_Atmo);
                subNode.AddValue("jetpack_EvaGround_NoAtmo", suitSet.jetpack_EvaGround_NoAtmo);
                subNode.AddValue("jetpack_EvaSpace", suitSet.jetpack_EvaSpace);

                subNode.AddValue("visor_Iva_ReflectionAdaptive", suitSet.visor_Iva_ReflectionAdaptive);
                subNode.AddValue("visor_EvaGround_ReflectionAdaptive", suitSet.visor_EvaGround_ReflectionAdaptive);
                subNode.AddValue("visor_EvaSpace_ReflectionAdaptive", suitSet.visor_EvaSpace_ReflectionAdaptive);

                subNode.AddValue("visor_Iva_BaseColor[0]", suitSet.visor_Iva_BaseColor[0]);
                subNode.AddValue("visor_Iva_BaseColor[1]", suitSet.visor_Iva_BaseColor[1]);
                subNode.AddValue("visor_Iva_BaseColor[2]", suitSet.visor_Iva_BaseColor[2]);
                subNode.AddValue("visor_Iva_BaseColor[3]", suitSet.visor_Iva_BaseColor[3]);
                subNode.AddValue("visor_Iva_BaseColor[4]", suitSet.visor_Iva_BaseColor[4]);
                subNode.AddValue("visor_Iva_BaseColor[5]", suitSet.visor_Iva_BaseColor[5]);

                subNode.AddValue("visor_EvaGround_BaseColor[0]", suitSet.visor_EvaGround_BaseColor[0]);
                subNode.AddValue("visor_EvaGround_BaseColor[1]", suitSet.visor_EvaGround_BaseColor[1]);
                subNode.AddValue("visor_EvaGround_BaseColor[2]", suitSet.visor_EvaGround_BaseColor[2]);
                subNode.AddValue("visor_EvaGround_BaseColor[3]", suitSet.visor_EvaGround_BaseColor[3]);
                subNode.AddValue("visor_EvaGround_BaseColor[4]", suitSet.visor_EvaGround_BaseColor[4]);
                subNode.AddValue("visor_EvaGround_BaseColor[5]", suitSet.visor_EvaGround_BaseColor[5]);

                subNode.AddValue("visor_EvaSpace_BaseColor[0]", suitSet.visor_EvaSpace_BaseColor[0]);
                subNode.AddValue("visor_EvaSpace_BaseColor[1]", suitSet.visor_EvaSpace_BaseColor[1]);
                subNode.AddValue("visor_EvaSpace_BaseColor[2]", suitSet.visor_EvaSpace_BaseColor[2]);
                subNode.AddValue("visor_EvaSpace_BaseColor[3]", suitSet.visor_EvaSpace_BaseColor[3]);
                subNode.AddValue("visor_EvaSpace_BaseColor[4]", suitSet.visor_EvaSpace_BaseColor[4]);
                subNode.AddValue("visor_EvaSpace_BaseColor[5]", suitSet.visor_EvaSpace_BaseColor[5]);




                subNode.AddValue("visor_Iva_ReflectionColor[0]", suitSet.visor_Iva_ReflectionColor[0]);
                subNode.AddValue("visor_Iva_ReflectionColor[1]", suitSet.visor_Iva_ReflectionColor[1]);
                subNode.AddValue("visor_Iva_ReflectionColor[2]", suitSet.visor_Iva_ReflectionColor[2]);
                subNode.AddValue("visor_Iva_ReflectionColor[3]", suitSet.visor_Iva_ReflectionColor[3]);
                subNode.AddValue("visor_Iva_ReflectionColor[4]", suitSet.visor_Iva_ReflectionColor[4]);
                subNode.AddValue("visor_Iva_ReflectionColor[5]", suitSet.visor_Iva_ReflectionColor[5]);

                subNode.AddValue("visor_EvaGround_ReflectionColor[0]", suitSet.visor_EvaGround_ReflectionColor[0]);
                subNode.AddValue("visor_EvaGround_ReflectionColor[1]", suitSet.visor_EvaGround_ReflectionColor[1]);
                subNode.AddValue("visor_EvaGround_ReflectionColor[2]", suitSet.visor_EvaGround_ReflectionColor[2]);
                subNode.AddValue("visor_EvaGround_ReflectionColor[3]", suitSet.visor_EvaGround_ReflectionColor[3]);
                subNode.AddValue("visor_EvaGround_ReflectionColor[4]", suitSet.visor_EvaGround_ReflectionColor[4]);
                subNode.AddValue("visor_EvaGround_ReflectionColor[5]", suitSet.visor_EvaGround_ReflectionColor[5]);

                subNode.AddValue("visor_EvaSpace_ReflectionColor[0]", suitSet.visor_EvaSpace_ReflectionColor[0]);
                subNode.AddValue("visor_EvaSpace_ReflectionColor[1]", suitSet.visor_EvaSpace_ReflectionColor[1]);
                subNode.AddValue("visor_EvaSpace_ReflectionColor[2]", suitSet.visor_EvaSpace_ReflectionColor[2]);
                subNode.AddValue("visor_EvaSpace_ReflectionColor[3]", suitSet.visor_EvaSpace_ReflectionColor[3]);
                subNode.AddValue("visor_EvaSpace_ReflectionColor[4]", suitSet.visor_EvaSpace_ReflectionColor[4]);
                subNode.AddValue("visor_EvaSpace_ReflectionColor[5]", suitSet.visor_EvaSpace_ReflectionColor[5]);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used to load the configuration for the head-Sets
        /// </summary>
        /// <param name="node"></param>
        /// <param name="listFull"></param>
        /// <param name="defaultHead"></param>
        /// /// ////////////////////////////////////////////////////////////////////////////////////////
        public void loadHeadConfig (ConfigNode node, List<Head_Set>[] listFull, Head_Set[] defaultHead, List<Head_Set>[] listClean)
        {
            //Util.log("+++++ 'loadHeadConfig()' +++++");
            bool hasCfgEntry = false;

            List<string> exclusivedHeads = new List<string>();

            // here we load the default settings for the DEFAULT_MALE head
            ConfigNode defaultNode = new ConfigNode();
            if (node.TryGetNode("DEFAULT_MALE", ref defaultNode))
            {
                int nodeLvl = new int();
                bool nodeBool = false;
                Color32 nodeColor = new Color32(255, 255, 255, 255);

                Util.log("Settings found for {0}, using them", defaultHead[0].name);              

                if (defaultNode.TryGetValue("isExclusive", ref nodeBool))
                    defaultHead[0].isExclusive = nodeBool;
                //Util.log("Settings for {0} = {1}", defaultHead[0].name, defaultHead[0].isExclusive);

                if (defaultNode.TryGetValue("lvlToHide_Eye_Left", ref nodeLvl))
                    defaultHead[0].lvlToHide_Eye_Left = nodeLvl;
                //Util.log("Settings for {0} = {1}", defaultHead[0].name, defaultHead[0].lvlToHide_Eye_Left);
                if (defaultNode.TryGetValue("lvlToHide_Eye_Right", ref nodeLvl))
                    defaultHead[0].lvlToHide_Eye_Right = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_Pupil_Left", ref nodeLvl))
                    defaultHead[0].lvlToHide_Pupil_Left = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_Pupil_Right", ref nodeLvl))
                    defaultHead[0].lvlToHide_Pupil_Right = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_TeethUp", ref nodeLvl))
                    defaultHead[0].lvlToHide_TeethUp = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_TeethDown", ref nodeLvl))
                    defaultHead[0].lvlToHide_TeethDown = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_Ponytail", ref nodeLvl))
                    defaultHead[0].lvlToHide_Ponytail = nodeLvl;

                if (defaultNode.TryGetValue("eyeballColor_Left[0]", ref nodeColor))
                    defaultHead[0].eyeballColor_Left[0] = nodeColor;
                //Util.log("Settings for {0} = {1}", defaultHead[0].name, defaultHead[0].eyeballColor_Left[0]);

                if (defaultNode.TryGetValue("eyeballColor_Left[1]", ref nodeColor))
                    defaultHead[0].eyeballColor_Left[1] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[2]", ref nodeColor))
                    defaultHead[0].eyeballColor_Left[2] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[3]", ref nodeColor))
                    defaultHead[0].eyeballColor_Left[3] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[4]", ref nodeColor))
                    defaultHead[0].eyeballColor_Left[4] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[5]", ref nodeColor))
                    defaultHead[0].eyeballColor_Left[5] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[0]", ref nodeColor))
                    defaultHead[0].eyeballColor_Right[0] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[1]", ref nodeColor))
                    defaultHead[0].eyeballColor_Right[1] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[2]", ref nodeColor))
                    defaultHead[0].eyeballColor_Right[2] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[3]", ref nodeColor))
                    defaultHead[0].eyeballColor_Right[3] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[4]", ref nodeColor))
                    defaultHead[0].eyeballColor_Right[4] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[5]", ref nodeColor))
                    defaultHead[0].eyeballColor_Right[5] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[0]", ref nodeColor))
                    defaultHead[0].pupilColor_Left[0] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[1]", ref nodeColor))
                    defaultHead[0].pupilColor_Left[1] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[2]", ref nodeColor))
                    defaultHead[0].pupilColor_Left[2] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[3]", ref nodeColor))
                    defaultHead[0].pupilColor_Left[3] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[4]", ref nodeColor))
                    defaultHead[0].pupilColor_Left[4] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[5]", ref nodeColor))
                    defaultHead[0].pupilColor_Left[5] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[0]", ref nodeColor))
                    defaultHead[0].pupilColor_Right[0] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[1]", ref nodeColor))
                    defaultHead[0].pupilColor_Right[1] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[2]", ref nodeColor))
                    defaultHead[0].pupilColor_Right[2] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[3]", ref nodeColor))
                    defaultHead[0].pupilColor_Right[3] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[4]", ref nodeColor))
                    defaultHead[0].pupilColor_Right[4] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[5]", ref nodeColor))
                    defaultHead[0].pupilColor_Right[5] = nodeColor;

                defaultHead[0].hasLoadedFromConfig = true;

            }

            // here we load the default settings for the DEFAULT_FEMALE head
            if (node.TryGetNode("DEFAULT_FEMALE", ref defaultNode))
            {
                int nodeLvl = new int();
                bool nodeBool = false;
                Color32 nodeColor = new Color32();

                Util.log("Settings found for {0}, using them", defaultHead[1].name);              

                if (defaultNode.TryGetValue("isExclusive", ref nodeBool))
                    defaultHead[1].isExclusive = nodeBool;
                //Util.log("Settings for {0} = {1}", defaultHead[1].name, defaultHead[1].isExclusive);

                if (defaultNode.TryGetValue("lvlToHide_Eye_Left", ref nodeLvl))
                    defaultHead[1].lvlToHide_Eye_Left = nodeLvl;
                //Util.log("Settings for {0} = {1}", defaultHead[1].name, defaultHead[1].lvlToHide_Eye_Left);
                if (defaultNode.TryGetValue("lvlToHide_Eye_Right", ref nodeLvl))
                    defaultHead[1].lvlToHide_Eye_Right = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_Pupil_Left", ref nodeLvl))
                    defaultHead[1].lvlToHide_Pupil_Left = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_Pupil_Right", ref nodeLvl))
                    defaultHead[1].lvlToHide_Pupil_Right = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_TeethUp", ref nodeLvl))
                    defaultHead[1].lvlToHide_TeethUp = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_TeethDown", ref nodeLvl))
                    defaultHead[1].lvlToHide_TeethDown = nodeLvl;

                if (defaultNode.TryGetValue("lvlToHide_Ponytail", ref nodeLvl))
                    defaultHead[1].lvlToHide_Ponytail = nodeLvl;

                if (defaultNode.TryGetValue("eyeballColor_Left[1]", ref nodeColor))
                    defaultHead[1].eyeballColor_Left[1] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[1]", ref nodeColor))
                    defaultHead[1].eyeballColor_Left[1] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[2]", ref nodeColor))
                    defaultHead[1].eyeballColor_Left[2] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[3]", ref nodeColor))
                    defaultHead[1].eyeballColor_Left[3] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[4]", ref nodeColor))
                    defaultHead[1].eyeballColor_Left[4] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Left[5]", ref nodeColor))
                    defaultHead[1].eyeballColor_Left[5] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[1]", ref nodeColor))
                    defaultHead[1].eyeballColor_Right[1] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[1]", ref nodeColor))
                    defaultHead[1].eyeballColor_Right[1] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[2]", ref nodeColor))
                    defaultHead[1].eyeballColor_Right[2] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[3]", ref nodeColor))
                    defaultHead[1].eyeballColor_Right[3] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[4]", ref nodeColor))
                    defaultHead[1].eyeballColor_Right[4] = nodeColor;

                if (defaultNode.TryGetValue("eyeballColor_Right[5]", ref nodeColor))
                    defaultHead[1].eyeballColor_Right[5] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[1]", ref nodeColor))
                    defaultHead[1].pupilColor_Left[1] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[1]", ref nodeColor))
                    defaultHead[1].pupilColor_Left[1] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[2]", ref nodeColor))
                    defaultHead[1].pupilColor_Left[2] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[3]", ref nodeColor))
                    defaultHead[1].pupilColor_Left[3] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[4]", ref nodeColor))
                    defaultHead[1].pupilColor_Left[4] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Left[5]", ref nodeColor))
                    defaultHead[1].pupilColor_Left[5] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[1]", ref nodeColor))
                    defaultHead[1].pupilColor_Right[1] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[1]", ref nodeColor))
                    defaultHead[1].pupilColor_Right[1] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[2]", ref nodeColor))
                    defaultHead[1].pupilColor_Right[2] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[3]", ref nodeColor))
                    defaultHead[1].pupilColor_Right[3] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[4]", ref nodeColor))
                    defaultHead[1].pupilColor_Right[4] = nodeColor;

                if (defaultNode.TryGetValue("pupilColor_Right[5]", ref nodeColor))
                    defaultHead[1].pupilColor_Right[5] = nodeColor;

                defaultHead[0].hasLoadedFromConfig = true;
            }

            // here we load the settings for the custom heads
            for (int i = 0; i < 2; i++)
            {
                foreach (Head_Set headSet in listFull[i])
                {
                    if (i == 1)
                    {
                        headSet.isFemale = true;
                    }

                    ConfigNode savedNode = new ConfigNode();
                    // if the headset has an entry in the .cfg, try to load the settings, if empty, load the default settings
                    if ( node.TryGetNode(headSet.name, ref savedNode)) 
                    {
                        Util.log("Settings found for Head Set {0}, using them", headSet.name);
                        hasCfgEntry = true;

                        int nodeLvl = new int();
                        bool nodeBool = false;
                        Color32 nodeColor = new Color32();                        

                        if (savedNode.TryGetValue("isExclusive", ref nodeBool))
                            headSet.isExclusive = nodeBool;
                        //Util.log("Settings for {0} = {1}", headSet.name, headSet.isExclusive);
                        else
                            headSet.isExclusive = defaultHead[i].isExclusive;

                        if (savedNode.TryGetValue("lvlToHide_Eye_Left", ref nodeLvl))                        
                            headSet.lvlToHide_Eye_Left = nodeLvl;
                        else
                            headSet.lvlToHide_Eye_Left = defaultHead[i].lvlToHide_Eye_Left;

                        //Util.log("Settings for {0} = {1}", headSet.name, headSet.lvlToHide_Eye_Left);
                        if (savedNode.TryGetValue("lvlToHide_Eye_Right", ref nodeLvl))
                            headSet.lvlToHide_Eye_Right = nodeLvl;
                        else
                            headSet.lvlToHide_Eye_Right = defaultHead[i].lvlToHide_Eye_Right;

                        if (savedNode.TryGetValue("lvlToHide_Pupil_Left", ref nodeLvl))
                            headSet.lvlToHide_Pupil_Left = nodeLvl;
                        else
                            headSet.lvlToHide_Pupil_Left = defaultHead[i].lvlToHide_Pupil_Left;

                        if (savedNode.TryGetValue("lvlToHide_Pupil_Right", ref nodeLvl))
                            headSet.lvlToHide_Pupil_Right = nodeLvl;
                        else
                            headSet.lvlToHide_Pupil_Right = defaultHead[i].lvlToHide_Pupil_Right;

                        if (savedNode.TryGetValue("lvlToHide_TeethUp", ref nodeLvl))
                            headSet.lvlToHide_TeethUp = nodeLvl;
                        else
                            headSet.lvlToHide_TeethUp = defaultHead[i].lvlToHide_TeethUp;

                        if (savedNode.TryGetValue("lvlToHide_TeethDown", ref nodeLvl))
                            headSet.lvlToHide_TeethDown = nodeLvl;
                        else
                            headSet.lvlToHide_TeethDown = defaultHead[i].lvlToHide_TeethDown;

                        if (savedNode.TryGetValue("lvlToHide_Ponytail", ref nodeLvl))
                            headSet.lvlToHide_Ponytail = nodeLvl;
                        else
                            headSet.lvlToHide_Ponytail = defaultHead[i].lvlToHide_Ponytail;

                        if (savedNode.TryGetValue("eyeballColor_Left[0]", ref nodeColor))                        
                            headSet.eyeballColor_Left[0] = nodeColor;
                        else
                            headSet.eyeballColor_Left[0] = defaultHead[i].eyeballColor_Left[0];

                        if (savedNode.TryGetValue("eyeballColor_Left[1]", ref nodeColor))
                            headSet.eyeballColor_Left[1] = nodeColor;
                        else
                            headSet.eyeballColor_Left[1] = defaultHead[i].eyeballColor_Left[1];

                        if (savedNode.TryGetValue("eyeballColor_Left[2]", ref nodeColor))
                            headSet.eyeballColor_Left[2] = nodeColor;
                        else
                            headSet.eyeballColor_Left[2] = defaultHead[i].eyeballColor_Left[2];

                        if (savedNode.TryGetValue("eyeballColor_Left[3]", ref nodeColor))
                            headSet.eyeballColor_Left[3] = nodeColor;
                        else
                            headSet.eyeballColor_Left[3] = defaultHead[i].eyeballColor_Left[3];

                        if (savedNode.TryGetValue("eyeballColor_Left[4]", ref nodeColor))
                            headSet.eyeballColor_Left[4] = nodeColor;
                        else
                            headSet.eyeballColor_Left[4] = defaultHead[i].eyeballColor_Left[4];

                        if (savedNode.TryGetValue("eyeballColor_Left[5]", ref nodeColor))
                            headSet.eyeballColor_Left[5] = nodeColor;
                        else
                            headSet.eyeballColor_Left[5] = defaultHead[i].eyeballColor_Left[5];

                        if (savedNode.TryGetValue("eyeballColor_Right[0]", ref nodeColor))
                            headSet.eyeballColor_Right[0] = nodeColor;
                        else
                            headSet.eyeballColor_Right[0] = defaultHead[i].eyeballColor_Right[0];

                        if (savedNode.TryGetValue("eyeballColor_Right[1]", ref nodeColor))
                            headSet.eyeballColor_Right[1] = nodeColor;
                        else
                            headSet.eyeballColor_Right[1] = defaultHead[i].eyeballColor_Right[1];

                        if (savedNode.TryGetValue("eyeballColor_Right[2]", ref nodeColor))
                            headSet.eyeballColor_Right[2] = nodeColor;
                        else
                            headSet.eyeballColor_Right[2] = defaultHead[i].eyeballColor_Right[2];

                        if (savedNode.TryGetValue("eyeballColor_Right[3]", ref nodeColor))
                            headSet.eyeballColor_Right[3] = nodeColor;
                        else
                            headSet.eyeballColor_Right[3] = defaultHead[i].eyeballColor_Right[3];

                        if (savedNode.TryGetValue("eyeballColor_Right[4]", ref nodeColor))
                            headSet.eyeballColor_Right[4] = nodeColor;
                        else
                            headSet.eyeballColor_Right[4] = defaultHead[i].eyeballColor_Right[4];

                        if (savedNode.TryGetValue("eyeballColor_Right[5]", ref nodeColor))
                            headSet.eyeballColor_Right[5] = nodeColor;
                        else
                            headSet.eyeballColor_Right[5] = defaultHead[i].eyeballColor_Right[5];

                        if (savedNode.TryGetValue("pupilColor_Left[0]", ref nodeColor))
                            headSet.pupilColor_Left[0] = nodeColor;
                        else
                            headSet.pupilColor_Left[0] = defaultHead[i].pupilColor_Left[0];

                        if (savedNode.TryGetValue("pupilColor_Left[1]", ref nodeColor))
                            headSet.pupilColor_Left[1] = nodeColor;
                        else
                            headSet.pupilColor_Left[1] = defaultHead[i].pupilColor_Left[1];

                        if (savedNode.TryGetValue("pupilColor_Left[2]", ref nodeColor))
                            headSet.pupilColor_Left[2] = nodeColor;
                        else
                            headSet.pupilColor_Left[2] = defaultHead[i].pupilColor_Left[2];

                        if (savedNode.TryGetValue("pupilColor_Left[3]", ref nodeColor))
                            headSet.pupilColor_Left[3] = nodeColor;
                        else
                            headSet.pupilColor_Left[3] = defaultHead[i].pupilColor_Left[3];

                        if (savedNode.TryGetValue("pupilColor_Left[4]", ref nodeColor))
                            headSet.pupilColor_Left[4] = nodeColor;
                        else
                            headSet.pupilColor_Left[4] = defaultHead[i].pupilColor_Left[4];

                        if (savedNode.TryGetValue("pupilColor_Left[5]", ref nodeColor))
                            headSet.pupilColor_Left[5] = nodeColor;
                        else
                            headSet.pupilColor_Left[5] = defaultHead[i].pupilColor_Left[5];

                        if (savedNode.TryGetValue("pupilColor_Right[0]", ref nodeColor))
                            headSet.pupilColor_Right[0] = nodeColor;
                        else
                            headSet.pupilColor_Right[0] = defaultHead[i].pupilColor_Right[0];

                        if (savedNode.TryGetValue("pupilColor_Right[1]", ref nodeColor))
                            headSet.pupilColor_Right[1] = nodeColor;
                        else
                            headSet.pupilColor_Right[1] = defaultHead[i].pupilColor_Right[1];

                        if (savedNode.TryGetValue("pupilColor_Right[2]", ref nodeColor))
                            headSet.pupilColor_Right[2] = nodeColor;
                        else
                            headSet.pupilColor_Right[2] = defaultHead[i].pupilColor_Right[2];

                        if (savedNode.TryGetValue("pupilColor_Right[3]", ref nodeColor))
                            headSet.pupilColor_Right[3] = nodeColor;
                        else
                            headSet.pupilColor_Right[3] = defaultHead[i].pupilColor_Right[3];

                        if (savedNode.TryGetValue("pupilColor_Right[4]", ref nodeColor))
                            headSet.pupilColor_Right[4] = nodeColor;
                        else
                            headSet.pupilColor_Right[4] = defaultHead[i].pupilColor_Right[4];

                        if (savedNode.TryGetValue("pupilColor_Right[5]", ref nodeColor))
                            headSet.pupilColor_Right[5] = nodeColor;
                        else
                            headSet.pupilColor_Right[5] = defaultHead[i].pupilColor_Right[5];

                        headSet.hasLoadedFromConfig = true;

                    }
                    // if the headset has no entry in the .cfg, load the default settings
                    if (hasCfgEntry == false)
                    {
                        if (headSet.hasLoadedFromConfig == false)
                        {
                            headSet.isExclusive = defaultHead[i].isExclusive;

                            headSet.lvlToHide_Eye_Left = defaultHead[i].lvlToHide_Eye_Left;
                            headSet.lvlToHide_Eye_Right = defaultHead[i].lvlToHide_Eye_Right;
                            headSet.lvlToHide_Pupil_Left = defaultHead[i].lvlToHide_Pupil_Left;
                            headSet.lvlToHide_Pupil_Right = defaultHead[i].lvlToHide_Pupil_Right;
                            headSet.lvlToHide_TeethUp = defaultHead[i].lvlToHide_TeethUp;
                            headSet.lvlToHide_TeethDown = defaultHead[i].lvlToHide_TeethDown;
                            headSet.lvlToHide_Ponytail = defaultHead[i].lvlToHide_Ponytail;

                            headSet.eyeballColor_Left[0] = defaultHead[i].eyeballColor_Left[0];
                            headSet.eyeballColor_Left[1] = defaultHead[i].eyeballColor_Left[1];
                            headSet.eyeballColor_Left[2] = defaultHead[i].eyeballColor_Left[2];
                            headSet.eyeballColor_Left[3] = defaultHead[i].eyeballColor_Left[3];
                            headSet.eyeballColor_Left[4] = defaultHead[i].eyeballColor_Left[4];
                            headSet.eyeballColor_Left[5] = defaultHead[i].eyeballColor_Left[5];

                            headSet.eyeballColor_Right[0] = defaultHead[i].eyeballColor_Right[0];
                            headSet.eyeballColor_Right[1] = defaultHead[i].eyeballColor_Right[1];
                            headSet.eyeballColor_Right[2] = defaultHead[i].eyeballColor_Right[2];
                            headSet.eyeballColor_Right[3] = defaultHead[i].eyeballColor_Right[3];
                            headSet.eyeballColor_Right[4] = defaultHead[i].eyeballColor_Right[4];
                            headSet.eyeballColor_Right[5] = defaultHead[i].eyeballColor_Right[5];

                            headSet.pupilColor_Left[0] = defaultHead[i].pupilColor_Left[0];
                            headSet.pupilColor_Left[1] = defaultHead[i].pupilColor_Left[1];
                            headSet.pupilColor_Left[2] = defaultHead[i].pupilColor_Left[2];
                            headSet.pupilColor_Left[3] = defaultHead[i].pupilColor_Left[3];
                            headSet.pupilColor_Left[4] = defaultHead[i].pupilColor_Left[4];
                            headSet.pupilColor_Left[5] = defaultHead[i].pupilColor_Left[5];

                            headSet.pupilColor_Right[0] = defaultHead[i].pupilColor_Right[0];
                            headSet.pupilColor_Right[1] = defaultHead[i].pupilColor_Right[1];
                            headSet.pupilColor_Right[2] = defaultHead[i].pupilColor_Right[2];
                            headSet.pupilColor_Right[3] = defaultHead[i].pupilColor_Right[3];
                            headSet.pupilColor_Right[4] = defaultHead[i].pupilColor_Right[4];
                            headSet.pupilColor_Right[5] = defaultHead[i].pupilColor_Right[5];
                        }

                        
                    }

                    
                    if (headSet.isExclusive)
                    {
                        exclusivedHeads.Add(headSet.name);
                    }

                    
                }
            }

            // Create/update the list of male and female heads minus the exclusive ones
            listClean[0].Clear();
            listClean[1].Clear();
            listClean[0].AddRange(listFull[0].Where(h => !exclusivedHeads.Contains(h.name)));
            listClean[1].AddRange(listFull[1].Where(h => !exclusivedHeads.Contains(h.name)));
            listClean[0].TrimExcess();
            listClean[1].TrimExcess();

            /*for (int i = 0; i < 2; i++)
            {
                if (listClean[i] != null)
                {
                    foreach (Head_Set head in listClean[i])
                    {
                        Util.log(" +++ listClean{0}] : {1} +++", i, head.name);
                    }
                }
                else
                {
                    Util.log(" +++ listClean[0] is null +++");
                }

            }*/
        }       

        private static void saveHeadConfig (ConfigNode node, List<Head_Set>[] map, Head_Set[] defaultMap)
        {
            for (int i = 0; i < 2; i++)
            {
                ConfigNode subNode = new ConfigNode();
                if (i == 0)
                    node.AddNode("DEFAULT_MALE", subNode);
                else
                    node.AddNode("DEFAULT_FEMALE", subNode);

                subNode.AddValue("isExclusive", defaultMap[i].isExclusive);
                subNode.AddValue("isFemale", defaultMap[i].isFemale);
                subNode.AddValue("lvlToHide_Eye_Left", defaultMap[i].lvlToHide_Eye_Left);
                subNode.AddValue("lvlToHide_Eye_Right", defaultMap[i].lvlToHide_Eye_Right);
                subNode.AddValue("lvlToHide_Pupil_Left", defaultMap[i].lvlToHide_Pupil_Left);
                subNode.AddValue("lvlToHide_Pupil_Right", defaultMap[i].lvlToHide_Pupil_Right);
                subNode.AddValue("lvlToHide_TeethUp", defaultMap[i].lvlToHide_TeethUp);
                subNode.AddValue("lvlToHide_TeethDown", defaultMap[i].lvlToHide_TeethDown);
                subNode.AddValue("lvlToHide_Ponytail", defaultMap[i].lvlToHide_Ponytail);
                subNode.AddValue("eyeballColor_Left[0]", defaultMap[i].eyeballColor_Left[0]);
                subNode.AddValue("eyeballColor_Left[1]", defaultMap[i].eyeballColor_Left[1]);
                subNode.AddValue("eyeballColor_Left[2]", defaultMap[i].eyeballColor_Left[2]);
                subNode.AddValue("eyeballColor_Left[3]", defaultMap[i].eyeballColor_Left[3]);
                subNode.AddValue("eyeballColor_Left[4]", defaultMap[i].eyeballColor_Left[4]);
                subNode.AddValue("eyeballColor_Left[5]", defaultMap[i].eyeballColor_Left[5]);
                subNode.AddValue("eyeballColor_Right[0]", defaultMap[i].eyeballColor_Right[0]);
                subNode.AddValue("eyeballColor_Right[1]", defaultMap[i].eyeballColor_Right[1]);
                subNode.AddValue("eyeballColor_Right[2]", defaultMap[i].eyeballColor_Right[2]);
                subNode.AddValue("eyeballColor_Right[3]", defaultMap[i].eyeballColor_Right[3]);
                subNode.AddValue("eyeballColor_Right[4]", defaultMap[i].eyeballColor_Right[4]);
                subNode.AddValue("eyeballColor_Right[5]", defaultMap[i].eyeballColor_Right[5]);
                subNode.AddValue("pupilColor_Left[0]", defaultMap[i].pupilColor_Left[0]);
                subNode.AddValue("pupilColor_Left[1]", defaultMap[i].pupilColor_Left[1]);
                subNode.AddValue("pupilColor_Left[2]", defaultMap[i].pupilColor_Left[2]);
                subNode.AddValue("pupilColor_Left[3]", defaultMap[i].pupilColor_Left[3]);
                subNode.AddValue("pupilColor_Left[4]", defaultMap[i].pupilColor_Left[4]);
                subNode.AddValue("pupilColor_Left[5]", defaultMap[i].pupilColor_Left[5]);
                subNode.AddValue("pupilColor_Right[0]", defaultMap[i].pupilColor_Right[0]);
                subNode.AddValue("pupilColor_Right[1]", defaultMap[i].pupilColor_Right[1]);
                subNode.AddValue("pupilColor_Right[2]", defaultMap[i].pupilColor_Right[2]);
                subNode.AddValue("pupilColor_Right[3]", defaultMap[i].pupilColor_Right[3]);
                subNode.AddValue("pupilColor_Right[4]", defaultMap[i].pupilColor_Right[4]);
                subNode.AddValue("pupilColor_Right[5]", defaultMap[i].pupilColor_Right[5]);
            }

            for (int i = 0; i < 2; i++)
            {
                foreach (Head_Set headSet in map[i])
                {
                    ConfigNode subNode = new ConfigNode();
                    node.AddNode(headSet.name, subNode);

                    subNode.AddValue("isExclusive", headSet.isExclusive);
                    subNode.AddValue("lvlToHide_Eye_Left", headSet.lvlToHide_Eye_Left);
                    subNode.AddValue("lvlToHide_Eye_Right", headSet.lvlToHide_Eye_Right);
                    subNode.AddValue("lvlToHide_Pupil_Left", headSet.lvlToHide_Pupil_Left);
                    subNode.AddValue("lvlToHide_Pupil_Right", headSet.lvlToHide_Pupil_Right);
                    subNode.AddValue("lvlToHide_TeethUp", headSet.lvlToHide_TeethUp);
                    subNode.AddValue("lvlToHide_TeethDown", headSet.lvlToHide_TeethDown);
                    subNode.AddValue("lvlToHide_Ponytail", headSet.lvlToHide_Ponytail);
                    subNode.AddValue("eyeballColor_Left[0]", headSet.eyeballColor_Left[0]);
                    subNode.AddValue("eyeballColor_Left[1]", headSet.eyeballColor_Left[1]);
                    subNode.AddValue("eyeballColor_Left[2]", headSet.eyeballColor_Left[2]);
                    subNode.AddValue("eyeballColor_Left[3]", headSet.eyeballColor_Left[3]);
                    subNode.AddValue("eyeballColor_Left[4]", headSet.eyeballColor_Left[4]);
                    subNode.AddValue("eyeballColor_Left[5]", headSet.eyeballColor_Left[5]);
                    subNode.AddValue("eyeballColor_Right[0]", headSet.eyeballColor_Right[0]);
                    subNode.AddValue("eyeballColor_Right[1]", headSet.eyeballColor_Right[1]);
                    subNode.AddValue("eyeballColor_Right[2]", headSet.eyeballColor_Right[2]);
                    subNode.AddValue("eyeballColor_Right[3]", headSet.eyeballColor_Right[3]);
                    subNode.AddValue("eyeballColor_Right[4]", headSet.eyeballColor_Right[4]);
                    subNode.AddValue("eyeballColor_Right[5]", headSet.eyeballColor_Right[5]);
                    subNode.AddValue("pupilColor_Left[0]", headSet.pupilColor_Left[0]);
                    subNode.AddValue("pupilColor_Left[1]", headSet.pupilColor_Left[1]);
                    subNode.AddValue("pupilColor_Left[2]", headSet.pupilColor_Left[2]);
                    subNode.AddValue("pupilColor_Left[3]", headSet.pupilColor_Left[3]);
                    subNode.AddValue("pupilColor_Left[4]", headSet.pupilColor_Left[4]);
                    subNode.AddValue("pupilColor_Left[5]", headSet.pupilColor_Left[5]);
                    subNode.AddValue("pupilColor_Right[0]", headSet.pupilColor_Right[0]);
                    subNode.AddValue("pupilColor_Right[1]", headSet.pupilColor_Right[1]);
                    subNode.AddValue("pupilColor_Right[2]", headSet.pupilColor_Right[2]);
                    subNode.AddValue("pupilColor_Right[3]", headSet.pupilColor_Right[3]);
                    subNode.AddValue("pupilColor_Right[4]", headSet.pupilColor_Right[4]);
                    subNode.AddValue("pupilColor_Right[5]", headSet.pupilColor_Right[5]);
                }
            }

        }
        
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Fill config for custom Kerbal heads and suits.
        /// called once at the main menu 
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void readKerbalsConfigs()
        {
            //Util.log("+++++ 'readKerbalconfig()' +++++");

            foreach (UrlDir.UrlConfig file in GameDatabase.Instance.GetConfigs("TextureReplacerReplaced"))
            {
                ConfigNode customNode = file.config.GetNode("CustomKerbals");
                if (customNode != null)
                {
                    // Merge into `customKerbalsNode`.
                    foreach (ConfigNode.Value entry in customNode.values)
                    {
                        customKerbalsNode.RemoveValue(entry.name);
                        customKerbalsNode.AddValue(entry.name, entry.value);
                    }
                }               

                ConfigNode classNode = file.config.GetNode("ClassSuits");
                if (classNode != null)
                    loadSuitMap(classNode, defaultClassSuits);

                ConfigNode headNode = file.config.GetNode("HeadSettings");
                if (headNode != null)               
                    loadHeadConfig(headNode, maleAndfemaleHeadsDB_full, defaulMaleAndFemaleHeads, maleAndfemaleHeadsDB_cleaned);
                
                ConfigNode suitNode = file.config.GetNode("SuitSettings");
                if (suitNode != null)                
                    loadSuitConfig(suitNode, KerbalSuitsDB_full, defaultSuit, false);
                
            }
            
            // Create lists of male heads and suits.            
            KerbalSuitsDB_cleaned.AddRange(KerbalSuitsDB_full.Where(s => !excludedSuits.Contains(s.name)));

            // Create lists of female heads and suits.
           // maleAndfemaleHeadsDB_cleaned[0].AddRange(KerbalHeadsDB_full.Where(h => !h.isFemale && !excludedHeads.Contains(h.name)));
           // maleAndfemaleHeadsDB_cleaned[1].AddRange(KerbalHeadsDB_full.Where(h => h.isFemale && !excludedHeads.Contains(h.name)));
            
            // Trim lists.
            KerbalHeadsDB_full.TrimExcess();
            KerbalSuitsDB_full.TrimExcess();
            KerbalSuitsDB_cleaned.TrimExcess();
           // maleAndfemaleHeadsDB_cleaned[0].TrimExcess();            
           // maleAndfemaleHeadsDB_cleaned[1].TrimExcess();
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Read configuration and perform pre-load initialization.
        /// </summary>
        /// <param name="rootNode">The <see cref="ConfigNode"/> that contain our saved data</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void readConfig(ConfigNode rootNode)
        {

            //Util.log("+++++ 'readConfig()' +++++");

            Util.parse(rootNode.GetValue("atmSuitPressure"), ref atmSuitPressure);
            Util.addLists(rootNode.GetValues("atmSuitBodies"), atmSuitBodies);
            Util.parse(rootNode.GetValue("forceLegacyFemales"), ref forceLegacyFemales);
            Util.parse(rootNode.GetValue("useKspSkin"), ref useKspSkin);

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Post-load initialization.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void load()
        {
            Util.log("++++ 'load()' ++++");

            // Populate KerbalHeadsDB_full and defaulMaleAndFemaleHeads
            Textures_Loader.LoadHeads(KerbalHeadsDB_full, maleAndfemaleHeadsDB_full, defaulMaleAndFemaleHeads);
           
            Textures_Loader.LoadSuits(KerbalSuitsDB_full, defaultSuit);

            readKerbalsConfigs();     

            foreach (Kerbal kerbal in Resources.FindObjectsOfTypeAll<Kerbal>())
            {
                int gender = kerbal.transform.name == "kerbalFemale" ? 1 : 0;

                // Save pointer to helmet & visor meshes so helmet removal can restore them.
                foreach (SkinnedMeshRenderer smr in kerbal.GetComponentsInChildren<SkinnedMeshRenderer>(true))
                {
                    if (smr.name.EndsWith("helmet", StringComparison.Ordinal))
                        helmetMesh[gender] = smr.sharedMesh;
                    else if (smr.name.EndsWith("visor", StringComparison.Ordinal))
                        visorMesh[gender] = smr.sharedMesh;
                    else if (smr.name.EndsWith("jetpack", StringComparison.Ordinal))
                        jetpackMesh[gender] = smr.sharedMesh;
                }

                // After an IVA space is initialized, suits are reset to these values. Replace stock textures with default ones.
                kerbal.textureStandard = defaultSuit.get_suit_Iva_Standard_Male(0);
                kerbal.textureVeteran = defaultSuit.get_suit_Iva_Veteran_Male(0);                

                if (kerbal.GetComponent<TRR_IvaModule>() == null)
                    kerbal.gameObject.AddComponent<TRR_IvaModule>();
            }

            Part[] evas = {
                PartLoader.getPartInfoByName("kerbalEVA").partPrefab,
                PartLoader.getPartInfoByName("kerbalEVAfemale").partPrefab
            };

            foreach (Part eva in evas)
            {
                if (eva.GetComponent<TRR_EvaModule>() == null)
                    eva.gameObject.AddComponent<TRR_EvaModule>();
            }

            // Re-read scenario if database is reloaded during the space center scene to avoid losing all per-game settings.(broken)
            /*
            if (HighLogic.CurrentGame != null)
            {
                Util.log("+++++ 'HighLogic ok' +++++");

                ConfigNode scenarioNode = HighLogic.CurrentGame.config.GetNodes("SCENARIO")
                  .FirstOrDefault(n => n.GetValue("name") == "TRR_Scenario");

                if (scenarioNode != null)
                    Util.log("++++ 'loadScenario()' +++++");
                    loadScenario(scenarioNode);
            }*/

            
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add an event handler to update the helmet when onVesselSituationChange
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void beginFlight()
        {
            GameEvents.onVesselSituationChange.Add(updateHelmets);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Remove the event handler to update the helmet when onVesselSituationChange
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void endFlight()
        {
            GameEvents.onVesselSituationChange.Remove(updateHelmets);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used to load the configuration data saved in the .cfg file and in persistent save
        /// </summary>
        /// <param name="node">The <see cref="ConfigNode"/> that contain our saved data</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void loadScenario(ConfigNode node)
        {

            //Util.log("+++++ 'loadscenario()' +++++");
            gameKerbalsDB.Clear();
            classSuitsDB.Clear();

            loadKerbals(node.GetNode("Kerbals"));
            loadSuitMap(node.GetNode("ClassSuits"), classSuitsDB, defaultClassSuits);

            ConfigNode headNode = node.GetNode("HeadSettings");
            if (headNode != null)
            {
                loadHeadConfig(headNode, maleAndfemaleHeadsDB_full, defaulMaleAndFemaleHeads, maleAndfemaleHeadsDB_cleaned);
            }

            ConfigNode suitNode = node.GetNode("SuitSettings");
            if (suitNode != null)
            {
                loadSuitConfig(suitNode, KerbalSuitsDB_full, defaultSuit, false);
            }

            Util.parse(node.GetValue("useKspSkin"), ref useKspSkin);
        }
                
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used to save the configuration data in the .cfg file and in persistent save
        /// </summary>
        /// <param name="node">The <see cref="ConfigNode"/> that contain our saved data</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void saveScenario(ConfigNode node)
        {
            saveKerbals(node.AddNode("Kerbals"));
            saveSuitMap(classSuitsDB, node.AddNode("ClassSuits"));
            saveHeadConfig(node.AddNode("HeadSettings"), maleAndfemaleHeadsDB_full, defaulMaleAndFemaleHeads);
            saveSuitConfig(node.AddNode("SuitSettings"), KerbalSuitsDB_full, defaultSuit);

            node.AddValue("useKspSkin", useKspSkin);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Reset the configuration data form the suits and the heads. Called by the "Reset to Default" button in the GUI. 
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void resetKerbals()
        {
            gameKerbalsDB.Clear();
            classSuitsDB.Clear();
            KerbalAndTheirHeadsDB.Clear();
            
            loadKerbals(null);           
            loadSuitMap(null, classSuitsDB, defaultClassSuits);
        }

        public void resetHead(Head_Set headSet, Head_Set[] defaultHead)
        {
            int i = 0;
            if (headSet.isFemale == true)
            {
                i = 1;
            }


            foreach (UrlDir.UrlConfig file in GameDatabase.Instance.GetConfigs("TextureReplacerReplaced"))
            {
                ConfigNode node = file.config.GetNode("HeadSettings");
                if (node != null)
                {
                    ConfigNode savedNode = new ConfigNode();
                    // if the headset has an entry in the .cfg, try to load the settings, if empty, load the default settings
                    if (node.TryGetNode(headSet.name, ref savedNode))
                    {
                        //Util.log("Settings found for {0}, using them", headSet.name);
                        int nodeLvl = new int();
                        bool nodeBool = false;
                        Color32 nodeColor = new Color32();                      

                        if (savedNode.TryGetValue("isExclusive", ref nodeBool))
                            headSet.isExclusive = nodeBool;
                        //Util.log("Settings for {0} = {1}", headSet.name, headSet.isExclusive);
                        else
                            headSet.isExclusive = defaultHead[i].isExclusive;

                        if (savedNode.TryGetValue("lvlToHide_Eye_Left", ref nodeLvl))
                            headSet.lvlToHide_Eye_Left = nodeLvl;
                        else
                            headSet.lvlToHide_Eye_Left = defaultHead[i].lvlToHide_Eye_Left;

                        //Util.log("Settings for {0} = {1}", headSet.name, headSet.lvlToHide_Eye_Left);
                        if (savedNode.TryGetValue("lvlToHide_Eye_Right", ref nodeLvl))
                            headSet.lvlToHide_Eye_Right = nodeLvl;
                        else
                            headSet.lvlToHide_Eye_Right = defaultHead[i].lvlToHide_Eye_Right;

                        if (savedNode.TryGetValue("lvlToHide_Pupil_Left", ref nodeLvl))
                            headSet.lvlToHide_Pupil_Left = nodeLvl;
                        else
                            headSet.lvlToHide_Pupil_Left = defaultHead[i].lvlToHide_Pupil_Left;

                        if (savedNode.TryGetValue("lvlToHide_Pupil_Right", ref nodeLvl))
                            headSet.lvlToHide_Pupil_Right = nodeLvl;
                        else
                            headSet.lvlToHide_Pupil_Right = defaultHead[i].lvlToHide_Pupil_Right;

                        if (savedNode.TryGetValue("lvlToHide_TeethUp", ref nodeLvl))
                            headSet.lvlToHide_TeethUp = nodeLvl;
                        else
                            headSet.lvlToHide_TeethUp = defaultHead[i].lvlToHide_TeethUp;

                        if (savedNode.TryGetValue("lvlToHide_TeethDown", ref nodeLvl))
                            headSet.lvlToHide_TeethDown = nodeLvl;
                        else
                            headSet.lvlToHide_TeethDown = defaultHead[i].lvlToHide_TeethDown;

                        if (savedNode.TryGetValue("lvlToHide_Ponytail", ref nodeLvl))
                            headSet.lvlToHide_Ponytail = nodeLvl;
                        else
                            headSet.lvlToHide_Ponytail = defaultHead[i].lvlToHide_Ponytail;

                        if (savedNode.TryGetValue("eyeballColor_Left[0]", ref nodeColor))
                            headSet.eyeballColor_Left[0] = nodeColor;
                        else
                            headSet.eyeballColor_Left[0] = defaultHead[i].eyeballColor_Left[0];

                        if (savedNode.TryGetValue("eyeballColor_Left[1]", ref nodeColor))
                            headSet.eyeballColor_Left[1] = nodeColor;
                        else
                            headSet.eyeballColor_Left[1] = defaultHead[i].eyeballColor_Left[1];

                        if (savedNode.TryGetValue("eyeballColor_Left[2]", ref nodeColor))
                            headSet.eyeballColor_Left[2] = nodeColor;
                        else
                            headSet.eyeballColor_Left[2] = defaultHead[i].eyeballColor_Left[2];

                        if (savedNode.TryGetValue("eyeballColor_Left[3]", ref nodeColor))
                            headSet.eyeballColor_Left[3] = nodeColor;
                        else
                            headSet.eyeballColor_Left[3] = defaultHead[i].eyeballColor_Left[3];

                        if (savedNode.TryGetValue("eyeballColor_Left[4]", ref nodeColor))
                            headSet.eyeballColor_Left[4] = nodeColor;
                        else
                            headSet.eyeballColor_Left[4] = defaultHead[i].eyeballColor_Left[4];

                        if (savedNode.TryGetValue("eyeballColor_Left[5]", ref nodeColor))
                            headSet.eyeballColor_Left[5] = nodeColor;
                        else
                            headSet.eyeballColor_Left[5] = defaultHead[i].eyeballColor_Left[5];

                        if (savedNode.TryGetValue("eyeballColor_Right[0]", ref nodeColor))
                            headSet.eyeballColor_Right[0] = nodeColor;
                        else
                            headSet.eyeballColor_Right[0] = defaultHead[i].eyeballColor_Right[0];

                        if (savedNode.TryGetValue("eyeballColor_Right[1]", ref nodeColor))
                            headSet.eyeballColor_Right[1] = nodeColor;
                        else
                            headSet.eyeballColor_Right[1] = defaultHead[i].eyeballColor_Right[1];

                        if (savedNode.TryGetValue("eyeballColor_Right[2]", ref nodeColor))
                            headSet.eyeballColor_Right[2] = nodeColor;
                        else
                            headSet.eyeballColor_Right[2] = defaultHead[i].eyeballColor_Right[2];

                        if (savedNode.TryGetValue("eyeballColor_Right[3]", ref nodeColor))
                            headSet.eyeballColor_Right[3] = nodeColor;
                        else
                            headSet.eyeballColor_Right[3] = defaultHead[i].eyeballColor_Right[3];

                        if (savedNode.TryGetValue("eyeballColor_Right[4]", ref nodeColor))
                            headSet.eyeballColor_Right[4] = nodeColor;
                        else
                            headSet.eyeballColor_Right[4] = defaultHead[i].eyeballColor_Right[4];

                        if (savedNode.TryGetValue("eyeballColor_Right[5]", ref nodeColor))
                            headSet.eyeballColor_Right[5] = nodeColor;
                        else
                            headSet.eyeballColor_Right[5] = defaultHead[i].eyeballColor_Right[5];

                        if (savedNode.TryGetValue("pupilColor_Left[0]", ref nodeColor))
                            headSet.pupilColor_Left[0] = nodeColor;
                        else
                            headSet.pupilColor_Left[0] = defaultHead[i].pupilColor_Left[0];

                        if (savedNode.TryGetValue("pupilColor_Left[1]", ref nodeColor))
                            headSet.pupilColor_Left[1] = nodeColor;
                        else
                            headSet.pupilColor_Left[1] = defaultHead[i].pupilColor_Left[1];

                        if (savedNode.TryGetValue("pupilColor_Left[2]", ref nodeColor))
                            headSet.pupilColor_Left[2] = nodeColor;
                        else
                            headSet.pupilColor_Left[2] = defaultHead[i].pupilColor_Left[2];

                        if (savedNode.TryGetValue("pupilColor_Left[3]", ref nodeColor))
                            headSet.pupilColor_Left[3] = nodeColor;
                        else
                            headSet.pupilColor_Left[3] = defaultHead[i].pupilColor_Left[3];

                        if (savedNode.TryGetValue("pupilColor_Left[4]", ref nodeColor))
                            headSet.pupilColor_Left[4] = nodeColor;
                        else
                            headSet.pupilColor_Left[4] = defaultHead[i].pupilColor_Left[4];

                        if (savedNode.TryGetValue("pupilColor_Left[5]", ref nodeColor))
                            headSet.pupilColor_Left[5] = nodeColor;
                        else
                            headSet.pupilColor_Left[5] = defaultHead[i].pupilColor_Left[5];

                        if (savedNode.TryGetValue("pupilColor_Right[0]", ref nodeColor))
                            headSet.pupilColor_Right[0] = nodeColor;
                        else
                            headSet.pupilColor_Right[0] = defaultHead[i].pupilColor_Right[0];

                        if (savedNode.TryGetValue("pupilColor_Right[1]", ref nodeColor))
                            headSet.pupilColor_Right[1] = nodeColor;
                        else
                            headSet.pupilColor_Right[1] = defaultHead[i].pupilColor_Right[1];

                        if (savedNode.TryGetValue("pupilColor_Right[2]", ref nodeColor))
                            headSet.pupilColor_Right[2] = nodeColor;
                        else
                            headSet.pupilColor_Right[2] = defaultHead[i].pupilColor_Right[2];

                        if (savedNode.TryGetValue("pupilColor_Right[3]", ref nodeColor))
                            headSet.pupilColor_Right[3] = nodeColor;
                        else
                            headSet.pupilColor_Right[3] = defaultHead[i].pupilColor_Right[3];

                        if (savedNode.TryGetValue("pupilColor_Right[4]", ref nodeColor))
                            headSet.pupilColor_Right[4] = nodeColor;
                        else
                            headSet.pupilColor_Right[4] = defaultHead[i].pupilColor_Right[4];

                        if (savedNode.TryGetValue("pupilColor_Right[5]", ref nodeColor))
                            headSet.pupilColor_Right[5] = nodeColor;
                        else
                            headSet.pupilColor_Right[5] = defaultHead[i].pupilColor_Right[5];

                    }
                    // if the headset has no entry in the .cfg, load the default settings
                    else
                    {
                        headSet.isExclusive = defaultHead[i].isExclusive;

                        headSet.lvlToHide_Eye_Left = defaultHead[i].lvlToHide_Eye_Left;
                        headSet.lvlToHide_Eye_Right = defaultHead[i].lvlToHide_Eye_Right;
                        headSet.lvlToHide_Pupil_Left = defaultHead[i].lvlToHide_Pupil_Left;
                        headSet.lvlToHide_Pupil_Right = defaultHead[i].lvlToHide_Pupil_Right;
                        headSet.lvlToHide_TeethUp = defaultHead[i].lvlToHide_TeethUp;
                        headSet.lvlToHide_TeethDown = defaultHead[i].lvlToHide_TeethDown;
                        headSet.lvlToHide_Ponytail = defaultHead[i].lvlToHide_Ponytail;

                        headSet.eyeballColor_Left[0] = defaultHead[i].eyeballColor_Left[0];
                        headSet.eyeballColor_Left[1] = defaultHead[i].eyeballColor_Left[1];
                        headSet.eyeballColor_Left[2] = defaultHead[i].eyeballColor_Left[2];
                        headSet.eyeballColor_Left[3] = defaultHead[i].eyeballColor_Left[3];
                        headSet.eyeballColor_Left[4] = defaultHead[i].eyeballColor_Left[4];
                        headSet.eyeballColor_Left[5] = defaultHead[i].eyeballColor_Left[5];

                        headSet.eyeballColor_Right[0] = defaultHead[i].eyeballColor_Right[0];
                        headSet.eyeballColor_Right[1] = defaultHead[i].eyeballColor_Right[1];
                        headSet.eyeballColor_Right[2] = defaultHead[i].eyeballColor_Right[2];
                        headSet.eyeballColor_Right[3] = defaultHead[i].eyeballColor_Right[3];
                        headSet.eyeballColor_Right[4] = defaultHead[i].eyeballColor_Right[4];
                        headSet.eyeballColor_Right[5] = defaultHead[i].eyeballColor_Right[5];

                        headSet.pupilColor_Left[0] = defaultHead[i].pupilColor_Left[0];
                        headSet.pupilColor_Left[1] = defaultHead[i].pupilColor_Left[1];
                        headSet.pupilColor_Left[2] = defaultHead[i].pupilColor_Left[2];
                        headSet.pupilColor_Left[3] = defaultHead[i].pupilColor_Left[3];
                        headSet.pupilColor_Left[4] = defaultHead[i].pupilColor_Left[4];
                        headSet.pupilColor_Left[5] = defaultHead[i].pupilColor_Left[5];

                        headSet.pupilColor_Right[0] = defaultHead[i].pupilColor_Right[0];
                        headSet.pupilColor_Right[1] = defaultHead[i].pupilColor_Right[1];
                        headSet.pupilColor_Right[2] = defaultHead[i].pupilColor_Right[2];
                        headSet.pupilColor_Right[3] = defaultHead[i].pupilColor_Right[3];
                        headSet.pupilColor_Right[4] = defaultHead[i].pupilColor_Right[4];
                        headSet.pupilColor_Right[5] = defaultHead[i].pupilColor_Right[5];
                    }
                }
            }
        }

        public void resetSuit(Suit_Set suitSet, Suit_Set defaultMap)
        {    
            foreach (UrlDir.UrlConfig file in GameDatabase.Instance.GetConfigs("TextureReplacerReplaced"))
            {
                ConfigNode node = file.config.GetNode("SuitSettings");
                if (node != null)
                {
                    ConfigNode savedNode = new ConfigNode();
                    // if the suit set has an entry in the .cfg, try to load the settings, if empty, load the default settings
                    if (node.TryGetNode(suitSet.name, ref savedNode))
                    {
                        Util.log("Settings found for {0}, using them", suitSet.name);
                        bool nodebool = true;
                        Color32 nodeColor = new Color32();
                        int nodeInt = 0;

                        // suit settings
                        /*if (savedNode.TryGetValue("isExclusive", ref nodebool))
                            suitSet.isExclusive = nodebool;
                        else
                            suitSet.isExclusive = defaultMap.isExclusive;*/

                        if (savedNode.TryGetValue("suit_Iva_Safe", ref nodeInt))
                            suitSet.suit_Iva_Safe = nodeInt;
                        else
                            suitSet.suit_Iva_Safe = defaultMap.suit_Iva_Safe;

                        if (savedNode.TryGetValue("suit_Iva_Unsafe", ref nodeInt))
                            suitSet.suit_Iva_Unsafe = nodeInt;
                        else
                            suitSet.suit_Iva_Unsafe = defaultMap.suit_Iva_Unsafe;

                        if (savedNode.TryGetValue("suit_EvaGround_Atmo", ref nodeInt))
                            suitSet.suit_EvaGround_Atmo = nodeInt;
                        else
                            suitSet.suit_EvaGround_Atmo = defaultMap.suit_EvaGround_Atmo;

                        if (savedNode.TryGetValue("suit_EvaGround_NoAtmo", ref nodeInt))
                            suitSet.suit_EvaGround_NoAtmo = nodeInt;
                        else
                            suitSet.suit_EvaGround_NoAtmo = defaultMap.suit_EvaGround_NoAtmo;

                        if (savedNode.TryGetValue("suit_EvaSpace", ref nodeInt))
                            suitSet.suit_EvaSpace = nodeInt;
                        else
                            suitSet.suit_EvaSpace = defaultMap.suit_EvaSpace;

                        //helmet settings
                        if (savedNode.TryGetValue("helmet_Iva_Safe", ref nodeInt))
                            suitSet.helmet_Iva_Safe = nodeInt;
                        else
                            suitSet.helmet_Iva_Safe = defaultMap.helmet_Iva_Safe;

                        if (savedNode.TryGetValue("helmet_Iva_Unsafe", ref nodeInt))
                            suitSet.helmet_Iva_Unsafe = nodeInt;
                        else
                            suitSet.helmet_Iva_Unsafe = defaultMap.helmet_Iva_Unsafe;

                        if (savedNode.TryGetValue("helmet_EvaGround_Atmo", ref nodeInt))
                            suitSet.helmet_EvaGround_Atmo = nodeInt;
                        else
                            suitSet.helmet_EvaGround_Atmo = defaultMap.helmet_EvaGround_Atmo;

                        if (savedNode.TryGetValue("helmet_EvaGround_NoAtmo", ref nodeInt))
                            suitSet.helmet_EvaGround_NoAtmo = nodeInt;
                        else
                            suitSet.helmet_EvaGround_NoAtmo = defaultMap.helmet_EvaGround_NoAtmo;

                        if (savedNode.TryGetValue("helmet_EvaSpace", ref nodeInt))
                            suitSet.helmet_EvaSpace = nodeInt;
                        else
                            suitSet.helmet_EvaSpace = defaultMap.helmet_EvaSpace;

                        //visor settings
                        if (savedNode.TryGetValue("visor_Iva_Safe", ref nodeInt))
                            suitSet.visor_Iva_Safe = nodeInt;
                        else
                            suitSet.visor_Iva_Safe = defaultMap.visor_Iva_Safe;

                        if (savedNode.TryGetValue("visor_Iva_Unsafe", ref nodeInt))
                            suitSet.visor_Iva_Unsafe = nodeInt;
                        else
                            suitSet.visor_Iva_Unsafe = defaultMap.visor_Iva_Unsafe;

                        if (savedNode.TryGetValue("visor_EvaGround_Atmo", ref nodeInt))
                            suitSet.visor_EvaGround_Atmo = nodeInt;
                        else
                            suitSet.visor_EvaGround_Atmo = defaultMap.visor_EvaGround_Atmo;

                        if (savedNode.TryGetValue("visor_EvaGround_NoAtmo", ref nodeInt))
                            suitSet.visor_EvaGround_NoAtmo = nodeInt;
                        else
                            suitSet.visor_EvaGround_NoAtmo = defaultMap.visor_EvaGround_NoAtmo;

                        if (savedNode.TryGetValue("visor_EvaSpace", ref nodeInt))
                            suitSet.visor_EvaSpace = nodeInt;
                        else
                            suitSet.visor_EvaSpace = defaultMap.visor_EvaSpace;

                        // jetpack settings
                        if (savedNode.TryGetValue("jetpack_EvaGround_Atmo", ref nodeInt))
                            suitSet.jetpack_EvaGround_Atmo = nodeInt;
                        else
                            suitSet.jetpack_EvaGround_Atmo = defaultMap.jetpack_EvaGround_Atmo;

                        if (savedNode.TryGetValue("jetpack_EvaGround_NoAtmo", ref nodeInt))
                            suitSet.jetpack_EvaGround_NoAtmo = nodeInt;
                        else
                            suitSet.jetpack_EvaGround_NoAtmo = defaultMap.jetpack_EvaGround_NoAtmo;

                        if (savedNode.TryGetValue("jetpack_EvaSpace", ref nodeInt))
                            suitSet.jetpack_EvaSpace = nodeInt;
                        else
                            suitSet.jetpack_EvaSpace = defaultMap.jetpack_EvaSpace;

                        // visor reflection settings
                        if (savedNode.TryGetValue("visor_Iva_ReflectionAdaptive", ref nodebool))
                            suitSet.visor_Iva_ReflectionAdaptive = nodebool;
                        else
                            suitSet.visor_Iva_ReflectionAdaptive = defaultMap.visor_Iva_ReflectionAdaptive;

                        if (savedNode.TryGetValue("visor_EvaGround_ReflectionAdaptive", ref nodebool))
                            suitSet.visor_EvaGround_ReflectionAdaptive = nodebool;
                        else
                            suitSet.visor_EvaGround_ReflectionAdaptive = defaultMap.visor_EvaGround_ReflectionAdaptive;

                        if (savedNode.TryGetValue("visor_EvaSpace_ReflectionAdaptive", ref nodebool))
                            suitSet.visor_EvaSpace_ReflectionAdaptive = nodebool;
                        else
                            suitSet.visor_EvaSpace_ReflectionAdaptive = defaultMap.visor_EvaSpace_ReflectionAdaptive;

                        // visor base color settings
                        if (savedNode.TryGetValue("visor_Iva_BaseColor[0]", ref nodeColor))
                            suitSet.visor_Iva_BaseColor[0] = nodeColor;
                        else
                            suitSet.visor_Iva_BaseColor[0] = defaultMap.visor_Iva_BaseColor[0];

                        if (savedNode.TryGetValue("visor_Iva_BaseColor[1]", ref nodeColor))
                            suitSet.visor_Iva_BaseColor[1] = nodeColor;
                        else
                            suitSet.visor_Iva_BaseColor[1] = defaultMap.visor_Iva_BaseColor[1];

                        if (savedNode.TryGetValue("visor_Iva_BaseColor[2]", ref nodeColor))
                            suitSet.visor_Iva_BaseColor[2] = nodeColor;
                        else
                            suitSet.visor_Iva_BaseColor[2] = defaultMap.visor_Iva_BaseColor[2];

                        if (savedNode.TryGetValue("visor_Iva_BaseColor[3]", ref nodeColor))
                            suitSet.visor_Iva_BaseColor[3] = nodeColor;
                        else
                            suitSet.visor_Iva_BaseColor[3] = defaultMap.visor_Iva_BaseColor[3];

                        if (savedNode.TryGetValue("visor_Iva_BaseColor[4]", ref nodeColor))
                            suitSet.visor_Iva_BaseColor[4] = nodeColor;
                        else
                            suitSet.visor_Iva_BaseColor[4] = defaultMap.visor_Iva_BaseColor[4];

                        if (savedNode.TryGetValue("visor_Iva_BaseColor[5]", ref nodeColor))
                            suitSet.visor_Iva_BaseColor[5] = nodeColor;
                        else
                            suitSet.visor_Iva_BaseColor[5] = defaultMap.visor_Iva_BaseColor[5];


                        if (savedNode.TryGetValue("visor_EvaGround_BaseColor[0]", ref nodeColor))
                            suitSet.visor_EvaGround_BaseColor[0] = nodeColor;
                        else
                            suitSet.visor_EvaGround_BaseColor[0] = defaultMap.visor_EvaGround_BaseColor[0];

                        if (savedNode.TryGetValue("visor_EvaGround_BaseColor[1]", ref nodeColor))
                            suitSet.visor_EvaGround_BaseColor[1] = nodeColor;
                        else
                            suitSet.visor_EvaGround_BaseColor[1] = defaultMap.visor_EvaGround_BaseColor[1];

                        if (savedNode.TryGetValue("visor_EvaGround_BaseColor[2]", ref nodeColor))
                            suitSet.visor_EvaGround_BaseColor[2] = nodeColor;
                        else
                            suitSet.visor_EvaGround_BaseColor[2] = defaultMap.visor_EvaGround_BaseColor[2];

                        if (savedNode.TryGetValue("visor_EvaGround_BaseColor[3]", ref nodeColor))
                            suitSet.visor_EvaGround_BaseColor[3] = nodeColor;
                        else
                            suitSet.visor_EvaGround_BaseColor[3] = defaultMap.visor_EvaGround_BaseColor[3];

                        if (savedNode.TryGetValue("visor_EvaGround_BaseColor[4]", ref nodeColor))
                            suitSet.visor_EvaGround_BaseColor[4] = nodeColor;
                        else
                            suitSet.visor_EvaGround_BaseColor[4] = defaultMap.visor_EvaGround_BaseColor[4];

                        if (savedNode.TryGetValue("visor_EvaGround_BaseColor[5]", ref nodeColor))
                            suitSet.visor_EvaGround_BaseColor[5] = nodeColor;
                        else
                            suitSet.visor_EvaGround_BaseColor[5] = defaultMap.visor_EvaGround_BaseColor[5];


                        if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[0]", ref nodeColor))
                            suitSet.visor_EvaSpace_BaseColor[0] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_BaseColor[0] = defaultMap.visor_EvaSpace_BaseColor[0];

                        if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[1]", ref nodeColor))
                            suitSet.visor_EvaSpace_BaseColor[1] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_BaseColor[1] = defaultMap.visor_EvaSpace_BaseColor[1];

                        if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[2]", ref nodeColor))
                            suitSet.visor_EvaSpace_BaseColor[2] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_BaseColor[2] = defaultMap.visor_EvaSpace_BaseColor[2];

                        if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[3]", ref nodeColor))
                            suitSet.visor_EvaSpace_BaseColor[3] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_BaseColor[3] = defaultMap.visor_EvaSpace_BaseColor[3];

                        if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[4]", ref nodeColor))
                            suitSet.visor_EvaSpace_BaseColor[4] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_BaseColor[4] = defaultMap.visor_EvaSpace_BaseColor[4];

                        if (savedNode.TryGetValue("visor_EvaSpace_BaseColor[5]", ref nodeColor))
                            suitSet.visor_EvaSpace_BaseColor[5] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_BaseColor[5] = defaultMap.visor_EvaSpace_BaseColor[5];



                        // visor reflection color settings
                        if (savedNode.TryGetValue("visor_Iva_ReflectionColor[0]", ref nodeColor))
                            suitSet.visor_Iva_ReflectionColor[0] = nodeColor;
                        else
                            suitSet.visor_Iva_ReflectionColor[0] = defaultMap.visor_Iva_ReflectionColor[0];

                        if (savedNode.TryGetValue("visor_Iva_ReflectionColor[1]", ref nodeColor))
                            suitSet.visor_Iva_ReflectionColor[1] = nodeColor;
                        else
                            suitSet.visor_Iva_ReflectionColor[1] = defaultMap.visor_Iva_ReflectionColor[1];

                        if (savedNode.TryGetValue("visor_Iva_ReflectionColor[2]", ref nodeColor))
                            suitSet.visor_Iva_ReflectionColor[2] = nodeColor;
                        else
                            suitSet.visor_Iva_ReflectionColor[2] = defaultMap.visor_Iva_ReflectionColor[2];

                        if (savedNode.TryGetValue("visor_Iva_ReflectionColor[3]", ref nodeColor))
                            suitSet.visor_Iva_ReflectionColor[3] = nodeColor;
                        else
                            suitSet.visor_Iva_ReflectionColor[3] = defaultMap.visor_Iva_ReflectionColor[3];

                        if (savedNode.TryGetValue("visor_Iva_ReflectionColor[4]", ref nodeColor))
                            suitSet.visor_Iva_ReflectionColor[4] = nodeColor;
                        else
                            suitSet.visor_Iva_ReflectionColor[4] = defaultMap.visor_Iva_ReflectionColor[4];

                        if (savedNode.TryGetValue("visor_Iva_ReflectionColor[5]", ref nodeColor))
                            suitSet.visor_Iva_ReflectionColor[5] = nodeColor;
                        else
                            suitSet.visor_Iva_ReflectionColor[5] = defaultMap.visor_Iva_ReflectionColor[5];


                        if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[0]", ref nodeColor))
                            suitSet.visor_EvaGround_ReflectionColor[0] = nodeColor;
                        else
                            suitSet.visor_EvaGround_ReflectionColor[0] = defaultMap.visor_EvaGround_ReflectionColor[0];

                        if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[1]", ref nodeColor))
                            suitSet.visor_EvaGround_ReflectionColor[1] = nodeColor;
                        else
                            suitSet.visor_EvaGround_ReflectionColor[1] = defaultMap.visor_EvaGround_ReflectionColor[1];

                        if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[2]", ref nodeColor))
                            suitSet.visor_EvaGround_ReflectionColor[2] = nodeColor;
                        else
                            suitSet.visor_EvaGround_ReflectionColor[2] = defaultMap.visor_EvaGround_ReflectionColor[2];

                        if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[3]", ref nodeColor))
                            suitSet.visor_EvaGround_ReflectionColor[3] = nodeColor;
                        else
                            suitSet.visor_EvaGround_ReflectionColor[3] = defaultMap.visor_EvaGround_ReflectionColor[3];

                        if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[4]", ref nodeColor))
                            suitSet.visor_EvaGround_ReflectionColor[4] = nodeColor;
                        else
                            suitSet.visor_EvaGround_ReflectionColor[4] = defaultMap.visor_EvaGround_ReflectionColor[4];

                        if (savedNode.TryGetValue("visor_EvaGround_ReflectionColor[5]", ref nodeColor))
                            suitSet.visor_EvaGround_ReflectionColor[5] = nodeColor;
                        else
                            suitSet.visor_EvaGround_ReflectionColor[5] = defaultMap.visor_EvaGround_ReflectionColor[5];


                        if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[0]", ref nodeColor))
                            suitSet.visor_EvaSpace_ReflectionColor[0] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_ReflectionColor[0] = defaultMap.visor_EvaSpace_ReflectionColor[0];

                        if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[1]", ref nodeColor))
                            suitSet.visor_EvaSpace_ReflectionColor[1] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_ReflectionColor[1] = defaultMap.visor_EvaSpace_ReflectionColor[1];

                        if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[2]", ref nodeColor))
                            suitSet.visor_EvaSpace_ReflectionColor[2] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_ReflectionColor[2] = defaultMap.visor_EvaSpace_ReflectionColor[2];

                        if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[3]", ref nodeColor))
                            suitSet.visor_EvaSpace_ReflectionColor[3] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_ReflectionColor[3] = defaultMap.visor_EvaSpace_ReflectionColor[3];

                        if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[4]", ref nodeColor))
                            suitSet.visor_EvaSpace_ReflectionColor[4] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_ReflectionColor[4] = defaultMap.visor_EvaSpace_ReflectionColor[4];

                        if (savedNode.TryGetValue("visor_EvaSpace_ReflectionColor[5]", ref nodeColor))
                            suitSet.visor_EvaSpace_ReflectionColor[5] = nodeColor;
                        else
                            suitSet.visor_EvaSpace_ReflectionColor[5] = defaultMap.visor_EvaSpace_ReflectionColor[5];
                    }
                    // if the suit set has no entry in the .cfg, load the default settings
                    else
                    {
                        //suitSet.isExclusive = defaultMap.isExclusive;

                        // suit settings
                        suitSet.suit_Iva_Safe = defaultMap.suit_Iva_Safe;
                        suitSet.suit_Iva_Unsafe = defaultMap.suit_Iva_Unsafe;
                        suitSet.suit_EvaGround_Atmo = defaultMap.suit_EvaGround_Atmo;
                        suitSet.suit_EvaGround_NoAtmo = defaultMap.suit_EvaGround_NoAtmo;
                        suitSet.suit_EvaSpace = defaultMap.suit_EvaSpace;

                        //helmet settings
                        suitSet.helmet_Iva_Safe = defaultMap.helmet_Iva_Safe;
                        suitSet.helmet_Iva_Unsafe = defaultMap.helmet_Iva_Unsafe;
                        suitSet.helmet_EvaGround_Atmo = defaultMap.helmet_EvaGround_Atmo;
                        suitSet.helmet_EvaGround_NoAtmo = defaultMap.helmet_EvaGround_NoAtmo;
                        suitSet.helmet_EvaSpace = defaultMap.helmet_EvaSpace;

                        //visor settings
                        suitSet.visor_Iva_Safe = defaultMap.visor_Iva_Safe;
                        suitSet.visor_Iva_Unsafe = defaultMap.visor_Iva_Unsafe;
                        suitSet.visor_EvaGround_Atmo = defaultMap.visor_EvaGround_Atmo;
                        suitSet.visor_EvaGround_NoAtmo = defaultMap.visor_EvaGround_NoAtmo;
                        suitSet.visor_EvaSpace = defaultMap.visor_EvaSpace;

                        // jetpack settings
                        suitSet.jetpack_EvaGround_Atmo = defaultMap.jetpack_EvaGround_Atmo;
                        suitSet.jetpack_EvaGround_NoAtmo = defaultMap.jetpack_EvaGround_NoAtmo;
                        suitSet.jetpack_EvaSpace = defaultMap.jetpack_EvaSpace;

                        // visor reflection settings
                        suitSet.visor_Iva_ReflectionAdaptive = defaultMap.visor_Iva_ReflectionAdaptive;
                        suitSet.visor_EvaGround_ReflectionAdaptive = defaultMap.visor_EvaGround_ReflectionAdaptive;
                        suitSet.visor_EvaSpace_ReflectionAdaptive = defaultMap.visor_EvaSpace_ReflectionAdaptive;

                        // visor Base color settings
                        suitSet.visor_Iva_BaseColor[0] = defaultMap.visor_Iva_BaseColor[0];
                        suitSet.visor_Iva_BaseColor[1] = defaultMap.visor_Iva_BaseColor[1];
                        suitSet.visor_Iva_BaseColor[2] = defaultMap.visor_Iva_BaseColor[2];
                        suitSet.visor_Iva_BaseColor[3] = defaultMap.visor_Iva_BaseColor[3];
                        suitSet.visor_Iva_BaseColor[4] = defaultMap.visor_Iva_BaseColor[4];
                        suitSet.visor_Iva_BaseColor[5] = defaultMap.visor_Iva_BaseColor[5];


                        suitSet.visor_EvaGround_BaseColor[0] = defaultMap.visor_EvaGround_BaseColor[0];
                        suitSet.visor_EvaGround_BaseColor[1] = defaultMap.visor_EvaGround_BaseColor[1];
                        suitSet.visor_EvaGround_BaseColor[2] = defaultMap.visor_EvaGround_BaseColor[2];
                        suitSet.visor_EvaGround_BaseColor[3] = defaultMap.visor_EvaGround_BaseColor[3];
                        suitSet.visor_EvaGround_BaseColor[4] = defaultMap.visor_EvaGround_BaseColor[4];
                        suitSet.visor_EvaGround_BaseColor[5] = defaultMap.visor_EvaGround_BaseColor[5];


                        suitSet.visor_EvaSpace_BaseColor[0] = defaultMap.visor_EvaSpace_BaseColor[0];
                        suitSet.visor_EvaSpace_BaseColor[1] = defaultMap.visor_EvaSpace_BaseColor[1];
                        suitSet.visor_EvaSpace_BaseColor[2] = defaultMap.visor_EvaSpace_BaseColor[2];
                        suitSet.visor_EvaSpace_BaseColor[3] = defaultMap.visor_EvaSpace_BaseColor[3];
                        suitSet.visor_EvaSpace_BaseColor[4] = defaultMap.visor_EvaSpace_BaseColor[4];
                        suitSet.visor_EvaSpace_BaseColor[5] = defaultMap.visor_EvaSpace_BaseColor[5];


                        // visor Reflection color settings
                        suitSet.visor_Iva_ReflectionColor[0] = defaultMap.visor_Iva_ReflectionColor[0];
                        suitSet.visor_Iva_ReflectionColor[1] = defaultMap.visor_Iva_ReflectionColor[1];
                        suitSet.visor_Iva_ReflectionColor[2] = defaultMap.visor_Iva_ReflectionColor[2];
                        suitSet.visor_Iva_ReflectionColor[3] = defaultMap.visor_Iva_ReflectionColor[3];
                        suitSet.visor_Iva_ReflectionColor[4] = defaultMap.visor_Iva_ReflectionColor[4];
                        suitSet.visor_Iva_ReflectionColor[5] = defaultMap.visor_Iva_ReflectionColor[5];


                        suitSet.visor_EvaGround_ReflectionColor[0] = defaultMap.visor_EvaGround_ReflectionColor[0];
                        suitSet.visor_EvaGround_ReflectionColor[1] = defaultMap.visor_EvaGround_ReflectionColor[1];
                        suitSet.visor_EvaGround_ReflectionColor[2] = defaultMap.visor_EvaGround_ReflectionColor[2];
                        suitSet.visor_EvaGround_ReflectionColor[3] = defaultMap.visor_EvaGround_ReflectionColor[3];
                        suitSet.visor_EvaGround_ReflectionColor[4] = defaultMap.visor_EvaGround_ReflectionColor[4];
                        suitSet.visor_EvaGround_ReflectionColor[5] = defaultMap.visor_EvaGround_ReflectionColor[5];


                        suitSet.visor_EvaSpace_ReflectionColor[0] = defaultMap.visor_EvaSpace_ReflectionColor[0];
                        suitSet.visor_EvaSpace_ReflectionColor[1] = defaultMap.visor_EvaSpace_ReflectionColor[1];
                        suitSet.visor_EvaSpace_ReflectionColor[2] = defaultMap.visor_EvaSpace_ReflectionColor[2];
                        suitSet.visor_EvaSpace_ReflectionColor[3] = defaultMap.visor_EvaSpace_ReflectionColor[3];
                        suitSet.visor_EvaSpace_ReflectionColor[4] = defaultMap.visor_EvaSpace_ReflectionColor[4];
                        suitSet.visor_EvaSpace_ReflectionColor[5] = defaultMap.visor_EvaSpace_ReflectionColor[5];
                    }
                }
            }


               

        }       



    }
}