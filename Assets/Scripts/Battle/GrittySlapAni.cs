using UnityEngine;
using System.Collections;

public class GrittySlapAni : MonoBehaviour {

    public GameObject player1;
    public GameObject enemy;
    public Animator animPlayer;
    float distTrav;
    public Vector3 initPos;
    


	// Use this for initialization
	void Start () {
        distTrav = 0.0f;
        initPos = player1.transform.position;
        animPlayer = GetComponent<Animator>();
        int attackHash = Animator.StringToHash("Attack(1)");
        int walkHash = Animator.StringToHash("Walking");
	}
	
	// Update is called once per frame
	void Update () {
        if (BattleCommands.runGrittySlap)
        {
            //StartCoroutine(this.runGritty());
            //this.runGritty();
            animPlayer.Play("Walk");
            if  (distTrav < 5)
            {
                player1.transform.Translate(new Vector3(0.0f, 0.0f, 2*Time.deltaTime));
                distTrav += 2*Time.deltaTime;
            }
            else
            {
                //anim.ResetTrigger("Walking");
                animPlayer.Play("Attack(1)");
                //BattleCommands.runGrittySlap = false;
                StartCoroutine(goBack());
                //player1.transform.position = initPos;
            }
        }
	}
    IEnumerator goBack()
    {
        yield return new WaitForSeconds(3.0f);
        player1.transform.position = initPos;
    }

    void runGritty()
    {
        System.Console.WriteLine("In gritty slap");
        float distTrav = 0.0f;
        Vector3 orig = player1.transform.position;
        /* while (timeElapsed < 2)
         {
             //float moving = Time.deltaTime;
             distTrav += 1;
             player1.transform.Translate(new Vector3(distTrav*.1f, 0.0f, 0.0f));
             //distTrav += Time.deltaTime;
             yield return new WaitForSeconds(.01f);
             Debug.Log("Dist traveled: " + distTrav);
             //timeElapsed += Time.deltaTime;
         } */
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        player1.transform.Translate(new Vector3(Time.deltaTime, 0.0f, 0.0f));
        //controller.SimpleMove(forward);
        Debug.Log("Out of loop");
        //player1.transform.position = orig;
        BattleCommands.runGrittySlap = false;
    }
}
