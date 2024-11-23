
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\Planets\Galaxy\Galaxy : "res://Planets/Planet.gd"
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("Galaxy").material.set_shader_parameter("pixels", amount);
		GetNode("Galaxy").size = new Vector2(amount, amount) ;
	
	}
	
	public void set_light(__TYPE _pos)
	{  
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Galaxy").material.set_shader_parameter("seed", converted_seed);
	
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Galaxy").material.set_shader_parameter("rotation", r);
	
	}
	
	public void update_time(__TYPE t)
	{  
		GetNode("Galaxy").material.set_shader_parameter("time", t * get_multiplier(GetNode("Galaxy").material) * 0.04);
	
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("Galaxy").material.set_shader_parameter("time", t * Mathf.Pi * 2 * GetNode("Galaxy").material.get_shader_parameter("time_speed"));
	
	}
	
	public void set_dither(__TYPE d)
	{  
		GetNode("Galaxy").material.set_shader_parameter("should_dither", d);
	
	}
	
	public __TYPE get_dither()
	{  
		return GetNode("Galaxy").material.get_shader_parameter("should_dither");
	
	}
	
	public __TYPE get_colors()
	{  
		return get_colors_from_shader(GetNode("Galaxy").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		set_colors_on_shader(GetNode("Galaxy").material, colors);
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(6 , randf_range(0.5,0.8), 1.4);
		Array cols = new Array(){};
		foreach(var i in 6)
		{
			var new_col = seed_colors[i].darkened(i/7.0);
			new_col = new_col.lightened((1.0 - (i/6.0)) * 0.6);
			cols.append(new_col);
	
		}
		set_colors(cols);
	
	
	}
	
	
	
}