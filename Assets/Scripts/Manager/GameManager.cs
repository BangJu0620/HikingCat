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

    public void GameStart()
    {
        gameData = new GameData();
        ResumeGame();
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
