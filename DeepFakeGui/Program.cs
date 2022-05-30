using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepFakeGui
{
    static class Program
    {
        static string ffmpegDownloadLink = "https://github.com/BtbN/FFmpeg-Builds/releases/download/autobuild-2022-05-30-12-36/ffmpeg-n5.0.1-4-ga5ebb3d25e-win64-lgpl-5.0.zip";
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CheckFFmpeg();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
        static void CheckFFmpeg()
        {
            bool installed = false;

            if (Directory.Exists("ffmpeg"))
            {
                
            }
            
            if (!installed)
            {
                var result = MessageBox.Show("FFmpeg is not installed. Do you want to install it?", "FFmpeg not installed", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://ffmpeg.zeranoe.com/builds/");
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}