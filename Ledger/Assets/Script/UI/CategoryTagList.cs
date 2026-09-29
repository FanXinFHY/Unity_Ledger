using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryTagList : MonoBehaviour
{
    public GameObject foodAndDrink;
    public GameObject accommodation;
    public GameObject entertainment;
    public GameObject other;

    public E_BillCategory GetBillCategory()
    {
        if (foodAndDrink.activeSelf) return E_BillCategory.foodAndDrink;
        if(accommodation.activeSelf) return E_BillCategory.accommodation;
        if (entertainment.activeSelf) return E_BillCategory.entertainment;
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
        other.SetActive(true);
    }
    public void Reset()
    {
        foodAndDrink.SetActive(false);
        accommodation.SetActive(false);
        entertainment.SetActive(false);
        other.SetActive(false);
    }
}
