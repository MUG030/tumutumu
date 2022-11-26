using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    /// <summary>
    /// MouseDownƒCƒxƒ“ƒg
    /// </summary>
    private void OnMouseDown()
    {
        levelManager.Instance.BomDown(this);
    }
}
