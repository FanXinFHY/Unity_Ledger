using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryDataPanel : MonoBehaviour
{
    [Header("Button")]
    public Button currentBillTypeButton;
    public Button currentExpensesButton;
    public Button currentIncomeButton;

    public Button expensesTypeButton;
    public Button incomeTypeButton;

    public Button foodAndDrinkExpensesButton;
    public Button accommodationExpensesButton;
    public Button entertainmentExpensesButton;
    public Button healthcareExpensesButton;
    public Button otherExpensesButton;

    public Button salaryIncomeButton;
    public Button sideJobIncomeButton;
    public Button transferIncomeButton;
    public Button envelopeIncomeButton;
    public Button otherIncomeButton;

    [Header("LuGameObject")]
    public GameObject currentLuGameObject;
    public GameObject foodAndDrinkLu;
    public GameObject accommodationLu;
    public GameObject entertainmentLu;
    public GameObject healthcareLu;
    public GameObject salaryLu;
    public GameObject sideJobLu;
    public GameObject transferLu;
    public GameObject envelopeLu;
    public GameObject otherLu;

    [Header("Text")]
    public TextMeshProUGUI percentText;
    public TextMeshProUGUI tipText;
    [Header("Other")]
    public Image barImage;
    private Tween numberTween;
    private float currentPercent;

    private void Awake()
    {
        currentBillTypeButton = expensesTypeButton;
        currentExpensesButton = foodAndDrinkExpensesButton;
        currentIncomeButton = salaryIncomeButton;
        currentLuGameObject = foodAndDrinkLu;
    }
    void Start()
    {
        expensesTypeButton.onClick.AddListener(() => SetCurrentBillTypeButton(E_BillType.expenses, expensesTypeButton));
        incomeTypeButton.onClick.AddListener(() => SetCurrentBillTypeButton(E_BillType.income, incomeTypeButton));
        expensesTypeButton.onClick.AddListener(() => UpdateTip());
        incomeTypeButton.onClick.AddListener(() => UpdateTip());

        foodAndDrinkExpensesButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.expenses,foodAndDrinkExpensesButton));
        accommodationExpensesButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.expenses, accommodationExpensesButton));
        entertainmentExpensesButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.expenses, entertainmentExpensesButton));
        healthcareExpensesButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.expenses, healthcareExpensesButton));
        otherExpensesButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.expenses, otherExpensesButton));

        salaryIncomeButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.income, salaryIncomeButton));
        sideJobIncomeButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.income, sideJobIncomeButton));
        transferIncomeButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.income, transferIncomeButton));
        envelopeIncomeButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.income, envelopeIncomeButton));
        otherIncomeButton.onClick.AddListener(() => SetCurrentCategoryButton(E_BillType.income, otherIncomeButton));

        SetViewData(E_BillType.expenses, DataManager.instance.foodAndDrinkExpenses, "吃喝");

    }

    private void OnEnable()
    {
        UpdateTip();
    }

    public void SetCurrentCategoryButton(E_BillType e_BillType, Button button)
    {
        if (e_BillType == E_BillType.expenses)
        {
            currentExpensesButton = button;
        }
        else
        {
            currentIncomeButton = button;
        }
        UpdateTip();
    }
    public void SetCurrentBillTypeButton(E_BillType e_BillType, Button button)
    {
        if (e_BillType == E_BillType.expenses)
        {
            currentBillTypeButton = button;
        }
        else
        {
            currentBillTypeButton = button;
        }
        UpdateTip();
    }
    public void UpdateTip()
    {
        currentLuGameObject.SetActive(false);
        if (currentBillTypeButton == expensesTypeButton)
        {
            if (currentExpensesButton == foodAndDrinkExpensesButton)
            {
                currentLuGameObject = foodAndDrinkLu;
                SetViewData(E_BillType.expenses, DataManager.instance.foodAndDrinkExpenses, "吃喝");
            }
            else if (currentExpensesButton == accommodationExpensesButton)
            {
                currentLuGameObject = accommodationLu;
                SetViewData(E_BillType.expenses, DataManager.instance.accommodationExpenses, "住宿");
            }
            else if (currentExpensesButton == entertainmentExpensesButton)
            {
                currentLuGameObject = entertainmentLu;
                SetViewData(E_BillType.expenses, DataManager.instance.entertainmentExpenses, "娱乐");
            }
            else if (currentExpensesButton == healthcareExpensesButton)
            {
                currentLuGameObject = healthcareLu;
                SetViewData(E_BillType.expenses, DataManager.instance.healthcareExpenses, "医疗");
            }
            else if (currentExpensesButton == otherExpensesButton)
            {
                currentLuGameObject = otherLu;
                SetViewData(E_BillType.expenses, DataManager.instance.otherExpenses, "其它");
            }
        }
        else
        {
            if (currentIncomeButton == salaryIncomeButton)
            {
                currentLuGameObject = salaryLu;
                SetViewData(E_BillType.income, DataManager.instance.salaryIncome, "工资");
            }
            else if (currentIncomeButton == sideJobIncomeButton)
            {
                currentLuGameObject = sideJobLu;
                SetViewData(E_BillType.income, DataManager.instance.sideJobIncome, "副业");
            }
            else if (currentIncomeButton == transferIncomeButton)
            {
                currentLuGameObject = transferLu;
                SetViewData(E_BillType.income, DataManager.instance.transferIncome, "转账");
            }
            else if (currentIncomeButton == envelopeIncomeButton)
            {
                currentLuGameObject = envelopeLu;
                SetViewData(E_BillType.income, DataManager.instance.envelopeIncome, "红包");
            }
            else if (currentIncomeButton == otherIncomeButton)
            {
                currentLuGameObject = otherLu;
                SetViewData(E_BillType.income, DataManager.instance.otherIncome, "其它");
            }
        }
        currentLuGameObject.SetActive(true);
    }
    public void SetViewData(E_BillType e_BillType,float  amount, string categoryTag)
    {
        float totalAmount = 0;
        if(e_BillType == E_BillType.expenses)
        {
            tipText.text = $"<cspace=-10>本月<u>{categoryTag}</u>累计消费</cspace>\n<size=120>￥</size><size=150>{amount:F2}</size>";
            if (DataManager.instance.totalExpenses <= 0f)
            {
                numberTween?.Kill();
                currentPercent = 0f;
                percentText.text = "0.00%";
                barImage.fillAmount = 0f;
                return;
            }
            totalAmount = DataManager.instance.totalExpenses;
        }else
        {
            tipText.text = $"<cspace=-10>本月<u>{categoryTag}</u>累计入账</cspace>\n<size=120>￥</size><size=150>{amount:F2}</size>";
            if (DataManager.instance.totalIncome <= 0f)
            {
                numberTween?.Kill();
                currentPercent = 0f;
                percentText.text = "0.00%";
                barImage.fillAmount = 0f;
                return;
            }
            totalAmount = DataManager.instance.totalIncome;
        }
        
        numberTween?.Kill();
        float targetPercent = Mathf.Clamp01(amount / totalAmount);
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
