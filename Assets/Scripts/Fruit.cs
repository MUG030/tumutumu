using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    /// <summary>���ʗpID</summary>
    public string ID;

    /// <summary>�I����ԕ\��Sprite</summary>
    public GameObject SelectSprite;

    /// <summary>�I�����</summary>
    public bool IsSelect { get; private set; }

    /// <summary>
    /// OnMouseDown�C�x���g
    /// </summary>
    private void OnMouseDown()
    {
        levelManager.Instance.FruitDown(this);
    }

    /// <summary>
    /// OnMouseEnter�C�x���g
    /// </summary>
    private void OnMouseEnter()
    {
        levelManager.Instance.FruitEnter(this);
    }

    /// <summary>
    /// OnMouseUp�C�x���g
    /// </summary>
    private void OnMouseUp()
    {
        levelManager.Instance.FruitUp();
    }

    public void SetIsSelect(bool isSelect)
    {
        IsSelect = isSelect;
        SelectSprite.SetActive(isSelect);
    }
}
