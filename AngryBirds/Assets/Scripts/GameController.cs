using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    public BoxCollider2D TapCollider;
    public string levelName;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _statusInfo;

    private bool _isGameEnded = false;

    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];

    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        // If all enemies are dead, game ended = win
        if (_isGameEnded)
        {
            string sceneName = SceneManager.GetActiveScene().name;  // Level1 / Level2 / Level3 etc.[web:71][web:69]

            if (sceneName == "Level2")
            {
                _statusInfo.text = "You Win! Next Level?";
            }
            else
            {
                _statusInfo.text = "You Win!";
            }

            _panel.gameObject.SetActive(true);
        }

        // Remove the used bird
        Birds.RemoveAt(0);

        // If no birds left but enemies still alive -> lose
        if (Birds.Count == 0 && Enemies.Count > 0)
        {
            _statusInfo.text = "You Lose!";
            _panel.gameObject.SetActive(true);
        }

        // If there are still birds, load the next one into the slingshot
        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
        else
        {
            _isGameEnded = true;
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}
