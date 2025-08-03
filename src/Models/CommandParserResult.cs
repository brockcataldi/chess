
abstract record CommandParserResult;
record CommandParserResultMove(Position Start, Position End) : CommandParserResult;
record CommandParserResultError(string Message) : CommandParserResult;