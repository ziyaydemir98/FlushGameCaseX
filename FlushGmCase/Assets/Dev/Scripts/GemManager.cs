using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using DG.Tweening;

public class GemManager : MonoBehaviour
{
    public enum GemsOfPool { FieldPool, BagPool }
    public GemsOfPool GemsOfPoolVariable;

    [SerializeField] List<GemTypes> gemTypes;

    [NonSerialized] public bool Growded;
    [NonSerialized] public float Price;
    private float _growTiming;

    int _randomValue;
    private void OnEnable()
    {

        SetupGem();
    }
    private void SetupGem()
    {
        ResetGem();
        _randomValue = UnityEngine.Random.Range(0, gemTypes.Count);
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer.material = gemTypes[_randomValue].MaterialOfGem;
        meshFilter.mesh = gemTypes[_randomValue].MeshOfGem;
        gameObject.name = gemTypes[_randomValue].GemName;
        _growTiming = gemTypes[_randomValue].GrowTime;
        Price = gemTypes[_randomValue].BeginPrice;
        switch (GemsOfPoolVariable)
        {
            case GemsOfPool.FieldPool:
                StartCoroutine(GemGrow(_randomValue));
                break;
        }
    }
    private void ResetGem()
    {
        switch (GemsOfPoolVariable)
        {
            case GemsOfPool.FieldPool:
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.zero;
                Growded = false;
                break;
            case GemsOfPool.BagPool:
                transform.localPosition = new Vector3(0, 1, 0);
                transform.localScale = Vector3.one;
                Growded = true;
                break;
        }
        
    }
    #region Grow Coroutine Functions
    private IEnumerator GemGrow(int count)
    {
        StartCoroutine(FirstPhase(count));
        yield return new WaitForSeconds(_growTiming);
        Debug.Log("FAZ 1 VE 2 TAMAMLANDI");
        StopCoroutine(GemGrow(count));
    }
    private IEnumerator FirstPhase(int count)
    {
        transform.DOScale(gemTypes[count].GrowdedGemScale, _growTiming * 0.25f);
        transform.DOMoveY(gemTypes[count].GrowdedGemScale.y, _growTiming * 0.25f);
        yield return new WaitForSeconds(_growTiming * 0.25f);
        Growded = true;
        Debug.Log("FAZ 1 TAMAMLANDI");
        StartCoroutine(SecondPhase(count));
        StopCoroutine(FirstPhase(count));
    }
    private IEnumerator SecondPhase(int count)
    {
        transform.DOScale(gemTypes[count].EndGemScale, _growTiming * 0.75f);
        transform.DOMoveY(gemTypes[count].EndGemScale.y, _growTiming * 0.75f);
        yield return new WaitForSeconds(_growTiming * 0.75f);
        Debug.Log("FAZ 2 TAMAMLANDI");
        StopCoroutine(SecondPhase(count));
    }
    #endregion

}
