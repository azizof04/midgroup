using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Dashboard.ViewModels
{
   public class QuesListViewModel
{
    public List<QuesViewModel> QuesList { get; set; }
}

public class QuesViewModel
{
    public int Id { get; set; }
    public string InputQuesName { get; set; }
    // Diğer Ques özellikleri
}
}