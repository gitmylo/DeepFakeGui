using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepFakeGui
{
    public class Utils
    {
        public static string videoPath = "";
        public static string imagePath = "";
        public static void runFfmpeg(string parameters)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = $"{Program.ffmpegPath}/ffmpeg.exe";
            startInfo.Arguments = parameters;//\"{Program.ffmpegPath}/ffmpeg.exe\" " + parameters;
            //startInfo.FileName = "cmd";
            //startInfo.Arguments = $"/c {Program.ffmpegPath}/ffmpeg.exe {parameters} & PAUSE";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;
            Process process = Process.Start(startInfo);
            process.WaitForExit();
        }
        
        public static void runFom(string parameters)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "py";
            startInfo.Arguments = $"\"{Program.fomPath}/demo.py\" " + parameters;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;
            Process process = Process.Start(startInfo);
            process.WaitForExit();
        }

        public static void prepare(string inputVideo, string inputImage, int Width)
        {
            inputVideo = Path.GetFullPath(inputVideo);
            inputImage = Path.GetFullPath(inputImage);

            string outputVideo = Path.GetFullPath(@"processing/v.mp4");
            string outputImage = Path.GetFullPath(@"processing/i.png");
            videoPath = $"\"{outputVideo}\"";
            imagePath = $"\"{outputImage}\"";

            string command1 = $"-i \"{inputVideo}\" -y -vf \"scale=w={Width}:h={Width}:force_original_aspect_ratio=1,pad={Width}:{Width}:(ow-iw)/2:(oh-ih)/2\" \"{outputVideo}\"";
            string command2 = $"-i \"{inputImage}\" -y -vf \"scale=w={Width}:h={Width}:force_original_aspect_ratio=1,pad={Width}:{Width}:(ow-iw)/2:(oh-ih)/2\" \"{outputImage}\"";
            runFfmpeg(command1);
            runFfmpeg(command2);
        }

        public static async Task generate(bool Absolute, string dataset)
        {
            string command = $"--source_image {imagePath} --driving_video {videoPath} --checkpoint \"{Path.GetFullPath($"models/{dataset}-cpk.pth.tar")}\" --config \"{Program.fomPath}\\config\\{dataset}-256.yaml\" --adapt_scale";
            if (!Absolute)
            {
                command += " --relative";
            }
            runFom(command);
        }
    }
}