abstract record EntryResult;
record EntryResultValid: EntryResult;
record EntryResultError(string Message): EntryResult;
