using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SpawnLocator
{

    Vector3 ComputeSpawnLocation(float playerAdvance);

    GameObject GetPrefab();

}
