using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }

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
    private void Awake()
    {
        LoadData();
    }
    private void OnDisable()
    {
        SaveData();
    }
    private void LoadData()
    {
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);
    }
}
