using System;
using System.Collections.Generic;
using System.IO;
using TestShedule;


namespace TestShedule
{
    public class Program
    {
        static int MAX_LESSON = 4;
        static int MAX_DAY = 6;
        static int VAN_LESSON = 5;
        static int TOAN_LESSON = 4;
        static int LY_LESSON = 2;
        static int HOA_LESSON = 2;
        static int SU_LESSON = 2;
        static int DIA_LESSON = 2;
        static int ANH_LESSON = 2;
        static int THEDUC_LESSON = 2;
        static int TUCHON_LESSON = 2;
        static int GDCD_LESSON = 1;
        static int CONGNGHE_LESSON = 1;
        static int MYTHUAT_LESSON = 1;
        static int Biology_LESSON = 2;

        static string Literature = "Literature";
        static string Math = "Math";
        static string Physics = "Physics";
        static string Chemistry = "Chemistry";
        static string History = "History";
        static string Geography = "Geography";
        static string English = "English";
        static string PhysicalEducation = "Physical Education";
        static string Free = "Free";
        static string CivicEducation = "Civic Education";
        static string Technology = "Technology";
        static string FineArt = "Fine Art";
        static string Biology = "Biology";


        static List<string> mathStudent = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" };
        static List<string> physicsStudent = new List<string>() { "1", "12", "13", "14", "5", "16", "17", "8" };
        static List<string> chemistryStudent = new List<string>() { "11", "22", "3", "14", "15", "26", "7", "8" };
        static List<string> biologyStudent = new List<string>() { "11", "22", "3", "14", "15", "6", "37", "18" };
        static List<string> literatureStudent = new List<string>() { "1", "12", "23", "4", "15", "6", "27", "38" };
        static List<string> historyStudent = new List<string>() { "11", "2", "23", "34", "15", "26", "17", "38" };
        static List<string> geographyStudent = new List<string>() { "21", "2", "3", "14", "5", "16", "17", "8" };
        static List<string> englishStudent = new List<string>() { "21", "2", "3", "14", "5", "16", "27", "28" };
        static List<string> civicEducationStudent = new List<string>() { "1", "12", "3", "14", "25", "26", "7", "8" };
        static List<string> technologyStudent = new List<string>() { "1", "2", "23", "4", "15", "6", "17", "28" };
        static List<string> physicalEducationStudent = new List<string>() { "21", "12", "3", "4", "15", "6", "7", "8" };
        static List<string> fineArtStudent = new List<string>() { "1", "2", "13", "4", "5", "26", "27", "8" };

        static List<string> listStudent = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" , "12", "13", "14", "16", "17",
        "11", "22",  "15", "26", "37", "18",  "23","27", "38",
        "21","28" ,  "25" };

        static public Random random;
        static GeneticAlgorithm genericAlgorithm;
        static int populationSize = 200;
        static float mutationRate = 0.1f;
        static int elitism = 5;
        static ClassOfTimetable[,] timetable;
        static List<ClassOfTimetable> validClass;


        static void Main(string[] args)
        {
            random = new Random();



            timetable = new ClassOfTimetable[MAX_DAY, MAX_LESSON];
            Console.WriteLine(MAX_DAY * MAX_LESSON);
            validClass = new List<ClassOfTimetable>();
            validClass.Add(new ClassOfTimetable(idClass: "T-1", idLecture: "1", lesson: TOAN_LESSON, idSubject: "Math"));
            validClass.Add(new ClassOfTimetable(idClass: "L-1", idLecture: "1", lesson: LY_LESSON, idSubject: "Physics"));
            validClass.Add(new ClassOfTimetable(idClass: "H-1", idLecture: "3", lesson: HOA_LESSON, idSubject: "Chemistry"));
            validClass.Add(new ClassOfTimetable(idClass: "S-1", idLecture: "5", lesson: Biology_LESSON, idSubject: "Biology"));
            validClass.Add(new ClassOfTimetable(idClass: "V-1", idLecture: "1", lesson: VAN_LESSON, idSubject: "Literature"));
            validClass.Add(new ClassOfTimetable(idClass: "LS-1", idLecture: "1", lesson: SU_LESSON, idSubject: "History"));
            validClass.Add(new ClassOfTimetable(idClass: "Đ-1", idLecture: "6", lesson: DIA_LESSON, idSubject: "Geography"));
            validClass.Add(new ClassOfTimetable(idClass: "A-1", idLecture: "6", lesson: ANH_LESSON, idSubject: "English"));
            validClass.Add(new ClassOfTimetable(idClass: "GDCD-1", idLecture: "6", lesson: GDCD_LESSON, idSubject: "Civic Education"));
            validClass.Add(new ClassOfTimetable(idClass: "CN-1", idLecture: "6", lesson: CONGNGHE_LESSON, idSubject: "Technology"));
            validClass.Add(new ClassOfTimetable(idClass: "TD-1", idLecture: "6", lesson: THEDUC_LESSON, idSubject: "Physical Education"));
            validClass.Add(new ClassOfTimetable(idClass: "MT-1", idLecture: "6", lesson: MYTHUAT_LESSON, idSubject: "Fine Art"));

        A: genericAlgorithm = new GeneticAlgorithm(populationSize: populationSize, dnaSize: 30, random, getRandomGene,
                fitnessFunction, elitism, mutationRate);



            while (!genericAlgorithm.isStop)
            {
                Update();
            }
            outGene(genericAlgorithm.BestGenes);
            export(genericAlgorithm.BestGenes);

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                Console.WriteLine(keyinfo.Key + " was pressed");
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    genericAlgorithm.isStop = false;
                    goto A;
                }
                else if (keyinfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);
            Console.ReadLine();
        }

        /*
         * write dna
         */
        static void Update()
        {
            genericAlgorithm.NewGeneration();
        }

        /*
         * get a random gene 
         */
        static private ClassOfTimetable getRandomGene()
        {
            int i = random.Next(validClass.Count);
            int max = random.Next(MAX_DAY * MAX_LESSON);
            validClass[i].idTime = max;
            return validClass[i];
        }

        /*
         * get rate fitness of dna
         */
        static private float fitnessFunction(int index)
        {
            float score = 0f;
            bool isValid = true;
            DNA dna = genericAlgorithm.Population[index];

            for (int i = 0; i < listStudent.Count; i++)
            {
                if (validStudent(listStudent[i]))
                    score++;
            }

            if (score == listStudent.Count)
            {
                dna.Genes.CopyTo(genericAlgorithm.BestGenes, 0);
                if (isValid == true)
                {
                    genericAlgorithm.isStop = true;
                }
                outGene(dna.Genes);
            }
            score /= listStudent.Count;
            return score;
        }

        static public void outGene(ClassOfTimetable[] genes)
        {

            Console.WriteLine();


            Console.WriteLine("==============================================");
            Console.WriteLine(genericAlgorithm.Generation);
            Console.WriteLine(genericAlgorithm.fitnessSum);
            Console.WriteLine(genericAlgorithm.BestFitness);
            Console.WriteLine("TIME");
            DateTime dateTime = new DateTime(2020, 10, 10);

            foreach (ClassOfTimetable cl in genes)
            {
                Console.WriteLine(cl.idSubject + "-" + getDate(cl.idTime, dateTime));
            }
            Console.WriteLine("==============================================");
            Console.WriteLine();

        }

        static void export(ClassOfTimetable[] genes)
        {
            Console.WriteLine(genericAlgorithm.Generation);
            Console.WriteLine(genericAlgorithm.fitnessSum);
            Console.WriteLine(genericAlgorithm.BestFitness);
            StreamWriter outputFitness = new StreamWriter(System.IO.Path.Combine(@"C:\Users\17520\Downloads\", "fitness.csv"));
            outputFitness.WriteLine("{0},{1}",
                "Subject","Time");
            Console.WriteLine();
            List<ClassOfTimetable> list = new List<ClassOfTimetable>();
            bool isValid = true;
            for (int i = 0; i < MAX_DAY * MAX_LESSON; i++)
            {
                isValid = true;
                for (int j = 0; j < list.Count; j++)
                {
                    if (genes[i].idSubject == list[j].idSubject)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid == true)
                {
                    list.Add(genes[i]);
                    list.Sort(ClassOfTimetable.CompareClass);
                }
            }
            DateTime dateTime = new DateTime(2020, 10, 10);
            
            foreach(ClassOfTimetable cl in list){
                Console.WriteLine(cl.idSubject + "-" + getDate(cl.idTime, dateTime));
                outputFitness.WriteLine("{0},{1}",
                    cl.idSubject, getDate(cl.idTime, dateTime));
            }
            if (outputFitness != null)
                outputFitness.Close();
            Console.WriteLine("Export sucess");
        }
        static bool validStudent(string student)
        {
            List<ClassOfTimetable> classS = new List<ClassOfTimetable>();
            if (mathStudent.Contains(student))
            {

                classS.Add(getClass(Math));
            }
            if (literatureStudent.Contains(student))
            {
                classS.Add(getClass(Literature));
            }

            if (physicsStudent.Contains(student))
            {
                classS.Add(getClass(Physics));
            }

            if (physicalEducationStudent.Contains(student))
            {
                classS.Add(getClass(PhysicalEducation));
            }
            if (geographyStudent.Contains(student))
            {
                classS.Add(getClass(Geography));
            }
            if (historyStudent.Contains(student))
            {
                classS.Add(getClass(History));
            }
            if (biologyStudent.Contains(student))
            {
                classS.Add(getClass(Biology));
            }
            if (englishStudent.Contains(student))
            {
                classS.Add(getClass(English));
            }
            if (chemistryStudent.Contains(student))
            {
                classS.Add(getClass(Chemistry));
            }
            if (civicEducationStudent.Contains(student))
            {
                classS.Add(getClass(CivicEducation));
            }
            if (technologyStudent.Contains(student))
            {
                classS.Add(getClass(Technology));
            }
            if (fineArtStudent.Contains(student))
            {
                classS.Add(getClass(FineArt));
            }

            for (int i = 0; i < classS.Count; i++)
            {
                for (int j = i + 1; j < classS.Count; j++)
                {
                    if (classS[i].idTime == classS[j].idTime)
                        return false;
                }
            }

            return true;

        }

        static ClassOfTimetable getClass(string name)
        {
            for (int i = 0; i < validClass.Count; i++)
            {
                if (validClass[i].idSubject == name)
                {
                    return validClass[i];
                }
            }
            return null;
        }

        static string getDate(int lesson, DateTime startDate)
        {
            DateTime x = startDate;
            int day = 0;
            int time = 0;
            day =lesson / 4;
            switch (lesson % 4)
            {
                case 0:
                    time = 7;
                    break;
                case 1:
                    time = 9;
                    break;
                case 2:
                    time = 13;
                    break;
                case 3:
                    time = 15;
                    break;

            }
            x = startDate.AddDays(day).AddHours(time);
            return x.ToShortDateString() + " " + x.ToShortTimeString();
        }
    }
  

}
