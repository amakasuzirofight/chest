using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirector : MonoBehaviour
{
    //タイルオブジェクト
    public static Dictionary<int, GameObject> tileObjs { get; private set; } = new Dictionary<int, GameObject>();
    void Start()
    {

    }
    /// <summary>
    /// タイル登録
    /// </summary>
    /// <param name="obj"></param>
    public bool RegistTileObjects(int registNum, GameObject obj)
    {
        if (tileObjs.ContainsKey(registNum) == false)
        {
            //Tiledataを生成したオブジェクトにアタッチ
            TileData data = obj.AddComponent<TileData>();
            data.tileNumber = registNum;
            tileObjs.Add(registNum, obj);
            //子オブジェクトにする
            
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
