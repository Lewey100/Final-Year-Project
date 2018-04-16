using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocator : MonoBehaviour {
    public List<string> ValidTags;

   public List<GameObject> targets = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        bool valid = false;

        for (int i = 0; i < ValidTags.Count; ++i) //When enemies enter trigger sphere, add them to target list
        {
            if (other.CompareTag(ValidTags[i]))
            {
                valid = true;
            }
        }

        if (!valid)
            return;

        targets.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < targets.Count; ++i)
        {
            if (other.gameObject == targets[i])
            {
                targets.Remove(other.gameObject); //When enemies leave trigger sphere, remove them
                return;
            }
        }
    }

    public List<GameObject> GetTargets()
    {
        return targets;
    }

    public bool IsInRange(GameObject o) //Not used.
    {
        for (int i = 0; i < targets.Count; ++i)
        {
            if (o == targets[i])
                return true;
        }
        return false;
    }
}
