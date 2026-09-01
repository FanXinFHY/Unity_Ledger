using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OperationPanel : MonoBehaviour
{
    public static OperationPanel instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        ClickMask();
    }

    public void ClickMask()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                GameObject clickedGameObject = results[0].gameObject;

                // 是否是自己或者自己的子物体(ISChildOf)
                bool isSelfOrChild = ((clickedGameObject == gameObject) || clickedGameObject.transform.IsChildOf(transform));

                if (!isSelfOrChild)
                {
                    DataManager.instance.SetCurrentBillID(-1);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    #region 按钮点击函数
    #endregion
}
