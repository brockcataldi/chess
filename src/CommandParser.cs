using System.Text.RegularExpressions;

/// <summary>
/// 
/// </summary>
partial class CommandParser
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^([a-h][1-8])(?:\s|-)?([a-h][1-8])$")]
    public static partial Regex LongNotation();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^([a-h][1-8])(?:\s|-)?([a-h][1-8])(?:\s|-)?([bnqr])$")]
    public static partial Regex LongNotationPromotion();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Position ParsePosition(string position)
    {
        return new Position(
            Array.IndexOf(Constants.RANKS, position[1]),
            Array.IndexOf(Constants.FILES, position[0])
        );
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
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

        return new CommandParserResultError("Invalid Command");
    }
}