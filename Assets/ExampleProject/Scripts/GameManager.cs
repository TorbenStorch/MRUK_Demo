using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameState State;
	public static event Action<GameState> OnGameStateChanged;


	private void Awake() => Instance = this;
	private void Start() => UpdateGameState(GameState.InitialStart);


	public void UpdateGameState(GameState newState)
	{
		State = newState;

		switch (newState)
		{
			case GameState.InitialStart:
				break;
			case GameState.TimerCountdown:
				break;
			case GameState.EndGame:
				break;
			case GameState.RestartGame:
				break;
			default:
				break;
		}

		OnGameStateChanged?.Invoke(newState);
	}
}
public enum GameState
{
	InitialStart,
	TimerCountdown,
	EndGame,
	RestartGame
}