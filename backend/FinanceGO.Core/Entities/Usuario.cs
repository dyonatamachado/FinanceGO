using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.Entities
{
    public class Usuario
    {
        public Usuario(int id, string nome, string email, string senha, DateTime dataDeNascimento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            DataDeNascimento = dataDeNascimento;
        }

        [Key]
        [Required]
        public int Id { get; private set; }
        [Required]
        public string Nome { get; private set; }
        [Required]
        public string Email { get; private set; }
        [Required]
        public string Senha { get; private set; }
        [Required]
        public DateTime DataDeNascimento { get; private set; }
        [Required]
        public List<Despesa> Despesas { get; private set; }
        [Required]
        public List<Receita> Receitas { get; private set; }

        public void AlterarSenha(string senhaAlteradaHash)
        {
            this.Senha = senhaAlteradaHash;
        }
    }
}