using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject addTransactionPanel;
    public GameObject selectDatePanel;
    public TextMeshProUGUI spentText;
    public TextMeshProUGUI availableText;
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

    public void RefreshMonthLedgerUI(List<Transaction> transactionList)
    {
        for(int i = transactionListContent.transform.childCount - 1;i >= 0;i--)
        {
            Destroy(transactionListContent.transform.GetChild(i).gameObject);
        }
        foreach(Transaction transaction in transactionList)
        {
            GameObject transactionItem = Instantiate(transactionPrefab, transactionListContent.transform);
            transactionItem.GetComponent<TransactionItem>().SetData(transaction);
        }
    }

    public void AddTransaction()
    {

    }

}
