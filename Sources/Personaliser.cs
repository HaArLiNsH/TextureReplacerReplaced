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

using KSP.UI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        /// Male and female heads textures (minus excluded).
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
        /// List of the suit set (minus excluded). 
        /// </summary>
        private readonly List<Suit_Set> KerbalSuitsDB_cleaned = new List<Suit_Set>();

        /// <summary>
        /// Here we have the list of all the kerbal and the head set each one uses.
        /// </summary>
       /* public Dictionary<string, int>[] maleAndfemaleHeadNumberOfUSe = new Dictionary<string, int>[]
        {
            new Dictionary<string, int>(), new Dictionary<string, int>()
        };*/

        public Dictionary<string, string> KerbalAndTheirHeadsDB = new Dictionary<string, string>();

        public Dictionary<string, int> headCount = new Dictionary<string, int>();

        /// <summary>
        /// List of your personalized Kerbals with their KerbalData
        /// </summary>
        private readonly Dictionary<string, KerbalData> gameKerbalsDB = new Dictionary<string, KerbalData>();

        /// <summary>
        /// Backed-up personalized textures from main configuration files.
        /// <para>These are used to initialise kerbals if a saved game doesn't contain `TRScenario`. </para>
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
        /// List of cabin specific suits
        /// </summary>
        //private readonly Dictionary<string, Suit_Set> cabinSuits = new Dictionary<string, Suit_Set>();

        /// <summary>
        /// Used for the helmet removal
        /// </summary>
        private Mesh[] helmetMesh = { null, null };

        /// <summary>
        /// Used for the helmet removal
        /// </summary>
        private Mesh[] visorMesh = { null, null };

        /// <summary>
        /// Remove IVA helmets in safe situations (landed/splashed and in orbit).
        /// <para>This is only initial setting for new games! Use the GUI to change it later. </para>
        /// </summary>
        public bool isHelmetRemovalEnabled = true;

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
        /// Spawn a Kerbal on EVA in his/her IVA suit without helmet and jetpack when in breathable atmosphere (+ sufficient pressure).
        /// /// <para>This is only initial setting for new games! Use the GUI to change it later. </para>
        /// </summary>
        public bool isAtmSuitEnabled = true;

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

        /* =========================================================================================
         * general TRR options
         * used in the main TRR configuration Gui
         * =========================================================================================
         */

        /// <summary>
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
        public bool isCollarRemovalEnabled = false;

        public bool useKspSkin = true;


        // !!!!!!!!!!!!!!!!!!!!!!!!   Need to implement these options and make a GUI for them !!!!!!!!!!!!!!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!   Maybe a new class  ??                                    !!!!!!!!!!!!!!!
        /* =========================================================================================
         * personal suit options
         * used for each suit texture pack
         * =========================================================================================
         */
/*
        /// <summary>
        /// Do the suit set is made to be used by the Veteran kerbals?
        /// </summary>
        public bool isMadeforveteran = false;

        /// <summary>
        /// Do the suit set include the veteran version for the suit ?
        /// </summary>
        public bool includeVeteran = false;

        /// <summary>
        /// Do the suit set is made to be used by the Badass(fearless) kerbals  ?
        /// </summary>
        public bool isMadeForBadass = false;

        /// <summary>
        /// Do the suit set include the Badass version for the suit ?
        /// </summary>
        public bool includeBadass = false;

        /// <summary>
        /// Do the suit set include the veteran badass version for the suit ?
        /// </summary>
        public bool includeVeteranBadass = false;

        /// <summary>
        /// Do the suit set use the male version of the suits ? 
        /// </summary>
        public bool UseMaleSuitenabled = true;

        /// <summary>
        /// Do the suit set use the female version of the suits ? 
        /// </summary>
        public bool UseFemaleSuitenabled = true;

        /// <summary>
        /// Is this suit set exclusive to his configured class? 
        /// </summary>
        public bool isSuitExclusive_Class = true;

        /// <summary>
        /// Is this suit set exclusive to his configured kerbal? 
        /// </summary>
        public bool isSuitExclusive_Kerbal = false;

        /// <summary>
        /// Force use Atmospheric IVA suit
        /// </summary>
        public bool ForceisAtmoIVAsuitEnabled = false;

        /// <summary>
        /// Force DON'T use Atmospheric IVA suit
        /// </summary>
        public bool ForceisAtmoIVAsuitDisabled = false;

        /// <summary>
        /// Force use EVA ground suit
        /// </summary>
        public bool ForceisEVAgroundSuitEnabled = false;

        /// <summary>
        /// Force DON'T use EVA ground suit
        /// </summary>
        public bool ForceisEVAgroundSuitDisabled = false;

        /// <summary>
        /// Force always use the IVA helmet
        /// </summary>
        public bool forceIVAhelmet = false;

        /// <summary>
        /// Force never use the IVA helmet
        /// </summary>
        public bool ForceIVAhelmetRemoval = false;

        /// <summary>
        /// Force never use the EVA ground helmet
        /// </summary>
        public bool ForceEVAgroundHelmetRemoval = false;

        /// <summary>
        /// Force never use the EVA space helmet
        /// </summary>
        public bool ForceEVAspaceHelmetRemoval = false;

        /// <summary>
        /// Force IVA suit state when toggle suit (bypass atmospheric & safe situation check)
        /// </summary>
        public bool ForceIvaSuitToggle = false;

        /// <summary>
        /// Force EVA ground suit state when toggle suit (bypass under suborbital check)
        /// </summary>
        public bool ForceEvaGroundSuitToggle = false;       

        /// <summary>
        /// Force IVA suit use IVA helmet
        /// </summary>
        public bool ForceIvaSuitUse_IVAhelmet = false;

        /// <summary>
        /// Force IVA suit use EVA ground helmet
        /// </summary>
        public bool ForceIvaSuitUse_EVAgroundHelmet = false;

        /// <summary>
        /// Force IVA suit use EVA space helmet
        /// </summary>
        public bool ForceIvaSuitUse_EVAspaceHelmet = true;

        /// <summary>
        /// Force use reflections for this suit
        /// </summary>
        public bool ForceUseReflections = false;

        /// <summary>
        /// Force DONT use reflections for this suit
        /// </summary>
        public bool ForceNoReflections = false;

        /// <summary>
        /// Choose the reflections colors 
        /// </summary>
        public Color ForcedVisorReflectionColour = new Color(0.5f, 0.5f, 0.5f);

        /// <summary>
        /// Force collar removal on IVA suits (for later)
        /// </summary>
        public bool ForceCollarRemoval = false;

        /// <summary>
        /// Force collar use on the IVA suits (for later)
        /// </summary>
        public bool ForceCollarUse = false;

        /// <summary>
        /// Force IVA suit on ground with no atmosphere
        /// </summary>
        public bool ForceIVAgroundSuit = false;

        /// <summary>
        /// Force IVA suit in space
        /// </summary>
        public bool ForceIVAspaceSuit = false;     */     
                
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Component bound to internal models that triggers Kerbal texture personalization
        /// when the internal model changes.
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private class TRR_IvaModule : MonoBehaviour
        {
            /// <summary>
            /// Called at the Start() <see cref="Personaliser.TRR_IvaModule"/>
            /// </summary>
            public void Start()
            {
                Personaliser.instance.personaliseIva(GetComponent<Kerbal>());
                Destroy(this);
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
                switch (personaliser.personaliseEva(part, actualSuitState))
                {
                    case 0:     //IVA suit, if no air switch to state 1 : EVAground
                        actualSuitState = 0;
                        hasEvaSuit = false;
                        hasEvaGroundSuit = false;
                        if (reflectionScript != null)
                            reflectionScript.setActive(hasEvaSuit);
                        //ScreenMessages.PostScreenMessage("IVA wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                    case 1:     //EVAground suit
                        actualSuitState = 1;
                        hasEvaSuit = true;
                        hasEvaGroundSuit = true;
                        if (reflectionScript != null)
                            reflectionScript.setActive(hasEvaSuit);
                        //ScreenMessages.PostScreenMessage("EVA ground wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                    case 2:     //EVA suit
                        actualSuitState = 2;
                        hasEvaSuit = true;
                        hasEvaGroundSuit = false;
                        if (reflectionScript != null)
                            reflectionScript.setActive(hasEvaSuit);
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
                Personaliser personaliser = Personaliser.instance;

                if (!isInitialised)
                {
                    if (!personaliser.isAtmSuitEnabled)
                    {
                        Events.First().active = false;
                        hasEvaSuit = true;
                        actualSuitState = 2;
                    }

                    isInitialised = true;
                }

                if (personaliser.personaliseEva(part, actualSuitState) == 2)
                {
                    actualSuitState = 2;
                    hasEvaSuit = true;
                }

                if (Reflections.instance.isVisorReflectionEnabled
                && Reflections.instance.reflectionType == Reflections.Type.REAL)
                {
                    reflectionScript = new Reflections.Script(part, 1);
                    reflectionScript.setActive(hasEvaSuit);
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

                switch (personaliser.personaliseEva(part, actualSuitState))
                {
                    case 0:     //IVA suit, if no air switch to state 1 : EVAground
                        actualSuitState = 0;
                        hasEvaSuit = false;
                        hasEvaGroundSuit = false;
                        if (reflectionScript != null)
                            reflectionScript.setActive(hasEvaSuit);
                        //ScreenMessages.PostScreenMessage("IVA wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                    case 1:     //EVAground suit
                        actualSuitState = 1;
                        hasEvaSuit = true;
                        hasEvaGroundSuit = true;
                        if (reflectionScript != null)
                            reflectionScript.setActive(hasEvaSuit);
                        //ScreenMessages.PostScreenMessage("EVA ground wanted", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                        break;
                    case 2:     //EVA suit
                        actualSuitState = 2;
                        hasEvaSuit = true;
                        hasEvaGroundSuit = false;
                        if (reflectionScript != null)
                            reflectionScript.setActive(hasEvaSuit);
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
            //Vessel vessel = kerbal.InVessel;
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

            /*List<Suit_Set> genderSuits = maleAndfemaleSuitsDB_cleaned[0];

            // Use female suits only if available, fall back to male suits otherwise.
            if (kerbalData.gender != 0 && maleAndfemaleSuitsDB_cleaned[1].Count != 0)
                genderSuits = maleAndfemaleSuitsDB_cleaned[1];
            else if (genderSuits.Count == 0)
                return defaultSuit;*/

            // We must use a different prime here to increase randomization so that the same head is not always combined with
            // the same suit.
            int number = ((kerbalData.hash + kerbal.name.Length) * 2053) & 0x7fffffff;
            return KerbalSuitsDB_cleaned[number % KerbalSuitsDB_cleaned.Count];
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is the main method that personalize and change the texture of your kerbal. 
        /// </summary>
        /// <param name="component">The kerbal we want to personalize, in term of <see cref="Component"/></param>
        /// <param name="protoKerbal">The <see cref="ProtoCrewMember"/> data of the kerbal we want to personalize</param>
        /// <param name="cabin">A handle to the part that contains this Kerbal</param>
        /// <param name="needsEVASuit">Does the kerbal need a EVA suit? (space or ground)</param>
        /// <param name="needsEVAgroundSuit">Does the kerbal need a EVA ground suit ?</param>
        /// /// ////////////////////////////////////////////////////////////////////////////////////////
        private void personaliseKerbal(Component component, ProtoCrewMember protoKerbal, Part cabin, bool needsEVASuit, bool needsEVAgroundSuit, int suitState)
        {
            Personaliser personaliser = Personaliser.instance;

            KerbalData kerbalData = getKerbalData(protoKerbal);

            int level = protoKerbal.experienceLevel;

            bool isEva = cabin == null;

            int gender = kerbalData.gender;
            bool isVeteran = kerbalData.isVeteran;
            bool isBadass = kerbalData.isBadass;

            Head_Set personaliseKerbal_Head = getKerbalHead(protoKerbal, kerbalData);
           // Suit_Set personaliseKerbal_Suit = null;

            // if (isEva || !cabinSuits.TryGetValue(cabin.partInfo.name, out kerbalData.cabinSuit))
            // personaliseKerbal_Suit = getKerbalSuit(protoKerbal, kerbalData);

            //if (isEva)
            Suit_Set personaliseKerbal_Suit = getKerbalSuit(protoKerbal, kerbalData);

            Suit_Filter suit_Filter = new Suit_Filter(kerbalData,level, personaliseKerbal_Suit);
            Suit_Selector suit_Selector = new Suit_Selector(kerbalData, level, personaliseKerbal_Suit);

            //personaliseKerbal_Head = personaliseKerbal_Head == defaulMaleAndFemaleHeads[(int)protoKerbal.gender] ? null : personaliseKerbal_Head;
            //personaliseKerbal_Suit = (isEva && needsEVASuit) || kerbalData.cabinSuit == null ? personaliseKerbal_Suit : kerbalData.cabinSuit;
            //personaliseKerbal_Suit = personaliseKerbal_Suit == defaultSuit ? null : personaliseKerbal_Suit;

            Transform model = isEva ? component.transform.Find("model01") : component.transform.Find("kbIVA@idle/model01");
            Transform flag = isEva ? component.transform.Find("model/kbEVA_flagDecals") : null;

            if (isEva)
                flag.GetComponent<Renderer>().enabled = needsEVASuit;

            // We must include hidden meshes, since flares are hidden when light is turned off.
            // All other meshes are always visible, so no performance hit here.
            foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>(true))
            {
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
                                //Util.log("+++++ {0} is level {1} : {2}.lvlToHide_Eye_Left  = {3} +++++", protoKerbal.name, protoKerbal.experienceLevel, personaliseKerbal_Head.name, personaliseKerbal_Head.lvlToHide_Eye_Left);
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
                            if (personaliseKerbal_Head != null)
                            {
                                newTexture = personaliseKerbal_Head.get_headTexture(protoKerbal.experienceLevel);
                                newNormalMap = personaliseKerbal_Head.get_headTextureNRM(protoKerbal.experienceLevel);                                
                            }
                            break;

                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pCube1":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_polySurface51":
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
                        



                        case "body01":
                        case "mesh_female_kerbalAstronaut01_body01":
                            
                           // defaultSuit.suit_EvaSpace

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
                            /*if (isEva)
                                smr.enabled = needsEVASuit;
                            else
                                smr.sharedMesh = needsEVASuit ? helmetMesh[(int)protoKerbal.gender] : null;*/

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
                                            break;
                                        }                                            
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            suit_Selector.select_helmet_EvaGround_NoAtmo(out newTexture, out newNormalMap);
                                            break;
                                        }  
                                        
                                    }
                                    else if (needsEVASuit) // if in space
                                    {
                                        if (personaliseKerbal_Suit.helmet_EvaGround_NoAtmo > 2)
                                        {
                                            smr.enabled = false;
                                            break;
                                        }
                                        else
                                        {
                                            smr.enabled = true;
                                            suit_Selector.select_helmet_EvaSpace(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                    else // if on the ground with atmo
                                    {
                                        if (personaliseKerbal_Suit.helmet_EvaGround_Atmo > 2) // hide the helmet 
                                        {
                                            smr.enabled = false;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
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
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            suit_Selector.select_visor_EvaGround_NoAtmo(out newTexture, out newNormalMap);
                                            
                                            break;
                                        }

                                    }
                                    else if (needsEVASuit) // if in space
                                    {
                                        if (personaliseKerbal_Suit.visor_EvaGround_NoAtmo > 2)
                                        {
                                            smr.enabled = false;
                                            break;
                                        }
                                        else
                                        {
                                            smr.enabled = true;
                                            suit_Selector.select_visor_EvaSpace(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                    else // if on the ground with atmo
                                    {
                                        if (personaliseKerbal_Suit.visor_EvaGround_Atmo > 2) // hide the visor 
                                        {
                                            smr.enabled = false;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            suit_Selector.select_visor_EvaGround_Atmo(out newTexture, out newNormalMap);
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
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = visorMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_visor_Iva_Unsafe(out newTexture, out newNormalMap);
                                            break;
                                        }
                                    }
                                    else // if in vehicle safe
                                    {
                                        if (personaliseKerbal_Suit.visor_Iva_Safe > 2) // hide the visor 
                                        {
                                            smr.enabled = false;
                                            smr.sharedMesh = null;
                                            break;
                                        }
                                        else // otherwise, select the good one and show it
                                        {
                                            smr.enabled = true;
                                            smr.sharedMesh = visorMesh[(int)protoKerbal.gender];
                                            suit_Selector.select_visor_Iva_Safe(out newTexture, out newNormalMap);
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
                                        if (personaliseKerbal_Suit.jetpack_EvaGround_NoAtmo > 1)
                                        {
                                            smr.enabled = false;
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

                            /* if (isEva)
                             {
                                 smr.enabled = needsEVASuit;
                                 if (personaliseKerbal_Suit != null)
                                 {
                                     if (isEva)
                                     {
                                         if (needsEVASuit && needsEVAgroundSuit)
                                         {
                                             suit_Filter.get_jetpack_EvaGround(out newTexture, out newNormalMap);
                                             break;
                                         }
                                         else if (needsEVASuit)
                                         {
                                             suit_Filter.get_jetpack_EvaSpace(out newTexture, out newNormalMap);
                                             break;
                                         }
                                         else
                                         {                                            
                                             break;
                                         }
                                     }
                                     else
                                     {                                        
                                         break;
                                     }
                                 }
                             }

                            break;*/

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
                                        if (personaliseKerbal_Suit.jetpack_EvaGround_NoAtmo > 1)
                                        {
                                            smr.enabled = false;
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
                            /* if (isEva)
                             {
                                 smr.enabled = needsEVASuit;
                                 if (personaliseKerbal_Suit != null)
                                 {
                                     if (isEva)
                                     {
                                         if (needsEVASuit && needsEVAgroundSuit)
                                         {
                                             suit_Filter.get_jetpack_EvaGround(out newTexture, out newNormalMap);
                                             break;
                                         }
                                         else if (needsEVASuit)
                                         {
                                             suit_Filter.get_jetpack_EvaSpace(out newTexture, out newNormalMap);
                                             break;
                                         }
                                         else
                                         {
                                             break;
                                         }
                                     }
                                     else
                                     {
                                         break;
                                     }
                                 }
                             }
                             break;*/
                    }

                    if (newTexture != null)
                        material.mainTexture = newTexture;

                    if (newNormalMap != null)
                        material.SetTexture(Util.BUMPMAP_PROPERTY, newNormalMap);
                }
            }
        }
        
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Personalize Kerbals in an internal space of a vessel. Used by <see cref="TRR_IvaModule"/>
        /// </summary>
        /// <param name="kerbal">The kerbal we want to personalize</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void personaliseIva(Kerbal kerbal)
        {
            bool needsSuit = !isHelmetRemovalEnabled || !isSituationSafe(kerbal.InVessel);

            Personaliser personaliser = Personaliser.instance;
           
            personaliseKerbal(kerbal, kerbal.protoCrewMember, kerbal.InPart, needsSuit, false, 0);
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
            if (!isHelmetRemovalEnabled || vessel == null)
                return;

            foreach (Part part in vessel.parts.Where(p => p.internalModel != null))
            {
                Kerbal[] kerbals = part.internalModel.GetComponentsInChildren<Kerbal>();
                             

                if (kerbals.Length != 0)
                {
                    bool hideHelmets = isSituationSafe(vessel);
                    //bool hideHelmets = (isSituationSafe(vessel) && isHelmetRemovalEnabled);
                    //Util.log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                    //Util.log("hidehelmet = : {0}", hideHelmets);

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
        private int personaliseEva(Part evaPart, int suitSelection)
        {
            int selection = suitSelection;
            bool evaSuit = false;
            bool evaGroundSuit = false;

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
               // if (selection == 0) ScreenMessages.PostScreenMessage("IVA suit", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                //else if (selection== 1) ScreenMessages.PostScreenMessage("EVA Ground suit", 2.0f, ScreenMessageStyle.UPPER_CENTER);
               // else if (selection== 2) ScreenMessages.PostScreenMessage("EVA Space suit", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                personaliseKerbal(evaPart, crew[0], null, evaSuit, evaGroundSuit,selection);
            }
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

            //string sceneName = SceneManager.GetSceneByName("").g

            //GameObject[] goArray = SceneManager.LoadScene("Menu_Level").GetRootGameObjects();
           /* Util.log("++++++++++++++++++++++++++++++++++++ pouet+++++++++++++++++++++++++++++++++++++++++");

            Util.log("scene count = {0}",SceneManager.sceneCount);
            GameObject[] goArray = SceneManager.GetSceneByName("VABmodern").GetRootGameObjects();
            if (goArray.Length > 0)
            {
                foreach (GameObject rootGo in goArray)
                {
                    Util.log(rootGo.name);
                }
                //GameObject rootGo = goArray[0];
                // Do something with rootGo here...                          

            }*/


           // KSPAssets.Loaders.AssetLoader.


            foreach (ProtoCrewMember protoKerb in roster.Crew)
            {
                Util.log(protoKerb.name);
            }


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
                //string headName = kerbalData.head == null ? "GENERIC" : kerbalData.head.name;
                // string suitName = kerbalData.suit == null ? "GENERIC" : kerbalData.suit.name; 

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
            ConfigNode defaultNode = new ConfigNode();
            if (node.TryGetNode("DEFAULT_SUIT", ref defaultNode))
            {
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
                    //Util.log("Settings found for {0}, using them", suitSet.name);
                    bool nodebool = true;
                    Color32 nodeColor = new Color32(255, 255, 255, 255);                    
                    int nodeInt = 0;

                    // suit settings
                    if (savedNode.TryGetValue("isExclusive", ref nodebool))
                        suitSet.isExclusive = nodebool;
                    else
                        suitSet.isExclusive = defaultMap.isExclusive;

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
                    suitSet.isExclusive = defaultMap.isExclusive;

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

                    // visor reflection color settings
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

        private static void saveSuitConfig(ConfigNode node, List<Suit_Set> map, Suit_Set defaultMap)
        {
            ConfigNode defaultNode = new ConfigNode();
            node.AddNode("DEFAULT_SUIT", defaultNode);

            defaultNode.AddValue("isExclusive", defaultMap.isExclusive);

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

                subNode.AddValue("isExclusive", suitSet.isExclusive);

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

            ConfigNode defaultNode = new ConfigNode();
            if (node.TryGetNode("DEFAULT_MALE", ref defaultNode))
            {
                int nodeLvl = new int();
                bool nodeBool = false;
                Color32 nodeColor = new Color32(255, 255, 255, 255);

                //Util.log("Settings found for {0}, using them", headSet.name);              

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
                Util.log("Settings for {0} = {1}", defaultHead[0].name, defaultHead[0].eyeballColor_Left[0]);

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
            }

            if (node.TryGetNode("DEFAULT_FEMALE", ref defaultNode))
            {
                int nodeLvl = new int();
                bool nodeBool = false;
                Color32 nodeColor = new Color32();

                //Util.log("Settings found for {0}, using them", headSet.name);              

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
            }

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
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void readKerbalsConfigs()
        {
            List<string> excludedHeads = new List<string>();
            List<string> excludedSuits = new List<string>();           
           // var eyelessHeads = new List<string>();

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

                ConfigNode genericNode = file.config.GetNode("GenericKerbals");
                if (genericNode != null)
                {
                    Util.addLists(genericNode.GetValues("excludedHeads"), excludedHeads);
                    Util.addLists(genericNode.GetValues("excludedSuits"), excludedSuits);                   
                    //Util.addLists(genericNode.GetValues("eyelessHeads"), eyelessHeads);
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


            

            // Tag female and eye-less heads.
            /*foreach (Head_Set head in KerbalHeadsDB_full)
            {
                head.isEyeless = eyelessHeads.Contains(head.name);
            }*/
            
            // Create lists of male heads and suits.            
            KerbalSuitsDB_cleaned.AddRange(KerbalSuitsDB_full.Where(s => !excludedSuits.Contains(s.name)));

            // Create lists of female heads and suits.
            maleAndfemaleHeadsDB_cleaned[0].AddRange(KerbalHeadsDB_full.Where(h => !h.isFemale && !excludedHeads.Contains(h.name)));
            maleAndfemaleHeadsDB_cleaned[1].AddRange(KerbalHeadsDB_full.Where(h => h.isFemale && !excludedHeads.Contains(h.name)));
            
            // Trim lists.
            KerbalHeadsDB_full.TrimExcess();
            KerbalSuitsDB_full.TrimExcess();
            KerbalSuitsDB_cleaned.TrimExcess();
            maleAndfemaleHeadsDB_cleaned[0].TrimExcess();            
            maleAndfemaleHeadsDB_cleaned[1].TrimExcess();
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Read configuration and perform pre-load initialization.
        /// </summary>
        /// <param name="rootNode">The <see cref="ConfigNode"/> that contain our saved data</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void readConfig(ConfigNode rootNode)
        {
            Util.parse(rootNode.GetValue("isHelmetRemovalEnabled"), ref isHelmetRemovalEnabled);
            Util.parse(rootNode.GetValue("isAtmSuitEnabled"), ref isAtmSuitEnabled);
            Util.parse(rootNode.GetValue("atmSuitPressure"), ref atmSuitPressure);
            Util.addLists(rootNode.GetValues("atmSuitBodies"), atmSuitBodies);
            Util.parse(rootNode.GetValue("forceLegacyFemales"), ref forceLegacyFemales);
            //Util.parse(rootNode.GetValue("isNewSuitStateEnabled"), ref isNewSuitStateEnabled);
            //Util.parse(rootNode.GetValue("isAutomaticSuitSwitchEnabled"), ref isAutomaticSuitSwitchEnabled);
            Util.parse(rootNode.GetValue("useKspSkin"), ref useKspSkin);

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Post-load initialization.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void load()
        {           
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

            // Re-read scenario if database is reloaded during the space center scene to avoid losing all per-game settings.
            if (HighLogic.CurrentGame != null)
            {
                ConfigNode scenarioNode = HighLogic.CurrentGame.config.GetNodes("SCENARIO")
                  .FirstOrDefault(n => n.GetValue("name") == "TRR_Scenario");

                if (scenarioNode != null)
                    loadScenario(scenarioNode);
            }
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

            Util.parse(node.GetValue("isHelmetRemovalEnabled"), ref isHelmetRemovalEnabled);
            Util.parse(node.GetValue("isAtmSuitEnabled"), ref isAtmSuitEnabled);
            //Util.parse(node.GetValue("isNewSuitStateEnabled"), ref isNewSuitStateEnabled);
           // Util.parse(node.GetValue("isAutomaticSuitSwitchEnabled"), ref isAutomaticSuitSwitchEnabled);
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

            node.AddValue("isHelmetRemovalEnabled", isHelmetRemovalEnabled);
            node.AddValue("isAtmSuitEnabled", isAtmSuitEnabled);
           // node.AddValue("isNewSuitStateEnabled", isNewSuitStateEnabled);
           // node.AddValue("isAutomaticSuitSwitchEnabled", isAutomaticSuitSwitchEnabled);
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
            //loadSuitConfig(null, KerbalSuitsDB_full, defaultSuit, true);
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
                        //Util.log("Settings found for {0}, using them", suitSet.name);
                        bool nodebool = true;
                        Color32 nodeColor = new Color32(255, 255, 255, 255);
                        int nodeInt = 0;

                        // suit settings
                        if (savedNode.TryGetValue("isExclusive", ref nodebool))
                            suitSet.isExclusive = nodebool;
                        else
                            suitSet.isExclusive = defaultMap.isExclusive;

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
                        suitSet.isExclusive = defaultMap.isExclusive;

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

                        // visor reflection color settings
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