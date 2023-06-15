using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    #region Variables
    [HideInInspector] public UnityEvent OnMoneyChange = new();

    private int playerMoney;
    public int PlayerMoney
    {
        get
        {
            return playerMoney;
        }
        set
        {
            playerMoney = value;
            OnMoneyChange.Invoke();
        }
    }
    #endregion

    private void Awake()
    {
        LoadData();
    }
    private void OnDisable()
    {
        SaveData();
    }
    #region Functions
    private void LoadData()
    {
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);
    }
    #endregion

}
