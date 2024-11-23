
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\Planets\GasPlanet\GasPlanet : "res://Planets/Planet.gd"
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("Cloud").material.set_shader_parameter("pixels", amount);
		GetNode("Cloud2").material.set_shader_parameter("pixels", amount);
		GetNode("Cloud").size = new Vector2(amount, amount);
		GetNode("Cloud2").size = new Vector2(amount, amount);
	
	}
	
	public void set_light(__TYPE pos)
	{  
		GetNode("Cloud").material.set_shader_parameter("light_origin", pos);
		GetNode("Cloud2").material.set_shader_parameter("light_origin", pos);
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Cloud").material.set_shader_parameter("seed", converted_seed);
		GetNode("Cloud2").material.set_shader_parameter("seed", converted_seed);
		GetNode("Cloud2").material.set_shader_parameter("cloud_cover", randf_range(0.28, 0.5));
	
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Cloud").material.set_shader_parameter("rotation", r);
		GetNode("Cloud2").material.set_shader_parameter("rotation", r);
		
	}
	
	public void update_time(__TYPE t)
	{  
		GetNode("Cloud").material.set_shader_parameter("time", t * get_multiplier(GetNode("Cloud").material) * 0.005);
		GetNode("Cloud2").material.set_shader_parameter("time", t * get_multiplier(GetNode("Cloud2").material) * 0.005);
		
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("Cloud").material.set_shader_parameter("time", t * get_multiplier(GetNode("Cloud").material));
		GetNode("Cloud2").material.set_shader_parameter("time", t * get_multiplier(GetNode("Cloud2").material));
	
	}
	
	public __TYPE get_colors()
	{  
		return get_colors_from_shader(GetNode("Cloud").material) + get_colors_from_shader(GetNode("Cloud2").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		set_colors_on_shader(GetNode("Cloud").material, colors.slice(0, 4));
		set_colors_on_shader(GetNode("Cloud2").material, colors.slice(4, 8));
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(8 + GD.Randi()%4, randf_range(0.3, 0.8), 1.0);
		Array cols1= new Array(){};
		Array cols2= new Array(){};
		foreach(var i in 4)
		{
			var new_col = seed_colors[i].darkened(i/6.0).darkened(0.7);
	//		new_col = new_col.lightened((1.0 - (i/4.0)) * 0.2);
			cols1.append(new_col);
		
		}
		foreach(var i in 4)
		{
			var new_col = seed_colors[i+4].darkened(i/4.0);
			new_col = new_col.lightened((1.0 - (i/4.0)) * 0.5);
			cols2.append(new_col);
	
		}
		set_colors(cols1 + cols2);
	
	
	
	}
	
	
	
}