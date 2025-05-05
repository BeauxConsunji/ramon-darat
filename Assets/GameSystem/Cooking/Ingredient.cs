using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Ingredient : MonoBehaviour
{
    public List<Transform> guidelines;
    public List<Sprite> sprites;
    public SpriteRenderer spriteRenderer;
    public int currentGuideline = 0;
    public bool done = false;
    public Minigame minigame; // drag the component in unity inspector
    public Transform guidelineContainer;
    // sound effects
    public AudioSource audioSource;

    void Start()
    {
        foreach (var guideline in guidelineContainer.GetComponentsInChildren<Guideline>()) {
            guidelines.Add(guideline.transform);
            guideline.gameObject.SetActive(false);
        }
        if (guidelines.Count > 0)
            guidelines[0].gameObject.SetActive(true);
        
        if (sprites.Count > 0)
            spriteRenderer.sprite = sprites[0];

        audioSource = GetComponent<AudioSource>();
        
    } 

    public void MarkGuidelineAsCompleted() {
        if (done) return;
        // play sound here
        
        audioSource.Play();
        if (currentGuideline + 1 >= guidelines.Count) {
            done = true;
            guidelines[currentGuideline].gameObject.SetActive(false);
            Debug.Log(guidelines.Count);
            spriteRenderer.sprite = sprites[guidelines.Count];
            if (minigame != null)
                minigame.MarkCompleted(gameObject);
            return;
        }
        guidelines[currentGuideline].gameObject.SetActive(false);
        spriteRenderer.sprite = sprites[currentGuideline];
        currentGuideline++;
        guidelines[currentGuideline].gameObject.SetActive(true);
        spriteRenderer.sprite = sprites[currentGuideline];
    }
}
