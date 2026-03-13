using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter : MonoBehaviour
{
    public UnityEvent onEnter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        onEnter.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
