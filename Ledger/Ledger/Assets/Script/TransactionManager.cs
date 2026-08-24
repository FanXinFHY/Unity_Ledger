using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Transaction
{
    public string date;
    public float amount;
    public string remark;
}
[Serializable]
public class MonthLedger
{
    public string month;
    public List<Transaction> transactionList;
}
[Serializable]
public class AllLedger
{
    public List<MonthLedger> monthLedgerList;
}