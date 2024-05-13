using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
	void Awake() => GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
	void OnDestroy() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
	private void GameManager_OnGameStateChanged(GameState state)
	{
		if (state == GameState.EndGame)
		{
			gameObject.SetActive(false);
		}
		if (state == GameState.RestartGame)
		{
			gameObject.SetActive(true);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			ShootManager.Instance.IncreaseAmmo(4);
			AudioManager.Instance.PlaySFX("collect");

			gameObject.SetActive(false);
		}
	}
}
