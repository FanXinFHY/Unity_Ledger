using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum E_BillType
{
    income,
    expenses,
}
[Serializable]
public class Bill
{
    public E_BillType e_BillType;
    public string date;
    public float amount;
    public string remark;
    public Bill(E_BillType e_BillType,string date,float amount,string remark)
    {
        this.e_BillType = e_BillType;
        this.date = date;
        this.amount = amount;
        this.remark = remark;
    }
}
[Serializable]
public class MonthLedger
{
    public string month;
    public float plannedExpenses;
    public List<Bill> billList;
    public MonthLedger(string month,float plannedExpense, List<Bill> billList)
    {
        this.month = month;
        this.plannedExpenses = plannedExpense;
        this.billList = billList;
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