using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private AllLedger allLedger;
    public MonthLedger currentMonthLedger;
    public int currentBillID;

    public static string year { get; private set; }
    public static string month { get; private set; }
    public static string day { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DateTime now = DateTime.Now;
        year = $"{now.Year}";
        month = $"{now.Year}.{now.Month}";
        day = $"{now.Year}.{now.Month}.{now.Day}";
    }
    void Start()
    {
        AppInit();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //App初始化
    public void AppInit()
    {
        Debug.Log($"文件存储路径:{GetSaveFilePath()}");
        //查找并默认显示本月账单
        SetAllLedger(LoadAllLedger());
        currentMonthLedger = FindMonthLedger(month, true);
        currentBillID = -1;
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


    //获得存储路径
    public static string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, "LedgerData.json");
    }

    //加载数据
    public static AllLedger LoadAllLedger()
    {
        if (File.Exists(GetSaveFilePath()))
        {
            string jsonText = File.ReadAllText(GetSaveFilePath());
            AllLedger allLedger = JsonUtility.FromJson<AllLedger>(jsonText);
            if (allLedger == null)
            {
                allLedger = new AllLedger(new List<MonthLedger>());
            }
            return allLedger;
        }
        else
        {
            AllLedger allLedger = new AllLedger(new List<MonthLedger>());
            return allLedger;
        }
    }

    //保存数据
    public static void SaveAllLedger()
    {
        string jsontext = JsonUtility.ToJson(DataManager.instance.GetAllLedger(), prettyPrint: true);
        File.WriteAllText(GetSaveFilePath(), jsontext);
    }

    //删除数据
    public void DeleteAllSaveData()
    {
        if (File.Exists(GetSaveFilePath()))
        {
            File.Delete(GetSaveFilePath());
            Debug.Log("旧存档文件已删除");
        }
        AppInit();
    }
    public void DeleteCurrentBill()
    {
        int count = currentMonthLedger.billList.RemoveAll(bill => bill.ID == currentBillID);
        if(count > 0)
        {
            Debug.Log($"删除成功！账单ID:{currentBillID}");
            SaveAllLedger(); 
            SetAllLedger(LoadAllLedger());
            currentMonthLedger = FindMonthLedger(month, true);
            currentBillID = -1;
            UIManager.instance.RefreshBillListContent();
        }
        else
        {
            Debug.Log("删除失败！");
        }
    }

    //查找指定月账单
    public MonthLedger FindMonthLedger(string month, bool isChange)
    {
        MonthLedger monthLedger = allLedger.monthLedgerList.Find(monthledger => monthledger.month == month); ;
        if (isChange)
        {
            currentMonthLedger = monthLedger;
        }
        return monthLedger;
    }

    //查找单条账单
    public int GetCurrentBillID()
    {
        return currentBillID;
    }
    public Bill FindBill()
    {
        return currentMonthLedger.billList.Find(bill => bill.ID == currentBillID);
    }
    public Bill FindBill(int ID)
    {
        return currentMonthLedger.billList.Find(bill =>  bill.ID == ID);
    }

    //分配订单ID
    public int GetBillID()
    {
        return allLedger.nextBillID++;
    }

    public void SetAllLedger(AllLedger allLedger)
    {
        this.allLedger = allLedger;
    }
    public AllLedger GetAllLedger()
    {
        return allLedger;
    }
    public void SetCurrentMonthLedger(MonthLedger currentMonthLedger)
    {
        this.currentMonthLedger = currentMonthLedger;
    }
    public MonthLedger GetCurrentMonthLedger()
    {
        return currentMonthLedger;
    }
    public void AddNewMonthLedger(MonthLedger monthLedger)
    {
        allLedger.monthLedgerList.Add(monthLedger);
    }
    public void SetCurrentBillID(int newBillID)
    {
        this.currentBillID = newBillID;
    }
    public void AddNewBill(Bill newBill)
    {
        currentMonthLedger.billList.Add(newBill);
    }

    #region 按钮点击函数

    #endregion
}
