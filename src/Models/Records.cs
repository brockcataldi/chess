record Position(int Rank, int File);

abstract record EntryResult;
record EntryResultValid: EntryResult;
record EntryResultError(string Message): EntryResult;

abstract record CommandParserResult;
record CommandParserResultMove(Position Start, Position End) : CommandParserResult;
record CommandParserResultPromotion(Position Start, Position End, char Promotion) : CommandParserResult;
record CommandParserResultError(string Message) : CommandParserResult;

abstract record CanMoveResult;
record CanMoveResultValid : CanMoveResult;
record CanMoveResultPromote: CanMoveResult;
record CanMoveResultError(string Message) : CanMoveResult;
record CanMoveResultEnPassant(Position Position) : CanMoveResult;


