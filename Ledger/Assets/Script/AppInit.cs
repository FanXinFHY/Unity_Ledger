using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppInit : MonoBehaviour
{
    void Awake()
    {
        //DeleteAllLedgerData();
        DataManager.instance.SetAllLedger(DataManager.LoadAllLedger());
    }
    void Start()
    {
        Debug.Log($"文件存储路径:{DataManager.GetSaveFilePath()}");
        //查找并默认显示本月账单
        MonthLedger currentMonthLedger = DataManager.instance.FindMonthLedger(DataManager.month,true);
        if (currentMonthLedger == null)
        {
            Debug.Log("本月暂无订单");          
        }
        else
        {      
            Debug.Log($"本月账单加载成功，账单数：{currentMonthLedger.billList.Count}");
        }
        UIManager.instance.RefreshBillListContent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //清空历史数据
    public void DeleteAllLedgerData()
    {
        DataManager.ClearAllSaveData();
    }
}
