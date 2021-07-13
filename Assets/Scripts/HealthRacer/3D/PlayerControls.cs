using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(BoxCollider))]
public class PlayerControls : MonoBehaviour
{
    [Header(header: "Graphics")]
    [Tooltip("Oyuncunun NavMeshAgent nesnesi")]
    public NavMeshAgent cart;
    [Tooltip("Oyuncunun Animator nesnesi")]
    public Animator animator;

    [Header(header: "Current State")]
    [Tooltip("Oyuncu Hareket Edebilir mi?")]
    public bool canPlayerMove = true;
    [Tooltip("Oyuncunun sepete eklediği ürün Listesi(Food)")]
    public List<Food> cartContent = new List<Food>();
    [Tooltip("Oyuncunun hareket etmesi için gerekli minimum mesafe")]
    [Range(4f, 20f)]
    public float foodPickingRange = 10;


    [Header(header: "Sound Effects")]
    [Tooltip("Hareket etme sesleri")]
    public AudioSource[] audioSources = new AudioSource[3];



    GameObject target;
    GameObject foodToPickup;
    Vector3 cartDestination;

    private void Start()
    {
        if (cart == null) cart = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (!canPlayerMove)
        {
            animator.SetFloat("MoveSpeed", 0);
            cart.SetDestination(transform.position);
            return;
        }
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(r, out RaycastHit hit))
        {
            cartDestination = hit.point;
            target = hit.transform.gameObject;

            if (foodToPickup && isCloseEnough(foodToPickup.transform.position))
            {
                cartContent.Add(foodToPickup.GetComponentInChildren<UIDisplay>().food);
                foodToPickup.SetActive(false);
                foodToPickup = null;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (target.CompareTag("FoodObject"))
                    foodToPickup = target;
                if (target.transform != transform.root && target.transform.parent.CompareTag("FoodObject"))
                    foodToPickup = target.transform.parent.gameObject;
                if (target.transform != transform.root && target.transform.parent != transform.root && target.transform.parent.parent.CompareTag("FoodObject"))
                    foodToPickup = target.transform.parent.parent.gameObject;


                if (foodToPickup)
                {
                    Vector3 newTarget1 = target.transform.position + Vector3.left * 3;
                    Vector3 newTarget2 = target.transform.position + Vector3.right * 3;
                    if ((transform.position - newTarget1).sqrMagnitude < (transform.position - newTarget2).sqrMagnitude)
                        cart.SetDestination(newTarget1);
                    else
                        cart.SetDestination(newTarget2);
                }
                else if (!isCloseEnough(cartDestination) && target.CompareTag("Ground"))
                {
                    cart.SetDestination(hit.point);
                }
            }


            if (cart.velocity.magnitude > 0)
            {
                animator.SetFloat("MoveSpeed", 1);
            }
            else
            {
                animator.SetFloat("MoveSpeed", 0);
            }
        }
    }
    bool isCloseEnough(Vector3 targetposition)
    {
        return (targetposition.toFloor() - transform.position).sqrMagnitude < foodPickingRange * foodPickingRange;
    }

}
