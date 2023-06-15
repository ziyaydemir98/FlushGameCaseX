using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    #region Variables
    [SerializeField] Transform container;
    [SerializeField] TextMeshProUGUI playerMoney;
    [SerializeField] TextMeshProUGUI totalSales;
    [SerializeField] Element elementPrefab;
    [SerializeField] List<ContainerElements> elementsList;
    [SerializeField] List<TextMeshProUGUI> gemCountListText;
    [SerializeField] CanvasRenderer salesListPanel;
    [SerializeField] Button panelButton;
    #endregion

    private void OnEnable()
    {
        InitializeElements();
        GameManager.Instance.OnMoneyChange.AddListener(SetMoneyText);
        panelButton.onClick.AddListener(PanelButton);
        LoadListData();
    }
    private void OnDisable()
    {
        SaveListData();
    }
    #region Functions
    private void SetMoneyText()
    {
        playerMoney.text = GameManager.Instance.PlayerMoney.ToString();
    }
    /// <summary>
    /// Hangi gemden kac adet satildiginin kaydini burada olusturup TextBox'lara atiyorum.
    /// </summary>
    /// <param name="name"></param>
    public void SetGemCountText(string name)
    {
        PlayerPrefs.SetInt("TotalSalesInt", PlayerPrefs.GetInt("TotalSalesInt",0) + 1);
        PlayerPrefs.SetInt($"{name}", PlayerPrefs.GetInt($"{name}",0) + 1);
        foreach (var item in gemCountListText)
        {
            if (item.name.Contains(name))
            {
                item.text = PlayerPrefs.GetInt($"{name}", 0).ToString();
            }
        }
        totalSales.text = PlayerPrefs.GetInt("TotalSalesInt", 0).ToString();
    }
    /// <summary>
    /// Kayitli verileri buradan cekip Sahne basladiginda Textbox'lara atiyorum.
    /// </summary>
    private void SaveListData()
    {
        foreach (var item in gemCountListText)
        {
            PlayerPrefs.SetString($"{item.name}", item.text);
        }
        PlayerPrefs.SetString("TotalSalesString", totalSales.text);
    }
    private void LoadListData()
    {
        foreach (var item in gemCountListText)
        {
            item.text = PlayerPrefs.GetString($"{item.name}", item.text);
        }
        totalSales.text = PlayerPrefs.GetString("TotalSalesString",totalSales.text);
    }
    public void PanelButton()
    {
        if (salesListPanel.gameObject.activeSelf)
        {
            salesListPanel.gameObject.SetActive(false);
        }
        else
        {
            salesListPanel.gameObject.SetActive(true);
        }
    }
    private void InitializeElements()
    {
        foreach (var elements in elementsList)
        {
            Element element = Instantiate(elementPrefab, container);
            element.TextMeshProUGUI.name = elements.ElementName;
            element.Icon.sprite = elements.IconElement;
            gemCountListText.Add(element.TextMeshProUGUI);
        }
    }
    
    #endregion

}
[System.Serializable]
public class ContainerElements
{
    public string ElementName;
    public Sprite IconElement; // IN LOBBY
}
