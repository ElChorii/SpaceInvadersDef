using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unused : MonoBehaviour
{
    //mantener sobjeto
    public static unused instance;
    public int selectedNumber;

    private void Awake()
    {
        if (unused.instance == null)
        {
            unused.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        
    }
}
