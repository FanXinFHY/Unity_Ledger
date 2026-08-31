using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Panel")]
    public GameObject addIncomeBillPanel;
    public GameObject addExpensesBillPanel;
    public GameObject selectDatePanel;
    public GameObject operationPanel;
    [Header("TextMeshProUGUI")]
    public TextMeshProUGUI expenseText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI plannedExpensesText;
    public TextMeshProUGUI availableText;
    public TextMeshProUGUI dateText;
    [Header("Button")]
    public Button addIncomeBillButton;
    public Button addExpensesBillButton;
    [Header("Content")]
    public GameObject billListContent;
    [Header("Prefab")]
    public GameObject billPrefab;
    [Header("GameObject")]
    public GameObject operationContent_Up;
    public GameObject operationContent_Down;

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
        float income = 0f;
        float expenses = 0f;
        for (int i = billListContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(billListContent.transform.GetChild(i).gameObject);
        }
        foreach (Bill bill in monthLedger.billList)
        {
            GameObject transactionItem = Instantiate(billPrefab, billListContent.transform);
            transactionItem.GetComponent<BillItem>().SetData(bill, isMask);
            isMask = !isMask;
            if(bill.e_BillType == E_BillType.expenses)
            {
                expenses += bill.amount;
            }
            else
            {
                income += bill.amount;
            }
        }

        dateText.text = $"{monthLedger.month}";
        expenseText.text = $"<size=60><cspace=-20px></cspace></size>{expenses}<size=60><cspace=-20px></cspace></size>";
        incomeText.text = $"<size=60><cspace=-20px></cspace></size>{income}<size=60><cspace=-20px></cspace></size>";
        plannedExpensesText.text = $"预计花费:{monthLedger.plannedExpenses}";
        float availableAmount = (monthLedger.plannedExpenses - expenses) > 0 ? (monthLedger.plannedExpenses - expenses) : 0;
        availableText.text = $"剩余可用:{availableAmount}";
    }

    public void SetOperationPanelPosition(Vector3 originalPosition,bool upOrDown)
    {
        Vector3 newPosition = operationPanel.transform.position;
        newPosition.x = originalPosition.x;
        newPosition.y = originalPosition.y;
        operationPanel.transform.position = newPosition;
        if(upOrDown)
        {
            operationContent_Up.SetActive(true);
            operationContent_Down.SetActive(false);
        }else
        {
            operationContent_Up.SetActive(false);
            operationContent_Down.SetActive(true);
        }
        operationPanel.SetActive(true);
    }
}
