using UnityEngine;
using UnityEngine.EventSystems;
public enum Direction { Up, Down, Left, Right,shoot }

// MobileButtonHandler.cs
public class MobileButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Direction direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (direction)
        {
            case Direction.Up: PlayerController.instance.Move4direction(0, 1); break;
            case Direction.Down: PlayerController.instance.Move4direction(0, -1); break;
            case Direction.Left: PlayerController.instance.Move4direction(-1, 0); break;
            case Direction.Right: PlayerController.instance.Move4direction(1, 0); break;
            case Direction.shoot: PlayerController.instance.ClickShootBTN(); break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerController.instance.Move4direction(0, 0);
    }
}
