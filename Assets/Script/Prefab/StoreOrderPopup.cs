using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOrderPopup : OrderedPopup
{
    public static StoreOrderPopup Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

}
