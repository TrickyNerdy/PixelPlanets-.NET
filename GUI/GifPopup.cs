
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class GifPopup : ColorRect
{

	public Node set_delay = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer/FrameDelay");
	public Node export_button = GetNode("PopupFront/VBoxContainer/HBoxContainer/ExportButton");
	public Node progressbar = GetNode("PopupFront/VBoxContainer/TextureProgressBar");
	public Node set_framecount = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer/GifFrameCount");
	public Node set_giftime = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer/GifTime");
	public Node set_delay = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer/FrameDelay");
	public int frames = 600;
	public int length = 10;
	public float frame_delay = 10.0f / 600.0;
	
	
	public void _on_CancelButton_pressed()
	{  
		visible = false;
		EmitSignal("cancel_gif");
	
	}
	
	public void _on_ExportButton_pressed()
	{  
		progressbar.visible = true;
		get_parent().export_gif(frames, frame_delay, progressbar);
	
	}
	
	public void _on_FrameDelay_value_changed(__TYPE value)
	{  
		frame_delay = value;
		length = frames * frame_delay;
		
		set_giftime.disconnect("value_changed", Callable(this, "_on_GifTime_value_changed"));
		set_giftime.value = length;
		set_giftime.connect("value_changed", Callable(this, "_on_GifTime_value_changed"));
	
	}
	
	public void _on_GifTime_value_changed(__TYPE value)
	{  
		length = value;
		frame_delay = length/frames;
		
		set_delay.disconnect("value_changed", Callable(this, "_on_FrameDelay_value_changed"));
		set_delay.value = frame_delay;
		set_delay.connect("value_changed", Callable(this, "_on_FrameDelay_value_changed"));
	
	}
	
	public void _on_GifFrameCount_value_changed(__TYPE value)
	{  
		frames = value;
		frame_delay = length/frames;
		
		set_delay.disconnect("value_changed", Callable(this, "_on_FrameDelay_value_changed"));
		set_delay.value = frame_delay;
		set_delay.connect("value_changed", Callable(this, "_on_FrameDelay_value_changed"));
	
	
	}
	
	
	
}