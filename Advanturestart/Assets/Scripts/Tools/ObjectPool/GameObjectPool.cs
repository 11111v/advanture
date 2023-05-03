using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity中GameObject的池子
///     与一般Object的区别是：GameObject可直接出现在场景（Scene）中
/// </summary>
public class GameObjectPool : Singleton<GameObjectPool>
{
    public bool shouldShowLog;
    public Transform root;

    // 预制体-对象池
    private Dictionary<GameObject, ObjectPool<GameObject>> prefabLookup;
    // 实例-对象池（隶属于同一预制体的实例对应的对象池是一样的）
    private Dictionary<GameObject, ObjectPool<GameObject>> instanceLookup;

    private bool hasRefreshedLog = false;

    public void Awake()
    {
        Init();
    }

    public void Update()
    {
        if (shouldShowLog && hasRefreshedLog)
        {
            ShowLog();
            hasRefreshedLog = false;
        }
    }

    private void Init()
    {
        prefabLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
        instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
    }

    public void WarmPoolNonStatic(GameObject prefab, int size, bool isActive,Transform parent=null)
    {
        var pool = new ObjectPool<GameObject>(() => { return InstantiatePrefab(prefab, isActive,parent); }, size);
        prefabLookup[prefab] = pool;

        hasRefreshedLog = true;
    }

    public GameObject CreateObjectNonStatic(GameObject prefab)
    {
        return CreateObjectNonStatic(prefab, Vector3.zero, Quaternion.identity);
    }

    public GameObject CreateObjectNonStatic(GameObject prefab,Vector3 position, Quaternion rotation, Transform parent=null)
    {
        if (!prefabLookup.ContainsKey(prefab))
            WarmPool(prefab, 1, true);

        var pool = prefabLookup[prefab];

        var go = pool.GetItem();
        if(parent!=null)
        {

            go.transform.SetParent(parent);
            go.transform.localPosition = position;
            go.transform.localRotation = rotation;
        }
        else
        {

            go.transform.position = position;
            go.transform.rotation = rotation;
        }
        go.SetActive(true);

        instanceLookup.Add(go, pool);
        hasRefreshedLog = true;
        return go;
    }

    public void ReleaseObjectNonStatic(GameObject go)
    {
        go.SetActive(false);

        if (instanceLookup.ContainsKey(go))
        {
            instanceLookup[go].ReleaseItem(go);
            instanceLookup.Remove(go);
            hasRefreshedLog = true;
        }
    }

    private GameObject InstantiatePrefab(GameObject prefab, bool isActive,Transform parent=null)
    {
        var go = GameObject.Instantiate(prefab) as GameObject;
        if (isActive)
            go.SetActive(true);
        else
            go.SetActive(false);

        if (parent != null)
            go.transform.SetParent(parent);
        go.transform.localScale=Vector3.one;
        go.transform.localPosition = new Vector3(0, 0, -300);

        return go;
    }

    public void ShowLog()
    {
        foreach (var item in prefabLookup)
        {
            Debug.Log(string.Format("“游戏对象池”  预制体名称：{0}  {1}个在被使用，共有{2}个", item.Key.name, item.Value.CountUsedItems, item.Value.Count));
        }
    }

    #region 静态接口（供外部直接调用）
    public static void WarmPool(GameObject prefab, int size, bool isActive = false,Transform parent=null)
    {
        Instance.WarmPoolNonStatic(prefab, size, isActive,parent);
    }

    public static GameObject CreateObject(GameObject prefab)
    {
        return Instance.CreateObjectNonStatic(prefab);
    }

    public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation,Transform parent=null)
    {
        return Instance.CreateObjectNonStatic(prefab, position, rotation, parent);
    }

    public static void ReleaseObject(GameObject go)
    {
        Instance.ReleaseObjectNonStatic(go);
    }
    #endregion
}