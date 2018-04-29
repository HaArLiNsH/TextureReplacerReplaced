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



namespace TextureReplacerReplaced
{
    /// <summary>
    /// This class handle the load and save of the configuration data in the persistant.sfs save
    /// </summary>
    [KSPScenario(ScenarioCreationOptions.AddToAllGames, new GameScenes[] {
            GameScenes.SPACECENTER,
            GameScenes.EDITOR,
            GameScenes.FLIGHT,
            GameScenes.TRACKSTATION,
        })
]
    public class TRR_Scenario : ScenarioModule
    {
        /// <summary>
        /// called at the OnLoad()
        /// </summary>
        /// <param name="node">The name of the config node</param>
        public override void OnLoad(ConfigNode node)
        {

            //Util.log("++++ 'OnLoad()' ++++");
            Reflections.instance.loadScenario(node);
            Personaliser.instance.loadScenario(node);
        }

        /// <summary>
        /// called at the OnSave()
        /// </summary>
        /// <param name="node">The name of the config node</param>
        public override void OnSave(ConfigNode node)
        {
            //Util.log("++++ 'OnSave()' ++++");
            Reflections.instance.saveScenario(node);
            Personaliser.instance.saveScenario(node);
        }
        
        

        
    }
}