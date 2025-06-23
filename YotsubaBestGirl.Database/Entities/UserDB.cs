using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Proto.Puser;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user")]
    public class UserDB : IProtoConvertible<User>
    {
        [JsonIgnore]
        public ICollection<CardDB> Cards { get; set; }

        [JsonIgnore]
        public ICollection<BookDB> Books { get; set; }

        [JsonIgnore]
        public ICollection<ChapterDB> Chapters { get; set; }

        [JsonIgnore]
        public ICollection<GachaHistoryDB> GachaHistory { get; set; }

        [JsonIgnore]
        public ICollection<ItemDB> Items { get; set; }

        [JsonIgnore]
        public ICollection<MemberDB> Members { get; set; }

        [JsonIgnore]
        public ICollection<MemberPictureDB> MemberPictures { get; set; }

        [JsonIgnore]
        public ICollection<OptionsDB> Options { get; set; }

        [JsonIgnore]
        public ICollection<StoryDB> Stories { get; set; }

        [JsonIgnore]
        public ICollection<UnitDB> Units { get; set; }

        [JsonIgnore]
        public ICollection<AdvertisingDB> Advertising { get; set; }

        [JsonIgnore]
        public ICollection<StageDB> Stages { get; set; }

        [JsonIgnore]
        public ICollection<PhotoStampDB> PhotoStamps { get; set; }

        [JsonIgnore]
        public ICollection<HomeMemberClothesDB> HomeMemberClothes { get; set; }

        [JsonIgnore]
        public ICollection<HomeMemberTapMotionDB> HomeMemberTapMotions { get; set; }

        [JsonIgnore]
        public ICollection<WorkLineupDB> WorkLineups { get; set; }

        [JsonIgnore]
        public ICollection<AppIconDB> AppIcons { get; set; }

        [JsonIgnore]
        public CurrencyDB Currency { get; set; }

        [JsonIgnore]
        public List<string> Clear { get; set; } // probably tags for cleared content, sent in user/load

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
            Books = new List<BookDB>();
            Chapters = new List<ChapterDB>();
            GachaHistory = new List<GachaHistoryDB>();
            Items = new List<ItemDB>();
            Members = new List<MemberDB>();
            MemberPictures = new List<MemberPictureDB>();
            Options = new List<OptionsDB>();
            Stories = new List<StoryDB>();
            Units = new List<UnitDB>();
            Advertising = new List<AdvertisingDB>();
            Stages = new List<StageDB>();
            PhotoStamps = new List<PhotoStampDB>();
            HomeMemberClothes = new List<HomeMemberClothesDB>();
            HomeMemberTapMotions = new List<HomeMemberTapMotionDB>();
            WorkLineups = new List<WorkLineupDB>();
            AppIcons = new List<AppIconDB>();

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

            // create default stuff like cards, currency, etc.
            AddDefaultItems();
        }

        public void AddDefaultItems()
        {
            // most are hardcoded based on my low level account, not a new one
            
            // cleared content tags
            Clear = new List<string>()
            {
                "unlock_story_ids",
                "options",
                "retryable_puzzle",
                "card_special_property",
                "bgm",
                "friend_approval_count",
                "unlock_dearlevels",
                "advertising",
                "card_story",
                "retryable_stage_group",
                "home_actor",
                "shop_exchange_lock",
                "home_background",
                "home_messgae",
                "feature_coupon",
                "story",
                "shop_challenge",
                "login_bonus_flag",
                "special_puzzle",
                "bonds_season_ranking",
                "app_icon",
                "challenge",
                "gacha_omake_history",
                "appointment_surprise",
                "review",
                "team_battle_selection_ids",
                "still_picture",
                "daily_unseen_lottery_result_ids",
                "retryable_quest",
                "feature_effects",
                "feature_item",
                "mileage",
                "team_battle_stage",
                "cooking_summary",
                "chapter_expire",
                "shop_dearpoint_stock",
                "unit_skill",
                "card_special",
                "team_battle_puzzle",
                "member",
                "present_count",
                "member_dearpoint",
                "season_oshimen_ids",
                "work",
                "chat_unread_categories",
                "news_unread_count",
                "stage_fail_lock",
                "feature_revival",
                "item",
                "collection_reward_flags",
                "currency",
                "team_battle_unread_lottery_result_ids",
                "player_title_summary",
                "vr_content",
                "card_special_property_slot",
                "stage_skip",
                "free_gacha_count",
                "photo_stamp",
                "member_likabilitylevel_flag",
                "card",
                "member_picture",
                "shop_fragment_lock",
                "encore_puzzle",
                "chapter",
                "challenge_done",
                "encore",
                "home_picture",
                "cooking_recipe",
                "shop_stock",
                "feature_selection_ids",
                "retryable_chapter",
                "member_likabilitypoint",
                "feature_treasure_hunting_stage",
                "group_photo",
                "book",
                "member_dearpoint_rank",
                "work_member_summary",
                "bonds_season_unlock",
                "gacha_history",
                "challenge_group",
                "photo_booth",
                "stage",
                "home_member_clothes",
                "friend_can_greeting_count",
                "last_bonds_season_ranking",
                "vip",
                "gacha_box",
                "cooking_tray",
                "news_latest_id",
                "unlock_likabilitylevels",
                "feature_unread_lottery_result_ids",
                "feature_team_member",
                "appointment",
                "member_dearlevel_flag",
                "unit",
                "work_lineup",
                "special_stage",
                "puzzle",
                "special_content",
                "func_tutorial_ids",
                "card_special_slot_status",
                "feature_team_create",
                "card_special_slot",
                "feature_appointment",
                "home_member_tap_motion",
                "user",
                "retryable_stage",
                "gacha_bonus_choice",
                "bonds_season"
            };

            // default options
            var defaultOptions = new OptionsDB();
            Options.Add(defaultOptions);

            // default members
            Members.Add(new MemberDB(1));
            Members.Add(new MemberDB(5));

            // default units
            Units.Add(new UnitDB(1));

            // default chapters
            Chapters.Add(new ChapterDB(1011, 1));
            Chapters.Add(new ChapterDB(4001, 1));

            // default items
            uint oneYearFromNow = (uint)DateTime.UtcNow.AddYears(1).Ticks;
            Items.Add(new ItemDB(3008, 1, 0, oneYearFromNow)); // random stuff idek what these are
            Items.Add(new ItemDB(5003, 1, 0, oneYearFromNow));
            Items.Add(new ItemDB(7001, 1, 0, oneYearFromNow));
            Items.Add(new ItemDB(7004, 1, 0, oneYearFromNow));

            // default member picture
            MemberPictures.Add(new MemberPictureDB(10001));
            MemberPictures.Add(new MemberPictureDB(10002));
            MemberPictures.Add(new MemberPictureDB(10003));
            MemberPictures.Add(new MemberPictureDB(10004));
            MemberPictures.Add(new MemberPictureDB(10005));

            // default books
            Books.Add(new BookDB(10001));

            // default story
            Stories.Add(new StoryDB(70010101, 2));

            // default gacha history
            GachaHistory.Add(new GachaHistoryDB(8001));

            // default card
            CardDB defaultCard = new CardDB(10001, 100011);
            Cards.Add(defaultCard);

            // default advertising
            Advertising.Add(new AdvertisingDB(1));

            // default stage
            Stages.Add(new StageDB(40001));

            // default photo stamp
            PhotoStamps.Add(new PhotoStampDB(50001));

            // default home member clothes
            HomeMemberClothes.Add(new HomeMemberClothesDB(92));

            // default home member tap motion
            HomeMemberTapMotions.Add(new HomeMemberTapMotionDB(110));

            // default work lineup
            WorkLineups.Add(new WorkLineupDB(1010028));

            // default app icons
            AppIcons.Add(new AppIconDB(1));

            Currency = new CurrencyDB()
            {
                PayCoin = int.MaxValue,
                FreeCoin = 0
            };
        }

        public User ToProto() // hehe double serialization ez clap
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<User>(jsonStr);
        }
    }   
}
