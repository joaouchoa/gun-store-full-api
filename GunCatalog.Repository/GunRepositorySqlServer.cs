using GunCatalog.Domain.Enumerable;
using GunCatalog.Domain.Model;
using GunCatalog.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunCatalog.Repository
{
    public class GunRepositorySqlServer : IGunRepository
    {
        private readonly SqlConnection sqlConnection;

        public GunRepositorySqlServer(IConfiguration config)
        {
            sqlConnection = new SqlConnection(config.GetConnectionString("Default"));
        }
        public async Task DeleteAsync(Guid id)
        {
            var comando = $"delete from Guns where id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task<Gun> GetAsync(Guid id)
        {
            Gun gun = null;

            var comando = $"select * from Guns where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                gun = new Gun
                {
                    id = (Guid)sqlDataReader["id"],
                    Modelo = (string)sqlDataReader["Modelo"],
                    Fabricante = (string)sqlDataReader["Fabricante"],
                    Calibre = (ECalibre)sqlDataReader["Calibre"],
                    Capacidade = (int)sqlDataReader["Capacidade"],
                    NumeroDeSerie = (string)sqlDataReader["NumeroDeSerie"],
                    Preco = (double)sqlDataReader["Preco"],
                    Active = (bool)sqlDataReader["Active"]
                };
            }

            await sqlConnection.CloseAsync();

            return gun;

        }

        public async Task<List<Gun>> GetAsync(string nome, string produtora)
        {
            var gunList = new List<Gun>();

            var comando = $"select * from Guns where Modelo = '{nome}' and Fabricante = '{produtora}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                gunList.Add(new Gun
                {
                    id = (Guid)sqlDataReader["id"],
                    Modelo = (string)sqlDataReader["Modelo"],
                    Fabricante = (string)sqlDataReader["Fabricante"],
                    Calibre = (ECalibre)sqlDataReader["Calibre"],
                    Capacidade = (int)sqlDataReader["Capacidade"],
                    NumeroDeSerie = (string)sqlDataReader["NumeroDeSerie"],
                    Preco = (double)sqlDataReader["Preco"],
                    Active = (bool)sqlDataReader["Active"]
                });
            }

            await sqlConnection.CloseAsync();

            return gunList;
        }

        public async Task<List<Gun>> GetListAsync(int pagina, int quantity)
        {
            var gunList  = new List<Gun>();

            var comand = $"select * from Guns order by id offset {((pagina - 1) * quantity)} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comand, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                gunList.Add(new Gun
                {
                    id = (Guid)sqlDataReader["id"],
                    Modelo = (string)sqlDataReader["Modelo"],
                    Fabricante = (string)sqlDataReader["Fabricante"],
                    Calibre = (ECalibre)sqlDataReader["Calibre"],
                    Capacidade = (int)sqlDataReader["Capacidade"],
                    NumeroDeSerie = (string)sqlDataReader["NumeroDeSerie"],
                    Preco = (double)sqlDataReader["Preco"],
                    Active = (bool)sqlDataReader["Active"]
                }) ;
            }

            await sqlConnection.CloseAsync();

            return gunList;
        }

        public async Task InsertAsync(Gun gun)
        {
            var comando = $"insert Guns (id, Modelo, Fabricante, Calibre, Capacidade, NumeroDeSerie, Preco) values ('{gun.id}','{gun.Modelo}','{gun.Fabricante}','{gun.Calibre}','{gun.Capacidade}','{gun.NumeroDeSerie}','{gun.Preco}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();

        }

        public async Task UpdateAsync(Gun gun)
        {
            var comando = $"update Guns set Modelo = '{gun.Modelo}', Fabricante = '{gun.Fabricante}', Calibre = {gun.Calibre.ToString()}, Capacidade = {gun.Capacidade.ToString()}, NumeroDeSerie = '{gun.NumeroDeSerie}', Preco = {gun.Preco.ToString().Replace(",", ".")} where id = '{gun.id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
    }
}
