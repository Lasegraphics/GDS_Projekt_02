using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScrollBackqround : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [HideInInspector] private Material myMaterial;
    [HideInInspector] private Vector2 offSet;
    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(speed, 0);
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
