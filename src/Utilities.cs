class Utilities
{
	public static bool AllTrue(bool[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == false)
			{
				return false;
			}
		}

		return true;
	}
}
