using System;
using System.Linq;
using System.Threading;

namespace FindSmallest
{
    class Program
    {
        private static int[] Results = new int[5];
        

        private static readonly int[][] Data = new int[][]{
            new[]{1,5,4,2}, 
            new[]{3,2,4,11,4},
            new[]{33,2,3,-1, 10},
            new[]{3,2,8,9,-1},
            new[]{1, 22,1,9,-3, 5}
        };

        
        

        private static int FindSmallest(int[] numbers)
        {
            if (numbers.Length < 1)
            {
                throw new ArgumentException("There must be at least one element in the array");
            }
            //Sikre at selvom number er større end smallestSoFar, så bliver den indsat i Results (Fordi smallestSoFar starter på 0)
            int workingThread = (int.Parse(Thread.CurrentThread.Name));
            int smallestSoFar = numbers[0];
            Program.Results[workingThread] = numbers[workingThread];

            foreach (int number in numbers)
            {
                if (number < smallestSoFar)
                {
                    
                    Console.WriteLine(Thread.CurrentThread.Name + " Found a smaller number: " + number + " which is smaller than " + smallestSoFar);
                    smallestSoFar = number;
                    
                    Program.Results[workingThread] = smallestSoFar; //Indsætter værdi

                    //Console.WriteLine(Thread.CurrentThread.Name + " Found a smaller number: " + smallestSoFar);
                }
            }
            return smallestSoFar;
        }

        static void Main()
        {

            Thread t1 = new Thread(() => FindSmallest(Data[0]));
            Thread t2 = new Thread(() => FindSmallest(Data[1]));
            Thread t3 = new Thread(() => FindSmallest(Data[2]));
            Thread t4 = new Thread(() => FindSmallest(Data[3]));
            Thread t5 = new Thread(() => FindSmallest(Data[4]));

            

            t1.Name = "0";
            t2.Name = "1";
            t3.Name = "2";
            t4.Name = "3";
            t5.Name = "4";

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();

            //Implementeret for at være sikker på tråde er færdige
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();

            Console.WriteLine("T0 threadstate " + t1.ThreadState.ToString()); 

       
            if (t1.ThreadState == ThreadState.Stopped && t2.ThreadState == ThreadState.Stopped && t3.ThreadState == ThreadState.Stopped
                && t4.ThreadState == ThreadState.Stopped && t5.ThreadState == ThreadState.Stopped)
            {
               //Get result
                Console.WriteLine("Thread #0 Result: " + Results[0]);
                Console.WriteLine("Thread #1 Result: " + Results[1]);
                Console.WriteLine("Thread #2 Result: " + Results[2]);
                Console.WriteLine("Thread #3 Result: " + Results[3]);
                Console.WriteLine("Thread #4 Result: " + Results[4]);

            }

           
        //    foreach (int[] d in Data)
        //    {
        //        int smallest = FindSmallest(d);
        //        Console.WriteLine("\t" + String.Join(", ", d) + "\n-> " + smallest);

        //        Console.ReadLine();  //For at se resultat
        //    }

            Console.ReadLine();
        }
    }
}
