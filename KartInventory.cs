using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace redo_PA5
{
    public class KartInventory
    {
    private const string FileName = "kart-inventory.txt";
    private Kart[] karts = new Kart[50];
    private int count = 0;

    public void LoadInventory()
    {
        if (File.Exists(FileName))
        {
            var lines = File.ReadAllLines(FileName);
            foreach (var line in lines)
            {
                var fields = line.Split('#');
                var kart = new Kart(int.Parse(fields[0]), fields[1], fields[2], bool.Parse(fields[3]));
                karts[count++] = kart;
            }
        }
    }

    public void SaveInventory()
    {
        using (var writer = new StreamWriter(FileName))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(karts[i].ToString());
            }
        }
    }

    public void AddKart(Kart kart)
    {
        if (count >= karts.Length)
        {
            Console.WriteLine("Inventory is full. Cannot add more karts.");
            return;
        }
        karts[count++] = kart;
        SaveInventory();
    }

    public void RemoveKart(int kartId)
    {
        int index = Array.FindIndex(karts, 0, count, k => k != null && k.GetKartId() == kartId);
        if (index != -1)
        {
            for (int i = index; i < count - 1; i++)
            {
                karts[i] = karts[i + 1];
            }
            karts[--count] = null;
            SaveInventory();
        }
    }

    public void EditKart(int kartId, string newName, string newSize, bool newAvailability)
    {
        int index = Array.FindIndex(karts, 0, count, k => k != null && k.GetKartId() == kartId);
        if (index != -1)
        {
            if (!string.IsNullOrEmpty(newName)) karts[index].SetKartName(newName);
            if (!string.IsNullOrEmpty(newSize)) karts[index].SetSize(newSize);
            karts[index].SetIsAvailable(newAvailability);
            SaveInventory();
        }
    }

    public Kart[] GetAvailableKarts()
    {
        return Array.FindAll(karts, k => k != null && k.GetIsAvailable());
    }

    public Kart[] GetAllKarts()
    {
        var result = new Kart[count];
        Array.Copy(karts, result, count);
        return result;
    }

    public Kart[] SearchKarts(string keyword)
    {
        return Array.FindAll(karts, k => k != null &&
            (k.GetKartName().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
             k.GetSize().Equals(keyword, StringComparison.OrdinalIgnoreCase) ||
             k.GetKartId().ToString() == keyword));
    }
}
}