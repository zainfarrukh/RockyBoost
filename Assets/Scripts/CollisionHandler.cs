using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;
    [SerializeField] AudioClip finish_sfx;
    [SerializeField] AudioClip death_sfx;
    [SerializeField] ParticleSystem finish_particle;
    [SerializeField] ParticleSystem death_particle;

    AudioSource audioSource;



    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        CheatDebug();

    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly!");
                    break;

                case "Finish":
                    StartFinishSequence();
                    break;

                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void StartCrashSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), waitTime);
        audioSource.PlayOneShot(death_sfx);
        death_particle.Play();
    }

    void StartFinishSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadLevel), waitTime);
        audioSource.PlayOneShot(finish_sfx);
        finish_particle.Play();
    }

    void LoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void CheatDebug()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
