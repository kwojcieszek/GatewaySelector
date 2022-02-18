using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GatewaySelector
{
    public class ExecuteCommand
    {
        private IEnumerable<ProcessStartInfo> ParseCommands(string[] args)
        {
            return args
                .Select(q => new ProcessStartInfo()
                {
                    FileName = "powershell",
                    Arguments = q,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }).ToList();
        }

        private string RunCommand(ProcessStartInfo processInfo)
        {
            var output = string.Empty;

            var process = new Process()
            {
                StartInfo = processInfo,
            };
            process.Start();

            while (!process.StandardOutput.EndOfStream)
                output += process.StandardOutput.ReadLine();

            process.WaitForExit();

            return output;
        }

        public string RunCommand(params string[] args)
        {
            return this.RunCommand(this.ParseCommands(args).FirstOrDefault());
        }
    }
}
