using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionItem : MonoBehaviour
{
    public Text dateText;
    public Text amountText;
    public Text remarkText;
    public GameObject mask;
    public void SetData(Transaction transaction,bool isMask)
    {
        dateText.text = $"{transaction.date}";
        amountText.text = $"{transaction.amount}";
        remarkText.text = $"{transaction.remark}";
        mask.SetActive(isMask);
    }
}
