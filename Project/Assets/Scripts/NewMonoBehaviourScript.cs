using Unity.Profiling;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float nextX = (Input.GetAxis("Horizontal"));
        float nextY = (Input.GetAxis("Vertical"));

        if(Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f){
            animator.SetInteger("AnimationState", 0);
        }
        else{
            animator.SetInteger("AnimationState", 1);
        }
        // transform.position = new Vector3(nextX, nextY, 0).normalized;
        transform.Translate((new Vector3(nextX, nextY, 0). normalized * Time.deltaTime) * moveSpeed);
    }
    
}
