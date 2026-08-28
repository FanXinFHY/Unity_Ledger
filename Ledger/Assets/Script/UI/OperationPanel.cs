using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OperationPanel : MonoBehaviour
{
    public GameObject transactionListContent;
    public GameObject operationContent_Up;
    public GameObject operationContent_Down;
    void Start()
    {
        transactionListContent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ClickMask();
    }

    public void JudgeUpOrDown(RectTransform parentRectTransform)
    {
        float middleHeight = parentRectTransform.rect.height / 2f;
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

                if (isSelfOrChild)
                {
                    return;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
