using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    /// <summary>識別用ID</summary>
    public string ID;

    /// <summary>選択状態表示Sprite</summary>
    public GameObject SelectSprite;

    /// <summary>選択状態</summary>
    public bool IsSelect { get; private set; }

    /// <summary>
    /// OnMouseDownイベント
    /// </summary>
    private void OnMouseDown()
    {
        levelManager.Instance.FruitDown(this);
    }

    /// <summary>
    /// OnMouseEnterイベント
    /// </summary>
    private void OnMouseEnter()
    {
        levelManager.Instance.FruitEnter(this);
    }

    /// <summary>
    /// OnMouseUpイベント
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
