using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Layout : MonoBehaviour
{
    [Header("Padding")]
    public float top;
    public float left;
    public float spacing;

    void Start()
    {
        
    }

    //清除容器item
    public void ClearContent()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    //排列容器item
    public void VerticalLayout(GameObject item)
    {
        float itemHeight = item.GetComponent<RectTransform>().rect.height;
        float y = top;
        foreach(Transform childTransform in transform)
        {
            RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
            childRectTransform.anchoredPosition = new Vector2(left, -y);
            y += itemHeight;
            y += spacing;
        }
        float totalHeight = transform.childCount == 0 ? 0 : top + itemHeight * transform.childCount + spacing * (transform.childCount - 1);
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0f,totalHeight);
    }
}
