using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreen;
    public static bool isGameActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        isGameActive = true;
        titleScreen.SetActive(false);
    }
}
