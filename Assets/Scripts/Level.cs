using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public SpriteRenderer backGround;

    public Transform spawnPos;

    private void Awake()
    {
        GameManager.Instance.level = this;
    }
}
