using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public Transform backGround1;
    public Transform backGround2;
    private Vector3 Pos1;
    private Vector3 Pos2;
    private void Start()
    {
        if (backGround1 != null && backGround2 != null)
        {
            Pos1 = backGround1.position;
            Pos2 = backGround2.position;
        }
    }
    private void Update()
    {
        ControllerBackGround();
    }
    void ControllerBackGround()
    {
        if (backGround1 != null && backGround2 != null)
        {
            if (Mathf.Abs(backGround2.position.x - Pos1.x) < 0.1f)
            {
                backGround1.position = Pos2;
                swap(ref backGround1, ref backGround2);
            }
        }
    }
    void swap(ref Transform a, ref Transform b)
    {
        Transform temp = a;
        a = b;
        b = temp;
    }
}
