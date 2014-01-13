using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ANDREICSLIB;
using HiddenWords.ServiceReference1;
using templates;
using ListViewSorter = ANDREICSLIB.ListViewSorter;

namespace HiddenWords
{
    public partial class Form1 : Form
    {
        #region licensing

        private const string AppTitle = "Hidden Words";
        private const double AppVersion = 1.3;
        private const String HelpString = "";
        private readonly String OtherText =
            @"©" + DateTime.Now.Year +
            @" Andrei Gec (http://www.andreigec.net)

Licensed under GNU LGPL (http://www.gnu.org/)

Zip Assets © SharpZipLib (http://www.sharpdevelop.net/OpenSource/SharpZipLib/)
";

        #endregion

        public const int maxAdd = 100;
        private readonly ListViewSorter LVS = new ListViewSorter();
        private Dictionary d;

        public Form1()
        {
            InitializeComponent();
        }

        public Licensing.DownloadedSolutionDetails GetDetails()
        {
            try
            {
                var sr = new ServicesClient();
                var ti = sr.GetTitleInfo(AppTitle);
                if (ti == null)
                    return null;
                return ToDownloadedSolutionDetails(ti);

            }
            catch (Exception)
            {
            }
            return null;
        }

        public static Licensing.DownloadedSolutionDetails ToDownloadedSolutionDetails(TitleInfoServiceModel tism)
        {
            return new Licensing.DownloadedSolutionDetails()
            {
                ZipFileLocation = tism.LatestTitleDownloadPath,
                ChangeLog = tism.LatestTitleChangelog,
                Version = tism.LatestTitleVersion
            };
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            wordaddlimit.Text = "Only Add " + maxAdd.ToString() + " Of The Longest Words Found";
            Licensing.CreateLicense(this, menuStrip1, new Licensing.SolutionDetails(GetDetails, HelpString, AppTitle, AppVersion, OtherText));

            string p = Path.GetDirectoryName(Application.ExecutablePath);
            Directory.SetCurrentDirectory(p);

            if (File.Exists("default.txt"))
            {
                dicfilebutton.Text = "default.txt";
                d = new Dictionary(dicfilebutton.Text);
            }
        }

        private void findbutton_Click(object sender, EventArgs e)
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
                if (wordaddlimit.Checked)
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

        private void dicfilebutton_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Select Dictionary File To Use";
            OFD.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            OFD.ShowDialog();
            if (string.IsNullOrEmpty(OFD.FileName) == false)
            {
                d = new Dictionary(OFD.FileName);
                if (string.IsNullOrEmpty(d.fileName) == false)
                    dicfilebutton.Text = d.fileName.Substring(d.fileName.LastIndexOf('\\') + 1);
            }
        }

        private void possiblechars_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = TextboxExtras.HandleInput(TextboxExtras.InputType.Create(true, false, false, false), e.KeyChar,
                                                possiblechars);
        }
        }
}