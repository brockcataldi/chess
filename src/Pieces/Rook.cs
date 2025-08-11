class Rook(bool color, int rank, int file) : 
	Piece(
		'R',
		new int[4, 2] {
			{1, 0},
			{0, 1},
			{-1, 0},
			{0, -1}
		},
		8,
		color,
		rank,
		file
	)
{ }
