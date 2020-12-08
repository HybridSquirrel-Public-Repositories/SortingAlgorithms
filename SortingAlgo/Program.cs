using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SortingAlg
{
    class Program
    {
        public Person[] people = new Person[10];
        
        

        static void Main()
        {
            Program p = new Program();
            genArray(p.people);
            p.Run();
        }

        void Run()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Need help, Write 'help'");
            while (true)
            {
                
                Console.Write("$> ");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "help":
                        Console.WriteLine("\nprint\nname\nage\ncomb\ncocktail\nradix\nempty\nprintlist\n");
                        break;
                    case "print":
                        Sort.Print(people);
                        break;
                    case "name":
                        Sort.SelectionSort(people); //Complexity: O(n^2)
                        Sort.Print(people);
                        break;
                    case "age":
                        Sort.BubbleSort(people); //Complexity: O(n*n) Best: O(n) Worst: O(n*n)
                        Sort.Print(people);
                        break;
                    case "comb":
                        Sort.CombSort(people); //Complexity: O(n^2) Best: O(n) Worst: O(n^2)
                        Sort.Print(people);
                        break;
                    case "cocktail":
                        Sort.CocktailSort(people); //Complexity: O(n*n) Best: O(n) Worst: O(n*n)
                        Sort.Print(people);
                        break;
                    case "radix":
                        int n = people.Length; //Complexity: O(n)
                        Sort.RadixSort(people, n);
                        Sort.Print(people);
                        break;
                   
                }
            }
        }
        
        static void genArray(Person[] people)
        {
            string[] arrs = {"Vishnu", "Saule", "Walahfrid", "Grimhildr", "Lysandra", "Anath", "Vikingr", "Jordanes", "Medb", "Teshub"};
            int[] arri = { 64, 34, 25, 12, 22, 11, 90, 42, 44, 54 };

            for (int i = 0; i < 10; i++)
            {
                Person per = new Person(arrs[i], arri[i]);

                people[i] = per;

            }

        }
    }

    class Person
    {
        public string name;
        public int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

       
    }

  
    class Sort
    {



        public static void Print(Person[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("\nPerson:" + arr[i].name + ", Age:" + arr[i].age);
            }
        }
        public static void BubbleSort(Person[] arr)
        {
            int count = arr.Length;
            for (int i = 0; i < count - 1; i++)
                for (int j = 0; j < count - 1; j++)
                {
                    if (arr[j].age < arr[j + 1].age)
                    {
                        Person temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
        }
        public static void SelectionSort(Person[] arr)
        {
            int count, miniPos;
            count = arr.Length;
            for (int i = 0; i < count - 1; i++)
            {
                miniPos = i;
                for (int j = i + 1; j < count; j++)
                {
                    if (arr[j].name.CompareTo(arr[miniPos].name) < 0)
                    {
                        miniPos = j;
                    }
                }
                Person temp = arr[miniPos];
                arr[miniPos] = arr[i];
                arr[i] = temp;
            }
        }
        private static int CombCount(int count)
        {
            count = (count * 10) / 13;
            if (count < 1)
                return 1;
            return count;
        }
        public static void CombSort(Person[] arr)
        {
            int n = arr.Length;
            int count = n;
            bool swapper = true;
            while (count != 1 || swapper == true)
            {
                count = CombCount(count);
                swapper = false;
                for (int i = 0; i < n - count; i++)
                {
                    if (arr[i].age > arr[i + count].age)
                    {
                        Person temp = arr[i];
                        arr[i] = arr[i + count];
                        arr[i + count] = temp;
                        swapper = true;
                    }
                }
            }
        }
        public static void CocktailSort(Person[] arr)
        {
            bool mixer = true;
            int shake = 0;
            int end = arr.Length;

            while (mixer == true)
            {
                mixer = false;
                for (int i = shake; i < end - 1; ++i)
                {
                    if (arr[i].name.Length > arr[i + 1].name.Length)
                    {
                        Person temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        mixer = true;
                    }
                }
                if (mixer == false)
                    break;
                mixer = false;
                end = end - 1;

                for (int i = end - 1; i >= shake; i--)
                {
                    if (arr[i].name.Length > arr[i + 1].name.Length)
                    {
                        Person temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        mixer = true;
                    }
                }
                shake = shake + 1;
            }
        }
        private static int RadixMax(Person[] arr, int n)
        {
            int max = arr[0].age;
            for (int i = 1; i < n; i++)
                if (arr[i].age > max)
                    max = arr[i].age;
            return max;
        }
        private static void RadixCount(Person[] arr, int n, int expo)
        {
            int[] output = new int[n];
            int[] count = new int[10];
            int i;
            for (i = 0; i < 10; i++)
                count[i] = 0;
            for (i = 0; i < n; i++)
                count[(arr[i].age / expo) % 10]++;
            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];
            for (i = n - 1; i >= 0; i--)
            {
                output[count[(arr[i].age / expo) % 10] - 1] = arr[i].age;
                count[(arr[i].age / expo) % 10]--;
            }
            for (i = 0; i < n; i++)
                arr[i].age = output[i];
        }
        public static void RadixSort(Person[] persArr, int n)
        {
            int max = RadixMax(persArr, n);
            for (int expo = 1; max / expo > 0; expo *= 10)
                RadixCount(persArr, n, expo);
        }

    }


}


