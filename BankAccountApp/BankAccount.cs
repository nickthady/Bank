namespace BankAccountApp
{
    public class BankAccount
    {
        private double _balance;
        private readonly string _owner;
        private readonly TransactionHistory _history;

        public BankAccount(string owner, double initialBalance = 0)
        {
            if (string.IsNullOrWhiteSpace(owner))
                throw new ArgumentException("A tulajdonos neve nem lehet null vagy üres.");
            if (initialBalance < 0)
                throw new ArgumentException("A kezdeti egyenleg nem lehet negatív.");

            _owner = owner;
            _balance = initialBalance;
            _history = new TransactionHistory();
        }

        public string GetOwner()
        {
            return _owner;
        }

        public double GetBalance()
        {
            return _balance;
        }

        public bool Deposit(double amount)
        {
            if (amount <= 0)
                return false;

            _balance += amount;
            _history.AddTransaction("Deposit", amount);
            return true;
        }

        public bool Withdraw(double amount)
        {
            if (amount <= 0)
                return false;
            if (amount > _balance)
                return false;

            _balance -= amount;
            _history.AddTransaction("Withdrawal", amount);
            return true;
        }

        public bool Transfer(BankAccount target, double amount)
        {
            if (target == null)
                return false;
            if (amount <= 0)
                return false;
            if (amount > _balance)
                return false;

            _balance -= amount;
            target._balance += amount;
            _history.AddTransaction("Transfer Out", amount);
            target._history.AddTransaction("Transfer In", amount);
            return true;
        }

        public TransactionHistory GetHistory()
        {
            return _history;
        }
    }
}