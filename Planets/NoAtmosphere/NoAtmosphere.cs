
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\Planets\NoAtmosphere\NoAtmosphere : "res://Planets/Planet.gd"
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("Ground").material.set_shader_parameter("pixels", amount);
		GetNode("Craters").material.set_shader_parameter("pixels", amount);
	
		GetNode("Ground").size = new Vector2(amount, amount);
		GetNode("Craters").size = new Vector2(amount, amount);
	
	}
	
	public void set_light(__TYPE pos)
	{  
		GetNode("Ground").material.set_shader_parameter("light_origin", pos);
		GetNode("Craters").material.set_shader_parameter("light_origin", pos);
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Ground").material.set_shader_parameter("seed", converted_seed);
		GetNode("Craters").material.set_shader_parameter("seed", converted_seed);
	
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Ground").material.set_shader_parameter("rotation", r);
		GetNode("Craters").material.set_shader_parameter("rotation", r);
	
	}
	
	public void update_time(__TYPE t)
	{  
		GetNode("Ground").material.set_shader_parameter("time", t * get_multiplier(GetNode("Ground").material) * 0.02);
		GetNode("Craters").material.set_shader_parameter("time", t * get_multiplier(GetNode("Craters").material) * 0.02);
	
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("Ground").material.set_shader_parameter("time", t * get_multiplier(GetNode("Ground").material));
		GetNode("Craters").material.set_shader_parameter("time", t * get_multiplier(GetNode("Craters").material));
	
	}
	
	public void set_dither(__TYPE d)
	{  
		GetNode("Ground").material.set_shader_parameter("should_dither", d);
	
	}
	
	public __TYPE get_dither()
	{  
		return GetNode("Ground").material.get_shader_parameter("should_dither");
	
	}
	
	public __TYPE get_colors()
	{  
		return get_colors_from_shader(GetNode("Ground").material) + get_colors_from_shader(GetNode("Craters").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		set_colors_on_shader(GetNode("Ground").material, colors.slice(0, 3));
		set_colors_on_shader(GetNode("Craters").material, colors.slice(3, 5));
	
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
		set_colors(cols + [cols[1], cols[2]]);
	
	
	}
	
	
	
}