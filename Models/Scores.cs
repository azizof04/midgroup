
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models;

 public class BestScore
{
    public int Id { get; set; }
    public User User { get; set; } // Kullanıcıyı temsil eden navigasyon özelliği
    public string UserId { get; set; } // Kullanıcı ID'si
    public ICollection<Score> Scores { get; set; } // Kullanıcının skorları

    public BestScore()
    {
        Scores = new List<Score>();
    }
}

public class Score
{
    public int Id { get; set; }
    public int Value { get; set; }
    public int BestScoreId { get; set; } // BestScore ile ilişkilendirilmiş skorun ID'si
    public BestScore BestScore { get; set; } // Skoru temsil eden navigasyon özelliği
}

