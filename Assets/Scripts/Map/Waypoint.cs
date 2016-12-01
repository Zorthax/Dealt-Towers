using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{

    public int orderNumber;

    void Start()
    {
        Destroy(GetComponent<Renderer>()); 
    }
}
