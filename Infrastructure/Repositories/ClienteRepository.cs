using Core.DTOs;
using Core.Endities;
using Dapper;
using Infrastructure.Dapper;
using Infrastructure.IRepositories;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDapperConnection _dapper;

        public ClienteRepository(IDapperConnection dapperConnection)
        {
            _dapper = dapperConnection;
        }

        public Paginated<Cliente> GetByFiltro(FiltroPaginated filtro)
        {
            var paginated = new Paginated<Cliente>
            {
                PageSize = filtro.PageSize,
                PageNumber = filtro.PageNumber
            };

            var queryWhere = ConstruirSqlWhere(filtro);

            var query = @"
                SELECT
                    COUNT(DISTINCT C.ID)
                FROM
	                CLIENTES C
                INNER JOIN CLIENTE_ENDERECOS E ON C.ID = E.ID_CLIENTE ";

            query += queryWhere;

            query += @"
                SELECT
	                DISTINCT C.ID,
	                C.NOME,
	                C.DT_NASCIMENTO,
	                C.STATUS,
	                C.DAT_INCLUSAO
                FROM
	                CLIENTES C
                INNER JOIN CLIENTE_ENDERECOS E ON C.ID = E.ID_CLIENTE ";

            query += queryWhere;

            query += $@"ORDER by C.ID OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY; ";

            using (var multi = _dapper.GetConnection().QueryMultiple(query, new
            {
                nome = $"%{filtro.Nome}%",
                dtNascimento = filtro.DtNascimento,
                logradouro = $"%{filtro.Logradouro}%",
                cep = filtro.Cep,
                uf = filtro.Uf,
                cidade = $"%{filtro.Cidade}%",
                bairro = $"%{filtro.Bairro}%",
                offset = paginated.GetOffSet(),
                pageSize = paginated.PageSize
            }))
            {
                paginated.Count = multi.Read<int>().First();
                paginated.Content = multi.Read<Cliente>().ToList();
            }

            return paginated;
        }

        public Cliente Insert(Cliente cliente)
        {
            var query = @"
                INSERT INTO
                    CLIENTES (NOME, DT_NASCIMENTO, STATUS)
                VALUES
                    (@nome, @dtNascimento, @status);
                SELECT CAST(SCOPE_IDENTITY() AS INT)";

            cliente.Id = _dapper.GetConnection().QuerySingle<int>(query, cliente);

            return cliente;
        }

        public Cliente GetBy(int id)
        {
            var query = @"
                 SELECT
                    ID,
                    NOME,
                    DT_NASCIMENTO,
                    STATUS,
                    DAT_INCLUSAO
                FROM
                    CLIENTES
                WHERE ID = @id";

            return _dapper.GetConnection().QueryFirstOrDefault<Cliente>(query, param: new { id });
        }

        public void Update(Cliente cliente)
        {
            var sql = @"
                UPDATE
	                CLIENTES
                SET
	                NOME = @nome,
	                DT_NASCIMENTO = @dtNascimento,
	                STATUS = @status
                WHERE
	                ID = @id ";

            _dapper.GetConnection().Execute(sql, cliente);
        }

        public void Delete(int id)
        {
            var sql = @"
                DELETE FROM
	                CLIENTES
                WHERE
	                ID = @id ";

            _dapper.GetConnection().Execute(sql, new
            {
                id
            });
        }

        private string ConstruirSqlWhere(FiltroPaginated filtro)
        {
            var sql = string.Empty;

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
                sql += " C.NOME LIKE @nome ";

            if (filtro.DtNascimento != null)
                sql += $@"{addEndClosure(sql)} CONVERT(DATE, C.DT_NASCIMENTO) = @dtNascimento ";

            if (!string.IsNullOrWhiteSpace(filtro.Logradouro))
                sql += $"{addEndClosure(sql)} E.LOGRADOURO LIKE @logradouro ";

            if (!string.IsNullOrWhiteSpace(filtro.Cep))
                sql += $"{addEndClosure(sql)} E.CEP = @cep ";

            if (!string.IsNullOrWhiteSpace(filtro.Uf))
                sql += $"{addEndClosure(sql)} E.UF = @uf ";

            if (!string.IsNullOrWhiteSpace(filtro.Cidade))
                sql += $"{addEndClosure(sql)} E.CIDADE LIKE @cidade ";

            if (!string.IsNullOrWhiteSpace(filtro.Bairro))
                sql += $"{addEndClosure(sql)} E.BAIRRO LIKE @bairro ";

            if (!string.IsNullOrWhiteSpace(sql))
                sql = $" WHERE {sql}";

            return sql;
        }

        private string addEndClosure(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
                return string.Empty;

            return " AND ";
        }
    }
}
