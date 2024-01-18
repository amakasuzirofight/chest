using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWindow : EditorWindow
{
    // ボタンの大きさ
    private readonly Vector2 _buttonMinSize = new Vector2(100, 20);
    private readonly Vector2 _buttonMaxSize = new Vector2(1000, 100);

    //! MenuItem("メニュー名/項目名") のフォーマットで記載してね
    [MenuItem("Window/SceneChange")]
    static void ShowWindow()
    {
        // ウィンドウを表示！
        EditorWindow.GetWindow<SceneWindow>();
    }

    void OnGUI()
    {
        // レイアウトを整える
        GUIStyle buttonStyle = new GUIStyle("button") { fontSize = 30 };
        var layoutOptions = new GUILayoutOption[]
        {
            GUILayout.MinWidth(_buttonMinSize.x),
            GUILayout.MinHeight(_buttonMinSize.y),
            GUILayout.MaxWidth(_buttonMaxSize.x),
            GUILayout.MaxHeight(_buttonMaxSize.y)
        };

        // Titleボタンを作る
        if (GUILayout.Button("Title", buttonStyle, layoutOptions))
        {
            // シーンを保存するか確認
            if (!EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { SceneManager.GetActiveScene() })) return;
            // Titleシーンを開く
            OpenScene("Title");
        }  
        if (GUILayout.Button("AmaTest", buttonStyle, layoutOptions))
        {
            // シーンを保存するか確認
            if (!EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { SceneManager.GetActiveScene() })) return;
            // Titleシーンを開く
            OpenScene("AmaTest");
        } 
        if (GUILayout.Button("StoneWalls_sample", buttonStyle, layoutOptions))
        {
            // シーンを保存するか確認
            if (!EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { SceneManager.GetActiveScene() })) return;
            // Titleシーンを開く
            OpenScene("StoneWalls_sample");
        }
    }

    // シーンを開ける関数
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

