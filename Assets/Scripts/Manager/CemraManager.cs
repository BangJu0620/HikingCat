using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private Camera mainCamera;

    public Transform player;
    public SpriteRenderer background;

    float cameraHeight;
    float cameraWidth;


    protected override void Initialize()
    {
        base.Initialize();
        if (mainCamera != null)
        {
            cameraHeight = mainCamera.orthographicSize;
            cameraWidth = cameraHeight * mainCamera.aspect;
        }
        dontDestroyOnLoad = false;

    }

    private void Start()
    {
        player = GameManager.Instance.player.transform;
        background = GameManager.Instance.level.backGround;

        mainCamera.transform.position = CalculaeCameraTargetPos();
    }


    void LateUpdate()
    {
        if (player != null && background != null)
        {
            Vector3 targetPos = CalculaeCameraTargetPos();

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, Time.deltaTime * 20f);
            // Relatively smooth tracking of playr positions
        }

    }

    Vector3 CalculaeCameraTargetPos()
    {
        if (player != null && background != null)
        {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y, mainCamera.transform.position.z);

            float minX = background.bounds.min.x + cameraWidth;
            //            Debug.Log("minX" + minX);
            float maxX = background.bounds.max.x - cameraWidth;
            //            Debug.Log("maxX" + maxX);
            float minY = background.bounds.min.y + cameraHeight;
            //            Debug.Log("minY" + minY);
            float maxY = background.bounds.max.y - cameraHeight;
            //            Debug.Log("maxY" + maxY);
            //Limit camera movement range

            targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
            //            Debug.Log("targetPos.x : " + targetPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);
            //            Debug.Log("targetPos.y : " + targetPos.y);
            
            return targetPos;
        }
        return Vector3.zero;
    }
}