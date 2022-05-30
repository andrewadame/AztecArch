using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerController : MonoBehaviour
{
    public ItemTierHolder[] TierSelection;
    public ObjectSpawnerController ObjectSpawner;
    public GameObject defaultObject;

    private void Awake()
    {
        if(TierSelection.Length == 0)
        {
            TierSelection = new ItemTierHolder[10];
        }
    }

    public GameObject GetItemFromTier(float[] TierOptions)
    {
        GameObject itemTier = ObjectSpawner.tempSpawn;
        //Remove when building
       
        float tierRange = Random.RandomRange(0, 100);
        if (tierRange < TierOptions[0])                      //base tier
        {
            Debug.Log("tier 0");
            return ChooseFromTier(TierSelection[0].ItemTier);
        }
        else if (tierRange < TierOptions[1])                      //base tier
        {
            Debug.Log("tier 1");
            return ChooseFromTier(TierSelection[1].ItemTier);
        }
        else if (tierRange < TierOptions[2])
        {
            Debug.Log("tier 2");
            return ChooseFromTier(TierSelection[2].ItemTier);
        }
        else
            return defaultObject;
    }

    public GameObject ChooseFromTier(ItemTierObject itemTier)
    {
        float tierObject = Random.RandomRange(0, itemTier.TierDrops.Length);
        return itemTier.TierDrops[Mathf.CeilToInt(tierObject) % itemTier.TierDrops.Length];
    }
}
