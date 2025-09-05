using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public void Show(GameObject target) => target.SetActive(true);
    public void Hide(GameObject target) => target.SetActive(false);
}
