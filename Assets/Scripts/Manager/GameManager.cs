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

    public GameData gameData;
    public float gameTime;

    public void GameStart()
    {
        gameData = new GameData();
        gameTime = 0;
        ResumeGame();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
            gameTime += Time.deltaTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void GameClear()
    {

    }
}
