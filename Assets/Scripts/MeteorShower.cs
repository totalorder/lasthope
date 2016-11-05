using UnityEngine;
using System.Collections;

public class MeteorShower : MonoBehaviour
{
    public GameObject meteorTemplate;
    public GameObject player;
    private float nextMeteorLaunch;
    private float launchRate = 1f / 0.5f;
    private float spawnRange = 650;

	// Use this for initialization
	void Start ()
	{
	    nextMeteorLaunch = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time > nextMeteorLaunch)
	    {
	        Vector3 towardsPlayer = (player.transform.position - transform.position).normalized;
	        Vector3 spawnPos = transform.position +
	                           Vector3.Cross(towardsPlayer, Vector3.up) * Random.Range(-spawnRange, spawnRange) +
	                           Vector3.Cross(towardsPlayer, Vector3.left) * Random.Range(-spawnRange, spawnRange);;

	        GameObject meteor = (GameObject)Instantiate(meteorTemplate, spawnPos, transform.rotation);
	        meteor.GetComponent<Rigidbody>().velocity = (player.transform.position - meteor.transform.position).normalized * 150;
	        nextMeteorLaunch = Time.time + launchRate;
	    }

	    if ( Input.GetMouseButtonDown(0)){
	        RaycastHit hit;
	        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
	        Debug.Log("Click");
	        if (Physics.Raycast(ray, out hit, 2000.0f)){
	            Debug.Log("Hit");
	            Debug.Log(hit.collider.transform.gameObject);
                Destroy(hit.collider.transform.gameObject);
	        }
	    }
	}
}
