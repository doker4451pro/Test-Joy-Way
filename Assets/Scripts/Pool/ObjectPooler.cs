﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }
    
    [SerializeField] private List<PoolItem> _poolObjectsList;
    

    private Dictionary<Type,List<MonoBehaviour>> _dictPoolObjects;
    private Dictionary<Type, Transform> _dictionaryRootTransforms;

    private void Awake()
    {
        Instance = this;
        _dictPoolObjects = new Dictionary<Type, List<MonoBehaviour>>();
        _dictionaryRootTransforms = new Dictionary<Type, Transform>();

        foreach (var item in _poolObjectsList)
        {
            var poolList = new List<MonoBehaviour>();
            var rootTransform = GetRootByType(item.ObjectToPool.GetType());
            for (int i = 0; i < item.StartCapacity; i++)
            {
                var monoBehaviour = Instantiate(item.ObjectToPool,rootTransform,true);
                monoBehaviour.gameObject.SetActive(false);
                poolList.Add(monoBehaviour);
            }
            _dictPoolObjects.Add(item.ObjectToPool.GetType(), poolList);
        }
    }

    //TODO сделать нормально
    private Transform GetRootByType(Type type)
    {
        Transform rootTransform;
        if (!_dictionaryRootTransforms.TryGetValue(type, out rootTransform))
        {
            var go = new GameObject(type.ToString());
            go.transform.SetParent(transform);
            rootTransform = go.transform;
            Debug.Log(type);
        }

        return rootTransform;
    }

    public T GetObjectOfType<T>() where T : MonoBehaviour
    {
        var list = new List<MonoBehaviour>();
        if (_dictPoolObjects.TryGetValue(typeof(T), out list))
        {
            MonoBehaviour returnedMonoBehaviour = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].isActiveAndEnabled)
                {
                    returnedMonoBehaviour = list[i];
                    break;
                }
            }
            
            if (returnedMonoBehaviour == null)
            {
                var parentTransform = GetRootByType(typeof(T));
                returnedMonoBehaviour = Instantiate(list[0],parentTransform,true);
                list.Add(returnedMonoBehaviour);
            }

            ((IPoolObject)returnedMonoBehaviour).GetFromPool();

            return (T)returnedMonoBehaviour;
        }
        else
        {
            return null;
        }
    }

    #region SerializableClass
    [System.Serializable]
    private class PoolItem
    {
        public string Name;
        [Header("Only IPoolObject")]
        public MonoBehaviour ObjectToPool;
        public int StartCapacity = 10;
    }
    #endregion

}
