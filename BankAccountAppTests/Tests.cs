using BankAccountApp;

namespace BankAccountAppTests
{
    [TestClass]
    public  class BankAccountTests
    {
        private BankAccount CreateDefaultAccount() => new BankAccount("Alice", 100);

        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var account = new BankAccount("Alice", 100);
            Assert.AreEqual("Alice", account.GetOwner());
            Assert.AreEqual(100, account.GetBalance());
        }
        // TODO: üres/null owner -> ArgumentException
        // TODO: negatív initialBalance -> ArgumentException

        [TestMethod]
        public void Deposit_ValidAmount()
        {
            var account = CreateDefaultAccount();
            bool result = account.Deposit(50);
            Assert.IsTrue(result);
            Assert.AreEqual(150, account.GetBalance());
        }
        // TODO: amount = 0 -> false
        // TODO: negatív amount -> false
        // TODO: több egymás utáni deposit helyes összeget ad-e

        [TestMethod]
        public void Withdraw_ValidAmount()
        {
            var account = CreateDefaultAccount();
            bool result = account.Withdraw(40);
            Assert.IsTrue(result);
            Assert.AreEqual(60, account.GetBalance());
        }
        // TODO: amount = 0 -> false
        // TODO: amount > balance -> false, egyenleg változatlan
        // TODO: pontosan az egyenleg összegének felvétele sikeres-e

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
        // TODO: amount > sender balance -> false, mindkét egyenleg változatlan
        // TODO: target = null -> false
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
        // TODO: null/üres type -> kivétel
        // TODO: amount <= 0 -> kivétel

        [TestMethod]
        public void GetLastTransaction_AfterOneDeposit()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            Assert.AreEqual("Deposit: 100.00", history.GetLastTransaction());
        }
        // TODO: üres history -> null
        // TODO: több tranzakció után csak az utolsót adja vissza

        [TestMethod]
        public void GetTotalDeposited_MixedTransactions()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            history.AddTransaction("Deposit", 50);
            history.AddTransaction("Withdrawal", 30);
            Assert.AreEqual(150, history.GetTotalDeposited());
        }
        // TODO: nincs deposit -> 0

        [TestMethod]
        public void Clear_AfterTransactions()
        {
            var history = new TransactionHistory();
            history.AddTransaction("Deposit", 100);
            history.Clear();
            Assert.AreEqual(0, history.GetTransactionCount());
        }
        // TODO: Clear után GetLastTransaction() -> null
        // TODO: üres history-n Clear() nem dob kivételt
    }
}

