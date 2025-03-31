using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float nextX = transform.position.x + (moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        float nextY = transform.position.y + (moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));

        transform.position = new Vector3(nextX, nextY, 0);
    }
}
