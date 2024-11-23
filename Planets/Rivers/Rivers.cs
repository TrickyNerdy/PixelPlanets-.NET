
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\Planets\Rivers\Rivers : "res://Planets/Planet.gd"
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("Land").material.set_shader_parameter("pixels", amount);
		GetNode("Cloud").material.set_shader_parameter("pixels", amount);
		GetNode("Land").size = new Vector2(amount, amount);
		GetNode("Cloud").size = new Vector2(amount, amount);
	
	}
	
	public void set_light(__TYPE pos)
	{  
		GetNode("Cloud").material.set_shader_parameter("light_origin", pos);
		GetNode("Land").material.set_shader_parameter("light_origin", pos);
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Cloud").material.set_shader_parameter("seed", converted_seed);
		GetNode("Cloud").material.set_shader_parameter("cloud_cover", randf_range(0.35, 0.6));
		GetNode("Land").material.set_shader_parameter("seed", converted_seed);
	
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Cloud").material.set_shader_parameter("rotation", r);
		GetNode("Land").material.set_shader_parameter("rotation", r);
	
	}
	
	public void update_time(__TYPE t)
	{  
		GetNode("Cloud").material.set_shader_parameter("time", t * get_multiplier(GetNode("Cloud").material) * 0.01);
		GetNode("Land").material.set_shader_parameter("time", t * get_multiplier(GetNode("Land").material) * 0.02);
	
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("Cloud").material.set_shader_parameter("time", t * get_multiplier(GetNode("Cloud").material) * 0.5);
		GetNode("Land").material.set_shader_parameter("time", t * get_multiplier(GetNode("Land").material));
	
	}
	
	public void set_dither(__TYPE d)
	{  
		GetNode("Land").material.set_shader_parameter("should_dither", d);
	
	}
	
	public __TYPE get_dither()
	{  
		return GetNode("Land").material.get_shader_parameter("should_dither");
	
	}
	
	public __TYPE get_colors()
	{  
		return get_colors_from_shader(GetNode("Land").material) + get_colors_from_shader(GetNode("Cloud").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		set_colors_on_shader(GetNode("Land").material, colors.slice(0, 6));
		set_colors_on_shader(GetNode("Cloud").material, colors.slice(6, 10));
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(GD.Randi()%2+3, randf_range(0.7, 1.0), randf_range(0.45, 0.55));
		Array land_colors = new Array(){};
		Array river_colors = new Array(){};
		Array cloud_colors = new Array(){};
		foreach(var i in 4)
		{
			var new_col = seed_colors[0].darkened(i/4.0);
			land_colors.append(Color.from_hsv(new_col.h + (0.2 * (i/4.0)), new_col.s, new_col.v));
		
		}
		foreach(var i in 2)
		{
			var new_col = seed_colors[1].darkened(i/2.0);
			river_colors.append(Color.from_hsv(new_col.h + (0.2 * (i/2.0)), new_col.s, new_col.v));
		
		}
		foreach(var i in 4)
		{
			var new_col = seed_colors[2].lightened((1.0 - (i/4.0)) * 0.8);
			cloud_colors.append(Color.from_hsv(new_col.h + (0.2 * (i/4.0)), new_col.s, new_col.v));
	
		}
		set_colors(land_colors + river_colors + cloud_colors);
	
	
	}
	
	
	
}