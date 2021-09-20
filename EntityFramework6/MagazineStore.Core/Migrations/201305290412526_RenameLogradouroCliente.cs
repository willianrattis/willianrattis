namespace MagazineStore.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameLogradouroCliente : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tbCliente", name: "sLogradouro", newName: "Logradouro");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.tbCliente", name: "Logradouro", newName: "sLogradouro");
        }
    }
}
