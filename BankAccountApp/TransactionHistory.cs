namespace BankAccountApp
{
    public class TransactionHistory
    {
        private readonly List<(string Type, double Amount)> _transactions;

        public TransactionHistory()
        {
            _transactions = new List<(string, double)>();
        }

        public void AddTransaction(string type, double amount)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("A tranzakció típusa nem lehet null vagy üres.");
            if (amount <= 0)
                throw new ArgumentException("A tranzakció összege csak pozitív szám lehet.");

            _transactions.Add((type, amount));
        }

        public int GetTransactionCount()
        {
            return _transactions.Count;
        }

        public string? GetLastTransaction()
        {
            if (_transactions.Count == 0)
                return null;

            var last = _transactions[_transactions.Count - 1];
            return $"{last.Type}: {last.Amount:F2}";
        }

        public double GetTotalDeposited()
        {
            double total = 0;
            foreach (var t in _transactions)
            {
                if (t.Type == "Deposit")
                    total += t.Amount;
            }
            return total;
        }

        public double GetTotalWithdrawn()
        {
            double total = 0;
            foreach (var t in _transactions)
            {
                if (t.Type == "Withdrawal")
                    total += t.Amount;
            }
            return total;
        }

        public void Clear()
        {
            _transactions.Clear();
        }
    }
}