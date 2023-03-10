using Godot;
using System;

public class Character : KinematicBody
{
	
	public StateMachine machine = new StateMachine(); 
	public State stateIdle,
		stateMove,
		stateJumpStart,
		stateJump,
		stateFall,
		stateLand;

	[Export]
	public float speed = 6,
		acceleration = 15, 
		jumpHeight = 2.25f,
		maxSlopeAngle = 40,
		landTime = 0.1f,
		jumpStartTime = 0.1f,
		stepDistance = 0.75f,
		cameraFovJump = 100,
		cameraFovLand = 80;
	[Export]
	NodePath cameraControllerPath,
		feetAudioPath;
	[Export]
	public AudioStream[] footstepSounds;
	[Export]
	public AudioStream jumpSound,
		landSound;

	public Vector3 gravityUnitVector;
	public float gravityMagnitude,
		maxSlopeAngleRad,
		ySpeed;
	public Vector3 velocity,
		snap = Vector3.Down;
	public CameraControllerSimple cameraController;
	public Disconnector jumpDisconnector = new Disconnector();
	public AudioTools3d feetAudio;

	//string debugText;



	public override void _Ready()
	{
		// get gravity
		gravityUnitVector = (Vector3) ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
		gravityMagnitude = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");

		snap = gravityUnitVector * 3;
		
		// calculate angles in radians
		maxSlopeAngleRad = Mathf.Pi / 180f * maxSlopeAngle;

		// get nodes
		cameraController = GetNode<CameraControllerSimple>(cameraControllerPath);
		feetAudio = GetNode<AudioTools3d>(feetAudioPath);

		// initialize states
		stateIdle = new CharacterStateIdle(){blackboard = this};
		stateMove = new CharacterStateMove(){blackboard = this};
		stateJumpStart = new CharacterStateJumpStart(){blackboard = this};
		stateJump = new CharacterStateJump(){blackboard = this};
		stateFall = new CharacterStateFall(){blackboard = this};
		stateLand = new CharacterStateLand(){blackboard = this};

		// set first state in machine
		machine.SetState(stateIdle);
	}



	public override void _PhysicsProcess(float delta)
	{
		// run machine
		if(machine != null && machine.CurrentState != null)
		{
			machine.CurrentState.RunState(delta);
			machine.SetState(machine.CurrentState.Transition());
		}

		// debug
		// if(debugText != machine.CurrentState.ToString())
		// {
		// 	GD.Print(machine.CurrentState.ToString());
		// 	debugText = machine.CurrentState.ToString();
		// }   
		//GD.Print(velocity.Length());
		//GD.Print(jumpStartY + " : " + fallStartY); 
	}
}
