using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
	[SerializeField] private bool isImmortal = false;
	[SerializeField] private int startHealth;
	public int health;
	[SerializeField] UnityEvent OnTakeDamage;
	[SerializeField] UnityEvent OnDie;
	[SerializeField] private IntegerValue healthValue;

	private void Awake()
	{
		ResetHealth();
	}

	public void ResetHealth()
	{
		SetHealth(startHealth);
	}

	public void TakeDamage(Damager damager)
	{
		if (isImmortal)
			return;

		SetHealth(Mathf.Max(0, health - damager.damage));

		OnTakeDamage?.Invoke();

		if (health == 0)
			Die();
	}

	public void SetHealth(int value)
	{
		health = value;
        
		if (healthValue == null)
			return;

		healthValue.Value = health;
	}

	public void Die()
	{
		OnDie?.Invoke();
	}

}
