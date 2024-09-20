using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HistorySystem : MonoBehaviour
{
    public static HistorySystem instance;
    [SerializeField] private Transform parentTranfrom;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) {
            instance = this;
        }
    }
    public Transform getparentTransform() {
        return parentTranfrom;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
