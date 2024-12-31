using UnityEngine;
using System.Collections.Generic;
public class StatusEffectManager : Singleton<StatusEffectManager>
{
    [SerializeField] List<GameObject> statusEffectPrefabs = new List<GameObject>();

    public GameObject GetStatusEffect(string statusEffectName)
    {
        return statusEffectPrefabs.Find(x => x.GetComponent<StatusEffect>().effectName == statusEffectName);
    }

}


