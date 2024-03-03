using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebUI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public double UserBestScore { get; set; }
        public int ScoreCount { get; set; }
        public DateTime LastSeen { get; set; }
    
    }
}