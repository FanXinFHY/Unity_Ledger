using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditBillPanel : MonoBehaviour
{
    private Bill currentBill;
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
    private void OnEnable()
    {
        currentBill = OperationPanel.instance.currentBill;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDate(Bill currentBill)
    {
        this.currentBill = currentBill;
        amountInputField.text = currentBill.amount;
        remarkInputField.text = currentBill.remark;
    }

    public void ResetDate()
    {
        currentBill = null;
        amountInputField.text = string.Empty;
        remarkInputField.text = string.Empty;
    }
    #region 按钮点击函数
    public void CancelButton()
    {
        ResetDate();
        gameObject.SetActive(false);
    }
    public void ConfirmButton()
    {
        string amountString = amountInputField.text;
        string remarkString = remarkInputField.text;
        //尝试将输入解析为浮点数，成功即创建新记账条，失败则清空输入并提醒
        if (float.TryParse(amountString, out float amount))
        {
            currentBill.amount = amount;
            currentBill.remark = remarkString;

            LedgerSaveManager.SaveAllLedger(AppInit.allLedger);
            UIManager.instance.RefreshMonthLedgerUI(currentMonthLedger);

            CancelButton();
        }
        else
        {
            Debug.Log("请输入正确金额！");
            amountInputField.text = currentBill.amount; 
        }
    }
    #endregion
}
