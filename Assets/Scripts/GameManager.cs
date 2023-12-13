using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RequirementsData;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public RequirementsData requirementsData;

    public DialogueManager dialogueManager;

    public bool dialogueTriggered = false;

    public GameObject endGameImage;
    public Image fadeOverlay;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager.ShowDialogue(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (requirementsData.CheckRequirement("Cash") && requirementsData.CheckRequirement("Fish"))
        {
            UpdateRequirementStatus("Cash and fish", true);
        }
        else
        {
            UpdateRequirementStatus("Cash and fish", false);
        }

        if (requirementsData.CheckRequirement("End") )
        {
            StartCoroutine(EndGameWithFade());
        }
    }

    public void UpdateRequirementStatus(string requirementName, bool status)
    {
        RequirementsData.Requirement requirement = requirementsData.requirements.Find(r => r.name == requirementName);
        if (requirement != null)
        {
            requirement.isMet = status;
        }
        else
        {
            Debug.LogWarning("Requirement not found: " + requirementName);
        }
    }

    IEnumerator EndGameWithFade()
    {
        // Fade in
        yield return StartCoroutine(FadeIn(fadeOverlay, 3.0f));

        endGameImage.SetActive(true);

        // Fade out
        yield return StartCoroutine(FadeOut(fadeOverlay, 2.0f));
    }

    IEnumerator FadeIn(Image image, float duration)
    {
        float elapsedTime = 0;
        Color color = image.color;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            image.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut(Image image, float duration)
    {
        float elapsedTime = 0;
        Color color = image.color;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / duration));
            image.color = color;
            yield return null;
        }
    }
}