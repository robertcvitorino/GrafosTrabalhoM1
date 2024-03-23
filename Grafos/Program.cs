
namespace Grafos
{
    public class Program
    {
        public static void Main()
        {
            int numeroVertices;
            do
            {
                Console.WriteLine("Digite o número de vértices do grafo:");
            } while (!int.TryParse(Console.ReadLine(), out numeroVertices) || numeroVertices <= 0);

            MatrizAdjacencia matrizAdj = new MatrizAdjacencia(numeroVertices);

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Adicionar aresta");
                Console.WriteLine("2. Imprimir matriz de adjacência");
                Console.WriteLine("3. Remover vertice do grafo");
                Console.WriteLine("4. Remover aresta do grafo");
                Console.WriteLine("5. O grafo é conexo");
                Console.WriteLine("6. O grafo é conexo (usando Busca em Profundidade)");
                Console.WriteLine("7. O grafo é conexo (usando Busca em Largura)");

                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                int opcao;
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    continue;
                }

                switch (opcao)
                {
                    case 0:
                        continuar = false;
                        break;
                    case 1:
                        MatrizAdjacencia.AdicionarAresta(matrizAdj);
                        break;
                    case 2:
                        matrizAdj.ImprimirMatrizAdjacencia();
                        break;
                    case 3:
                        matrizAdj.RemoverVertice();
                        break;
                    case 4:
                        matrizAdj.RemoverAresta();
                        break;
                    case 5:
                        matrizAdj.EConexo();
                        break;
                    case 6:
                        matrizAdj.BuscaEmProfundidade();
                        break;
                    case 7:
                        matrizAdj.BuscaEmLargura();
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }
            }
        }

        
    }
}