﻿namespace TransactionStorage.Model
{
    public class Transaction
    {
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }
    }
}
