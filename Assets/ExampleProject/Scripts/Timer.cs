using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI textMeshProUGUI;
	[SerializeField] float totalCountdownTimeInSec = 120f;
	float countdownTime;
	bool isCounting = false;
	bool canStartCountDown = false;

	void Awake() => GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
	void OnDestroy() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
	void GameManager_OnGameStateChanged(GameState state)
	{
		if (state == GameState.InitialStart)
		{
			textMeshProUGUI.text = "Press [B] to Start";
			countdownTime = totalCountdownTimeInSec;
			canStartCountDown = true;
		}
		else if (state == GameState.EndGame)
		{
			CountdownStop();
		}
	}
	void Update()
	{
		if (Input.GetButtonDown("Fire2") || OVRInput.GetDown(OVRInput.RawButton.B))
		{
			if (canStartCountDown)
			{
				countdownTime = totalCountdownTimeInSec;
				if (GameManager.Instance.State == GameState.EndGame)
				{
					GameManager.Instance.UpdateGameState(GameState.RestartGame);
				}
				canStartCountDown = false;
				CountdownStart();
				AudioManager.Instance.PlaySFX("collect");
			}
		}
	}
	void CountdownStart()
	{
		GameManager.Instance.UpdateGameState(GameState.TimerCountdown);
		isCounting = true;
		StartCoroutine(Countdown());
	}
	void CountdownStop()
	{
		isCounting = false;
		textMeshProUGUI.text = "Score: " + ScoreManager.Instance.score
			+ "\n" + "Time left: " + SecodsToMinSec(countdownTime)
			+ "\n" + "Press [B] to Restart";
		canStartCountDown = true;
	}
	IEnumerator Countdown()
	{
		while (isCounting && countdownTime > 0)
		{
			textMeshProUGUI.text = SecodsToMinSec(countdownTime);

			yield return new WaitForSeconds(1f);

			countdownTime -= 1f;
		}

		if (isCounting)
		{
			GameManager.Instance.UpdateGameState(GameState.EndGame);
		}
	}
	string SecodsToMinSec(float timeInSec)
	{
		int minutes = Mathf.FloorToInt(timeInSec / 60);
		int seconds = Mathf.FloorToInt(timeInSec % 60);
		return string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
