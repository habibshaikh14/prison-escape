using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField] float scrollSpeed = 0.2f;
    private bool waterLevelIncrease = false;

    // Update is called once per frame
    void Update()
    {
        if (waterLevelIncrease)
        {
            IncreaseWaterLevel();
        }
    }

    private void IncreaseWaterLevel()
    {
        Vector2 newWaterLevel = new Vector2(0f, scrollSpeed * Time.deltaTime);
        transform.Translate(newWaterLevel);
    }

    public void StartWaterLevelIncrease()
    {
        waterLevelIncrease = true;
    }
}
