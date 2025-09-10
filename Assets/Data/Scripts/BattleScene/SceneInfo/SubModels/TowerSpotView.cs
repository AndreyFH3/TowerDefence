using UnityEngine;
using UnityEngine.EventSystems;

namespace Levels.Game.Sub
{
    public class TowerSpotView : MonoBehaviour, IPointerClickHandler
    {
        public System.Action<Vector3> OnClick;
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(transform.position);
        }
    }
}