using System;
using System.Net.Mime;

namespace CelebritiesSystem
{
    public class CelebrityResponseDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string[] Roles { get; set; }
        public string ImageUrl { get; set; }        
        
        
    }
}
