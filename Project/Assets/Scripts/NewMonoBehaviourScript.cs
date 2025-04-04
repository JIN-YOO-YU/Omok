using Unity.Profiling;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Animator animator;
    private float nextX = 0;
    private float nextY = 0;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        nextX = Input.GetAxis("Horizontal");
        nextY = Input.GetAxis("Vertical");
 
        if(nextX == 0f && nextY == 0f){
            animator.SetInteger("AnimationState", 0);
        }
        else{
            animator.SetInteger("AnimationState", 1);
            transform.Translate(new Vector3(nextX, nextY, 0).normalized * Time.deltaTime * moveSpeed);
        }  
    }
    
}
