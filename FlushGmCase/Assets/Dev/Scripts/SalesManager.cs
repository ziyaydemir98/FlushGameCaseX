using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{
    #region Variables
    [SerializeField] float minSalePrice;
    PlayerManager _playerManager;
    public List<GemManager> SellGem = new List<GemManager>();
    int _count;
    #endregion

    #region Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerManager = other.gameObject.GetComponent<PlayerManager>();
            foreach (var gem in _playerManager.BagList)
            {
                SellGem.Add(gem);
            }
            _count = SellGem.Count-1;
            if (_count >= 0)
            {
                StartCoroutine(SellRotuine());

            }
            else
            {
                StopCoroutine(SellRotuine());
                return;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SellGem.Clear();
            _count = -1;
            StopCoroutine(SellRotuine());
        }
    }
    private IEnumerator SellRotuine()
    {
        CalculatePrice();
        yield return new WaitForSeconds(1 * 0.10f);
        _playerManager.BagList.Remove(_playerManager.BagList[_count]);
        PoolManager.Instance.SendGem(SellGem[_count]);
        _count--;
        if (_count < 0)
        {
            StopCoroutine(SellRotuine());
        }
        else
        {
            StartCoroutine(SellRotuine());
        }
        
    }
    private void CalculatePrice()
    {
        int price = (int)((SellGem[_count].transform.localScale.x * 100) + minSalePrice);
        GameManager.Instance.PlayerMoney += price;
        Debug.Log(price);
    }
    #endregion

}
