using Framework_Modelo.Models;
using System.Collections.Generic;

namespace Framework_Modelo.Interfaces
{
    public interface IModelosService
    {
        List<Models.Tabela> GetTabela();
    }
}
