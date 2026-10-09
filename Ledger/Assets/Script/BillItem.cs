using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BillItem : MonoBehaviour
{
    private Bill bill;
    public Image tagIcon;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI remarkText;
    public Button operationButton;
    private RectTransform rectTransform;
    [Header("TagIcon")]
    public Sprite otherIcon;
    public Sprite foodAndDrinkIcon;
    public Sprite accommodationIcon;
    public Sprite entertainmentIcon;
    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        operationButton.onClick.AddListener(() => OperationButton());
    }
    public void SetData(Bill bill)
    {
        this.bill = bill;
        dateText.text = $"{bill.date}";
        amountText.text = $"{bill.amount}";
        if(bill.e_BillType == E_BillType.income)
        {
            amountText.color = new Color32(0x89, 0xD9, 0x68, 0xFF); ;
        }
        switch (bill.e_BillCategory)
        {
            case E_BillCategory.other: 
                tagIcon.sprite = otherIcon; 
                break;
            case E_BillCategory.foodAndDrink:
                tagIcon.sprite = foodAndDrinkIcon;
                break;
            case E_BillCategory.accommodation:
                tagIcon.sprite= accommodationIcon;
                break;
            case E_BillCategory.entertainment:
                tagIcon.sprite = entertainmentIcon;
                break;
            default:break;
        }
        remarkText.text = bill.remark;
    }

    public void OperationButton()
    {
        DataManager.instance.SetCurrentBillID(bill.ID);
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
