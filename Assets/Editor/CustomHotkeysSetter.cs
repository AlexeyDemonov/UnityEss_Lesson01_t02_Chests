using UnityEngine;
using UnityEditor;

public class CustomHotkeysSetter
{
    [MenuItem("Edit/CustomHotkeys/PlayOrStop _F5")]
    static void PlayOrStopGame()
    {
        if (!EditorApplication.isPlaying)
            EditorApplication.ExecuteMenuItem("File/Save");

        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("GameObject/CustomHotkeys/ResetTransform #R")]
    static void ResetTransform()
    {
        var selectedObject = Selection.activeGameObject;

        if(selectedObject != null)
        {
            selectedObject.transform.position = Vector3.zero;
            selectedObject.transform.rotation = Quaternion.identity;
            selectedObject.transform.localScale = Vector3.one;
        }
    }
}