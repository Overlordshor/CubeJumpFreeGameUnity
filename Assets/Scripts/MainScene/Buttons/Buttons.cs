﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private float speed;
    private bool move = false;
    private RectTransform buttonRect;
    private new Animation animation;
    private new AudioSource audio;
    private ButtonSound sound;
    private SceneArrengement scene;
    private Image image;

    public void GoAway()
    {
        speed = 50f;
        move = true;
        StartCoroutine("DisableButtons");
    }

    private void Start()
    {
        buttonRect = GetComponent<RectTransform>();
        animation = GetComponent<Animation>();
        audio = GetComponentInParent<AudioSource>();
        scene = FindObjectOfType<SceneArrengement>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (move)
        {
            buttonRect.offsetMin -= new Vector2(0, speed);
        }
    }

    private void OnMouseUpAsButton()
    {
        audio.Play();

        if (!animation.isPlaying)
        {
            switch (gameObject.name)
            {
                case "SettingButton":
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
                    }
                    break;

                case "Sound":
                    sound = gameObject.GetComponent<ButtonSound>();
                    image.sprite = sound.GetSprite(scene.Sound);
                    sound.GetSound(!scene.Sound);
                    scene.Sound = !scene.Sound;
                    break;

                case "Prompt":
                    gameObject.GetComponent<ButtonPrompt>().GetPrompt();
                    break;

                case "ShopButton":
                    gameObject.transform.GetChild(0).gameObject.SetActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
                    break;

                case "CancelButton":
                    transform.parent.gameObject.SetActive(false);
                    break;

                case "AcceptButton":
                    gameObject.GetComponent<ButtonAccept>().SelectCube();
                    break;

                case "Language":
                    gameObject.GetComponent<ButtonLanguage>().GetLanguage();
                    break;

                case "AdvertisingButton":
                    print("SHOW ADVERTISING");
                    break;

                case "Restart":
                    gameObject.GetComponent<ButtonsEndGame>().Restart();
                    break;

                case "Return":
                    gameObject.GetComponent<ButtonsEndGame>().ReturnToMenu();
                    break;

                case "PlayButton":
                    scene.StartGame();
                    break;

                case "AcceptExitButton":
                    gameObject.GetComponent<AcceptExitButton>().Exit();
                    break;
            }
        }
    }

    private IEnumerator DisableButtons()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}