using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
	public int damage;
	[SerializeField] private Vector2 size = new Vector2(1f, 1f);
	public LayerMask hittableLayers;
	[SerializeField] private UnityEvent OnDamageableHit;
	private bool canDamage = true;
	private Collider[] hitColliders;
	private ContactFilter2D attackContactFilter;
	private Collider2D[] overlapResults = new Collider2D[1];

	private void Awake()
	{
		attackContactFilter.layerMask = hittableLayers;
		attackContactFilter.useLayerMask = true;
		attackContactFilter.useTriggers = true;
	}

	private void OnEnable()
	{
		canDamage = true;
	}

	public void OnUpdate()
	{
		UpdateDamager();
	}

	public void EnableDamage()
	{
		canDamage = true;
	}

	public void DisableDamage()
	{
		canDamage = false;
	}

	public void UpdateDamager()
	{
		if (!canDamage)
			return;

		Vector2 pointA = (Vector2)transform.position - size * 0.5f;
		Vector2 pointB = pointA + size;

		int hitCount = Physics2D.OverlapArea(pointA, pointB, attackContactFilter, overlapResults);

		for (int i = 0; i < hitCount; i++)
		{
			if (!canDamage)
				continue;

			Damageable damageable = overlapResults[i].GetComponent<Damageable>();

			if (damageable == null)
				continue;

	        OnDamageableHit?.Invoke();
	        DisableDamage();
			damageable.TakeDamage(this);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position , size);
	}

}
