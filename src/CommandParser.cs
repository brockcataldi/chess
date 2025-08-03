using System.Text.RegularExpressions;

partial class CommandParser
{
    public static char[] files = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];

    public static char[] ranks = ['1', '2', '3', '4', '5', '6', '7', '8'];

    [GeneratedRegex(@"^([a-h][1-8])(?:\s|-)?([a-h][1-8])$")]
    public static partial Regex LongNotation();

    [GeneratedRegex(@"^([a-h][1-8])(?:\s|-)?([a-h][1-8])(?:\s|-)?([bnqr])$")]
    public static partial Regex LongNotationPromotion();

    public static Position ParsePosition(string position)
    {
        return new Position(
            Array.IndexOf(ranks, position[1]),
            Array.IndexOf(files, position[0])
        );
    }

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