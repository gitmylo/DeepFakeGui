using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepFakeGui
{
    static class Program
    {
        public static string fomPath;
        public static string ffmpegPath;
        static string ffmpegDownloadLink = "https://github.com/BtbN/FFmpeg-Builds/releases/download/autobuild-2022-05-30-12-36/ffmpeg-n5.0.1-4-ga5ebb3d25e-win64-lgpl-5.0.zip";
        static string fomDownloadLink = "https://github.com/AliaksandrSiarohin/first-order-model/archive/refs/heads/master.zip";
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.CreateDirectory("templateimages");
            Directory.CreateDirectory("motionvideos");
            Directory.CreateDirectory("output");
            Directory.CreateDirectory("processing");
            CheckFFmpeg();
            CheckFOM();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
        static void CheckFFmpeg()
        {
            bool installed = false;

            if (File.Exists("ffmpeg.zip"))
            {
                File.Delete("ffmpeg.zip");
            }

            if (Directory.Exists("ffmpeg"))
            {
                var dirs = Directory.GetDirectories("ffmpeg");
                if (dirs.Length > 0)
                {
                    installed = true;
                }
            }
            
            if (!installed)
            {
                var result = MessageBox.Show("FFmpeg is not installed. Do you want to install it?", "FFmpeg not installed", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show(
                        "Downloading FFmpeg... this may take a while, you will be notified when it is done. hit OK to begin.", "Downloading FFmpeg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(ffmpegDownloadLink, "ffmpeg.zip");
                    }
                    MessageBox.Show("Extracting FFmpeg... hit OK to begin.", "Extracting FFmpeg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZipFile.ExtractToDirectory("ffmpeg.zip", "ffmpeg");
                    MessageBox.Show("Ffmpeg is now installed. press ok to continue.", "FFmpeg installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Application.Exit();
                }
            }
            
            var mainFfmpegDir = @"ffmpeg/";
            mainFfmpegDir = Directory.GetDirectories(mainFfmpegDir).First() + "/bin";
            mainFfmpegDir = Path.GetFullPath(mainFfmpegDir);
            ffmpegPath = mainFfmpegDir;
        }
        
        static void CheckFOM()
        {
            bool installed = false;

            Directory.CreateDirectory("models");

            if (Directory.Exists("fom"))
            {
                var dirs = Directory.GetDirectories("fom");
                if (dirs.Length > 0)
                {
                    installed = true;
                }
            }
            
            if (!installed)
            {
                var result = MessageBox.Show("FOM is not installed. Do you want to install it?", "FOM not installed", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show(
                        "Downloading FOM... this may take a while, you will be notified when it is done. hit OK to begin.", "Downloading FOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(fomDownloadLink, "fom.zip");
                    }
                    MessageBox.Show("Extracting FOM... hit OK to begin.", "Extracting FOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZipFile.ExtractToDirectory("fom.zip", "fom");
                    var mainDir = @"fom/first-order-model-master";
                    mainDir = Path.GetFullPath(mainDir);
                    MessageBox.Show("Running pip install... hit OK to begin.", "Running pip install", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("cmd.exe", $"/k pip install -r {mainDir}/requirements.txt");
                    MessageBox.Show("FOM is now installed. Opening readme. and models downloads. download the vox models and put them in the models folder", "FOM installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("https://github.com/AliaksandrSiarohin/first-order-model#readme");
                    Process.Start("https://drive.google.com/drive/folders/1PyQJmkdCsAkOYwUyaj_l-l0as-iLDgeH");
                }
                else
                {
                    Application.Exit();
                }
            }
            
            var mainFomDir = @"fom/first-order-model-master";
            mainFomDir = Path.GetFullPath(mainFomDir);
            fomPath = mainFomDir;
        }
    }
}