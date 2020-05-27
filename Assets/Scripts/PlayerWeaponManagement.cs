
using UnityEngine;

public class PlayerWeaponManagement : MonoBehaviour
{

    public GameObject Gun1;
    public bool hasGun = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGun)
        {
            if (Gun1.activeInHierarchy)
            {

            }
            else{
                Gun1.SetActive(true);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gun")
        {
            hasGun = true;
        }
    }
}
