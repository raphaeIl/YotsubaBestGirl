using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Proto.Puser;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("users")]
    public class UserDB
    {
        [JsonIgnore]
        public ICollection<CardDB> Cards { get; set; }

        [Key]
        public int Uid { get; set; } // do not ever manually set this
        
        public string? Muid { get; set; }
        public string? WalletId { get; set; }
        public string? Status { get; set; }
        public string? SessionId { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? LastnameKana { get; set; }
        public string? FirstnameKana { get; set; }
        public string? Nickname { get; set; }
        public string? Comment { get; set; }
        public int Birthday { get; set; }
        public int BirthdayCount { get; set; }
        public uint BirthdayLastDate { get; set; }
        public int HomeBackgroundId { get; set; }
        public int Tutorial { get; set; }
        public int IsTutorialFinish { get; set; }
        public int IsClearBeginner { get; set; }
        public int ActiveUnit { get; set; }
        public int FavoriteCharacters { get; set; }
        public int PlayerTitleId { get; set; }
        public int PlayerTitleTargetId { get; set; }
        public long ProfileCardId { get; set; }
        public int ProfileBackgroundId { get; set; }
        public int HomeBgm { get; set; }
        public long Leaf { get; set; }
        public int Ruby { get; set; }
        public int Fragment { get; set; }
        public int FriendPoint { get; set; }
        public int Exp { get; set; }
        public int Level { get; set; }
        public int Ap { get; set; }
        public uint LastApDate { get; set; }
        public int SpecialAp { get; set; }
        public uint LastSpecialApDate { get; set; }
        public int ContinueLoginNum { get; set; }
        public int LoginNum { get; set; }
        public int TotalLoginNum { get; set; }
        public uint LastLoginDate { get; set; }
        public uint LastChallengeLoginDate { get; set; }
        public int ChallengeUpdateStep { get; set; }
        public int LastSeasonId { get; set; }
        public int EventPlayingPolicy { get; set; }
        public uint OpenedAt { get; set; }
        public uint CreatedAt { get; set; }

        public UserDB()
        {
            Cards = new List<CardDB>();

            Muid = "IiaqKNMkWhlC";
            WalletId = "MDxSD96XfmGZm";
            Status = "open";
            SessionId = "seggs";
            Lastname = "上杉";
            Firstname = "風太郎";
            LastnameKana = "ウエスギ";
            FirstnameKana = "フータロー";
            Nickname = "Raphael";
            Comment = "よろしくお願いします";
            Birthday = 102;
            BirthdayLastDate = 1749495563;
            HomeBackgroundId = 10028;
            Tutorial = 140;
            ActiveUnit = 1;
            PlayerTitleId = 505001;
            Leaf = int.MaxValue; // some currency?
            Exp = 2001;
            Level = 9;
            Ap = 28;
            LastApDate = 1749497296;
            SpecialAp = 3;
            LastSpecialApDate = 1749495563;
            ContinueLoginNum = 2;
            LoginNum = 2;
            TotalLoginNum = 6;
            LastLoginDate = 1749514629;
            LastChallengeLoginDate = 1749409163;
            ChallengeUpdateStep = 6;
            LastSeasonId = 55;
            OpenedAt = 1749497296;
            CreatedAt = 1749495563;
        }

        public User ToProto() // hehe double serialization ez clap
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<User>(jsonStr);
        }
    }   
}
