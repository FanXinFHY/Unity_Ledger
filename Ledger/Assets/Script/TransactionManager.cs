using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Transaction
{
    public string date;
    public float amount;
    public string remark;
    public Transaction(string date,float amount,string remark)
    {
        this.date = date;
        this.amount = amount;
        this.remark = remark;
    }
}
[Serializable]
public class MonthLedger
{
    public string month;
    public float plannedSpend;
    public List<Transaction> transactionList;
    public MonthLedger(string month,float plannedSpend, List<Transaction> transactionList)
    {
        this.month = month;
        this.plannedSpend = plannedSpend;
        this.transactionList = transactionList;
    }
}
[Serializable]
public class AllLedger
{
    public List<MonthLedger> monthLedgerList;
    public AllLedger(List<MonthLedger> monthLedgerList)
    {
        this.monthLedgerList = monthLedgerList;
    }

}