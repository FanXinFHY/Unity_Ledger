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
    public TextMeshProUGUI plannedSpendText;
    public TextMeshProUGUI availableText;
    public TextMeshProUGUI dateText;
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

        dateText.text = $"{monthLedger.month}";
        spentText.text = $"<size=60><cspace=-20px></cspace></size>{spent}<size=60><cspace=-20px></cspace></size>";
        plannedSpendText.text = $"预计花费:{monthLedger.plannedSpend}";
        float availableAmount = (monthLedger.plannedSpend - spent) > 0 ? (monthLedger.plannedSpend - spent) : 0;
        availableText.text = $"剩余可用:{availableAmount}";
    }

    public void AddTransaction()
    {

    }

}
