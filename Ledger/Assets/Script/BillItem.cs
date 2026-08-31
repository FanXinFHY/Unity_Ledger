using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BillItem : MonoBehaviour
{
    private Bill bill;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI remarkText;
    public Button operationButton;
    private RectTransform rectTransform;
    [Header("GameObject")]
    public GameObject mask;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        operationButton.onClick.AddListener(() => OperationButton());
    }
    public void SetData(Bill bill,bool isMask)
    {
        this.bill = bill;
        dateText.text = $"{bill.date}";
        amountText.text = $"{bill.amount}";
        if(bill.e_BillType == E_BillType.income)
        {
            amountText.color = Color.green;
        }
        remarkText.text = bill.remark;
        mask.SetActive(isMask);
    }

    public void OperationButton()
    {
        DataManager.instance.SetCurrentBill(bill);
        UIManager.instance.SetOperationPanelPosition(operationButton.transform.position, JudgeUpOrDown());
    }
    public bool JudgeUpOrDown()
    {
        Vector3 myWorldPosition = rectTransform.TransformPoint(rectTransform.rect.center);
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(null, myWorldPosition);

        bool upOrDown = screenPosition.y <= Screen.height / 2f; ;

        if (upOrDown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
