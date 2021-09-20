namespace MagazineStore.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSexoCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbCliente", "Sexo", c => c.Int(nullable: false));
            AlterColumn("dbo.tbCliente", "Logradouro", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbCliente", "Logradouro", c => c.String(unicode: false));
            DropColumn("dbo.tbCliente", "Sexo");
        }
    }
}
