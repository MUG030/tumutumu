using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    /// <summary>識別用ID</summary>
    public string ID;

    /// <summary>選択状態表示Sprite</summary>
    public GameObject SelectSprite;

    /// <summary>
    /// OnMouseDownイベント
    /// </summary>
    private void OnMouseDown()
    {
        SelectSprite.SetActive(true);
    }

    /// <summary>
    /// OnMouseEnterイベント
    /// </summary>
    private void OnMouseEnter()
    {
        
    }

    /// <summary>
    /// OnMouseUpイベント
    /// </summary>
    private void OnMouseUp()
    {
        SelectSprite.SetActive(false);
    }
}
