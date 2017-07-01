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
        /// List of the veterans
        /// </summary>                      
        private static readonly string[] VETERANS = { "Jebediah Kerman", "Bill Kerman", "Bob Kerman", "Valentina Kerman" };
        
        /// <summary>
        /// Default head textures (from `Default/`).
        /// </summary>
        public readonly Head[] defaultHead = { new Head { name = "DEFAULT" }, new Head { name = "DEFAULT" } };

        /// <summary>
        /// Default suit textures (from `Default/`).
        /// </summary>
        public readonly Suit defaultSuit = new Suit { name = "DEFAULT" };
        
        /// <summary>
        /// Heads textures, including excluded by configuration.
        /// </summary>
        public readonly List<Head> heads = new List<Head>();

        /// <summary>
        /// Suits textures, including excluded by configuration.
        /// </summary>
        public readonly List<Suit> suits = new List<Suit>();

        /// <summary>
        /// Male and female heads textures (minus excluded).        
        /// <typeparamref name="Male"/>
        /// <typeparamref name="Female"/>
        /// </summary>
        private readonly List<Head>[] kerbalHeads = { new List<Head>(), new List<Head>() };

        /// <summary>
        /// Male and female suits textures (minus excluded).        
        /// <typeparamref name="Male"/>
        /// <typeparamref name="Female"/>
        /// </summary>
        private readonly List<Suit>[] kerbalSuits = { new List<Suit>(), new List<Suit>() };

        /// <summary>
        /// List of your personalized Kerbals with their KerbalData
        /// </summary>
        private readonly Dictionary<string, KerbalData> gameKerbals = new Dictionary<string, KerbalData>();

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
        public readonly Dictionary<string, Suit> classSuits = new Dictionary<string, Suit>();

        /// <summary>
        /// List of the default class specific suits
        /// </summary>
        public readonly Dictionary<string, Suit> defaultClassSuits = new Dictionary<string, Suit>();

        /// <summary>
        /// List of cabin specific suits
        /// </summary>
        private readonly Dictionary<string, Suit> cabinSuits = new Dictionary<string, Suit>();

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
        public bool isEVAgroundSuitEnabled = false;

        /// <summary>
        /// Do we use the automatic suit state switcher ? 
        /// </summary>
        public bool isAutomaticSuitSwitchEnabled = false;

        /// <summary>
        /// remove collar on IVA suits ? (for later)
        /// </summary>
        public bool isCollarRemovalEnabled = false;


        /* =========================================================================================
         * personal suit options
         * used for each suit texture pack
         * =========================================================================================
         */

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
        /// Force IVA suit state when toggle suit (bypass atmospheric & safe situation)
        /// </summary>
        public bool ForceIVAsuittoggle = false;

        /// <summary>
        /// IVA suit use IVA helmet
        /// </summary>
        public bool IVAsuitUse_IVAhelmet = false;

        /// <summary>
        /// IVA suit use EVA ground helmet
        /// </summary>
        public bool IVAsuitUse_EVAgroundHelmet = false;

        /// <summary>
        /// IVA suit use EVA space helmet
        /// </summary>
        public bool IVAsuitUse_EVAspaceHelmet = true;

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
        /// Force collar removal on IVA suits
        /// </summary>
        public bool ForceCollarRemoval = false;

        /// <summary>
        /// Force collar use on the IVA suits 
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
        /// Contain the configuration data of a head texture
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public class Head
        {
            /// <summary>
            /// the name of the head texture
            /// </summary>
            public string name;

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
            public Texture2D head;

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
        public class Suit
        {
            /// <summary>
            /// The name of the suit set
            /// </summary>
            public string name;

            /// <summary>
            /// Is the suit set made for female kerbal?
            /// </summary>
            public bool isFemale;

            /* =====================================================================================
             * Level 0 textures (default textures)
             * =====================================================================================
             */
              
            /// <summary>
            /// Texture for the veteran suit
            /// </summary>
            public Texture2D suitVeteran;

            /// <summary>
            /// Texture for the IVA suit
            /// </summary>
            public Texture2D suit;

            /// <summary>
            /// Normal map for the IVA suit
            /// </summary>
            public Texture2D suitNRM;

            /// <summary>
            /// Texture for the IVA helmet
            /// </summary>
            public Texture2D helmet;

            /// <summary>
            /// Normal map for the IVA helmet
            /// </summary>
            public Texture2D helmetNRM;

            /// <summary>
            /// Texture for the IVA visor
            /// </summary>
            public Texture2D visor;

            /// <summary>
            /// Normal map for the IVA visor 
            /// </summary>
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// <summary>
            /// Texture for the EVA space suit
            /// </summary>
            public Texture2D evaSuit;

            /// <summary>
            /// Normal map for the EVA space suit
            /// </summary>
            public Texture2D evaSuitNRM;

            /// <summary>
            /// Texture for the EVA space helmet
            /// </summary>
            public Texture2D evaHelmet;

            /// <summary>
            /// Normal map for the EVA space helmet 
            /// </summary>
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// <summary>
            /// Texture for the EVA space visor
            /// </summary>
            public Texture2D evaVisor;

            /// <summary>
            /// Normal map for the EVA space visor 
            /// </summary>
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// <summary>
            /// Texture for the EVA space jetpack
            /// </summary>
            public Texture2D evaJetpack;

            /// <summary>
            /// Normal map for the EVA space jetpack
            /// </summary>
            public Texture2D evaJetpackNRM;

            /// <summary>
            /// Texture for the EVA ground suit
            /// </summary>
            public Texture2D evaGroundSuit;

            /// <summary>
            /// Normal map for the EVA ground suit
            /// </summary>
            public Texture2D evaGroundSuitNRM;

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
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// <summary>
            /// Texture for the EVA ground jetpack
            /// </summary>
            public Texture2D evaGroundJetpack;

            /// <summary>
            /// normal map for the EVA ground jetpack
            /// </summary>
            public Texture2D evaGroundJetpackNRM;

            /* =====================================================================================
             * Level 1-5 textures (Level textures)
             * =====================================================================================
             */

            /// <summary>
            /// The texture list for the leveled IVA suit
            /// </summary>
            private Texture2D[] levelSuits;

            /// <summary>
            /// The texture list for the leveled IVA helmet
            /// </summary>
            private Texture2D[] levelHelmets;

            /// <summary>
            /// The texture list for the leveled IVA visor
            /// </summary>
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// <summary>
            /// Normal map list for the leveled IVA visor
            /// </summary>
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// <summary>
            /// The texture list for the leveled EVA space suit
            /// </summary>
            private Texture2D[] levelEvaSuits;

            /// <summary>
            /// The texture list for the leveled EVA space helmet
            /// </summary>
            private Texture2D[] levelEvaHelmets;

            /// <summary>
            /// The texture list for the leveled EVA space visor
            /// </summary>
            private Texture2D[] levelEvaVisors;

            /// <summary>
            /// Normal map list for the leveled EVA space visor
            /// </summary>
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// <summary>
            /// The texture list for the leveled EVA space jetpack
            /// </summary>
            private Texture2D[] levelEvaJetpacks;

            /// <summary>
            /// The texture list for the leveled EVA ground suit
            /// </summary>
            private Texture2D[] levelEvaGroundSuits;

            /// <summary>
            /// The texture list for the leveled EVA ground helmet
            /// </summary>
            private Texture2D[] levelEvaGroundHelmets;

            /// <summary>
            /// The texture list for the leveled EVA ground visor
            /// </summary>
            private Texture2D[] levelEvaGroundVisors;

            /// <summary>
            /// Normal map list for the leveled EVA ground visor
            /// </summary>
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// <summary>
            /// The texture list for the leveled EVA ground jetpack
            /// </summary>
            private Texture2D[] levelEvaGroundJetpacks;

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA suit for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA suit texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getSuit(int level)
            {
                return level != 0 && levelSuits != null ? levelSuits[level - 1] : suit;
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
            public Texture2D getHelmet(int level)
            {
                return level != 0 && levelHelmets != null ? levelHelmets[level - 1] : helmet;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA visor for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA visor texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// ************************************************************************************
            /// <summary>
            /// Used to get the IVA visor normal map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The IVA visor normal map for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space suit for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space suit texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaSuit(int level)
            {
                return level != 0 && levelEvaSuits != null ? levelEvaSuits[level - 1] : evaSuit;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space helmet for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space helmet texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaHelmet(int level)
            {
                return level != 0 && levelEvaHelmets != null ? levelEvaHelmets[level - 1] : evaHelmet;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space visor for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space visor texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaVisor(int level)
            {
                return level != 0 && levelEvaVisors != null ? levelEvaVisors[level - 1] : evaVisor;
            }

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space visor normal map for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space visor normal map for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            /// ************************************************************************************
            /// <summary>
            /// Used to get the EVA space jetpack for the level of the kerbal
            /// </summary>
            /// <param name="level">The level of the kerbal</param>
            /// <returns>The EVA space jetpack texture for the level of the kerbal 
            /// (if no texture for his level, it return the last one saved in the class parameter)</returns>
            /// ************************************************************************************
            public Texture2D getEvaJetpack(int level)
            {
                return level != 0 && levelEvaJetpacks != null ? levelEvaJetpacks[level - 1] : evaJetpack;
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
                return level != 0 && levelEvaGroundSuits != null ? levelEvaGroundSuits[level - 1] : evaGroundSuit;
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
            /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  need to add it !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

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
                    case "kerbalMain":
                        suitVeteran = suitVeteran ?? texture;
                        return false;

                    case "kerbalMainGrey":
                        suit = suit ?? texture;
                        return true;

                    case "kerbalMainNRM":
                        suitNRM = suitNRM ?? texture;
                        return true;

                    case "kerbalHelmetGrey":
                        helmet = helmet ?? texture;
                        return true;

                    case "kerbalHelmetNRM":
                        helmetNRM = helmetNRM ?? texture;
                        return true;

                    case "kerbalVisor":
                        visor = visor ?? texture;
                        return true;

                    case "EVAtexture":
                        evaSuit = evaSuit ?? texture;
                        return true;

                    case "EVAtextureNRM":
                        evaSuitNRM = evaSuitNRM ?? texture;
                        return true;

                    case "EVAhelmet":
                        evaHelmet = evaHelmet ?? texture;
                        return true;

                    case "EVAvisor":
                        evaVisor = evaVisor ?? texture;
                        return true;

                    case "EVAjetpack":
                        evaJetpack = evaJetpack ?? texture;
                        return true;

                    case "EVAjetpackNRM":
                        evaJetpackNRM = evaJetpackNRM ?? texture;
                        return true;

                    case "EVAgroundTexture":
                        evaGroundSuit = evaGroundSuit ?? texture;
                        return true;

                    case "EVAgroundTextureNRM":
                        evaGroundSuitNRM = evaGroundSuitNRM ?? texture;
                        return true;

                    case "EVAgroundHelmet":
                        evaGroundHelmet = evaGroundHelmet ?? texture;
                        return true;

                    case "EVAgroundHelmetNRM":
                        evaGroundHelmetNRM = evaGroundHelmetNRM ?? texture;
                        return true;

                    case "EVAgroundVisor":
                        evaGroundVisor = evaGroundVisor ?? texture;
                        return true;

                    case "EVAgroundJetpack":
                        evaGroundJetpack = evaGroundJetpack ?? texture;
                        return true;

                    case "EVAgroundJetpackNRM":
                        evaGroundJetpackNRM = evaGroundJetpackNRM ?? texture;
                        return true;

                    case "kerbalMainGrey1":
                    case "kerbalMainGrey2":
                    case "kerbalMainGrey3":
                    case "kerbalMainGrey4":
                    case "kerbalMainGrey5":
                        level = originalName.Last() - 0x30;
                        levelSuits = levelSuits ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelSuits[i] = texture;
                        return true;

                    case "kerbalHelmetGrey1":
                    case "kerbalHelmetGrey2":
                    case "kerbalHelmetGrey3":
                    case "kerbalHelmetGrey4":
                    case "kerbalHelmetGrey5":
                        level = originalName.Last() - 0x30;
                        levelHelmets = levelHelmets ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelHelmets[i] = texture;
                        return true;

                    case "EVAtexture1":
                    case "EVAtexture2":
                    case "EVAtexture3":
                    case "EVAtexture4":
                    case "EVAtexture5":
                        level = originalName.Last() - 0x30;
                        levelEvaSuits = levelEvaSuits ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaSuits[i] = texture;
                        return true;

                    case "EVAhelmet1":
                    case "EVAhelmet2":
                    case "EVAhelmet3":
                    case "EVAhelmet4":
                    case "EVAhelmet5":
                        level = originalName.Last() - 0x30;
                        levelEvaHelmets = levelEvaHelmets ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaHelmets[i] = texture;
                        return true;

                    case "EVAvisor1":
                    case "EVAvisor2":
                    case "EVAvisor3":
                    case "EVAvisor4":
                    case "EVAvisor5":
                        level = originalName.Last() - 0x30;
                        levelEvaVisors = levelEvaVisors ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaVisors[i] = texture;
                        return true;

                    case "EVAjetpack1":
                    case "EVAjetpack2":
                    case "EVAjetpack3":
                    case "EVAjetpack4":
                    case "EVAjetpack5":
                        level = originalName.Last() - 0x30;
                        levelEvaJetpacks = levelEvaJetpacks ?? new Texture2D[5];

                        for (int i = level - 1; i < 5; ++i)
                            levelEvaJetpacks[i] = texture;
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
            public int hash;
            public int gender;
            public bool isVeteran;

            public Head head;
            public Suit suit;
            public Suit cabinSuit;
        }

        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Component bound to internal models that triggers Kerbal texture personalization
        /// when the internal model changes.
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private class TRR_IvaModule : MonoBehaviour
        {
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
            private Reflections.Script reflectionScript = null;

            [KSPField(isPersistant = true)]
            private bool isInitialised = false;

            [KSPField(isPersistant = true)]
            public bool hasEvaSuit = false;

            [KSPField(isPersistant = true)]
            public bool hasEvaGroundSuit = false;

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

                    switch (personaliser.personaliseEva(part, actualSuitState))
                    {
                        case 0:     //IVA suit, if no air switch to state 1 : EVAground
                            actualSuitState = 0;
                            hasEvaSuit = false;
                            hasEvaGroundSuit = false;
                            if (reflectionScript != null)
                                reflectionScript.setActive(hasEvaSuit);
                            break;
                        case 1:     //EVAground suit
                            actualSuitState = 1;
                            hasEvaSuit = true;
                            hasEvaGroundSuit = true;
                            if (reflectionScript != null)
                                reflectionScript.setActive(hasEvaSuit);
                            break;
                        case 2:     //EVA suit
                            actualSuitState = 2;
                            hasEvaSuit = true;
                            hasEvaGroundSuit = false;
                            if (reflectionScript != null)
                                reflectionScript.setActive(hasEvaSuit);
                            break;
                    }
                }
                else // Legacy suit state from TextureReplacer
                {
                    if (personaliser.personaliseEvaLegacy(part, !hasEvaSuit))
                    {
                        hasEvaSuit = !hasEvaSuit;

                        if (reflectionScript != null)
                            reflectionScript.setActive(hasEvaSuit);
                    }
                    else
                    {
                        ScreenMessages.PostScreenMessage("No breathable atmosphere", 5.0f, ScreenMessageStyle.UPPER_CENTER);
                    }
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
            /// Update()
            /// </summary>
            /// ************************************************************************************
            public void Update()
            {
                Personaliser personaliser = Personaliser.instance;
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
                } else
                {// legacy system from TR
                    if (!hasEvaSuit && !personaliser.isAtmBreathable())
                    {
                        personaliser.personaliseEvaLegacy(part, true);
                        hasEvaSuit = true;

                        if (reflectionScript != null)
                            reflectionScript.setActive(true);
                    }

                }
                personaliser.personaliseEva(part, actualSuitState);
            }

            /// ************************************************************************************
            /// <summary>
            /// OnDestroy()
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
        private static readonly string DIR_DEFAULT = Util.DIR + "Default/";
        private static readonly string DIR_HEADS = Util.DIR + "Heads/";
        private static readonly string DIR_SUITS = Util.DIR + "Suits/";

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Whether a vessel is in a "safe" situation, so Kerbals don't need helmets (landed/splashed or in orbit).
        /// </summary>
        /// <param name="vessel"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private static bool isSituationSafe(Vessel vessel)
        {
            return vessel.situation != Vessel.Situations.FLYING && vessel.situation != Vessel.Situations.SUB_ORBITAL;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Whether atmosphere is breathable.
        /// </summary>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public bool isAtmBreathable()
        {
            bool value = !HighLogic.LoadedSceneIsFlight
                         || (FlightGlobals.getStaticPressure() >= atmSuitPressure
                         && atmSuitBodies.Contains(FlightGlobals.currentMainBody.bodyName));
            return value;
        }
        
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Whether we are under sub orbit.
        /// <para>Used to change automatically suits.</para>        
        /// </summary>        
        /// <param name="vessel"></param>
        /// <returns></returns>
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
        /// <param name="kerbal"></param>
        /// <returns></returns>     
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private Suit getClassSuit(ProtoCrewMember kerbal)
        {
            Suit suit;
            classSuits.TryGetValue(kerbal.experienceTrait.Config.Name, out suit);
            return suit;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Return the kerbalData of a kerbal
        /// <para> </para>
        /// </summary>
        /// <param name="kerbal"></param>
        /// <returns></returns>     
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public KerbalData getKerbalData(ProtoCrewMember kerbal)
        {
            KerbalData kerbalData;

            if (!gameKerbals.TryGetValue(kerbal.name, out kerbalData))
            {
                kerbalData = new KerbalData
                {
                    hash = kerbal.name.GetHashCode(),
                    gender = (int)kerbal.gender,
                    isVeteran = VETERANS.Any(n => n == kerbal.name)
                };
                gameKerbals.Add(kerbal.name, kerbalData);

                if (forceLegacyFemales)
                    kerbal.gender = ProtoCrewMember.Gender.Male;
            }
            return kerbalData;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Return the head chosen for the kerbal
        /// <para> If no head chosen, pick a random one</para>
        /// </summary>
        /// <param name="kerbal"></param>
        /// <param name="kerbalData"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public Head getKerbalHead(ProtoCrewMember kerbal, KerbalData kerbalData)
        {
            if (kerbalData.head != null)
                return kerbalData.head;

            List<Head> genderHeads = kerbalHeads[kerbalData.gender];
            if (genderHeads.Count == 0)
                return defaultHead[(int)kerbal.gender];

            // Hash is multiplied with a large prime to increase randomisation, since hashes returned by `GetHashCode()` are
            // close together if strings only differ in the last (few) char(s).
            int number = (kerbalData.hash * 4099) & 0x7fffffff;
            return genderHeads[number % genderHeads.Count];
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Search the KerbalData of a kerbal and return his suit set
        /// <para>If no suit set for this kerbal, search the classSuit list and return the list for his class </para>
        /// <para>If no suit set for his class in the classSuit list, give the kerbal a random suit</para>
        /// </summary>
        /// <param name="kerbal"></param>
        /// <param name="kerbalData"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public Suit getKerbalSuit(ProtoCrewMember kerbal, KerbalData kerbalData)
        {
            Suit suit = kerbalData.suit ?? getClassSuit(kerbal);
            if (suit != null)
                return suit;

            List<Suit> genderSuits = kerbalSuits[0];

            // Use female suits only if available, fall back to male suits otherwise.
            if (kerbalData.gender != 0 && kerbalSuits[1].Count != 0)
                genderSuits = kerbalSuits[1];
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
        /// <param name="component"></param>
        /// <param name="kerbal"></param>
        /// <param name="cabin"></param>
        /// <param name="needsSuit"></param>
        /// <param name="needGroundSuit"></param>
        /// /// ////////////////////////////////////////////////////////////////////////////////////////
        private void personaliseKerbal(Component component, ProtoCrewMember kerbal, Part cabin, bool needsSuit, bool needGroundSuit)
        {
            KerbalData kerbalData = getKerbalData(kerbal);
            bool isEva = cabin == null;

            Head head = getKerbalHead(kerbal, kerbalData);
            Suit suit = null;

            if (isEva || !cabinSuits.TryGetValue(cabin.partInfo.name, out kerbalData.cabinSuit))
                suit = getKerbalSuit(kerbal, kerbalData);

            head = head == defaultHead[(int)kerbal.gender] ? null : head;
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
                                newTexture = head.head;
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
                            //bool isEVAGroundSuit = isEva && needsSuit && needGroundSuit;

                            if (suit != null)
                            {
                                // newTexture = isEvaSuit ? suit.getEvaSuit(kerbal.experienceLevel) : suit.getSuit(kerbal.experienceLevel);
                                // newNormalMap = isEvaSuit ? suit.evaSuitNRM : suit.suitNRM;

                                if (isEva)
                                {
                                    if (needsSuit && needGroundSuit)
                                    {
                                        newTexture = suit.getEvaGroundSuit(kerbal.experienceLevel);
                                        newNormalMap = suit.evaGroundSuitNRM;
                                    }
                                    else if (needsSuit)
                                    {
                                        newTexture = suit.getEvaSuit(kerbal.experienceLevel);
                                        newNormalMap = suit.evaSuitNRM;
                                    }
                                    else
                                    {
                                        newTexture = suit.getSuit(kerbal.experienceLevel);
                                        newNormalMap = suit.suitNRM;
                                    }
                                }
                                else
                                {
                                    newTexture = suit.getSuit(kerbal.experienceLevel);
                                    newNormalMap = suit.suitNRM;
                                }
                            }
                            if (newTexture == null)
                            {
                                // This required for two reasons: to fix IVA suits after KSP resetting them to the stock ones all the
                                // time and to fix the switch from non-default to default texture during EVA suit toggle.
                                newTexture = isEvaSuit ? defaultSuit.evaSuit
                                  : kerbalData.isVeteran ? defaultSuit.suitVeteran
                                  : defaultSuit.suit;
                            }

                            if (newNormalMap == null)
                                newNormalMap = isEvaSuit ? defaultSuit.evaSuitNRM : defaultSuit.suitNRM;

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
                                if (isEva && needGroundSuit)
                                {
                                    newTexture = suit.getEvaGroundHelmet(kerbal.experienceLevel);
                                    newNormalMap = suit.evaGroundHelmetNRM;
                                }
                                else if (isEva)
                                {
                                    newTexture = suit.getEvaHelmet(kerbal.experienceLevel);
                                    newNormalMap = suit.helmetNRM;
                                }
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
                                newTexture = isEva ? suit.getEvaVisor(kerbal.experienceLevel) : suit.getEvaVisor(kerbal.experienceLevel);

                                if (newTexture != null)
                                    material.color = Color.white;
                            }
                            break;

                        case "jetpack":
                        case "mesh_female_kerbalAstronaut01_jetpack":
                            if (isEva)
                            {
                                smr.enabled = needsSuit;
                                if (suit != null)
                                {
                                    if (needsSuit && needGroundSuit)
                                    {
                                        newTexture = suit.getEvaGroundJetpack(kerbal.experienceLevel);
                                        newNormalMap = suit.evaGroundJetpackNRM;
                                    }
                                    else if (needsSuit)
                                    {
                                        newTexture = suit.getEvaJetpack(kerbal.experienceLevel);
                                        newNormalMap = suit.evaJetpackNRM;
                                    }
                                }
                            }

                            break;
                        default: // Jetpack.
                            if (isEva)
                            {
                                smr.enabled = needsSuit;
                                if (suit != null)
                                {
                                    if (needsSuit && needGroundSuit)
                                    {
                                        newTexture = suit.getEvaGroundJetpack(kerbal.experienceLevel);
                                        newNormalMap = suit.evaGroundJetpackNRM;
                                    }
                                    else if (needsSuit)
                                    {
                                        newTexture = suit.getEvaJetpack(kerbal.experienceLevel);
                                        newNormalMap = suit.evaJetpackNRM;
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
        private void personaliseKerbalLegacy(Component component, ProtoCrewMember kerbal, Part cabin, bool needsSuit)
        {
            KerbalData kerbalData = getKerbalData(kerbal);
            bool isEva = cabin == null;

            Head head = getKerbalHead(kerbal, kerbalData);
            Suit suit = null;

            if (isEva || !cabinSuits.TryGetValue(cabin.partInfo.name, out kerbalData.cabinSuit))
                suit = getKerbalSuit(kerbal, kerbalData);

            head = head == defaultHead[(int)kerbal.gender] ? null : head;
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
                                newTexture = head.head;
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
                                newTexture = isEvaSuit ? suit.getEvaSuit(kerbal.experienceLevel) : suit.getSuit(kerbal.experienceLevel);
                                newNormalMap = isEvaSuit ? suit.evaSuitNRM : suit.suitNRM;
                            }

                            if (newTexture == null)
                            {
                                // This required for two reasons: to fix IVA suits after KSP resetting them to the stock ones all the
                                // time and to fix the switch from non-default to default texture during EVA suit toggle.
                                newTexture = isEvaSuit ? defaultSuit.evaSuit
                                  : kerbalData.isVeteran ? defaultSuit.suitVeteran
                                  : defaultSuit.suit;
                            }

                            if (newNormalMap == null)
                                newNormalMap = isEvaSuit ? defaultSuit.evaSuitNRM : defaultSuit.suitNRM;

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
                                newTexture = isEva ? suit.getEvaHelmet(kerbal.experienceLevel) : suit.getHelmet(kerbal.experienceLevel);
                                newNormalMap = suit.helmetNRM;
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
                                newTexture = isEva ? suit.evaVisor : suit.visor;

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
                                    newTexture = suit.evaJetpack;
                                    newNormalMap = suit.evaJetpackNRM;
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
        /// Personalize Kerbals in an internal space of a vessel. Used by IvaModule.
        /// </summary>
        /// <param name="kerbal"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void personaliseIva(Kerbal kerbal)
        {
            bool needsSuit = !isHelmetRemovalEnabled || !isSituationSafe(kerbal.InVessel);

            Personaliser personaliser = Personaliser.instance;

            // new suit State for TRR
            if (personaliser.isNewSuitStateEnabled)
            {
                personaliseKerbal(kerbal, kerbal.protoCrewMember, kerbal.InPart, needsSuit, false);
            }
            else // Legacy suit state from TextureReplacer
            {
                personaliseKerbalLegacy(kerbal, kerbal.protoCrewMember, kerbal.InPart, needsSuit);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method to show or hide the helmet and visor. Called on vessel state change event.
        /// </summary>
        /// <param name="action"></param>
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
        /// Set external IVA//EVAground/EVAspace suits. 
        /// <para>The different codes send are used to maintain the logic to switch suits. (IVA->EVAground->EVAspace)</para>
        /// <para>Does a loop between EVA->EVAground suits outside of breathable atmosphere.</para>
        /// This function is used by <see cref="Personaliser.TRR_EvaModule"/>.
        /// </summary>
        /// <param name="evaPart"></param>
        /// <param name="suitSelection"></param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private int personaliseEva(Part evaPart, int suitSelection)
        {

            int selection = suitSelection;
            bool evaSuit = false;
            bool evaGroundSuit = false;

            List<ProtoCrewMember> crew = evaPart.protoModuleCrew;
            if (crew.Count != 0)
            {
                switch (selection)
                {
                    case 0:  //IVA suit, if no air switch to state 1 : EVAground
                        if (!isAtmBreathable())
                        {
                            evaSuit = true;
                            selection = 1;
                        }
                        break;
                    case 1: // EVA ground
                        evaSuit = true;
                        evaGroundSuit = true;
                        break;
                    case 2: // EVA
                        evaSuit = true;
                        //evaGroundSuit = true;
                        break;

                }

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
        private bool personaliseEvaLegacy(Part evaPart, bool evaSuit)
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
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Load per-game custom kerbals mapping.
        /// <para> This list is first loaded from the file @default.cfg</para>
        /// <para>Then this loaded in the persistent.sfs save</para>
        /// </summary>
        /// <param name="node"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void loadKerbals(ConfigNode node)
        {
            node = node ?? customKerbalsNode;

            KerbalRoster roster = HighLogic.CurrentGame.CrewRoster;

            foreach (ProtoCrewMember kerbal in roster.Crew.Concat(roster.Tourist).Concat(roster.Unowned))
            {
                if (kerbal.rosterStatus == ProtoCrewMember.RosterStatus.Dead
                    && kerbal.type != ProtoCrewMember.KerbalType.Unowned)
                {
                    continue;
                }

                KerbalData kerbalData = getKerbalData(kerbal);

                string value = node.GetValue(kerbal.name);
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
                        kerbalData.head = headName == "DEFAULT" ? defaultHead[(int)kerbal.gender]
                          : heads.Find(h => h.name == headName);
                    }

                    if (suitName != null && suitName != "GENERIC")
                        kerbalData.suit = suitName == "DEFAULT" ? defaultSuit : suits.Find(s => s.name == suitName);

                    kerbal.gender = forceLegacyFemales ? ProtoCrewMember.Gender.Male : (ProtoCrewMember.Gender)kerbalData.gender;
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Save per-game custom Kerbals mapping.        
        /// <para>Then this is saved in the persistent.sfs save</para>
        /// </summary>
        /// <param name="node"></param>
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
                string headName = kerbalData.head == null ? "GENERIC" : kerbalData.head.name;
                string suitName = kerbalData.suit == null ? "GENERIC" : kerbalData.suit.name;

                node.AddValue(kerbal.name, genderName + " " + headName + " " + suitName);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Load suit mapping.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="map"></param>
        /// <param name="defaultMap"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void loadSuitMap(ConfigNode node, IDictionary<string, Suit> map, IDictionary<string, Suit> defaultMap = null)
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
                            Suit suit = suits.Find(s => s.name == suitName);
                            if (suit != null)
                                map[entry.name] = suit;
                        }
                    }
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Save suit mapping.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="node"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private static void saveSuitMap(Dictionary<string, Suit> map, ConfigNode node)
        {
            foreach (var entry in map)
            {
                string suitName = entry.Value == null ? "GENERIC" : entry.Value.name;

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
            var femaleHeads = new List<string>();
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
                    Util.addLists(genericNode.GetValues("femaleHeads"), femaleHeads);
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
            foreach (Head head in heads)
            {
                head.isFemale = femaleHeads.Contains(head.name);
                head.isEyeless = eyelessHeads.Contains(head.name);
            }
            // Tag female suits.
            foreach (Suit suit in suits)
                suit.isFemale = femaleSuits.Contains(suit.name);

            // Create lists of male heads and suits.
            kerbalHeads[0].AddRange(heads.Where(h => !h.isFemale && !excludedHeads.Contains(h.name)));
            kerbalSuits[0].AddRange(suits.Where(s => !s.isFemale && !excludedSuits.Contains(s.name)));

            // Create lists of female heads and suits.
            kerbalHeads[1].AddRange(heads.Where(h => h.isFemale && !excludedHeads.Contains(h.name)));
            kerbalSuits[1].AddRange(suits.Where(s => s.isFemale && !excludedSuits.Contains(s.name)));

            // Trim lists.
            heads.TrimExcess();
            suits.TrimExcess();
            kerbalHeads[0].TrimExcess();
            kerbalSuits[0].TrimExcess();
            kerbalHeads[1].TrimExcess();
            kerbalSuits[1].TrimExcess();
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Read configuration and perform pre-load initialization.
        /// </summary>
        /// <param name="rootNode"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void readConfig(ConfigNode rootNode)
        {
            Util.parse(rootNode.GetValue("isHelmetRemovalEnabled"), ref isHelmetRemovalEnabled);
            Util.parse(rootNode.GetValue("isAtmSuitEnabled"), ref isAtmSuitEnabled);
            Util.parse(rootNode.GetValue("atmSuitPressure"), ref atmSuitPressure);
            Util.addLists(rootNode.GetValues("atmSuitBodies"), atmSuitBodies);
            Util.parse(rootNode.GetValue("forceLegacyFemales"), ref forceLegacyFemales);
            Util.parse(rootNode.GetValue("isNewSuitStateEnabled"), ref isNewSuitStateEnabled);
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

            foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture)
            {
                Texture2D texture = texInfo.texture;
                if (texture == null || !texture.name.StartsWith(Util.DIR, StringComparison.Ordinal))
                    continue;

                // Add a head texture.
                if (texture.name.StartsWith(DIR_HEADS, StringComparison.Ordinal))
                {
                    texture.wrapMode = TextureWrapMode.Clamp;

                    string headName = texture.name.Substring(DIR_HEADS.Length);
                    if (headName.EndsWith("NRM", StringComparison.Ordinal))
                    {
                        string baseName = headName.Substring(0, headName.Length - 3);

                        Head head = heads.Find(h => h.name == baseName);
                        if (head != null)
                            head.headNRM = texture;
                    }
                    else if (heads.All(h => h.name != headName))
                    {
                        Head head = new Head { name = headName, head = texture };
                        heads.Add(head);
                    }
                }
                // Add a suit texture.
                else if (texture.name.StartsWith(DIR_SUITS, StringComparison.Ordinal))
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
                            index = suits.Count;
                            suits.Add(new Suit { name = dirName });
                            suitDirs.Add(dirName, index);
                        }

                        Suit suit = suits[index];
                        if (!suit.setTexture(originalName, texture))
                            Util.log("Unknown suit texture name \"{0}\": {1}", originalName, texture.name);
                    }
                }
                else if (texture.name.StartsWith(DIR_DEFAULT, StringComparison.Ordinal))
                {
                    int lastSlash = texture.name.LastIndexOf('/');
                    string originalName = texture.name.Substring(lastSlash + 1);

                    if (originalName == "kerbalHead")
                    {
                        defaultHead[0].head = texture;
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                    else if (originalName == "kerbalHeadNRM")
                    {
                        defaultHead[0].headNRM = texture;
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                    else if (originalName == "kerbalGirl_06_BaseColor")
                    {
                        defaultHead[1].head = texture;
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                    else if (originalName == "kerbalGirl_06_BaseColorNRM")
                    {
                        defaultHead[1].headNRM = texture;
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                    else if (defaultSuit.setTexture(originalName, texture) || originalName == "kerbalMain")
                    {
                        texture.wrapMode = TextureWrapMode.Clamp;
                    }
                }

                lastTextureName = texture.name;
            }

            readKerbalsConfigs();

            // Initialize default Kerbal, which is only loaded when the main menu shows.
            foreach (Texture2D texture in Resources.FindObjectsOfTypeAll<Texture2D>())
            {
                if (texture.name != null)
                {
                    if (texture.name == "kerbalHead")
                        defaultHead[0].head = defaultHead[0].head ?? texture;
                    else if (texture.name == "kerbalGirl_06_BaseColor")
                        defaultHead[1].head = defaultHead[1].head ?? texture;
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
                kerbal.textureStandard = defaultSuit.suit;
                kerbal.textureVeteran = defaultSuit.suitVeteran;

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
        /// <param name="node"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void loadScenario(ConfigNode node)
        {
            gameKerbals.Clear();
            classSuits.Clear();

            loadKerbals(node.GetNode("Kerbals"));
            loadSuitMap(node.GetNode("ClassSuits"), classSuits, defaultClassSuits);

            Util.parse(node.GetValue("isHelmetRemovalEnabled"), ref isHelmetRemovalEnabled);
            Util.parse(node.GetValue("isAtmSuitEnabled"), ref isAtmSuitEnabled);
            Util.parse(node.GetValue("isNewSuitStateEnabled"), ref isNewSuitStateEnabled);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used to save the configuration data in the .cfg file and in persistent save
        /// </summary>
        /// <param name="node"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void saveScenario(ConfigNode node)
        {
            saveKerbals(node.AddNode("Kerbals"));
            saveSuitMap(classSuits, node.AddNode("ClassSuits"));

            node.AddValue("isHelmetRemovalEnabled", isHelmetRemovalEnabled);
            node.AddValue("isAtmSuitEnabled", isAtmSuitEnabled);
            node.AddValue("isNewSuitStateEnabled", isNewSuitStateEnabled);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Reset the configuration data form the suits and the heads. Called by the "Reset to Default" button in the GUI. 
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void resetKerbals()
        {
            gameKerbals.Clear();
            classSuits.Clear();

            loadKerbals(null);
            loadSuitMap(null, classSuits, defaultClassSuits);
        }
    }
}