// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class BGM : MonoBehaviour
// {
//     private static BGM instance;
//     [SerializeField] private AudioSource audioSource;
//     [SerializeField] private AudioClip defaultClip;
//     [SerializeField] private AudioClip endingClip;
//     [SerializeField] private float fadeDuration = 0.75f;
//     [SerializeField] private bool shouldLoop = true;

//     private void Awake()
//     {
//         if (instance != null && instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }

//         instance = this;
//         DontDestroyOnLoad(gameObject);

//         ApplyForScene(SceneManager.GetActiveScene());

//         if (SceneLoader.Instance)
//             SceneLoader.Instance.OnSceneLoaded += ApplyForScene;
//     }

//     private void OnDestroy()
//     {
//         if (SceneLoader.Instance)
//             SceneLoader.Instance.OnSceneLoaded -= ApplyForScene;
//     }

//     private void ApplyForScene(Scene scene)
//     {
//         if (!SoundManager.Instance) return;

//         bool isEnding = SceneLoader.Instance && SceneLoader.Instance.IsEndingScene();
//         var clip = isEnding ? endingClip : defaultClip;

//         if (clip) SoundManager.Instance.PlayBGM(clip, shouldLoop, fadeDuration);
//     }
//     private void Update()
//     {
//         if (SceneLoader.Instance.IsEndingScene())
//         {
//             if (audioSource.clip != endingClip)
//             {
//                 audioSource.Stop();
//                 audioSource.clip = endingClip;
//                 audioSource.loop = true;
//                 audioSource.Play();
//             }
//         }
//         else
//         {
//             if (audioSource.clip != defaultClip)
//                 PlayDefault();
//         }
//     }

//     private void PlayDefault()
//     {
//         if (defaultClip && audioSource)
//         {
//             audioSource.Stop();
//             audioSource.clip = defaultClip;
//             audioSource.loop = true;
//             audioSource.Play();
//         }
//     }
// }
