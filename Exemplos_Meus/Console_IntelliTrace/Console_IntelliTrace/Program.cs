using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console_IntelliTrace
{
    class Program
    {
        static void Main(string[] args)
        {
            var jogador1 = new Jogador();
            var jogador2 = new Jogador();

            jogador1.nome = "José";
            jogador2.nome = "João";

            jogador1.lancarDado();
            jogador2.lancarDado();

            Console.WriteLine(jogador1.nome + " obteve o número " + jogador1.getValorDado() + " ao lançar o dado.");
            Console.WriteLine(jogador2.nome + " obteve o número " + jogador2.getValorDado() + " ao lançar o dado.");
            Console.WriteLine("");
            Console.WriteLine("*****************Resultado****************");
            Console.WriteLine("");

            if (jogador1.getValorDado() > jogador2.getValorDado())
            {
                System.Console.WriteLine(jogador1.nome + " venceu!");
            }
            else if (jogador1.getValorDado() < jogador2.getValorDado())
            {
                System.Console.WriteLine(jogador2.nome + " venceu!");
            }
            else
            {
                System.Console.WriteLine("Não houve vencedor, a partida terminou empatada!");
            }

            Console.Read();
        }
    }

    class Jogador
    {
        public string nome { get; set; }
        private int valorDado;

        public int getValorDado()
        {
            return valorDado;
        }

        public void lancarDado()
        {
            valorDado = new Random().Next(1, 6);
        }
    }
}
