namespace MagazineStore.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancaTabelaEnderecoToCliente : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbCliente", "sEnderecoID", "dbo.tbEndereco");
            DropIndex("dbo.tbCliente", new[] { "sEnderecoID" });
            AddColumn("dbo.tbCliente", "sLogradouro", c => c.String(maxLength: 100));
            AddColumn("dbo.tbCliente", "sBairro", c => c.String(maxLength: 100));
            AddColumn("dbo.tbCliente", "sCidade", c => c.String(maxLength: 100));
            AddColumn("dbo.tbCliente", "sUF", c => c.String(maxLength: 2));
            AddColumn("dbo.tbCliente", "sComplemento", c => c.String(maxLength: 50));
            AddColumn("dbo.tbCliente", "sNumero", c => c.String(maxLength: 10));
            DropColumn("dbo.tbCliente", "sEnderecoID");
            DropTable("dbo.tbEndereco");
        }
        
        public override void Down()
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
                        sNumero = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.sID);
            
            AddColumn("dbo.tbCliente", "sEnderecoID", c => c.String(nullable: false, maxLength: 40));
            DropColumn("dbo.tbCliente", "sNumero");
            DropColumn("dbo.tbCliente", "sComplemento");
            DropColumn("dbo.tbCliente", "sUF");
            DropColumn("dbo.tbCliente", "sCidade");
            DropColumn("dbo.tbCliente", "sBairro");
            DropColumn("dbo.tbCliente", "sLogradouro");
            CreateIndex("dbo.tbCliente", "sEnderecoID");
            AddForeignKey("dbo.tbCliente", "sEnderecoID", "dbo.tbEndereco", "sID", cascadeDelete: true);
        }
    }
}
