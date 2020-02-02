using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;



    int currentSceneIndex;
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

    public IEnumerator LoadNextFromSplash()
    {
        if(currentSceneIndex == 0)
        {
            yield return new WaitForSeconds(loadDelay);
            SceneManager.LoadScene(currentSceneIndex += 1);
        }
    }
}
