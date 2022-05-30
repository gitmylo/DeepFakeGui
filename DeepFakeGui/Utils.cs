using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DeepFakeGui
{
    public class Utils
    {
        public static string videoPath = "";
        public static string imagePath = "";
        public static async Task runFfmpeg(string parameters)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/K start cmd.exe /K \"{Program.ffmpegPath}\\ffmpeg.exe\" " + parameters;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            Process process = Process.Start(startInfo);
            process.WaitForExit();
        }
        
        public static async Task runFom(string parameters)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/K start cmd.exe /K py \"{Program.fomPath}\\demo.py\" " + parameters + " && exit";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            Process process = Process.Start(startInfo);
            await process.WaitForExitAsync();
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

        public static async Task generate(bool Absolute)
        {
            string command = $"--source_image {imagePath} --driving_video {videoPath} --checkpoint \"{Path.GetFullPath("models/vox-adv-cpk.pth.tar")}\" --config \"{Program.fomPath}\\config\\vox-adv-256.yaml\" --adapt_scale";
            if (!Absolute)
            {
                command += " --relative";
            }
            await runFom(command);
        }
    }

    public static class Extensions
    {
        public static Task WaitForExitAsync(this Process process, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (process.HasExited) return Task.CompletedTask;

            var tcs = new TaskCompletionSource<object>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.TrySetResult(null);
            if(cancellationToken != default(CancellationToken))
                cancellationToken.Register(() => tcs.SetCanceled());

            return process.HasExited ? Task.CompletedTask : tcs.Task;
        }
    }
}