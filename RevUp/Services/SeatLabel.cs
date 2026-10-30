namespace RevUp.Services;

public static class SeatLabel
{
	/// <summary>How many places are left, in German, e.g. "Noch 3 Plätze frei" or "Ausgebucht".</summary>
	public static string For(int freeSeats) => freeSeats switch
	{
		<= 0 => "Ausgebucht",
		1 => "Noch 1 Platz frei",
		_ => $"Noch {freeSeats} Plätze frei",
	};
}