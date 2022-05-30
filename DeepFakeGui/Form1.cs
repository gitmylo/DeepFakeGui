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
            Bitmap image = new Bitmap(file);
            imagePreviewBox.Image = image;
            imagePath = imagePickDialog.FileName;
        }

        private void videoPickDialog_FileOk(object sender, CancelEventArgs e)
        {
            videoPath = videoPickDialog.FileName;
        }

        private async void processButton_Click(object sender, EventArgs e)
        {
            Utils.prepare(videoPath, imagePath, (int) numericUpDown1.Value);
            await Utils.generate(checkBox1.Checked);
            if (File.Exists("output.mp4"))
            {
                File.Move("output.mp4", "output/output.mp4");
            }
        }
    }
}