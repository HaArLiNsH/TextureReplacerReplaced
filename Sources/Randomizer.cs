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
    public class Randomizer
    {
        private System.Random random = new System.Random();
       /* private Dictionary<string, int>[] maleAndfemaleHeadNumberOfUSe = new Dictionary<string, int>[]
        {
            new Dictionary<string, int>(), new Dictionary<string, int>()
        };*/

        private Dictionary<string, int> headNumberOfUse = new Dictionary<string, int>();

        private List<string> headListCleaned = new List<string>();
                
        Personaliser personaliser = Personaliser.instance;

        
        public Head_Set randomize(int gender)
        {
            Head_Set headSetChoosen = null;

            headNumberOfUse = personaliser.maleAndfemaleHeadsDB_cleaned[gender].ToDictionary(k => k.headSetName, v => 0);
            
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(headNumberOfUse);

            foreach (KeyValuePair<string, int> kvp in list)
            {
                int count = personaliser.KerbalAndTheirHeadsDB.Count(k => k.Value.Contains(kvp.Key));
                headNumberOfUse[kvp.Key] = count;
            }

            /*Util.log("00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            foreach (KeyValuePair<string, int> data in headNumberOfUse)
            {
                Util.log("The head_set : {0} is used {1} times", data.Key, data.Value);
            }*/

            list = new List<KeyValuePair<string, int>>(headNumberOfUse);

            List<KeyValuePair<string, int>> sortedList = (from kv in headNumberOfUse orderby kv.Value select kv).ToList();

            
            int counter = sortedList[0].Value;
            
           // Util.log("counter = {0}", counter);
            

           // Util.log("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
            foreach (KeyValuePair<string,int> data in sortedList)
            {
                if (data.Value <= counter)
                {
                    headListCleaned.Add(data.Key);
                   // Util.log("head ADDED !!! headListcleaned = {0}", headListCleaned.Count);
                }
                //Util.log("Sorted LIST : {0} : {1} ", data.Key, data.Value);
            }

            //List<Head_Set> genderHeads = personaliser.maleAndfemaleHeadsDB_cleaned[gender];

            int number = random.Next(0, headListCleaned.Count);

            string choice = headListCleaned[number];

            headSetChoosen = personaliser.maleAndfemaleHeadsDB_cleaned[gender].Find(x => x.headSetName == choice);



                /* foreach (KeyValuePair<string, int> data in headNumberOfUse)
                 {
                     Util.log("The head_set : {0} is used {1} times", data.Key, data.Value);
                 }*/


                //Util.log("the head chosen for is {0} and is used {1} times", headSetChoosen.headSetName, headNumberOfUse[headSetChoosen.headSetName]);

            return headSetChoosen;
        }


    }
}