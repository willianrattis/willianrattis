namespace MagazineStore.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCliente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbCliente",
                c => new
                    {
                        sID = c.String(nullable: false, maxLength: 40),
                        sNome = c.String(),
                    })
                .PrimaryKey(t => t.sID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbCliente");
        }
    }
}
