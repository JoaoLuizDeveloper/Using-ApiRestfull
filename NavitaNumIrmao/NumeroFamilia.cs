using System;
using System.Collections.Generic;
using System.Linq;

namespace NavitaNumIrmao
{
    class NumeroFamilia
    {
        public static void Main(string[] args)
        {
            //Se voce tiver um numero com 10 ou mais digitos ele dara erro na conversão para int32.
            //Sendo assim ele vai cair automaticamente na regra de ser maior do que 100.000.000
            try
            {
                Console.WriteLine("Digite um Numero não negativo e menor que 100.000.000 e descubra o menor numero de sua Familia!");
                int Num = Convert.ToInt32(Console.ReadLine());

                //Em caso de numero negativo informa ao usuario que o numero deve ser positivo.
                //While de repetição vai garantir que o usuario só ira continuar caso digite um numero positivo.
                if (Num < 0)
                {
                    while(Num < 0)
                    {
                        Console.WriteLine("Por Favor Digite um Numero Positivo: ");
                        Num = Convert.ToInt32(Console.ReadLine());
                    }                    
                }

                //Caso Maior que 100.000.000 informa que o valor excede e retorna -1.
                if (Num > 100000000)
                {
                    Console.WriteLine("Resultado Excede 100.000.000 ");
                    Console.WriteLine(-1);
                    Environment.Exit(0);
                }

                //Por fim Retornamos o maior valor para o usuario.
                Console.WriteLine("O Maior Numero dessa Familia de numeros é: ");
                Console.WriteLine(MaiorNumero(Num));
            }
            catch
            {
                Console.WriteLine("Resultado Excede 100.000.000 ");
                Console.WriteLine(-1);
                Environment.Exit(0);
            }
        }

        public static int MaiorNumero(int numero)
        {
            //Criando Lista de Inteiros e separando cada numero
            var listaNum = new List<int>();
            
            while (numero > 0)
            {
                var n = numero % 10;
                listaNum.Add(n);
                numero = numero / 10;
            }

            //Craindo campo string e Recebendo os valores ordenados do maior para o menor
            string maiorNumero = "";

            foreach(var num in listaNum.OrderByDescending(x => x))
            {
                maiorNumero += num;
            }
            
            //Convertendo para Inteiro e retornando o maior Valor da Familia
            return Convert.ToInt32(maiorNumero);
        }
    }
}