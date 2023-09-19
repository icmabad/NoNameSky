using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider slider;
     public Gradient gradient;
     public Image fill;
     //public int health=100;
     GameObject g;


     void start(){
     	g.SetActive(false);
     }

     void update(){
     }

     public void SetMaxHealth(int health)
     {
         //slider.maxValue = health;
         slider.value = health;

         fill.color = gradient.Evaluate(1f);
     }

     public void SetHealth(int h)
     {

         slider.value = h;
         fill.color = gradient.Evaluate(slider.normalizedValue);
     }
}
