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

    [Header("Calculation")]
    public float totalIncome;
    public float totalExpenses;
    public float foodAndDrinkExpenses;
    public float accommodationExpenses;
    public float entertainmentExpenses;
    public float otherExpenses;



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
    }
    void Start()
    {
        AppInit();
    }

    //App初始化
    public void AppInit()
    {
        Debug.Log($"文件存储路径:{GetSaveFilePath()}");
        //查找并默认显示本月账单
        SetAllLedger(LoadAllLedger());
        currentMonthLedger = FindMonthLedger(GetToMonth(), true);
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
        UIManager.instance.RefreshTotalDeposit();
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
            SaveAllLedger(); 
            SetAllLedger(LoadAllLedger());
            Debug.Log($"删除成功！账单ID:{currentBillID}");

            currentBillID = -1;
            UIManager.instance.RefreshBillListContent();
        }
        else
        {
            Debug.Log("删除失败！");
        }
    }
    //复制数据
    public void CopyCurrentBill()
    {
        Bill currentBill = FindBill();
        Bill copyBill = new Bill(GetBillID(),currentBill.e_BillType, currentBill.e_BillCategory,GetToMinute(),currentBill.amount,currentBill.remark);
        AddNewBill(copyBill);
        //computeTotalDeposit(copyBill);
        SaveAllLedger();
        SetAllLedger(LoadAllLedger());
        Debug.Log($"复制成功！账单ID:{currentBillID}");

        currentBillID = -1;
        UIManager.instance.RefreshBillListContent();
    }

    //查找指定月账单
    public MonthLedger FindMonthLedger(string month, bool isToCurrentMonth)
    {
        MonthLedger monthLedger = allLedger.monthLedgerList.Find(monthledger => monthledger.month == month); ;
        if (isToCurrentMonth)
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

    //账单数据计算
    public void computeData()
    {
        totalIncome = 0;
        totalExpenses = 0;
        foodAndDrinkExpenses = 0;
        accommodationExpenses = 0;
        entertainmentExpenses = 0;
        otherExpenses = 0;
        foreach(Bill bill in GetCurrentMonthLedger().billList)
        {
            if(bill.e_BillType == E_BillType.expenses)
            {
                totalExpenses += bill.amount;
                if(bill.e_BillCategory == E_BillCategory.foodAndDrink)
                {
                    foodAndDrinkExpenses += bill.amount;
                    continue;
                }
                if (bill.e_BillCategory == E_BillCategory.accommodation)
                {
                    accommodationExpenses += bill.amount;
                    continue;
                }
                if (bill.e_BillCategory == E_BillCategory.entertainment)
                {
                    entertainmentExpenses += bill.amount;
                    continue;
                }
                otherExpenses += bill.amount;
                continue;
            }

            totalIncome += bill.amount;
        }
    }
    //计算存款
    public void computeTotalDeposit(Bill bill)
    {
        if(bill.e_BillType == E_BillType.income)
        {
            allLedger.totalDeposit += bill.amount;
        }else
        {
            allLedger.totalDeposit -= bill.amount;
        }
        UIManager.instance.RefreshTotalDeposit();
    }
    public void setTotalDeposit(float totalDeposit)
    {
        allLedger.totalDeposit = totalDeposit;
    }
    public float getTotalDeposit()
    {
        return allLedger.totalDeposit;
    }
    public void setOnlineFunds(float onlineFunds)
    {
        allLedger.onlineFunds = onlineFunds;
    }
    public float getOnlineFunds()
    {
        return allLedger.onlineFunds;
    }
    public void setCash(float cash)
    {
        allLedger.cash = cash;
    }
    public float getCash()
    {
        return allLedger.cash;
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

    #region 获取日期
    public static string GetYear()
    {
        DateTime now = DateTime.Now;
        return  $"{now.Year}";
    }
    public static string GetMonth()
    {
        DateTime now = DateTime.Now;
        return  $"{now.Month}";
    }
    public static string GetToMonth()
    {
        DateTime now = DateTime.Now;
        return  $"{now.Year}.{now.Month}";
    }
    public static string GetDay()
    {
        DateTime now = DateTime.Now;
        return  $"{now.Day}";
    }
    public static string GetToDay()
    {
        DateTime now = DateTime.Now;
        return  $"{now.Year}.{now.Month}.{now.Day}";
    }
    public static string GetHour()
    {
        DateTime now = DateTime.Now;
        return  $"{now.Hour}";
    }

    public static string GetMinute()
    {
        DateTime now = DateTime.Now;
        if(now.Minute >= 10)
        {
            return  $"{now.Minute}";
        }else
        {
            return $"0{now.Minute}";
        }

    }
    public static string GetToMinute()
    {
        DateTime now = DateTime.Now;
        if (now.Minute >= 10)
        {
            return $"{now.Year}.{now.Month}.{now.Day}\n{now.Hour}:{now.Minute}";
        }
        else
        {
            return $"{now.Year}.{now.Month}.{now.Day}\n{now.Hour}:0{now.Minute}";
        }
    }
    #endregion

    #region 按钮点击函数

    #endregion
}
