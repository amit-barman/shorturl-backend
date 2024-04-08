namespace shorturl.Utils;

public class RandomUrl {
	private static readonly string _letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

	public static string? getRandomURL( int range )
	{
		string result = "";
		for(int i = 0; i < range; i++)
		{
			result += _letters[new Random().Next(_letters.Length)];
		}
		return result;
	}
}