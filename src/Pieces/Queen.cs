class Queen(bool color, int rank, int file) : 
	Piece(
		'Q',
		new int[8, 2] {
			{1, 0},
			{0, 1},
			{-1, 0},
			{0, -1},
			{1, 1},
			{1, -1},
			{-1, 1},
			{-1, -1}
		},
		8,
		color,
		rank,
		file
	)
{ }
