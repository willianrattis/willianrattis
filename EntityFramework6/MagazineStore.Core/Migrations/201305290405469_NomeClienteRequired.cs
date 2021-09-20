namespace MagazineStore.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeClienteRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbCliente", "sNome", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbCliente", "sNome", c => c.String(maxLength: 100));
        }
    }
}
