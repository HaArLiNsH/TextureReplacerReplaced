/*
 * Copyright © 2017 HaArLiNsH 
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
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextureReplacerReplaced
{
    public class Head_Gui : MonoBehaviour
    {
        public Texture2D icon;

        /// <summary>
        /// Ui window size
        /// </summary>
        private Rect windowRect = new Rect(Screen.width - 600, 60, 580, 610);

        /// <summary>
        /// check to open or close the GUI
        /// </summary>
        private bool isEnabled = false;

        /// <summary>
        /// unique ID of the window of the GUI
        /// </summary>
        private const int WINDOW_ID = 107058;

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Load the configurations at the Awake()
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void Awake()
        {

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add the GUI button at the Start()
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public void Start()
        {

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Called when we enable (open) the GUI.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void enable()
        {
            isEnabled = true;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Called when we disable (close) the GUI.
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void disable()
        {
            isEnabled = false;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Open the GUI when we push the button
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        void OnGUI()
        {
            // Make a background box
           // GUI.Box(new Rect(10, 10, 100, 90), "Loader Menu");

           // GUI.Button(new Rect(10, 10, 100, 20), new GUIContent("Click me", icon, "This is the tooltip"));
           // GUI.Label(new Rect(10, 40, 100, 20), GUI.tooltip);

            if (isEnabled)
            {
                GUI.skin = HighLogic.Skin;
                windowRect = GUILayout.Window(WINDOW_ID, windowRect, windowHandler, "Heads Menu");
                windowRect.x = Math.Max(0, Math.Min(Screen.width - 30, windowRect.x));
                windowRect.y = Math.Max(0, Math.Min(Screen.height - 30, windowRect.y));
            }

            }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The method that populate the GUI window
        /// </summary>
        /// <param name="id"></param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void windowHandler(int id)
        {

        }

    }
}
