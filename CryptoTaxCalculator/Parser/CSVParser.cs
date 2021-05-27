using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CryptoTaxCalculator.Parser
{
    public class CSVParser
    {
        public List<Transaction> Transactions = new List<Transaction>();

        public CSVParser(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string currentLine;
                bool headerFound = false;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if(headerFound)
                    {
                        Transaction transaction = new Transaction();
                        string[] splitLine = currentLine.Split(',');
                        DateTime time;
                        DateTime.TryParse(splitLine[0], out time);
                        if(time != null)
                        {
                            transaction.TimeStamp = time;
                        }
                        transaction.TransactionType = splitLine[1];
                        transaction.Asset = splitLine[2];
                        transaction.Quantity = float.Parse(splitLine[3]);
                        transaction.RemaingQuantity = transaction.Quantity;
                        transaction.SpotPrice = decimal.Parse(splitLine[4]);
                        transaction.SubTotal = decimal.Parse(splitLine[5]);
                        transaction.TotalWithFees = decimal.Parse(splitLine[6]);
                        transaction.Fees = decimal.Parse(splitLine[7]);
                        transaction.Notes = splitLine[8];

                        Transactions.Add(transaction);
                    }
                    if (!headerFound && currentLine.IndexOf("TimeStamp", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        headerFound = true;
                    }
                }
                    
            }
        }
    }
}
