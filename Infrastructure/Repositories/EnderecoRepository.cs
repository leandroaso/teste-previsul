using Core.Endities;
using Dapper;
using Infrastructure.Dapper;
using Infrastructure.IRepositories;

namespace Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly IDapperConnection _dapper;

        public EnderecoRepository(IDapperConnection dapperConnection)
        {
            _dapper = dapperConnection;
        }

        public IEnumerable<Endereco> FindByClienteId(int id)
        {
            var query = @"
                SELECT
                    ID,
                    LOGRADOURO,
                    CEP,
                    UF,
                    CIDADE,
                    BAIRRO,
                    STATUS,
                    DAT_INCLUSAO
                FROM
                    CLIENTE_ENDERECOS 
                WHERE 
                    ID_CLIENTE = @id 
               ";

            return _dapper.GetConnection()
                .Query<Endereco>(query, new { id = id });
        }

        public Endereco Insert(Endereco endereco)
        {
            var query = @"
                INSERT INTO
                    CLIENTE_ENDERECOS (LOGRADOURO, CEP, UF, CIDADE, BAIRRO, STATUS, ID_CLIENTE)
                VALUES
                    (@logradouro, @cep, @uf, @cidade, @bairro, @status, @idCliente);
                SELECT CAST(SCOPE_IDENTITY() AS INT)";

            endereco.Id = _dapper.GetConnection().QuerySingle<int>(query, endereco);

            return endereco;
        }

        public void Update(Endereco endereco)
        {
            var sql = @"
                UPDATE
	                CLIENTE_ENDERECOS
                SET
	                LOGRADOURO = @logradouro,
                    CEP = @cep,
                    UF = @uf,
                    CIDADE = @cidade,
                    BAIRRO = @bairro,
                    STATUS = @status
                WHERE
	                ID = @id ";

            _dapper.GetConnection().Execute(sql, endereco);
        }

        public void Delete(int id)
        {
            var sql = @"
                DELETE FROM
	                CLIENTE_ENDERECOS
                WHERE
	                ID = @id ";

            _dapper.GetConnection().Execute(sql, new
            {
                id
            });
        }

        public Endereco FindBy(int id)
        {
            var query = @"
                SELECT
                   ID,
                    LOGRADOURO,
                    CEP,
                    UF,
                    CIDADE,
                    BAIRRO,
                    STATUS,
                    DAT_INCLUSAO
                FROM
                    CLIENTE_ENDERECOS                 
                WHERE ID = @id ";

            return _dapper.GetConnection()
                .QueryFirstOrDefault<Endereco>(query, new { id });
        }
    }
}
