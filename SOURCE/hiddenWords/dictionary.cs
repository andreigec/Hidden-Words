using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace hiddenWords
{
    public class Dictionary
    {
        //sorted string to list of words that it can do
        private readonly Dictionary<string, List<string>> words;

        public string fileName = "";

        public Dictionary(String filename)
        {
            try
            {
                words = new Dictionary<string, List<string>>();
                var FS = new FileStream(filename, FileMode.Open);
                var SR = new StreamReader(FS);

                string file = SR.ReadToEnd();
                fileName = filename;

                foreach (char c in file)
                {
                    if ((c < 97 || c > 122) && (c != '\n'))
                    {
                        DialogResult DR =
                            MessageBox.Show(
                                "This file needs to be sanatised for faster access before it can be used.\nSanatised file name will be same as old with a number at the end.\nProceed?",
                                "Prompt", MessageBoxButtons.YesNo);
                        if (DR == DialogResult.Yes)
                        {
                            KeyValuePair<string, string> kvp = sanatiseFile(file, filename);
                            file = kvp.Value;
                            fileName = kvp.Key;
                        }
                        else
                            return;
                        break;
                    }
                }

                string[] filesplit = file.Split('\n');

                foreach (string s in filesplit)
                {
                    if (s.Length < 3)
                        continue;

                    string sortedKey = sortString(s);

                    if (words.ContainsKey(sortedKey))
                    {
                        List<string> existing = words[sortedKey];
                        if (existing.Contains(s) == false)
                            existing.Add(s);
                        words[sortedKey] = existing;
                    }
                    else
                    {
                        var newlist = new List<string>();
                        newlist.Add(s);
                        words[sortedKey] = newlist;
                    }
                }

                SR.Close();
                FS.Close();
            }
            catch
            {
                MessageBox.Show("Error adding dictionary file:" + filename);
            }
        }

        private String sortString(String unsorted)
        {
            var b = new List<char>();
            b.AddRange(unsorted);
            b.Sort();

            string sortedKey = "";
            foreach (char c in b)
                sortedKey += c;
            return sortedKey;
        }

        //save to file and return
        public KeyValuePair<String, String> sanatiseFile(String fileContents, String filename)
        {
            string name = filename.Substring(0, filename.LastIndexOf('.'));
            string extention = filename.Substring(filename.LastIndexOf('.'));

            int start = 0;
            while (File.Exists(name + start.ToString() + extention))
            {
                start++;
            }

            string newFileName = name + start.ToString() + extention;

            string[] filesplit = fileContents.Split('\n');

            string acceptedstr = "";
            var accepted = new List<string>();

            var r = new Regex("^[a-zA-Z]+(:Po)*?");
            foreach (string s in filesplit)
            {
                MatchCollection mc = r.Matches(s);
                foreach (Match m in mc)
                {
                    string mstr = m.ToString().ToLower();
                    if (mstr.Length < 3)
                        continue;

                    if (accepted.Contains(mstr) == false)
                        accepted.Add(mstr);

                    acceptedstr += mstr + "\n";
                }
            }

            var FS = new FileStream(newFileName, FileMode.CreateNew);
            var SW = new StreamWriter(FS);
            SW.Write(acceptedstr);
            SW.Close();
            FS.Close();
            return new KeyValuePair<string, string>(newFileName, acceptedstr);
        }

        private Dictionary<char, int> getWordCount(String charlist)
        {
            var pch = new Dictionary<char, int>();
            foreach (char c in charlist)
            {
                if (pch.ContainsKey(c) == false)
                    pch[c] = 0;

                pch[c] = pch[c] + 1;
            }
            return pch;
        }

        public List<string> getPossibleWords(String unsorted)
        {
            string possibleChars = sortString(unsorted);
            var wordsdic = new Dictionary<int, List<string>>();
            for (int a = unsorted.Length; a > 0; a--)
            {
                wordsdic.Add(a, new List<string>());
            }

            Dictionary<char, int> pch = getWordCount(possibleChars);

            //get all the words
            foreach (var kvp in words)
            {
                bool found = true;

                Dictionary<char, int> pch2 = getWordCount(kvp.Key);

                foreach (var p in pch2)
                {
                    if (pch.ContainsKey(p.Key) == false || p.Value > pch[p.Key])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    List<string> newlist = wordsdic[kvp.Key.Length];

                    foreach (string s in kvp.Value)
                    {
                        if (newlist.Contains(s) == false)
                        {
                            newlist.Add(s);
                        }
                    }
                    wordsdic[kvp.Key.Length] = newlist;
                }
            }

            var outlist = new List<string>();
            foreach (var ll in wordsdic.Values)
            {
                outlist.AddRange(ll);
            }

            return outlist;
        }
    }
}