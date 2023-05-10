/* ======================================================================================== */
/* FMOD Studio - Unity Integration Demo.                                                    */
/* Firelight Technologies Pty, Ltd. 2012-2016.                                              */
/* Liam de Koster-Kjaer                                                                     */
/*                                                                                          */
/* Use this script in conjunction with the Viking Village scene tutorial and Unity 5.4.     */
/* http://www.fmod.org/training/                                                            */
/*                                                                                          */
/* 1. Import Viking Village asset package                                                   */
/* 2. Import FMOD Studio Unity Integration package                                          */
/* 3. Replace Audio listener with FMOD Studio listener on player controller                 */
/*   (FlyingRigidBodyFPSController_HighQuality)                                             */
/* 4. Add footsteps script to the player controller                                         */
/* 5. Set footsteps script variable ‘Step Distance’ to a reasonable value (2.0f)            */
/* 6. Change terrain texture import settings so we can sample pixel values                  */
/*     - terrain_01_m                                                                       */
/*     - terrain_wetmud_01_sg                                                               */
/*         - Texture Type: Advanced                                                         */
/*         - Non Power of 2: N/A                                                            */
/*         - Mapping: None                                                                  */
/*         - Convolution Type: N/A                                                          */
/*         - Fixup Edge Seams: N/A                                                          */
/*         - Read/Write Enabled: Yes                                                        */
/*         - Import Type: Default                                                           */
/*         - Alpha from Greyscale: No                                                       */
/*         - Alpha is Transparency: No                                                      */
/*         - Bypass sRGB sampling: No                                                       */
/*         - Encode as RGBM: Off                                                            */
/*         - Sprite Mode: None                                                              */
/*         - Generate Mip Maps: No                                                          */
/*         - Wrap Mode: Repeat                                                              */
/*         - Filter Mode: Bilinear                                                          */
/*         - Aniso Level: 3                                                                 */
/* ======================================================================================== */


using UnityEngine;
using System.Collections;

//This script plays footstep sounds.
//It will play a footstep sound after a set amount of distance travelled.
//When playing a footstep sound, this script will cast a ray downwards. 
//If that ray hits the ground terrain mesh, it will retreive material values to determine the surface at the current position.
//If that ray does not hit the ground terrain mesh, we assume it has hit a wooden prop and set the surface values for wood.
public class Footsteps2 : MonoBehaviour
{
	//FMOD Studio variables
	//The FMOD Studio Event path.
	//This script is designed for use with an event that has a game parameter for each of the surface variables, but it will still compile and run if they are not present.
	[SerializeField] FMODUnity.EventReference eventName;
	

	//Surface variables
	//Range: 0.0f - 1.0f
	//These values represent the amount of each type of surface found when raycasting to the ground.
	//They are exposed to the UI (public) only to make it easy to see the values as the player moves through the scene.
	public float m_Forest;
	public float m_Grass;
	public float m_Snow;
	

	//Step variables
	//These variables are used to control when the player executes a footstep.
	//This is very basic, and simply executes a footstep based on distance travelled.
	//Ideally, in this case, footsteps would be triggered based on the headbob script. Or if there was an animated player model it could be triggered from the animation system.
	//You could also add variation based on speed travelled, and whether the player is running or walking. 
	public float m_StepDistance = 2.0f;
	float m_StepRand;
	Vector3 m_PrevPos;
	float m_DistanceTravelled;

	

	void Start()
	{
		//Initialise random, set seed
		Random.InitState(System.DateTime.Now.Second);

		//Initialise member variables
		m_StepRand = Random.Range(0.0f, 0.5f);
		m_PrevPos = transform.position;
		
	}

	void Update()
	{
		m_DistanceTravelled += (transform.position - m_PrevPos).magnitude;
		if (m_DistanceTravelled >= m_StepDistance + m_StepRand)//TODO: Play footstep sound based on position from headbob script
		{
			PlayFootstepSound();
			m_StepRand = Random.Range(0.0f, 0.5f);//Adding subtle random variation to the distance required before a step is taken - Re-randomise after each step.
			m_DistanceTravelled = 0.0f;
		}

		m_PrevPos = transform.position;

		
	}

	void PlayFootstepSound()
	{
		//Defaults
		m_Grass = 0.0f;
		m_Snow = 0.0f;		
		m_Forest = 1.0f;

		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000.0f))		{
			

			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
			{
				Debug.Log("TAG " + hit.collider.tag);

				if(hit.collider.tag == "Grass")
                {
					m_Grass = 1.0f;
					m_Snow = 0.0f;
					m_Forest = 0.0f;
				}
				if (hit.collider.tag == "Snow")
				{
					m_Grass = 0.0f;
					m_Snow = 1.0f;
					m_Forest = 0.0f;
				}
				if (hit.collider.tag == "Forest")
				{
					m_Grass = 0.0f;
					m_Snow = 0.0f;
					m_Forest = 1.0f;
				}

			}
			else
			{
				m_Grass = 0.0f;
				m_Snow = 0.0f;				
				m_Forest = 1.0f;
			}
		}
		
		
		FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(eventName);
		e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

		SetParameter(e, "Forest2", m_Forest);
		SetParameter(e, "Snow2", m_Snow);			
		SetParameter(e, "Grass2", m_Grass);

		e.start();
		e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
		
	}

	void SetParameter(FMOD.Studio.EventInstance e, string name, float value)
	{
		e.setParameterByName(name, value);
	}

	
}
