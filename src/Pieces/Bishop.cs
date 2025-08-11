class Bishop(bool color, int rank, int file) : 
	Piece(
		'B',
		new int[4, 2] {
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
