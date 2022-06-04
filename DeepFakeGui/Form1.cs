using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepFakeGui
{
    public partial class Form1 : Form
    {
        string imagePath = "";
        string videoPath = "";
        
        /**
         * ffmpeg flags
         *
         * input file
         * -i input file
         *
         * scaling
         * -vf "scale=w={Width}:h={Height}:force_original_aspect_ratio=1,pad={Width}:{Height}:(ow-iw)/2:(oh-ih)/2"
         */
        
        public Form1()
        {
            InitializeComponent();
        }

        private void imageLoadButton_Click(object sender, EventArgs e)
        {
            imagePickDialog.ShowDialog();
        }

        private void videoLoadButton_Click(object sender, EventArgs e)
        {
            videoPickDialog.ShowDialog();
        }

        private void imagePickDialog_FileOk(object sender, CancelEventArgs e)
        {
            var file = imagePickDialog.FileName;
            try
            {
                Bitmap image = new Bitmap(file);
                imagePreviewBox.Image = image;
            }
            catch (Exception exception) {}
            imagePath = file;
        }

        private void videoPickDialog_FileOk(object sender, CancelEventArgs e)
        {
            videoPath = videoPickDialog.FileName;
        }

        private async void processButton_Click(object sender, EventArgs e)
        {
            Utils.prepare(videoPath, imagePath, (int) numericUpDown1.Value);
            await Utils.generate(checkBox1.Checked, modelBox.Text);
            Thread.Sleep(3000);
            Utils.runFfmpeg($"-i \"{Path.GetFullPath("result.mp4")}\" -i {Utils.videoPath} -c copy -map 0:0? -map 1:1? -shortest \"{Path.GetFullPath($"output/{((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}.mp4")}\"", true);
            /*if (File.Exists("result.mp4"))
            {
                File.Move("result.mp4", $"output/{((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}.mp4");
            }*/
        }

        private void form1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            //Ask the user if they want to open the output folder and open it if they do
            if (MessageBox.Show("Would you like to open the output folder?", "Open output folder?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("explorer.exe", "output");
            }
        }
    }
}