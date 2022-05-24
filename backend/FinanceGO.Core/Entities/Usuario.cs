using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.Entities
{
    public class Usuario
    {
        public Usuario(string nome, string email, string senha, DateTime dataDeNascimento)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            DataDeNascimento = dataDeNascimento;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public List<Despesa> Despesas { get; private set; }
        public List<Receita> Receitas { get; private set; }

        public void AlterarSenha(string senhaAlteradaHash)
        {
            this.Senha = senhaAlteradaHash;
        }
    }
}