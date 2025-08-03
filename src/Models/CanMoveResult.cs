abstract record CanMoveResult;

record CanMoveResultValid : CanMoveResult;

record CanMoveResultError(string Message) : CanMoveResult;

record CanMoveResultEnPassant(Position Position) : CanMoveResult;