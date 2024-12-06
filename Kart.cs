using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace redo_PA5
{
    public class Kart
    {
    private int kartId;
    private string kartName;
    private string size; 
    private bool isAvailable;

    public void SetKartId(int id) { this.kartId = id; }
    public int GetKartId() { return this.kartId; }

    public void SetKartName(string name) { this.kartName = name; }
    public string GetKartName() { return this.kartName; }

    public void SetSize(string size) { this.size = size; }
    public string GetSize() { return this.size; }

    public void SetIsAvailable(bool available) { this.isAvailable = available; }
    public bool GetIsAvailable() { return this.isAvailable; }

    public Kart(int id, string name, string size, bool isAvailable)
    {
        SetKartId(id);
        SetKartName(name);
        SetSize(size);
        SetIsAvailable(isAvailable);
    }

    public override string ToString()
    {
        return $"{GetKartId()}#{GetKartName()}#{GetSize()}#{GetIsAvailable()}";
    }
}
}