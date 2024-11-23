
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\Planets\LavaWorld\LavaWorld : "res://Planets/Planet.gd"
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("Land").material.set_shader_parameter("pixels", amount);
		GetNode("Craters").material.set_shader_parameter("pixels", amount);
		GetNode("LavaRivers").material.set_shader_parameter("pixels", amount);
		
		GetNode("Land").size = new Vector2(amount, amount);
		GetNode("Craters").size = new Vector2(amount, amount);
		GetNode("LavaRivers").size = new Vector2(amount, amount);
		
	}
	
	public void set_light(__TYPE pos)
	{  
		GetNode("Land").material.set_shader_parameter("light_origin", pos);
		GetNode("Craters").material.set_shader_parameter("light_origin", pos);
		GetNode("LavaRivers").material.set_shader_parameter("light_origin", pos);
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Land").material.set_shader_parameter("seed", converted_seed);
		GetNode("Craters").material.set_shader_parameter("seed", converted_seed);
		GetNode("LavaRivers").material.set_shader_parameter("seed", converted_seed);
	
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Land").material.set_shader_parameter("rotation", r);
		GetNode("Craters").material.set_shader_parameter("rotation", r);
		GetNode("LavaRivers").material.set_shader_parameter("rotation", r);
	
	}
	
	public void update_time(__TYPE t)
	{  	
		GetNode("Land").material.set_shader_parameter("time", t * get_multiplier(GetNode("Land").material) * 0.02);
		GetNode("Craters").material.set_shader_parameter("time", t * get_multiplier(GetNode("Craters").material) * 0.02);
		GetNode("LavaRivers").material.set_shader_parameter("time", t * get_multiplier(GetNode("LavaRivers").material) * 0.02);
	
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("Land").material.set_shader_parameter("time", t * get_multiplier(GetNode("Land").material));
		GetNode("Craters").material.set_shader_parameter("time", t * get_multiplier(GetNode("Craters").material));
		GetNode("LavaRivers").material.set_shader_parameter("time", t * get_multiplier(GetNode("LavaRivers").material));
	
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
		return get_colors_from_shader(GetNode("Land").material) + get_colors_from_shader(GetNode("Craters").material) + get_colors_from_shader(GetNode("LavaRivers").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		set_colors_on_shader(GetNode("Land").material, colors.slice(0, 3));
		set_colors_on_shader(GetNode("Craters").material, colors.slice(3, 5));
		set_colors_on_shader(GetNode("LavaRivers").material, colors.slice(5, 8));
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(GD.Randi()%3+2, randf_range(0.6, 1.0), randf_range(0.7, 0.8));
		Array land_colors = new Array(){};
		Array lava_colors = new Array(){};
		foreach(var i in 3)
		{
			var new_col = seed_colors[0].darkened(i/3.0);
			land_colors.append(Color.from_hsv(new_col.h + (0.2 * (i/4.0)), new_col.s, new_col.v));
		
		}
		foreach(var i in 3)
		{
			var new_col = seed_colors[1].darkened(i/3.0);
			lava_colors.append(Color.from_hsv(new_col.h + (0.2 * (i/3.0)), new_col.s, new_col.v));
	
		}
		set_colors(land_colors + [land_colors[1], land_colors[2]] + lava_colors);
	
	
	}
	
	
	
}