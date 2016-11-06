using System.Linq;
using UnityEngine;

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

	    // Initial spawn
	    Vector3 playerDifference = player.transform.position - transform.position;
	    float halfDistance = playerDifference.magnitude / 2f;
	    foreach(int index in Enumerable.Range(1, 6))
	    {
	        Vector3 spawnPos = transform.position +
	                           playerDifference.normalized * (halfDistance / 5f) * index;
	        SpawnMeteor(spawnPos);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time > nextMeteorLaunch)
	    {
            SpawnMeteor(transform.position);
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

    private void SpawnMeteor(Vector3 meteorSpawn)
    {
        Vector3 towardsPlayer = (player.transform.position - meteorSpawn).normalized;
        Vector3 spawnPos = meteorSpawn +
                           Vector3.Cross(towardsPlayer, Vector3.up) * Random.Range(-spawnRange, spawnRange) +
                           Vector3.Cross(towardsPlayer, Vector3.left) * Random.Range(-spawnRange, spawnRange);

        GameObject meteor = (GameObject) Instantiate(meteorTemplate, spawnPos, transform.rotation);
        meteor.GetComponent<Rigidbody>().velocity = (player.transform.position - meteor.transform.position).normalized * 150;
    }
}
