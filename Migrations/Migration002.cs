using FluentMigrator;

namespace Migrations
{
    [Migration(2L, "Criação da tabela CLIENTE_ENDERECOS")]
    public class Migration002 : Migration
    {
        public override void Up()
        {
            CriarTabelaClienteEnderecos();
        }
        private void CriarTabelaClienteEnderecos()
        {
            const string TABELA = "CLIENTE_ENDERECOS";

            if (Schema.Table(TABELA).Exists())
                return;

            Create.Table(TABELA)
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ID_CLIENTE").AsInt32().NotNullable().ForeignKey("FK_CLIENTE_CLIENTE_ENDERECOS", "CLIENTES", "ID")
                .WithColumn("LOGRADOURO").AsAnsiString(100).NotNullable()
                .WithColumn("CEP").AsAnsiString(8).NotNullable()
                .WithColumn("UF").AsAnsiString(2).NotNullable()
                .WithColumn("CIDADE").AsAnsiString(100).NotNullable()
                .WithColumn("BAIRRO").AsAnsiString(60).NotNullable()
                .WithColumn("STATUS").AsInt16().NotNullable()
                .WithColumn("DAT_INCLUSAO").AsDateTime().WithDefault(SystemMethods.CurrentDateTime).NotNullable();
        }

        public override void Down()
        {
        }
    }
}
