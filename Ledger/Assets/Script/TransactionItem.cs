using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionItem : MonoBehaviour
{
    public Text dateText;
    public Text amountText;
    public Text remarkText;
    public void SetData(Transaction transaction)
    {
        dateText.text = $"{transaction.date}";
        amountText.text = $"{transaction.amount}";
        remarkText.text = $"{transaction.remark}";
    }
}
