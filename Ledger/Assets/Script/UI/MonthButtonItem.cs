using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonthButtonItem : MonoBehaviour
{
    public string month;
    public TextMeshProUGUI monthText;

    public void SetData(string month)
    {
        this.month = month;
        monthText.text = month;
        GetComponent<Button>().onClick.AddListener(MonthButton);
    }
    public void MonthButton()
    {
        DataManager.instance.SetCurrentMonthLedger(DataManager.instance.FindMonthLedger(month, true));
        UIManager.instance.selectMonthPanel.SetActive(false);
        UIManager.instance.RefreshBillListContent();
    }
}
