using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Serializable]
    public struct GameData
    {
        public float gameTime;
        public float fallDistance;
        public int deathCount;

        public int headHitCount;
        public int verticalWallHitCount;
        public int deathByFootCount;
        public int landingMissCount;
    }
    
    public enum GameState
    {
        Title, OnLoad, InGame, UI
    }

    public GameData gameData;
//    public GameState gameState = GameState.Title;
    public float gameTime;

    public Player player;
    public Level level;

    public void GameStart()
    {
        gameData = new GameData();
        gameTime = 0;
        ResumeGame();

    }

    private void Update()
    {
//        if (gameState == GameState.InGame)
        if(Time.timeScale != 0)
            gameTime += Time.deltaTime;
    }

    public void PauseGame()
    {
//        gameState = GameState.UI;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
//        gameState = GameState.InGame;
        Time.timeScale = 1f;
    }

    public void GameClear()
    {

    }
}
