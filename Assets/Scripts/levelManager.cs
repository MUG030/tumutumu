using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class levelManager : MonoBehaviour
{

    private List<Fruit> _AllFruits = new List<Fruit>();

    /// <summary>�I�𒆂̃t���[�c</summary>
    private List<Fruit> _SelectFruites = new List<Fruit>();

    /// <summary>�I�𒆂̃t���[�cID</summary>
    private string _SelectID = "";

    /// <summary>�V���O���g���C���X�^���X</summary>
    public static levelManager Instance { get; private set; }

    public GameObject[] FruitPrefabs;

    /// <summary>�I����`��I�u�W�F�N�g</summary>
    public LineRenderer LineRenderer;

    /// <summary>�{��Prefab</summary>
    public GameObject BomPrefab;

    /// <summary>�t���[�c��3�q���Ȃ��Ə����Ȃ�</summary>
    public int FruitDestroyCount = 3;

    /// <summary>�t���[�c���q���͈�</summary>
    public float FruitConnectRange = 1.5f;

    /// <summary>�{���𐶐����邽�߂ɕK�v�ȃt���[�c�̐�</summary>
    public int BomSpawnCount = 5;

    /// <summary>�{���ŏ����͈�</summary>
    public float BomDeatroyRange = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        FruitSpawn(40);
    }

    // Update is called once per frame
    void Update()
    {
        LineRendererUpdate();
    }

    /// <summary>
    /// �I�𒆂̃t���[�c���q�����̕`����X�V
    /// </summary>
    private void LineRendererUpdate()
    {
        if (_SelectFruites.Count >= 2)
        {
            LineRenderer.positionCount = _SelectFruites.Count;
            LineRenderer.SetPositions(_SelectFruites.Select(fruit => fruit.transform.position).ToArray());
            LineRenderer.gameObject.SetActive(true);
        }
        else LineRenderer.gameObject.SetActive(false);
    }

    /// <summary>
    /// �t���[�c����
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
            var FruitObject = Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)], Position, Quaternion.identity);
            _AllFruits.Add(FruitObject.GetComponent<Fruit>());

            X++;
            if(X==MaxX)
            {
                X = 0;
                Y++;
            }
        }
    }

    /// <summary>
    /// FruitDown�C�x���g
    /// </summary>
    /// <param name="fruit">�t���[�c</param>
    public void FruitDown(Fruit fruit)
    {
        _SelectFruites.Add(fruit);
        fruit.SetIsSelect(true);

        _SelectID = fruit.ID;
    }

    /// <summary>
    /// FruitEnter�C�x���g
    /// </summary>
    /// <param name="fruit">�t���[�c</param>
    public void FruitEnter(Fruit fruit)
    {
        if (_SelectID != fruit.ID) return;

        if (fruit.IsSelect)
        {
            if(_SelectFruites.Count >= 2 && _SelectFruites[_SelectFruites.Count -2] == fruit)
            {
                var RemoveFruit = _SelectFruites[_SelectFruites.Count - 1];
                RemoveFruit.SetIsSelect(false);
                _SelectFruites.Remove(RemoveFruit);
            }
        }
        else
        {
            var Length = (_SelectFruites[_SelectFruites.Count -1].transform.position - fruit.transform.position ).magnitude;
            if(Length < FruitConnectRange)
            {
                _SelectFruites.Add(fruit);
                fruit.SetIsSelect(true);
            }
        }
    }

    /// <summary>
    /// FruitUp�C�x���g
    /// </summary>
    public void FruitUp()
    {
        if(_SelectFruites.Count >= FruitDestroyCount)
        {
            DestroyFruits(_SelectFruites);
            if (_SelectFruites.Count >= BomSpawnCount)
                Instantiate(BomPrefab, _SelectFruites[_SelectFruites.Count -1].transform.position, Quaternion.identity);
        }
        else
        {
            foreach (var FruitItem in _SelectFruites)
                FruitItem.SetIsSelect(false);
        }

        _SelectID = "";
        _SelectFruites.Clear();
    }

    /// <summary>
    /// �{����������
    /// </summary>
    /// <param name="bom"></param>
    public void BomDown(Bom bom)
    {
        var RemoveFruits = new List<Fruit>();

        foreach(var FruitItem in _AllFruits)
        {
            var Length = (FruitItem.transform.position - bom.transform.position).magnitude;
            if (Length < BomDeatroyRange)
                RemoveFruits.Add(FruitItem);
        }

        DestroyFruits(RemoveFruits);
        Destroy(bom.gameObject);
    }

    /// <summary>
    /// �t���[�c��T��
    /// </summary>
    /// <param name="fruits"></param>
    private void DestroyFruits(List<Fruit> fruits)
    {
        foreach(var FruitItem in fruits)
        {
            Destroy(FruitItem.gameObject);
            _AllFruits.Remove(FruitItem);
        }

        FruitSpawn(fruits.Count);
    }
}
