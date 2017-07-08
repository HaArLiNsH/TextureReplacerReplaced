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
using UnityEngine;

namespace TextureReplacerReplaced
{
    /// <summary>
    /// The configuration windows in the space center scene
    /// </summary>
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class TRR_Gui : MonoBehaviour
    {
        /// <summary>
        /// icon for the toolbar
        /// </summary>
        private static readonly string APP_ICON_PATH = Util.DIR + "Plugins/AppIcon";

        /// <summary>
        /// The 3 types of reflections : "None", "Static", "Real"
        /// </summary>
        private static readonly string[] REFLECTION_TYPES = { "None", "Static", "Real" };

        /// <summary>
        /// the color of the selected item
        /// </summary>
        private static readonly Color SELECTED_COLOUR = new Color(0.7f, 0.9f, 1.0f);

        /// <summary>
        /// the color for the class
        /// </summary>
        private static readonly Color CLASS_COLOUR = new Color(1.0f, 0.8f, 1.0f);

        /// <summary>
        /// unique ID of the window of the GUI
        /// </summary>
        private const int WINDOW_ID = 107056;

        /// <summary>
        /// Classes from config files.
        /// </summary>
        private readonly List<string> classes = new List<string>();

        /// <summary>
        /// Ui window size
        /// </summary>
        private Rect windowRect = new Rect(Screen.width - 600, 60, 580, 610);

        /// <summary>
        /// vector used for the scroll in the roster area of the GUI
        /// </summary>
        private Vector2 rosterScroll = Vector2.zero;

        /// <summary>
        /// helper for the selected kerbal
        /// </summary>
        private ProtoCrewMember selectedKerbal = null;

        /// <summary>
        /// helper for the selected class
        /// </summary>
        private string selectedClass = null;

        /// <summary>
        /// check to open or close the GUI
        /// </summary>
        private bool isEnabled = false;

        /// <summary>
        /// Application launcher icon.
        /// </summary>
        private Texture2D appIcon = null;

        /// <summary>
        /// Application launcher button.
        /// </summary>
        private ApplicationLauncherButton appButton = null;

        /// <summary>
        /// Checker to see if we use the GUI or not 
        /// <para> used in the @default.cfg file (default = true)</para>
        /// </summary>
        private bool isGuiEnabled = true;

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The method that populate the GUI window
        /// </summary>
        /// <param name="id"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void windowHandler(int id)
        {
            Reflections reflections = Reflections.instance;
            Personaliser personaliser = Personaliser.instance;

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical(GUILayout.Width(200));

            // Roster area.
            rosterScroll = GUILayout.BeginScrollView(rosterScroll);
            GUILayout.BeginVertical();

            foreach (ProtoCrewMember kerbal in HighLogic.CurrentGame.CrewRoster.Crew)
            {
                switch (kerbal.rosterStatus)
                {
                    case ProtoCrewMember.RosterStatus.Assigned:
                        GUI.contentColor = Color.cyan;
                        break;

                    case ProtoCrewMember.RosterStatus.Dead:
                        continue;
                    case ProtoCrewMember.RosterStatus.Missing:
                        GUI.contentColor = Color.yellow;
                        break;

                    default:
                        GUI.contentColor = Color.white;
                        break;
                }

                if (GUILayout.Button(kerbal.name))
                {
                    selectedKerbal = kerbal;
                    selectedClass = null;
                }
            }

            foreach (ProtoCrewMember kerbal in HighLogic.CurrentGame.CrewRoster.Unowned)
            {
                switch (kerbal.rosterStatus)
                {
                    case ProtoCrewMember.RosterStatus.Dead:
                        GUI.contentColor = Color.cyan;
                        break;

                    default:
                        continue;
                }

                if (GUILayout.Button(kerbal.name))
                {
                    selectedKerbal = kerbal;
                    selectedClass = null;
                }
            }

            GUI.contentColor = Color.white;
            GUI.color = CLASS_COLOUR;

            // Class suits.
            foreach (string clazz in classes)
            {
                if (GUILayout.Button(clazz))
                {
                    selectedKerbal = null;
                    selectedClass = clazz;
                }
            }

            GUI.color = Color.white;

            GUILayout.EndVertical();
            GUILayout.EndScrollView();

            if (GUILayout.Button("Reset to Defaults"))
                personaliser.resetKerbals();

            GUILayout.EndVertical();

            // Textures.
            Personaliser.Head_Set defaultHead = personaliser.defaulMaleAndFemaleHeads[0];
            Personaliser.Suit_Set defaultSuit = personaliser.defaultSuit;
            Personaliser.KerbalData kerbalData = null;
            Personaliser.Head_Set head = null;
            Personaliser.Suit_Set suit = null;
            int headIndex = -1;
            int suitIndex = -1;

            if (selectedKerbal != null)
            {
                kerbalData = personaliser.getKerbalData(selectedKerbal);
                defaultHead = personaliser.defaulMaleAndFemaleHeads[(int)selectedKerbal.gender];

                head = personaliser.getKerbalHead(selectedKerbal, kerbalData);
                suit = personaliser.getKerbalSuit(selectedKerbal, kerbalData);

                headIndex = personaliser.KerbalHeadsDB_full.IndexOf(head);
                suitIndex = personaliser.KerbalSuitsDB_full.IndexOf(suit);
            }
            else if (selectedClass != null)
            {
                personaliser.classSuitsDB.TryGetValue(selectedClass, out suit);

                if (suit != null)
                    suitIndex = personaliser.KerbalSuitsDB_full.IndexOf(suit);
            }

            GUILayout.Space(10);
            GUILayout.BeginVertical();

            if (head != null)
            {
                GUILayout.Box(head.headTexture, GUILayout.Width(200), GUILayout.Height(200));

                GUILayout.Label(head.headName);
            }

            if (suit != null)
            {
                Texture2D suitTex = suit == defaultSuit && kerbalData != null && kerbalData.isVeteran ?
                                    defaultSuit.Suit_Iva_Veteran_Male0 : (suit.Suit_Iva_Standard_Male0 ?? defaultSuit.Suit_Iva_Standard_Male0);
                Texture2D helmetTex = suit.ivaHelmet ?? defaultSuit.ivaHelmet;
                Texture2D evaSuitTex = suit.evaSpaceSuit_Male ?? defaultSuit.evaSpaceSuit_Male;
                Texture2D evaHelmetTex = suit.evaSpaceHelmet ?? defaultSuit.evaSpaceHelmet;

                GUILayout.BeginHorizontal();
                GUILayout.Box(suitTex, GUILayout.Width(100), GUILayout.Height(100));
                GUILayout.Space(10);
                GUILayout.Box(helmetTex, GUILayout.Width(100), GUILayout.Height(100));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                GUILayout.Box(evaSuitTex, GUILayout.Width(100), GUILayout.Height(100));
                GUILayout.Space(10);
                GUILayout.Box(evaHelmetTex, GUILayout.Width(100), GUILayout.Height(100));
                GUILayout.EndHorizontal();

                GUILayout.Label(suit.suitSetName);
            }

            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(120));

            if (kerbalData != null)
            {
                GUILayout.BeginHorizontal();
                GUI.enabled = personaliser.KerbalHeadsDB_full.Count != 0;

                if (GUILayout.Button("<"))
                {
                    headIndex = headIndex == -1 ? 0 : headIndex;
                    headIndex = (personaliser.KerbalHeadsDB_full.Count + headIndex - 1) % personaliser.KerbalHeadsDB_full.Count;

                    kerbalData.head = personaliser.KerbalHeadsDB_full[headIndex];
                }
                if (GUILayout.Button(">"))
                {
                    headIndex = (headIndex + 1) % personaliser.KerbalHeadsDB_full.Count;

                    kerbalData.head = personaliser.KerbalHeadsDB_full[headIndex];
                }

                GUI.enabled = true;
                GUILayout.EndHorizontal();

                GUI.color = kerbalData.head == defaultHead ? SELECTED_COLOUR : Color.white;
                if (GUILayout.Button("Default"))
                    kerbalData.head = defaultHead;

                GUI.color = kerbalData.head == null ? SELECTED_COLOUR : Color.white;
                if (GUILayout.Button("Unset/Generic"))
                    kerbalData.head = null;

                GUI.color = Color.white;
            }

            if (kerbalData != null || selectedClass != null)
            {
                GUILayout.Space(130);

                GUILayout.BeginHorizontal();
                GUI.enabled = personaliser.KerbalSuitsDB_full.Count != 0;

                if (GUILayout.Button("<"))
                {
                    suitIndex = suitIndex == -1 ? 0 : suitIndex;
                    suitIndex = (personaliser.KerbalSuitsDB_full.Count + suitIndex - 1) % personaliser.KerbalSuitsDB_full.Count;

                    if (kerbalData != null)
                    {
                        kerbalData.suit = personaliser.KerbalSuitsDB_full[suitIndex];
                        kerbalData.cabinSuit = null;
                    }
                    else
                    {
                        personaliser.classSuitsDB[selectedClass] = personaliser.KerbalSuitsDB_full[suitIndex];
                    }
                }
                if (GUILayout.Button(">"))
                {
                    suitIndex = (suitIndex + 1) % personaliser.KerbalSuitsDB_full.Count;

                    if (kerbalData != null)
                    {
                        kerbalData.suit = personaliser.KerbalSuitsDB_full[suitIndex];
                        kerbalData.cabinSuit = null;
                    }
                    else
                    {
                        personaliser.classSuitsDB[selectedClass] = personaliser.KerbalSuitsDB_full[suitIndex];
                    }
                }

                GUI.enabled = true;
                GUILayout.EndHorizontal();

                GUI.color = suit == defaultSuit && (kerbalData == null || kerbalData.suit != null) ?
                  SELECTED_COLOUR : Color.white;

                if (GUILayout.Button("Default"))
                {
                    if (kerbalData != null)
                    {
                        kerbalData.suit = defaultSuit;
                        kerbalData.cabinSuit = null;
                    }
                    else
                    {
                        personaliser.classSuitsDB[selectedClass] = defaultSuit;
                    }
                }

                GUI.color = suit == null || (kerbalData != null && kerbalData.suit == null) ? SELECTED_COLOUR : Color.white;
                if (GUILayout.Button("Unset/Generic"))
                {
                    if (kerbalData != null)
                    {
                        kerbalData.suit = null;
                        kerbalData.cabinSuit = null;
                    }
                    else
                    {
                        personaliser.classSuitsDB[selectedClass] = null;
                    }
                }

                GUI.color = Color.white;
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            personaliser.isHelmetRemovalEnabled = GUILayout.Toggle(
              personaliser.isHelmetRemovalEnabled, "Remove helmets in safe situations");

            personaliser.isAtmSuitEnabled = GUILayout.Toggle(
              personaliser.isAtmSuitEnabled, "Spawn Kerbals in IVA suits when in breathable atmosphere");

            personaliser.isNewSuitStateEnabled = GUILayout.Toggle(
              personaliser.isNewSuitStateEnabled, "Kerbals use another EVA suit when on the ground and with no air");

            /*personaliser.isAutomaticSuitSwitchEnabled = GUILayout.Toggle(
              personaliser.isAutomaticSuitSwitchEnabled, "Use the automatic switch system ? (disable the Toggle suit)");*/

            Reflections.Type reflectionType = reflections.reflectionType;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Reflections", GUILayout.Width(120));
            reflectionType = (Reflections.Type)GUILayout.SelectionGrid((int)reflectionType, REFLECTION_TYPES, 3);
            GUILayout.EndHorizontal();

            if (reflectionType != reflections.reflectionType)
                reflections.setReflectionType(reflectionType);

            GUILayout.EndVertical();
            GUI.DragWindow(new Rect(0, 0, Screen.width, 30));
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Called when we enable (open) the GUI.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void enable()
        {
            isEnabled = true;
            selectedKerbal = null;
            selectedClass = null;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Called when we disable (close) the GUI.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void disable()
        {
            isEnabled = false;
            selectedKerbal = null;
            selectedClass = null;

            rosterScroll = Vector2.zero;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add the button to open the GUI in the toolbar when in the Space center scene
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void addAppButton()
        {
            if (appButton == null)
            {
                appButton = ApplicationLauncher.Instance.AddModApplication(
                  enable, disable, null, null, null, null, ApplicationLauncher.AppScenes.SPACECENTER, appIcon);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Remove the button to open the GUI in the toolbar when not in the Space center scene
        /// </summary>
        /// <param name="scenes"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void removeAppButton(GameScenes scenes)
        {
            if (appButton != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(appButton);
                appButton = null;
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Load the configurations at the Awake()
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void Awake()
        {
            if (isGuiEnabled)
            {
                foreach (ConfigNode node in GameDatabase.Instance.GetConfigNodes("TextureReplacerReplaced"))
                    Util.parse(node.GetValue("isGUIEnabled"), ref isGuiEnabled);

                foreach (ConfigNode node in GameDatabase.Instance.GetConfigNodes("EXPERIENCE_TRAIT"))
                {
                    string className = node.GetValue("name");
                    if (className != null)
                        classes.AddUnique(className);
                }

                appIcon = GameDatabase.Instance.GetTexture(APP_ICON_PATH, false);
                if (appIcon == null)
                    Util.log("Application icon missing: {0}", APP_ICON_PATH);

                GameEvents.onGUIApplicationLauncherReady.Add(addAppButton);
                GameEvents.onGameSceneLoadRequested.Add(removeAppButton);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add the GUI button at the Start()
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void Start()
        {
            if (ApplicationLauncher.Ready)
                addAppButton();
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Open the GUI when we push the button
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void OnGUI()
        {
            if (isEnabled)
            {
                GUI.skin = HighLogic.Skin;
                windowRect = GUILayout.Window(WINDOW_ID, windowRect, windowHandler, "TextureReplacerReplaced");
                windowRect.x = Math.Max(0, Math.Min(Screen.width - 30, windowRect.x));
                windowRect.y = Math.Max(0, Math.Min(Screen.height - 30, windowRect.y));
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Cleaning at OnDestroy()
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void OnDestroy()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(addAppButton);
            GameEvents.onGameSceneLoadRequested.Remove(removeAppButton);
        }
    }
}