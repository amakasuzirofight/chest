#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;


public class ObjectGenerate : EditorWindow
{
    [MenuItem("EditorExtensions/MultiPrefabGenerator")]
    private static void Open()
    {
        GetWindow<ObjectGenerate>("Generate multi prefab.");
    }

    public static int X { get; private set; }
    public static int Y { get; private set; }
    public static MapInstanceEditorData mapInstanceEditorData { get; private set; }

    public List<GameObject> _targetObjectsInEditor;

    private bool _isOpen = true;

    private void OnGUI()
    {
        EditorGUILayout.LabelField("ステージタイル生成");

        X = EditorGUILayout.IntField("横マス", X);
        Y = EditorGUILayout.IntField("縦マス", Y);


        if (GUILayout.Button("生成ボタン"))
        {
            //先にすでにあるタイルを廃棄
            TileDirector tileDirectObj = FindObjectOfType(typeof(TileDirector)) as TileDirector;
            GameObject parrent;
            if (tileDirectObj == null)
            {
                parrent = PrefabUtility.InstantiatePrefab(mapInstanceEditorData.getTileDirector) as GameObject;
            }
            else
            {
                parrent = tileDirectObj.gameObject;
            }
            TileDirector tileDirector = parrent.GetComponent<TileDirector>();
            tileDirector.ResetRegistTileObjects();
            //どちらも０であれば何もしない
            if (X == 0 && Y == 0)
            {
                return;
            }

            //オブジェクト生成する
            int registNum = 0;  //タイル登録用番号
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    GameObject obj =
                        PrefabUtility.InstantiatePrefab(mapInstanceEditorData.getInstanceObj) as GameObject;
                    obj.transform.position = new Vector3(j, 0, i);
                    tileDirector.RegistTileObjects(registNum, obj);
                    obj.transform.parent = parrent.transform;
                    registNum++;

                }
            }
        }
        EditorGUILayout.Space(5);
        if (GUILayout.Button("破棄"))
        {
            TileDirector tileDirectObj = (FindObjectOfType(typeof(TileDirector))as TileDirector);

            //先にすでにあるタイルを廃棄
            tileDirectObj.ResetRegistTileObjects();
            DestroyImmediate(tileDirectObj.gameObject);
        }
        bool isOpen = EditorGUILayout.Foldout(_isOpen, "生成用データ");

        //折りたたみの状態が変わったらログ表示
        if (_isOpen != isOpen)
        {
            _isOpen = isOpen;
            //Debug.Log(_isOpen ? "開きました！" : "閉じました！");
        }

        //開いている時はGUI追加
        if (isOpen)
        {
            EditorGUI.indentLevel++;
            mapInstanceEditorData = EditorGUILayout.ObjectField(mapInstanceEditorData, typeof(MapInstanceEditorData), true) as MapInstanceEditorData;
            EditorGUI.indentLevel--;
        }
    }
}
#endif
