using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
	[SerializeField] List<GameObject> bullets;

	void Update()
	{
		if (ShootManager.Instance.ammoCount > bullets.Count)
		{
			Debug.LogError("AmmoPack Count mismatch!");
			return;
		}
		foreach (var item in bullets)
		{
			item.SetActive(false);
		}
		for (int i = 0; i < ShootManager.Instance.ammoCount; i++)
		{
			bullets[i].SetActive(true);
		}
	}
}


