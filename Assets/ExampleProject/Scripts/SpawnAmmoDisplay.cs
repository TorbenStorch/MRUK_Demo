using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class SpawnAmmoDisplay : MonoBehaviour
{
	[SerializeField] GameObject ammoDisplayPrefab;
	MRUKRoom room;
	public void SpawnDisplay()
	{
		room = MRUK.Instance.GetCurrentRoom();
		if (room == null)
		{
			Debug.LogError("Room not found!");
			return;
		}

		Vector3 spawnPosition = room.GetCeilingAnchor().GetAnchorCenter();
		Instantiate(ammoDisplayPrefab, spawnPosition, Quaternion.identity);
	}
}
