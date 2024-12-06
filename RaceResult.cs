using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace redo_PA5
{
    public class RaceResult
    {
    private int interactionId;
    private string playerEmail;
    private int kartId;
    private DateTime raceDate;
    private int timeElapsed; 
    private string track;
    private bool kartReturned;

    public void SetInteractionId(int id) { this.interactionId = id; }
    public int GetInteractionId() { return this.interactionId; }

    public void SetPlayerEmail(string email) { this.playerEmail = email; }
    public string GetPlayerEmail() { return this.playerEmail; }

    public void SetKartId(int id) { this.kartId = id; }
    public int GetKartId() { return this.kartId; }

    public void SetRaceDate(DateTime date) { this.raceDate = date; }
    public DateTime GetRaceDate() { return this.raceDate; }

    public void SetTimeElapsed(int time) { this.timeElapsed = time; }
    public int GetTimeElapsed() { return this.timeElapsed; }

    public void SetTrack(string track) { this.track = track; }
    public string GetTrack() { return this.track; }

    public void SetKartReturned(bool returned) { this.kartReturned = returned; }
    public bool GetKartReturned() { return this.kartReturned; }

    public RaceResult(int id, string email, int kartId, DateTime date, int time, string track, bool returned)
    {
        SetInteractionId(id);
        SetPlayerEmail(email);
        SetKartId(kartId);
        SetRaceDate(date);
        SetTimeElapsed(time);
        SetTrack(track);
        SetKartReturned(returned);
    }

    public override string ToString()
    {
        return $"{GetInteractionId()}#{GetPlayerEmail()}#{GetKartId()}#{GetRaceDate():MM/dd/yyyy}#{GetTimeElapsed()}#{GetTrack()}#{GetKartReturned()}";
    }
}
}