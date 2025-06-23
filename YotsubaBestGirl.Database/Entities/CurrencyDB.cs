using System.ComponentModel.DataAnnotations.Schema;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Proto.Puser;

namespace YotsubaBestGirl.Database.Entities
{
    public class CurrencyDB : IProtoConvertible<Proto.Puser.Currency>
    {
        public int FreeCoin { get; set; }

        public int PayCoin { get; set; }

        public int TotalCoin => FreeCoin + PayCoin;

        public CurrencyDB()
        {
            FreeCoin = 0;
            PayCoin = 0;
        }

        public Currency ToProto()
        {
            return new Currency
            {
                FreeCoin = this.FreeCoin,
                PayCoin = this.PayCoin,
                TotalCoin = this.TotalCoin
            };
        }
    }
}
