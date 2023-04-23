using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZengaBlock : MonoBehaviour
{
    public Stack stack;

    public void InitializeZengaBlock(Stack stack,int positionalId)
    {
        this.stack = stack;
        var myMaterial = GlobalContext.Instance.GetMaterialByStackMastery(stack.mastery);
        SetMaterial(myMaterial);
    }

    public void SetMaterial(Material material)
    {
        GetComponent<Renderer>().material = material;
    }

}
