/// Created by Kriogenic to edit the text of Metroid Dread - 15/10/2021



using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BTXTEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<TextEntry> dataList = new List<TextEntry>();
        public MainWindow()
        {
            InitializeComponent();
            DG_DATA.ItemsSource = dataList;
            DG_DATA.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled | ScrollBarVisibility.Hidden;
            mnuSave.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //  ProcessFile(@"C:\Users\Kriogenic\AppData\Roaming\yuzu\dump\010093801237C000\romfs\system\localization\us_english.txt");
        }

        private void ProcessFile(string FileName)
        {

            //Load the BTXT file
            byte[] fileBytes = File.ReadAllBytes(FileName);

            //Check the header of the BTXT
            if (PatternAt(fileBytes, new byte[] { 0x42, 0x54, 0x58, 0x54, 0x01, 0x00, 0x0A, 0x00 }) == 0) //FOUND HEADER
            {
                //Loop reading all elements
                dataList.Clear();
                using (BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open)))
                {
                    reader.BaseStream.Seek(0x08, SeekOrigin.Begin);

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        byte k;
                        List<byte> bytetext = new List<byte>();
                        while ((k = reader.ReadByte()) != 0x00)
                        {
                            bytetext.Add(k);
                        }
                        string str = Encoding.Default.GetString(bytetext.ToArray());


                        List<byte> byteatext = new List<byte>();
                        byte[] ka = reader.ReadBytes(2);
                        while (!ByteArrayCompare(ka, new byte[] { 0x00, 0x00 }))
                        {
                            byteatext.Add(ka[0]);
                            byteatext.Add(ka[1]);

                            ka = reader.ReadBytes(2);
                        }


                        string strs = Encoding.Unicode.GetString(byteatext.ToArray());


                        TextEntry TE = new TextEntry(str, strs);
                        dataList.Add(TE);
                    }
                }
                DG_DATA.Items.Refresh();
                mnuSave.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("The selected file was not found to be a valid Metroid Dread Locale file", "Incorrect File Format", MessageBoxButton.OK, MessageBoxImage.Error);
                dataList.Clear();
                DG_DATA.Items.Refresh();
                mnuSave.IsEnabled = false;
            }

        }

        private void SaveFile(string FileName)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Create)))
                {
                    byte[] header = new byte[] { 0x42, 0x54, 0x58, 0x54, 0x01, 0x00, 0x0A, 0x00 };

                    writer.Write(header);

                    foreach (TextEntry TE in dataList)
                    {
                        writer.Write(Encoding.Default.GetBytes(TE.NAME));
                        writer.Write((byte)0x00);
                        writer.Write(Encoding.Unicode.GetBytes(TE.TEXT));
                        writer.Write(new byte[] { 0x00, 0x00 });
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show("There was an error: " + err.Message + Environment.NewLine + "File has NOT been saved!", "Something went wrong!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }
        int FindNextMatch(byte[] src, byte match, int startIndex = 0)
        {
            for (int i = startIndex; i < src.Length; i++)
            {
                if (src[i] == match)
                {
                    return i;
                }
            }

            return -1;
        }
        int PatternAt(byte[] src, byte[] pattern, int startIndex = 0)
        {
            int maxFirstCharSlot = src.Length - pattern.Length + 1;
            if (startIndex > maxFirstCharSlot) startIndex = maxFirstCharSlot - 1;
            for (int i = startIndex; i < maxFirstCharSlot; i++)
            {
                if (src[i] != pattern[0]) // compare only first byte
                    continue;

                // found a match on first byte, now try to match rest of the pattern
                for (int j = pattern.Length - 1; j >= 1; j--)
                {
                    if (src[i + j] != pattern[j]) break;
                    if (j == 1) return i;
                }
            }
            return -1;
        }

        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Metroid Dread Locale (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                ProcessFile(openFileDialog.FileName);
            }

        }

        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Metroid Dread Locale (*.txt)|*.txt";
            saveFileDialog.Title = "Save File!";
            saveFileDialog.OverwritePrompt = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveFile(saveFileDialog.FileName);
                MessageBox.Show(saveFileDialog.FileName);
            }
        }
    }

    public class TextEntry
    {
        public string NAME { get; set; }
        public string TEXT { get; set; }

        public TextEntry(string a_name, string a_text)
        {
            NAME = a_name;
            TEXT = a_text;
        }
    }
}
