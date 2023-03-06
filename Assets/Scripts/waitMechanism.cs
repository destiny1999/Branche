using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitMechanism : MonoBehaviour
{
    static bool ok = false;
    public static void SetOkToTrue()
    {
        ok = true;
    }
    public static void SetOkToFalse()
    {
        ok = false;
    }
    public static bool getOk()
    {
        return ok;
    }
}
