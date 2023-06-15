using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{
    #region Variables
    PlayerManager _playerManager;
    public List<GemManager> SellGem = new List<GemManager>();
    int _count;
    #endregion

    #region Functions
    /// <summary>
    /// Player objesi satis bolgesine girdiginde cantasindaki listede ne kadar eleman varsa satis alaninin kendi listesine de ekliyorum.
    /// Olusan listenin buyuklugunce bir sayisal deger tanimliyorum.
    /// Sayisal deger buyuklugunce Satis rutini baslatiyorum.
    /// </summary>
    /// <param name="other"></param>
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
    /// <summary>
    /// Player alandan ciktiginda alandaki listeyi ve sayisal degerleri sifirliyorum. Satis Rutinini durduruyorum
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SellGem.Clear();
            _count = -1;
            StopCoroutine(SellRotuine());
        }
    }
    /// <summary>
    /// Satis rutininde satilacak objenin fiyati hesaplaniyor.
    /// Player objesindeki cantadan obje siliniyor.
    /// Obje Pool'a gonderiliyor.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SellRotuine()
    {
        CalculatePrice();
        yield return new WaitForSeconds(1 * 0.10f);
        UIManager.Instance.SetGemCountText(_playerManager.BagList[_count].name);
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
    /// <summary>
    /// Urunun kac degere satilacagini burada hesapliyorum
    /// </summary>
    private void CalculatePrice()
    {
        int price = (int)((SellGem[_count].transform.localScale.x * 100) + SellGem[_count].Price);
        GameManager.Instance.PlayerMoney += price;
        Debug.Log(price);
    }
    #endregion

}
