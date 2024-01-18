using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirector : MonoBehaviour
{
    //�^�C���I�u�W�F�N�g
    public static Dictionary<int, GameObject> tileObjs { get; private set; } = new Dictionary<int, GameObject>();
    void Start()
    {

    }
    /// <summary>
    /// �^�C���o�^
    /// </summary>
    /// <param name="obj"></param>
    public bool RegistTileObjects(int registNum, GameObject obj)
    {
        if (tileObjs.ContainsKey(registNum) == false)
        {
            //Tiledata�𐶐������I�u�W�F�N�g�ɃA�^�b�`
            TileData data = obj.AddComponent<TileData>();
            data.tileNumber = registNum;
            tileObjs.Add(registNum, obj);
            //�q�I�u�W�F�N�g�ɂ���
            
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetRegistTileObjects()
    {
        if (tileObjs.Count == 0)
        {
            return;
        }
        foreach (var item in tileObjs)
        {
            DestroyImmediate(item.Value);
        }
        tileObjs.Clear();
    }


}
