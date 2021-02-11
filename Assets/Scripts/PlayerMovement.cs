using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI carryText;
    public GameObject carryImage;
    public float moveSpeed = 3f;
    public bool isCarryingObject = false;
    public bool hasFoundGlasses = false;
    public Rigidbody2D rb;
    public Animator animator;
    public int inventory;
    public int inventorySource;
    public int selectedCell;
    public GameObject gameManager;
    Vector2 movement;




    void OnCollisionEnter2D(Collision2D collision){
      if(!isCarryingObject){
        if(collision.gameObject.transform.parent.gameObject.name == "ItemPickup") {
          GameObject.Find("ItemPickUpAudio").GetComponent<AudioSource>().Play();
          int.TryParse(collision.gameObject.tag, out selectedCell);
          Debug.Log(selectedCell);
          inventorySource = gameManager.GetComponent<game_manager>().randomCellSelector.IndexOf(selectedCell); // retrieves the random index position
          Debug.Log(inventorySource);

          inventory = gameManager.GetComponent<game_manager>().randomSpriteSelector[inventorySource]; //retrieves the Sprite (type) number at location
          switch(inventory) {
          case 1:
            carryText.text = "Floppy";
            break;
          case 2:
            carryText.text = "Glasses";
            break;
          case 3:
            carryText.text = "Mail";
            break;
          case 4:
            carryText.text = "Newspaper";
            break;
          case 5:
            carryText.text = "Watch";
            break;
          case 6:
            carryText.text = "Hat";
            break;
          case 7:
            carryText.text = "Book";
            break;
          case 8:
            carryText.text = "Shoes";
            break;
          case 9:
            carryText.text = "Keys";
            break;
          case 10:
            carryText.text = "Tech";
            break;
          case 11:
            carryText.text = "Guitar";
            break;
          case 12:
            carryText.text = "Ball";
            break;
          case 13:
            carryText.text = "Money";
            break;
          case 14:
            carryText.text = "Ring";
            break;
          case 15:
            carryText.text = "Magn. Glass";
            break;
          default:
            break;
          }
          carryImage.GetComponent<Image>().sprite = collision.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
          isCarryingObject = true;
          // carryImage.SetActive(true);
          carryText.gameObject.SetActive(true);
          // Debug.Log($"found {collision.gameObject.name}");
          // Debug.Log(carryImage);
          collision.gameObject.SetActive(false);
        }
      }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0.0f){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0.0f;
        }
        else if(Input.GetAxisRaw("Vertical") != 0.0f){
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = 0.0f;
        }
        else{
        movement.y = 0.0f;
        movement.x = 0.0f;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
