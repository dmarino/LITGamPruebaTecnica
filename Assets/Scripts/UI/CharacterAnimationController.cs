using UnityEngine;

    /*
        there are a lot of ways to structure the change of animation
        
        1) i can create 3 methods, one for each animation
        this can help designers to not look for the name of the animations in the animator if they don´t know how
        so its user friendly kinda
    
        public void Macarena()
        {
            _animator.Play("Macarena");
        }
    

        2) i can create a single method and pass the name of the animation to play
        this is more scalable because i don't have to create a method for each animation and easier to read
        also user friendly in a way because if someone creates a new animation it's easier to know the name
        than to know how to code a whole method
        also also this way i can use this sscript with more characters with different animations

        i think the decition dependes of the people i'm working with, the number of animations of the character, the number of characters
    */

[RequireComponent(typeof(Animator))]
public class CharacterAnimationController : MonoBehaviour
{
    //variable to store the animator
    private Animator _animator;

    void Start()
    {
        //here i'm just getting the animator
        _animator = gameObject.GetComponent<Animator>();
    }

    //Changes the animation of the character
    //animation: the name of the animation in the animator
    public void ChangeAnimation(string animation)
    {
        _animator.Play(animation);
    }

}
