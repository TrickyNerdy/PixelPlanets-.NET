
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\Planets\Asteroids\Asteroid : "res://Planets/Planet.gd"
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("Asteroid").material.set_shader_parameter("pixels", amount);
		GetNode("Asteroid").size = new Vector2(amount, amount);
	
	}
	
	public void set_light(__TYPE pos)
	{  
		GetNode("Asteroid").material.set_shader_parameter("light_origin", pos);
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Asteroid").material.set_shader_parameter("seed", converted_seed);
	
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Asteroid").material.set_shader_parameter("rotation", r);
	
	}
	
	public void update_time(__TYPE _t)
	{  
	
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("Asteroid").material.set_shader_parameter("rotation", t * Mathf.Pi * 2.0);
	
	}
	
	public void set_dither(__TYPE d)
	{  
		GetNode("Asteroid").material.set_shader_parameter("should_dither", d);
	
	}
	
	public __TYPE get_dither()
	{  
		return GetNode("Asteroid").material.get_shader_parameter("should_dither");
	
	}
	
	public __TYPE get_colors()
	{  
		return get_colors_from_shader(GetNode("Asteroid").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		set_colors_on_shader(GetNode("Asteroid").material, colors);
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(3 + GD.Randi()%2, randf_range(0.3, 0.6), 0.7);
		Array cols= new Array(){};
		foreach(var i in 3)
		{
			var new_col = seed_colors[i].darkened(i/3.0);
			new_col = new_col.lightened((1.0 - (i/3.0)) * 0.2);
	
			cols.append(new_col);
	
		}
		set_colors(cols);
	
	
	}
	
	
	
}