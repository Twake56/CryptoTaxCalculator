using CryptoTaxCalculator.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoTaxCalculator
{
    public class Calculator
    {
        public enum CalculateType
        {
            FIFO,
        }

        public CalculateType calculateType;

        public Calculator(CalculateType calcType)
        {
            calculateType = calcType;
        }

        public void Calculate(List<Transaction> transactions)
        {
            switch(calculateType)
            {
                case (CalculateType.FIFO):
                    CalculateFIFO(transactions);
                    break;

                default:
                    break;
            }
        }

        private void CalculateFIFO(List<Transaction> transactions)
        {
            List<OutputItem> outputItems = new List<OutputItem>();
            var groupedTransactions = transactions.GroupBy(t => t.Asset);
            foreach(var groupedTransaction in groupedTransactions)
            {
                var buyGroup = groupedTransaction.ToList().Where(t => t.TransactionType == "Buy").ToList();
                var sellGroup = groupedTransaction.ToList().Where(t => t.TransactionType == "Sell").ToList();
                float feeRate = CalculateFeeRate(sellGroup.First());

                foreach(Transaction sell in sellGroup)
                {
                    float sellQuant = sell.Quantity;
                    foreach(Transaction buy in buyGroup)
                    {
                        if(buy.RemaingQuantity == 0)
                        {
                            continue;
                        }
                        else if(sellQuant <= buy.Quantity)
                        {
                            buy.RemaingQuantity -= sellQuant;
                            outputItems.Add(CreateOutputLine(sell, buy, sellQuant, feeRate));
                            break;
                        }
                        else
                        {
                            buy.RemaingQuantity = 0;
                            sellQuant -= buy.Quantity;
                            outputItems.Add(CreateOutputLine(sell, buy, buy.Quantity, feeRate));
                        }
                    }
                }

                
            }
        }

        private OutputItem CreateOutputLine(Transaction sell, Transaction buy, float sellQuant, float feeRate)
        {
            OutputItem outputItem = new OutputItem();
            outputItem.Volume = sellQuant;
            outputItem.Symbol = sell.Asset;
            outputItem.DateAcquired = buy.TimeStamp;
            outputItem.DateSold = sell.TimeStamp;

            float feeAmount = (sellQuant * (float)sell.SpotPrice) * feeRate;
            outputItem.Proceeds = (decimal)((sellQuant * (float)sell.SpotPrice) - feeAmount);
            outputItem.CostBasis = (decimal)(sellQuant * (float)buy.SpotPrice * (1 + feeRate));
            outputItem.Gain = outputItem.Proceeds - outputItem.CostBasis;
            outputItem.Currency = "USD";


            return outputItem;
        }

        private float CalculateFeeRate(Transaction transaction)
        {
            return (float)transaction.Fees / (float)transaction.SubTotal;
        }
    }
}
