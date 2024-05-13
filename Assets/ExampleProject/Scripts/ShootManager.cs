using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
	public static ShootManager Instance;

	[SerializeField] GameObject projectilePrefab;
	[SerializeField] Transform projectileSpawnPoint;
	[SerializeField] float shootForce = 10f;
	const int maxAmmoCount = 12;
	public int ammoCount { get; private set; }


	void Awake()
	{
		Instance = this;
		GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
	}
	void OnDestroy() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

	void GameManager_OnGameStateChanged(GameState state)
	{
		if (state == GameState.EndGame)
		{
			ammoCount = 0;
		}

		if (state == GameState.RestartGame)
		{
			gameObject.SetActive(true);
			ammoCount = maxAmmoCount;
		}
	}


	private void Start() => ammoCount = maxAmmoCount;

	void Update()
	{
		if (ammoCount == 0)
		{
			GameManager.Instance.UpdateGameState(GameState.EndGame);
			return;
		}
		if (Input.GetButtonDown("Fire1") || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
		{
			AudioManager.Instance.PlaySFX("shoot");
			ShootProjectile();
		}
	}
	public void IncreaseAmmo(int increaseAmount)
	{
		if (ammoCount + increaseAmount > maxAmmoCount) ammoCount = maxAmmoCount;
		else ammoCount += increaseAmount;
	}
	void ShootProjectile()
	{
		ammoCount -= 1;
		GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
		Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();

		if (rb == null)
		{
			Debug.LogError("Rigidbody component not found on the ball prefab.");
			Destroy(projectileInstance);
			return;
		}

		Vector3 shootDirection = projectileSpawnPoint.forward;
		rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);

		Destroy(projectileInstance, 5f);
	}


}