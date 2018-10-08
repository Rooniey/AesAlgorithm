using Caliburn.Micro;
using System;
using System.Diagnostics;

namespace WpfGui.CustomFramework
{
    public class DebugLogger : ILog
    {
        private readonly Type _type;

        public DebugLogger(Type type)
        {
            _type = type;
        }

        private string CreateLogMessage(string format, params object[] args)
        {
            return $"[{DateTime.Now:o}] {string.Format(format, args)}";
        }

        public void Error(Exception exception)
        {
            Debug.WriteLine(CreateLogMessage(exception.ToString()), "ERROR");
        }

        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "INFO");
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "WARN");
        }
    }
}