using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private EnemySet enemySet;
	[SerializeField] private List<Enemy> enemyPrefabs;
	[SerializeField, Range(.5f, 1f)] private float distanceBetweenEnemiesX;
	[SerializeField, Range(.5f, 1f)] private float distanceBetweenEnemiesY;
	[SerializeField, Range(.02f, .1f)] private float appearingDelay = .05f;
    [SerializeField] private UnityEvent OnAllEnemiesDie;
	private int rowsInEnemyGrid, colsInEnemyGrid;
    private GameObject[][] enemiesGrid;
    private IEnumerator AnimatedAppearing;

	public void SpawnEnemies(Level level)
	{
		rowsInEnemyGrid = level.enemyIndex.Length;
		colsInEnemyGrid = level.enemyIndex[0].Length;
        enemySet.Clear();
        enemiesGrid = new GameObject[rowsInEnemyGrid][];

		for (int row = 0; row < rowsInEnemyGrid; row++)
		{
            enemiesGrid[row] = new GameObject[colsInEnemyGrid];

			for (int col = 0; col < level.enemyIndex[row].Length; col++)
			{
				int enemyIndex = level.enemyIndex[row][col];
				if (enemyIndex >= enemyPrefabs.Count || enemyIndex < 1)
					continue;

				Enemy enemy = SpawnEnemy(enemyIndex, row, col);
                enemy.spawner = this;
				enemy.gameObject.SetActive(false);
                enemiesGrid[row][col] = enemy.gameObject;
				SetEnemyPosition(enemy, row, col);
			}
		}

        AnimatedAppearing = AppearEnemies();
        StartCoroutine(AnimatedAppearing);
	}

	private Enemy SpawnEnemy(int index, int row, int col)
	{
		Enemy enemy = Instantiate(enemyPrefabs[index - 1]);
		enemy.transform.parent = transform;
		enemySet.AddItem(enemy);
		return enemy;
	}

	private void SetEnemyPosition(Enemy enemy, int row, int col)
	{
		float x = distanceBetweenEnemiesX * (col - colsInEnemyGrid * .5f);
		float y = distanceBetweenEnemiesY * (rowsInEnemyGrid * .5f - row);
		enemy.transform.localPosition = new Vector2(x, y);
	}

	private IEnumerator AppearEnemies()
	{
        for (int col = 0; col < colsInEnemyGrid; col++)
        {
            yield return new WaitForSeconds(appearingDelay);

            for (int row = 0; row < rowsInEnemyGrid; row++)
            {
                if (enemiesGrid[row][col] == null)
                    continue;

                enemiesGrid[row][col].SetActive(true);
            }
        }
	}

    public void OnenemyDie(Enemy enemy)
    {
        enemySet.RemoveItem(enemy);
        Destroy(enemy.gameObject);

        // ClearEnemies(); //TODO: remove this

        if (enemySet.Items.Count == 0)
            OnAllEnemiesDie?.Invoke();
    }

    private void OnDisable()
    {
        enemySet.Clear();
    }

    private void ClearEnemies()
    {
        for (int i = 0; i < enemySet.Items.Count; i++)
            Destroy(enemySet.Items[i].gameObject);
            
        enemySet.Clear();
    }
}
