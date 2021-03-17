using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  public GameObject prefab;
  private Vector3 spawnPos = new Vector3(15f, 0, 0);

  // Start is called before the first frame update
  void Start()
  {
    InvokeRepeating("SpawnObstacle", 1.5f, 1.5f);
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  void SpawnObstacle()
  {
    Instantiate(prefab, spawnPos, prefab.transform.rotation);
  }
}
