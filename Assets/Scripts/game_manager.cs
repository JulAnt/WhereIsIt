using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
  public TextMeshProUGUI  CounterText; 
  public Sprite[] Floppy;
  public Sprite[] Glasses;
  public Sprite[] Mail;
  public Sprite[] Newspaper;
  public Sprite[] Watches;
  public Sprite[] Hats;
  public Sprite[] Briefcases;
  public Sprite[] Shoes;
  public Sprite[] Keys;
  public Sprite[] Tech;
  public Sprite[] Guitars;
  public Sprite[] Balls;
  public Sprite[] Money;
  public Sprite[] Ring;
  public Sprite[] MagnifyGlass;
  public Sprite[] Book;
  public List<int> randomSpriteSelector;
  public List<int> randomCellSelector;
  public List<int> returnedItems;
  public GameObject ItemLocations;
  public GameObject carryUI;
  public GameObject DogUI;
  public GameObject WinScreen;
  public GameObject GameUI;

      

void SpriteSelector(int childLocation, int chosenSprite) {
  GameObject chosen = ItemLocations.transform.GetChild(childLocation).gameObject;
  chosen.gameObject.SetActive(true);
  chosen.transform.GetChild(0).gameObject.SetActive(true);
  SpriteRenderer spriteChild = chosen.GetComponentInChildren<SpriteRenderer>();
  switch(chosenSprite) {
  case 1:
    spriteChild.sprite = Floppy[(Random.Range(0, Floppy.Length -1 ))];
    break;
  case 2:
    spriteChild.sprite = Glasses[(Random.Range(0, Glasses.Length -1 ))];
    break;
  case 3:
    spriteChild.sprite = Mail[(Random.Range(0, Mail.Length -1 ))];
    break;
  case 4:
    spriteChild.sprite = Newspaper[(Random.Range(0, Newspaper.Length -1 ))];
    break;
  case 5:
    spriteChild.sprite = Watches[(Random.Range(0, Watches.Length -1 ))];
    break;
  case 6:
    spriteChild.sprite = Hats[(Random.Range(0, Hats.Length -1 ))];
    break;
  case 7:
    spriteChild.sprite = Book[(Random.Range(0, Book.Length -1 ))];
    break;
  case 8:
    spriteChild.sprite = Shoes[(Random.Range(0, Shoes.Length -1 ))];
    break;
  case 9:
    spriteChild.sprite = Keys[(Random.Range(0, Keys.Length -1 ))];
    break;
  case 10:
    spriteChild.sprite = Tech[(Random.Range(0, Tech.Length -1 ))];
    break;
  case 11:
    spriteChild.sprite = Guitars[(Random.Range(0, Guitars.Length -1 ))];
    break;
  case 12:
    spriteChild.sprite = Balls[(Random.Range(0, Balls.Length -1 ))];
    break;
  case 13:
    spriteChild.sprite = Money[(Random.Range(0, Money.Length -1 ))];
    break;
  case 14:
    spriteChild.sprite = Ring[(Random.Range(0, Ring.Length -1 ))];
    break;
  case 15:
    spriteChild.sprite = MagnifyGlass[(Random.Range(0, MagnifyGlass.Length -1 ))];
    break;
  default:
  // spriteChild.sprite = Briefcases[(Random.Range(0, Briefcases.Length -1 ))];
    break;
}
  chosen.gameObject.SetActive(true);

}

    // Start is called before the first frame update
    void Awake() {
      ItemLocations = GameObject.Find("ItemPickup");
      while (randomSpriteSelector.Count < 6) {
        int selected = Random.Range(1, 15);
        if(!randomSpriteSelector.Contains(selected)){
            randomSpriteSelector.Add(selected);
        }
      }
       while (randomCellSelector.Count < 6) {
        int selected = Random.Range(0, 24);
        if(!randomCellSelector.Contains(selected)){
            randomCellSelector.Add(selected);
        }
      }
      for(int i = 0; i<6 ; i++){
        SpriteSelector(randomCellSelector[i],randomSpriteSelector[i]);
      }

    }

void OnCollisionEnter2D(Collision2D collision){
      if(collision.gameObject.name == "Player" && collision.gameObject.GetComponent<PlayerMovement>().isCarryingObject == true) {
        collision.gameObject.GetComponent<PlayerMovement>().isCarryingObject = false;
        carryUI.SetActive(false);
                        GameObject.Find("ItemDepositAudio").GetComponent<AudioSource>().Play();
        StopAllCoroutines();
        DogUI.SetActive(false);
        returnedItems.Add(collision.gameObject.GetComponent<PlayerMovement>().inventory);
        int removeMe = collision.gameObject.GetComponent<PlayerMovement>().selectedCell;
        randomSpriteSelector.Remove(collision.gameObject.GetComponent<PlayerMovement>().inventory);
        randomCellSelector.Remove(removeMe);
        // Debug.Log(removeMe);
        StartCoroutine(DogCounter());
      Debug.Log("item deposited");
      }
      
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")){
        SceneManager.LoadScene("MainMenu");
        }
      CounterText.text = ($"{returnedItems.Count} / 6");
      if(returnedItems.Count == 6) {
        StopAllCoroutines();
        GameUI.SetActive(false);
        WinScreen.SetActive(true);
      }
    }

    IEnumerator DogCounter() {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        bool added = false;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(Random.Range(5,9));
        DogUI.SetActive(true);
        // GameObject.Find("DogHeadBark1").transform.localPosition = new Vector2(-505,-253);
        GameObject.Find("DogBark").GetComponent<AudioSource>().Play();
        int newLocation = -1;
        int newItem = -1;
        do {
        newLocation = Random.Range(0, 24);
        if(!randomCellSelector.Contains(newLocation)){
            randomCellSelector.Add(newLocation);
            added = true;
          }
        } while (added == false);
        newItem = returnedItems[Random.Range(0, returnedItems.Count)];
        randomSpriteSelector.Add(newItem);
        returnedItems.Remove(newItem);
        SpriteSelector(newLocation,newItem);
        yield return new WaitForSeconds(1);
        DogUI.SetActive(false);
        // GameObject.Find("DogHeadBark1").transform.localPosition = new Vector2(-873,-679);
        if(returnedItems.Count > 0) {
        StartCoroutine(DogCounter());
        }
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
