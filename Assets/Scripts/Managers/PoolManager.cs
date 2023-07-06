using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들 보관할 변수
    public GameObject[] prefab;
    
    // 풀 담당을 하는 리스트들
    private List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefab.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
            
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])   
        {
            if (!item.activeSelf)
            {
                select = item;
                select.gameObject.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefab[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
    
    
}
