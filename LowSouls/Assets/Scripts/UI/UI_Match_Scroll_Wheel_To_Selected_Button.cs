using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LS
{
    public class UI_Match_Scroll_Wheel_To_Selected_Button : MonoBehaviour
    {
        [SerializeField] GameObject currentSelected;
        [SerializeField] GameObject previousSelected;
        [SerializeField] RectTransform currentSelectedTransform;
        [SerializeField] RectTransform contentPanel;
        [SerializeField] ScrollRect scrollRect;

        private void Update()
        {
            currentSelected = EventSystem.current.currentSelectedGameObject;

            if (currentSelected != null)
            {
                if (currentSelectedTransform != previousSelected)
                {
                    previousSelected = currentSelected;
                    currentSelectedTransform = currentSelected.GetComponent<RectTransform>();
                    SnapTo(currentSelectedTransform);
                }
            }
        }

        private void SnapTo(RectTransform targegt)
        {
            Canvas.ForceUpdateCanvases();

            Vector2 newPos = (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position) - (Vector2)scrollRect.transform.InverseTransformPoint(targegt.position);
            newPos.x = 0;

            contentPanel.anchoredPosition = newPos;
        }
    }
}
