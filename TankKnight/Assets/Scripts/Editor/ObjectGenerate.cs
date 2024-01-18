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
        EditorGUILayout.LabelField("�X�e�[�W�^�C������");

        X = EditorGUILayout.IntField("���}�X", X);
        Y = EditorGUILayout.IntField("�c�}�X", Y);


        if (GUILayout.Button("�����{�^��"))
        {
            //��ɂ��łɂ���^�C����p��
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
            //�ǂ�����O�ł���Ή������Ȃ�
            if (X == 0 && Y == 0)
            {
                return;
            }

            //�I�u�W�F�N�g��������
            int registNum = 0;  //�^�C���o�^�p�ԍ�
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
        if (GUILayout.Button("�j��"))
        {
            TileDirector tileDirectObj = (FindObjectOfType(typeof(TileDirector))as TileDirector);

            //��ɂ��łɂ���^�C����p��
            tileDirectObj.ResetRegistTileObjects();
            DestroyImmediate(tileDirectObj.gameObject);
        }
        bool isOpen = EditorGUILayout.Foldout(_isOpen, "�����p�f�[�^");

        //�܂肽���݂̏�Ԃ��ς�����烍�O�\��
        if (_isOpen != isOpen)
        {
            _isOpen = isOpen;
            //Debug.Log(_isOpen ? "�J���܂����I" : "���܂����I");
        }

        //�J���Ă��鎞��GUI�ǉ�
        if (isOpen)
        {
            EditorGUI.indentLevel++;
            mapInstanceEditorData = EditorGUILayout.ObjectField(mapInstanceEditorData, typeof(MapInstanceEditorData), true) as MapInstanceEditorData;
            EditorGUI.indentLevel--;
        }
    }
}
#endif
