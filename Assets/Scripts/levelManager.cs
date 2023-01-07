using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class levelManager : MonoBehaviour
{

    private List<Fruit> _AllFruits = new List<Fruit>();

    /// <summary>�I�𒆂̃t���[�c</summary>
    private List<Fruit> _SelectFruites = new List<Fruit>();

    /// <summary>�I�𒆂̃t���[�cID</summary>
    private string _SelectID = "";

    /// <summary>�X�R�A</summary>
    private int _Score = 0;

    /// <summary>���ݎ���{s}</summary>
    public float _CurrentTime = 60;

    /// <summary>�v���C���̏��</summary>
    public bool _IsPlaying = true;

    /// <summary>�V���O���g���C���X�^���X</summary>
    public static levelManager Instance { get; private set; }

    public GameObject[] FruitPrefabs;

    /// <summary>�I����`��I�u�W�F�N�g</summary>
    public LineRenderer LineRenderer;

    /// <summary>�{��Prefab</summary>
    public GameObject BomPrefab;

    /// <summary>�X�R�A�\���e�L�X�g</summary>
    public TextMeshProUGUI ScoreText;

    /// <summary>���ԕ\���e�L�X�g</summary>
    public TextMeshProUGUI TimerText;
    /// <summary>�I�����</summary>
    public GameObject FinishDialog;

    /// <summary>�t���[�c��3�q���Ȃ��Ə����Ȃ�</summary>
    public int FruitDestroyCount = 3;

    /// <summary>�t���[�c���q���͈�</summary>
    public float FruitConnectRange = 1.5f;

    /// <summary>�{���𐶐����邽�߂ɕK�v�ȃt���[�c�̐�</summary>
    public int BomSpawnCount = 5;

    /// <summary>�{���ŏ����͈�</summary>
    public float BomDeatroyRange = 1.5f;

    /// <summary>�v���C����{s}</summary>
    public float PlayTime = 60;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        FruitSpawn(40);
        ScoreText.text = "0";
        _CurrentTime = PlayTime;
    }

    // Update is called once per frame
    void Update()
    {
        LineRendererUpdate();
        TimerUpdate();
    }

    /// <summary>
    /// ���ԍX�V
    /// </summary>
    private void TimerUpdate()
    {
        if(_IsPlaying)
        {
            _CurrentTime -= Time.deltaTime;
            if(_CurrentTime <= 0)
            {
                _CurrentTime = 0;
                FruitUp();
                _IsPlaying = false;
                FinishDialog.SetActive(true);
            }
            TimerText.text = ((int)_CurrentTime).ToString();
        }
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
            if (X == MaxX)
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
        if (!_IsPlaying) return;
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
        if (!_IsPlaying) return;
        if (_SelectID != fruit.ID) return;

        if (fruit.IsSelect)
        {
            if (_SelectFruites.Count >= 2 && _SelectFruites[_SelectFruites.Count - 2] == fruit)
            {
                var RemoveFruit = _SelectFruites[_SelectFruites.Count - 1];
                RemoveFruit.SetIsSelect(false);
                _SelectFruites.Remove(RemoveFruit);
            }
        }
        else
        {
            var Length = (_SelectFruites[_SelectFruites.Count - 1].transform.position - fruit.transform.position).magnitude;
            if (Length < FruitConnectRange)
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
        if (!_IsPlaying) return;

        if (_SelectFruites.Count >= FruitDestroyCount)
        {
            DestroyFruits(_SelectFruites);
            if (_SelectFruites.Count >= BomSpawnCount)
                Instantiate(BomPrefab, _SelectFruites[_SelectFruites.Count - 1].transform.position, Quaternion.identity);
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
        if (!_IsPlaying) return;

        var RemoveFruits = new List<Fruit>();

        foreach (var FruitItem in _AllFruits)
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
        foreach (var FruitItem in fruits)
        {
            Destroy(FruitItem.gameObject);
            _AllFruits.Remove(FruitItem);
        }

        FruitSpawn(fruits.Count);
        AddScore(fruits.Count);
    }

    /// <summary>
    /// �X�R�A��ǉ�
    /// </summary>
    /// <param name="fruiteCount">�������t���[�c�̐�</param>
    private void AddScore(int fruiteCount)
    {
        _Score += (int)(fruiteCount * 100 * (1 + (fruiteCount - 3) * 0.1f));
        ScoreText.text = _Score.ToString();
    }
}
