﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using OpenCvSharp;


namespace ctmeasure
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        public static frmmain fmain;
        public static softkeyyt skey;
        public static string keypath;

        [STAThread]
        static void Main()
       {
            //using var src = new Mat("lenna.jpg", ImreadModes.Grayscale);
            //using var dst = new Mat();

            //Cv2.Canny(src, dst, 50, 200);
            //using (new Window("src image", src))
            //using (new Window("dst image", dst))
            //{
            //    Cv2.WaitKey();
            //}

            Process[] proc = Process.GetProcessesByName(Application.ProductName);
            if (proc.Length > 1)
            {
                MessageBox.Show("应用程序已经打开...");
                Thread.Sleep(1000);
                Environment.Exit(1);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                fmain = new frmmain();
                skey = new softkeyyt();
                if (!getkey()) { Application.Exit(); return; };
                Application.Run(fmain);
            }
        }

        public static bool getkey(){
            if (skey.FindPort(0, ref keypath) != 0)
            {
                MessageBox.Show("请插入正版软件电子锁！","提示");
                //return false;
                return true;
            }
            else return true;
        }

        public static bool getversion()
        {
            return true;
            string outstring = "";
            if (Program.skey.YReadString(ref outstring, 0, 13, "00000000", "FFFFFFFF", Program.keypath) != 0) return false;
            else
            {
                if (outstring.Substring(0, 6).ToLower() != fmain.ondrawingstr+fmain.mver) return false;
                else { 
                    int vint=int.Parse(outstring.Substring(6).Replace(".",""));
                    int aint=int.Parse(Application.ProductVersion.Replace(".",""));
                    if(vint<=aint) return true;
                    else return false;
                }
            }
        }
    }
}