using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    /// <summary>選択中のフルーツ</summary>
    private List<Fruit> _SelectFruites = new List<Fruit>();

    /// <summary>選択中のフルーツID</summary>
    private string _SelectID = "";

    /// <summary>シングルトンインスタンス</summary>
    public static levelManager Instance { get; private set; }

    public GameObject[] FruitPrefabs;

    /// <summary>フルーツを3つ繋げないと消せない</summary>
    public int FruitDestroyCount = 3;

    /// <summary>フルーツを繋ぐ範囲</summary>
    public float FruitConnectRange = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        FruitSpawn(40);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// フルーツ生成
    /// </summary>
    /// <param name="count"></param>
    private void FruitSpawn(int count)
    {
        var StartX = -2;
        var StartY = 5;
        var X = 0;
        var Y = 0;
        var MaxX = 5;

        for (int i = 0; i < count; i++)
        {
            var Position = new Vector3(StartX + X, StartY + Y, 0);
            Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)], Position, Quaternion.identity);

            X++;
            if(X==MaxX)
            {
                X = 0;
                Y++;
            }
        }
    }

    /// <summary>
    /// FruitDownイベント
    /// </summary>
    /// <param name="fruit">フルーツ</param>
    public void FruitDown(Fruit fruit)
    {
        _SelectFruites.Add(fruit);
        fruit.SetIsSelect(true);

        _SelectID = fruit.ID;
    }

    /// <summary>
    /// FruitDownイベント
    /// </summary>
    /// <param name="fruit">フルーツ</param>
    public void FruitEnter(Fruit fruit)
    {
        if (_SelectID != fruit.ID) return;

        if (fruit.IsSelect)
        {

        }
        else
        {
            
        }
    }

    /// <summary>
    /// FruitDownイベント
    /// </summary>
    public void FruitUp()
    {

    }
}
