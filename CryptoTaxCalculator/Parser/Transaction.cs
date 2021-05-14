using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoTaxCalculator.Parser
{

    public class Transaction
    {
        public DateTime TimeStamp;

        public string TransactionType;

        public string Asset;

        public float Quantity;

        public decimal SpotPrice;

        public decimal SubTotal;

        public decimal TotalWithFees;

        public decimal Fees;

        public string Notes;

    }
}
