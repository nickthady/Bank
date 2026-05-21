using BankAccountApp;

namespace BankAccountAppTests
{
    [TestClass]
    public class BankAccountTests
    {
        private BankAccount CreateDefaultAccount() => new BankAccount("Alice", 100);

        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var account = new BankAccount("Alice", 100);
            Assert.AreEqual("Alice", account.GetOwner());
            Assert.AreEqual(100, account.GetBalance());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullOwner_ThrowsArgumentException()
        {
            var account = new BankAccount(null, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyOwner_ThrowsArgumentException()
        {
            var account = new BankAccount("", 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NegativeInitialBalance_ThrowsArgumentException()
        {
            var account = new BankAccount("Alice", -50);
        }

        [TestMethod]
        public void Deposit_ValidAmount()
        {
            var account = CreateDefaultAccount();
            bool result = account.Deposit(50);
            Assert.IsTrue(result);
            Assert.AreEqual(150, account.GetBalance());
        }

        [TestMethod]
        public void Deposit_ZeroAmount_ReturnsFalse()
        {
            var account = CreateDefaultAccount();
            bool result = account.Deposit(0);
            Assert.IsFalse(result);
            Assert.AreEqual(100, account.GetBalance());
        }

        [TestMethod]
        public void Deposit_NegativeAmount_ReturnsFalse()
        {
            var account = CreateDefaultAccount();
            bool result = account.Deposit(-10);
            Assert.IsFalse(result);
            Assert.AreEqual(100, account.GetBalance());
        }

        [TestMethod]
        public void Deposit_MultipleDeposits_CorrectBalance()
        {
            var account = CreateDefaultAccount();
            account.Deposit(50);
            account.Deposit(25);
            Assert.AreEqual(175, account.GetBalance());
        }

        [TestMethod]
        public void Withdraw_ValidAmount()
        {
            var account = CreateDefaultAccount();
            bool result = account.Withdraw(40);
            Assert.IsTrue(result);
            Assert.AreEqual(60, account.GetBalance());
        }

        [TestMethod]
        public void Withdraw_ZeroAmount_ReturnsFalse()
        {
            var account = CreateDefaultAccount();
            bool result = account.Withdraw(0);
            Assert.IsFalse(result);
            Assert.AreEqual(100, account.GetBalance());
        }

        [TestMethod]
        public void Withdraw_MoreThanBalance_ReturnsFalse()
        {
            var account = CreateDefaultAccount();
            bool result = account.Withdraw(200);
            Assert.IsFalse(result);
            Assert.AreEqual(100, account.GetBalance());
        }

        [TestMethod]
        public void Withdraw_ExactBalance_Succeeds()
        {
            var account = CreateDefaultAccount();
            bool result = account.Withdraw(100);
            Assert.IsTrue(result);
            Assert.AreEqual(0, account.GetBalance());
        }

        [TestMethod]
        public void Transfer_ValidAmount()
        {
            var sender = new BankAccount("Alice", 200);
            var receiver = new BankAccount("Bob", 50);
            bool result = sender.Transfer(receiver, 100);
            Assert.IsTrue(result);
            Assert.AreEqual(100, sender.GetBalance());
            Assert.AreEqual(150, receiver.GetBalance());
        }

        [TestMethod]
        public void Transfer_MoreThanBalance_ReturnsFalse()
        {
            var sender = new BankAccount("Alice", 200);
            var receiver = new BankAccount("Bob", 50);
            bool result = sender.Transfer(receiver, 500);
            Assert.IsFalse(result);
            Assert.AreEqual(200, sender.GetBalance());
            Assert.AreEqual(50, receiver.GetBalance());
        }

        [TestMethod]
        public void Transfer_NullTarget_ReturnsFalse()
        {
            var sender = new BankAccount("Alice", 200);
            bool result = sender.Transfer(null, 100);
            Assert.IsFalse(result);
        }
    }

    [TestClass]
    public class TransactionHistoryTests
    {
        [TestMethod]
        public void AddTransaction_ValidEntry()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            Assert.AreEqual(1, history.GetTransactionCount());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTransaction_NullType_ThrowsArgumentException()
        {
            var history = new TransactionHistory();
            history.AddTransaction(null, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTransaction_EmptyType_ThrowsArgumentException()
        {
            var history = new TransactionHistory();
            history.AddTransaction("", 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTransaction_ZeroAmount_ThrowsArgumentException()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTransaction_NegativeAmount_ThrowsArgumentException()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", -50);
        }

        [TestMethod]
        public void GetLastTransaction_AfterOneDeposit()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            Assert.AreEqual("Deposit: 100.00", history.GetLastTransaction());
        }

        [TestMethod]
        public void GetLastTransaction_EmptyHistory_ReturnsNull()
        {
            var history = new TransactionHistory();
            Assert.IsNull(history.GetLastTransaction());
        }

        [TestMethod]
        public void GetLastTransaction_MultipleTransactions_ReturnsLast()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            history.AddTransaction("Withdrawal", 40);
            Assert.AreEqual("Withdrawal: 40.00", history.GetLastTransaction());
        }

        [TestMethod]
        public void GetTotalDeposited_MixedTransactions()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            history.AddTransaction("Deposit", 50);
            history.AddTransaction("Withdrawal", 30);
            Assert.AreEqual(150, history.GetTotalDeposited());
        }

        [TestMethod]
        public void GetTotalDeposited_NoDeposits_ReturnsZero()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Withdrawal", 30);
            Assert.AreEqual(0, history.GetTotalDeposited());
        }

        [TestMethod]
        public void Clear_AfterTransactions()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            history.Clear();
            Assert.AreEqual(0, history.GetTransactionCount());
        }

        [TestMethod]
        public void Clear_GetLastTransaction_ReturnsNull()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            history.Clear();
            Assert.IsNull(history.GetLastTransaction());
        }

        [TestMethod]
        public void Clear_EmptyHistory_DoesNotThrow()
        {
            var history = new TransactionHistory();
            history.Clear();
            Assert.AreEqual(0, history.GetTransactionCount());
        }
    }
}