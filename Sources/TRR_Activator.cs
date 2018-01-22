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

using UnityEngine;

namespace TextureReplacerReplaced
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class TRR_Activator : MonoBehaviour
    {
        /// <summary>
        /// status check for in flight situation
        /// </summary>
        private bool hasFlightHandlers = false;

        /// <summary>
        /// the object to call the reflection update
        /// </summary>
        private TRReflectionUpdater reflectionUpdater = null;

        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Reflection updater. We don't want this to run every frame unless real reflections are enabled
        /// so it's wrapped inside another component and enabled only when needed.
        /// </summary>
        /// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private class TRReflectionUpdater : MonoBehaviour
        {
            public void Update()
            {
                //Util.log("++++ 'Update()' ++++");
                Reflections.Script.updateScripts();
            }
        }        

        /// ****************************************************************************************
        /// <summary>
        /// Start() >
        /// </summary>
        /// ****************************************************************************************
        public void Start()
        {
            //Util.log("++++ 'Start()' ++++");

            if (!TextureReplacerReplaced.isLoaded)
                return;

            Replacer.instance.beginScene();

            if (HighLogic.LoadedSceneIsFlight)
            {
                Replacer.instance.beginFlight();
                Personaliser.instance.beginFlight();

                hasFlightHandlers = true;
            }

            if ((HighLogic.LoadedSceneIsFlight || HighLogic.LoadedSceneIsEditor)
                && Reflections.instance.reflectionType == Reflections.Type.REAL)
            {
                reflectionUpdater = gameObject.AddComponent<TRReflectionUpdater>();
            }
        }

        /// ****************************************************************************************
        /// <summary>
        /// OnDestroy() >
        /// </summary>
        /// ****************************************************************************************
        public void OnDestroy()
        {
            if (hasFlightHandlers)
            {
                Replacer.instance.endFlight();
                Personaliser.instance.endFlight();
            }

            if (reflectionUpdater != null)
                Destroy(reflectionUpdater);
        }
    }
}