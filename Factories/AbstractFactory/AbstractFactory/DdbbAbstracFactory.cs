namespace DdbbAbstracFactory
{
    #region Interface

    public interface IDataAccess
    {
        void GetData();
    }

    #endregion

    #region DatabasesConfigs

    public class DapperDataAccess: IDataAccess
    {
        public void GetData()
        {
            Console.WriteLine("Dapper");
        }
    }
    
    public class EfDataAccess: IDataAccess
    {
        public void GetData()
        {
            Console.WriteLine("Entity Framework");
        }
    }

    #endregion
     
    #region Factory
    
    public interface IDataAccessFactory
    {
        IDataAccess CreateDataAccess();
    }
    
    public class DapperFactory: IDataAccessFactory
    {
        public IDataAccess CreateDataAccess() => new DapperDataAccess();
    }
    
    public class EfFactory: IDataAccessFactory
    {
        public IDataAccess CreateDataAccess() => new EfDataAccess();
    }
    
    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {

            IDataAccessFactory dapper = new DapperFactory();
            var dataAccess = dapper.CreateDataAccess();
            dataAccess.GetData();
            
            IDataAccessFactory ef = new EfFactory();
            var dataAccessEf = ef.CreateDataAccess();
            dataAccessEf.GetData();
        }
    }
};

