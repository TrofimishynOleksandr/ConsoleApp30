namespace ConsoleApp2
{
    internal class Program
    {
        public static void NumbersRange(int start, int end)
        {
            for(int i = start; i <= end; i++)
            {
                Console.WriteLine(i);
                //Thread.Sleep(200);
            }
        }

        public static void Task5()
        {
            List<int> ints = new List<int>();
            try
            {
                StreamWriter sw = new StreamWriter("D:\\test.txt");
                Random rand = new Random();
                for (int i = 0; i < 10000; i++)
                {
                    int number = rand.Next(-50000, 50000);
                    ints.Add(number);
                    sw.WriteLine(number);
                }

                int max = 0, min = 0;
                double avg = 0;

                Thread findMax = new Thread(() => { max = ints.Max(); });
                Thread findMin = new Thread(() => { min = ints.Min(); });
                Thread findAvg = new Thread(() => { avg = ints.Average(); });

                findMax.Start();
                findMin.Start();
                findAvg.Start();

                findMax.Join();
                findMin.Join();
                findAvg.Join();

                sw.WriteLine($"Max: {max}, min: {min}, avg: {avg}");
                sw.Close();
            }
            catch { }
        }

        static void Main(string[] args)
        {
            //1
            Thread thread1 = new Thread(() => NumbersRange(0, 50));
            thread1.Start();
            thread1.Join();

            //2
            int start, end;
            do
            {
                Console.Write("Enter start of range: ");
            } while (!int.TryParse(Console.ReadLine(), out start));
            do
            {
                Console.Write("Enter end of range: ");
            } while (!int.TryParse(Console.ReadLine(), out end));

            Thread thread2 = new Thread(() => NumbersRange(start, end));
            thread2.Start();
            thread2.Join();

            //3 Діапазон не розбивається правильно на частини, виводиться тільки остання частина, наприклад 4 потоки від 0 до 100 -> всі виводять від 75 до 100
            Console.Write("Enter number of threads: ");
            int threadsAmount;
            while (!int.TryParse(Console.ReadLine(), out threadsAmount) || threadsAmount <= 0);

            do {
                Console.Write("Enter start of range: ");
            } while (!int.TryParse(Console.ReadLine(), out start));
            do {
                Console.Write("Enter end of range: ");
            } while (!int.TryParse(Console.ReadLine(), out end) || end < start);

            double difference = end - start;
            double numbersPerThread = difference / threadsAmount;

            for (int i = 0; i < threadsAmount - 1; i++)
            {
                new Thread(() => NumbersRange(start + (int)numbersPerThread * i, start + (int)numbersPerThread * (i+1))).Start();
            }
            new Thread(() => NumbersRange(start + (int)numbersPerThread * (threadsAmount - 1), end)).Start();

            //4-5
            Thread thread5 = new Thread(Task5);
            thread5.Start();
        }
    }
}
