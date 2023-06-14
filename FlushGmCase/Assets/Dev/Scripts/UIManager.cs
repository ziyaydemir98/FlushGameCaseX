using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerMoney;
    private void OnEnable()
    {
        GameManager.Instance.OnMoneyChange.AddListener(SetMoneyText);
    }
    void SetMoneyText()
    {
        playerMoney.text = GameManager.Instance.PlayerMoney.ToString();
    }
}
