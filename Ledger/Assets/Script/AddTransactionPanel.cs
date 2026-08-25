using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTransactionPanel : MonoBehaviour
{
    [Header("Button")]
    public Button cancelButton;
    public Button confirmButton;
    [Header("InputField")]
    public InputField amountInputField;
    public InputField remarkInputField;

    void Start()
    {
        
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
            string day = month + $"{now.Day}";
            Transaction newTransaction = new Transaction(day, amount, remarkInputField.text);

            MonthLedger currentMonthLedger = AppInit.allLedger.monthLedgerList.Find(monthLedger => monthLedger.month == month);
            if (currentMonthLedger == null)
            {
                currentMonthLedger = new MonthLedger(month,new List<Transaction>());
                AppInit.allLedger.monthLedgerList.Add(currentMonthLedger);
            }
            currentMonthLedger.transactionList.Add(newTransaction);
            
            LedgerSaveManager.SaveAllLedger(AppInit.allLedger);
            UIManager.instance.RefreshMonthLedgerUI(currentMonthLedger.transactionList);
        }
        else
        {
            Debug.Log("请输入正确金额！");
            amountInputField.text = string.Empty;
        }
    }
    #endregion
}
