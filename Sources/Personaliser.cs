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
        public readonly Head_Set[] defaulMaleAndFemaleHeads = { new Head_Set { headName = "DEFAULT" }, new Head_Set { headName = "DEFAULT" } };

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
        private readonly List<Head_Set>[] maleAndfemaleHeadsDB_cleaned = { new List<Head_Set>(), new List<Head_Set>() };

        /// <summary>
        /// Male and female suits textures (minus excluded).  
        /// </summary>
        private readonly List<Suit_Set>[] maleAndfemaleSuitsDB_cleaned = { new List<Suit_Set>(), new List<Suit_Set>() };

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
        private readonly Dictionary<string, Suit_Set> cabinSuits = new Dictionary<string, Suit_Set>();

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
        /// Contain the configuration data and textures of the head set
        /// <para>Here you will find all the textures for a head set and their functions </para>
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public class Head_Set
        {
            /// <summary>
            /// the name of the head texture
            /// </summary>
            public string headName;

            /// <summary>
            /// Is the head texture for a female model ?
            /// </summary>
            public bool isFemale;

            /// <summary>
            /// Is the head texture made to be used without the eyes 3D meshes ?
            /// </summary>
            public bool isEyeless;

            /// <summary>
            /// The head texture itself
            /// </summary>
            public Texture2D headTexture;

            /// <summary>
            /// The head normal map
            /// </summary>
            public Texture2D headNRM;
        }

        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Contain the configuration data and textures of the suit set
        /// <para>Here you will find all the textures for a suit set and their functions </para>
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public class Suit_Set
        {
            /// <summary>
            /// The name of the suit set
            /// </summary>
            public string suitSetName;

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  This will disappear !!!!!!!!!!!!!!!!!!!!!!!
            /// <summary>
            /// Is the suit set made for female kerbal?
            /// </summary>
            public bool isFemale;

            /* =====================================================================================
             * Level 0 textures (default textures)
             * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! all the Texture2D need to be converted to Texture2D[] !!!!!!!!!!!!!!!!!!!!!!!!!!!!
             * =====================================================================================
             */
                        
            /// <summary>
            /// Texture for the Male IVA suit
            /// </summary>
            public Texture2D Suit_Iva_Standard_Male0;

            /// <summary>
            /// Normal map for the Male IVA suit
            /// </summary>
            public Texture2D Suit_Iva_Standard_MaleNRM0;

            /// <summary>
            /// Texture for the veteran Male IVA suit
            /// </summary>
            public Texture2D Suit_Iva_Veteran_Male0;

            /// <summary>
            /// Normal map for the veteran Male IVA suit
            /// </summary>
            public Texture2D Suit_Iva_Veteran_MaleNRM0;            

            /// <summary>
            /// Texture for the Badass Male IVA suit
            /// </summary>
            public Texture2D Suit_Iva_Badass_Male0;

            /// <summary>
            /// Normal map for the Badass Male IVA suit
            /// </summary>
            public Texture2D Suit_Iva_Badass_MaleNRM0;

            /// <summary>
            /// Texture for the Veteran + Badass Male IVA suit
            /// </summary>
            public Texture2D Suit_Iva_VetBad_Male0;

            /// <summary>
            /// Normal map for the Veteran + Badass Male IVA suit
            /// </summary>
            public Texture2D Suit_Iva_VetBad_MaleNRM0;

            /// <summary>
            /// Texture for the Female IVA suit
            /// </summary>
            public Texture2D ivaSuit_Female;            

            /// <summary>
            /// Normal map for the Female IVA suit
            /// </summary>
            public Texture2D ivaSuit_Female_NRM;

            /// <summary>
            /// Texture for the veteran Female IVA suit
            /// </summary>
            public Texture2D ivaSuit_Veteran_Female;

            /// <summary>
            /// Normal map for the veteran Female IVA suit
            /// </summary>
            public Texture2D ivaSuit_Veteran_Female_NRM;

            /// <summary>
            /// Texture for the Badass Female IVA suit
            /// </summary>
            public Texture2D ivaSuit_Badass_Female;

            /// <summary>
            /// Normal map for the Badass Female IVA suit
            /// </summary>
            public Texture2D ivaSuit_Badass_Female_NRM;

            /// <summary>
            /// Texture for the Veteran + Badass Female IVA suit
            /// </summary>
            public Texture2D ivaSuit_VetBad_Female;

            /// <summary>
            /// Normal map for the Veteran + Badass Female IVA suit
            /// </summary>
            public Texture2D ivaSuit_VetBad_Female_NRM;

            /// <summary>
            /// Texture for the IVA helmet
            /// </summary>
            public Texture2D ivaHelmet;

            /// <summary>
            /// Normal map for the IVA helmet
            /// </summary>
            public Texture2D ivaHelmetNRM;

            /// <summary>
            /// Texture for the IVA visor
            /// </summary>
            public Texture2D ivaVisor;

            /// <summary>
            /// Normal map for the IVA visor 
            /// </summary>
            public Texture2D ivaVisorNRM;

            /// <summary>
            /// Texture for the Male EVA ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Male;

            /// <summary>
            /// Normal map for the Male EVA ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Male_NRM;

            /// <summary>
            /// Texture for the veteran Male EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Veteran_Male;

            /// <summary>
            /// Normal map for the veteran Male EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Veteran_Male_NRM;

            /// <summary>
            /// Texture for the Badass Male EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Badass_Male;

            /// <summary>
            /// Normal map for the Badass Male EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Badass_Male_NRM;

            /// <summary>
            /// Texture for the Veteran + Badass Male EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_VetBad_Male;

            /// <summary>
            /// Normal map for the Veteran + Badass Male EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_VetBad_Male_NRM;

            /// <summary>
            /// Texture for the Female EVA ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Female;

            /// <summary>
            /// Normal map for the Female EVA ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Female_NRM;

            /// <summary>
            /// Texture for the veteran Female EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Veteran_Female;

            /// <summary>
            /// Normal map for the veteran Female EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Veteran_Female_NRM;

            /// <summary>
            /// Texture for the Badass Female EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Badass_Female;

            /// <summary>
            /// Normal map for the Badass Female EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_Badass_Female_NRM;

            /// <summary>
            /// Texture for the Veteran + Badass Female EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_VetBad_Female;

            /// <summary>
            /// Normal map for the Veteran + Badass Female EVA Ground suit
            /// </summary>
            public Texture2D evaGroundSuit_VetBad_Female_NRM;

            /// <summary>
            /// Texture for the EVA ground helmet
            /// </summary>
            public Texture2D evaGroundHelmet;

            /// <summary>
            /// normal map for the EVA ground helmet
            /// </summary>
            public Texture2D evaGroundHelmetNRM;

            /// <summary>
            /// Texture for the EVA ground visor
            /// </summary>
            public Texture2D evaGroundVisor;

            /// <summary>
            /// Normal map for the EVA ground visor 
            /// </summary>
            public Texture2D evaGroundVisorNRM;

            /// <summary>
            /// Texture for the EVA ground jetpack
            /// </summary>
            public Texture2D evaGroundJetpack;

            /// <summary>
            /// normal map for the EVA ground jetpack
            /// </summary>
            public Texture2D evaGroundJetpackNRM;

            /// <summary>
            /// Texture for the Male EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Male;            

            /// <summary>
            /// Normal map for the Male EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Male_NRM;

            /// <summary>
            /// Texture for the veteran Male EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Veteran_Male;

            /// <summary>
            /// Normal map for the veteran Male EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Veteran_Male_NRM;

            /// <summary>
            /// Texture for the Badass Male EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Badass_Male;

            /// <summary>
            /// Normal map for the Badass Male EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Badass_Male_NRM;

            /// <summary>
            /// Texture for the Veteran + Badass Male EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_VetBad_Male;

            /// <summary>
            /// Normal map for the Veteran + Badass Male EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_VetBad_Male_NRM;

            /// <summary>
            /// Texture for the Female EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_female;

            /// <summary>
            /// Normal map for the Female EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Female_NRM;

            /// <summary>
            /// Texture for the veteran Female EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Veteran_Female;

            /// <summary>
            /// Normal map for the veteran Female EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Veteran_Female_NRM;

            /// <summary>
            /// Texture for the Badass Female EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Badass_Female;

            /// <summary>
            /// Normal map for the Badass Female EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_Badass_Female_NRM;

            /// <summary>
            /// Texture for the Veteran + Badass Female EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_VetBad_Female;

            /// <summary>
            /// Normal map for the Veteran + Badass Female EVA space suit
            /// </summary>
            public Texture2D evaSpaceSuit_VetBad_Female_NRM;

            /// <summary>
            /// Texture for the EVA space helmet
            /// </summary>
            public Texture2D evaSpaceHelmet;

            /// <summary>
            /// Normal map for the EVA space helmet 
            /// </summary>
            public Texture2D evaSpaceHelmetNRM;

            /// <summary>
            /// Texture for the EVA space visor
            /// </summary>
            public Texture2D evaSpaceVisor;

            /// <summary>
            /// Normal map for the EVA space visor 
            /// </summary>
            public Texture2D evaSpaceVisorNRM;

            /// <summary>
            /// Texture for the EVA space jetpack
            /// </summary>
            public Texture2D evaSpaceJetpack;

            /// <summary>
            /// Normal map for the EVA space jetpack
            /// </summary>
            public Texture2D evaSpaceJetpackNRM;            

            /* =====================================================================================
             * Level 1-5 textures (Level textures)
             * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!   these will fusion /disappear !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
             * =====================================================================================
             */

            /// <summary>
            /// The texture list for the leveled IVA suit
            /// </summary>
            private Texture2D[] levelIvaSuits;

            /// <summary>
            /// Normal map list for the leveled IVA suit
            /// </summary>
            private Texture2D[] levelIvaSuitsNRM;

            /// <summary>
            /// The texture list for the leveled IVA helmet
            /// </summary>
            private Texture2D[] levelIvaHelmets;

            /// <summary>
            /// Normal map list for the leveled IVA helmet
            /// </summary>
            private Texture2D[] levelIvaHelmetsNRM;

            /// <summary>
            /// The texture list for the leveled IVA visor
            /// </summary>
            private Texture2D[] levelIvaVisors;

            /// <summary>
            /// Normal map list for the leveled IVA visor
            /// </summary>
            private Texture2D[] levelIvaVisorsNRM;

            /// <summary>
            /// The texture list for the leveled EVA space suit
            /// </summary>
            private Texture2D[] levelEvaSpaceSuits;

            /// <summary>
            /// Normal map list for the leveled EVA space suit
            /// </summary>
            private Texture2D[] levelEvaSpaceSuitsNRM;

            /// <summary>
            /// The texture list for the leveled EVA space helmet
            /// </summary>
            private Texture2D[] levelEvaSpaceHelmets;

            /// <summary>
            /// Normal map list for the leveled EVA space helmet
            /// </summary>
            private Texture2D[] levelEvaSpaceHelmetsNRM;

            /// <summary>
            /// The texture list for the leveled EVA space visor
            /// </summary>
            private Texture2D[] levelEvaSpaceVisors;

            /// <summary>
            /// Normal map list for the leveled EVA space visor
            /// </summary>
            private Texture2D[] levelEvaSpaceVisorsNRM;

            /// <summary>
            /// The texture list for the leveled EVA space jetpack
            /// </summary>
            private Texture2D[] levelEvaSpaceJetpacks;

            /// <summary>
            /// Normal map list for the leveled EVA space jetpack
            /// </summary>
            private Texture2D[] levelEvaSpaceJetpacksNRM;

            /// <summary>
            /// The texture list for the leveled EVA ground suit
            /// </summary>
            private Texture2D[] levelEvaGroundSuits;

            /// <summary>
            /// Normal map list for the leveled EVA ground suit
            /// </summary>
            private Texture2D[] levelEvaGroundSuitsNRM;

            /// <summary>
            /// The texture list for the leveled EVA ground helmet
            /// </summary>
            private Texture2D[] levelEvaGroundHelmets;

            /// <summary>
            /// Normal map list for the leveled EVA Ground helmet
            /// </summary>
            private Texture2D[] levelEvaGroundHelmetsNRM;

            /// <summary>
            /// The texture list for the leveled EVA ground visor
            /// </summary>
            private Texture2D[] levelEvaGroundVisors;

            /// <summary>
            /// Normal map list for the leveled EVA ground visor
            /// </summary>
            private Texture2D[] levelEvaGroundVisorsNRM;

            /// <summary>
            /// The texture list for the leveled EVA ground jetpack
            /// </summary>
            private Texture2D[] levelEvaGroundJetpacks;

            /// <summary>
            /// Normal Map list for the leveled EVA ground jetpack
            /// </summary>
            private Texture2D[] levelEvaGroundJetpacksNRM;


            // !!!!!!!!!!!!!!!!!!!!!!!!     need revamp of these functions !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA suit for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA suit texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getIvaSuit(int level)
            {
                return level != 0 && levelIvaSuits != null ? levelIvaSuits[level - 1] : Suit_Iva_Standard_Male0;
                /* if (level != 0 && levelSuits != null)                 
                 *      return levelSuits[level-1];
                 * else
                 *      return suit;
                */
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA suit Normal Map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA suit texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getIvaSuitNRM(int level)
            {
                return level != 0 && levelIvaSuitsNRM != null ? levelIvaSuitsNRM[level - 1] : Suit_Iva_Standard_MaleNRM0;
                /* if (level != 0 && levelSuits != null)                 
                 *      return levelSuits[level-1];
                 * else
                 *      return suit;
                */
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA helmet for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA helmet texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getIvaHelmet(int level)
            {
                return level != 0 && levelIvaHelmets != null ? levelIvaHelmets[level - 1] : ivaHelmet;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA helmet Normal Map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA helmet texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getIvaHelmetNRM(int level)
            {
                return level != 0 && levelIvaHelmetsNRM != null ? levelIvaHelmetsNRM[level - 1] : ivaHelmetNRM;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA visor for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA visor texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getIvavisor(int level)
            {
                return level != 0 && levelIvaVisors != null ? levelIvaVisors[level - 1] : ivaVisor;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA visor normal map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA visor normal map for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getIvaVisorNRM (int level)
            {
                return level != 0 && levelIvaVisorsNRM != null ? levelIvaVisorsNRM[level - 1] : ivaVisorNRM;
            }


            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space suit for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space suit texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSpaceSuit(int level)
            {
                return level != 0 && levelEvaSpaceSuits != null ? levelEvaSpaceSuits[level - 1] : evaSpaceSuit_Male;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space suit Normal map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space suit texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSpaceSuitNRM(int level)
            {
                return level != 0 && levelEvaSpaceSuitsNRM != null ? levelEvaSpaceSuitsNRM[level - 1] : evaSpaceSuit_Male_NRM;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space helmet for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space helmet texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSpaceHelmet(int level)
            {
                return level != 0 && levelEvaSpaceHelmets != null ? levelEvaSpaceHelmets[level - 1] : evaSpaceHelmet;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space helmet normal map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space helmet texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSpaceHelmetNRM(int level)
            {
                return level != 0 && levelEvaSpaceHelmetsNRM != null ? levelEvaSpaceHelmetsNRM[level - 1] : evaSpaceHelmetNRM;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space visor for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space visor texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSpaceVisor(int level)
            {
                return level != 0 && levelEvaSpaceVisors != null ? levelEvaSpaceVisors[level - 1] : evaSpaceVisor;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space visor normal map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space visor normal map for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSpaceVisorNRM(int level)
            {
                return level != 0 && levelEvaSpaceVisorsNRM != null ? levelEvaSpaceVisorsNRM[level - 1] : evaSpaceVisorNRM;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space jetpack for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space jetpack texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSpaceJetpack(int level)
            {
                return level != 0 && levelEvaSpaceJetpacks != null ? levelEvaSpaceJetpacks[level - 1] : evaSpaceJetpack;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space jetpack normal map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space jetpack texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSpaceJetpackNRM(int level)
            {
                return level != 0 && levelEvaSpaceJetpacksNRM != null ? levelEvaSpaceJetpacksNRM[level - 1] : evaSpaceJetpackNRM;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA ground suit for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA ground texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaGroundSuit(int level)
            {
                return level != 0 && levelEvaGroundSuits != null ? levelEvaGroundSuits[level - 1] : evaGroundSuit_Male;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA ground suit Normal Map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA ground texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaGroundSuitNRM(int level)
            {
                return level != 0 && levelEvaGroundSuitsNRM != null ? levelEvaGroundSuitsNRM[level - 1] : evaGroundSuit_Male_NRM;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA ground helmet for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA ground helmet texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaGroundHelmet(int level)
            {
                return level != 0 && levelEvaGroundHelmets != null ? levelEvaGroundHelmets[level - 1] : evaGroundHelmet;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA ground helmet Normal Map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA ground helmet texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaGroundHelmetNRM(int level)
            {
                return level != 0 && levelEvaGroundHelmetsNRM != null ? levelEvaGroundHelmetsNRM[level - 1] : evaGroundHelmetNRM;
            }


            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA ground visor for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA ground visor texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaGroundVisor(int level)
            {
                return level != 0 && levelEvaGroundVisors != null ? levelEvaGroundVisors[level - 1] : evaGroundVisor;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA ground visor normal map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA ground visor normal map for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaGroundVisorNRM(int level)
            {
                return level != 0 && levelEvaGroundVisorsNRM != null ? levelEvaGroundVisorsNRM[level - 1] : evaGroundVisorNRM;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA ground jetpack for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA ground jetpack texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaGroundJetpack(int level)
            {
                return level != 0 && levelEvaGroundJetpacks != null ? levelEvaGroundJetpacks[level - 1] : evaGroundJetpack;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA ground jetpack Normal Map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA ground jetpack texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaGroundJetpackNRM(int level)
            {
                return level != 0 && levelEvaGroundJetpacksNRM != null ? levelEvaGroundJetpacksNRM[level - 1] : evaGroundJetpackNRM;
            }

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!    need revamp of the list with the new Texture2D[] and add the formatted new names for the texture like "ivaSuit_Male.dds" instead of "kerbalMainGrey.dds" !!!!!!!!!!!!!!!!!
            /// ************************************************************************************
            /// <summary>
            /// Search for the name of the texture, then set the good one in the suit set.
            /// <para>Related to <see cref="Personaliser.Suit"/> class. </para> 
            /// </summary>
            /// <param name="originalName">The name of the texture file (like KerbalMain.dds) 
            /// we want to save in the suit set.</param>
            /// <param name="texture">The texture we want to save in the suit set.</param>
            /// <returns>True if the texture is found and saved and false if not.</returns>
            /// ************************************************************************************
            public bool setTexture(string originalName, Texture2D texture)
            {
                int level;

                switch (originalName)
                {
                    /* ========================================
                    * IVA suit
                    * =======================================*/
                    case "kerbalMainGrey":
                    case "IvaSuit_Male":
                    case "Suit_IVA_Standard_Male":
                        Suit_Iva_Standard_Male0 = Suit_Iva_Standard_Male0 ?? texture;
                        return true;

                    case "kerbalMainNRM":
                    case "IvaSuit_MaleNRM":
                        Suit_Iva_Standard_MaleNRM0 = Suit_Iva_Standard_MaleNRM0 ?? texture;
                        return true;

                    case "kerbalMain":
                    case "ivaSuit_Veteran_Male":
                        Suit_Iva_Veteran_Male0 = Suit_Iva_Veteran_Male0 ?? texture;
                        return false;

                    case "ivaSuit_Veteran_Male_NRM":
                        Suit_Iva_Veteran_MaleNRM0 = Suit_Iva_Veteran_MaleNRM0 ?? texture;
                        return false;
                    
                    case "ivaSuit_Badass_Male":
                        Suit_Iva_Badass_Male0 = Suit_Iva_Badass_Male0 ?? texture;
                        return false;

                    case "ivaSuit_Badass_Male_NRM":
                        Suit_Iva_Badass_MaleNRM0 = Suit_Iva_Badass_MaleNRM0 ?? texture;
                        return false;

                    case "ivaSuit_Veteran_Female":
                        ivaSuit_Veteran_Female = ivaSuit_Veteran_Female ?? texture;
                        return false;

                    case "ivaSuit_Veteran_Female_NRM":
                        ivaSuit_Veteran_Female_NRM = ivaSuit_Veteran_Female_NRM ?? texture;
                        return false;

                    case "ivaSuit_Badass_Female":
                        ivaSuit_Badass_Female = ivaSuit_Badass_Female ?? texture;
                        return false;

                    case "ivaSuit_Badass_Female_NRM":
                        ivaSuit_Badass_Female_NRM = ivaSuit_Badass_Female_NRM ?? texture;
                        return false;

                    case "kerbalHelmetGrey":
                    case "IvaHelmet":
                        ivaHelmet = ivaHelmet ?? texture;
                        return true;

                    case "kerbalHelmetGreyNRM":
                    case "kerbalHelmetNRM":
                    case "IvaHelmet.NRM":
                        ivaHelmetNRM = ivaHelmetNRM ?? texture;
                        return true;

                    case "kerbalVisor":
                    case "IvaVisor":
                        ivaVisor = ivaVisor ?? texture;
                        return true;

                    case "kerbalVisorNRM":
                    case "IvaVisorNRM":
                        ivaVisorNRM = ivaVisorNRM ?? texture;
                        return true;
                    /* ========================================
                    * EVA ground suit
                    * =======================================*/
                    case "EvaGroundSuit_Male":
                    case "EVAgroundTexture":
                        evaGroundSuit_Male = evaGroundSuit_Male ?? texture;
                        return true;

                    case "EvaGroundSuit_Male_NRM":
                    case "EVAgroundTextureNRM":
                        evaGroundSuit_Male_NRM = evaGroundSuit_Male_NRM ?? texture;
                        return true;

                    case "EvaGroundSuit_Veteran_Male":
                        evaGroundSuit_Veteran_Male = evaGroundSuit_Veteran_Male ?? texture;
                        return false;

                    case "EvaGroundSuit_Veteran_Male_NRM":
                        evaGroundSuit_Veteran_Male_NRM = evaGroundSuit_Veteran_Male_NRM ?? texture;
                        return false;

                    case "EvaGroundSuit_Badass_Male":
                        evaGroundSuit_Badass_Male = evaGroundSuit_Badass_Male ?? texture;
                        return false;

                    case "EvaGroundSuit_Badass_Male_NRM":
                        evaGroundSuit_Badass_Male_NRM = evaGroundSuit_Badass_Male_NRM ?? texture;
                        return false;

                    case "EvaGroundSuit_Veteran_Female":
                        evaGroundSuit_Veteran_Female = evaGroundSuit_Veteran_Female ?? texture;
                        return false;

                    case "EvaGroundSuit_Veteran_Female_NRM":
                        evaGroundSuit_Veteran_Female_NRM = evaGroundSuit_Veteran_Female_NRM ?? texture;
                        return false;

                    case "EvaGroundSuit_Badass_Female":
                        evaGroundSuit_Badass_Female = evaGroundSuit_Badass_Female ?? texture;
                        return false;

                    case "EvaGroundSuit_Badass_Female_NRM":
                        evaGroundSuit_Badass_Female_NRM = evaGroundSuit_Badass_Female_NRM ?? texture;
                        return false;

                    case "EVAgroundHelmet":
                        evaGroundHelmet = evaGroundHelmet ?? texture;
                        return true;

                    case "EVAgroundHelmetNRM":
                        evaGroundHelmetNRM = evaGroundHelmetNRM ?? texture;
                        return true;

                    case "EVAgroundVisor":
                        evaGroundVisor = evaGroundVisor ?? texture;
                        return true;

                    case "EVAgroundVisorNRM":
                        evaGroundVisorNRM = evaGroundVisorNRM ?? texture;
                        return true;

                    case "EVAgroundJetpack":
                        evaGroundJetpack = evaGroundJetpack ?? texture;
                        return true;

                    case "EVAgroundJetpackNRM":
                        evaGroundJetpackNRM = evaGroundJetpackNRM ?? texture;
                        return true;

                    /* ========================================
                    * EVA space suit
                    * =======================================*/
                    case "EVAtexture":
                    case "EvaSpaceSuit_Male":
                        evaSpaceSuit_Male = evaSpaceSuit_Male ?? texture;
                        return true;

                    case "EVAtextureNRM":
                    case "EvaSpaceSuit_Male_NRM":
                        evaSpaceSuit_Male_NRM = evaSpaceSuit_Male_NRM ?? texture;
                        return true;

                    case "EvaSpaceSuit_Veteran_Male":
                        evaSpaceSuit_Veteran_Male = evaSpaceSuit_Veteran_Male ?? texture;
                        return false;

                    case "EvaSpaceSuit_Veteran_Male_NRM":
                        evaSpaceSuit_Veteran_Male_NRM = evaSpaceSuit_Veteran_Male_NRM ?? texture;
                        return false;

                    case "EvaSpaceSuit_Badass_Male":
                        evaSpaceSuit_Badass_Male = evaSpaceSuit_Badass_Male ?? texture;
                        return false;

                    case "EvaSpaceSuit_Badass_Male_NRM":
                        evaSpaceSuit_Badass_Male_NRM = evaSpaceSuit_Badass_Male_NRM ?? texture;
                        return false;

                    case "EvaSpaceSuit_Veteran_Female":
                        evaSpaceSuit_Veteran_Female = evaSpaceSuit_Veteran_Female ?? texture;
                        return false;

                    case "EvaSpaceSuit_Veteran_Female_NRM":
                        evaSpaceSuit_Veteran_Female_NRM = evaSpaceSuit_Veteran_Female_NRM ?? texture;
                        return false;

                    case "EvaSpaceSuit_Badass_Female":
                        evaSpaceSuit_Badass_Female = evaSpaceSuit_Badass_Female ?? texture;
                        return false;

                    case "EvaSpaceSuit_Badass_Female_NRM":
                        evaSpaceSuit_Badass_Female_NRM = evaSpaceSuit_Badass_Female_NRM ?? texture;
                        return false;

                    case "EVAhelmet":
                    case "EvaSpaceHelmet":
                        evaSpaceHelmet = evaSpaceHelmet ?? texture;
                        return true;

                    case "EVAhelmetNRM":
                    case "EvaSpaceHelmetNRM":
                        evaSpaceHelmetNRM = evaSpaceHelmetNRM ?? texture;
                        return true;

                    case "EVAvisor":
                    case "EvaSpaceVisor":
                        evaSpaceVisor = evaSpaceVisor ?? texture;
                        return true;

                    case "EVAvisorNRM":
                    case "EvaSpaceVisorNRM":
                        evaSpaceVisorNRM = evaSpaceVisorNRM ?? texture;
                        return true;

                    case "EVAjetpack":
                    case "EvaSpaceJetpack":
                        evaSpaceJetpack = evaSpaceJetpack ?? texture;
                        return true;

                    case "EVAjetpackNRM":
                    case "EvaSpaceJetpackNRM":
                        evaSpaceJetpackNRM = evaSpaceJetpackNRM ?? texture;
                        return true;
                     // !!!!!!!!!!!!!!!!!!!!!!!!   need to fusion/disappear !!!!!!!!!!!!!!!!
                    /* ========================================
                    * LEVEL suit
                    * =======================================*/
                    case "kerbalMainGrey1":
                    case "kerbalMainGrey2":
                    case "kerbalMainGrey3":
                    case "kerbalMainGrey4":
                    case "kerbalMainGrey5":
                        level = originalName.Last() - 0x30;
                        levelIvaSuits = levelIvaSuits ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelIvaSuits[i] = texture;
                        return true;

                    case "kerbalHelmetGrey1":
                    case "kerbalHelmetGrey2":
                    case "kerbalHelmetGrey3":
                    case "kerbalHelmetGrey4":
                    case "kerbalHelmetGrey5":
                        level = originalName.Last() - 0x30;
                        levelIvaHelmets = levelIvaHelmets ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelIvaHelmets[i] = texture;
                        return true;
                                            
                    case "kerbalVisor1":
                    case "kerbalVisor2":
                    case "kerbalVisor3":
                    case "kerbalVisor4":
                    case "kerbalVisor5":
                    case "IVAvisor1":
                    case "IVAvisor2":
                    case "IVAvisor3":
                    case "IVAvisor4":
                    case "IVAvisor5":
                        level = originalName.Last() - 0x30;
                        levelIvaVisors = levelIvaVisors ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelIvaVisors[i] = texture;
                        return true;

                    case "kerbalVisorNRM1":
                    case "kerbalVisorNRM2":
                    case "kerbalVisorNRM3":
                    case "kerbalVisorNRM4":
                    case "kerbalVisorNRM5":
                    case "IVAvisorNRM1":
                    case "IVAvisorNRM2":
                    case "IVAvisorNRM3":
                    case "IVAvisorNRM4":
                    case "IVAvisorNRM5":
                    
                        level = originalName.Last() - 0x30;
                        levelIvaVisorsNRM = levelIvaVisorsNRM ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelIvaVisorsNRM[i] = texture;
                        return true;

                    case "EVAtexture1":
                    case "EVAtexture2":
                    case "EVAtexture3":
                    case "EVAtexture4":
                    case "EVAtexture5":
                        level = originalName.Last() - 0x30;
                        levelEvaSpaceSuits = levelEvaSpaceSuits ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaSpaceSuits[i] = texture;
                        return true;

                    case "EVAhelmet1":
                    case "EVAhelmet2":
                    case "EVAhelmet3":
                    case "EVAhelmet4":
                    case "EVAhelmet5":
                        level = originalName.Last() - 0x30;
                        levelEvaSpaceHelmets = levelEvaSpaceHelmets ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaSpaceHelmets[i] = texture;
                        return true;

                    case "EVAvisor1":
                    case "EVAvisor2":
                    case "EVAvisor3":
                    case "EVAvisor4":
                    case "EVAvisor5":
                        level = originalName.Last() - 0x30;
                        levelEvaSpaceVisors = levelEvaSpaceVisors ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaSpaceVisors[i] = texture;
                        return true;

                    case "EVAvisorNRM1":
                    case "EVAvisorNRM2":
                    case "EVAvisorNRM3":
                    case "EVAvisorNRM4":
                    case "EVAvisorNRM5":
                    case "EVAspaceNRM1":
                    case "EVAspaceNRM2":
                    case "EVAspaceNRM3":
                    case "EVAspaceNRM4":
                    case "EVAspaceNRM5":
                        level = originalName.Last() - 0x30;
                        levelEvaSpaceVisorsNRM = levelEvaSpaceVisorsNRM ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaSpaceVisorsNRM[i] = texture;
                        return true;

                    case "EVAjetpack1":
                    case "EVAjetpack2":
                    case "EVAjetpack3":
                    case "EVAjetpack4":
                    case "EVAjetpack5":
                        level = originalName.Last() - 0x30;
                        levelEvaSpaceJetpacks = levelEvaSpaceJetpacks ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaSpaceJetpacks[i] = texture;
                        return true;
                    case "EVAgroundTexture1":
                    case "EVAgroundTexture2":
                    case "EVAgroundTexture3":
                    case "EVAgroundTexture4":
                    case "EVAgroundTexture5":
                        level = originalName.Last() - 0x30;
                        levelEvaGroundSuits = levelEvaGroundSuits ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaGroundSuits[i] = texture;
                        return true;

                    case "EVAgroundHelmet1":
                    case "EVAgroundHelmet2":
                    case "EVAgroundHelmet3":
                    case "EVAgroundHelmet4":
                    case "EVAgroundHelmet5":
                        level = originalName.Last() - 0x30;
                        levelEvaGroundHelmets = levelEvaGroundHelmets ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaGroundHelmets[i] = texture;
                        return true;

                    case "EVAgroundVisor1":
                    case "EVAgroundVisor2":
                    case "EVAgroundVisor3":
                    case "EVAgroundVisor4":
                    case "EVAgroundVisor5":
                        level = originalName.Last() - 0x30;
                        levelEvaGroundVisors = levelEvaGroundVisors ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaGroundVisors[i] = texture;
                        return true;

                    case "EVAgroundVisorNRM1":
                    case "EVAgroundVisorNRM2":
                    case "EVAgroundVisorNRM3":
                    case "EVAgroundVisorNRM4":
                    case "EVAgroundVisorNRM5":
                        level = originalName.Last() - 0x30;
                        levelEvaGroundVisorsNRM = levelEvaGroundVisorsNRM ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaGroundVisorsNRM[i] = texture;
                        return true;

                    case "EVAgroundJetpack1":
                    case "EVAgroundJetpack2":
                    case "EVAgroundJetpack3":
                    case "EVAgroundJetpack4":
                    case "EVAgroundJetpack5":
                        level = originalName.Last() - 0x30;
                        levelEvaGroundJetpacks = levelEvaGroundJetpacks ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaGroundJetpacks[i] = texture;
                        return true;

                    default:
                        return false;
                }
            }
        }

        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// The data of a kerbal
        /// <para>Contain the head and suit texture used by the kerbal</para>   
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public class KerbalData
        {
            /// <summary>
            /// The hash code of the kerbal
            /// </summary>
            public int hash;

            /// <summary>
            /// The gender of the kerbal
            /// </summary>
            public int gender;

            /// <summary>
            /// Is the kerbal a veteran?
            /// </summary>
            public bool isVeteran;

            /// <summary>
            /// The head of the kerbal
            /// </summary>
            public Head_Set head;

            /// <summary>
            /// The suit set of the kerbal
            /// </summary>
            public Suit_Set suit;

            /// <summary>
            /// The forced cabin suit (IVA) of the kerbal
            /// </summary>
            public Suit_Set cabinSuit;
        }

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
        /*
         * // Legacy suit state from TextureReplacer
                {
                    if (personaliser.personaliseEvaLegacy(part, !hasEvaSuit))
                    {
                        hasEvaSuit = !hasEvaSuit;

                        if (reflectionScript != null)
                            reflectionScript.setActive(hasEvaSuit);
                    }else
                    {
                        ScreenMessages.PostScreenMessage("No breathable atmosphere", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                    }*/
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
                
                /*if (personaliser.isAutomaticSuitSwitchEnabled)
                {
                    if (personaliser.isNewSuitStateEnabled)
                    {
                        if (personaliser.isUnderSubOrbit(GetComponent<Vessel>()))
                        {
                            if (personaliser.isAtmBreathable())
                            {
                                actualSuitState = 0; //IVA suit
                                hasEvaSuit = false;
                                hasEvaGroundSuit = false;
                            }
                            else
                            {
                                actualSuitState = 1; //EVAground suit
                                hasEvaSuit = true;
                                hasEvaGroundSuit = true;
                                if (reflectionScript != null)
                                    reflectionScript.setActive(true);
                            }
                        }
                        else
                        {
                            actualSuitState = 2; //EVA suit
                            hasEvaSuit = true;
                            hasEvaGroundSuit = false;
                            if (reflectionScript != null)
                                reflectionScript.setActive(true);
                        }
                    }
                    else
                    {
                        if (personaliser.isAtmBreathable())
                        {
                            actualSuitState = 0; //IVA suit
                            hasEvaSuit = false;
                            hasEvaGroundSuit = false;
                        }
                        else
                        {
                            actualSuitState = 2; //EVA suit
                            hasEvaSuit = true;
                            hasEvaGroundSuit = false;
                            if (reflectionScript != null)
                                reflectionScript.setActive(true);
                        }
                    }
                    
                }
                personaliser.personaliseEva(part, actualSuitState);*/

               
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


            /*
             * // legacy system from TR
                        if (!hasEvaSuit && !personaliser.isAtmBreathable())
                        {
                            personaliser.personaliseEvaLegacy(part, true);
                            hasEvaSuit = true;

                            if (reflectionScript != null)
                                reflectionScript.setActive(true);
                        }*/


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
        /// List of the different installDirectory
        /// </summary>        
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public readonly List<string> installDirectory = new List<string>();
        // public readonly string[] TRRinstallDirectory = new string[] { };
        /*
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// List of the Default folder
        /// </summary>
        /// <param name="install_dir"></param>        
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private static List<string> DIR_DEFAULT(List<string> install_dir)
        {
            List<string> default_dir = new List<string>() ;            
            foreach (string install_path in install_dir)
            {
                default_dir.Add(install_path + "Default/");
            }
            return default_dir;
        }                

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// List of the Heads folder
        /// </summary>
        /// <param name="install_dir"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private static List<string> DIR_HEADS(List<string> install_dir)
        {
            List<string> heads_dir = new List<string>();
            foreach (string install_path in install_dir)
            {
                heads_dir.Add(install_path + "Default/");
            }
            return heads_dir;
        }
             
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// List of the Suits folder
        /// </summary>
        /// <param name="install_dir"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private static List<string> DIR_SUITS(List<string> install_dir)
        {
            List<string> suits_dir = new List<string>();
            foreach (string install_path in install_dir)
            {
                suits_dir.Add(install_path + "Default/");
            }
            return suits_dir;
        }*/
       // private static readonly string DIR_DEFAULT = Util.DIR + "Default/";
       // private static readonly string DIR_HEADS = Util.DIR + "Heads/";
       // private static readonly string DIR_SUITS = Util.DIR + "Suits/";

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
                    isVeteran = kerbal.veteran
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
            if (kerbalData.head != null)
                return kerbalData.head;

            List<Head_Set> genderHeads = maleAndfemaleHeadsDB_cleaned[kerbalData.gender];
            if (genderHeads.Count == 0)
                return defaulMaleAndFemaleHeads[(int)kerbal.gender];

            // Hash is multiplied with a large prime to increase randomisation, since hashes returned by `GetHashCode()` are
            // close together if strings only differ in the last (few) char(s).
            int number = (kerbalData.hash * 4099) & 0x7fffffff;
            return genderHeads[number % genderHeads.Count];
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

            List<Suit_Set> genderSuits = maleAndfemaleSuitsDB_cleaned[0];

            // Use female suits only if available, fall back to male suits otherwise.
            if (kerbalData.gender != 0 && maleAndfemaleSuitsDB_cleaned[1].Count != 0)
                genderSuits = maleAndfemaleSuitsDB_cleaned[1];
            else if (genderSuits.Count == 0)
                return defaultSuit;

            // We must use a different prime here to increase randomization so that the same head is not always combined with
            // the same suit.
            int number = ((kerbalData.hash + kerbal.name.Length) * 2053) & 0x7fffffff;
            return genderSuits[number % genderSuits.Count];
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

            Head_Set personaliseKerbal_Head = getKerbalHead(protoKerbal, kerbalData);
            Suit_Set personaliseKerbal_Suit = null;

            if (isEva || !cabinSuits.TryGetValue(cabin.partInfo.name, out kerbalData.cabinSuit))
                personaliseKerbal_Suit = getKerbalSuit(protoKerbal, kerbalData);

            personaliseKerbal_Head = personaliseKerbal_Head == defaulMaleAndFemaleHeads[(int)protoKerbal.gender] ? null : personaliseKerbal_Head;
            personaliseKerbal_Suit = (isEva && needsEVASuit) || kerbalData.cabinSuit == null ? personaliseKerbal_Suit : kerbalData.cabinSuit;
            personaliseKerbal_Suit = personaliseKerbal_Suit == defaultSuit ? null : personaliseKerbal_Suit;

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
                        case "eyeballRight":
                        case "pupilLeft":
                        case "pupilRight":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballLeft":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballRight":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilLeft":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilRight":
                            if (personaliseKerbal_Head != null && personaliseKerbal_Head.isEyeless)
                                smr.sharedMesh = null;

                            break;

                        case "headMesh01":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pCube1":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_polySurface51":
                        case "headMesh":
                        case "ponytail":
                            if (personaliseKerbal_Head != null)
                            {
                                newTexture = personaliseKerbal_Head.headTexture;
                                newNormalMap = personaliseKerbal_Head.headNRM;
                            }
                            break;

                        case "tongue":
                        case "upTeeth01":
                        case "upTeeth02":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth01":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth01":
                        case "downTeeth01":
                            break;

                        case "body01":
                        case "mesh_female_kerbalAstronaut01_body01":
                            bool isEvaSuit = isEva && needsEVASuit;                            

                            if (personaliseKerbal_Suit != null)
                            {
                                // newTexture = isEvaSuit ? suit.getEvaSuit(kerbal.experienceLevel) : suit.getSuit(kerbal.experienceLevel);
                                // newNormalMap = isEvaSuit ? suit.evaSuitNRM : suit.suitNRM;

                                if (isEva)
                                {
                                    if (needsEVASuit && needsEVAgroundSuit)
                                    {
                                        newTexture = personaliseKerbal_Suit.getEvaGroundSuit(protoKerbal.experienceLevel);
                                        newNormalMap = personaliseKerbal_Suit.getEvaGroundSuitNRM(protoKerbal.experienceLevel);
                                    }
                                    else if (needsEVASuit)
                                    {
                                        newTexture = personaliseKerbal_Suit.getEvaSpaceSuit(protoKerbal.experienceLevel);
                                        newNormalMap = personaliseKerbal_Suit.getEvaSpaceSuitNRM(protoKerbal.experienceLevel);
                                    }
                                    else
                                    {
                                        newTexture = personaliseKerbal_Suit.getIvaSuit(protoKerbal.experienceLevel);
                                        newNormalMap = personaliseKerbal_Suit.getIvaSuitNRM(protoKerbal.experienceLevel);
                                    }
                                }
                                else
                                {
                                    newTexture = personaliseKerbal_Suit.getIvaSuit(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Suit.getIvaSuitNRM(protoKerbal.experienceLevel);
                                }
                            }
                            if (newTexture == null)
                            {
                                // This required for two reasons: to fix IVA suits after KSP resetting them to the stock ones all the
                                // time and to fix the switch from non-default to default texture during EVA suit toggle.
                                newTexture = isEvaSuit ? defaultSuit.evaSpaceSuit_Male
                                  : kerbalData.isVeteran ? defaultSuit.Suit_Iva_Veteran_Male0
                                  : defaultSuit.Suit_Iva_Standard_Male0;
                            }

                            if (newNormalMap == null)
                                newNormalMap = isEvaSuit ? defaultSuit.evaSpaceSuit_Male_NRM : defaultSuit.Suit_Iva_Standard_MaleNRM0;

                            // Update textures in Kerbal IVA object since KSP resets them to these values a few frames later.
                            if (!isEva)
                            {
                                Kerbal kerbalIVA = (Kerbal)component;

                                kerbalIVA.textureStandard = newTexture;
                                kerbalIVA.textureVeteran = newTexture;
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
                                if (isEva && needsEVAgroundSuit)
                                {
                                    newTexture = personaliseKerbal_Suit.getEvaGroundHelmet(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Suit.getEvaGroundHelmetNRM(protoKerbal.experienceLevel);
                                }
                                else if (isEva)
                                {
                                    newTexture = personaliseKerbal_Suit.getEvaSpaceHelmet(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Suit.getEvaSpaceHelmetNRM(protoKerbal.experienceLevel);
                                }
                                else
                                {
                                    newTexture = personaliseKerbal_Suit.getIvaHelmet(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Suit.getIvaHelmetNRM(protoKerbal.experienceLevel);
                                }
                            }
                            break;

                        case "visor":
                        case "mesh_female_kerbalAstronaut01_visor":
                          /*  if (isEva)
                                smr.enabled = needsEVASuit;
                            else
                                smr.sharedMesh = needsEVASuit ? visorMesh[(int)protoKerbal.gender] : null;

                            // Textures have to be replaced even when hidden since it may become visible later on situation change.
                            if (personaliseKerbal_Suit != null)
                            {
                                newTexture = isEva ? personaliseKerbal_Suit.getEvaSpaceVisor(protoKerbal.experienceLevel) : personaliseKerbal_Suit.getEvaSpaceVisor(protoKerbal.experienceLevel);

                                

                                if (newTexture != null)
                                    material.color = Color.white;
                            }*/
                            

                            if (isEva)
                                smr.enabled = needsEVASuit;
                            else
                                smr.sharedMesh = needsEVASuit ? visorMesh[(int)protoKerbal.gender] : null;

                            // Textures have to be replaced even when hidden since it may become visible later on situation change.
                            if (personaliseKerbal_Suit != null)
                            {
                                if (isEva && needsEVAgroundSuit)
                                {
                                    newTexture = personaliseKerbal_Suit.getEvaGroundVisor(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Suit.getEvaGroundVisorNRM(protoKerbal.experienceLevel);
                                }
                                else if (isEva)
                                {
                                    newTexture = personaliseKerbal_Suit.getEvaSpaceVisor(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Suit.getEvaSpaceVisorNRM(protoKerbal.experienceLevel);
                                }
                                else
                                {
                                    newTexture = personaliseKerbal_Suit.getIvavisor(protoKerbal.experienceLevel);
                                    newNormalMap = personaliseKerbal_Suit.getIvaVisorNRM(protoKerbal.experienceLevel);
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
                                    if (needsEVASuit && needsEVAgroundSuit)
                                    {
                                        newTexture = personaliseKerbal_Suit.getEvaGroundJetpack(protoKerbal.experienceLevel);
                                        newNormalMap = personaliseKerbal_Suit.getEvaGroundJetpackNRM(protoKerbal.experienceLevel);
                                    }
                                    else if (needsEVASuit)
                                    {
                                        newTexture = personaliseKerbal_Suit.getEvaSpaceJetpack(protoKerbal.experienceLevel);
                                        newNormalMap = personaliseKerbal_Suit.getEvaSpaceJetpackNRM(protoKerbal.experienceLevel);
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
                                    if (needsEVASuit && needsEVAgroundSuit)
                                    {
                                        newTexture = personaliseKerbal_Suit.getEvaGroundJetpack(protoKerbal.experienceLevel);
                                        newNormalMap = personaliseKerbal_Suit.getEvaGroundJetpackNRM(protoKerbal.experienceLevel);
                                    }
                                    else if (needsEVASuit)
                                    {
                                        newTexture = personaliseKerbal_Suit.getEvaSpaceJetpack(protoKerbal.experienceLevel);
                                        newNormalMap = personaliseKerbal_Suit.getEvaSpaceJetpackNRM(protoKerbal.experienceLevel);
                                    }
                                }
                                /*if (needsSuit && suit != null)
                                {
                                    newTexture = isEva ? suit.getEvaJetpack(kerbal.experienceLevel) : suit.getEvaJetpack(kerbal.experienceLevel);
                                    newNormalMap = suit.evaJetpackNRM;
                                }*/
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
        /// Legacy texture replacer
        /// </summary>
        /// <param name="component"></param>
        /// <param name="kerbal"></param>
        /// <param name="cabin"></param>
        /// <param name="needsSuit"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /*private void personaliseKerbalLegacy(Component component, ProtoCrewMember kerbal, Part cabin, bool needsSuit)
        {
            KerbalData kerbalData = getKerbalData(kerbal);
            bool isEva = cabin == null;

            Head head = getKerbalHead(kerbal, kerbalData);
            Suit suit = null;

            if (isEva || !cabinSuits.TryGetValue(cabin.partInfo.name, out kerbalData.cabinSuit))
                suit = getKerbalSuit(kerbal, kerbalData);

            head = head == defaulMaleAndFemaleHeads[(int)kerbal.gender] ? null : head;
            suit = (isEva && needsSuit) || kerbalData.cabinSuit == null ? suit : kerbalData.cabinSuit;
            suit = suit == defaultSuit ? null : suit;

            Transform model = isEva ? component.transform.Find("model01") : component.transform.Find("kbIVA@idle/model01");
            Transform flag = isEva ? component.transform.Find("model/kbEVA_flagDecals") : null;

            if (isEva)
                flag.GetComponent<Renderer>().enabled = needsSuit;

            // We must include hidden meshes, since flares are hidden when light is turned off.
            // All other meshes are always visible, so no performance hit here.
            foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>(true))
            {
                var smr = renderer as SkinnedMeshRenderer;

                // Thruster jets, flag decals and headlight flares.
                if (smr == null)
                {
                    if (renderer.name != "screenMessage")
                        renderer.enabled = needsSuit;
                }
                else
                {
                    Material material = renderer.material;
                    Texture2D newTexture = null;
                    Texture2D newNormalMap = null;

                    switch (smr.name)
                    {
                        case "eyeballLeft":
                        case "eyeballRight":
                        case "pupilLeft":
                        case "pupilRight":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballLeft":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_eyeballRight":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilLeft":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pupilRight":
                            if (head != null && head.isEyeless)
                                smr.sharedMesh = null;

                            break;

                        case "headMesh01":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_pCube1":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_polySurface51":
                        case "headMesh":
                        case "ponytail":
                            if (head != null)
                            {
                                newTexture = head.headTexture;
                                newNormalMap = head.headNRM;
                            }
                            break;

                        case "tongue":
                        case "upTeeth01":
                        case "upTeeth02":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_upTeeth01":
                        case "mesh_female_kerbalAstronaut01_kerbalGirl_mesh_downTeeth01":
                        case "downTeeth01":
                            break;

                        case "body01":
                        case "mesh_female_kerbalAstronaut01_body01":
                            bool isEvaSuit = isEva && needsSuit;

                            if (suit != null)
                            {
                                newTexture = isEvaSuit ? suit.getEvaSpaceSuit(kerbal.experienceLevel) : suit.getIvaSuit(kerbal.experienceLevel);
                                newNormalMap = isEvaSuit ? suit.getEvaSpaceSuitNRM(kerbal.experienceLevel) : suit.getIvaSuitNRM(kerbal.experienceLevel);
                            }

                            if (newTexture == null)
                            {
                                // This required for two reasons: to fix IVA suits after KSP resetting them to the stock ones all the
                                // time and to fix the switch from non-default to default texture during EVA suit toggle.
                                newTexture = isEvaSuit ? defaultSuit.evaSpaceSuit_Male
                                  : kerbalData.isVeteran ? defaultSuit.ivaSuit_Veteran_Male
                                  : defaultSuit.ivaSuit_Male;
                            }

                            if (newNormalMap == null)
                                newNormalMap = isEvaSuit ? defaultSuit.evaSpaceSuit_Male_NRM : defaultSuit.ivaSuit_Male_NRM;

                            // Update textures in Kerbal IVA object since KSP resets them to these values a few frames later.
                            if (!isEva)
                            {
                                Kerbal kerbalIVA = (Kerbal)component;

                                kerbalIVA.textureStandard = newTexture;
                                kerbalIVA.textureVeteran = newTexture;
                            }
                            break;

                        case "helmet":
                        case "mesh_female_kerbalAstronaut01_helmet":
                            if (isEva)
                                smr.enabled = needsSuit;
                            else
                                smr.sharedMesh = needsSuit ? helmetMesh[(int)kerbal.gender] : null;

                            // Textures have to be replaced even when hidden since it may become visible later on situation change.
                            if (suit != null)
                            {
                                newTexture = isEva ? suit.getEvaSpaceHelmet(kerbal.experienceLevel) : suit.getIvaHelmet(kerbal.experienceLevel);
                                newNormalMap = suit.ivaHelmetNRM;
                            }
                            break;

                        case "visor":
                        case "mesh_female_kerbalAstronaut01_visor":
                            if (isEva)
                                smr.enabled = needsSuit;
                            else
                                smr.sharedMesh = needsSuit ? visorMesh[(int)kerbal.gender] : null;

                            // Textures have to be replaced even when hidden since it may become visible later on situation change.
                            if (suit != null)
                            {
                                newTexture = isEva ? suit.evaSpaceVisor : suit.ivaVisor;

                                if (newTexture != null)
                                    material.color = Color.white;
                            }
                            break;

                        default: // Jetpack.
                            if (isEva)
                            {
                                smr.enabled = needsSuit;

                                if (needsSuit && suit != null)
                                {
                                    newTexture = suit.evaSpaceJetpack;
                                    newNormalMap = suit.evaSpaceJetpackNRM;
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
        }*/

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






            /*// new suit State for TRR
            if (personaliser.isNewSuitStateEnabled)
            {
                personaliseKerbal(kerbal, kerbal.protoCrewMember, kerbal.InPart, needsSuit, false);
            }
            else // Legacy suit state from TextureReplacer
            {
                personaliseKerbalLegacy(kerbal, kerbal.protoCrewMember, kerbal.InPart, needsSuit);
            }*/
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
                if (selection == 0) ScreenMessages.PostScreenMessage("IVA suit", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                else if (selection== 1) ScreenMessages.PostScreenMessage("EVA Ground suit", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                else if (selection== 2) ScreenMessages.PostScreenMessage("EVA Space suit", 2.0f, ScreenMessageStyle.UPPER_CENTER);
                personaliseKerbal(evaPart, crew[0], null, evaSuit, evaGroundSuit);
            }
            return selection;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Legacy suit state from TextureReplacer
        /// Set external EVA/IVA suit. Fails and return false if trying to remove EVA suit outside of breathable atmosphere.
        /// This function is used by EvaModule.
        /// </summary>
        /// <param name="evaPart"></param>
        /// <param name="evaSuit"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
       /* private bool personaliseEvaLegacy(Part evaPart, bool evaSuit)
        {
            bool success = true;

            List<ProtoCrewMember> crew = evaPart.protoModuleCrew;
            if (crew.Count != 0)
            {
                if (!evaSuit && !isAtmBreathable())
                {
                    evaSuit = true;
                    success = false;
                }

                personaliseKerbalLegacy(evaPart, crew[0], null, evaSuit);
            }
            return success;
        }*/

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

                    if (headName != null && headName != "GENERIC")
                    {
                        kerbalData.head = headName == "DEFAULT" ? defaulMaleAndFemaleHeads[(int)ProtoKerbal.gender]
                          : KerbalHeadsDB_full.Find(h => h.headName == headName);
                    }

                    if (suitName != null && suitName != "GENERIC")
                        kerbalData.suit = suitName == "DEFAULT" ? defaultSuit : KerbalSuitsDB_full.Find(s => s.suitSetName == suitName);

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

                string genderName = kerbalData.gender == 0 ? "M" : "F";
                string headName = kerbalData.head == null ? "GENERIC" : kerbalData.head.headName;
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
            var femaleSuits = new List<string>();
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
                    Util.addLists(genericNode.GetValues("femaleSuits"), femaleSuits);
                    Util.addLists(genericNode.GetValues("eyelessHeads"), eyelessHeads);
                }

                ConfigNode classNode = file.config.GetNode("ClassSuits");
                if (classNode != null)
                    loadSuitMap(classNode, defaultClassSuits);

                ConfigNode cabinNode = file.config.GetNode("CabinSuits");
                if (cabinNode != null)
                    loadSuitMap(cabinNode, cabinSuits);
            }

            // Tag female and eye-less heads.
            foreach (Head_Set head in KerbalHeadsDB_full)
            {
                head.isEyeless = eyelessHeads.Contains(head.headName);
            }
            // Tag female suits.
            foreach (Suit_Set suit in KerbalSuitsDB_full)
                suit.isFemale = femaleSuits.Contains(suit.suitSetName);

            // Create lists of male heads and suits.
            maleAndfemaleHeadsDB_cleaned[0].AddRange(KerbalHeadsDB_full.Where(h => !h.isFemale && !excludedHeads.Contains(h.headName)));
            maleAndfemaleSuitsDB_cleaned[0].AddRange(KerbalSuitsDB_full.Where(s => !s.isFemale && !excludedSuits.Contains(s.suitSetName)));

            // Create lists of female heads and suits.
            maleAndfemaleHeadsDB_cleaned[1].AddRange(KerbalHeadsDB_full.Where(h => h.isFemale && !excludedHeads.Contains(h.headName)));
            maleAndfemaleSuitsDB_cleaned[1].AddRange(KerbalSuitsDB_full.Where(s => s.isFemale && !excludedSuits.Contains(s.suitSetName)));

            // Trim lists.
            KerbalHeadsDB_full.TrimExcess();
            KerbalSuitsDB_full.TrimExcess();
            maleAndfemaleHeadsDB_cleaned[0].TrimExcess();
            maleAndfemaleSuitsDB_cleaned[0].TrimExcess();
            maleAndfemaleHeadsDB_cleaned[1].TrimExcess();
            maleAndfemaleSuitsDB_cleaned[1].TrimExcess();
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
            Util.addLists(rootNode.GetValues("installDirectory"), installDirectory);

            
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Post-load initialization.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void load()
        {
            
            var suitDirs = new Dictionary<string, int>();
            string lastTextureName = "";

            // Populate KerbalHeadsDB_full and defaulMaleAndFemaleHeads
            Textures.LoadHeads(KerbalHeadsDB_full, maleAndfemaleHeadsDB_cleaned);
            Textures.DefaultHeads(defaulMaleAndFemaleHeads);
            Textures.LoadSuits(KerbalSuitsDB_full, defaultSuit);

            /*foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture)
            {
                Texture2D texture = texInfo.texture;
                if (texture == null || !texture.name.StartsWith(Util.DIR, StringComparison.Ordinal))
                    continue;

                // Add a suit texture.
                if (texture.name.StartsWith(DIR_SUITS, StringComparison.Ordinal))
                {
                    texture.wrapMode = TextureWrapMode.Clamp;

                    int lastSlash = texture.name.LastIndexOf('/');
                    int dirNameLength = lastSlash - DIR_SUITS.Length;
                    string originalName = texture.name.Substring(lastSlash + 1);

                    if (dirNameLength < 1)
                    {
                        Util.log("Suit texture should be inside a subdirectory: {0}", texture.name);
                    }
                    else
                    {
                        string dirName = texture.name.Substring(DIR_SUITS.Length, dirNameLength);

                        int index;
                        if (!suitDirs.TryGetValue(dirName, out index))
                        {
                            index = KerbalSuitsDB_full.Count;
                            KerbalSuitsDB_full.Add(new Suit_Set { suitSetName = dirName });
                            suitDirs.Add(dirName, index);
                        }

                        Suit_Set suit = KerbalSuitsDB_full[index];
                        if (!suit.setTexture(originalName, texture))
                            Util.log("Unknown suit texture name \"{0}\": {1}", originalName, texture.name);
                    }
                }
                else if (texture.name.StartsWith(DIR_DEFAULT, StringComparison.Ordinal))
                {
                    int lastSlash = texture.name.LastIndexOf('/');
                    string originalName = texture.name.Substring(lastSlash + 1);

                    if (defaultSuit.setTexture(originalName, texture) || originalName == "kerbalMain")
                    {
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                }

                lastTextureName = texture.name;
            }*/

            readKerbalsConfigs();

            // Initialize default Kerbal, which is only loaded when the main menu shows.
            foreach (Texture2D texture in Resources.FindObjectsOfTypeAll<Texture2D>())
            {
                if (texture.name != null)
                {
                    if (texture.name == "kerbalHead")
                        defaulMaleAndFemaleHeads[0].headTexture = defaulMaleAndFemaleHeads[0].headTexture ?? texture;
                    else if (texture.name == "kerbalGirl_06_BaseColor")
                        defaulMaleAndFemaleHeads[1].headTexture = defaulMaleAndFemaleHeads[1].headTexture ?? texture;
                    else
                        defaultSuit.setTexture(texture.name, texture);
                }
            }

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
                kerbal.textureStandard = defaultSuit.Suit_Iva_Standard_Male0;
                kerbal.textureVeteran = defaultSuit.Suit_Iva_Veteran_Male0;

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

            loadKerbals(null);
            loadSuitMap(null, classSuitsDB, defaultClassSuits);
        }
    }
}