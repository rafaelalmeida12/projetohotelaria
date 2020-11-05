using System;
using System.Collections.Generic;
using System.Linq;
using projetohotelaria.Infraestrutura.Repositorio;
using projetohotelaria.Models;

namespace projetohotelaria.Businnes
{
    public class ProdutoServices
    {
        private Contexto _context;
        public ProdutoServices(Contexto context)
        {
            _context = context;
        }

        public IEnumerable<Produto> ListarTodos()
        {
            var consulta = _context.Produto
                .OrderBy(p => p.Descricao).ToList();
            return consulta;
        }

    }
}