using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestroyer : MonoBehaviour
{

    public void DestroyAllLooseObjs()
    {
        GameObject[] loseChickens = GameObject.FindGameObjectsWithTag("chicken");
        GameObject[] loseProjectiles = GameObject.FindGameObjectsWithTag("projectile");

        foreach(GameObject obj in loseChickens)
            Destroy(obj);

        foreach (GameObject obj in loseProjectiles)
            Destroy(obj);

    }
}
