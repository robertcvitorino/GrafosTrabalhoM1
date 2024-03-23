using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafos
{
    internal class MatrizAdjacencia
    {
        private int[][] matriz;
        private int numVertices;

        public MatrizAdjacencia(int tamanho)
        {
            matriz = new int[tamanho][];
            numVertices = tamanho;
            for (int i = 0; i < tamanho; i++)
            {
                matriz[i] = new int[tamanho];
                for (int j = 0; j < tamanho; j++)
                {
                    matriz[i][j] = 0;
                }
            }
        }

        public void LimparMatriz()
        {
            for (int i = 0; i < matriz.Length; i++)
            {
                for (int j = 0; j < matriz[i].Length; j++)
                {
                    matriz[i][j] = 0;
                }
            }
        }

        public void AdicionarVertice(int vertice, List<int> listaAdjacencia)
        {
            foreach (int adjVertice in listaAdjacencia)
            {
                matriz[vertice - 1][adjVertice - 1] = 1;
            }
        }

        public void RemoverAresta()
        {
            int verticeOrigem, verticeDestino, opcao;
            do
            {
                Console.WriteLine(" 0. Para remover aresta:");
                Console.WriteLine(" 1. Para remover arco:");
            } while (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 0 || opcao > 2);

            do
            {
                Console.WriteLine("\nDigite a origem:");
            } while (!int.TryParse(Console.ReadLine(), out verticeOrigem) || verticeOrigem <= 0 || verticeOrigem > matriz.Length);

            do
            {
                Console.WriteLine("Digite o destino:");
            } while (!int.TryParse(Console.ReadLine(), out verticeDestino) || verticeDestino <= 0 || verticeDestino > matriz.Length);

            if (verticeOrigem < 1 || verticeOrigem > matriz.Length ||
                verticeDestino < 1 || verticeDestino > matriz.Length)
            {
                Console.WriteLine("Vértices inválidos!");
                return;
            }
            if (opcao == 0)
            {
                matriz[verticeOrigem - 1][verticeDestino - 1] = 0;
                matriz[verticeDestino - 1][verticeOrigem - 1] = 0;
                
            }
            else
            {
                matriz[verticeOrigem - 1][verticeDestino - 1] = 0;
            }
        }

        public bool SaoVerticesAdjacentes(int i, int j)
        {
            return matriz[i][j] == 1;
        }

        public void ImprimirMatrizAdjacencia()
        {
            for (int i = 0; i < matriz.Length; i++)
            {
                Console.Write("[ " + (i + 1) + " ] => ");
                for (int j = 0; j < matriz[i].Length; j++)
                {
                    if (SaoVerticesAdjacentes(i, j))
                    {
                        Console.Write((j + 1) + "\t");
                    }
                }
                Console.WriteLine();
            }
        }

        public void AdicionarAresta(int verticeOrigem, int verticeDestino)
        {
            matriz[verticeOrigem - 1][verticeDestino - 1] = 1;
            matriz[verticeDestino - 1][verticeOrigem - 1] = 1;
        }

        public void RemoverVertice()
        {
            int vertice;
            do
            {
                Console.WriteLine("\nDigite vertice que deseja remover:");
            } while (!int.TryParse(Console.ReadLine(), out vertice) || vertice <= 0 || vertice > matriz.Length);

            numVertices -= 1;

            for (int i = vertice - 1; i < matriz.Length - 1; i++)
            {
                matriz[i] = matriz[i + 1];
            }

            for (int i = 0; i < matriz.Length; i++)
            {
                for (int j = vertice - 1; j < matriz[i].Length - 1; j++)
                {
                    matriz[i][j] = matriz[i][j + 1];
                }
                Array.Resize(ref matriz[i], matriz[i].Length - 1);
            }

            Array.Resize(ref matriz, matriz.Length - 1);

        }

        public static void AdicionarAresta(MatrizAdjacencia matrizAdj)
        {
            int verticeOrigem, verticeDestino;
            do
            {
                Console.WriteLine("Digite a origem da aresta:");
            } while (!int.TryParse(Console.ReadLine(), out verticeOrigem) || verticeOrigem <= 0 || verticeOrigem > matrizAdj.matriz.Length);

            do
            {
                Console.WriteLine("Digite o destino da aresta:");
            } while (!int.TryParse(Console.ReadLine(), out verticeDestino) || verticeDestino <= 0 || verticeDestino > matrizAdj.matriz.Length);

            matrizAdj.AdicionarAresta(verticeOrigem, verticeDestino);
        }

        private void BuscaEmProfundidadeRecursiva(int vertice, bool[] visitado)
        {
            visitado[vertice] = true;
            Console.Write(vertice + " ");


            for (int i = 0; i < numVertices; i++)
            {

                if (vertice > numVertices || vertice < 0)
                {
                    return;
                }
                if (matriz[vertice][i] == 1 && !visitado[i])
                {
                    BuscaEmProfundidadeRecursiva(i, visitado);
                }
            }
        }

        public void BuscaEmProfundidade()
        {
            int verticeOrigem;
            do
            {
                Console.WriteLine("\nDigite a vertice inicial:");
            } while (!int.TryParse(Console.ReadLine(), out verticeOrigem) || verticeOrigem <= 0 || verticeOrigem > matriz.Length);

            bool[] visitado = new bool[numVertices];
            Console.WriteLine("Sequência DFS a partir do vértice " + verticeOrigem + ":");
            BuscaEmProfundidadeRecursiva(verticeOrigem, visitado);
            Console.WriteLine();
        }

        public void BuscaEmLargura()
        {
            int verticeOrigem;
            do
            {
                Console.WriteLine("\nDigite a vertice inicial:");
            } while (!int.TryParse(Console.ReadLine(), out verticeOrigem) || verticeOrigem <= 0 || verticeOrigem > matriz.Length - 1);

            bool[] visitado = new bool[numVertices];
            Queue<int> fila = new Queue<int>();
            visitado[verticeOrigem - 1] = true;
            fila.Enqueue(verticeOrigem);

            Console.WriteLine("\nSequência BFS a partir do vértice " + verticeOrigem + ":");
            while (fila.Count != 0)
            {
                int vertice = fila.Dequeue();
                Console.Write(vertice + " ");

                for (int i = 0; i < numVertices; i++)
                {
                    if (vertice > numVertices || vertice < 0)
                    {
                        return;
                    }
                    if (matriz[vertice ][i] == 1 && !visitado[i])
                    {
                        visitado[i] = true;
                        fila.Enqueue(i);
                    }
                }
            }
            Console.WriteLine();
        }

        private void BuscaEmProfundidadeConexo(int vertice, bool[] visitado)
        {
            visitado[vertice] = true;

            for (int i = 0; i < numVertices; i++)
            {                
                if (matriz[vertice][i] == 1 && visitado[i] == false)
                {
                    BuscaEmProfundidadeConexo(i, visitado);
                }
            }
        }

        public void EConexo()
        {
            bool[] visitado = new bool[numVertices];
            EncontrarSubgrafosConexos();
            List<int> verticesConexos = new List<int>();
            List<int> verticesNaoConexos = new List<int>();

            for (int i = 0; i < numVertices; i++)
            {
                if (visitado[i])
                    verticesConexos.Add(i);
                else
                    verticesNaoConexos.Add(i);
            }
            if (verticesConexos.Any())
            {
                Console.WriteLine("\n");
                Console.Write("Vértices Conexos:");

                foreach (int vertice in verticesConexos)
                {
                    Console.Write((vertice + 1) + " ");
                }
                Console.WriteLine("\n");
            }

            if (verticesNaoConexos.Any())
            {
                Console.WriteLine("\nVértices não Conexos:");

                foreach (int vertice in verticesNaoConexos)
                {
                    Console.Write((vertice + 1) + " ");
                }
                Console.WriteLine("\n");
                EncontrarSubgrafosConexos();
            }          
           
        }

        public void EncontrarSubgrafosConexos()
        {
            bool[] visitado = new bool[numVertices];
            List<List<int>> grafosConexos = new List<List<int>>();

            for (int i = 0; i < numVertices; i++)
            {
                if (!visitado[i])
                {
                    List<int> conexo = new List<int>();
                    List<int> naoConexo = new List<int>();

                    BuscaEmProfundidadeConexo(i, visitado);

                    for (int j = 0; j < numVertices; j++)
                    {
                        if (visitado[j])                        
                            conexo.Add(j);                        
                        else
                            naoConexo.Add(i);
                    }
                    grafosConexos.Add(conexo);
                }
            }
            if (grafosConexos.Any())
            {
                ImprimeSubGrafosConexos(grafosConexos);
            }           
        }
        public void ImprimeSubGrafosConexos(List<List<int>> subgrafosConexos)
        {
            Console.WriteLine("\nSubgrafo");
            for (int i = 0; i < subgrafosConexos.Count; i++)
            {
                Console.Write((i + 1) + " ");

            }
            Console.WriteLine("\n");
        }
    }

}
