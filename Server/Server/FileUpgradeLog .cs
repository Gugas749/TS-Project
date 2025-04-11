using DbUp.Engine.Output;
using System;
using System.IO;

namespace Server
{
    public class FileUpgradeLog : IUpgradeLog
    {
        private readonly StreamWriter _logWriter;

        public FileUpgradeLog(string filePath)
        {
            _logWriter = new StreamWriter(filePath, append: true) { AutoFlush = true };
        }

        // Logs Trace level messages (used for fine-grained debug output)
        public void LogTrace(string format, params object[] args)
        {
            _logWriter.WriteLine($"[TRACE] {string.Format(format, args)}");
        }

        // Logs Debug level messages (used for debugging)
        public void LogDebug(string format, params object[] args)
        {
            _logWriter.WriteLine($"[DEBUG] {string.Format(format, args)}");
        }

        // Logs Information level messages (used for regular information)
        public void LogInformation(string format, params object[] args)
        {
            _logWriter.WriteLine($"[INFO] {string.Format(format, args)}");
        }

        // Logs Warning level messages
        public void LogWarning(string format, params object[] args)
        {
            _logWriter.WriteLine($"[WARNING] {string.Format(format, args)}");
        }

        // Logs Error level messages (used for non-fatal errors)
        public void LogError(string format, params object[] args)
        {
            _logWriter.WriteLine($"[ERROR] {string.Format(format, args)}");
        }

        // Logs Error level messages with an exception
        public void LogError(Exception ex, string format, params object[] args)
        {
            _logWriter.WriteLine($"[ERROR] {string.Format(format, args)}");
            _logWriter.WriteLine($"Exception: {ex.Message}");
            _logWriter.WriteLine(ex.StackTrace);
        }

        // Dispose of the StreamWriter once done
        public void Dispose()
        {
            _logWriter?.Dispose();
        }
    }
}
