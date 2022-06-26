using Core.Endities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Test
{
    public class ClienteControllerTest
    {
        private readonly HttpClient _client;
        public ClienteControllerTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _client = webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task DeveSalvarClienteCorretamente()
        {
            var cliente = new Cliente
            {
                Nome = "cliente de teste",
                DtNascimento = new DateTime(1992, 3, 30),
                status = 1,
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Logradouro = "rua 1",
                        Cidade = "Fortaleza",
                        Bairro = "bairro de teste",
                        Cep="60543458",
                        Status = 1,
                        Uf = "CE"
                    }
                }
            };

            HttpResponseMessage respose = await Insert(cliente);

            var stringResult = await respose.Content.ReadAsStringAsync();
            var clienteResponse = JsonConvert.DeserializeObject<Cliente>(stringResult);

            Assert.Equal(HttpStatusCode.OK, respose.StatusCode);
            Assert.NotNull(clienteResponse);
        }

        [Fact]
        public async Task DeveAtualizarClienteCorretamente()
        {
            short statusUpdate = 2;
            var nomeUpdate = "cliente de teste up";
            var dtNascimentoUpdate = new DateTime(1992, 3, 31);
            var logradouroUpdate = "rua 1 up";
            var bairroUpdate = "bairro de teste up";

            var cliente = new Cliente
            {
                Nome = "cliente de teste",
                DtNascimento = new DateTime(1992, 3, 30),
                status = 1,
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Logradouro = "rua 1",
                        Cidade = "Fortaleza",
                        Bairro = "bairro de teste",
                        Cep="60543458",
                        Status = 1,
                        Uf = "CE"
                    }
                }
            };

            var stringResult = await Insert(cliente).Result.Content.ReadAsStringAsync();
            var clienteInsert = JsonConvert.DeserializeObject<Cliente>(stringResult);

            clienteInsert.Nome = nomeUpdate;
            clienteInsert.status = statusUpdate;
            clienteInsert.DtNascimento = dtNascimentoUpdate;
            clienteInsert.Enderecos.First().Logradouro = logradouroUpdate;
            clienteInsert.Enderecos.First().Bairro = bairroUpdate;
            clienteInsert.Enderecos.First().Status = statusUpdate;

            var jsonContent = JsonConvert.SerializeObject(clienteInsert);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage resposeUpdate = await _client.PutAsync("api/cliente", contentString);

            var stringUpdateCliente = await _client.GetStringAsync("api/cliente?id=" + clienteInsert.Id);
            var clienteUpdate = JsonConvert.DeserializeObject<Cliente>(stringUpdateCliente);

            Assert.Equal(HttpStatusCode.OK, resposeUpdate.StatusCode);
            Assert.Equal(statusUpdate, clienteUpdate.status);
            Assert.Equal(nomeUpdate, clienteUpdate.Nome);
            Assert.Equal(dtNascimentoUpdate, clienteUpdate.DtNascimento);
            Assert.Equal(logradouroUpdate, clienteUpdate.Enderecos.First().Logradouro = logradouroUpdate);
            Assert.Equal(bairroUpdate, clienteUpdate.Enderecos.First().Bairro = bairroUpdate);
            Assert.Equal(statusUpdate, clienteUpdate.Enderecos.First().Status = statusUpdate);
        }

        [Fact]
        public async Task DeveDeletarClienteCorretamente()
        {
            var cliente = new Cliente
            {
                Nome = "cliente de teste",
                DtNascimento = new DateTime(1992, 3, 30),
                status = 1,
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Logradouro = "rua 1",
                        Cidade = "Fortaleza",
                        Bairro = "bairro de teste",
                        Cep="60543458",
                        Status = 1,
                        Uf = "CE"
                    }
                }
            };

            var stringResult = await Insert(cliente).Result.Content.ReadAsStringAsync();

            var clienteInsert = JsonConvert.DeserializeObject<Cliente>(stringResult);

            var respose = await _client.DeleteAsync("api/cliente?id=" + clienteInsert.Id);

            Assert.Equal(HttpStatusCode.OK, respose.StatusCode);
        }

        [Fact]
        public async Task DeveBuscarClienteCorretamente()
        {
            var cliente = new Cliente
            {
                Nome = "cliente de teste",
                DtNascimento = new DateTime(1992, 3, 30),
                status = 1,
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Logradouro = "rua 1",
                        Cidade = "Fortaleza",
                        Bairro = "bairro de teste",
                        Cep="60543458",
                        Status = 1,
                        Uf = "CE"
                    }
                }
            };

            var stringResult = await Insert(cliente).Result.Content.ReadAsStringAsync();
            var clienteInsert = JsonConvert.DeserializeObject<Cliente>(stringResult);

            var respose = await _client.GetAsync("api/cliente?id=" + clienteInsert.Id);
            var stringCliente = await respose.Content.ReadAsStringAsync();
            var clienteBusca = JsonConvert.DeserializeObject<Cliente>(stringCliente);

            Assert.Equal(HttpStatusCode.OK, respose.StatusCode);
            Assert.NotNull(clienteBusca);
        }

        private async Task<HttpResponseMessage> Insert(Cliente cliente)
        {
            var jsonContent = JsonConvert.SerializeObject(cliente);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return await _client.PostAsync("api/cliente", contentString);
        }
    }
}
