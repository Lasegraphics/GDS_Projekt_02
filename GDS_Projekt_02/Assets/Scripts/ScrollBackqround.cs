using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScrollBackqround : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Material myMaterial;
    Vector2 offSet;
    public static AudioManagerMenu Instance = null;
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

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
