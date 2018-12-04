using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using UnityEngine;

namespace GameUtil
{
	/// ini配置文件的读写
    /// <summary>
    /// This class is a very simple ini parser.
    /// 
    /// In order to write an ini file:
    /// 
    /// <code>
    /// IniParser parser = new IniParser();
    /// parser.addSection("Section1");
    /// parser.addString("Section1", "test", "Hello");
    /// parser.writeFile(ini);
    /// </code>
    /// 
    /// In order to parse an ini file:
    /// 
    /// <code>
    /// IniParser parser = new IniParser();
    /// parser.parseFile(path_to_ini_file);
    /// string s1 = parser.getString("Section1", "test"); 
    /// </code>
    /// 
    /// Ángel Luis.
    /// </summary>
    class IniParser
    {
        private Dictionary<string, Dictionary<string, object>> data = new Dictionary<string, Dictionary<string, object>>();

        /// <summary>
        /// This method parse text.
        /// </summary>
        /// <param name="text"></param>
        public void parseText(string text)
        {
            string[] lines = text.Split('\n');
            parseLines(lines);
        }
        /// <summary>
        /// This method parse lines.
        /// </summary>
        /// <param name="lines"></param>
        public void parseLines(string[] lines)
        {
            string lastSection = "";
            foreach (string line in lines)
            {
                int startSection = line.IndexOf('[');
                int endSection = line.IndexOf(']');
                if (startSection != -1 && endSection != -1 && endSection > startSection)
                {
                    string section = lastSection = line.Substring(startSection + 1, endSection - startSection - 1);
                    data.Add(section, new Dictionary<string, object>());
                }
                else if (line.Contains("=") && !line.Trim().StartsWith(";"))
                {
                    string[] keyValue = line.Split('=');
                    if (keyValue.Length == 1)
                    {
                        Dictionary<string, object> hash = (Dictionary<string, object>)(data[lastSection]);
                        hash.Add(keyValue[0].Trim(), "");
                        continue;
                    }

                    int hasComments = keyValue[1].Trim().IndexOf(';');
                    if (hasComments == -1)
                    {
                        Dictionary<string, object> hash = (Dictionary<string, object>)(data[lastSection]);
                        hash.Add(keyValue[0].Trim(), keyValue[1].Trim());
                    }
                    else
                    {
                        Dictionary<string, object> hash = (Dictionary<string, object>)(data[lastSection]);
                        hash.Add(keyValue[0].Trim(), keyValue[1].Trim().Substring(0, hasComments));
                    }
                }
                else
                {
                    if (line.Trim().StartsWith(";"))
                    {
                        string[] comment = line.Split(';');
                        Dictionary<string, object> hash = (Dictionary<string, object>)(data[lastSection]);
                        hash.Add(";", comment[1]);
                    }

                }
            }
        }
        /// <summary>
        /// This method parse a file given by parameter.
        /// </summary>
        /// <param name="path">A string that contains the path of file to parse</param>
        public void parseFile(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new FileNotFoundException("file doesn't exist:"+path);
            }

            string[] lines = System.IO.File.ReadAllLines(path);
            parseLines(lines);

        }
        /// <summary>
        /// This method will write the data into file given by path.
        /// </summary>
        /// <param name="path">A string that contains the path of file</param>
        public void writeFile(string path)
        {
            ArrayList lines = new ArrayList();
            foreach (KeyValuePair<string, Dictionary<string, object>> de in data)
            {
                lines.Add("[" + de.Key + "]");
                foreach (KeyValuePair<string, object> d in de.Value)
                {
                    if (d.Key != ";")
                    {
                        lines.Add(d.Key + "=" + d.Value);
                    }
                    else
                    {
                        lines.Add(d.Key + d.Value);
                    }
                }
            }

            System.IO.File.WriteAllLines(path, (string[])lines.ToArray(typeof(string)));
        }

        /// <summary>
        /// This method return the number of sections
        /// </summary>
        /// <returns>Return the number of sections</returns>
        public int getSectionSize()
        {
            return data.Count;
        }

        /// <summary>
        /// This method get all the sections.
        /// </summary>
        /// <returns>Return a list of string with the name of all sections</returns>
        public List<String> getSections()
        {
            List<String> keys = new List<string>(data.Keys);
            return keys;
        }

        /// <summary>
        /// This method will add a section to ini file.
        /// </summary>
        /// <param name="section">Name of section.</param>
        public void addSection(string section)
        {
            if (!data.Keys.Contains(section))
            {
                data[section] = new Dictionary<string, object>();
            }
        }
        /// <summary>
        /// This method will add a string inside section with key <i>key</i> and value <i>value</i>.
        /// If the section doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of key</param>
        /// <param name="value">Value of Key</param>
        public void addString(string section, string key, string value)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section " + section + " doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            hash[key] = value;
        }
        /// <summary>
        /// This method will add an integer inside section with key <i>key</i> and value <i>value</i>.
        /// If the section doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of key</param>
        /// <param name="value">Value of Key</param>
        public void addInteger(string section, string key, int value)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section " + section + " doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            hash[key] = value;
        }
        /// <summary>
        /// This method will add a float inside section with key <i>key</i> and value <i>value</i>.
        /// If the section doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of key</param>
        /// <param name="value">Value of Key</param>
        public void addFloat(string section, string key, float value)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section " + section + " doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            hash[key] = value;
        }
        /// <summary>
        /// This method will add a double inside section with key <i>key</i> and value <i>value</i>.
        /// If the section doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of key</param>
        /// <param name="value">Value of Key</param>
        public void addDouble(string section, string key, double value)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section " + section + " doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            hash[key] = value;
        }
        /// <summary>
        /// This method will add a boolean inside section with key <i>key</i> and value <i>value</i>.
        /// If the section doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of key</param>
        /// <param name="value">Value of Key</param>
        public void addBoolean(string section, string key, bool value)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section " + section + " doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            hash[key] = value;
        }
        /// <summary>
        /// This method return a string inside section <i>section</i> with key <i>key</i>.
        /// If the section or key doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of Key</param>
        /// <returns>Value of Key</returns>
        public string getString(string section, string key)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            if (!hash.Keys.Contains(key))
            {
                throw new ArgumentException("Key doesn't exist");
            }
            return (string)hash[key];
        }
        /// <summary>
        /// This method return an integer inside section <i>section</i> with key <i>key</i>.
        /// If the section or key doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of Key</param>
        /// <returns>Value of Key</returns>
        public int getInteger(string section, string key)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            if (!hash.Keys.Contains(key))
            {
                throw new ArgumentException("Key doesn't exist");
            }
            int num;
            if (!int.TryParse((string)hash[key], out num))
            {
                throw new FormatException("Format is invalid");
            }
            return num;

        }
        /// <summary>
        /// This method return a boolean inside section <i>section</i> with key <i>key</i>.
        /// If the section or key doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of Key</param>
        /// <returns>Value of Key</returns>
        public bool getBoolean(string section, string key)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            if (!hash.Keys.Contains(key))
            {
                throw new ArgumentException("Key doesn't exist");
            }
            return ((string)hash[key]).ToLower() == "true" ? true : false;
        }
        /// <summary>
        /// This method return a float inside section <i>section</i> with key <i>key</i>.
        /// If the section or key doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of Key</param>
        /// <returns>Value of Key</returns>
        public float getFloat(string section, string key)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            if (!hash.Keys.Contains(key))
            {
                throw new ArgumentException("Key doesn't exist");
            }
            float num;
            if (!float.TryParse((string)hash[key], out num))
            {
                throw new FormatException("Format is invalid");
            }
            return num;
        }
        /// <summary>
        /// This method return a double inside section <i>section</i> with key <i>key</i>.
        /// If the section or key doesn't exist it throw ArgumentException.
        /// </summary>
        /// <param name="section">Name of section</param>
        /// <param name="key">Name of Key</param>
        /// <returns>Value of Key</returns>
        public double getDouble(string section, string key)
        {
            if (!data.Keys.Contains(section))
            {
                throw new ArgumentException("Section doesn't exist");
            }
            Dictionary<string, object> hash = data[section];
            if (!hash.Keys.Contains(key))
            {
                throw new ArgumentException("Key doesn't exist");
            }
            double num;
            if (!double.TryParse((string)hash[key], out num))
            {
                throw new FormatException("Format is invalid");
            }
            return num;
        }
    }
}
