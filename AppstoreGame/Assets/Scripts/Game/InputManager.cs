using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Character _character;

    private void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (IsPointerOverUI(touch.fingerId))
                return;
            Vector2 worldPos = _camera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _character.OnStart(worldPos);
                    break;

                case TouchPhase.Moved:
                    _character.OnDragging(worldPos);
                    break;

                case TouchPhase.Ended:
                    _character.OnEnded();
                    break;
            }

        }
    }

    private bool IsPointerOverUI(int fingerId = -1)
    {
        if (fingerId != -1)
        {
            return EventSystem.current.IsPointerOverGameObject(fingerId);
        }
        return true;
    }
}
