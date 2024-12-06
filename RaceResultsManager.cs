using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace redo_PA5
{
    public class RaceResultsManager
    {
    private const string ResultsFileName = "results.txt";
    private RaceResult[] results = new RaceResult[100];
    private int count = 0;

    public void LoadResults()
    {
        if (File.Exists(ResultsFileName))
        {
            var lines = File.ReadAllLines(ResultsFileName);
            foreach (var line in lines)
            {
                var fields = line.Split('#');
                var result = new RaceResult(
                    int.Parse(fields[0]),
                    fields[1],
                    int.Parse(fields[2]),
                    DateTime.Parse(fields[3]),
                    int.Parse(fields[4]),
                    fields[5],
                    bool.Parse(fields[6])
                );
                results[count++] = result;
            }
        }
    }

    public void SaveResults()
    {
        using (var writer = new StreamWriter(ResultsFileName))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(results[i].ToString());
            }
        }
    }

    public void AddResult(RaceResult result)
    {
        if (count >= results.Length)
        {
            Console.WriteLine("Results storage is full. Cannot add more results.");
            return;
        }
        results[count++] = result;
        SaveResults();
    }

    public RaceResult[] GetResultsByEmail(string email)
    {
        return Array.FindAll(results, r => r != null && r.GetPlayerEmail() == email);
    }

    public RaceResult[] GetResults()
    {
        var resultArray = new RaceResult[count];
        Array.Copy(results, resultArray, count);
        return resultArray;
    }
}
}