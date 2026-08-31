using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppInit : MonoBehaviour
{
    public static AllLedger allLedger { get; private set; }

    void Awake()
    {
        //LedgerSaveManager.ClearAllSaveData();//清空历史数据
        allLedger = LedgerSaveManager.LoadAllLedger();
    }
    void Start()
    {
        Debug.Log($"文件存储路径:{LedgerSaveManager.GetSaveFilePath()}");
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
            Debug.Log($"本月账单加载成功，账单数：{currentMonthLedger.billList.Count}");
            UIManager.instance.RefreshMonthLedgerUI(currentMonthLedger);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
