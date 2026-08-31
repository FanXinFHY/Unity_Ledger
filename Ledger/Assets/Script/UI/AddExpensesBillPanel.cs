using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddExpensesBillPanel : MonoBehaviour
{
    [Header("Button")]
    public Button cancelButton;
    public Button confirmButton;
    [Header("InputField")]
    public TMP_InputField amountInputField;
    public TMP_InputField remarkInputField;

    void Start()
    {
        cancelButton.onClick.AddListener(CancelButton);
        confirmButton.onClick.AddListener(ConfirmButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region 按钮点击函数
    public void CancelButton()
    {
        amountInputField.text = string.Empty;
        remarkInputField.text = string.Empty;   
        gameObject.SetActive(false);
    }
    public void ConfirmButton()
    {
        string amountString = amountInputField.text;
        string remarkString = remarkInputField.text;
        //尝试将输入解析为浮点数，成功即创建新记账条，失败则清空输入并提醒
        if(float.TryParse(amountString, out float amount))
        {
            //创建一个新的Transaction
            DateTime now = DateTime.Now;
            string month = $"{now.Year}.{now.Month}";
            string day = month + $".{now.Day}\n{now.Hour}:{now.Minute}";
            Bill newTransaction = new Bill(E_BillType.expenses,day, amount, remarkInputField.text);

            MonthLedger currentMonthLedger = AppInit.allLedger.monthLedgerList.Find(monthLedger => monthLedger.month == month);
            if (currentMonthLedger == null)
            {
                currentMonthLedger = new MonthLedger(month,0f,new List<Bill>());
                AppInit.allLedger.monthLedgerList.Add(currentMonthLedger);
            }
            currentMonthLedger.billList.Add(newTransaction);
            
            LedgerSaveManager.SaveAllLedger(AppInit.allLedger);
            UIManager.instance.RefreshMonthLedgerUI(currentMonthLedger);

            amountInputField.text = string.Empty;
            remarkInputField.text = string.Empty;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("请输入正确金额！");
            amountInputField.text = string.Empty;
        }
    }
    #endregion
}
