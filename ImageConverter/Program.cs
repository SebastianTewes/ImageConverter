using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageConverter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Select input files..");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "image files (*.bmp)|*.bmp|all files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;

            if(openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileNames.Length > 0)
            {
                // Print all selected fileNames
                foreach(string fileName in openFileDialog.FileNames)
                {
                    Console.WriteLine(fileName);
                }
                // select target format
                Console.WriteLine();
                ImageFormat imageFormat = readConversionType();
                // Select target folder
                Console.WriteLine();
                Console.WriteLine("Select target folder..");
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult dialogResult = folderBrowserDialog.ShowDialog();
                if (dialogResult == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                {
                    Console.WriteLine(folderBrowserDialog.SelectedPath);
                    // save images to target folder
                    Console.WriteLine();
                    Console.WriteLine("progress conversion..");
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName);
                        Image image = Image.FromFile(fileName);
                        image.Save(folderBrowserDialog.SelectedPath + @"\" +  name + "." + imageFormat.ToString(), imageFormat);
                        Console.WriteLine(folderBrowserDialog.SelectedPath + @"\" + name + "." + imageFormat.ToString());
                    }
                }
                // if no browser selected
                else
                {
                    Console.WriteLine("no folder selected --> quit");
                }
            }
            // if no file selected
            else
            {
                Console.WriteLine("no file selected --> quit");
            }
            


            Console.ReadKey();
        }

        private static ImageFormat readConversionType()
        {
            ImageFormat imageFormat;
            Console.WriteLine("select image type to convert to..");
            Console.WriteLine("[0] - bmp");
            Console.WriteLine("[1] - gif");
            Console.WriteLine("[2] - jpeg");
            Console.WriteLine("[3] - png");
            Console.WriteLine("[4] - tiff");
            ConsoleKeyInfo consoleKey = new ConsoleKeyInfo();
            do
            {
                Console.CursorLeft = 0;
                consoleKey = Console.ReadKey();
            } while (consoleKey.KeyChar != '0' && consoleKey.KeyChar != '1' && consoleKey.KeyChar != '2' && consoleKey.KeyChar != '3' && consoleKey.KeyChar != '4');
            switch (consoleKey.KeyChar)
            {
                case '0':
                    imageFormat = ImageFormat.Bmp;
                    break;
                case '1':
                    imageFormat = ImageFormat.Gif;
                    break;
                case '2':
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case '3':
                    imageFormat = ImageFormat.Png;
                    break;
                case '4':
                    imageFormat = ImageFormat.Tiff;
                    break;
                default:
                    imageFormat = null;
                    break;
            }
            Console.WriteLine(" selected " + imageFormat.ToString());
            return imageFormat;
        }
    }
}
