using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'computePrices' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY s
     *  2. INTEGER_ARRAY p
     *  3. INTEGER_ARRAY q
     */

    public static List<int> computePrices(List<int> s, List<int> p, List<int> q)
    {
        List<int> prices = new List<int>();
        foreach(var quantity in q) {
            int selectedShare = 0;
            int index = 0;
            foreach(var share in s) {
                if (quantity >= share && share >= selectedShare) {
                    selectedShare = share;
                    index = s.IndexOf(selectedShare);
                }
            }
            prices.Add(p.ElementAt(index));
        }
        return prices;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> s = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(sTemp => Convert.ToInt32(sTemp)).ToList();

        List<int> p = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(pTemp => Convert.ToInt32(pTemp)).ToList();

        int k = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> q = new List<int>();

        for (int i = 0; i < k; i++)
        {
            int qItem = Convert.ToInt32(Console.ReadLine().Trim());
            q.Add(qItem);
        }

        List<int> res = Result.computePrices(s, p, q);

        textWriter.WriteLine(String.Join("\n", res));

        textWriter.Flush();
        textWriter.Close();
    }
}
