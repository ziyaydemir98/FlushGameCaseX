using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public bool Collectable;
    public GameObject CellBox;
    private BoxCollider _collider;
    private GemManager _currentGem;
    public void InstantCell()
    {
        _collider = GetComponent<BoxCollider>();
        GameObject box =  Instantiate(CellBox, this.gameObject.transform);
        box.transform.SetParent(transform);
        _collider.size = new Vector3(box.transform.localScale.x,5,box.transform.localScale.z);
        
    }
    public void InstantGem()
    {
        _currentGem = PoolManager.Instance.CallGem();
        _currentGem.transform.SetParent(transform);
        _currentGem.SetupGem();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Collectable)
        {
            PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();
            playerManager.BagSet(_currentGem);
            _currentGem = null;
            InstantGem();
            Collectable = false;
        }
    }
}
