using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledger : ScriptableObject
{
    private List<int> ledger = new List<int>();

    //get the entire ledger
    public List<int> GetLedger()
    {
        return ledger;
    }

    //store a value in the ledger if it does not contain it.
    public bool StoreValue(int value)
    {
        if (!ledger.Contains(value))
        {
            ledger.Add(value);

            return true;
        }
        else
            return false;
    }
}

