using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ANDREICSLIB;
using ANDREICSLIB.ClassExtras;
using ANDREICSLIB.Helpers;
using ANDREICSLIB.Licensing;
using HiddenWords.ServiceReference1;
using ListViewSorter = templates.ListViewSorter;

namespace HiddenWords
{
    public partial class Form1 : Form
    {
        #region licensing
        private const String HelpString = "";

        private readonly String OtherText =
            @"©" + DateTime.Now.Year +
            @" Andrei Gec (http://www.andreigec.net)

Licensed under GNU LGPL (http://www.gnu.org/)

Zip Assets © SharpZipLib (http://www.sharpdevelop.net/OpenSource/SharpZipLib/)
";

        #endregion


        #region settings

        private static string configPath = "config.cfg";
        public void SaveConfig()
        {
            var savethese1 = new List<Control>();
            var savethese2 = new List<ToolStripItem>();
            var tp = new List<Tuple<string, string>>();

            if (dontSaveOptionsToFileToolStripMenuItem.Checked)
            {
                savethese2.Add(dontSaveOptionsToFileToolStripMenuItem);
            }
            else
            {
                savethese2.Add(onlyAddTop100WordsByLengthToolStripMenuItem);
            }

            FormConfigRestore.SaveConfig(this, configPath, savethese1, savethese2, tp);
        }

        public List<Tuple<string, string>> LoadConfig()
        {
            return FormConfigRestore.LoadConfig(this, configPath);
        }
        
        #endregion settings

        public const int maxAdd = 100;
        private readonly ListViewSorter LVS = new ListViewSorter();
        private Dictionary d;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Licensing.LicensingForm(this, menuStrip1, HelpString, OtherText);
            LoadConfig();

            string p = Path.GetDirectoryName(Application.ExecutablePath);
            Directory.SetCurrentDirectory(p);

            d = new Dictionary("default.txt");
        }

        private void Run()
        {
            if (d == null)
            {
                MessageBox.Show("You need to add a dictionary file first");
                return;
            }
            List<string> output = d.getPossibleWords(possiblechars.Text);

            words.Items.Clear();

            int count = 0;
            foreach (string s in output)
            {
                if (onlyAddTop100WordsByLengthToolStripMenuItem.Checked)
                {
                    if (count >= maxAdd)
                        break;
                    count++;
                }

                var LVI = new ListViewItem();
                LVI.Text = s;
                LVI.SubItems.Add(s.Length.ToString());

                words.Items.Add(LVI);
            }
        }

        private void words_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            words.ListViewItemSorter = LVS;
            if (LVS.LastSort == e.Column)
            {
                if (words.Sorting == SortOrder.Ascending)
                    words.Sorting = SortOrder.Descending;
                else
                    words.Sorting = SortOrder.Ascending;
            }
            else
            {
                words.Sorting = SortOrder.Descending;
            }
            LVS.ByColumn = e.Column;

            words.Sort();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void possiblechars_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = TextboxExtras.HandleInput(TextboxExtras.InputType.Create(true, false, false, false), e.KeyChar,
                                                possiblechars);
        }

        private void possiblechars_TextChanged(object sender, EventArgs e)
        {
            Run();
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveConfig();
            }
            catch (Exception)
            {
            }
        }
    }
}