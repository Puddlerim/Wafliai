using UnityEngine;

public class InspectSystem : MonoBehaviour
{
    public Transform objectToInspect;

    public float roatationSpeed = 100f;

    private Vector3 previousMousePosition;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - previousMousePosition;
            objectToInspect.Rotate(Vector3.up, -delta.x * roatationSpeed * Time.deltaTime, Space.World);
            objectToInspect.Rotate(Vector3.right, delta.y * roatationSpeed * Time.deltaTime, Space.World);
            previousMousePosition = Input.mousePosition;
        }
    }
}
