using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Hucre icerisindeki gemlerin toplanabilir olup olmadigini bu bool degerden guncelliyorum.
    /// Hucrenin gerekli Componentlerini tanimliyorum.
    /// CellBox Hucrenin icerisindeki Gorsel bir taban
    /// </summary>
    [NonSerialized] public bool Collectable;
    public GameObject CellBox;
    private BoxCollider _collider;
    private GemManager _currentGem;
    #endregion

    #region Functions
    public void InstantCell()
    {
        _collider = GetComponent<BoxCollider>();
        GameObject box = Instantiate(CellBox, this.gameObject.transform);
        box.transform.SetParent(transform);
        _collider.size = new Vector3(box.transform.localScale.x, 5, box.transform.localScale.z);

    }
    /// <summary>
    /// Gem objesi icerisindeki gerekli fonksiyonlari Gemi pool'dan cektikten sonra gerceklestiriyorum.
    /// </summary>
    public void InstantGem()
    {
        _currentGem = PoolManager.Instance.CallGem();
        _currentGem.transform.SetParent(transform);
        _currentGem.SetupGem();
    }
    /// <summary>
    /// Hucre icerisine Player geldiginde PlayerManager tanimliyorum.
    /// Hucredeki objeyi Player icerisindeki bagList'e eklemek icin bir fonksiyon cagiriyorum.
    /// Toplanabilir ozelligi sifirliyorum.
    /// </summary>
    /// <param name="other"></param>
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
    #endregion

}
