using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDataAccess.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Currency: EntityBase
    {

        [DataMember]
        [Display(Name = "ID")]
        public int CurrencyId { get; set; }
        [DataMember]
        [Display(Name = "Unit")]
        public int? Unit { get; set; }
        [DataMember]
        [Display(Name = "Currency Code")]
        public string CurrencyCode { get; set; }
        [DataMember]
        [Display(Name = "Currency Name")]
        public string CurrencyName { get; set; }
        [DataMember]
        [Display(Name = "Currency NameTr")]
        public string CurrencyNameTr { get; set; }
        [DataMember]
        [Display(Name = "Forex Selling")]
        public decimal? ForexSelling { get; set; }
        [DataMember]
        [Display(Name = "Forex Buying")]
        public decimal? ForexBuying { get; set; }

        [DataMember]
        [Display(Name = "Banknote Buying")]
        public decimal? BanknoteBuying { get; set; }
        [DataMember]
        [Display(Name = "Banknote Selling")]
        public decimal? BanknoteSelling { get; set; }


        internal GkfxDomain.Currency ToDomainCurrency()
        {
            return new GkfxDomain.Currency()
            {
                Unit=this.Unit,
                BanknoteBuying=this.BanknoteBuying,
                BanknoteSelling=this.BanknoteSelling,
                CurrencyCode=this.CurrencyCode,
                CurrencyId=this.CurrencyId,
                CurrencyName=this.CurrencyName,
                CurrencyNameTr=this.CurrencyNameTr,
                ForexBuying=this.ForexBuying,
                ForexSelling=this.ForexSelling

            };
        }

    }
}
