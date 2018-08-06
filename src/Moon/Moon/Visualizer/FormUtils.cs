using LiveCharts.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moon.Visualizer
{
    public static class FormUtils
    {
        public static void AddListItem(ListView List, ListViewItem item)
        {
            if (List.InvokeRequired)

            {

                List.Invoke((MethodInvoker)delegate { List.Items.Add(item); });

            }

            else

            {

                List.Items.Add(item);
            }


        }
        public static void ClearListItem(ListView List)
        {
            if (List.InvokeRequired)

            {

                List.Invoke((MethodInvoker)delegate { List.Items.Clear(); });

            }

            else

            {

                List.Items.Clear();
            }


        }

        public static void SetTextBoxContent(TextBox Target, string Value)
        {
            if (Target.InvokeRequired)

            {

                Target.Invoke((MethodInvoker)delegate { Target.Text = Value; });

            }

            else

            {

                Target.Text = Value;
            }

        }
        public static void SetTextBoxContentMuliLine(TextBox Target, string Value)
        {
            if (Target.InvokeRequired)

            {

                Target.Invoke((MethodInvoker)delegate { Target.Text += Value + Environment.NewLine; });

            }

            else

            {

                Target.Text += Value + Environment.NewLine;
            }

        }

        public static void SetLabelText(Label Target, string content)
        {
            if (Target.InvokeRequired)

            {

                Target.Invoke((MethodInvoker)delegate { Target.Text = content; });

            }

            else

            {

                Target.Text = content;
            }

        }

        public static void SetJaugeText(SolidGauge Target, double content)
        {
            if (Target.InvokeRequired)

            {

                Target.Invoke((MethodInvoker)delegate { Target.Value = content; });

            }

            else

            {

                Target.Value = content;
            }

        }

    }
}
