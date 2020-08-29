using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Shape[] allShapes;
    public Transform[] QueueShape = new Transform[3];
    Shape[] ShapeQueue = new Shape[3];
    float ShapeScale = 0.35f;
    Shape GetRandomShape()
    {
        int i = Random.Range(0, allShapes.Length);
        if (allShapes[i])
        {
            return allShapes[i];    
        }
        else
        {
            Debug.Log("WARNING!");
            return null;
        }
    }
    public Shape SpawnShape()
    {
        Shape shape = null;
        shape = GetQueuedShape();
        shape.transform.position = transform.position;
        shape.transform.localScale = Vector3.one;
        if (shape)
        {
            return shape;
        }
        else
        {
            Debug.Log("WARNING!");
            return null;
        }
    }

    public void InitQueue()
    {
        for(int i = 0; i < ShapeQueue.Length; i++)
        {
            if (ShapeQueue[i])
            {
                Destroy(ShapeQueue[i].gameObject);
            }
            ShapeQueue[i] = null;
        }
        FillQueue();
    }
    public void FillQueue()
    {
        for (int i = 0; i < ShapeQueue.Length; i++)
        {
            if (!ShapeQueue[i])
            {
                ShapeQueue[i] = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as Shape;
                ShapeQueue[i].transform.position = QueueShape[i].position + ShapeQueue[i].QueueOffset;
                ShapeQueue[i].transform.localScale = new Vector3(ShapeScale, ShapeScale, ShapeScale);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Shape GetQueuedShape()
    {
        Shape FirstShape = null;
        if (ShapeQueue[0])
        {
            FirstShape = ShapeQueue[0];
        }
        for(int i = 1; i < ShapeQueue.Length; i++)
        {
            ShapeQueue[i - 1] = ShapeQueue[i];
            ShapeQueue[i - 1].transform.position = QueueShape[i - 1].position + ShapeQueue[i].QueueOffset;
        }
        ShapeQueue[ShapeQueue.Length - 1] = null;
        FillQueue();
        return FirstShape;
    }

}
