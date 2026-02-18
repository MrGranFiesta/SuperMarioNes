using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePooling : MonoBehaviour
{
    private int _pollSize = 3;
    private List<GameObject> _listFired = new List<GameObject>();

    private static FirePooling _instance;
    public static FirePooling Instance { get { return _instance; } }

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InstanciateList(_pollSize);
    }

    private void InstanciateList(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject fire = Instantiate(ResourceManager.GetFired());
            fire.SetActive(false);
            _listFired.Add(fire);
            fire.transform.parent = transform;
        }
    }

    public GameObject GetFireAndCreate(
        Vector3 position,
        int direction
        ) {
        for (int i = 0; i < _listFired.Count; i++) {
            GameObject fire = _listFired[i];
            if (!fire.activeSelf) {
                fire.SetActive(true);
                fire.transform.position = position;
                fire.GetComponent<FireController>().SetDirectionX(direction);
                //TODO Conf Direction
                return fire;
            }
        }
        InstanciateList(1);
        GameObject lastFire = _listFired[_listFired.Count -1];
        lastFire.SetActive(true);
        lastFire.transform.position = position;
        //TODO Conf Direction
        return lastFire;
    }
}
