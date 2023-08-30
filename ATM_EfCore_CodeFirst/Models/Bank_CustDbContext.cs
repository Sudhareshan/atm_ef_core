using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ATM_EfCore_CodeFirst.Models
{
    public class Bank_CustDbContext : DbContext
    {
        public Bank_CustDbContext(DbContextOptions<Bank_CustDbContext> options) : base(options)
        {

        }

        public DbSet<Bank_CustomersModel> Customers { get; set; }

        public DbSet<TransationModel> Transations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<Bank_CustomersModel>().ToTable(tb => tb.HasTrigger("tr_Ministatement"));
        }



        //public class TriggerCheckResult
        //{
        //    public int Count { get; set; }
        //}
        //public void CreateTrigger()
        //{
        //    string triggerName = "tr_Ministatement";

        //    // Check if the trigger already exists
        //    var triggerExists = Database.SqlQueryRaw<TriggerCheckResult>(
        //"SELECT COUNT(*) AS Count FROM sys.triggers WHERE [name] = {0}",
        //triggerName).FirstOrDefault();

        //    if (triggerExists!=null)
        //    {
        //        string triggerSql = @"
        //    CREATE TRIGGER tr_Ministatement ON Customers
        //      AFTER UPDATE
        //      AS
        //      BEGIN
        //          declare @accountNumber bigint ,@newAmount money ,@oldAmount money ,@Tans_Time datetime,
        //          @status varchar(100);
        //          select @accountNumber=AccountNumber  from  inserted;
        //          select @newAmount=Balance from inserted;
        //          select @oldAmount=Balance from deleted;

        //          if @oldAmount<@newAmount

        //          set @status ='Amount Credited '+convert(varchar(50),(@newAmount-@oldAmount));
        //          else
        //          set @status ='Amount Debited '+convert(varchar(50),(@oldAmount-@newAmount));

        //         insert into Transations(Trans_Time,Amount,Bank_CustomersAccountNumber) values (GETDATE(),@status,@accountNumber);
        //      END
        //";

        //        Database.ExecuteSqlRaw(triggerSql);

        //    }

        //}



        ////protected override void OnModelCreating(ModelBuilder modelBuilder)
        ////{
        ////    base.OnModelCreating(modelBuilder);

        ////    modelBuilder.HasAnnotation("SqlServer:CreateTrigger:tr_Mini_statement",
        ////     @"CREATE TRIGGER tr_Ministate ON Customers
        ////      AFTER UPDATE
        ////      AS
        ////      BEGIN
        ////          declare @accountNumber bigint ,@newAmount money ,@oldAmount money ,@Tans_Time datetime,
        ////          @status varchar(100);
        ////          select @accountNumber=AccountNumber  from  inserted;
        ////          select @newAmount=Balance from inserted;
        ////          select @oldAmount=Balance from deleted;

        ////          if @oldAmount<@newAmount

        ////          set @status ='Amount Credited '+convert(varchar(50),(@newAmount-@oldAmount));
        ////          else
        ////          set @status ='Amount Debited '+convert(varchar(50),(@oldAmount-@newAmount));

        ////         insert into Transations(Trans_Time,Amount,Bank_CustomersAccountNumber) values (GETDATE(),@status,@accountNumber);
        ////      END");


        ////}
    }
}
