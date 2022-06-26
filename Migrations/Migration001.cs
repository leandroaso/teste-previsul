using FluentMigrator;

namespace Migrations
{
    [Migration(1L, "Criação da tabela CLIENTES")]
    public class Migration001 : Migration
    {
        public override void Up()
        {
            CriarTabelaClientes();
        }

        private void CriarTabelaClientes()
        {
            const string TABELA = "CLIENTES";

            if (Schema.Table(TABELA).Exists())
                return;

            Create.Table(TABELA)
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("NOME").AsAnsiString(200).NotNullable()
                .WithColumn("DT_NASCIMENTO").AsDateTime().NotNullable()
                .WithColumn("STATUS").AsInt16().NotNullable()
                .WithColumn("DAT_INCLUSAO").AsDateTime().WithDefault(SystemMethods.CurrentDateTime).NotNullable();
        }

        public override void Down()
        {
        }

    }
}
