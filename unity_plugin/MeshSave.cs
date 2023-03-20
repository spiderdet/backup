using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MeshSave : MonoBehaviour
{
#if UNITY_EDITOR
    class MeshInfo 
    {
        public List<int> indexs = new List<int>();
        public List<Vector3> vertices = new List<Vector3>();
    }
    public void SaveAsset()
    {
        try
        {
            //获取所有顶点.
            Dictionary<Vector3, int> verticesXY = new Dictionary<Vector3, int>();
            MeshFilter[] meshs = this.GetComponentsInChildren<MeshFilter>();
            int index = 0;  
            foreach (MeshFilter meshFilter in meshs)
            {
                
                Mesh mesh = meshFilter.sharedMesh;
                if (mesh != null)
                {
                    Vector3[] vector3s = mesh.vertices;

                    foreach (Vector3 v in vector3s)
                    {
                        if (!verticesXY.ContainsKey(v))
                        {
                            Debug.Log(v);
                            index++;
                            verticesXY.Add(v, index);

                        }
                    }
                    Debug.Log("------"+ meshFilter.gameObject.name);
                }
            }

            //写入json.
            MeshInfo meshInfo = new MeshInfo();
            foreach (Vector3 vertice in verticesXY.Keys)
            {
                meshInfo.indexs.Add(verticesXY[vertice]);
                meshInfo.vertices.Add(vertice);
            }
            string json = JsonUtility.ToJson(meshInfo, true);
            Debug.Log(json);
            File.WriteAllText(Application.dataPath + "\\" + this.gameObject.name + ".json", json);
            Debug.Log("保存成功");
        }
        catch (Exception e)
        {
            Debug.LogWarning("提取mesh失败：" + e.ToString());
        }
    }
#endif
}


