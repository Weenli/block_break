using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public Button[] SkillFill;
    float[] MaxCooldown = new float[3];
    float[] CurrentCooldown = new float[3];
    bool[] IsOnCooldown = { false, false, false };
    // Start is called before the first frame update
    void Start()
    {
        MaxCooldown[0] = 60f;
        MaxCooldown[1] = 40f;
        MaxCooldown[2] = 20f;
        for(int i = 0; i < 3; i++)
        {
            CurrentCooldown[i] = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            if (IsOnCooldown[i])
            {
                CurrentCooldown[i] -= Time.deltaTime;
            }
            if(CurrentCooldown[i] <= 0)
            {
                CurrentCooldown[i] = 0;
                IsOnCooldown[i] = false;
                SkillFill[i].interactable = true;
            }
        }
    }
    public bool CanUseSkill(int number)
    {
        if(CurrentCooldown[number] <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void PutOnCooldown(int number)
    {
        CurrentCooldown[number] = MaxCooldown[number];
        IsOnCooldown[number] = true;
        SkillFill[number].interactable = false;
    }
    public void Restart()
    {
        for(int i = 0; i < 3; i++)
        {
            CurrentCooldown[i] = 0f;
            IsOnCooldown[i] = false;
            SkillFill[i].interactable = true;
        }
    }
}
