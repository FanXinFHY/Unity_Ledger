using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditTotalDepositPanel : MonoBehaviour
{
    [Header("Button")]
    public Button cancelButton;
    public Button confirmButton;
    [Header("InputField")]
    public TMP_InputField onlineFundsInputField;
    public TMP_InputField cashInputField;

    void Start()
    {
        cancelButton.onClick.AddListener(CancelButton);
        confirmButton.onClick.AddListener(ConfirmButton);
    }
    #region 按钮点击函数
    public void CancelButton()
    {
        onlineFundsInputField.text = string.Empty;
        cashInputField.text = string.Empty;
        gameObject.SetActive(false);
    }
    public void ConfirmButton()
    {
        string onlineFundsString = onlineFundsInputField.text;
        string cashString = cashInputField.text;
        bool isUpdateOnlineFunds = false;
        bool isUpdateCash = false;

        //尝试将输入解析为浮点数，成功即标记更新，失败则清空输入并提醒
        if (float.TryParse(onlineFundsString, out float onlineFunds) )
        {
            isUpdateOnlineFunds = true;
        }else
        {
            Debug.Log("请输入正确线上金额！");
            onlineFundsInputField.text = string.Empty;
        }
        if (float.TryParse(cashString, out float cash))
        {
            isUpdateCash = true;
        }
        else
        {
            Debug.Log("请输入正确现金金额！");
            cashInputField.text = string.Empty;
        }
        if(isUpdateOnlineFunds && isUpdateCash)
        {
            DataManager.instance.setTotalDeposit(onlineFunds + cash);
            DataManager.instance.setOnlineFunds(onlineFunds);
            DataManager.instance.setCash(cash);
            DataManager.SaveAllLedger();
            UIManager.instance.RefreshTotalDeposit();

            onlineFundsInputField.text = string.Empty;
            cashInputField.text = string.Empty;

            gameObject.SetActive(false);
        }
    }
    #endregion
}
