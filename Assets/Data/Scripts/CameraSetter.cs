using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
    }
}
