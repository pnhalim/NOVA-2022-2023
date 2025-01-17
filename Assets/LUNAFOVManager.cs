using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUNAFOVManager : MonoBehaviour
{
    public SketchYawOffset sketchYawOffset;

    void Start(){
        Debug.Log("start");
    }

    public void LUNALeft()
    {
        sketchYawOffset.offset = 20;
        StateMachineNOVA.LUNA = LUNAState.left;
    }
    public void LUNACenter()
    {
        sketchYawOffset.offset = 0;
        StateMachineNOVA.LUNA = LUNAState.center;
    }
    public void LUNARight()
    {
        sketchYawOffset.offset = -20;
        StateMachineNOVA.LUNA = LUNAState.right;
    }
}
