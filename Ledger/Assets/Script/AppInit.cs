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
        DateTime now = DateTime.Now;
        string currentMonth = $"{now.Year}.{now.Month}";

        MonthLedger currentMonthLedger = allLedger.monthLedgerList
            .Find(ledger => ledger.month == currentMonth);

        List<Transaction> currentMonthLedgerList;
        if(currentMonthLedger != null)
        {
            currentMonthLedgerList = currentMonthLedger.transactionList;
            Debug.Log($"本月账单加载成功，账单数：{currentMonthLedgerList.Count}");
        }else
        {
            currentMonthLedgerList = new List<Transaction>();
            Debug.Log("本月暂无订单");
        }
        UIManager.instance.RefreshMonthLedgerUI(currentMonthLedgerList);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
