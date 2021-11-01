using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour
{
   [System.Serializable]
   public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timebetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    public bool finishedSpawing;

    public Animator waveTextAnim;
    public Text waveText;

    public AudioClip gameoverVoice, gameover, fightVoiceClip;
    public AudioSource audioSource, bgAudioSource, gameoverAudioSource;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));

    }

    private void Update()
    {
        if (finishedSpawing == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawing = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                waveText.text = "GAME OVER";
                waveTextAnim.Play("FadeOut");
                Debug.Log("GameOver");

                gameoverAudioSource.PlayOneShot(gameoverVoice,.7f);
                bgAudioSource.Stop();
                bgAudioSource.clip = gameover;
                bgAudioSource.Play();
            }
        }
    }
    IEnumerator StartNextWave(int index)
    {
        int waveLevel = index + 1;
        waveText.text = "WAVE" + waveLevel;
        waveTextAnim.Play("FadeOut");

        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];
        gameoverAudioSource.PlayOneShot(fightVoiceClip, 1);

        for (int i =0; i < currentWave.count; i++)
        {
            if(player == null)
            {
                yield break;
            }

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if(i == currentWave.count - 1)
            {
                finishedSpawing = true;
            }
            else
             {
                finishedSpawing = false;
            }
            yield return new WaitForSeconds(currentWave.timebetweenSpawns);
        }
    }

  
}
