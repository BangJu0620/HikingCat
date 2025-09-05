using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadIcon : MonoBehaviour
{
    [SerializeField] Image loadIcon;

    private void Update()
    {
        if (loadIcon != null)
        {
            float rotationSpeed = 180f; // degrees per second
            loadIcon.rectTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}
