 using UnityEngine;
 using System.Collections;
 using UnityEditor;
using Unity.VisualScripting;

public class MeshInfo : EditorWindow
{
    private int vertexCount;
    private int submeshCount;
    private int triangleCount;
 
    [MenuItem("Tools/Mesh Info")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MeshInfo window = (MeshInfo)EditorWindow.GetWindow(typeof(MeshInfo));
        window.titleContent.text = "Mesh Info";
    }
 
    void OnSelectionChange()
    {
        Repaint();
    }
 
    void OnGUI()
    {
        vertexCount = 0;
        //triangleCount = 0;
        //submeshCount = 0;
        float dis = 0f;
        
        foreach (GameObject g in Selection.gameObjects)
        {
            foreach(MeshFilter mf in g.GetComponentsInChildren<MeshFilter>())
            {
                vertexCount += mf.sharedMesh.vertexCount;
                Vector3[] vWorldPos = mf.sharedMesh.vertices;
                if (vertexCount == 2) {
                    dis = Vector3.Distance(vWorldPos[0], vWorldPos[1]);
                }
                EditorGUILayout.LabelField("Vertices: ", vertexCount.ToString());
                foreach (Vector3 v in vWorldPos) {
                    //EditorGUILayout.LabelField("x: "+v.);
                }
                EditorGUILayout.LabelField("distance : ", dis.ToString());
                //triangleCount += mf.sharedMesh.triangles.Length / 3;
                //submeshCount += mf.sharedMesh.subMeshCount;
            }
        }
 
        
        //EditorGUILayout.LabelField("Triangles: ", triangleCount.ToString());
        //EditorGUILayout.LabelField("SubMeshes: ", submeshCount.ToString());
    }
}
