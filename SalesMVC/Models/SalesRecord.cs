using System;
using SalesMVC.Models.Enums;

namespace SalesMVC.Models
{
    public class SalesRecord
    {

        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }
        public Seller Selller { get; set; }

        public SalesRecord()
        {
        }

        public SalesRecord(int iD, DateTime date, double amount, SalesStatus status, Seller selller)
        {
            ID = iD;
            Date = date;
            Amount = amount;
            Status = status;
            Selller = selller;
        }
    }
}
