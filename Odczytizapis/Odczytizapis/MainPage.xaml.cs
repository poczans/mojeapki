using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;


namespace Odczytizapis
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
           
        }
        string folderName = "Pliki";

        string fileName = "pliki.txt";

        private void zapisdanych(string data)//metoda do zapisywania danych opierająca się na IsolatedStorage
        {

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {

                if (!isf.DirectoryExists(folderName))

                    isf.CreateDirectory(folderName);

                string filePath = System.IO.Path.Combine(folderName, fileName);

                using (IsolatedStorageFileStream rawStream = isf.CreateFile(filePath))
                {

                    StreamWriter writer = new StreamWriter(rawStream);//stworzenie nowego streamwritera

                    writer.WriteLine(data);

                    writer.Close();//zamkniecie writera

                }

            }

        }
        private string wczytywaniedanych()// metoda do odczytywania danych
        {

            string result = null;

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {

                string filePath = System.IO.Path.Combine(folderName, fileName);

                if (isf.FileExists(filePath))
                {

                    try
                    {

                        using (IsolatedStorageFileStream rawStream = isf.OpenFile(filePath,

                               System.IO.FileMode.Open))
                        {

                            StreamReader reader = new StreamReader(rawStream);

                            result = reader.ReadLine();

                            reader.Close();
                            textBox1.Text = result;//okreslenie miejsca odczytu jako textBox_odczyt

                        }

                    }

                    catch
                    {

                    }

                }

            }

            return result;//zwrot wyniku

        }

        private void button_zapis_Click(object sender, RoutedEventArgs e)// akcja po naciśnięciu przycisku zapis.
        {
            zapisdanych(textBox1.Text);
        }

        private void button_odczyt_Click(object sender, RoutedEventArgs e)// akcja po naciśnięciu przycisku odczyt.
        {
            wczytywaniedanych();
        }

        private void textBox_zapis_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}