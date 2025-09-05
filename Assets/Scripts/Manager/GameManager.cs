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

        public int fallCount => headHitCount + verticalWallHitCount + landingMissCount + deathByFootCount;
    }
    
    public enum GameState
    {
        Title, OnLoad, InGame, UI
    }

    public GameData gameData;
    public float gameTime;

    public Player player;
    public Level level;

    protected override void Initialize()
    {
        base.Initialize();
        SoundManager.Instance.PlayBGM(Constants.BGMPath.TitleBGM);
    }

    public void GameStart()
    {
        gameData = new GameData();
        gameTime = 0;
        ResumeGame();
        SoundManager.Instance.PlayBGM(Constants.BGMPath.InGameBGM);

    }

    private void Update()
    {
        if(Time.timeScale != 0)
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

    public void GoToTitle()
    {
        Initialize();
        ResumeGame();
    }

    public void EndSceneEnter()
    {
        SoundManager.Instance.PlayBGM(Constants.BGMPath.EndBGM);
    }
}
