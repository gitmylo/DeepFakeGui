using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DeepFakeGui
{
    public class Utils
    {
        public static async void runFfmpeg(string parameters)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = $@"{Program.ffmpegPath}\ffmpeg.exe";
            startInfo.Arguments = parameters;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            Process process = Process.Start(startInfo);
            await process.WaitForExitAsync();
        }
        
        public static async void runFom(string parameters)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = $@"{Program.fomPath}\fom.exe";
            startInfo.Arguments = parameters;
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

            string command1 = $"-i {inputVideo} -vf \"scale=w={Width}:h={Width}:force_original_aspect_ratio=1,pad={Width}:{Width}:(ow-iw)/2:(oh-ih)/2\"";
            string command2 = $"";
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