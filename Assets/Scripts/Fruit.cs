using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    /// <summary>���ʗpID</summary>
    public string ID;

    /// <summary>�I����ԕ\��Sprite</summary>
    public GameObject SelectSprite;

    /// <summary>
    /// OnMouseDown�C�x���g
    /// </summary>
    private void OnMouseDown()
    {
        SelectSprite.SetActive(true);
    }

    /// <summary>
    /// OnMouseEnter�C�x���g
    /// </summary>
    private void OnMouseEnter()
    {
        
    }

    /// <summary>
    /// OnMouseUp�C�x���g
    /// </summary>
    private void OnMouseUp()
    {
        SelectSprite.SetActive(false);
    }
}
