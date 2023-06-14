using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance = null;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PoolManager").AddComponent<PoolManager>();
            }
            return instance;
        }
    }
    private Queue<GemManager> poolOfGem;
    [SerializeField] private GemManager gemPrefab;
    [SerializeField] private int poolSize;
    
    private void OnEnable()
    {
        instance = this;
        InitializedPool();
    }

    public GemManager CallGem()
    {
        GemManager gem = poolOfGem.Dequeue();
        gem.gameObject.SetActive(true);
        if (poolOfGem.Count < 100)
        {
            SizeUp();
        }
        return gem;

    }
    public void SendGem(GemManager gem)
    {
        poolOfGem.Enqueue(gem);
        gem.transform.SetParent(transform);
        gem.transform.localPosition = Vector3.zero;
        gem.gameObject.SetActive(false);
    }
    private void InitializedPool()
    {
        poolOfGem = new Queue<GemManager>();
        for (int i = 0; i < poolSize; i++)
        {
            GemManager gem = Instantiate(gemPrefab, transform);
            gem.gameObject.SetActive(false);
            poolOfGem.Enqueue(gem);
        }
    }
    private void SizeUp()
    {
        for (int i = 0; i < 100; i++)
        {
            GemManager gem = Instantiate(gemPrefab, transform);
            gem.gameObject.SetActive(false);
            poolOfGem.Enqueue(gem);
        }
    }
}
