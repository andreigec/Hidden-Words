using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace templates
{
    public class ListViewSorter : IComparer
    {
        public int ByColumn { get; set; }

        public int LastSort { get; set; }

        #region IComparer Members

        public int Compare(object o1, object o2)
        {
            if (!(o1 is ListViewItem))
                return (0);
            if (!(o2 is ListViewItem))
                return (0);

            var lvi1 = (ListViewItem) o2;
            string str1 = lvi1.SubItems[ByColumn].Text;
            var lvi2 = (ListViewItem) o1;
            string str2 = lvi2.SubItems[ByColumn].Text;

            int result = 0;

            //test to see if the string is a number - perform an int compare instead of a string compare
            if (Regex.IsMatch(str1, "^[0-9]+$") && Regex.IsMatch(str2, "^[0-9]+$"))
            {
                int s1 = int.Parse(str1);
                int s2 = int.Parse(str2);
                if (lvi1.ListView.Sorting == SortOrder.Ascending)
                    result = s1.CompareTo(s2);
                else
                    result = s2.CompareTo(s1);
            }
            else
            {
                if (lvi1.ListView.Sorting == SortOrder.Ascending)
                    result = String.Compare(str1, str2);
                else
                    result = String.Compare(str2, str1);
            }

            LastSort = ByColumn;

            return (result);
        }

        #endregion
    }
}