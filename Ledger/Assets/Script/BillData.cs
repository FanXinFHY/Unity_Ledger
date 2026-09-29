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
public enum E_BillCategory
{
    other,
    foodAndDrink,
    accommodation,
    entertainment,
}

[Serializable]
public class Bill
{
    public int ID;
    public E_BillType e_BillType;
    public E_BillCategory e_BillCategory;
    public string date;
    public float amount;
    public string remark;
    public Bill() { }
    public Bill(int ID, E_BillType e_BillType, E_BillCategory e_BillCategory,string date,float amount,string remark)
    {
        this.ID = ID;
        this.e_BillType = e_BillType;
        this.e_BillCategory = e_BillCategory;
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
    public MonthLedger() { }
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
    public int nextBillID;
    public List<MonthLedger> monthLedgerList;
    public AllLedger() { }
    public AllLedger(List<MonthLedger> monthLedgerList)
    {
        this.monthLedgerList = monthLedgerList;
    }

}