using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   public static UIManager Instance;
    private bool isHoveringUI = false;
    
    private void Awake()
    {
        Instance = this;
    }
    public void HoveringState(bool state)
    {
        isHoveringUI = state;

    }

    public bool IsHoveringUI()
    {
       return isHoveringUI;
    }
}
