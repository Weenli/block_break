using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class IconToggle : MonoBehaviour
{
    public Sprite IconTrue;
    public Sprite IconFalse;
    public bool IconState = true;
    Image IconImage;

    // Start is called before the first frame update
    void Start()
    {
        IconImage = GetComponent<Image>();
    }
    void Update()
    {
        
    }
    public void ToggleIcon(bool state)
    {
        if(!IconImage || !IconTrue || !IconFalse)
        {
            return;
        }
        IconImage.sprite = (state) ? IconTrue : IconFalse;
    }
    public void ToggleIconX(int x)
    {
        if (!IconImage || !IconTrue || !IconFalse)
        {
            return;
        }
        if (x == 0)
        {
            ToggleIcon(true);
        }
        else if(x == 1) {
            ToggleIcon(false);
        }
        
    }


}
