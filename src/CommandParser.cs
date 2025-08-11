using System.Text.RegularExpressions;

/// <summary>
/// Simple Command Parser Class.
/// </summary>
partial class CommandParser
{

	/// <summary>
	/// Regex to test basic long form notation, ie: e2e4, e2-e4, e2 e4
	/// </summary>
	/// <returns></returns>
	[GeneratedRegex(@"^([a-h][1-8])(?:\s|-)?([a-h][1-8])$")]
	public static partial Regex LongNotation();

	/// <summary>
	/// Regex to test basic long form notation ending in promotion, ie: e7e8q, e7-e8-q, e7 e8 q
	/// </summary>
	/// <returns></returns>
	[GeneratedRegex(@"^([a-h][1-8])(?:\s|-)?([a-h][1-8])(?:\s|-)?([bnqr])$")]
	public static partial Regex LongNotationPromotion();

	/// <summary>
	/// Regex for "show <letter><number>"
	/// </summary>
	/// <returns></returns>
	[GeneratedRegex(@"^show ([a-h][1-8])$")]
	public static partial Regex Show();

	/// <summary>
	/// Parse the standard notation of letter and number to array indexes.
	/// </summary>
	/// <param name="position">The string</param>
	/// <returns>The position</returns>
	public static Position ParsePosition(string position)
	{
		return new Position(
			Array.IndexOf(Constants.RANKS, position[1]),
			Array.IndexOf(Constants.FILES, position[0])
		);
	}

	/// <summary>
	/// Parse the actual commands
	/// </summary>
	/// <param name="text">Raw Commands</param>
	/// <returns>The result of the command</returns>
	public static CommandParserResult Parse(string text)
	{
		string command = text.ToLower().Trim();

		if (LongNotation().IsMatch(command))
		{
			Match match = LongNotation().Match(command);

			return new CommandParserResultMove(
				ParsePosition(match.Groups[1].Value),
				ParsePosition(match.Groups[2].Value)
			);
		}

		if (LongNotationPromotion().IsMatch(command))
		{
			Match match = LongNotationPromotion().Match(command);
			return new CommandParserResultPromotion(
				ParsePosition(match.Groups[1].Value),
				ParsePosition(match.Groups[2].Value),
				match.Groups[3].Value[0] // quick cast
			);
		}

		if (Show().IsMatch(command))
		{
			Match match = Show().Match(command);
			return new CommandParserResultShow(
				ParsePosition(match.Groups[1].Value)
			);
		}

		return new CommandParserResultError("Invalid Command");
	}
}
