using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
	[SerializeField] private LevelSet levelSet;
	[SerializeField, Range(1f, 3f)] private float delayBeforeLevelStartInSeconds = 1f;
	[SerializeField] private SpawnEvent OnSpawnTimer;
	[SerializeField] private GameEvent OnLevelsDone;
	[SerializeField] private BoolValue isGamePlayStarted;
	private float delayTimer;
	private int currentLevel = 0;
	private bool isCountingDelay = true;


	private void OnEnable()
	{
		isGamePlayStarted.Value = false;

		ResetDelayTimer();
	}

	private void ResetDelayTimer()
	{
		delayTimer = 0;
		isCountingDelay = true;
	}

	public void OnUpdate()
	{
		if (!isCountingDelay)
			return;

		CountDelay();
	}

	private void CountDelay()
	{
		delayTimer += Time.deltaTime;
		if (delayTimer < delayBeforeLevelStartInSeconds)
			return;

		isCountingDelay = false;
		SpawnEnemies();
	}

	public void SpawnEnemies()
	{
		OnSpawnTimer?.Invoke(levelSet.Items[currentLevel]);
		isGamePlayStarted.Value = true;
	}

	public void NextLevel()
	{
		isGamePlayStarted.Value = false;

		if (currentLevel == levelSet.Items.Count - 1)
		{
			LevelsDone();
			return;
		}

		currentLevel++;
		ResetDelayTimer();
	}

	private void LevelsDone()
	{
		OnLevelsDone?.InvokeEvent();
	}
}

[System.Serializable]
public class SpawnEvent : UnityEvent<Level> { }
