using Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var tasks = new List<Task>();
            var bank = new BankAccount();

            for(int i=0; i<10; i++)
            {
                tasks.Add(Task.Factory.StartNew( () =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bank.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew( () =>
                {
                    for (int k = 0; k < 1000; k++)
                    {
                        bank.Draw(100);
                    }
                }));
                
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Task count {tasks.Count}");

            Console.WriteLine($"Final balance is {bank.Balance}");


            //Inicializacion de tarea mediante valores predeterminados Run

            var t = Task<int>.Run(() => {
                int max = 1000000;
                int ctr = 0;
                for (ctr = 0; ctr <= max; ctr++)
                {
                    if (ctr == max / 2 && DateTime.Now.Hour <= 12)
                    {
                        ctr++;
                        break;
                    }
                }
                return ctr;
            });
            Console.WriteLine("Finished {0:N0} iterations.", t.Result);

            //Procesos con await, finalizacion requerida

            var task = new Task(() =>
            {
                Console.WriteLine("Tarea Interna 1");

                for (int x = 0; x < 5; x++)
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                        {
                            for (int j = 0; j < 100; j++)
                            {
                                bank.Deposit(100);
                            }
                        })); 
                }
                Task.WaitAll(tasks.ToArray());

                Console.WriteLine($"Task count {tasks.Count}");
                Console.WriteLine($"Final balance is {bank.Balance}");
            });

            task.Start();

            var task2 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Tarea interna 2");

            });
            task2.Start();

            await task;
            await task2;


            int resultSum = await SumAsync(2);
            Console.WriteLine("Resultado de suma " + resultSum);


            Console.ReadLine();


        }
        //Metodo asincrona Task
        //Para leer archivos, solicitudes a servicios o BD
        public static async Task<int> SumAsync(int num)
        {
            int num2 = 10;
            var task = new Task<int> (() => num + num2);
            task.Start();
            int result = await task;
            return result;
        }


        

    }
}
