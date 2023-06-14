using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using DG.Tweening;

public class GemManager : MonoBehaviour
{
    #region Variables
    [SerializeField] List<GemTypes> gemTypes;
    public bool Growded;
    public float Price;
    private float _growTiming;
    private CellManager _cellManager;
    int _randomValue;
    #endregion

    #region Gem Functions
    public void SetupGem()
    {
        ResetGem();
        _cellManager = transform.GetComponentInParent<CellManager>();
        _randomValue = UnityEngine.Random.Range(0, gemTypes.Count);
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer.material = gemTypes[_randomValue].MaterialOfGem;
        meshFilter.mesh = gemTypes[_randomValue].MeshOfGem;
        gameObject.name = gemTypes[_randomValue].GemName;
        _growTiming = gemTypes[_randomValue].GrowTime;
        Price = gemTypes[_randomValue].BeginPrice;

        StartCoroutine(GrowGem(gemTypes[_randomValue].TargetScale,_growTiming));
    }
    private void ResetGem()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.zero;
        Growded = false;
    }
    #endregion

    #region Grow Coroutine Functions
    private IEnumerator GrowGem(Vector3 target, float time)
    {
        transform.DOScale(target, time);
        transform.DOMoveY(target.y, time);
        yield return new WaitForSeconds(time * 0.25f);
        Growded = true;
        _cellManager.Collectable = Growded;
        yield return new WaitForSeconds(time * 0.75f);
        StopCoroutine(GrowGem(target, time));
    }
    public void StopGrow()
    {
        if (Growded)
        {
            transform.DOKill();
        }
        else return;
    }
    #endregion

}
