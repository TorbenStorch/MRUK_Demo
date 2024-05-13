using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class SpawnAmmoPack : MonoBehaviour
{
	[SerializeField] GameObject ammoPrefab;
	GameObject ammoObj;
	MRUKRoom room;
	bool spawnAmmoPack = true;

	void Awake() => GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
	void OnDestroy() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
	private void GameManager_OnGameStateChanged(GameState state)
	{
		if (state == GameState.EndGame)
		{
			spawnAmmoPack = false;
		}

		if (state == GameState.RestartGame)
		{
			spawnAmmoPack = true;
			StartCoroutine(AmmoSpawning());
		}
	}

	public void StartSpawning()
	{
		spawnAmmoPack = true;
		if (room == null) room = MRUK.Instance.GetCurrentRoom();
		if (room == null)
		{
			Debug.LogError("Room not found!");
			return;
		}

		if (ammoObj == null)
		{
			ammoObj = Instantiate(ammoPrefab, Vector3.zero, Quaternion.identity);
			ammoObj.SetActive(false);
		}

		StartCoroutine(AmmoSpawning());
	}

	IEnumerator AmmoSpawning()
	{
		while (spawnAmmoPack)
		{
			if (!ammoObj.activeSelf)
			{
				yield return new WaitForSeconds(3f);
				SpawnAmmoAtRandomPosition();
			}
			yield return null;
		}
	}

	void SpawnAmmoAtRandomPosition()
	{
		var randomPos = room.GenerateRandomPositionInRoom(ammoPrefab.transform.localScale.x, true);
		Vector3 spawnPosition = randomPos.Value;

		ammoObj.SetActive(true);
		ammoObj.transform.position = spawnPosition;
	}
}
