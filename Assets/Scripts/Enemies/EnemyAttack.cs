using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyAttackState
{
	Idle,
	Dive
}

public class EnemyAttack : MonoBehaviour
{
	private EnemyAttackState state = EnemyAttackState.Idle;
	[Range(1f, 2f)] public float sinMultiplier = 3f;
	[Range(0.01f, 0.1f)] public float speed = 0.02f;
	[SerializeField, Range(1f, 5f)] float rechargeTime = 3f;
    [SerializeField, Range(.5f, 1f)] float attackRadius = 1f;
    [SerializeField] private FloatValue playerXPosition;
    public UnityEvent OnShootTimer;
	private float rechargeTimer = 0;
	private Vector2 nextPosition = Vector2.zero;
	private Camera camera;

	public void DoAttack()
	{
		state = EnemyAttackState.Dive;
		camera = Camera.main;
	}

	public void OnUpdate()
	{
		if (state == EnemyAttackState.Idle)
			return;

		CheckIfOutOfScreen();
		UpdateDive();
        UpdateShoot();
	}

	private void CheckIfOutOfScreen()
	{
		Vector2 screenPosition = camera.WorldToScreenPoint(transform.position);

		nextPosition = screenPosition;

		if (screenPosition.x < 0)
			nextPosition.x = Screen.width - 1;
		else if (screenPosition.x > Screen.width)
			nextPosition.x = 1;

		if (screenPosition.y < 0)
			nextPosition.y = Screen.height + 1;

		transform.position = camera.ScreenToWorldPoint(nextPosition);
	}

	private void UpdateDive()
	{
		nextPosition.y = transform.position.y - speed;
		nextPosition.x = Mathf.Sin(nextPosition.y) * sinMultiplier;
		transform.position = nextPosition;
	}

	private void UpdateShoot()
	{
		rechargeTimer += Time.deltaTime;
		if (rechargeTimer < rechargeTime)
			return;

        if (Mathf.Abs(transform.position.x - playerXPosition.value)<attackRadius)
        {
            rechargeTimer = 0;  
            OnShootTimer?.Invoke();          
        }

	}
}
