using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        T origin = Resources.Load<T>(path);
        
        if(origin == null)
        {
            Debug.Log($"Faild load {path}");
            return null;
        }

        return origin;
    }

    public GameObject Instantiate(string path, Transform parents = null)
    {
        GameObject origin = Load<GameObject>($"Prefabs/{path}");
        
        if (origin == null)
            return null;

        GameObject go = null;
        
        go = Object.Instantiate(origin, parents);
        go.name = origin.name;
        return go;
    }

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation, Transform parents = null)
    {
        GameObject origin = Load<GameObject>($"Prefabs/{path}");

        if (origin == null)
            return null;
        
        GameObject go = Instantiate(path, parents);
        go.transform.position = position;
        go.transform.rotation = rotation;

        return go;
    }

    public void Destory(GameObject go, float time = 0)
    {
        if (go == null)
        {
            Debug.Log("Null Exception: Gameobjec is null");
            return;
        }
        
        Object.Destroy(go, time);
    }
}
