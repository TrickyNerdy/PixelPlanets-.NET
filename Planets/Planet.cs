
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class Planet : Control
{
	 
	public __TYPE time = 1000.0;
	public __TYPE override_time = false;
	public __TYPE original_colors;
	[Export] public float relative_scale = 1.0;
	[Export] public float gui_zoom = 1.0;
	
	public void _ready()
	{  
		original_colors = get_colors();
	
	}
	
	public void set_pixels(__TYPE _amount)
	{  
	}
	
	public void set_light(__TYPE _pos)
	{  
	}
	
	public void set_seed(__TYPE _sd)
	{  
	}
	
	public void set_rotates(__TYPE _r)
	{  
	}
	
	public void update_time(__TYPE _t)
	{  
	}
	
	public void set_custom_time(__TYPE _t)
	{  
	
	}
	
	public __TYPE get_multiplier(__TYPE mat)
	{  
		return (Mathf.Round(mat.get_shader_parameter("size")) * 2.0) / mat.get_shader_parameter("time_speed");
		
	}
	
	public void _process(__TYPE delta)
	{  
		time += delta	;
		if(!override_time)
		{
			update_time(time);
	
		}
	}
	
	public void set_dither(__TYPE _d)
	{  
	
	}
	
	public void get_dither()
	{  
	
	}
	
	public void get_colors()
	{  
	
	}
	
	public __TYPE get_colors_from_shader(__TYPE mat, string uniform_name = "colors")
	{  
		return mat.get_shader_parameter(uniform_name);
	
	}
	
	public void set_colors_on_shader(__TYPE mat, __TYPE colors, string uniform_name = "colors")
	{  
		mat.set_shader_parameter(uniform_name, colors);
	
	}
	
	public void randomize_colors()
	{  
	
	// Using ideas from https://www.iquilezles.org/www/articles/palettes/palettes.htm
	}
	
	public __TYPE _generate_new_colorscheme(__TYPE n_colors, int hue_diff = 0.9, int saturation = 0.5)
	{  
	//	Vector3 a = new Vector3(GD.RandRange(0.0, 0.5), GD.RandRange(0.0, 0.5), GD.RandRange(0.0, 0.5));
		Vector3 a = new Vector3(0.5,0.5,0.5);
	//	Vector3 b = new Vector3(GD.RandRange(0.1, 0.6), GD.RandRange(0.1, 0.6), GD.RandRange(0.1, 0.6));
		Vector3 b = new Vector3(0.5,0.5,0.5) * saturation;
		Vector3 c = new Vector3(randf_range(0.5, 1.5), randf_range(0.5, 1.5), randf_range(0.5, 1.5)) * hue_diff;
		Vector3 d = new Vector3(randf_range(0.0, 1.0), randf_range(0.0, 1.0), randf_range(0.0, 1.0)) * randf_range(1.0, 3.0);
	
		var cols = PackedColorArray();
		var n = (float)(n_colors - 1.0);
		n = Mathf.Max(1, n);
		foreach(var i in GD.Range(0, n_colors, 1))
		{
			Vector3 vec3 = new Vector3();
			vec3.x = (a.x + b.x *cos(6.28318 * (c.x*float(i/n) + d.x)));
			vec3.y = (a.y + b.y *cos(6.28318 * (c.y*float(i/n) + d.y)));
			vec3.z = (a.z + b.z *cos(6.28318 * (c.z*float(i/n) + d.z)));
	
			cols.append(new Color(vec3.x, vec3.y, vec3.z));
		
		}
		return cols;
	
	}
	
	public __TYPE get_layers()
	{  
		Array layers = new Array(){};
		foreach(var c in get_children())
		{
			layers.append(new Dictionary(){{"name", c.get_name()}, {"visible", c.visible}});
		}
		return layers;
	
	}
	
	public void toggle_layer(__TYPE num)
	{  
		get_children()[num].visible = !get_children()[num].visible;
	
	
	}
	
	
	
}