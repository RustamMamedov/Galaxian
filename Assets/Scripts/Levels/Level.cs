public class Level
{
	public int[][] enemyIndex;

	public Level(StoredLevelRow[] level)
	{
		int columnCount = level[0].row.Length;
		int rowCount = level.Length;

		enemyIndex = new int[rowCount][];

		for (int row = 0; row < rowCount; row++)
		{
			enemyIndex[row] = new int[columnCount];
			for (int col = 0; col < columnCount; col++)
			{
				enemyIndex[row][col] = level[row].row[col];
			}
		}
	}
}


[System.Serializable]
public class StoredLevels
{
	public StoredLevel[] levels;
}

[System.Serializable]
public class StoredLevel
{
	public StoredLevelRow[] level;
}

[System.Serializable]
public class StoredLevelRow
{
	public int[] row;
}


