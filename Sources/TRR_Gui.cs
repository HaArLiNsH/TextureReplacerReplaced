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

namespace TextureReplacerReplaced
{
    /// <summary>
    /// The configuration windows in the space center scene
    /// </summary>
    [KSPAddon(KSPAddon.Startup.FlightAndKSC, false)]
    public class TRR_Gui : MonoBehaviour
    {
        //Head_Gui head_Gui = new Head_Gui();

        private bool headGui_IsEnabled = false;

        private Rect headGui_windowRect = new Rect(60, 80, 660, 710);

        private const int WINDOW_ID_HEAD = 107057;

        private bool suitGui_IsEnabled = false;

        private Rect suitGui_windowRect = new Rect(60, 80, 1095, 710);

        private const int WINDOW_ID_SUIT = 107058;

        private Vector2 headScroll = Vector2.zero;

        private Vector2 suitScroll = Vector2.zero;

        private Vector2 headSettingScroll = Vector2.zero;

        private Vector2 suitSettingScroll = Vector2.zero;

        private Vector2 suitConfigScroll = Vector2.zero;

        private Head_Set selectedHeadSet = null;

        private Suit_Set selectedsuitSet = null;

        int stateIndex = 0;
        int levelIndex = 0;

        /// <summary>
        /// icon for the toolbar
        /// </summary>
        private static readonly string APP_ICON_PATH = Util.DIR + "Plugins/AppIcon";

        /// <summary>
        /// The 3 types of reflections : "None", "Static", "Real"
        /// </summary>
        private static readonly string[] REFLECTION_TYPES = { "None", "Static", "Real" };

        private static readonly string[] SUIT_4_CHOICES = {"IVA","EVA Ground", "EVA Space", "None"};

        private static readonly string[] SUIT_3_CHOICES = { "IVA", "EVA Ground", "EVA Space" };

        private static readonly string[] JETPACK_3_CHOICES = {  "EVA Ground", "EVA Space", "None"};

        private static readonly string[] LEVEL_CHOICES = {"Level 0", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5"};

        private static readonly string[] SUIT_STATES = {"IVA Safe : In Vehicle, Safe (landed or in orbit)", "IVA Unsafe : In Vehicle, UnSafe (flying)" ,
            "EVAground Atmo : Out Of Vehicle, On the Ground, With Atmosphere", "EVAground NoAtmo : Out Of Vehicle, On the Ground, Without Atmosphere",
        "EVAspace : Out Of Vehicle, In Space"};


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
        private Rect windowRect = new Rect(Screen.width - 590, 80, 530, 710);

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
            Randomizer randomizer = new Randomizer();

            GUIStyle imageStyle = new GUIStyle();
            imageStyle.border = new UnityEngine.RectOffset(0, 0, 0, 0);

            GUIStyle textFieldStyle = new GUIStyle();

            GUIStyle labelStyle = new GUIStyle();


            GUIStyle buttonStyle = new GUIStyle();


            int lvlCellWidth = 20;

            int colorCellWidth = 35;
            int colorCellColumwWidth = 150;
            int suitCellSize = 120;
            int textureAndColorColumnWidth = 250;
            int suitsetColumnWidth = 130;

            if (personaliser.useKspSkin)
            {
                labelStyle.font = HighLogic.Skin.font;
                labelStyle.wordWrap = false;
                labelStyle.normal.textColor = Color.white;
                labelStyle.padding.top = 5;
                labelStyle.padding.bottom = 0;
                labelStyle.padding.left = 0;
                labelStyle.padding.right = 0;
                labelStyle.fontSize = 14;

                buttonStyle = HighLogic.Skin.button;
                buttonStyle.fontSize = 14;

                textFieldStyle = HighLogic.Skin.textField;
                textFieldStyle.padding.left = 5;
                textFieldStyle.padding.right = 5;
                textFieldStyle.fontSize = 14;
                textFieldStyle.fontStyle = FontStyle.Normal;
            }
            else
            {
                labelStyle = GUI.skin.label;
                labelStyle.padding.top = 5;
                labelStyle.padding.bottom = 0;
                labelStyle.padding.left = 0;
                labelStyle.padding.right = 0;
                labelStyle.margin.top = 0;
                labelStyle.margin.bottom = 0;
                labelStyle.margin.left = 0;
                labelStyle.margin.right = 0;

                buttonStyle = GUI.skin.button;

                //textFieldStyle = GUI.skin.textField;

            }





            if (GUI.Button(new Rect(505, 5, 20, 20), "X"))
                appButton.SetFalse();

            GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();

                    GUILayout.BeginVertical(GUILayout.Width(180));

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

                                if (GUILayout.Button(kerbal.name, buttonStyle))
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

                                if (GUILayout.Button(kerbal.name, buttonStyle))
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
                                if (GUILayout.Button(clazz, buttonStyle))
                                {
                                    selectedKerbal = null;
                                    selectedClass = clazz;
                                }
                            }

                            GUI.color = Color.white;

                            GUILayout.EndVertical();
                        GUILayout.EndScrollView();

                    if (GUILayout.Button("Reset to Defaults", buttonStyle))
                        personaliser.resetKerbals();

                    GUILayout.EndVertical();

                // Textures.
                Head_Set defaultHead = personaliser.defaulMaleAndFemaleHeads[0];
                Suit_Set defaultSuit = personaliser.defaultSuit;
                KerbalData kerbalData = null;
                Head_Set head = null;
                Suit_Set suit = null;
                int headIndex = -1;           
                int suitIndex = -1;

                if (selectedKerbal != null)
                {
                    kerbalData = personaliser.getKerbalData(selectedKerbal);
                    defaultHead = personaliser.defaulMaleAndFemaleHeads[(int)selectedKerbal.gender];
                                
                    head = personaliser.getKerbalHead(selectedKerbal, kerbalData);
                    suit = personaliser.getKerbalSuit(selectedKerbal, kerbalData);
                
                    headIndex = personaliser.maleAndfemaleHeadsDB_full[(int)selectedKerbal.gender].IndexOf(head);
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
                    if (selectedKerbal != null)
                    {
                        GUILayout.Label(selectedKerbal.name);
                    }
                    //GUILayout.Space(10);

                    if (head != null)
                    {
                        GUILayout.Box(head.headTexture[0], imageStyle, GUILayout.Width(200), GUILayout.Height(200));

                        GUILayout.Label(head.name);
                    }

                    if (suit != null)
                    {
                        Texture2D suitTex = suit == defaultSuit && kerbalData != null && kerbalData.isVeteran ?
                                            defaultSuit.get_suit_Iva_Standard_Male(0) : (suit.get_suit_Iva_Standard_Male(0) ?? defaultSuit.get_suit_Iva_Standard_Male(0));
                        Texture2D helmetTex = suit.get_helmet_Iva_Standard_Male(0) ?? defaultSuit.get_helmet_Iva_Standard_Male(0);
                        Texture2D evaSuitTex = suit.get_suit_EvaSpace_Standard_Male(0) ?? defaultSuit.get_suit_EvaSpace_Standard_Male(0);
                        Texture2D evaHelmetTex = suit.get_helmet_EvaSpace_Standard_Male(0) ?? defaultSuit.get_helmet_EvaSpace_Standard_Male(0);

                            GUILayout.BeginHorizontal();
                            GUILayout.Box(suitTex, imageStyle, GUILayout.Width(100), GUILayout.Height(100));
                            GUILayout.Space(10);
                            GUILayout.Box(helmetTex, imageStyle, GUILayout.Width(100), GUILayout.Height(100));
                            GUILayout.EndHorizontal();

                            GUILayout.Space(10);

                            GUILayout.BeginHorizontal();
                            GUILayout.Box(evaSuitTex, imageStyle, GUILayout.Width(100), GUILayout.Height(100));
                            GUILayout.Space(10);
                            GUILayout.Box(evaHelmetTex, imageStyle, GUILayout.Width(100), GUILayout.Height(100));
                            GUILayout.EndHorizontal();

                        GUILayout.Label(suit.name);
                    }

                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(GUILayout.Width(80));
            GUILayout.Space(40);
            if (kerbalData != null)
                    {
                            GUILayout.BeginHorizontal();
                            GUI.enabled = personaliser.KerbalHeadsDB_full.Count != 0;

                            if (GUILayout.Button("<", buttonStyle))
                            {
                                headIndex = headIndex == -1 ? 0 : headIndex;
                                headIndex = (personaliser.maleAndfemaleHeadsDB_full[(int)selectedKerbal.gender].Count + headIndex - 1) % personaliser.maleAndfemaleHeadsDB_full[(int)selectedKerbal.gender].Count;

                                kerbalData.head = personaliser.maleAndfemaleHeadsDB_full[(int)selectedKerbal.gender][headIndex];

                                string value = "";

                                if (personaliser.KerbalAndTheirHeadsDB.TryGetValue(selectedKerbal.name, out value))
                                {
                                    personaliser.KerbalAndTheirHeadsDB[selectedKerbal.name] = kerbalData.head.name;
                                }
                                else
                                {
                                    personaliser.KerbalAndTheirHeadsDB.Add(selectedKerbal.name, kerbalData.head.name);
                                }

                            }
                            if (GUILayout.Button(">", buttonStyle))
                            {
                                headIndex = (headIndex + 1) % personaliser.maleAndfemaleHeadsDB_full[(int)selectedKerbal.gender].Count;

                                kerbalData.head = personaliser.maleAndfemaleHeadsDB_full[(int)selectedKerbal.gender][headIndex];

                                string value = "";

                                if (personaliser.KerbalAndTheirHeadsDB.TryGetValue(selectedKerbal.name, out value) )
                                {
                                    personaliser.KerbalAndTheirHeadsDB[selectedKerbal.name] = kerbalData.head.name;
                                }
                                else
                                {
                                    personaliser.KerbalAndTheirHeadsDB.Add(selectedKerbal.name, kerbalData.head.name);
                                }
                            }

                            GUI.enabled = true;
                            GUILayout.EndHorizontal();

                        GUI.color = kerbalData.head == defaultHead ? SELECTED_COLOUR : Color.white;
                        if (GUILayout.Button("Default", buttonStyle))
                        {
                            kerbalData.head = defaultHead;

                            string value = "";

                            if (personaliser.KerbalAndTheirHeadsDB.TryGetValue(selectedKerbal.name, out value))
                            {
                                personaliser.KerbalAndTheirHeadsDB[selectedKerbal.name] = kerbalData.head.name;
                            }
                            else
                            {
                                personaliser.KerbalAndTheirHeadsDB.Add(selectedKerbal.name, kerbalData.head.name);
                            }
                        }
                

                        GUI.color = kerbalData.head == null ? SELECTED_COLOUR : Color.white;
                        if (GUILayout.Button("Random", buttonStyle))
                        {
                            kerbalData.head = randomizer.randomize((int) selectedKerbal.gender);

                            string value = "";

                            if (personaliser.KerbalAndTheirHeadsDB.TryGetValue(selectedKerbal.name, out value))
                            {
                                personaliser.KerbalAndTheirHeadsDB[selectedKerbal.name] = kerbalData.head.name;
                            }
                            else
                            {
                                personaliser.KerbalAndTheirHeadsDB.Add(selectedKerbal.name, kerbalData.head.name);
                            }
                            //Util.log("{0} use this head set : {1}", selectedKerbal.name, kerbalData.head.headSetName);
                        }
                   

                        GUI.color = Color.white;
                    }

                    if (kerbalData != null || selectedClass != null)
                    {
                        GUILayout.Space(200);

                            GUILayout.BeginHorizontal();
                            GUI.enabled = personaliser.KerbalSuitsDB_full.Count != 0;

                            if (GUILayout.Button("<", buttonStyle))
                            {
                                suitIndex = suitIndex == -1 ? 0 : suitIndex;
                                suitIndex = (personaliser.KerbalSuitsDB_full.Count + suitIndex - 1) % personaliser.KerbalSuitsDB_full.Count;

                                if (kerbalData != null)
                                {
                                    kerbalData.suit = personaliser.KerbalSuitsDB_full[suitIndex];
                                    //kerbalData.cabinSuit = null;
                                }
                                else
                                {
                                    personaliser.classSuitsDB[selectedClass] = personaliser.KerbalSuitsDB_full[suitIndex];
                                }
                            }
                            if (GUILayout.Button(">", buttonStyle))
                            {
                                suitIndex = (suitIndex + 1) % personaliser.KerbalSuitsDB_full.Count;

                                if (kerbalData != null)
                                {
                                    kerbalData.suit = personaliser.KerbalSuitsDB_full[suitIndex];
                                    //kerbalData.cabinSuit = null;
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

                        if (GUILayout.Button("Default", buttonStyle))
                        {
                            if (kerbalData != null)
                            {
                                kerbalData.suit = defaultSuit;
                                //kerbalData.cabinSuit = null;
                            }
                            else
                            {
                                personaliser.classSuitsDB[selectedClass] = defaultSuit;
                            }
                        }

                        GUI.color = suit == null || (kerbalData != null && kerbalData.suit == null) ? SELECTED_COLOUR : Color.white;
                        if (GUILayout.Button("Class", buttonStyle))
                        {
                            if (kerbalData != null)
                            {
                                //kerbalData.suit = null;
                                //kerbalData.cabinSuit = null;

                                kerbalData.suit = personaliser.getClassSuit(selectedKerbal);


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

           // personaliser.isHelmetRemovalEnabled = GUILayout.Toggle(
           //   personaliser.isHelmetRemovalEnabled, "Remove helmets in safe situations");

           // personaliser.isAtmSuitEnabled = GUILayout.Toggle(
            //  personaliser.isAtmSuitEnabled, "Spawn Kerbals in IVA suits when in breathable atmosphere");

            //personaliser.isNewSuitStateEnabled = GUILayout.Toggle(
             // personaliser.isNewSuitStateEnabled, "Kerbals use another EVA suit when on the ground and with no air");

            personaliser.useKspSkin = GUILayout.Toggle(
               personaliser.useKspSkin, "Use KSP style for the GUI ?");


            /*personaliser.isAutomaticSuitSwitchEnabled = GUILayout.Toggle(
              personaliser.isAutomaticSuitSwitchEnabled, "Use the automatic switch system ? (disable the Toggle suit)");*/

            Reflections.Type reflectionType = reflections.reflectionType;

                GUILayout.BeginHorizontal();
                GUILayout.Label("Reflections", GUILayout.Width(120));

                reflectionType = (Reflections.Type)GUILayout.SelectionGrid((int)reflectionType, REFLECTION_TYPES, 3);
                GUILayout.EndHorizontal();

            if (reflectionType != reflections.reflectionType)
                reflections.setReflectionType(reflectionType);

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Heads Menu"))
            {
                headGui_IsEnabled = true;
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Suits Menu"))
            {
                suitGui_IsEnabled = true;
            }

            /*if (GUILayout.Button("Save Settings"))
            {
               
                ConfigNode scenarioNode = new ConfigNode();
                scenarioNode.name = "TRR_Scenario";
                personaliser.saveScenario(scenarioNode);

            }*/
                

            GUILayout.EndHorizontal();
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
            if (!isEnabled)
            {
                isEnabled = true;
                selectedKerbal = null;
                selectedClass = null;
            }            
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
                  enable, disable, null, null, null, null, ApplicationLauncher.AppScenes.SPACECENTER |ApplicationLauncher.AppScenes.FLIGHT, appIcon);
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
            Personaliser personaliser = Personaliser.instance;

            if (isEnabled)
            {
                if (personaliser.useKspSkin)
                {                    
                    GUI.skin = HighLogic.Skin;                   
                }
                

                windowRect = GUILayout.Window(WINDOW_ID, windowRect, windowHandler, "TextureReplacerReplaced");
                windowRect.x = Math.Max(0, Math.Min(Screen.width - 30, windowRect.x));
                windowRect.y = Math.Max(0, Math.Min(Screen.height - 30, windowRect.y));
            }

            if (headGui_IsEnabled)
            {
                if (personaliser.useKspSkin)
                    GUI.skin = HighLogic.Skin;

                headGui_windowRect = GUILayout.Window(WINDOW_ID_HEAD, headGui_windowRect, head_WindowHandler, "Heads Menu");
            }

            if (suitGui_IsEnabled)
            {
                if (personaliser.useKspSkin)
                    GUI.skin = HighLogic.Skin;

                suitGui_windowRect = GUILayout.Window(WINDOW_ID_SUIT, suitGui_windowRect, suit_WindowHandler, "Suits Menu");
            }


        }

        public void head_WindowHandler(int id)
        {
            Reflections reflections = Reflections.instance;
            Personaliser personaliser = Personaliser.instance;
            //Color32 color = new Color32(255,255,255,255);

            GUIStyle imageStyle = new GUIStyle();
            imageStyle.border = new UnityEngine.RectOffset(0,0,0,0);

            GUIStyle textFieldStyle = new GUIStyle();

            GUIStyle labelStyle = new GUIStyle();
            

            GUIStyle buttonStyle = new GUIStyle();


            int lvlCellWidth = 20;

            int colorCellWidth = 35;
            int colorCellColumwWidth = 150;
            int headCellSize = 180;
            int textureAndColorColumnWidth = 380;
            int headsetColumnWidth = 140;

            if (personaliser.useKspSkin)
            {
                labelStyle.font = HighLogic.Skin.font;
                labelStyle.wordWrap = false;
                labelStyle.normal.textColor = Color.white;
                labelStyle.padding.top = 5;
                labelStyle.padding.bottom = 0;
                labelStyle.padding.left = 0;
                labelStyle.padding.right = 0;
                labelStyle.fontSize = 14;

                buttonStyle = HighLogic.Skin.button;                
                buttonStyle.fontSize = 14;

                textFieldStyle = HighLogic.Skin.textField;
                textFieldStyle.padding.left = 5;
                textFieldStyle.padding.right = 5;
                textFieldStyle.fontSize = 14;
                textFieldStyle.fontStyle = FontStyle.Normal;
            }
            else
            {
                labelStyle = GUI.skin.label;
                labelStyle.padding.top = 5;
                labelStyle.padding.bottom = 0;
                labelStyle.padding.left = 0;
                labelStyle.padding.right = 0;
                labelStyle.margin.top = 0;
                labelStyle.margin.bottom = 0;
                labelStyle.margin.left = 0;
                labelStyle.margin.right = 0;

                buttonStyle = GUI.skin.button;

                //textFieldStyle = GUI.skin.textField;

            }

            //string[] level = { "Level 0","Level 1", "Level 2", "Level 3", "Level 4", "Level 5"};

            //GUISkin customSkin = HighLogic.Skin;
           // customSkin.label = style;


            GUILayout.BeginVertical(); // start of the Gui column
            GUILayout.BeginHorizontal(); // start of the Gui row

            if (GUI.Button(new Rect(635, 5, 20, 20), "X"))
            {
                headGui_IsEnabled = false;
                selectedHeadSet = null;
            }
                        
            GUILayout.BeginVertical(GUILayout.Width(headsetColumnWidth)); // start of head set name column
            headScroll = GUILayout.BeginScrollView(headScroll);
            GUILayout.BeginVertical();

            if (personaliser.defaulMaleAndFemaleHeads[0] != null)
            {
                if (GUILayout.Button("DEFAULT Male", buttonStyle))
                {
                    selectedHeadSet = personaliser.defaulMaleAndFemaleHeads[0];
                }
            }           

            if (personaliser.defaulMaleAndFemaleHeads[1] != null)
            {
                if (GUILayout.Button("DEFAULT Female", buttonStyle))
                {
                    selectedHeadSet = personaliser.defaulMaleAndFemaleHeads[1];
                }
            }
            

            foreach (Head_Set headSet in personaliser.KerbalHeadsDB_full)
            {
                if (GUILayout.Button(headSet.name, buttonStyle))
                {
                    selectedHeadSet = headSet;
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();

            if (GUILayout.Button("Reset All to Default"))
            {
                foreach (UrlDir.UrlConfig file in GameDatabase.Instance.GetConfigs("TextureReplacerReplaced"))
                {
                    ConfigNode headNode = file.config.GetNode("HeadSettings");
                    if (headNode != null)
                        personaliser.loadHeadConfig(headNode, personaliser.maleAndfemaleHeadsDB_full, personaliser.defaulMaleAndFemaleHeads, personaliser.maleAndfemaleHeadsDB_cleaned);
                }
            }
                
            GUILayout.EndVertical(); // end of head set name column

            // Textures.            
            Head_Set head = null;

            if (selectedHeadSet != null)
            {
                head = selectedHeadSet;
            }
            GUILayout.Space(10);

            GUILayout.BeginVertical(); // start of the main setting column
            if (head != null)
                GUILayout.Label(head.name);

            GUILayout.BeginHorizontal(); // start of the main setting row

            GUILayout.BeginVertical(GUILayout.Width(textureAndColorColumnWidth)); // start of the texture + color column

            headSettingScroll = GUILayout.BeginScrollView(headSettingScroll); 
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [0] row
            if (head != null)
            {
                GUILayout.Label("Level 0", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));                
            }
            GUILayout.BeginHorizontal(); 
            if (head != null)
            {
                GUILayout.Box(head.headTexture[0], imageStyle, GUILayout.Width(headCellSize), GUILayout.Height(headCellSize));
                
                GUILayout.BeginVertical(GUILayout.Height(headCellSize));
                    if (head != null)
                    {
                        GUILayout.Label("Left eyeball color", labelStyle);
                    }
                    GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                    if (head != null)
                    {

                        byte GUI_R = head.eyeballColor_Left[0].r;
                        byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                        head.eyeballColor_Left[0].r = GUI_R;
                        GUILayout.Label("R", labelStyle);

                        byte GUI_G = head.eyeballColor_Left[0].g;
                        byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                        head.eyeballColor_Left[0].g = GUI_G;
                        GUILayout.Label("G", labelStyle);

                        byte GUI_B = head.eyeballColor_Left[0].b;
                        byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                        head.eyeballColor_Left[0].b = GUI_B;
                        GUILayout.Label("B", labelStyle);
                    }
                    GUILayout.EndHorizontal();
                    if (head != null)
                    {
                        GUILayout.Label("Right eyeball color", labelStyle);
                    }
                    GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                    if (head != null)
                    {

                        byte GUI_R = head.eyeballColor_Right[0].r;
                        byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                        head.eyeballColor_Right[0].r = GUI_R;
                        GUILayout.Label("R", labelStyle);

                        byte GUI_G = head.eyeballColor_Right[0].g;
                        byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                        head.eyeballColor_Right[0].g = GUI_G;
                        GUILayout.Label("G", labelStyle);

                        byte GUI_B = head.eyeballColor_Right[0].b;
                        byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                        head.eyeballColor_Right[0].b = GUI_B;
                        GUILayout.Label("B", labelStyle);
                    }
                    GUILayout.EndHorizontal();
                    if (head != null)
                    {
                        GUILayout.Label("Left pupil color", labelStyle);
                    }
                    GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                    if (head != null)
                    {

                        byte GUI_R = head.pupilColor_Left[0].r;
                        byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                        head.pupilColor_Left[0].r = GUI_R;
                        GUILayout.Label("R", labelStyle);

                        byte GUI_G = head.pupilColor_Left[0].g;
                        byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                        head.pupilColor_Left[0].g = GUI_G;
                        GUILayout.Label("G", labelStyle);

                        byte GUI_B = head.pupilColor_Left[0].b;
                        byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                        head.pupilColor_Left[0].b = GUI_B;
                        GUILayout.Label("B", labelStyle);
                    }
                    GUILayout.EndHorizontal();
                    if (head != null)
                    {
                        GUILayout.Label("Right pupil color", labelStyle);
                    }
                    GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                    if (head != null)
                    {

                        byte GUI_R = head.pupilColor_Right[0].r;
                        byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                        head.pupilColor_Right[0].r = GUI_R;
                        GUILayout.Label("R", labelStyle);

                        byte GUI_G = head.pupilColor_Right[0].g;
                        byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                        head.pupilColor_Right[0].g = GUI_G;
                        GUILayout.Label("G", labelStyle);

                        byte GUI_B = head.pupilColor_Right[0].b;
                        byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                        head.pupilColor_Right[0].b = GUI_B;
                        GUILayout.Label("B", labelStyle);
                    }
                    GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [0] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [1] row
            if (head != null)
            {
                GUILayout.Label("Level 1", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (head != null)
            {
                GUILayout.Box(head.headTexture[1], imageStyle, GUILayout.Width(headCellSize), GUILayout.Height(headCellSize));
                GUILayout.BeginVertical(GUILayout.Height(headCellSize));
                if (head != null)
                {
                    GUILayout.Label("Left eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Left[1].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Left[1].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Left[1].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Left[1].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Left[1].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Left[1].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Right[1].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Right[1].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Right[1].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Right[1].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Right[1].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Right[1].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Left pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Left[1].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Left[1].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Left[1].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Left[1].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Left[1].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Left[1].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Right[1].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Right[1].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Right[1].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Right[1].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Right[1].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Right[1].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [1] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [2] row
            if (head != null)
            {
                GUILayout.Label("Level 2", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (head != null)
            {
                GUILayout.Box(head.headTexture[2], imageStyle, GUILayout.Width(headCellSize), GUILayout.Height(headCellSize));
                GUILayout.BeginVertical(GUILayout.Height(headCellSize));
                if (head != null)
                {
                    GUILayout.Label("Left eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Left[2].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Left[2].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Left[2].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Left[2].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Left[2].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Left[2].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Right[2].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Right[2].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Right[2].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Right[2].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Right[2].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Right[2].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Left pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Left[2].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Left[2].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Left[2].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Left[2].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Left[2].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Left[2].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Right[2].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Right[2].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Right[2].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Right[2].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Right[2].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Right[2].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [2] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [3] row
            if (head != null)
            {
                GUILayout.Label("Level 3", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (head != null)
            {
                GUILayout.Box(head.headTexture[3], imageStyle, GUILayout.Width(headCellSize), GUILayout.Height(headCellSize));
                GUILayout.BeginVertical(GUILayout.Height(headCellSize));
                if (head != null)
                {
                    GUILayout.Label("Left eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Left[3].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Left[3].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Left[3].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Left[3].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Left[3].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Left[3].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Right[3].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Right[3].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Right[3].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Right[3].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Right[3].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Right[3].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Left pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Left[3].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Left[3].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Left[3].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Left[3].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Left[3].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Left[3].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Right[3].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Right[3].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Right[3].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Right[3].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Right[3].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Right[3].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [3] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [4] row
            if (head != null)
            {
                GUILayout.Label("Level 4", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (head != null)
            {
                GUILayout.Box(head.headTexture[4], imageStyle, GUILayout.Width(headCellSize), GUILayout.Height(headCellSize));
                GUILayout.BeginVertical(GUILayout.Height(headCellSize));
                if (head != null)
                {
                    GUILayout.Label("Left eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Left[4].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Left[4].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Left[4].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Left[4].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Left[4].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Left[4].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Right[4].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Right[4].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Right[4].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Right[4].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Right[4].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Right[4].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Left pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Left[4].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Left[4].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Left[4].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Left[4].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Left[4].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Left[4].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Right[4].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Right[4].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Right[4].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Right[4].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Right[4].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Right[4].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [4] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [5] row
            if (head != null)
            {
                GUILayout.Label("Level 5", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (head != null)
            {
                GUILayout.Box(head.headTexture[5], imageStyle, GUILayout.Width(headCellSize), GUILayout.Height(headCellSize));
                GUILayout.BeginVertical(GUILayout.Height(headCellSize));
                if (head != null)
                {
                    GUILayout.Label("Left eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Left[5].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Left[5].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Left[5].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Left[5].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Left[5].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Left[5].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right eyeball color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.eyeballColor_Right[5].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.eyeballColor_Right[5].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.eyeballColor_Right[5].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.eyeballColor_Right[5].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.eyeballColor_Right[5].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.eyeballColor_Right[5].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Left pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Left[5].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Left[5].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Left[5].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Left[5].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Left[5].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Left[5].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                if (head != null)
                {
                    GUILayout.Label("Right pupil color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (head != null)
                {

                    byte GUI_R = head.pupilColor_Right[5].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    head.pupilColor_Right[5].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = head.pupilColor_Right[5].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    head.pupilColor_Right[5].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = head.pupilColor_Right[5].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    head.pupilColor_Right[5].b = GUI_B;
                    GUILayout.Label("B", labelStyle);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [5] row

            GUILayout.Space(10);

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.EndVertical(); // end of the texture + color column

            GUILayout.BeginVertical(); // start of the setting column
            if (head != null)
            {
                head.isExclusive = GUILayout.Toggle(head.isExclusive, "Exclusive");

                GUILayout.Space(20);

                GUILayout.Label("Level to start");
                GUILayout.Label("hiding element");
                GUILayout.Space(5);
                GUILayout.Label("Enter 6 or more");
                GUILayout.Label("to cancel");
            }
            

                GUILayout.BeginHorizontal();
                if (head != null)
                {
                    int GUI_lvlToHide_Eye_Left = head.lvlToHide_Eye_Left;
                    int.TryParse(GUILayout.TextField(GUI_lvlToHide_Eye_Left.ToString(), 2, GUILayout.MaxWidth(lvlCellWidth)), out GUI_lvlToHide_Eye_Left);

                    head.lvlToHide_Eye_Left = GUI_lvlToHide_Eye_Left;
                    GUILayout.Label("Left eyeball", labelStyle);
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                if (head != null)
                {
                    int GUI_lvlToHide_Eye_Right = head.lvlToHide_Eye_Right;
                    int.TryParse(GUILayout.TextField(GUI_lvlToHide_Eye_Right.ToString(), 2, GUILayout.MaxWidth(lvlCellWidth)), out GUI_lvlToHide_Eye_Right);

                    head.lvlToHide_Eye_Right = GUI_lvlToHide_Eye_Right;
                    GUILayout.Label("Right eyeball", labelStyle);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (head != null)
                {
                    int GUI_lvlToHide_Pupil_Left = head.lvlToHide_Pupil_Left;
                    int.TryParse(GUILayout.TextField(GUI_lvlToHide_Pupil_Left.ToString(), 2, GUILayout.MaxWidth(lvlCellWidth)), out GUI_lvlToHide_Pupil_Left);

                    head.lvlToHide_Pupil_Left = GUI_lvlToHide_Pupil_Left;
                    GUILayout.Label("Left pupil", labelStyle);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (head != null)
                {
                    int GUI_lvlToHide_Pupil_Right = head.lvlToHide_Pupil_Right;
                    int.TryParse(GUILayout.TextField(GUI_lvlToHide_Pupil_Right.ToString(), 2, GUILayout.MaxWidth(lvlCellWidth)), out GUI_lvlToHide_Pupil_Right);

                    head.lvlToHide_Pupil_Right = GUI_lvlToHide_Pupil_Right;
                    GUILayout.Label("Right pupil", labelStyle);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (head != null)
                {
                    int GUI_lvlToHide_TeethUp = head.lvlToHide_TeethUp;
                    int.TryParse(GUILayout.TextField(GUI_lvlToHide_TeethUp.ToString(), 2, GUILayout.MaxWidth(lvlCellWidth)), out GUI_lvlToHide_TeethUp);

                    head.lvlToHide_TeethUp = GUI_lvlToHide_TeethUp;
                    GUILayout.Label("Up teeth", labelStyle);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (head != null)
                {
                    int GUI_lvlToHide_TeethDown = head.lvlToHide_TeethDown;
                    int.TryParse(GUILayout.TextField(GUI_lvlToHide_TeethDown.ToString(), 2, GUILayout.MaxWidth(lvlCellWidth)), out GUI_lvlToHide_TeethDown);

                    head.lvlToHide_TeethDown = GUI_lvlToHide_TeethDown;
                    GUILayout.Label("Down teeth", labelStyle);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (head != null)
                {
                    int GUI_lvlToHide_Ponytail = head.lvlToHide_Ponytail;
                    int.TryParse(GUILayout.TextField(GUI_lvlToHide_Ponytail.ToString(), 2, GUILayout.MaxWidth(lvlCellWidth)), out GUI_lvlToHide_Ponytail);

                    head.lvlToHide_Ponytail = GUI_lvlToHide_Ponytail;
                    GUILayout.Label("Ponytail", labelStyle);
                }
                GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (head != null)
            {
                if (GUILayout.Button("Default", GUILayout.Width(100)))
                    personaliser.resetHead(head, personaliser.defaulMaleAndFemaleHeads);
                //Util.log("Clicked Button");
            }

            /*GUILayout.Space(10);

            if (head != null)
            {
                if (GUILayout.Button("Save Settings", GUILayout.Width(100)))
                {
                    ConfigNode scenarioNode = new ConfigNode();
                    scenarioNode.name = "TRR_Scenario";
                    personaliser.saveScenario(scenarioNode);
                }
            }*/

            GUILayout.EndVertical(); // end of the setting column

            GUILayout.EndHorizontal(); // end of the main setting row
            GUILayout.EndVertical(); // end of the main setting column

            GUILayout.EndHorizontal();// end of the Gui row
            GUILayout.EndVertical();// end of the Gui column
            

            GUI.DragWindow(new Rect(0, 0, Screen.width, 30));
        }

        public void suit_WindowHandler(int id)
        {
            Reflections reflections = Reflections.instance;
            Personaliser personaliser = Personaliser.instance;

            GUIStyle imageStyle = new GUIStyle();
            imageStyle.border = new UnityEngine.RectOffset(0, 0, 0, 0);

            GUIStyle textFieldStyle = new GUIStyle();

            GUIStyle labelStyle = new GUIStyle();


            GUIStyle buttonStyle = new GUIStyle();


            int selectionButtonWidth = 40;

            int colorCellWidth = 35;
            int colorCellColumwWidth = 150;
            int suitCellSize = 120;
            int suitImgSize = 150;
            int textureAndColorColumnWidth = 230;
            int suitsetColumnWidth = 130;
            int suitConfigWidth = 375;

            if (personaliser.useKspSkin)
            {
                labelStyle.font = HighLogic.Skin.font;
                labelStyle.wordWrap = false;
                labelStyle.normal.textColor = Color.white;
                labelStyle.padding.top = 5;
                labelStyle.padding.bottom = 0;
                labelStyle.padding.left = 0;
                labelStyle.padding.right = 0;
                labelStyle.fontSize = 14;

                buttonStyle = HighLogic.Skin.button;
                buttonStyle.fontSize = 14;

                textFieldStyle = HighLogic.Skin.textField;
                textFieldStyle.padding.left = 5;
                textFieldStyle.padding.right = 5;
                textFieldStyle.fontSize = 14;
                textFieldStyle.fontStyle = FontStyle.Normal;
            }
            else
            {
                labelStyle = GUI.skin.label;
                labelStyle.padding.top = 5;
                labelStyle.padding.bottom = 0;
                labelStyle.padding.left = 0;
                labelStyle.padding.right = 0;
                labelStyle.margin.top = 0;
                labelStyle.margin.bottom = 0;
                labelStyle.margin.left = 0;
                labelStyle.margin.right = 0;

                buttonStyle = GUI.skin.button;

                //textFieldStyle = GUI.skin.textField;

            }

            GUILayout.BeginVertical(); // start of the Gui column
            GUILayout.BeginHorizontal(); // start of the Gui row

            if (GUI.Button(new Rect(1070, 5, 20, 20), "X"))
            {
                suitGui_IsEnabled = false;
                selectedsuitSet = null;
            }

            GUILayout.BeginVertical(GUILayout.Width(suitsetColumnWidth)); // start of suit set name column
            suitScroll = GUILayout.BeginScrollView(suitScroll);
            GUILayout.BeginVertical();

            if (personaliser.defaultSuit != null)
            {
                if (GUILayout.Button(personaliser.defaultSuit.name, buttonStyle))
                {
                    selectedsuitSet = personaliser.defaultSuit;
                }
            }

            

            foreach (Suit_Set suitSet in personaliser.KerbalSuitsDB_full)
            {
                if (GUILayout.Button(suitSet.name, buttonStyle))
                {
                    selectedsuitSet = suitSet;
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();

            if (GUILayout.Button("Reset all to Defaults"))
            {
                foreach (UrlDir.UrlConfig file in GameDatabase.Instance.GetConfigs("TextureReplacerReplaced"))
                {
                    ConfigNode suitNode = file.config.GetNode("SuitSettings");
                    if (suitNode != null)
                        personaliser.loadSuitConfig(suitNode, personaliser.KerbalSuitsDB_full, personaliser.defaultSuit, false);
                }
            }
                
            GUILayout.EndVertical(); // end of suit set name column

            Suit_Set suit = null;

            if (selectedsuitSet != null)
            {
                suit = selectedsuitSet;
            }
            GUILayout.Space(10);

            GUILayout.BeginVertical(); // start of the main setting column
            if (suit != null)
                GUILayout.Label(suit.name);

            GUILayout.BeginHorizontal(); // start of the main setting row

            GUILayout.BeginVertical(GUILayout.Width(textureAndColorColumnWidth)); // start of the texture + color column
            suitSettingScroll = GUILayout.BeginScrollView(suitSettingScroll);
            GUILayout.BeginVertical();
            GUILayout.Space(10);

            GUILayout.BeginVertical();// start of the lvl [0] row
            if (suit != null)
            {
                GUILayout.Label("Level 0", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (suit != null)
            {
                //GUILayout.Box(suit.suitTexture[0], imageStyle, GUILayout.Width(suitCellSize), GUILayout.Height(suitCellSize));

                GUILayout.BeginVertical(GUILayout.Height(suitCellSize));
                if (suit != null)
                {
                    GUILayout.Label("Iva Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_Iva_ReflectionColor[0].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_Iva_ReflectionColor[0].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_Iva_ReflectionColor[0].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_Iva_ReflectionColor[0].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_Iva_ReflectionColor[0].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_Iva_ReflectionColor[0].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[0].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[0].b = GUI_B;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Ground Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaGround_ReflectionColor[0].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaGround_ReflectionColor[0].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaGround_ReflectionColor[0].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaGround_ReflectionColor[0].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaGround_ReflectionColor[0].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaGround_ReflectionColor[0].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[0].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[0].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Space Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaSpace_ReflectionColor[0].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaSpace_ReflectionColor[0].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaSpace_ReflectionColor[0].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaSpace_ReflectionColor[0].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaSpace_ReflectionColor[0].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaSpace_ReflectionColor[0].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_EvaSpace_ReflectionColor[0].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_EvaSpace_ReflectionColor[0].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [0] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [1] row
            if (suit != null)
            {
                GUILayout.Label("Level 1", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (suit != null)
            {
                //GUILayout.Box(suit.suitTexture[1], imageStyle, GUILayout.Width(suitCellSize), GUILayout.Height(suitCellSize));

                GUILayout.BeginVertical(GUILayout.Height(suitCellSize));
                if (suit != null)
                {
                    GUILayout.Label("Iva Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_Iva_ReflectionColor[1].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_Iva_ReflectionColor[1].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_Iva_ReflectionColor[1].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_Iva_ReflectionColor[1].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_Iva_ReflectionColor[1].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_Iva_ReflectionColor[1].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[1].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[1].b = GUI_B;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Ground Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaGround_ReflectionColor[1].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaGround_ReflectionColor[1].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaGround_ReflectionColor[1].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaGround_ReflectionColor[1].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaGround_ReflectionColor[1].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaGround_ReflectionColor[1].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[1].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[1].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Space Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaSpace_ReflectionColor[1].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaSpace_ReflectionColor[1].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaSpace_ReflectionColor[1].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaSpace_ReflectionColor[1].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaSpace_ReflectionColor[1].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaSpace_ReflectionColor[1].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_EvaSpace_ReflectionColor[1].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_EvaSpace_ReflectionColor[1].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [1] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [2] row
            if (suit != null)
            {
                GUILayout.Label("Level 2", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (suit != null)
            {
                //GUILayout.Box(suit.suitTexture[2], imageStyle, GUILayout.Width(suitCellSize), GUILayout.Height(suitCellSize));

                GUILayout.BeginVertical(GUILayout.Height(suitCellSize));
                if (suit != null)
                {
                    GUILayout.Label("Iva Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_Iva_ReflectionColor[2].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_Iva_ReflectionColor[2].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_Iva_ReflectionColor[2].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_Iva_ReflectionColor[2].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_Iva_ReflectionColor[2].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_Iva_ReflectionColor[2].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[2].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[2].b = GUI_B;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Ground Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaGround_ReflectionColor[2].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaGround_ReflectionColor[2].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaGround_ReflectionColor[2].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaGround_ReflectionColor[2].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaGround_ReflectionColor[2].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaGround_ReflectionColor[2].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[2].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[2].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Space Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaSpace_ReflectionColor[2].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaSpace_ReflectionColor[2].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaSpace_ReflectionColor[2].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaSpace_ReflectionColor[2].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaSpace_ReflectionColor[2].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaSpace_ReflectionColor[2].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_EvaSpace_ReflectionColor[2].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_EvaSpace_ReflectionColor[2].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [2] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [3] row
            if (suit != null)
            {
                GUILayout.Label("Level 3", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (suit != null)
            {
                //GUILayout.Box(suit.suitTexture[3], imageStyle, GUILayout.Width(suitCellSize), GUILayout.Height(suitCellSize));

                GUILayout.BeginVertical(GUILayout.Height(suitCellSize));
                if (suit != null)
                {
                    GUILayout.Label("Iva Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_Iva_ReflectionColor[3].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_Iva_ReflectionColor[3].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_Iva_ReflectionColor[3].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_Iva_ReflectionColor[3].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_Iva_ReflectionColor[3].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_Iva_ReflectionColor[3].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[3].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[3].b = GUI_B;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Ground Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaGround_ReflectionColor[3].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaGround_ReflectionColor[3].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaGround_ReflectionColor[3].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaGround_ReflectionColor[3].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaGround_ReflectionColor[3].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaGround_ReflectionColor[3].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[3].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[3].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Space Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaSpace_ReflectionColor[3].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaSpace_ReflectionColor[3].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaSpace_ReflectionColor[3].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaSpace_ReflectionColor[3].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaSpace_ReflectionColor[3].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaSpace_ReflectionColor[3].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_EvaSpace_ReflectionColor[3].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_EvaSpace_ReflectionColor[3].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [3] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [4] row
            if (suit != null)
            {
                GUILayout.Label("Level 4", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (suit != null)
            {
                //GUILayout.Box(suit.suitTexture[4], imageStyle, GUILayout.Width(suitCellSize), GUILayout.Height(suitCellSize));

                GUILayout.BeginVertical(GUILayout.Height(suitCellSize));
                if (suit != null)
                {
                    GUILayout.Label("Iva Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_Iva_ReflectionColor[4].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_Iva_ReflectionColor[4].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_Iva_ReflectionColor[4].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_Iva_ReflectionColor[4].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_Iva_ReflectionColor[4].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_Iva_ReflectionColor[4].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[4].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[4].b = GUI_B;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Ground Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaGround_ReflectionColor[4].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaGround_ReflectionColor[4].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaGround_ReflectionColor[4].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaGround_ReflectionColor[4].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaGround_ReflectionColor[4].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaGround_ReflectionColor[4].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[4].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[4].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Space Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaSpace_ReflectionColor[4].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaSpace_ReflectionColor[4].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaSpace_ReflectionColor[4].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaSpace_ReflectionColor[4].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaSpace_ReflectionColor[4].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaSpace_ReflectionColor[4].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_EvaSpace_ReflectionColor[4].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_EvaSpace_ReflectionColor[4].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [4] row
            GUILayout.Space(10);
            GUILayout.BeginVertical();// start of the lvl [5] row
            if (suit != null)
            {
                GUILayout.Label("Level 5", GUILayout.Width(colorCellColumwWidth), GUILayout.Height(20));
            }
            GUILayout.BeginHorizontal();
            if (suit != null)
            {
                //GUILayout.Box(suit.suitTexture[5], imageStyle, GUILayout.Width(suitCellSize), GUILayout.Height(suitCellSize));

                GUILayout.BeginVertical(GUILayout.Height(suitCellSize));
                if (suit != null)
                {
                    GUILayout.Label("Iva Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_Iva_ReflectionColor[5].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_Iva_ReflectionColor[5].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_Iva_ReflectionColor[5].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_Iva_ReflectionColor[5].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_Iva_ReflectionColor[5].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_Iva_ReflectionColor[5].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[5].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[5].b = GUI_B;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Ground Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaGround_ReflectionColor[5].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaGround_ReflectionColor[5].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaGround_ReflectionColor[5].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaGround_ReflectionColor[5].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaGround_ReflectionColor[5].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaGround_ReflectionColor[5].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_Iva_ReflectionColor[5].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_Iva_ReflectionColor[5].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();
                if (suit != null)
                {
                    GUILayout.Label("Eva Space Visor Reflection color", labelStyle);
                }
                GUILayout.BeginHorizontal(GUILayout.Width(colorCellColumwWidth));
                if (suit != null)
                {

                    byte GUI_R = suit.visor_EvaSpace_ReflectionColor[5].r;
                    byte.TryParse(GUILayout.TextField(GUI_R.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_R);
                    suit.visor_EvaSpace_ReflectionColor[5].r = GUI_R;
                    GUILayout.Label("R", labelStyle);

                    byte GUI_G = suit.visor_EvaSpace_ReflectionColor[5].g;
                    byte.TryParse(GUILayout.TextField(GUI_G.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_G);
                    suit.visor_EvaSpace_ReflectionColor[5].g = GUI_G;
                    GUILayout.Label("G", labelStyle);

                    byte GUI_B = suit.visor_EvaSpace_ReflectionColor[5].b;
                    byte.TryParse(GUILayout.TextField(GUI_B.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_B);
                    suit.visor_EvaSpace_ReflectionColor[5].b = GUI_B;
                    GUILayout.Label("B", labelStyle);

                    /*byte GUI_A = suit.visor_EvaSpace_ReflectionColor[5].a;
                    byte.TryParse(GUILayout.TextField(GUI_A.ToString(), 3, GUILayout.Width(colorCellWidth)), out GUI_A);
                    suit.visor_EvaSpace_ReflectionColor[5].a = GUI_A;
                    GUILayout.Label("A", labelStyle);*/
                }
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();// end of the lvl [5] row

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.EndVertical(); // end of the texture + color column

            /////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////
            // SECOND COLUMN
            /////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////

            GUILayout.BeginVertical(); // start of the setting column
            if (suit != null)
            {
                
                suit.isExclusive = GUILayout.Toggle(suit.isExclusive, "Exclusive");

                if (GUILayout.Button("Reset to Default", GUILayout.Width(100)))
                    personaliser.resetSuit(suit, personaliser.defaultSuit);
                

                GUILayout.Label("Choose your texture for the situation :");

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("<", buttonStyle, GUILayout.Width(selectionButtonWidth)))
                {
                    if (stateIndex > 0)
                        stateIndex--;
                }                
                if (GUILayout.Button(">", buttonStyle, GUILayout.Width(selectionButtonWidth)))
                {
                    if (stateIndex < 4)
                        stateIndex++;
                }
                GUILayout.Label(SUIT_STATES[stateIndex]);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("<", buttonStyle, GUILayout.Width(selectionButtonWidth)))
                {
                    if (levelIndex > 0)
                        levelIndex--;                  
                }                                
                if (GUILayout.Button(">", buttonStyle, GUILayout.Width(selectionButtonWidth)))
                {
                    if (levelIndex < 5)
                        levelIndex++;
                }
                GUILayout.Label(LEVEL_CHOICES[levelIndex]);
                GUILayout.EndHorizontal();

                suitConfigScroll = GUILayout.BeginScrollView(suitConfigScroll); // start of the suits config scroll
                GUILayout.BeginVertical();
                
                switch (stateIndex)
                {
                case 0: // IVA Safe
                        GUILayout.BeginVertical(); // start of the IVA_safe
                        //GUILayout.Label("In Vehicle and Safe (non flying or in orbit)");

                        GUILayout.BeginVertical();// start of the suit selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Suit :");
                        suit.suit_Iva_Safe = GUILayout.SelectionGrid(suit.suit_Iva_Safe, SUIT_3_CHOICES, 3);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.suit_Iva_Safe)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the suit selection

                        GUILayout.BeginVertical();// start of the helmet selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Helmet :");
                        suit.helmet_Iva_Safe = GUILayout.SelectionGrid(suit.helmet_Iva_Safe, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.helmet_Iva_Safe)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the helmet selection

                        GUILayout.BeginVertical();// start of the visor selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Visor :");
                        suit.visor_Iva_Safe = GUILayout.SelectionGrid(suit.visor_Iva_Safe, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.visor_Iva_Safe)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the visor selection

                        GUILayout.EndVertical(); // end of the IVA_safe
                        break;

                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////

                    case 1:
                        GUILayout.BeginVertical(); // start of the IVA_Unsafe
                        //GUILayout.Label("In Vehicle and Unsafe (flying) :");

                        GUILayout.BeginVertical();// start of the suit selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Suit :");
                        suit.suit_Iva_Unsafe = GUILayout.SelectionGrid(suit.suit_Iva_Unsafe, SUIT_3_CHOICES, 3);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.suit_Iva_Unsafe)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the suit selection

                        GUILayout.BeginVertical();// start of the helmet selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Helmet :");
                        suit.helmet_Iva_Unsafe = GUILayout.SelectionGrid(suit.helmet_Iva_Unsafe, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.helmet_Iva_Unsafe)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the helmet selection

                        GUILayout.BeginVertical();// start of the visor selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Visor :");
                        suit.visor_Iva_Unsafe = GUILayout.SelectionGrid(suit.visor_Iva_Unsafe, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.visor_Iva_Unsafe)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the visor selection
                        GUILayout.EndVertical(); // end of the IVA_Unsafe
                        break;

                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////

                    case 2:
                        GUILayout.BeginVertical(); // start of the EVAground_Atmo
                        //GUILayout.Label("Out of Vehicle, On the Ground, With Atmosphere :");

                        GUILayout.BeginVertical();// start of the suit selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Suit :");
                        suit.suit_EvaGround_Atmo = GUILayout.SelectionGrid(suit.suit_EvaGround_Atmo, SUIT_3_CHOICES, 3);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.suit_EvaGround_Atmo)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the suit selection

                        GUILayout.BeginVertical();// start of the helmet selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Helmet :");
                        suit.helmet_EvaGround_Atmo = GUILayout.SelectionGrid(suit.helmet_EvaGround_Atmo, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.helmet_EvaGround_Atmo)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the helmet selection

                        GUILayout.BeginVertical();// start of the visor selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Visor :");
                        suit.visor_EvaGround_Atmo = GUILayout.SelectionGrid(suit.visor_EvaGround_Atmo, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.visor_EvaGround_Atmo)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 3:
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the visor selection

                        GUILayout.BeginVertical();// start of the jetpack selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Jetpack :");
                        suit.jetpack_EvaGround_Atmo = GUILayout.SelectionGrid(suit.jetpack_EvaGround_Atmo, JETPACK_3_CHOICES, 3);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.jetpack_EvaGround_Atmo)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:

                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the jetpack selection
                        GUILayout.EndVertical(); // end of the EVAground_Atmo
                        break;

                        /////////////////////////////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////////////////////

                    case 3:
                        GUILayout.BeginVertical(); // start of the EVAground_NOAtmo

                        GUILayout.BeginVertical();// start of the suit selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Suit :");
                        suit.suit_EvaGround_NoAtmo = GUILayout.SelectionGrid(suit.suit_EvaGround_NoAtmo, SUIT_3_CHOICES, 3);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.suit_EvaGround_NoAtmo)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the suit selection

                        GUILayout.BeginVertical();// start of the helmet selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Helmet :");
                        suit.helmet_EvaGround_NoAtmo = GUILayout.SelectionGrid(suit.helmet_EvaGround_NoAtmo, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.helmet_EvaGround_NoAtmo)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the helmet selection

                        GUILayout.BeginVertical();// start of the visor selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Visor :");
                        suit.visor_EvaGround_NoAtmo = GUILayout.SelectionGrid(suit.visor_EvaGround_NoAtmo, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.visor_EvaGround_NoAtmo)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 3:
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the visor selection

                        GUILayout.BeginVertical();// start of the jetpack selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Jetpack :");
                        suit.jetpack_EvaGround_NoAtmo = GUILayout.SelectionGrid(suit.jetpack_EvaGround_NoAtmo, JETPACK_3_CHOICES, 3);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.jetpack_EvaGround_NoAtmo)
                        {
                            
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:

                                break;

                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the jetpack selection
                        GUILayout.EndVertical(); // end of the EVAground_NOAtmo
                        break;

                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////

                    case 4:
                        GUILayout.BeginVertical(); // start of the EVAspace
                        GUILayout.BeginVertical();// start of the suit selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Suit :");
                        suit.suit_EvaSpace = GUILayout.SelectionGrid(suit.suit_EvaSpace, SUIT_3_CHOICES, 3);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.suit_EvaSpace)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_suit_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the suit selection

                        GUILayout.BeginVertical();// start of the helmet selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Helmet :");
                        suit.helmet_EvaSpace = GUILayout.SelectionGrid(suit.helmet_EvaSpace, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.helmet_EvaSpace)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_helmet_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the helmet selection

                        GUILayout.BeginVertical();// start of the visor selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Visor :");
                        suit.visor_EvaSpace = GUILayout.SelectionGrid(suit.visor_EvaSpace, SUIT_4_CHOICES, 4);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.visor_EvaSpace)
                        {
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_Iva_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_visor_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 3:
                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the visor selection

                        GUILayout.BeginVertical();// start of the jetpack selection 
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Jetpack :");
                        suit.jetpack_EvaSpace = GUILayout.SelectionGrid(suit.jetpack_EvaSpace, JETPACK_3_CHOICES, 3);
                        GUILayout.EndHorizontal();
                        GUILayout.BeginVertical();

                        switch (suit.jetpack_EvaSpace)
                        {                           
                            case 0:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaGround_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 1:
                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_VetBad_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Veteran_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Badass_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Standard_Female(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Female standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();

                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_VetBad_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Veteran_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Veteran", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Badass_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male Badass", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                GUILayout.Box(suit.get_jetpack_EvaSpace_Standard_Male(levelIndex), imageStyle, GUILayout.Width(suitImgSize), GUILayout.Height(suitImgSize));
                                GUILayout.Label("Male standard", labelStyle);
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                break;

                            case 2:

                                break;
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndVertical();// end of the jetpack selection
                        GUILayout.EndVertical(); // end of the EVAspace
                        break;

                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////
                }
                
                GUILayout.EndVertical();
                GUILayout.EndScrollView();// end of the suits config scroll

            }
            GUILayout.EndVertical(); // end of the setting column


            GUILayout.EndHorizontal(); // end of the main setting row
            GUILayout.EndVertical(); // end of the main setting column


            GUILayout.EndHorizontal();// end of the Gui row
            GUILayout.EndVertical();// end of the Gui column

            //if (GUILayout.Button("I am pouet"))
            // Util.log("Clicked Button");

            GUI.DragWindow(new Rect(0, 0, Screen.width, 30));
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