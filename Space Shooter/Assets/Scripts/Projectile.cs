using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;

    public GameObject explosionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * amtToMove);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //update game ui
            Player.score += 100;
            Player.UpdadeStats();
            if(Player.score >= 1000)
            {
                SceneManager.LoadScene("Win");
            }

            //Play destroy animation
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            enemy.SetPositionAndSpeed();
            enemy.minSpeed += 0.5f;
            enemy.maxSpeed += 1f;


            Destroy(gameObject);
        }
    }

}
