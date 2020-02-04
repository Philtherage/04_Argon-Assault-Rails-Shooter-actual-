using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;
    [SerializeField] float restartLevelDelay = 1f;


    int currentSceneIndex;

    private void Awake()
    {
        int currentLevelLoaders = FindObjectsOfType<LevelLoader>().Length;
        if(currentLevelLoaders > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {  
        StartCoroutine(LoadNextFromSplash());  
    }

    private IEnumerator LoadNextFromSplash()
    {
        if (currentSceneIndex == 0)
        {
            yield return new WaitForSeconds(loadDelay);
            if(currentSceneIndex > 0) { yield break; }
            SceneManager.LoadScene(currentSceneIndex += 1);           
        }
    }

    private IEnumerator RestartLevelLoadDelay()
    {
        yield return new WaitForSeconds(restartLevelDelay);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartLevelLoadDelay());
    }
}
