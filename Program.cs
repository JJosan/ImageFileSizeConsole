using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageFileSizeConsole
{
    internal class Program
    {
        public static HashSet<string> usedFlags = new HashSet<string>();
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided");
                return;
            }

            string folderPath = string.Empty;
            int daysBack = 0;
            DateTime? lastFileDate = null;
            int maxWidth = 0;

            
            int flagCount = 0;

            for (int i = 0; i < args.Length; i++)
            {
                string curr = args[i].ToLower();
                switch (curr)
                {
                    case "-p":
                        if (checkFlags(curr)) { return; }
                        usedFlags.Add(curr);
                        folderPath = args[i + 1];
                        flagCount++;
                        i++;
                        break;
                    case "-d":
                        if (checkFlags(curr)) { return; }
                        usedFlags.Add(curr);
                        if (args[i + 1].Contains('/'))
                        {
                            lastFileDate = Convert.ToDateTime(args[i + 1]);
                        }
                        else
                        {
                            lastFileDate = DateTime.Now.AddDays(-daysBack);
                        }
                        flagCount++;
                        i++;
                        break;
                    case "-m":
                        if (checkFlags(curr)) { return; }
                        usedFlags.Add(curr);
                        maxWidth = Convert.ToInt32(args[i + 1]);
                        i++;
                        break;
                    default:
                        break;
                }
            }

            if (flagCount != 2)
            {
                Console.WriteLine("Missing required flags");
                return;
            }

            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                DateTime fileCreationDate = File.GetCreationTime(file);
                if (fileCreationDate <= lastFileDate)
                {
                    try
                    {
                        Bitmap src = new Bitmap(file);
                        Bitmap target = new Bitmap(src.Width, src.Height);
                        Graphics g = Graphics.FromImage(target);
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), 0, 0, target.Width, target.Height);
                        g.DrawImage(src, 0, 0, target.Width, target.Height);
                        src.Dispose();
                        if (maxWidth == 0 || maxWidth >= target.Width)
                        {
                            target.Save(file, ImageFormat.Jpeg);
                        }
                        else
                        {
                            // resizing
                            double scale = (double)target.Width / maxWidth;
                            int newWidth = (int)Math.Round(target.Width / scale);
                            int newHeight = (int)Math.Round(target.Height / scale);
                            Bitmap resized = new Bitmap(target, new Size(newWidth, newHeight));
                            resized.Save(file, ImageFormat.Jpeg);
                        }
                    } catch (Exception ex)
                    {
                        // skip
                    }

                }
            }  
        }

        static bool checkFlags(string flag)
        {
            if (usedFlags.Contains(flag))
            {
                Console.WriteLine("Cannot use flag multiple times");
                return true;
            }
            return false;
        }

    }
}
