using Framework_Modelo.Interfaces;
using Framework_Modelo.Models;
using System.Collections.Generic;

namespace Framework_Modelo.Services
{
    public class ModelosService : IModelosService
    {
        private readonly string _connectionString;

        public ModelosService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Models.Tabela> GetTabela()
        {
            List<Models.Tabela> lista = new List<Models.Tabela>();

            lista.Add(new Models.Tabela() { Item_1 = "Item 1", Item_2 = "Item 4", Item_3 = "Item 7", Item_4 = "Item 10", Item_5 = "Item 13" });
            lista.Add(new Models.Tabela() { Item_1 = "Item 2", Item_2 = "Item 5", Item_3 = "Item 8", Item_4 = "Item 11", Item_5 = "Item 14" });
            lista.Add(new Models.Tabela() { Item_1 = "Item 3", Item_2 = "Item 6", Item_3 = "Item 9", Item_4 = "Item 12", Item_5 = "Item 15" });

            return lista;
        }
    }
}
