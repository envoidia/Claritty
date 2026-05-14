using System.Diagnostics;

namespace Claritty.Src;

public sealed class ShellProcess
{
    private Process _process = null!;

    public void Start(string shell = "/bin/bash")
    {
        this._process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = shell,
                Arguments = "-i",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        this._process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                this.OnOutput(e.Data + "\n");
                Debug.Log($"Out: {e.Data}");
            }
        };

        this._process.ErrorDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                this.OnOutput(e.Data + "\n");
                Debug.Log($"Err: {e.Data}");
            }
        };

        this._process.Exited += (sender, e) => this.OnExited();
        this._process.EnableRaisingEvents = true;

        this._process.Start();
        this._process.BeginOutputReadLine();
        this._process.BeginErrorReadLine();
    }

    public void WriteInput(string input)
    {
        if (this._process?.StandardInput is not null && !this._process.HasExited)
        {
            this._process.StandardInput.Write(input);
            this._process.StandardInput.Flush();

            Debug.Log($"WriteInput: {input}");
        }
    }

    public void SendKey(char key)
    {
        this.WriteInput(key.ToString());
    }

    public void SendLine(string line)
    {
        this.WriteInput(line + "\n");
    }

    public void Resize(int columns, int rows)
    {
        // todo
    }

    public void ExecuteCurrentLine(string command)
    {
        if (this._process?.StandardInput is not { } stdin || this._process.HasExited)
        {
            return;
        }

        stdin.WriteLine(command);
        stdin.Flush();

        Debug.Log($"Exec: {command}");

    }

    public void OnOutput(string output)
    {
        Terminal.OutHist.Add(output);
    }

    public void OnExited()
    {
        // todo
    }
}
