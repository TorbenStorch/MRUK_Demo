using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject enemyPefab;
	GameObject enemyObj;
	[SerializeField] float visibleTime = 2f;
	bool showEnemies = true;

	void Awake() => GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
	void OnDestroy() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
	void GameManager_OnGameStateChanged(GameState state)
	{
		if (state == GameState.EndGame)
		{
			showEnemies = false;
			enemyObj.SetActive(false);
		}

		if (state == GameState.RestartGame)
		{
			showEnemies = true;
			enemyObj.SetActive(false);
			StartCoroutine(EnemyMovement());
		}
	}

	void Start()
	{
		enemyObj = Instantiate(enemyPefab, transform.position, transform.rotation, transform);
		enemyObj.SetActive(false);

		showEnemies = true;
		StartCoroutine(EnemyMovement());
	}

	IEnumerator EnemyMovement()
	{
		float currentRotation;
		while (showEnemies)
		{
			yield return new WaitForSeconds(Random.Range(1f, 10f));

			currentRotation = Random.Range(0f, 7f) * 45f;
			enemyObj.transform.localRotation = Quaternion.Euler(0f, currentRotation, 0f);

			enemyObj.SetActive(true);

			yield return new WaitForSeconds(visibleTime);
			enemyObj.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (enemyObj == null) return;
		if (enemyObj.activeInHierarchy && other.CompareTag("Bullet"))
		{
			enemyObj.SetActive(false);

			ShootManager.Instance.IncreaseAmmo(1);
			ScoreManager.Instance.IncreaseScore(1);
			AudioManager.Instance.PlaySFX("hit");
		}
	}
}
