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
        public readonly Head_Set[] defaulMaleAndFemaleHeads = { new Head_Set { headSetName = "DEFAULT" }, new Head_Set { headSetName = "DEFAULT" } };

        /// <summary>
        /// Default suit textures (from `Default/`).
        /// </summary>
        public readonly Suit_Set defaultSuit = new Suit_Set { suitSetName = "DEFAULT" };
        
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
        public bool isNewSuitStateEnabled = false;

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

        // !!!!!!!!!!!!!!!!!!!!!!!!   Need to implement these options and make a GUI for them !!!!!!!!!!!!!!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!   Maybe a new class  ??                                    !!!!!!!!!!!!!!!
        /* =========================================================================================
         * personal suit options
         * used for each suit texture pack
         * =========================================================================================
         */

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
        public bool ForceIVAspaceSuit = false;          
                
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
            [KSPEvent(guiActive = true, guiName = "Toggle EVA Suit")]
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
           
            if (value == false) ScreenMessages.PostScreenMessage("Atmosphere non breathable !", 5.0f, ScreenMessageStyle.UPPER_CENTER);
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
        private Suit_Set getClassSuit(ProtoCrewMember kerbal)
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
                return kerbalData.head;            

            if (genderHeads.Count == 0)
            {               
                return defaulMaleAndFemaleHeads[(int)kerbal.gender];
            }
            
            // if the kerbal had no head set , choose one randomly.    
            kerbalData.head = random.randomize((int)kerbal.gender);

            string value = "";

            if (KerbalAndTheirHeadsDB.TryGetValue(kerbal.name, out value))
            {
                KerbalAndTheirHeadsDB[kerbal.name] = kerbalData.head.headSetName;
            }
            else
            {
                KerbalAndTheirHeadsDB.Add(kerbal.name, kerbalData.head.headSetName);
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
        private void personaliseKerbal(Component component, ProtoCrewMember protoKerbal, Part cabin, bool needsEVASuit, bool needsEVAgroundSuit)
        {
            Personaliser personaliser = Personaliser.instance;

            KerbalData kerbalData = getKerbalData(protoKerbal);
            
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

            personaliseKerbal_Head = personaliseKerbal_Head == defaulMaleAndFemaleHeads[(int)protoKerbal.gender] ? null : personaliseKerbal_Head;
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
                                newTexture = personaliseKerbal_Head.get_eyeball_LeftTexture(protoKerbal.experienceLevel);
                                newNormalMap = personaliseKerbal_Head.get_eyeball_LeftTextureNRM(protoKerbal.experienceLevel);
                            }
                            break;

                        case "eyeballRight":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballRight":
                            if (personaliseKerbal_Head != null)
                            {
                                newTexture = personaliseKerbal_Head.get_eyeball_RightTexture(protoKerbal.experienceLevel);
                                newNormalMap = personaliseKerbal_Head.get_eyeball_RightTextureNRM(protoKerbal.experienceLevel);
                            }
                            break;

                        case "pupilLeft":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilLeft":
                            Util.log("+++++++++++++++++++++++  pupilLeft of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            if (personaliseKerbal_Head != null)
                            {
                                smr.sharedMaterial.color = Color.white;
                                newTexture = personaliseKerbal_Head.get_pupil_LeftTexture(protoKerbal.experienceLevel);
                                newNormalMap = personaliseKerbal_Head.get_pupil_LeftTextureNRM(protoKerbal.experienceLevel);
                                if (newTexture != null)
                                {
                                    smr.sharedMaterial.color = Color.white;
                                }
                                else
                                {
                                    smr.sharedMaterial.color = Color.black;
                                }
                            }
                            break;

                        case "pupilRight":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilRight":
                            Util.log("+++++++++++++++++++++++  pupilRight of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            if (personaliseKerbal_Head != null)
                            {                               
                                newTexture = personaliseKerbal_Head.get_pupil_RightTexture(protoKerbal.experienceLevel);
                                newNormalMap = personaliseKerbal_Head.get_pupil_RightTextureNRM(protoKerbal.experienceLevel);
                                if (newTexture != null)
                                {                                    
                                    smr.sharedMaterial.color = Color.white;
                                } else
                                {
                                    smr.sharedMaterial.color = Color.black;
                                }                                   
                            }
                            break;

                        case "headMesh01":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pCube1":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_polySurface51":
                        case "headMesh":
                        case "ponytail":
                            if (personaliseKerbal_Head != null)
                            {
                                newTexture = personaliseKerbal_Head.get_headTexture(protoKerbal.experienceLevel);
                                newNormalMap = personaliseKerbal_Head.get_headTextureNRM(protoKerbal.experienceLevel);                                
                            }
                            break;

                        case "tongue":
                            Util.log("+++++++++++++++++++++++  TONGUE of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;
                        case "upTeeth01":
                            Util.log("+++++++++++++++++++++++  upTeeth01 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;
                        case "upTeeth02":
                            Util.log("+++++++++++++++++++++++  upTeeth02 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth01":
                            Util.log("+++++++++++++++++++++++  mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth01 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth01":
                            Util.log("+++++++++++++++++++++++  mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth01 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth02":
                            Util.log("+++++++++++++++++++++++  mesh_female_kerbalAstronaut02_kerbalGirl_mesh_upTeeth01 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth02":
                            Util.log("+++++++++++++++++++++++  mesh_female_kerbalAstronaut02_kerbalGirl_mesh_downTeeth01 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;
                        case "downTeeth01":
                            Util.log("+++++++++++++++++++++++  downTeeth01 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;
                        case "downTeeth02":
                            Util.log("+++++++++++++++++++++++  downTeeth02 of {0} ++++++++++++++++++++++++", protoKerbal.name);
                            break;



                        case "body01":
                        case "mesh_female_kerbalAstronaut01_body01":  
                            if (personaliseKerbal_Suit != null)
                            {   
                                if (isEva)
                                {
                                    if (needsEVASuit && needsEVAgroundSuit)
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaGround_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaGround_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaGround_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaGround_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaGround_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaGround_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaGround_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaGround_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaGround_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaGround_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaGround_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaGround_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaGround_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaGround_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaGround_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaGround_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        
                                    }
                                    else if (needsEVASuit)
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaSpace_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaSpace_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaSpace_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaSpace_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaSpace_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaSpace_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaSpace_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaSpace_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaSpace_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaSpace_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaSpace_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaSpace_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaSpace_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaSpace_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_EvaSpace_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_EvaSpace_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_Iva_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_Iva_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_Iva_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_Iva_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_Iva_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_Iva_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_Iva_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_Iva_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_Iva_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_suit_Iva_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (isBadass && isVeteran)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_suit_Iva_VetBad_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_suit_Iva_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_suit_Iva_VetBad_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_suit_Iva_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else if (isVeteran)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_suit_Iva_Veteran_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_suit_Iva_Veteran_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else if (isBadass)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_suit_Iva_Badass_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Badass_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_suit_Iva_Badass_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_suit_Iva_Standard_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Standard_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_suit_Iva_Standard_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_suit_Iva_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;

                        case "helmet":
                        case "mesh_female_kerbalAstronaut01_helmet":
                            if (isEva)
                                smr.enabled = needsEVASuit;
                            else
                                smr.sharedMesh = needsEVASuit ? helmetMesh[(int)protoKerbal.gender] : null;

                            // Textures have to be replaced even when hidden since it may become visible later on situation change.
                            if (personaliseKerbal_Suit != null)
                            {
                                if (isEva)
                                {
                                    if (needsEVASuit && needsEVAgroundSuit)
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaGround_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaGround_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaGround_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaGround_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaGround_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaGround_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaGround_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaGround_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaGround_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaGround_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaGround_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaGround_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaGround_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaGround_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaGround_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaGround_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }

                                    }
                                    else if (needsEVASuit)
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaSpace_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaSpace_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaSpace_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaSpace_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaSpace_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaSpace_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaSpace_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaSpace_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaSpace_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaSpace_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaSpace_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaSpace_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaSpace_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaSpace_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_EvaSpace_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_EvaSpace_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_Iva_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_Iva_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_Iva_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_Iva_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_Iva_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_Iva_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_Iva_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_helmet_Iva_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (isBadass && isVeteran)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_helmet_Iva_VetBad_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_helmet_Iva_VetBad_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else if (isVeteran)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_helmet_Iva_Veteran_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_helmet_Iva_Veteran_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else if (isBadass)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_helmet_Iva_Badass_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Badass_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_helmet_Iva_Badass_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_helmet_Iva_Standard_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Standard_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_helmet_Iva_Standard_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_helmet_Iva_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;

                        case "visor":
                        case "mesh_female_kerbalAstronaut01_visor": 

                            if (isEva)
                                smr.enabled = needsEVASuit;
                            else
                                smr.sharedMesh = needsEVASuit ? visorMesh[(int)protoKerbal.gender] : null;

                            // Textures have to be replaced even when hidden since it may become visible later on situation change.
                            if (personaliseKerbal_Suit != null)
                            {
                                if (isEva)
                                {
                                    if (needsEVASuit && needsEVAgroundSuit)
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaGround_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaGround_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaGround_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaGround_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaGround_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaGround_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaGround_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaGround_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaGround_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaGround_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaGround_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaGround_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaGround_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaGround_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaGround_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaGround_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }

                                    }
                                    else if (needsEVASuit)
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaSpace_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaSpace_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaSpace_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaSpace_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaSpace_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaSpace_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaSpace_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaSpace_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaSpace_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaSpace_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaSpace_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaSpace_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaSpace_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaSpace_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_EvaSpace_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_EvaSpace_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (isBadass && isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_Iva_VetBad_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_Iva_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_Iva_VetBad_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_Iva_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isVeteran)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_Iva_Veteran_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_Iva_Veteran_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else if (isBadass)
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_Iva_Badass_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_Iva_Badass_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (gender == 0)
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_Iva_Standard_Male(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                            else
                                            {
                                                newTexture = personaliseKerbal_Suit.get_visor_Iva_Standard_Female(protoKerbal.experienceLevel);
                                                newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (isBadass && isVeteran)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_visor_Iva_VetBad_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_visor_Iva_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_visor_Iva_VetBad_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_visor_Iva_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else if (isVeteran)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_visor_Iva_Veteran_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_visor_Iva_Veteran_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else if (isBadass)
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_visor_Iva_Badass_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Badass_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_visor_Iva_Badass_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (gender == 0)
                                        {
                                            newTexture = personaliseKerbal_Suit.get_visor_Iva_Standard_Male(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Standard_MaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                        else
                                        {
                                            newTexture = personaliseKerbal_Suit.get_visor_Iva_Standard_Female(protoKerbal.experienceLevel);
                                            newNormalMap = personaliseKerbal_Suit.get_visor_Iva_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                            break;
                                        }
                                    }
                                }
                            }

                            break;

                        case "jetpack":
                        case "mesh_female_kerbalAstronaut01_jetpack":
                            if (isEva)
                            {
                                smr.enabled = needsEVASuit;
                                if (personaliseKerbal_Suit != null)
                                {
                                    if (isEva)
                                    {
                                        if (needsEVASuit && needsEVAgroundSuit)
                                        {
                                            if (isBadass && isVeteran)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_VetBad_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_VetBad_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else if (isVeteran)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Veteran_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Veteran_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else if (isBadass)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Badass_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Badass_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Standard_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Standard_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }

                                        }
                                        else if (needsEVASuit)
                                        {
                                            if (isBadass && isVeteran)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_VetBad_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_VetBad_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else if (isVeteran)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Veteran_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Veteran_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else if (isBadass)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Badass_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Badass_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Standard_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Standard_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                        }
                                        
                                    }
                                   
                                }
                            }

                            break;
                        default: // Jetpack.
                            if (isEva)
                            {
                                smr.enabled = needsEVASuit;
                                if (personaliseKerbal_Suit != null)
                                {
                                    if (isEva)
                                    {
                                        if (needsEVASuit && needsEVAgroundSuit)
                                        {
                                            if (isBadass && isVeteran)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_VetBad_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_VetBad_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else if (isVeteran)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Veteran_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Veteran_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else if (isBadass)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Badass_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Badass_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Standard_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaGround_Standard_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaGround_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }

                                        }
                                        else if (needsEVASuit)
                                        {
                                            if (isBadass && isVeteran)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_VetBad_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_VetBad_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_VetBad_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_VetBad_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else if (isVeteran)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Veteran_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Veteran_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Veteran_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Veteran_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else if (isBadass)
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Badass_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Badass_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Badass_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Badass_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (gender == 0)
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Standard_Male(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Standard_MaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                                else
                                                {
                                                    newTexture = personaliseKerbal_Suit.get_jetpack_EvaSpace_Standard_Female(protoKerbal.experienceLevel);
                                                    newNormalMap = personaliseKerbal_Suit.get_jetpack_EvaSpace_Standard_FemaleNRM(protoKerbal.experienceLevel);
                                                    break;
                                                }
                                            }
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
           
            personaliseKerbal(kerbal, kerbal.protoCrewMember, kerbal.InPart, needsSuit, false);
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
                personaliseKerbal(evaPart, crew[0], null, evaSuit, evaGroundSuit);
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

            /*for (int i = 0; i < 2; i++)
            {
                maleAndfemaleHeadNumberOfUSe[i] = maleAndfemaleHeadsDB_cleaned[i].ToDictionary(k => k.headSetName, v => 0);
            }*/

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
                            if (headName == "DEFAULT")
                            {
                                kerbalData.head = defaulMaleAndFemaleHeads[(int)ProtoKerbal.gender];                                
                            }
                            else
                            {                                
                                bool headIsInTheDB = KerbalHeadsDB_full.Exists(h => h.headSetName == headName);

                                if (headIsInTheDB)
                                {
                                    kerbalData.head = KerbalHeadsDB_full.Find(h => h.headSetName == headName);
                                }
                                else
                                {
                                    kerbalData.head = getKerbalHead(ProtoKerbal, kerbalData);
                                    headName = kerbalData.head.headSetName;
                                }
                            }
                        }
                        else
                        {
                            kerbalData.head = getKerbalHead(ProtoKerbal, kerbalData);
                            headName = kerbalData.head.headSetName;
                        }
                    }
                    else
                    {
                        kerbalData.head = getKerbalHead(ProtoKerbal, kerbalData);
                        headName = kerbalData.head.headSetName;
                    }
                    //Util.log("pouet ici !!!!!!!!!!!!!!!!!");
                    //Util.log(" pouet name : {0} : {1}", ProtoKerbal.name, headName);
                    if (!KerbalAndTheirHeadsDB.ContainsKey(ProtoKerbal.name))
                        KerbalAndTheirHeadsDB.Add(ProtoKerbal.name, headName);
                    else
                    {
                        KerbalAndTheirHeadsDB.Remove(ProtoKerbal.name);
                        KerbalAndTheirHeadsDB.Add(ProtoKerbal.name, headName);
                    }
                        
                   // Util.log("pouet réussi !!!!!!!!!!!!!!!!!");
                    /* for (int i = 0; i < 2; i++)
                     {
                         List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(maleAndfemaleHeadNumberOfUSe[i]);

                         foreach (KeyValuePair<string, int> kvp in list)
                         {
                             int count = KerbalAndTheirHeadsDB.Count(k => k.Value.Contains(kvp.Key));
                             maleAndfemaleHeadNumberOfUSe[i][kvp.Key] = count;                            
                         }
                     }*/


                    if (suitName != null)
                    {
                        if (suitName != "GENERIC")
                        {
                            if (suitName == "DEFAULT")
                            {
                                kerbalData.suit = defaultSuit;
                            }
                            else
                            {
                                bool suitIsInTheDB = KerbalSuitsDB_full.Exists(s => s.suitSetName == suitName);

                                if (suitIsInTheDB)
                                {
                                    kerbalData.suit = KerbalSuitsDB_full.Find(s => s.suitSetName == suitName);
                                }
                                else
                                {
                                    kerbalData.suit = getKerbalSuit(ProtoKerbal, kerbalData);
                                    suitName = kerbalData.suit.suitSetName;
                                }
                            }
                        }
                        else
                        {
                            kerbalData.suit = getKerbalSuit(ProtoKerbal, kerbalData);
                            suitName = kerbalData.suit.suitSetName;
                        }
                    }
                    else
                    {
                        kerbalData.suit = getKerbalSuit(ProtoKerbal, kerbalData);
                        suitName = kerbalData.suit.suitSetName;
                    }
                    
                   // if (suitName != null && suitName != "GENERIC")
                     //   kerbalData.suit = suitName == "DEFAULT" ? defaultSuit : KerbalSuitsDB_full.Find(s => s.suitSetName == suitName);

                    ProtoKerbal.gender = forceLegacyFemales ? ProtoCrewMember.Gender.Male : (ProtoCrewMember.Gender)kerbalData.gender;

                    

                }
            }
           /* Util.log("00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            foreach (KeyValuePair<string, string> data in KerbalAndTheirHeadsDB)
            {
                Util.log(" {0} use this head set : {1}", data.Key, data.Value);
            }

            for (int i =0; i < 2; i++)
            {                
                foreach (KeyValuePair<string, int> data in maleAndfemaleHeadNumberOfUSe[i])
                {
                    Util.log("The head_set : {0} is used {1} times", data.Key, data.Value);
                }
            }*/
            

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

                string genderName = kerbalData.gender == 0 ? "M" : "F";                
                string headName = kerbalData.head == null ? "GENERIC" : kerbalData.head.headSetName;
                string suitName = kerbalData.suit == null ? "GENERIC" : kerbalData.suit.suitSetName;
               
                node.AddValue(kerbal.name, genderName + " " + headName + " " + suitName);
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
                        if (suitName == "DEFAULT")
                        {
                            map[entry.name] = defaultSuit;
                        }
                        else
                        {
                            Suit_Set suit = KerbalSuitsDB_full.Find(s => s.suitSetName == suitName);
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
                string suitName = entry.Value == null ? "GENERIC" : entry.Value.suitSetName;

                node.AddValue(entry.Key, suitName);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Fill config for custom Kerbal heads and suits.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void readKerbalsConfigs()
        {
            var excludedHeads = new List<string>();
            var excludedSuits = new List<string>();
           // var femaleSuits = new List<string>();
            var eyelessHeads = new List<string>();

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
                   // Util.addLists(genericNode.GetValues("femaleSuits"), femaleSuits);
                    Util.addLists(genericNode.GetValues("eyelessHeads"), eyelessHeads);
                }

                ConfigNode classNode = file.config.GetNode("ClassSuits");
                if (classNode != null)
                    loadSuitMap(classNode, defaultClassSuits);

                //ConfigNode cabinNode = file.config.GetNode("CabinSuits");
                //if (cabinNode != null)
                   // loadSuitMap(cabinNode, cabinSuits);
            }

            // Tag female and eye-less heads.
            foreach (Head_Set head in KerbalHeadsDB_full)
            {
                head.isEyeless = eyelessHeads.Contains(head.headSetName);
            }
            // Tag female suits.
           // foreach (Suit_Set suit in KerbalSuitsDB_full)
               // suit.isFemale = femaleSuits.Contains(suit.suitSetName);

            // Create lists of male heads and suits.
            //maleAndfemaleHeadsDB_cleaned[0].AddRange(maleAndfemaleHeadsDB_full.Where(h.isMale && !excludedHeads.Contains(h.headSetName)));
            //maleAndfemaleSuitsDB_cleaned[0].AddRange(KerbalSuitsDB_full.Where(s => !s.isFemale && !excludedSuits.Contains(s.suitSetName)));
            KerbalSuitsDB_cleaned.AddRange(KerbalSuitsDB_full.Where(s => !excludedSuits.Contains(s.suitSetName)));

            // Create lists of female heads and suits.
            maleAndfemaleHeadsDB_cleaned[0].AddRange(KerbalHeadsDB_full.Where(h => !h.isFemale && !excludedHeads.Contains(h.headSetName)));
            maleAndfemaleHeadsDB_cleaned[1].AddRange(KerbalHeadsDB_full.Where(h => h.isFemale && !excludedHeads.Contains(h.headSetName)));
            //maleAndfemaleSuitsDB_cleaned[1].AddRange(KerbalSuitsDB_full.Where(s => s.isFemale && !excludedSuits.Contains(s.suitSetName)));


           /* Util.log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");            
            Util.log("maleAndfemaleHeadsDB_cleaned[0] count = : {0}", maleAndfemaleHeadsDB_cleaned[0].Count);
            Util.log("maleAndfemaleHeadsDB_cleaned[1] count = : {0}", maleAndfemaleHeadsDB_cleaned[1].Count);*/


            // Trim lists.
            KerbalHeadsDB_full.TrimExcess();
            KerbalSuitsDB_full.TrimExcess();
            KerbalSuitsDB_cleaned.TrimExcess();
            maleAndfemaleHeadsDB_cleaned[0].TrimExcess();
            //maleAndfemaleSuitsDB_cleaned[0].TrimExcess();
            maleAndfemaleHeadsDB_cleaned[1].TrimExcess();
            //maleAndfemaleSuitsDB_cleaned[1].TrimExcess();
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
            Util.parse(rootNode.GetValue("isNewSuitStateEnabled"), ref isNewSuitStateEnabled);
            Util.parse(rootNode.GetValue("isAutomaticSuitSwitchEnabled"), ref isAutomaticSuitSwitchEnabled);
            

            
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Post-load initialization.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void load()
        {

            //var suitDirs = new Dictionary<string, int>();
            //string lastTextureName = "";
           
            // Populate KerbalHeadsDB_full and defaulMaleAndFemaleHeads
            Textures_Loader.LoadHeads(KerbalHeadsDB_full, maleAndfemaleHeadsDB_full, defaulMaleAndFemaleHeads);
            //Textures_Loader.DefaultHeads(defaulMaleAndFemaleHeads);
            Textures_Loader.LoadSuits(KerbalSuitsDB_full, defaultSuit);

            /*Util.log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");            
            Util.log("maleAndfemaleHeadsDB_full[0] count = : {0}", maleAndfemaleHeadsDB_full[0].Count);
            Util.log("maleAndfemaleHeadsDB_full[1] count = : {0}", maleAndfemaleHeadsDB_full[1].Count);*/

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
                  .FirstOrDefault(n => n.GetValue("name") == "TRScenario");

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

            Util.parse(node.GetValue("isHelmetRemovalEnabled"), ref isHelmetRemovalEnabled);
            Util.parse(node.GetValue("isAtmSuitEnabled"), ref isAtmSuitEnabled);
            Util.parse(node.GetValue("isNewSuitStateEnabled"), ref isNewSuitStateEnabled);
            Util.parse(node.GetValue("isAutomaticSuitSwitchEnabled"), ref isAutomaticSuitSwitchEnabled);
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

            node.AddValue("isHelmetRemovalEnabled", isHelmetRemovalEnabled);
            node.AddValue("isAtmSuitEnabled", isAtmSuitEnabled);
            node.AddValue("isNewSuitStateEnabled", isNewSuitStateEnabled);
            node.AddValue("isAutomaticSuitSwitchEnabled", isAutomaticSuitSwitchEnabled);
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
    }
}