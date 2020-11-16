using System;
using TestShedule;


namespace TestShedule
{
	public class DNA
	{

		/*
		 * A gene is a region of DNA that encodes function. A chromosome consists of a long strand of DNA containing 
		 */
		public ClassOfTimetable[] Genes { get; private set; }
		public float Fitness { get; private set; }

		private Random random;
		private Func<ClassOfTimetable> getRandomGene;
		private Func<int, float> fitnessFunction;


		/*
		 * int size: length of genes
		 * Random random: random to crossover
		 * Func<T> getRandomeGene: auto generate a gene
		 * fitnessFunction: 
		 * bool shouldInitGenes: decide generate genes or not
		 * */
		public DNA(int size, Random random, Func<ClassOfTimetable> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
		{
			Genes = new ClassOfTimetable[size];
			this.random = random;
			this.getRandomGene = getRandomGene;
			this.fitnessFunction = fitnessFunction;

			if (shouldInitGenes)
			{
				for (int i = 0; i < Genes.Length; i++)
				{
					Genes[i] = getRandomGene();

				}
			}
		}
		/*
		 * natural selection
		 * calculate the fitness of the individual
		 */

		public float CalculateFitness(int index)
		{
			Fitness = fitnessFunction(index);
			return Fitness;
		}

		/*
		 * one point crosser
		 * return a new object of type DNA by joining this object with another one (return child of mother and father)
		 * */

		public DNA Crossover(DNA otherParent)
		{
			DNA child = new DNA(Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

			for (int i = 0; i < Genes.Length; i++)
			{
				child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
			}

			return child;
		}

		/*
		 * need to know how to likely it is to mutate gene
		 */
		public void Mutate(float mutationRate)
		{
			for (int i = 1; i < Genes.Length - 1; i++)
			{
				if (random.NextDouble() < mutationRate)
				{
					Genes[i] = getRandomGene();
				}
			}
		}
	}
}