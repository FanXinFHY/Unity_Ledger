using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddIncomeBillPanel : MonoBehaviour
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
        if (float.TryParse(amountString, out float amount))
        {
            //创建一个新的Bill
            Bill newBill = new Bill(DataManager.instance.GetBillID(),E_BillType.income,DataManager.day, amount, remarkInputField.text);

            MonthLedger currentMonthLedger = DataManager.instance.FindMonthLedger(DataManager.month,true);
            if (currentMonthLedger == null)
            {
                currentMonthLedger = new MonthLedger(DataManager.month, 0f, new List<Bill>());
                DataManager.instance.SetCurrentMonthLedger(currentMonthLedger);
                DataManager.instance.AddNewMonthLedger(currentMonthLedger);
            }
            DataManager.instance.AddNewBill(newBill);

            DataManager.SaveAllLedger();
            UIManager.instance.RefreshBillListContent();

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
