namespace MagazineStore.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLogradouroCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbCliente", "sLogradouro", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbCliente", "sLogradouro");
        }
    }
}
