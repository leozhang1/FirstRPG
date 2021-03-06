﻿using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

public class IEnumerablePractice
{
    // myFilter1 takes a sequence of integers with values from 0 to 100
    // It removes all multiples of 7 greater than 75
    // Each number is then squared
    // Finall remove any resulting integer that is odd.
    public static IEnumerable<int> myFilter1(IEnumerable<int> input)
    {
        // DeMorgan's law
        // not (x % 7 == 0 && x > 75) ==> (x % 7 != 0 || x <= 75)

        // filter the sequence, then apply something to each value, then filter again
        return input
        .Where(x => x % 7 != 0 || x <= 75)
        .Select(x => x * x)
        .Where(x => x % 2 == 0);
    }

    // myFilter2 takes a sequence of integers with values from 0 to 100
    // It removes all multiples of 3 less than 35
    // Then cube each number
    // Finally remove any resulting integer that is even.
    public static IEnumerable<int> myFilter2(IEnumerable<int> input)
    {
        // DeMorgan's law
        // not (x%3 == 0 && x < 35) ==>  (x%3 != 0 || x >= 35)
        return input
        .Where(x => x % 3 != 0 || x >= 35)
        .Select(x => x * x * x)
        .Where(x => x % 2 == 1);
    }

    static double[] data = new double[10000000];

    static string divBegin = "<div id=\"text\">";
    static string divEnd = "</div>";

    // [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        try
        {
            WebClient wc = new WebClient();
            string htmlData =
            wc.DownloadString("http://rickleinecker.com/Rick-Leinecker-Magazine-Articles-and-Writing.html");

            print(htmlData);

            int index = htmlData.IndexOf(divBegin);

            while (index >= 0)
            {
                int endIndex = htmlData.IndexOf(divEnd, index);

                // information between the beginning div and end div tag
                string fText =
                htmlData.Substring(index + divBegin.Length, endIndex - (index + divBegin.Length)).Trim();

                String[] words = fText.Split(' ');

                // check if 'Compute' was there
                if (words[0][0] != 'C')
                    break;

                print("Magazine:" + words[0] + ", Number:" + words[1] + ", Date:" + words[2]);

                /// <summary>
                /// moves to the next div tag
                /// </summary>
                /// <returns></returns>
                index = htmlData.IndexOf(divBegin, endIndex);
            }
        }
        catch(Exception e)
        {
            print(e.StackTrace);
            return;
        }


    }

    #region

    // #1
        // Important to seed with 5 for repeatability.
        // System.Random rnd = new System.Random(5);

        // Produces a list of 100 random numbers.
        // var listForProblem = Enumerable.Range(1, 100).Select(i => rnd.Next() % 101);

        // var firstResults = myFilter1(listForProblem);

        // Show the results.
        // print(String.Join(" ", firstResults));

        // var secondResults = myFilter2(listForProblem);
        // Show the results.
        // print(String.Join(" ", secondResults));

        // #2
        // WebClient client = new WebClient();
        // string url = "http://rickleinecker.com/Rick-Leinecker-Magazine-Articles-and-Writing.html";

        // string text = client.DownloadString(url);

        // print(text);

        // for each string
        // print ($"Magazine: COMPUTE!, Number: {num}, Date: {date}")


        // #3


        // single threaded
        // Stopwatch stopwatch = new Stopwatch();
        // stopwatch.Start();
        // for (int i = 0; i < data.Length; ++i)
        // {
        //     data[i] = Math.Sin(i) + Math.Cos(i) + Math.Tan(i)
        //             + Math.Sinh(i) + Math.Cosh(i) + Math.Tanh(i);
        // }
        // stopwatch.Stop();
        // print("time in miliseconds: " + stopwatch.ElapsedMilliseconds);

        // Thread[] threads = new Thread[4];
        // int upperBound = data.Length / threads.Length;
        // print(upperBound);

        // stopwatch.Start();

        // for (int i = 0; i < threads.Length; ++i)
        // {
        //     // the i passed into longtask isn't working, so we make a local tmp variable and make it work
        //     int j = i;
        //     threads[i] = new Thread( delegate () { LongTask(j * upperBound, upperBound); } );
        //     // threads[i] = new Thread(() => LongTask(j * upperBound, upperBound));
        //     threads[i].Start();
        // }

        // foreach (Thread t in threads)
        // {
        //     t.Join();
        // }
        // stopwatch.Stop();

        // print("time in miliseconds: " + stopwatch.ElapsedMilliseconds);

    #endregion

    static void LongTask(int startingIndex, int reps)
    {
        for (int idx = startingIndex; idx < startingIndex + reps; ++idx)
        {
            data[idx] = (int)(Math.Sin(idx) + Math.Cos(idx) + Math.Tan(idx)
                    + Math.Sinh(idx) + Math.Cosh(idx) + Math.Tanh(idx));
        }
    }

    static void print(dynamic msg)
    {
        UnityEngine.Debug.Log(msg);
    }
}


// string searchString = "k";
// string s1 = "seek" ;
// string s2 = "leineker";

// returns the index of the a specified string.
// the specified starting index is used to improved search times
// for super long strings e.g. if I want ot search for "f" in "abcdefg",
// i can say "abcdefg".indexOf("f", 4), which would start the search at "e" instead
// of "abcdefg".indexOf("f"), which would start the search at "a"
// print(s1.IndexOf(searchString, 0));
// print(s2.IndexOf(searchString, 3));

// print("abcdefg".IndexOf("f"));

// print("leooo".LastIndexOf("o"));

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Net;

// namespace problem2
// {
//     class Program
//     {
//         static string text = "<div id="text">";
//         static string divEnd = "</div>";

//         static void Main(string[] args)
//         {
//             try
//             {
//                 WebClient wc = new WebClient();
//                 string htmlData = wc.DownloadString("http://rickleinecker.com/Rick-Leinecker-Magazine-Articles-and-Writing.html%22);

//                 int index = htmlData.IndexOf(text);

//                 while (index >= 0)
//                 {
//                     int endIndex = htmlData.IndexOf(divEnd, index);
//                     string fText = htmlData.Substring(index + text.Length, endIndex - (index + text.Length)).Trim();

//                     String[] words;

//                     words = fText.Split(' ');

//                     if (words[0][0] != 'C')
//                         break;

//                     Console.WriteLine("Magazine:" + words[0] + ", Number:" + words[1] + ", Date:" + words[2]);

//                     index = endIndex;
//                     index = htmlData.IndexOf(text, index);
//                 }
//             }
//             catch
//             {
//                 return;
//             }
//         }
//     }
// }
