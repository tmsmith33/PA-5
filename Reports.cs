using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace redo_PA5
{
    public class Reports
    {
        public static void DailyKartRaceReport(RaceResult[] results)
    {
        var today = DateTime.Today;
        var dailyResults = results.Where(r => r != null && r.GetRaceDate().Date == today).ToArray();
        foreach (var result in dailyResults)
        {
            Console.WriteLine(result);
        }
    }

    public static void KartsCurrentlyInUse(RaceResult[] results)
    {
        var inUse = results.Where(r => r != null && !r.GetKartReturned()).ToArray();
        foreach (var result in inUse)
        {
            Console.WriteLine(result);
        }
    }

    public static void AverageRaceResultsByKartSize(RaceResult[] results, Kart[] karts)
    {
        var groupedBySize = results
            .Where(r => r != null)
            .GroupBy(r => karts.FirstOrDefault(k => k.GetKartId() == r.GetKartId())?.GetSize())
            .Where(g => g.Key != null)
            .Select(g => new { Size = g.Key, AverageTime = g.Average(r => r.GetTimeElapsed()) });

        foreach (var group in groupedBySize)
        {
            Console.WriteLine($"Size: {group.Size}, Average Time: {group.AverageTime} seconds");
        }
    }

    public static void Top5Karts(RaceResult[] results, Kart[] karts)
    {
        var topKarts = results
            .Where(r => r != null)
            .GroupBy(r => r.GetKartId())
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new
            {
                Kart = karts.FirstOrDefault(k => k.GetKartId() == g.Key),
                UsageCount = g.Count()
            });

        Console.WriteLine("\nTop 5 Karts Used in the Tournament:");
        foreach (var entry in topKarts)
        {
            Console.WriteLine($"Kart: {entry.Kart?.GetKartName() ?? "Unknown"}, Used: {entry.UsageCount} times");
        }
    }

    public static void TrackLeaderboard(RaceResult[] results)
    {
        var leaderboard = results
            .Where(r => r != null)
            .GroupBy(r => r.GetTrack())
            .Select(g => new
            {
                Track = g.Key,
                BestTime = g.Min(r => r.GetTimeElapsed()),
                BestPlayer = g.First(r => r.GetTimeElapsed() == g.Min(r2 => r2.GetTimeElapsed())).GetPlayerEmail()
            });

        Console.WriteLine("\nTrack Leaderboard:");
        foreach (var entry in leaderboard)
        {
            Console.WriteLine($"Track: {entry.Track}, Best Time: {entry.BestTime}s, Player: {entry.BestPlayer}");
        }
    }
}
}