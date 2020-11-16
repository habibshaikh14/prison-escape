using UnityEngine;

public class WaterLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            FindObjectOfType<VerticalScroll>().StartWaterLevelIncrease();
            Destroy(gameObject);
        }
    }
}
