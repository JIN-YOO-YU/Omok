using Unity.Profiling;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    Animator animator;
    float nextX = 0;
    float nextY = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        nextX = Input.GetAxis("Horizontal");
        nextY = Input.GetAxis("Vertical");
 
        if(nextX == 0f && nextY == 0f){
            animator.SetInteger("AnimationState", 0);
        }
        else{
            animator.SetInteger("AnimationState", 1);
            transform.Translate((new Vector3(nextX, nextY, 0). normalized * Time.deltaTime) * moveSpeed);
        }
        // transform.position = new Vector3(nextX, nextY, 0).normalized;
        
    }
    
}
