using System;

namespace AssetManagement.Core.Exceptions
{
    public class FileNotReadException : Exception
    {
        public FileNotReadException() { }

        public FileNotReadException(string message) : base(message) { }
    }
}