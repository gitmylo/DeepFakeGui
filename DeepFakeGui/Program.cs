﻿using System;
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

        private static void PatchFOM()
        {
            string fomPath = "fom/first-order-model-master";
            Dictionary<string, string> patches = new Dictionary<string, string>();
            patches.Add(
                @"config = yaml.load(f)",
                @"config = yaml.load(f, Loader=yaml.FullLoader)");
            string text = File.ReadAllText(fomPath + "\\demo.py");
            foreach (var keyvalue in patches)
            {
                text = text.Replace(keyvalue.Key, keyvalue.Value);
            }
            File.WriteAllText(fomPath + "\\demo.py", text);

            patches = new Dictionary<string, string>();
            patches.Add(
                @"from skimage.draw import circle",
                @"#from skimage.draw import circle");
            patches.Add(
                @"for kp_ind, kp in enumerate(kp_array):",
                @"#for kp_ind, kp in enumerate(kp_array):");
            patches.Add(
                @"rr, cc = circle(kp[1], kp[0], self.kp_size, shape=image.shape[:2])",
                @"#rr, cc = circle(kp[1], kp[0], self.kp_size, shape=image.shape[:2])");
            patches.Add(
                @"image[rr, cc] = np.array(self.colormap(kp_ind / num_kp))[:3]",
                @"#image[rr, cc] = np.array(self.colormap(kp_ind / num_kp))[:3]");
            
            text = File.ReadAllText(fomPath + "\\logger.py");
            foreach (var keyvalue in patches)
            {
                text = text.Replace(keyvalue.Key, keyvalue.Value);
            }
            File.WriteAllText(fomPath + "\\logger.py", text);
            
            patches = new Dictionary<string, string>();
            
            patches.Add(
                @"from skimage.util import pad",
                @"#from skimage.util import pad");
            patches.Add(
                @"return pad(clip, ((0, 0), pad_h, pad_w, (0, 0)), mode='edge')",
                @"return np.pad(clip, ((0, 0), pad_h, pad_w, (0, 0)), mode='edge')");
            
            text = File.ReadAllText(fomPath + "\\augmentation.py");
            foreach (var keyvalue in patches)
            {
                text = text.Replace(keyvalue.Key, keyvalue.Value);
            }
            File.WriteAllText(fomPath + "\\augmentation.py", text);
            
            File.WriteAllText(fomPath + "\\requirements.txt",
                "imageio==2.19.3\n" +
                "matplotlib==3.5.1\n" +
                "numpy==1.20.3\n" +
                "pandas==1.4.2\n" +
                "python-dateutil==2.8.2\n" +
                "pytz==2021.1\n" +
                "PyYAML==6.0\n" +
                "scikit-image==0.19.2\n" +
                "scikit-learn==1.0.2\n" +
                "scipy==1.7.3\n" +
                "torch==1.11.0+cu113\n" +
                "torchvision==0.12.0+cu113\n" +
                "4.62.3\n");
            
            MessageBox.Show("FOM patched, now installing packages do not close the cmd window, even if it gets stuck... press OK to continue", "FOM patched", MessageBoxButtons.OK);
            
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "py";
            startInfo.Arguments = $"-m pip install -r \"{Path.GetFullPath(fomPath + "\\requirements.txt")}\"";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;
            Process process = Process.Start(startInfo);
            process.WaitForExit();
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
            
            if (File.Exists("fom.zip"))
            {
                File.Delete("fom.zip");
            }

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
                    MessageBox.Show("Patching FOM... hit OK to begin.", "Patching FOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PatchFOM();
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