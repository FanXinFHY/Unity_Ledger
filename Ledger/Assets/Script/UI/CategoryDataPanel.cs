using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CategoryDataPanel : MonoBehaviour
{
    [Header("Button")]
    public Button foodAndDrinkButton;
    public Button accommodationButton;
    public Button entertainmentButton;
    public Button otherButton;
    [Header("Text")]
    public TextMeshProUGUI percentText;
    public TextMeshProUGUI tipText;
    [Header("Other")]
    public Image barImage;
    private Tween numberTween;
    private float currentPercent;

    void Start()
    {
        foodAndDrinkButton.onClick.AddListener(() => SetBarTargetPercent(DataManager.instance.foodAndDrinkExpenses));
        accommodationButton.onClick.AddListener(() => SetBarTargetPercent(DataManager.instance.accommodationExpenses));
        entertainmentButton.onClick.AddListener(() => SetBarTargetPercent(DataManager.instance.entertainmentExpenses));
        otherButton.onClick.AddListener(() => SetBarTargetPercent(DataManager.instance.otherExpenses)); 
        foodAndDrinkButton.onClick.AddListener(() => SetTipText("吃喝", DataManager.instance.foodAndDrinkExpenses));
        accommodationButton.onClick.AddListener(() => SetTipText("住宿",DataManager.instance.accommodationExpenses));
        entertainmentButton.onClick.AddListener(() => SetTipText("娱乐",DataManager.instance.entertainmentExpenses));
        otherButton.onClick.AddListener(() => SetTipText("其它",DataManager.instance.otherExpenses));
        SetBarTargetPercent(DataManager.instance.foodAndDrinkExpenses);
        SetTipText("吃喝", DataManager.instance.foodAndDrinkExpenses);
    }

    private void OnEnable()
    {
        UpdateTip();
    }

    public void UpdateTip()
    {
        if(foodAndDrinkButton.transform.GetChild(0).gameObject.activeSelf == true)
        {
            SetBarTargetPercent(DataManager.instance.foodAndDrinkExpenses);
            SetTipText("吃喝", DataManager.instance.foodAndDrinkExpenses);
            return;
        }
        else if(accommodationButton.transform.GetChild(0).gameObject.activeSelf == true)
        {
            SetBarTargetPercent(DataManager.instance.accommodationExpenses);
            SetTipText("住宿", DataManager.instance.accommodationExpenses);
            return;
        }
        else if(entertainmentButton.transform.GetChild(0).gameObject.activeSelf == true)
        {
            SetBarTargetPercent(DataManager.instance.entertainmentExpenses);
            SetTipText("娱乐", DataManager.instance.entertainmentExpenses);
            return;
        }
        else if(otherButton.transform.GetChild(0).gameObject.activeSelf == true)
        {
            SetBarTargetPercent(DataManager.instance.otherExpenses);
            SetTipText("其它", DataManager.instance.otherExpenses);
            return;
        }
    }

    public void SetBarTargetPercent(float  categoryExpenses)
    {
        if (DataManager.instance.totalExpenses <= 0f)
        {
            numberTween?.Kill();
            currentPercent = 0f;
            percentText.text = "0.00%";
            barImage.fillAmount = 0f;
            return;
        }
        numberTween?.Kill();
        float targetPercent = Mathf.Clamp01(categoryExpenses / DataManager.instance.totalExpenses);
        numberTween = DOVirtual.Float(
            currentPercent,
            targetPercent,
            1f,
            x =>
            {
                currentPercent = x;
                percentText.text = $"{(x * 100f):F2}%";
                barImage.fillAmount = x;
            })
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                currentPercent = targetPercent;
                percentText.text = $"{(targetPercent * 100):F2}%";
                barImage.fillAmount = targetPercent;
            });
    }
    public void SetTipText(string categoryTag,float number)
    {
        tipText.text = $"<cspace=-10>本月{categoryTag}累计消费</cspace>\n<size=120>￥</size><size=150>{number:F2}</size>";
    }
}
