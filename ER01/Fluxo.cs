using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ER01
{
    public class Fluxo
    {
        public void Start()
        {
            int quantidadeProcessados = CsvPessoasParaSql();
            GravarQuantidadeProcessados(quantidadeProcessados);
        }
        private int CsvPessoasParaSql()
        {
            List<Entidades.Pessoa> pessoas = ObterPessoas();

            Conexoes.SqlServer sql = new Conexoes.SqlServer();

            int quantidadeProcessadas = 0;
            foreach (var pessoa in pessoas)
            {
                sql.InserirPessoa(pessoa);
                quantidadeProcessadas++;
            }

            return quantidadeProcessadas;
        }
        private List<Entidades.Pessoa> ObterPessoas()
        {
            string conteudoArquivo = File.ReadAllText(@"C:\Conexoes\pessoas.csv");
            string[] linhas = conteudoArquivo.Split("\n");

            int contadorLinhas = 0;

            List<Entidades.Pessoa> pessoas = new List<Entidades.Pessoa>();
            foreach (var linha in linhas)
            {
                if (contadorLinhas == 0)
                {
                    contadorLinhas++;
                    continue;
                }

                if (linha == "")
                    continue;

                string[] colunas = linha.Split(",");

                Entidades.Pessoa pessoa = new Entidades.Pessoa();

                pessoa.Nome = colunas[0];
                pessoa.Telefone = Convert.ToInt64(colunas[1].Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""));

                pessoas.Add(pessoa);
            }

            return pessoas;
        }

        private void GravarQuantidadeProcessados(int qtde)
        {
            string conteudo = "Teste\nQuantidade de registros processados: " + qtde;

            File.WriteAllText("C:\\Conexoes\\resultado.txt", conteudo);
        }
    }
}
