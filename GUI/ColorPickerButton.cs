
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\GUI\ColorPickerButton : Button
{
	 
	public signal color_picked;
	public signal on_selected;
	
	public __TYPE index = 0;
	public __TYPE own_color;
	public __TYPE is_active = false;
	
	public void _ready()
	{  
		
		add_theme_stylebox_override("normal", get_theme_stylebox("normal"));
		add_theme_stylebox_override("hover", get_theme_stylebox("hover").duplicate());
		add_theme_stylebox_override("pressed", get_theme_stylebox("pressed").duplicate());
	
	}
	
	public void set_index(__TYPE i)
	{  
		index = i;
	
	}
	
	public void set_color(__TYPE color)
	{  
		own_color = color;
		get_theme_stylebox("normal").bg_color = color;
		get_theme_stylebox("normal").bg_color = color;
		get_theme_stylebox("pressed").bg_color = color;
	
	}
	
	public void _on_picker_color_changed(__TYPE color)
	{  
		if(is_active)
		{
			set_color(color);
			EmitSignal("color_picked", color, index);
	
		}
	}
	
	public void _on_ColorPickerButton_pressed()
	{  
		is_active = true;
		EmitSignal("on_selected", this);
	
	
	}
	
	
	
}