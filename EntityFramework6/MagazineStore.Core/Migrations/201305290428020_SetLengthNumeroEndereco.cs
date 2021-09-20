namespace MagazineStore.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetLengthNumeroEndereco : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbEndereco", "sNumero", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbEndereco", "sNumero", c => c.String());
        }
    }
}
