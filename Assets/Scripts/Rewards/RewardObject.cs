using UnityEngine;

public class RewardObject : MonoBehaviour
{
	[SerializeField] private RewardCounter rewardCounter;
	[Range(10, 500)] public int reward = 10;

	public void AddReward()
	{
		rewardCounter.AddReward(reward);
	}
}
