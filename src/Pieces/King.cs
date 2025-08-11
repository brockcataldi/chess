class King(bool color, int rank, int file) : 
	Piece(
		'K',
		new int[8, 2] {
			{ 1, 0 },
			{ 1, 1 },
			{ 1, -1 },
			{ 0, 1 },
			{ 0, -1 },
			{ -1, 1 },
			{ -1, 0 },
			{ -1, -1 }
		},
		1,
		color,
		rank,
		file
	)
{ }
