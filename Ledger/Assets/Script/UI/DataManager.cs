using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;


    private AllLedger allLedger;
    private MonthLedger currentMonthLedger;
    private Bill currentBill;

    public static string year { get; private set; }
    public static string month { get; private set; }
    public static string day { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
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

    }

    // Update is called once per frame
    void Update()
    {
        
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

    //清空数据
    public static void ClearAllSaveData()
    {
        if (File.Exists(GetSaveFilePath()))
        {
            File.Delete(GetSaveFilePath());
            Debug.Log("旧存档文件已删除");
        }
    }
    public MonthLedger FindMonthLedger(string month,bool isChange)
    {
        MonthLedger monthLedger = allLedger.monthLedgerList.Find(monthledger => monthledger.month == month); ;
        if(isChange)
        {
            currentMonthLedger = monthLedger;
        }
        return monthLedger;
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
        this .currentMonthLedger = currentMonthLedger;
    }
    public MonthLedger GetCurrentMonthLedger()
    {
        return currentMonthLedger;
    }
    public void AddNewMonthLedger(MonthLedger monthLedger)
    {
        allLedger.monthLedgerList.Add(monthLedger);
    }
    public void SetCurrentBill(Bill newBill)
    {
        this.currentBill = newBill;
    }
    public Bill GetCurrentBill()
    {
        return currentBill;
    }
    public void AddNewBill(Bill newBill)
    {
        currentMonthLedger.billList.Add(newBill);
    }
}
