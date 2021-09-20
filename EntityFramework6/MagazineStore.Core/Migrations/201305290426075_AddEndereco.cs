namespace MagazineStore.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEndereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbEndereco",
                c => new
                    {
                        sID = c.String(nullable: false, maxLength: 40),
                        sLogradouro = c.String(maxLength: 100),
                        sBairro = c.String(maxLength: 100),
                        sCidade = c.String(maxLength: 100),
                        sUF = c.String(maxLength: 2),
                        sComplemento = c.String(maxLength: 50),
                        sNumero = c.String(),
                    })
                .PrimaryKey(t => t.sID);
            
            AddColumn("dbo.tbCliente", "sEnderecoID", c => c.String(nullable: false, maxLength: 40));
            CreateIndex("dbo.tbCliente", "sEnderecoID");
            AddForeignKey("dbo.tbCliente", "sEnderecoID", "dbo.tbEndereco", "sID", cascadeDelete: true);
            DropColumn("dbo.tbCliente", "Logradouro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbCliente", "Logradouro", c => c.String(maxLength: 100, unicode: false));
            DropForeignKey("dbo.tbCliente", "sEnderecoID", "dbo.tbEndereco");
            DropIndex("dbo.tbCliente", new[] { "sEnderecoID" });
            DropColumn("dbo.tbCliente", "sEnderecoID");
            DropTable("dbo.tbEndereco");
        }
    }
}
