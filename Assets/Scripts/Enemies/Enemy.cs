using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
	public EnemySpawner spawner { set; get; }
    public GameObject bulletPrefab;
    public UnityEvent OnStartAttack;
    public bool isAttacking = false;

    public void Die()
    {
        spawner.OnenemyDie(this);
    }

    public void Attack()
    {
        if (isAttacking)
            return;

        isAttacking = true;
        OnStartAttack?.Invoke();
    }

    public void CreateBullet()
    {
        // Actually it's better to use pool for bullets
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }


}
