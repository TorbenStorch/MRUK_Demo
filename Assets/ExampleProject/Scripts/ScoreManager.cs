using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;
	[SerializeField] TextMeshProUGUI textMeshProUGUI;
	public int score { get; private set; }

	void Awake()
	{
		Instance = this;
		GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
	}
	void OnDestroy() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
	void GameManager_OnGameStateChanged(GameState state)
	{
		if (state == GameState.InitialStart || state == GameState.RestartGame)
		{
			score = 0;
		}
	}
	void Start() => textMeshProUGUI.text = "SCORE: " + score;
	void Update() => textMeshProUGUI.text = "SCORE: " + score;
	public void IncreaseScore(int amount) => score += amount;
}
