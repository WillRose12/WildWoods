using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float moveSpeed = 6f;

	Rigidbody2D rb;

	Player target;
	Vector2 moveDirection;
    
    [SerializeField]
    int numberOfProjectiles;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    private int Damage;
  
    Vector2 startPoint;

    float raduis, movespeed;




    // Use this for initialization
    void Start () {
        raduis = 5f;
        moveSpeed = 5f;
		rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindObjectOfType<Player>();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        StartCoroutine(ExecuteAfterTime(2));
        Destroy(gameObject, 10f);
       
		
	}

    // Code to execute after the delay
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;
        startPoint = gameObject.transform.position;
        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180 * raduis);
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180 * raduis);
            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;
            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            angle += angleStep;

        }
        

    }

    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.name.Equals ("Player")) {
			Debug.Log ("hit");
            Player p = col.gameObject.GetComponent<Player>();
            p.TakeDamage(Damage);
            Destroy (gameObject);
		}
        

    }

    void Spread()
    {

    }
    
}
