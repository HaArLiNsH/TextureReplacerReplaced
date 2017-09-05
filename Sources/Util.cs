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

//#define TR_LOG_HIERARCHY

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TextureReplacerReplaced
{
    /// <summary>
    /// Utilitarian class. Here you find all the utility method to make your life easier
    /// </summary>
    internal static class Util
    {
        /// <summary>
        /// Delimiters used to split a string composed of multiple words
        /// </summary>
        private static readonly char[] CONFIG_DELIMITERS = { ' ', '\t', ',' };

        /// <summary>
        /// name of the install directory of TextureReplacerReplaced
        /// </summary>
        public static readonly string DIR = "TextureReplacerReplaced/";
        //public static readonly List<string> DIR = Personaliser.instance.installDirectory;
        //public static readonly string[] DIR = Personaliser.instance.installDirectory;

        /// <summary>
        /// The id of the BumpMap property
        /// </summary>
        public static readonly int BUMPMAP_PROPERTY = Shader.PropertyToID("_BumpMap");

        /// <summary>
        /// The id of the Cube property
        /// </summary>
        public static readonly int CUBE_PROPERTY = Shader.PropertyToID("_Cube");

        /// <summary>
        /// The id of the Reflect color property
        /// </summary>
        public static readonly int REFLECT_COLOR_PROPERTY = Shader.PropertyToID("_ReflectColor");

        /// <summary>
        /// 
        /// </summary>
        public static readonly System.Random random = new System.Random();

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///  True if `i` is a power of two.
        /// </summary>
        /// <param name="i">The number we need to check</param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static bool isPow2(int i)
        {
            return i > 0 && (i & (i - 1)) == 0;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Split a space- and/or comma-separated configuration file value into its tokens.
        /// </summary>
        /// <param name="value">The string we need to split</param>
        /// <returns></returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static string[] splitConfigValue(string value)
        {
            return value.Split(CONFIG_DELIMITERS, StringSplitOptions.RemoveEmptyEntries);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Print a log entry for TextureReplacer. `String.Format()`-style formatting is supported.
        /// </summary>
        /// <param name="s">The message we want to log</param>
        /// <param name="args">the argument of the message</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void log(string s, params object[] args)
        {
            Type callerClass = new StackTrace(1, false).GetFrame(0).GetMethod().DeclaringType;
            UnityEngine.Debug.Log("[TRR." + callerClass.Name + "] " + String.Format(s, args));
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Print a log entry for TextureReplacer in the console. `String.Format()`-style formatting is supported.
        /// <para>Log as an error but this is a trick to make this log appears on the screen</para>
        /// </summary>
        /// <param name="s">The message we want to log</param>
        /// <param name="args">the argument of the message</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void logConsole(string s, params object[] args)
        {
            Type callerClass = new StackTrace(1, false).GetFrame(0).GetMethod().DeclaringType;
            UnityEngine.Debug.LogError("[TRR." + callerClass.Name + "] " + String.Format(s, args));
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// convert the specified string representation of a logical value to its Boolean equivalent
        /// </summary>
        /// <param name="name">the name of the string we want to try to parse</param>
        /// <param name="variable">the output value of the parsing, here a bool</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void parse(string name, ref bool variable)
        {
            bool value;
            if (bool.TryParse(name, out value))
                variable = value;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// convert the specified string representation of a logical value to its integer equivalent
        /// </summary>
        /// <param name="name">the name of the string we want to try to parse</param>
        /// <param name="variable">the output value of the parsing, here a int</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void parse(string name, ref int variable)
        {
            int value;
            if (int.TryParse(name, out value))
                variable = value;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// convert the specified string representation of a logical value to its double equivalent
        /// </summary>
        /// <param name="name">the name of the string we want to try to parse</param>
        /// <param name="variable">the output value of the parsing, here a double</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void parse(string name, ref double variable)
        {
            double value;
            if (double.TryParse(name, out value))
                variable = value;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// convert the specified string representation of a logical value to its user determined variable equivalent
        /// </summary>
        /// <param name="name">the name of the string we want to try to parse</param>
        /// <param name="variable">the output value of the parsing, here a user determined variable</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void parse<E>(string name, ref E variable)
        {
            try
            {
                variable = (E)Enum.Parse(typeof(E), name, true);
            }
            catch (ArgumentException)
            {
            }
            catch (OverflowException)
            {
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// convert the specified string representation of a logical value to its Color32 equivalent
        /// </summary>
        /// <param name="name">the name of the string we want to try to parse</param>
        /// <param name="variable">the output value of the parsing, here a Color32</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void parse(string name, ref Color32 variable)
        {
            if (name != null)
            {
                string[] components = splitConfigValue(name);
                if (components.Length >= 3)
                {
                    byte.TryParse(components[0], out variable.r);
                    byte.TryParse(components[1], out variable.g);
                    byte.TryParse(components[2], out variable.b);
                }
                if (components.Length >= 4)
                    byte.TryParse(components[3], out variable.a);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// convert the specified string representation of a logical value to its Color equivalent
        /// </summary>
        /// <param name="name">the name of the string we want to try to parse</param>
        /// <param name="variable">the output value of the parsing, here a Color</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void parse(string name, ref Color variable)
        {
            if (name != null)
            {
                string[] components = splitConfigValue(name);
                if (components.Length >= 3)
                {
                    float.TryParse(components[0], out variable.r);
                    float.TryParse(components[1], out variable.g);
                    float.TryParse(components[2], out variable.b);
                }
                if (components.Length >= 4)
                    float.TryParse(components[3], out variable.a);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// convert the specified string list representation of a logical value to its user determined string list equivalent
        /// </summary>
        /// <param name="lists">the name of the string list we want to try to parse</param>
        /// <param name="variable">the output value of the parsing, here a user determined string list</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void addLists(string[] lists, ICollection<string> variable)
        {
            foreach (string list in lists)
            {
                foreach (string item in splitConfigValue(list))
                {
                    if (!variable.Contains(item))
                        variable.Add(item);
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// convert the specified string list representation of a logical value to its user determined Regex list equivalent
        /// </summary>
        /// <param name="lists">the name of the string list we want to try to parse</param>
        /// <param name="variable">the output value of the parsing, here a user determined Regex list</param>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public static void addRELists(string[] lists, ICollection<Regex> variable)
        {
            foreach (string list in lists)
            {
                foreach (string item in splitConfigValue(list))
                    variable.Add(new Regex(item));
            }
        }

#if TR_LOG_HIERARCHY
    public static void logDownHierarchy(Transform tf, int indent = 0)
    {
      string sIndent = "";
      for (int i = 0; i < indent; ++i)
        sIndent += "  ";

      if (tf.gameObject != null)
        UnityEngine.Debug.Log(sIndent + "- " + tf.gameObject.name + ": " + tf.gameObject.GetType());

      foreach (Component c in tf.GetComponents<Component>())
      {
        UnityEngine.Debug.Log(sIndent + " * " + c);

        Renderer r = c as Renderer;
        if (r != null)
        {
          UnityEngine.Debug.Log(sIndent + "   shader:  " + r.material.shader);
          UnityEngine.Debug.Log(sIndent + "   texture: " + r.material.mainTexture);
        }
      }

      for (int i = 0; i < tf.childCount; ++i)
        logDownHierarchy(tf.GetChild(i), indent + 1);
    }

    /**
     * Print hierarchy up from a transform.
     */
    public static void logUpHierarchy(Transform tf)
    {
      for (; tf != null; tf = tf.parent)
      {
        if (tf.gameObject != null)
          UnityEngine.Debug.Log("+ " + tf.gameObject.name + ": " + tf.gameObject.GetType());

        foreach (Component c in tf.GetComponents<Component>())
        {
          UnityEngine.Debug.Log(" * " + c);

          Renderer r = c as Renderer;
          if (r != null)
          {
            UnityEngine.Debug.Log("   shader:  " + r.material.shader);
            UnityEngine.Debug.Log("   texture: " + r.material.mainTexture);
          }
        }
      }
    }
#endif
    }
}