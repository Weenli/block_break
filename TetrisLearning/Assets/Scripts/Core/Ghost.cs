using UnityEngine;

public class Ghost : MonoBehaviour
{
    Shape GhostShape = null;
    bool hitbottom = false;
    public Color ghostcolor = new Color(1f, 1f, 1f, 0.4f);
    public void DrawGhost(Shape originalshape, Board gameboard)
    {
        if (!GhostShape)
        {   
            GhostShape = Instantiate(originalshape, originalshape.transform.position, originalshape.transform.rotation) as Shape;
            GhostShape.gameObject.name = "GhostShape";

            SpriteRenderer[] allRenderers = GhostShape.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer r in allRenderers)
            {
                r.color = ghostcolor;
            }
        }
        else
        {
            GhostShape.transform.position = originalshape.transform.position;
            GhostShape.transform.rotation = originalshape.transform.rotation;
        }
        hitbottom = false;
        while (!hitbottom)
        {
            GhostShape.MoveDown();
            if (!gameboard.IsValidPosition(GhostShape))
            {
                GhostShape.MoveUp();
                hitbottom = true;
            }
        }
    }
    public void Reset()
    {
        Destroy(GhostShape.gameObject);

    }
}

