using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitRewardCounter : MonoBehaviour
{
    [SerializeField] private RewardCounter rewarCounter;

    private void Awake()
    {
        rewarCounter.InitScore();    
    }

}
