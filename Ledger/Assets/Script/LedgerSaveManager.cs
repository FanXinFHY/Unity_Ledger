using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LedgerSaveManager
{
    //获得存储路径
    public static string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, "LedgerData.json");
    }

    //加载数据
    public static AllLedger LoadAllLedger()
    {
        if(File.Exists(GetSaveFilePath()))
        {
            string jsonText = File.ReadAllText(GetSaveFilePath());
            AllLedger allLedger = JsonUtility.FromJson<AllLedger>(jsonText);
            if(allLedger == null)
            {
                allLedger = new AllLedger(new List<MonthLedger>());
            }
            return allLedger;
        }else
        {
            AllLedger allLedger = new AllLedger(new List<MonthLedger>());
            return allLedger;
        }
    }

    //保存数据
    public static void SaveAllLedger(AllLedger allLedger)
    {
        string jsontext = JsonUtility.ToJson(allLedger, prettyPrint: true);
        File.WriteAllText(GetSaveFilePath(),jsontext);
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
}
