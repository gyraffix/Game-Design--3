using UnityEngine;

public class MoveWIthObject : MonoBehaviour
{
    public GameObject following;
    private Vector3 offset;
    private float height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - following.transform.position;
        height = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + following.transform.position;
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
}
