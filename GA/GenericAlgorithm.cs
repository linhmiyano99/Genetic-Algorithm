using System;
using System.Collections.Generic;

public class GeneticAlgorithm<T>
{
	public List<DNA<T>> Population { get; private set; }
	public int Generation { get; private set; }
	public float BestFitness { get; private set; }
	public T[] BestGenes { get; private set; }

	public int Elitism;
	public float MutationRate;

	private List<DNA<T>> newPopulation;
	private Random random;
	public  float fitnessSum;
	private int dnaSize;
	private Func<T> getRandomGene;
	private Func<int, float> fitnessFunction;

	public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction,
		int elitism, float mutationRate = 0.01f)
	{
		Generation = 1;
		Elitism = elitism;
		MutationRate = mutationRate;
		Population = new List<DNA<T>>(populationSize);
		newPopulation = new List<DNA<T>>(populationSize);
		this.random = random;
		this.dnaSize = dnaSize;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;

		BestGenes = new T[dnaSize];

		for (int i = 0; i < populationSize; i++)
		{
			Population.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
		}
	}

	/*
	 * create new generation
	 * int numNewDNA :number of new dna
	 * bool crossoverNewDNA : crossover or not
	 */
	public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
	{
		/*
		 * new size of population = popution size + number of new dna
		 */
		int finalCount = Population.Count + numNewDNA;

		if (finalCount <= 0)
		{
			return;
		}

		/*
		 * sort population
		 */
		if (Population.Count > 0)
		{
			CalculateFitness();
			/*
			 * sort by fitness
			 */
			Population.Sort(CompareDNA);
		}

		/*
		 * clear new polution to update
		 * Elitism :keep to copy to new population
		 */
		newPopulation.Clear();

		for (int i = 0; i < finalCount; i++)
		{
			if (i < Elitism && i < Population.Count)
			{
				newPopulation.Add(Population[i]);
			}
			else if (i < Population.Count || crossoverNewDNA)
			{
				DNA<T> parent1 = ChooseParent();
				DNA<T> parent2 = ChooseParent();

				DNA<T> child = parent1.Crossover(parent2);

				child.Mutate(MutationRate);

				newPopulation.Add(child);
			}
			else
			{
				newPopulation.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
			}
		}

		List<DNA<T>> tmpList = Population;
		Population = newPopulation;
		newPopulation = tmpList;

		Generation++;
	}

	/*
	 * compare DNA follow fitness
	 */
	private int CompareDNA(DNA<T> a, DNA<T> b)
	{
		if (a.Fitness > b.Fitness)
		{
			return -1;
		}
		else if (a.Fitness < b.Fitness)
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	/*
	 * Calculate Fitness
	 * get sum fitness to chosse parent
	 * update best fitness
	 */
	private void CalculateFitness()
	{

		/*
		 * fitnessSum: sum all fitness
		 */
		fitnessSum = 0;

		DNA<T> best = Population[0];

		for (int i = 0; i < Population.Count; i++)
		{
			fitnessSum += Population[i].CalculateFitness(i);

			if (Population[i].Fitness > best.Fitness)
			{
				best = Population[i];
			}
		}

		/*
		 * update best fitness and best gene
		 */
		BestFitness = best.Fitness;
		best.Genes.CopyTo(BestGenes, 0);
	}

	/*
	 * choose parent
	 * Roulette Wheel Selection
	 * https://www.tutorialspoint.com/genetic_algorithms/genetic_algorithms_parent_selection.htm
	 */
	private DNA<T> ChooseParent()
	{
		double randomNumber = random.NextDouble() * fitnessSum;

		for (int i = 0; i < Population.Count; i++)
		{
			if (randomNumber < Population[i].Fitness)
			{
				return Population[i];
			}

			randomNumber -= Population[i].Fitness;
		}

		return null;
	}
}
