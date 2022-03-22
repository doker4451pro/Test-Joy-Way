using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    [System.Serializable]
    public class PoolItem
    {
        public string Name;
        [Header("Only IPoolObject")]
        public GameObject ObjectToPool;
        public int StartCapacity = 10;
    }

    [SerializeField]
    private List<PoolItem> itemsToPool;
    private Dictionary<string, List<GameObject>> PoolDict;

    private void Awake()
    {
        Instance = this;
        PoolDict = new Dictionary<string, List<GameObject>>();

        foreach (var item in itemsToPool)
        {
            var poolList = new List<GameObject>();
            for (int i = 0; i < item.StartCapacity; i++)
            {
                var gameObject = Instantiate(item.ObjectToPool);
                gameObject.SetActive(false);
                poolList.Add(gameObject);
            }
            PoolDict.Add(item.Name, poolList);
        }
    }
    public GameObject GetPoolObjectByName(string name)
    {
        var list = new List<GameObject>();
        if (PoolDict.TryGetValue(name, out list))
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].activeSelf)
                {
                    list[i].GetComponent<IPoolObject>().GetFromPool();
                    return list[i];
                }
            }
            var newItem = Instantiate(list[0]);
            list.Add(newItem);
            newItem.GetComponent<IPoolObject>().GetFromPool();
            return newItem;
        }
        else
        {
            return null;
        }
    }

    public void ReturnPoolObject(IPoolObject gameObject)
    {
        gameObject.ReturnToPool();
    }
}
