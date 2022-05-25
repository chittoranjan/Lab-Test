using Microsoft.AspNetCore.Http;

namespace Resolver.Utilities
{
    public class FilePathModel
    {
        public string DbPath { get; set; }
        public string FullPath { get; set; }
        public IFormFile File { get; set; } 
    }
}
