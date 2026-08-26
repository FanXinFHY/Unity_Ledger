using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Panel")]
    public GameObject addTransactionPanel;
    public GameObject selectDatePanel;
    [Header("TextMeshProUGUI")]
    public TextMeshProUGUI spentText;
    public TextMeshProUGUI plannedSpend;
    [Header("Button")]
    public Button addTransactionButton;
    [Header("Content")]
    public GameObject transactionListContent;
    [Header("Prefab")]
    public GameObject transactionPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RefreshMonthLedgerUI(MonthLedger monthLedger)
    {
        bool isMask = false;
        float spent = 0f;
        for (int i = transactionListContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transactionListContent.transform.GetChild(i).gameObject);
        }
        foreach (Transaction transaction in monthLedger.transactionList)
        {
            GameObject transactionItem = Instantiate(transactionPrefab, transactionListContent.transform);
            transactionItem.GetComponent<TransactionItem>().SetData(transaction, isMask);
            isMask = !isMask;
            spent += transaction.amount;
        }

        spentText.text = $"<size=60><cspace=-20px>已消费</cspace></size>{spent}<size=60><cspace=-20px>元</cspace></size>";
        plannedSpend.text = $"预计花费:{monthLedger.plannedSpend}";
    }

    public void AddTransaction()
    {

    }

}
