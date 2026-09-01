using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Panel")]
    public GameObject addIncomeBillPanel;
    public GameObject addExpensesBillPanel;
    public GameObject selectMonthPanel;
    public GameObject operationPanel;
    public GameObject billListEmptyPanel;
    [Header("TextMeshProUGUI")]
    public TextMeshProUGUI expenseText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI plannedExpensesText;
    public TextMeshProUGUI availableText;
    public TextMeshProUGUI dateText;
    [Header("Button")]
    public Button selectMonthButton;
    public Button addIncomeBillButton;
    public Button addExpensesBillButton;
    public Button deleteConfirmButton;
    public Button deleteCancelButton;
    [Header("Content")]
    public GameObject billListContent;
    public GameObject monthButtonListContent;
    [Header("Prefab")]
    public GameObject billPrefab;
    public GameObject monthButtonPrefab;
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

    public void RefreshBillListContent()
    {
        MonthLedger monthLedger = DataManager.instance.GetCurrentMonthLedger();

        //不管目标月是否有订单记录都清除容器
        for (int i = billListContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(billListContent.transform.GetChild(i).gameObject);
        }
        //如果目标月没有记录，则显示空面板并且更新日期按钮显示当月日期
        if (monthLedger.billList.Count == 0)
        {
            selectMonthButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = DataManager.month;
            billListEmptyPanel.gameObject.SetActive(true);
            Debug.Log("月账单刷新成功！");
            return;
        }
        //如果当月有记录，则隐藏空面板并更新相应UI和容器
        bool isMask = false;
        float income = 0f;
        float expenses = 0f;
        billListEmptyPanel.gameObject.SetActive(false);
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

        Debug.Log("月账单刷新成功！");
    }

    public void RefreshMonthButtonListContent()
    {
        for (int i = monthButtonListContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(monthButtonListContent.transform.GetChild(i).gameObject);
        }

        int count = DataManager.instance.GetAllLedger().monthLedgerList.Count;
        if(DataManager.instance.GetAllLedger().monthLedgerList.Count == 0|| DataManager.instance.GetAllLedger().monthLedgerList[count-1].month != DataManager.month)
        {
            GameObject transactionItem = Instantiate(monthButtonPrefab, monthButtonListContent.transform);
            transactionItem.GetComponent<MonthButtonItem>().SetData(DataManager.month);
        }
        for (int i = count - 1; i >= 0 ;i--)
        {
            GameObject transactionItem = Instantiate(monthButtonPrefab, monthButtonListContent.transform);
            transactionItem.GetComponent<MonthButtonItem>().SetData(DataManager.instance.GetAllLedger().monthLedgerList[i].month);
        }
        Debug.Log("所有月订单刷新成功！");
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
    #region 按钮点击函数

    #endregion

}
