using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BitwiseTest2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string mem1Save = "";
        string mem2Save = "";
        string mem3Save = "";
        string targetSave = "";

        public MainWindow()
        {
            InitializeComponent();
        }
        private string andCalculation(string a, string b)
        {
            if (a.Length != b.Length) return null;
            string result = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '1' && b[i] == '1')
                {
                    result += "1";
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }
        private string orCalculation(string a, string b)
        {
            if (a.Length != b.Length) return null;
            string result = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '1' || b[i] == '1')
                {
                    result += "1";
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }
        private string xorCalculation(string a, string b)
        {
            if (a.Length != b.Length) return null;
            string result = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '1' ^ b[i] == '1')
                {
                    result += "1";
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }

        private void recallBN_Click(object sender, RoutedEventArgs e)
        {
            Button castSender = sender as Button;
            char memcell = castSender.Name["recall".Length];
            char calccell = castSender.Name["recallx".Length];
            TextBox memcellTBX = FindName("mem" + memcell + "TBX") as TextBox;
            TextBox calccellTBX = FindName("calc" + calccell + "TBX") as TextBox;
            calccellTBX.Text = memcellTBX.Text;
        }
        private void storeBN_Click(object sender, RoutedEventArgs e)
        {
            if (resultTBX.Text == "") return;
            Button castSender = sender as Button;
            char memcell = castSender.Name["store".Length];
            TextBox memcellTBX = FindName("mem" + memcell + "TBX") as TextBox;
            memcellTBX.Text = resultTBX.Text;

            resultTBX.Text = "";
            calcATBX.Text = "";
            calcBTBX.Text = "";
        }


        private void andBN_Click(object sender, RoutedEventArgs e)
        {
            string result = andCalculation(calcATBX.Text, calcBTBX.Text);
            if (result != null)
            {
                resultTBX.Text = result;
                checkForWin();
            }
        }

        private void orBN_Click(object sender, RoutedEventArgs e)
        {
            string result = orCalculation(calcATBX.Text, calcBTBX.Text);
            if (result != null)
            {
                resultTBX.Text = result;
                checkForWin();
            }
        }

        private void xorBN_Click(object sender, RoutedEventArgs e)
        {
            string result = xorCalculation(calcATBX.Text, calcBTBX.Text);
            if (result != null)
            {
                resultTBX.Text = result;
                checkForWin();
            }
        }

        private void checkForWin()
        {
            if (resultTBX.Text == targetTBX.Text)
            {
                MessageBox.Show("You win!");
            }
        }

        private void buildMissionBN_Click(object sender, RoutedEventArgs e)
        {
            string mem1 = "";
            string mem2 = "";
            string mem3 = "";
            string target = "";
            string code = "";
            while (mem1 == target || mem2 == target || mem3 == target)
            {

                int length = 0;
                try
                {
                    length = Convert.ToInt32(lengthTBX.Text);
                }
                catch
                {
                    return;
                }
                if (length + "" != lengthTBX.Text || length <= 0 || length > 7)
                {
                    return;
                }

                code = makeMission(length, out mem1, out mem2, out mem3, out target);
                
            }
            codeTBX.Text = code;
            loadGame(mem1, mem2, mem3, target);
        }
        private string makeMission(int length, out string mem1, out string mem2, out string mem3, out string target)
        {
            mem1 = "";
            mem2 = "";
            mem3 = "";
            target = "";

            Random random = new Random();
            List<int> choices = new int[] { 1, 2, 3, 4, 5, 6, 7 }.ToList<int>();
            int[] puzzle = new int[length];
            for (int i = 0; i < length; i++)
            {
                int selection = random.Next(0, choices.Count);
                puzzle[i] = choices[selection];
                choices.RemoveAt(selection);
            }
            //puzzle = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            for (int i = 0; i < length; i++)
            {
                if (puzzle[i] % 2 >= 1)
                {
                    mem1 += "1";
                }
                else
                {
                    mem1 += "0";
                }
                if (puzzle[i] % 4 >= 2)
                {
                    mem2 += "1";
                }
                else
                {
                    mem2 += "0";
                }
                if (puzzle[i] % 8 >= 4)
                {
                    mem3 += "1";
                }
                else
                {
                    mem3 += "0";
                }
                target += random.Next(0, 2);
            }
            string code = "";
            for (int i = 0; i < length; i++)
            {
                code += puzzle[i];
            }
            return code;
        }

        private int getComboId(char a, char b)
        {
            int rVal = 0;
            if (a == '1')
            {
                rVal += 2;
            }
            if (b == '1')
            {
                rVal += 1;
            }
            return rVal;
        }
        private void loadGame(string mem1, string mem2, string mem3, string target)
        {
            mem1TBX.Text = mem1;
            mem2TBX.Text = mem2;
            mem3TBX.Text = mem3;
            targetTBX.Text = target;
            calcATBX.Text = "";
            calcBTBX.Text = "";
            resultTBX.Text = "";
            mem1Save = mem1TBX.Text;
            mem2Save = mem2TBX.Text;
            mem3Save = mem3TBX.Text;
            targetSave = targetTBX.Text;
        }

        private void saveBN_Click(object sender, RoutedEventArgs e)
        {
            mem1Save = mem1TBX.Text;
            mem2Save = mem2TBX.Text;
            mem3Save = mem3TBX.Text;
            targetSave = targetTBX.Text;
        }

        private void resetBN_Click(object sender, RoutedEventArgs e)
        {
            mem1TBX.Text = mem1Save;
            mem2TBX.Text = mem2Save;
            mem3TBX.Text = mem3Save;
            targetTBX.Text = targetSave;
            calcATBX.Text = "";
            calcBTBX.Text = "";
            resultTBX.Text = "";
        }

    }
}
