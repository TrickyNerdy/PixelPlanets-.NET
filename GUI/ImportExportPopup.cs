
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\GUI\ImportExportPopup : ColorRect
{
	 
	public signal set_colors;
	
	@onready var textedit = GetNode("PopupFront/HBoxContainer/VBoxContainer/TextEdit");
	@onready var apply_button = GetNode("PopupFront/HBoxContainer/VBoxContainer2/ApplyColors");
	
	public __TYPE current_colors = new Array(){};
	
	public void set_current_colors(__TYPE colors)
	{  
		current_colors = colors;
		textedit.text = "";
		int index = 0;
		foreach(var c in colors)
		{
			textedit.text += "#" + c.to_html(false);
			if(index < colors.size() - 1)
			{
				textedit.text += "\n";
			}
			index += 1;
	
		}
	}
	
	public void show_popup()
	{  
		visible = true;
	
	}
	
	public void _on_CloseButton_pressed()
	{  
		visible = false;
	
	}
	
	public void _on_CopyToClipboard_pressed()
	{  
		DisplayServer.clipboard_set(textedit.text);
	
	}
	
	public void _on_PasteFromClipboard_pressed()
	{  
		textedit.text = DisplayServer.clipboard_get();
	
	}
	
	public __TYPE _convert_to_colors()
	{  
		var text = textedit.text.replace(",", "").split("\n");
		Array colors = new Array(){};
		foreach(var t in text)
		{
			t = t.replace(",", "");
			colors.append(new Color(t));
		
		}
		foreach(var i in GD.Range(current_colors.size() - colors.size()))
		{
			colors.append(new Color());
		}
		return colors;
	
	}
	
	public void _on_ApplyColors_pressed()
	{  
		var colors = _convert_to_colors();
		EmitSignal("set_colors", colors);
		visible = false;
	
	
	}
	
	
	
}