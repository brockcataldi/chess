abstract class Piece(char symbol, bool color)
{
    public char Symbol { get; init; } = symbol; // Assign the constructor parameter to the Symbol property
    public bool Color { get; init; } = color;   // Assign the constructor parameter to the Color property
}