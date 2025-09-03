using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    [SerializeField] private AudioClip[] footStepSFXs;
    [SerializeField] private AudioClip[] jumpStepSFXs;
    [SerializeField] private AudioClip[] wallHitSFXs;

    [SerializeField] private float FootStepVolume;
    [SerializeField] private float JumpStepVolume;
    [SerializeField] private float WallHitVolume;

    public void PlayFootStepSFX()
    {
        if (footStepSFXs.Length > 0)
        {
            SoundManager.Instance.PlaySFX(footStepSFXs[Random.Range(0, footStepSFXs.Length)], FootStepVolume);
        }
    }

    public void PlayJumpStepSFX()
    {
        if (jumpStepSFXs.Length > 0)
        {
            SoundManager.Instance.PlaySFX(jumpStepSFXs[Random.Range(0, jumpStepSFXs.Length)], JumpStepVolume);
        }
    }

    public void PlayWallHitSFX()
    {
        if (wallHitSFXs.Length > 0)
        {
            SoundManager.Instance.PlaySFX(wallHitSFXs[Random.Range(0, wallHitSFXs.Length)], WallHitVolume);
        }
    }
}
