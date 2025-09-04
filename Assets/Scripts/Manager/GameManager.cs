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
}
