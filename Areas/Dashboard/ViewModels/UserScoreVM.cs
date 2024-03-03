using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Areas.Dashboard.ViewModels
{
    public class UserScoreVM
    {
        public User UserId { get; set; }
        public List<FClass> Vaxt { get; set; }
        

    }
}