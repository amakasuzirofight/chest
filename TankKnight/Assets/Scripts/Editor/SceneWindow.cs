using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWindow : EditorWindow
{
    // �{�^���̑傫��
    private readonly Vector2 _buttonMinSize = new Vector2(100, 20);
    private readonly Vector2 _buttonMaxSize = new Vector2(1000, 100);

    //! MenuItem("���j���[��/���ږ�") �̃t�H�[�}�b�g�ŋL�ڂ��Ă�
    [MenuItem("Window/SceneChange")]
    static void ShowWindow()
    {
        // �E�B���h�E��\���I
        EditorWindow.GetWindow<SceneWindow>();
    }

    void OnGUI()
    {
        // ���C�A�E�g�𐮂���
        GUIStyle buttonStyle = new GUIStyle("button") { fontSize = 30 };
        var layoutOptions = new GUILayoutOption[]
        {
            GUILayout.MinWidth(_buttonMinSize.x),
            GUILayout.MinHeight(_buttonMinSize.y),
            GUILayout.MaxWidth(_buttonMaxSize.x),
            GUILayout.MaxHeight(_buttonMaxSize.y)
        };

        // Title�{�^�������
        if (GUILayout.Button("Title", buttonStyle, layoutOptions))
        {
            // �V�[����ۑ����邩�m�F
            if (!EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { SceneManager.GetActiveScene() })) return;
            // Title�V�[�����J��
            OpenScene("Title");
        }  
        if (GUILayout.Button("AmaTest", buttonStyle, layoutOptions))
        {
            // �V�[����ۑ����邩�m�F
            if (!EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { SceneManager.GetActiveScene() })) return;
            // Title�V�[�����J��
            OpenScene("AmaTest");
        } 
        if (GUILayout.Button("StoneWalls_sample", buttonStyle, layoutOptions))
        {
            // �V�[����ۑ����邩�m�F
            if (!EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { SceneManager.GetActiveScene() })) return;
            // Title�V�[�����J��
            OpenScene("StoneWalls_sample");
        }
    }

    // �V�[�����J����֐�
    private void OpenScene(string sceneName)
    {
        var sceneAssets = AssetDatabase.FindAssets("t:SceneAsset")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(path => AssetDatabase.LoadAssetAtPath(path, typeof(SceneAsset)))
            .Where(obj => obj != null)
            .Select(obj => (SceneAsset)obj)
            .Where(asset => asset.name == sceneName);
        var scenePath = AssetDatabase.GetAssetPath(sceneAssets.First());
        EditorSceneManager.OpenScene(scenePath);
    }
}

