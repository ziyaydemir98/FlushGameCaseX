using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    #region Variables
    /// <summary>
    /// OBJECT POOL DESIGN PATTERN
    /// </summary>
    private Queue<GemManager> poolOfGem;
    [SerializeField] private GemManager gemPrefab;
    [SerializeField] private int poolSize;
    #endregion


    private void OnEnable()
    {
        InitializedPool();
    }
    #region Functions
    /// <summary>
    /// Pooldan obje cekmek icin gerekli fonksiyon.
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Poola obje gondermek icin gerekli fonksiyon
    /// </summary>
    /// <param name="gem"></param>
    public void SendGem(GemManager gem)
    {
        poolOfGem.Enqueue(gem);
        gem.transform.SetParent(transform);
        gem.transform.localPosition = Vector3.zero;
        gem.gameObject.SetActive(false);
    }
    /// <summary>
    /// Pool objectleri olusturmak icin gerekli fonksiyon
    /// </summary>
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
    /// <summary>
    /// Pool buyuklugu dustugunde daha fazla obje olusturmak icin gerekli fonksiyon.
    /// </summary>
    private void SizeUp()
    {
        for (int i = 0; i < 100; i++)
        {
            GemManager gem = Instantiate(gemPrefab, transform);
            gem.gameObject.SetActive(false);
            poolOfGem.Enqueue(gem);
        }
    }
    #endregion

}
