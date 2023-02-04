using Godot;
using System;
using System.Collections.Generic;

public class TaskList : Node
{
	
	[Export]
	NodePath taskListOpenPath,
		taskTemplatePath;
	[Export]
	string[] taskDescriptions;

	Control taskListOpen,
		taskListContainer;
	Disconnector discon = new Disconnector();
	List<CheckBox> tasks = new List<CheckBox>();


	
	public override void _Ready()
	{
		// get nodes
		taskListOpen = GetNode<Control>(taskListOpenPath);
		var taskTemplate = GetNode<CheckBox>(taskTemplatePath);
		taskListContainer = (Control) taskTemplate.GetParent();

		// populate task list
		foreach(var s in taskDescriptions)
		{
			var newTask = (CheckBox) taskTemplate.Duplicate();
			newTask.Text = s;
			tasks.Add(newTask);
			taskListContainer.AddChild(newTask);
			// newTask.MarginBottom = taskTemplate.MarginBottom;
			// newTask.MarginTop = taskTemplate.MarginTop;
			// newTask.MarginLeft = taskTemplate.MarginLeft;
			// newTask.MarginRight = taskTemplate.MarginRight;
		}

		// clear template task
		taskTemplate.QueueFree();
	}



	public override void _Process(float delta)
	{
		if(Engine.TimeScale == 0)
		{
			return;
		}


		if(discon.Trip(PlayerInput.objective))
		{
			taskListOpen.Visible = !taskListOpen.Visible;
		}
	}



	public void CompleteTask(int index)
	{
		tasks[index].Pressed = true;
	}
}
