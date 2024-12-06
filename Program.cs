using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using redo_PA5;
public class Program
{
    public static void Main(string[] args)
    {
        var inventory = new KartInventory();
        var resultsManager = new RaceResultsManager();

        inventory.LoadInventory();
        resultsManager.LoadResults();

        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Admin Menu");
            Console.WriteLine("2. Player Menu");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AdminMenu(inventory, resultsManager);
                    break;
                case "2":
                    PlayerMenu(inventory, resultsManager);
                    break;
                case "3":
                    return;

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private static void AdminMenu(KartInventory inventory, RaceResultsManager resultsManager)
    {
        while (true)
        {
            Console.WriteLine("\nAdmin Menu:");
            Console.WriteLine("1. Add a new kart");
            Console.WriteLine("2. Remove a kart");
            Console.WriteLine("3. Edit kart information");
            Console.WriteLine("4. Access the report menu");
            Console.WriteLine("5. Back to main menu");
            Console.WriteLine("6. Search karts");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Kart ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Kart Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Size (small/medium/large): ");
                    string size = Console.ReadLine();
                    Console.Write("Is the kart available? (true/false): ");
                    bool isAvailable = bool.Parse(Console.ReadLine());

                    inventory.AddKart(new Kart(id, name, size, isAvailable));
                    Console.WriteLine("Kart added successfully!");
                    break;

                case "2":
                    Console.Write("Enter Kart ID to remove: ");
                    int removeId = int.Parse(Console.ReadLine());
                    inventory.RemoveKart(removeId);
                    Console.WriteLine("Kart removed successfully!");
                    break;

                case "3":
                    Console.Write("Enter Kart ID to edit: ");
                    int editId = int.Parse(Console.ReadLine());
                    Console.Write("Enter new Kart Name: ");
                    string newName = Console.ReadLine();
                    Console.Write("Enter new Size (small/medium/large): ");
                    string newSize = Console.ReadLine();
                    Console.Write("Is the kart available? (true/false): ");
                    bool newAvailability = bool.Parse(Console.ReadLine());

                    inventory.EditKart(editId, newName, newSize, newAvailability);
                    Console.WriteLine("Kart updated successfully!");
                    break;

                case "4":
                    ReportsMenu(inventory, resultsManager);
                    break;

                case "5":
                    return;

                case "6":
                    Console.Write("Enter keyword to search for karts: ");
                    string keyword = Console.ReadLine();
                    var searchResults = inventory.SearchKarts(keyword);
                    if (searchResults.Length > 0)
                    {
                        Console.WriteLine("Search Results:");
                        foreach (var kart in searchResults)
                        {
                            Console.WriteLine($"{kart.GetKartId()}: {kart.GetKartName()} ({kart.GetSize()}) - Available: {kart.GetIsAvailable()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No karts found matching your search.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private static void PlayerMenu(KartInventory inventory, RaceResultsManager resultsManager)
    {
        while (true)
        {
            Console.WriteLine("\nPlayer Menu:");
            Console.WriteLine("1. View available karts");
            Console.WriteLine("2. Race a kart");
            Console.WriteLine("3. View list of karts you have raced");
            Console.WriteLine("4. Return a kart");
            Console.WriteLine("5. Back to main menu");
            Console.WriteLine("6. Search karts");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Available Karts:");
                    var availableKarts = inventory.GetAvailableKarts();
                    foreach (var kart in availableKarts)
                    {
                        Console.WriteLine($"{kart.GetKartId()}: {kart.GetKartName()} ({kart.GetSize()})");
                    }
                    break;

                case "2":
                    Console.Write("Enter your email: ");
                    string email = Console.ReadLine();
                    Console.WriteLine("Available Karts:");
                    availableKarts = inventory.GetAvailableKarts();
                    foreach (var kart in availableKarts)
                    {
                        Console.WriteLine($"{kart.GetKartId()}: {kart.GetKartName()} ({kart.GetSize()})");
                    }
                    Console.Write("Enter Kart ID to race: ");
                    int kartId = int.Parse(Console.ReadLine());
                    Console.Write("Enter the track name: ");
                    string track = Console.ReadLine();
                    Console.Write("Enter time elapsed (in seconds): ");
                    int timeElapsed = int.Parse(Console.ReadLine());

                    resultsManager.AddResult(new RaceResult(
                        resultsManager.GetResults().Length + 1,
                        email,
                        kartId,
                        DateTime.Now,
                        timeElapsed,
                        track,
                        false
                    ));
                    inventory.EditKart(kartId, null, null, false);
                    Console.WriteLine("Race recorded successfully!");
                    break;

                case "3":
                    Console.Write("Enter your email: ");
                    email = Console.ReadLine();
                    var racedKarts = resultsManager.GetResultsByEmail(email);
                    Console.WriteLine("Your Raced Karts:");
                    foreach (var result in racedKarts)
                    {
                        Console.WriteLine($"Kart ID: {result.GetKartId()}, Track: {result.GetTrack()}, Date: {result.GetRaceDate()}");
                    }
                    break;

                case "4":
                    Console.Write("Enter Kart ID to return: ");
                    int returnKartId = int.Parse(Console.ReadLine());
                    var resultToReturn = Array.Find(resultsManager.GetResults(), r => r != null && r.GetKartId() == returnKartId && !r.GetKartReturned());
                    if (resultToReturn != null)
                    {
                        resultToReturn.SetKartReturned(true);
                        inventory.EditKart(returnKartId, null, null, true);
                        resultsManager.SaveResults();
                        Console.WriteLine("Kart returned successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Kart not found or already returned.");
                    }
                    break;

                case "5":
                    return;

                case "6":
                    Console.Write("Enter keyword to search for karts: ");
                    string keyword = Console.ReadLine();
                    var searchResults = inventory.SearchKarts(keyword);
                    if (searchResults.Length > 0)
                    {
                        Console.WriteLine("Search Results:");
                        foreach (var kart in searchResults)
                        {
                            Console.WriteLine($"{kart.GetKartId()}: {kart.GetKartName()} ({kart.GetSize()}) - Available: {kart.GetIsAvailable()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No karts found matching your search.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private static void ReportsMenu(KartInventory inventory, RaceResultsManager resultsManager)
    {
        while (true)
        {
            Console.WriteLine("\nReports Menu:");
            Console.WriteLine("1. Daily Kart Race Report");
            Console.WriteLine("2. Karts Currently in Use");
            Console.WriteLine("3. Average Race Results by Kart Size");
            Console.WriteLine("4. Top 5 Karts Used in Tournament");
            Console.WriteLine("5. Track Leaderboard");
            Console.WriteLine("6. Back to Admin Menu");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Reports.DailyKartRaceReport(resultsManager.GetResults());
                    break;

                case "2":
                    Reports.KartsCurrentlyInUse(resultsManager.GetResults());
                    break;

                case "3":
                    Reports.AverageRaceResultsByKartSize(resultsManager.GetResults(), inventory.GetAllKarts());
                    break;

                case "4":
                    Reports.Top5Karts(resultsManager.GetResults(), inventory.GetAllKarts());
                    break;

                case "5":
                    Reports.TrackLeaderboard(resultsManager.GetResults());
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }
}