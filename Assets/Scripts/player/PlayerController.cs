﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private UI_Inventory uiInventory;
    
    public static float CurrHealth;
    public float Healthup = 0;
    public float Dmg = 10;
    public float firerateup = 0;
    public float Speedup = 0;

    //public float MaxSpeed = 1.5f;

    public Rigidbody2D rb;
    public Camera cam;

    private Inventory inventory;

    Vector2 movement;
    Vector2 mousePos;

    void Start()
    {
        //sets health at scene start
        MaxHealth();

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        inventory.GetItemList();


    }

    
    // Update is called once per frame
    void Update()
    {
        //MaxHealth = MaxHealth + inventory.FindItemHeal() * 6.5;
        /*
        if(CurrHealth > MaxHealth())
        {
            CurrHealth = MaxHealth();
        }
        */

        
        movement.x = Input.GetAxisRaw("Horizontal");

        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

       
        if (CurrHealth < 0)
        {
            Debug.Log("you lose");
        }
        //facemouse();
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * MaxSpeed() * Time.fixedDeltaTime);

        Vector2 look = mousePos - rb.position;

        float angle = Mathf.Atan2(look.y, look.x)* Mathf.Rad2Deg -90f;

        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld)
        {
            
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestorySelf();
            Debug.Log("collected");
        }
    }

    
    public float MaxHealth()
    {
        float maxHealth = 100;
        

        maxHealth = maxHealth + Healthup * 6;
        Debug.Log($"You have {maxHealth.ToString()}");

        return maxHealth;
    }

    public float MaxSpeed()
    {
        float maxspeed = 1.5f;
        

        maxspeed = maxspeed + Speedup / 2;
        
        return maxspeed;
    }

public static float Damage()
    {
        int damage = 10;
        

        return damage;
    }


    /* void facemouse()
     {
         Vector3 mousePosition = Input.mousePosition;
         mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

         Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
         transform.up = direction;
     }*/
}
