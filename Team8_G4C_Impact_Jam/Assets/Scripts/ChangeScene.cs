using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

    public string Level;
    public bool ChangeByCollision;

    public void TrocarScene(string i)
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(i);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && ChangeByCollision == true)
        {
            TrocarScene(Level);
        }
    }
}
