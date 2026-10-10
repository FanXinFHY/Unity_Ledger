using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditBillPanel : MonoBehaviour
{
    [Header("Button")]
    public Button cancelButton;
    public Button confirmButton;
    [Header("InputField")]
    public TMP_InputField amountInputField;
    public TMP_InputField remarkInputField;
    [Header("TextMeshProUGUI")]
    public TextMeshProUGUI amountPlaceholderText;
    public TextMeshProUGUI remarkPlaceholderText;
    [Header("Other")]
    public CategoryTagList categoryTagList;
    private E_BillCategory e_BillCategory;

    void Start()
    {
        cancelButton.onClick.AddListener(CancelButton);
        confirmButton.onClick.AddListener(ConfirmButton);
    }
    private void OnEnable()
    {
        SetDate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDate()
    {
        Bill currentBill = DataManager.instance.FindBill();
        amountPlaceholderText.text = currentBill.amount.ToString();
        remarkPlaceholderText.text = currentBill.remark;
        e_BillCategory = currentBill.e_BillCategory;
        categoryTagList.SelecteBillCategory(e_BillCategory);
    }

    public void ResetDate()
    {
        amountInputField.text = string.Empty;
        remarkInputField.text = string.Empty;
        DataManager.instance.SetCurrentBillID(-1);
        categoryTagList.Reset();
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
        bool isChange = false;
        DataManager.instance.computeTotalDeposit(DataManager.instance.FindBill(), true);
        if(amountString != string.Empty)
        {
            isChange = true;
            DataManager.instance.FindBill().amount = float.Parse(amountString);
        }
        if(remarkString != string.Empty)
        {
            DataManager.instance.FindBill().remark = remarkString;
            isChange = true;
        }
        if(e_BillCategory != categoryTagList.GetBillCategory())
        {
            DataManager.instance.FindBill().e_BillCategory = categoryTagList.GetBillCategory();
            isChange = true;
        }
        DataManager.instance.computeTotalDeposit(DataManager.instance.FindBill(), false);
        if (isChange)
        {
            DataManager.SaveAllLedger();
            UIManager.instance.RefreshBillListContent();
        }
        CancelButton();
    }
    #endregion
}
