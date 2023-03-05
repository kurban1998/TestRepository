using System;

namespace MyTestProject.Exceptions
{
    public class PathReferenceNotFoundException : Exception
    {
        public PathReferenceNotFoundException() { }
        public PathReferenceNotFoundException(string message) : base(message) { }
    }
}
