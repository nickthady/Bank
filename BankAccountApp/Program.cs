using BankAccountApp;

var account = new BankAccount("Alice", 200);
account.Deposit(50);
account.Withdraw(30);

Console.WriteLine($"Tulajdonos: {account.GetOwner()}");
Console.WriteLine($"Egyenleg: {account.GetBalance()} Ft");
Console.WriteLine($"Utolsó tranzakció: {account.GetHistory().GetLastTransaction()}");
Console.WriteLine($"Összes befizetés: {account.GetHistory().GetTotalDeposited()} Ft");
Console.WriteLine($"Összes kivétel: {account.GetHistory().GetTotalWithdrawn()} Ft");