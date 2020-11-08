using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace GA
{

    public class Program
    {
        static int MAX_LESSON = 5; 
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


        static public Random random;
        static GeneticAlgorithm genericAlgorithm;
        static int populationSize = 200;
        static float mutationRate = 0.1f;
        static int elitism = 5;
        static ClassOfTimetable[,] timetable;
        static ClassOfTimetable[] validClass;
        static void Main(string[] args)
        {
            random = new Random();
            validClass = new ClassOfTimetable[13];
            validClass[0] = new ClassOfTimetable(idClass: "T-1", idLecture: "1", lesson: TOAN_LESSON, idSubject: "Math");
            validClass[1] = new ClassOfTimetable(idClass: "L-1", idLecture: "1", lesson: LY_LESSON, idSubject: "Physics");
            validClass[2] = new ClassOfTimetable(idClass: "H-1", idLecture: "3", lesson: HOA_LESSON, idSubject: "Chemistry");
            validClass[3] = new ClassOfTimetable(idClass: "S-1", idLecture: "5", lesson: Biology_LESSON, idSubject: "Biology");
            validClass[4] = new ClassOfTimetable(idClass: "V-1", idLecture: "1", lesson: VAN_LESSON, idSubject: "Literature");
            validClass[5] = new ClassOfTimetable(idClass: "LS-1", idLecture: "1", lesson: SU_LESSON, idSubject: "History");
            validClass[6] = new ClassOfTimetable(idClass: "Đ-1", idLecture: "6", lesson: DIA_LESSON, idSubject: "Geography");
            validClass[7] = new ClassOfTimetable(idClass: "A-1", idLecture: "6", lesson: ANH_LESSON, idSubject: "English");
            validClass[8] = new ClassOfTimetable(idClass: "GDCD-1", idLecture: "6", lesson: GDCD_LESSON, idSubject: "Civic Education");
            validClass[9] = new ClassOfTimetable(idClass: "CN-1", idLecture: "6", lesson: CONGNGHE_LESSON, idSubject: "Technology");
            validClass[10] = new ClassOfTimetable(idClass: "TD-1", idLecture: "6", lesson: THEDUC_LESSON, idSubject: "Physical Education");
            validClass[11] = new ClassOfTimetable(idClass: "MT-1", idLecture: "6", lesson: MYTHUAT_LESSON, idSubject: "Fine Art");
            validClass[12] = new ClassOfTimetable(idClass: "TC-1", idLecture: "6", lesson: TUCHON_LESSON, idSubject: "Free");

            timetable = new ClassOfTimetable[MAX_DAY, MAX_LESSON];
            Console.WriteLine(MAX_DAY * MAX_LESSON);

        A: genericAlgorithm = new GeneticAlgorithm(populationSize: populationSize, dnaSize: 30, random, getRandomGene, 
                fitnessFunction, elitism, mutationRate);

           

             while(!genericAlgorithm.isStop)
            {
                Update();
            }
            outGene(genericAlgorithm.BestGenes);
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                Console.WriteLine(keyinfo.Key + " was pressed");
                if(keyinfo.Key == ConsoleKey.Enter)
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
            int i = random.Next(validClass.Length);
            return validClass[i];
        }

        /*
         * get rate fitness of dna
         */
        static private float fitnessFunction(int index)
        {
            float score = 0;
            bool isValid = true;
            DNA dna = genericAlgorithm.Population[index];
            int toan = 0, ly = 0, hoa = 0, van = 0, 
                su = 0, dia = 0, Biology = 0, anh = 0, 
                gdcn = 0, theduc = 0, tuchon = 0, 
                congnghe = 0, mythuat = 0, Biologyhoat = 0;
            for (int i = 0; i < MAX_DAY; i++)
            {

                for (int j = i * MAX_LESSON; j < i * MAX_LESSON + MAX_LESSON; j++)
                {
                    switch (dna.Genes[j].idClass)
                    {
                        case "T-1":
                            toan++;
                            break;
                        case "L-1":
                            ly++;
                            break;
                        case "H-1":
                            hoa++;
                            break;
                        case "V-1":
                            van++;
                            break;
                        case "LS-1":
                            su++;
                            break;
                        case "Đ-1":
                            dia++;
                            break;
                        case "S-1":
                            Biology++;
                            break;
                        case "A-1":
                            anh++;
                            break;
                        case "GDCD-1":
                            gdcn++;
                            break;
                        case "CN-1":
                            congnghe++;
                            break;
                        case "TD-1":
                            theduc++;
                            break;
                        case "TC-1":
                            tuchon++;
                            break;
                        case "SH-1":
                            Biologyhoat++;
                            break;
                        case "MT-1":
                            mythuat++;
                            break;
                    
                    }
                   
                }
                if (validDay(dna.Genes[i * MAX_LESSON], dna.Genes[i * MAX_LESSON + 1],
                    dna.Genes[i * MAX_LESSON + 2], dna.Genes[i * MAX_LESSON + 3],
                    dna.Genes[i * MAX_LESSON + 4]))
                {
                    score++;
                }
            }


            if (toan == TOAN_LESSON)
                score ++;
            if (ly == LY_LESSON)
                score ++;
            if (hoa == HOA_LESSON)
                score ++;
            if (Biology == Biology_LESSON)
                score ++;
            if (van == VAN_LESSON)
                score ++;
            if (su == SU_LESSON)
                score ++;
            if (dia == DIA_LESSON)
                score ++;
            if (anh == ANH_LESSON)
                score ++;
            if (theduc == THEDUC_LESSON)
                score ++;
            if (tuchon == TUCHON_LESSON)
                score ++;
            if (gdcn == GDCD_LESSON)
                score ++;
            if (mythuat == MYTHUAT_LESSON)
                score ++;
            if (congnghe == CONGNGHE_LESSON)
                score ++;
            if((toan == TOAN_LESSON) && (ly == LY_LESSON) 
                && (hoa == HOA_LESSON) && (Biology == Biology_LESSON) 
                && (van == VAN_LESSON) && (su == SU_LESSON)
                && (dia == DIA_LESSON) && (anh == ANH_LESSON)
                && (theduc == THEDUC_LESSON) && (tuchon == TUCHON_LESSON)
                && (mythuat == MYTHUAT_LESSON) && (congnghe == CONGNGHE_LESSON)
               )
            {
                dna.Genes.CopyTo(genericAlgorithm.BestGenes, 0);
                if (isValid == true)
                {
                    genericAlgorithm.isStop = true;
                }
                score += 10;
                outGene(dna.Genes);
            }
            score /= 100000.0f;

            score = (float)((Math.Pow(2, score) - 1) / (2 - 1));

            return score;
        }

        static public void outGene(ClassOfTimetable[] genes)
        {
            StreamWriter outputFitness = new StreamWriter(System.IO.Path.Combine(@"C:\Users\17520\Downloads\", "fitness.csv"));
            outputFitness.WriteLine("{0},{1},{2},{3},{4},{5},{6}", 
                "Lesson/Day", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");

            Console.WriteLine();


            Console.WriteLine("==============================================");
            Console.WriteLine(genericAlgorithm.Generation);
            Console.WriteLine(genericAlgorithm.fitnessSum);
            Console.WriteLine(genericAlgorithm.BestFitness);
            Console.WriteLine("TIME TABLE");
            for (int i = 0; i < MAX_DAY; i++)
            {
                Console.WriteLine("=====================================================");

                switch (i)
                {
                    case 0:
                        Console.WriteLine("[Monday]");
                        break;
                    case 1:
                        Console.WriteLine("[Tuesday]");
                        break;
                    case 2:
                        Console.WriteLine("[Wednesday]");
                        break;
                    case 3:
                        Console.WriteLine("[Thursday]");
                        break;
                    case 4:
                        Console.WriteLine("[Friday]");
                        break;
                    case 5:
                        Console.WriteLine("[Saturday]");
                        break;

                }
                for (int j = i * MAX_LESSON; j < i * MAX_LESSON + MAX_LESSON; j++)
                {
                    timetable[i, j % MAX_LESSON] = genes[j];
                    Console.Write(genes[j].idSubject);
                    Console.Write("|");
                }
                Console.WriteLine();

            }
            Console.WriteLine("==============================================");
            Console.WriteLine();
            for (int i = 0; i < MAX_LESSON; i++)
            {
                outputFitness.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
                    i + 1, timetable[0,i].idSubject, timetable[1,i].idSubject,
                    timetable[2,i].idSubject, timetable[3,i].idSubject,  
                    timetable[4,i].idSubject, timetable[5,i].idSubject);
                
            }
            if (outputFitness != null)
                outputFitness.Close();

        }

        static bool validDay(ClassOfTimetable class1, ClassOfTimetable class2, 
            ClassOfTimetable class3, ClassOfTimetable class4, ClassOfTimetable class5)
        {

            int toan = 0, ly = 0, hoa = 0, van = 0,
                su = 0, dia = 0, Biology = 0, anh = 0,
                gdcn = 0, theduc = 0, tuchon = 0,
                congnghe = 0, mythuat = 0, Biologyhoat = 0;

            switch (class1.idClass)
            {
                case "T-1":
                    toan++;
                    break;
                case "L-1":
                    ly++;
                    break;
                case "H-1":
                    hoa++;
                    break;
                case "V-1":
                    van++;
                    break;
                case "LS-1":
                    su++;
                    break;
                case "Đ-1":
                    dia++;
                    break;
                case "S-1":
                    Biology++;
                    break;
                case "A-1":
                    anh++;
                    break;
                case "GDCD-1":
                    gdcn++;
                    break;
                case "CN-1":
                    congnghe++;
                    break;
                case "TD-1":
                    theduc++;
                    break;
                case "TC-1":
                    tuchon++;
                    break;
                case "SH-1":
                    Biologyhoat++;
                    break;
                case "MT-1":
                    mythuat++;
                    break;

            }
            switch (class2.idClass)
            {
                case "T-1":
                    toan++;
                    break;
                case "L-1":
                    ly++;
                    break;
                case "H-1":
                    hoa++;
                    break;
                case "V-1":
                    van++;
                    break;
                case "LS-1":
                    su++;
                    break;
                case "Đ-1":
                    dia++;
                    break;
                case "S-1":
                    Biology++;
                    break;
                case "A-1":
                    anh++;
                    break;
                case "GDCD-1":
                    gdcn++;
                    break;
                case "CN-1":
                    congnghe++;
                    break;
                case "TD-1":
                    theduc++;
                    break;
                case "TC-1":
                    tuchon++;
                    break;
                case "SH-1":
                    Biologyhoat++;
                    break;
                case "MT-1":
                    mythuat++;
                    break;

            }
            switch (class3.idClass)
            {
                case "T-1":
                    toan++;
                    break;
                case "L-1":
                    ly++;
                    break;
                case "H-1":
                    hoa++;
                    break;
                case "V-1":
                    van++;
                    break;
                case "LS-1":
                    su++;
                    break;
                case "Đ-1":
                    dia++;
                    break;
                case "S-1":
                    Biology++;
                    break;
                case "A-1":
                    anh++;
                    break;
                case "GDCD-1":
                    gdcn++;
                    break;
                case "CN-1":
                    congnghe++;
                    break;
                case "TD-1":
                    theduc++;
                    break;
                case "TC-1":
                    tuchon++;
                    break;
                case "SH-1":
                    Biologyhoat++;
                    break;
                case "MT-1":
                    mythuat++;
                    break;

            }

            switch (class4.idClass)
            {
                case "T-1":
                    toan++;
                    break;
                case "L-1":
                    ly++;
                    break;
                case "H-1":
                    hoa++;
                    break;
                case "V-1":
                    van++;
                    break;
                case "LS-1":
                    su++;
                    break;
                case "Đ-1":
                    dia++;
                    break;
                case "S-1":
                    Biology++;
                    break;
                case "A-1":
                    anh++;
                    break;
                case "GDCD-1":
                    gdcn++;
                    break;
                case "CN-1":
                    congnghe++;
                    break;
                case "TD-1":
                    theduc++;
                    break;
                case "TC-1":
                    tuchon++;
                    break;
                case "SH-1":
                    Biologyhoat++;
                    break;
                case "MT-1":
                    mythuat++;
                    break;

            }
            switch (class5.idClass)
            {
                case "T-1":
                    toan++;
                    break;
                case "L-1":
                    ly++;
                    break;
                case "H-1":
                    hoa++;
                    break;
                case "V-1":
                    van++;
                    break;
                case "LS-1":
                    su++;
                    break;
                case "Đ-1":
                    dia++;
                    break;
                case "S-1":
                    Biology++;
                    break;
                case "A-1":
                    anh++;
                    break;
                case "GDCD-1":
                    gdcn++;
                    break;
                case "CN-1":
                    congnghe++;
                    break;
                case "TD-1":
                    theduc++;
                    break;
                case "TC-1":
                    tuchon++;
                    break;
                case "SH-1":
                    Biologyhoat++;
                    break;
                case "MT-1":
                    mythuat++;
                    break;

            }
            if(toan > 2 || van > 2 || ly > 1|| hoa > 1|| van > 1||
                su > 1|| dia > 1|| Biology > 1|| anh > 1||
                gdcn > 1|| theduc > 1|| tuchon > 1||
                congnghe > 1|| mythuat > 1|| Biologyhoat > 1)
            {
                return false;
            }
            if(toan == 2 || van == 2)
            {
                if(!(class1.idClass == class2.idClass || class2.idClass == class3.idClass || class3.idClass == class4.idClass || class4.idClass == class5.idClass))
                {
                    return false;
                }
            }
            return true;

        }
    }
}
