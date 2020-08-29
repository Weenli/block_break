using UnityEngine;
using System.Collections;
public class TouchController : MonoBehaviour
{
    public delegate void TouchEventHandler(Vector2 swipe);
    public static event TouchEventHandler SwipeEvent;
    public static event TouchEventHandler SwipeEndEvent;
    int MinSwipeDistance = 100;
    Vector2 TouchMovement;



    void OnSwipe()
    {
        if (SwipeEvent != null)
        {
            SwipeEvent(TouchMovement);
        }
    }
    void OnSwipeEnd()
    {
        if (SwipeEndEvent != null)
        {
            SwipeEndEvent(TouchMovement);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                TouchMovement = Vector2.zero;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                TouchMovement += touch.deltaPosition;
                if (TouchMovement.magnitude > MinSwipeDistance)
                {
                   
                    OnSwipe();
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                OnSwipeEnd();
            }
           
        }

    }
}
