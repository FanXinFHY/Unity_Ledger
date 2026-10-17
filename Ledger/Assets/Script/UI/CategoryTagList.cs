using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryTagList : MonoBehaviour
{
    public GameObject foodAndDrink;
    public GameObject accommodation;
    public GameObject entertainment;
    public GameObject healthcare;
    public GameObject salary;
    public GameObject sideJob;
    public GameObject transfer;
    public GameObject envelope;
    public GameObject other;

    public E_BillCategory GetBillCategory()
    {
        if (foodAndDrink.activeSelf) return E_BillCategory.foodAndDrink;
        if (accommodation.activeSelf) return E_BillCategory.accommodation;
        if (entertainment.activeSelf) return E_BillCategory.entertainment;
        if (healthcare.activeSelf) return E_BillCategory.healthcare;
        if (salary.activeSelf) return E_BillCategory.salary;
        if (sideJob.activeSelf) return E_BillCategory.sideJob;
        if (transfer.activeSelf) return E_BillCategory.transfer;
        if (envelope.activeSelf) return E_BillCategory.envelope;
        return E_BillCategory.other;
    }
    public void SelecteBillCategory(E_BillCategory billCategory)
    {
        if (billCategory == E_BillCategory.foodAndDrink)
        {
            foodAndDrink.SetActive(true);
            return;
        }
        if (billCategory == E_BillCategory.accommodation)
        {
            accommodation.SetActive(true);
            return;
        }
        if (billCategory == E_BillCategory.entertainment)
        {
            entertainment.SetActive(true);
            return;
        }
        if (billCategory == E_BillCategory.healthcare)
        {
            healthcare.SetActive(true);
            return;
        }
        if (billCategory == E_BillCategory.salary)
        {
            salary.SetActive(true);
            return;
        }
        if (billCategory == E_BillCategory.sideJob)
        {
            sideJob.SetActive(true);
            return;
        }
        if (billCategory == E_BillCategory.transfer)
        {
            transfer.SetActive(true);
            return;
        }
        if (billCategory == E_BillCategory.envelope)
        {
            envelope.SetActive(true);
            return;
        }
        other.SetActive(true);
    }

    public void DisplayCategoryTag(E_BillType e_BillType)
    {
        if(e_BillType == E_BillType.expenses)
        {
            foodAndDrink.transform.parent.gameObject.SetActive(true);
            accommodation.transform.parent.gameObject.SetActive(true);
            entertainment.transform.parent.gameObject.SetActive(true);
            healthcare.transform.parent.gameObject.SetActive(true);
            salary.transform.parent.gameObject.SetActive(false);
            sideJob.transform.parent.gameObject.SetActive(false);
            transfer.transform.parent.gameObject.SetActive(false);
            envelope.transform.parent.gameObject.SetActive(false);
        }else
        {
            foodAndDrink.transform.parent.gameObject.SetActive(false);
            accommodation.transform.parent.gameObject.SetActive(false);
            entertainment.transform.parent.gameObject.SetActive(false);
            healthcare.transform.parent.gameObject.SetActive(false);
            salary.transform.parent.gameObject.SetActive(true);
            sideJob.transform.parent.gameObject.SetActive(true);
            transfer.transform.parent.gameObject.SetActive(true);
            envelope.transform.parent.gameObject.SetActive(true);
        }
    }
    public void Reset()
    {
        foodAndDrink.SetActive(false);
        accommodation.SetActive(false);
        entertainment.SetActive(false);
        healthcare.SetActive(false);
        salary.SetActive(false);
        sideJob.SetActive(false);
        transfer.SetActive(false);
        envelope.SetActive(false);
        other.SetActive(false);
    }
}
