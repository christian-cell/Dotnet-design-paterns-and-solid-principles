namespace CompositeCommand
{

    #region BankAccount

    public class BankAccount
    {
        private int _balance;
        private int _overdraftLimit = -500;

        public BankAccount(int balance = 0)
        {
            this._balance = balance;
        }

        public void Deposit(int amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposit ${amount} , balance is now {_balance}");
        }

        public bool Withdraw(int amount)
        {
            if (_balance - amount >= _overdraftLimit)
            {
                _balance -= amount;
                Console.WriteLine($"Deposit ${amount} , balance is now {_balance}");

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{nameof(_balance)} : {_balance}";
        }
    }

    #endregion

    #region BankAccount Commands

    public abstract class Command
    {
        public abstract void Call();
        public abstract void Undo();
        public bool Success;
    }
    
    public class BankAccountCommand : Command
    {
        private BankAccount _account;

        public enum Action 
        {
            Deposit, Withdraw
        }

        private Action _action;
        private int _amount;
        private bool _succeded;
        
        public BankAccountCommand(BankAccount account, Action action, int amount, bool succeded)
        {
            _account = account;
            _action = action;
            _amount = amount;
            _succeded = succeded;
        }

        public override void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    break;
                case Action.Withdraw:
                    _succeded = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }

    #endregion    
    
    
    
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }
}