using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
	[SerializeField] private EnemySet enemies;
	private float randomNextAttackTime = -1f;
	private float attackTimer = 0;

	public void OnUpdate()
	{
		// if (Input.GetKeyDown(KeyCode.Space))
		// 	RandomEnemyAttack();

		if (randomNextAttackTime < 0)
		{
			randomNextAttackTime = Random.Range(1f, 5f);
			return;
		}

		attackTimer += Time.deltaTime;

        if (attackTimer < randomNextAttackTime)
            return;

        attackTimer = 0;
        RandomEnemyAttack();
	}

	private void RandomEnemyAttack()
	{
		Enemy enemy = enemies.Items[Random.Range(0, enemies.Items.Count)];
		enemy.Attack();
	}
}
