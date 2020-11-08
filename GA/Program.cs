using System;

namespace GA
{
    public class Program
    {
        static public Random random;
        static GeneticAlgorithm<char> genericAlgorithm;
        static readonly string validCharacer = "abcdefghik ";
        static private string targetString = "ghabkii";
        static int populationSize = 200;
        static float mutationRate = 0.01f;
        static int elitism = 5;
        static string best = "";

        static void Main(string[] args)
        {
            random = new Random();
            genericAlgorithm= new GeneticAlgorithm<char>(populationSize, targetString.Length, random, getRandomCharacter, 
                fitnessFunction, elitism, mutationRate);
            Console.WriteLine(targetString);
            Console.WriteLine(best.ToString());


            while (best != targetString)
            {
                Update();
            }
            Console.ReadLine();
        }

        /*
         * write dna
         */
        static void Update()
        {

            char[] bestGenes = genericAlgorithm.BestGenes;
            best = "";
            for(int i =0; i < bestGenes.Length; i++)
            {
                best += bestGenes[i];
            }

            genericAlgorithm.NewGeneration();
            Console.WriteLine("==============================================");

            Console.WriteLine(targetString);
            Console.WriteLine(best);
            Console.WriteLine(genericAlgorithm.Generation);
            Console.WriteLine(genericAlgorithm.fitnessSum);
            Console.WriteLine(genericAlgorithm.BestFitness);

            for (int i = 0; i < genericAlgorithm.BestGenes.Length; i++)
            Console.Write(genericAlgorithm.BestGenes[i]);
            Console.WriteLine("==============================================");


        }

        /*
         * get a random gene 
         */
        static private char getRandomCharacter()
        {
            int i = random.Next(validCharacer.Length);
            return validCharacer[i];
        }

        /*
         * get rate fitness of dna
         */
        static private float fitnessFunction(int index)
        {
            float score = 0;
            DNA<char> dna = genericAlgorithm.Population[index];

            for (int i = 0; i < dna.Genes.Length; i++)
            {
                if (dna.Genes[i] == targetString[i])
                {
                    score += 1;
                }
            }

            score /= targetString.Length;
            score = (float)((Math.Pow(2, score) - 1) / (2 - 1));
 
            return score;
        }
    }
}
