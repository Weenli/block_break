using UnityEngine;
using UnityEngine.UI;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] Skyboxes;
    public int i;

    public void ChangeSkybox()
    {
        RenderSettings.skybox = Skyboxes[i];
        RenderSettings.skybox.SetFloat("_Rotation", i*12);
    }

    void Changer()
    {
        ChangeSkybox();
        if (i == 4) i = 0;
        else i++;
    }
    private void Start()
    {
        i = 0;
        InvokeRepeating("Changer", 0, 12);
    }
}