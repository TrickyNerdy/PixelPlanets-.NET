
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\GUI\SpritesheetPopup : ColorRect
{
	 
	@onready var w_frames = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer/WidthFrames");
	@onready var h_frames = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer/HeightFrames");
	@onready var export_button = GetNode("PopupFront/VBoxContainer/HBoxContainer/ExportButton");
	@onready var r_info = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer2/ResolutionInfo");
	@onready var f_info = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer2/FrameInfo");
	@onready var warning = GetNode("PopupFront/VBoxContainer/SpritesheetSettings/VBoxContainer2/WarningResolution");
	@onready var progressbar = GetNode("PopupFront/VBoxContainer/TextureProgressBar");
	public __TYPE pixels = 100;
	public __TYPE sheet_size = new Vector2(50,3);
	public __TYPE pixel_margin = 0.0;
	
	public void _on_CancelButton_pressed()
	{  
		visible = false;
	
	}
	
	public void _on_ExportButton_pressed()
	{  
		progressbar.visible = true;
		get_parent().export_spritesheet(sheet_size, progressbar, pixel_margin);
	
	}
	
	public void _on_HeightFrames_value_changed(__TYPE value)
	{  
		var val = (int)(value)
		export_button.disabled = false;
		
		if(val <= 0)
		{
			export_button.disabled = true;
		}
		else
		{
			sheet_size.y = val;
			_update_info();
	
		}
	}
	
	public void _on_WidthFrames_value_changed(__TYPE value)
	{  
		var val = (int)(value)
		export_button.disabled = false;
		
		if(val <= 0)
		{
			export_button.disabled = true;
		}
		else
		{
			sheet_size.x = val;
			_update_info();
	
		}
	}
	
	public void set_pixels(__TYPE p)
	{  
		
		progressbar.visible = false;
		pixels = p;
		_update_info();
	
	}
	
	public void _update_info()
	{  
		var x_size = sheet_size.x * (pixels + pixel_margin) + pixel_margin;
		var y_size = sheet_size.y * (pixels + pixel_margin) + pixel_margin;
		
		f_info.text = "Total Frames: %s" % (sheet_size.x*sheet_size.y);
		r_info.text = "Image resolution: \n%sx%s" % [x_size, y_size]
		
		warning.visible = false;
		export_button.disabled = false;
		
		if(x_size > 16384 || y_size > 16384) // max godot image resolution
		{
			warning.visible = true;
			export_button.disabled = true;
	
	
		}
	}
	
	public void _on_PixelMargin_value_changed(__TYPE value)
	{  
		pixel_margin = value;
		_update_info();
	
	
	}
	
	
	
}