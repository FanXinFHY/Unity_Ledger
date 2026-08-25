using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppInit : MonoBehaviour
{
    public static AllLedger allLedger { get; private set; }

    void Awake()
    {
        allLedger = LedgerSaveManager.LoadAllLedger();
    }
    void Start()
    {
        //查找并默认显示本月账单
        DateTime now = DateTime.Now;
        string currentMonth = $"{now.Year}.{now.Month}";

        MonthLedger currentMonthLedger = allLedger.monthLedgerList.Find(monthledger => monthledger.month == currentMonth);

        if (currentMonthLedger == null)
        {
            Debug.Log("本月暂无订单");          
        }
        else
        {      
            Debug.Log($"本月账单加载成功，账单数：{currentMonthLedger.transactionList.Count}");
            UIManager.instance.RefreshMonthLedgerUI(currentMonthLedger.transactionList);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
