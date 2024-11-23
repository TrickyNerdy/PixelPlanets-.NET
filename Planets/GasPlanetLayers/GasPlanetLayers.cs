
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class GasPlanetLayers: Planet
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("GasLayers").material.set_shader_parameter("pixels", amount);
		 // times 3 here because in this case ring is 3 times larger than planet
		GetNode("Ring").material.set_shader_parameter("pixels", amount*3.0);
		
		GetNode("GasLayers").size = new Vector2(amount, amount);
		GetNode("Ring").position = new Vector2(-amount, -amount);
		GetNode("Ring").size = new Vector2(amount, amount)*3.0;
	
	}
	
	public void set_light(__TYPE pos)
	{  
		GetNode("GasLayers").material.set_shader_parameter("light_origin", pos);
		GetNode("Ring").material.set_shader_parameter("light_origin", pos);
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("GasLayers").material.set_shader_parameter("seed", converted_seed);
		GetNode("Ring").material.set_shader_parameter("seed", converted_seed);
	
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("GasLayers").material.set_shader_parameter("rotation", r);
		GetNode("Ring").material.set_shader_parameter("rotation", r+0.7);
	
	}
	
	public void update_time(__TYPE t)
	{  
		GetNode("GasLayers").material.set_shader_parameter("time", t * get_multiplier(GetNode("GasLayers").material) * 0.004);
		GetNode("Ring").material.set_shader_parameter("time", t * 314.15 * 0.004);
	
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("GasLayers").material.set_shader_parameter("time", t * get_multiplier(GetNode("GasLayers").material));
		GetNode("Ring").material.set_shader_parameter("time", t * 314.15 * GetNode("Ring").material.get_shader_parameter("time_speed") * 0.5);
	
	}
	
	public void set_dither(__TYPE d)
	{  
		GetNode("GasLayers").material.set_shader_parameter("should_dither", d);
	
	}
	
	public __TYPE get_dither()
	{  
		return GetNode("GasLayers").material.get_shader_parameter("should_dither");
	
	
	}
	
	public __TYPE get_colors()
	{  
		return get_colors_from_shader(GetNode("GasLayers").material) + get_colors_from_shader(GetNode("GasLayers").material, "dark_colors") + get_colors_from_shader(GetNode("Ring").material) + get_colors_from_shader(GetNode("Ring").material, "dark_colors");
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		var cols1 = colors.slice(0, 3);
		var cols2 = colors.slice(3, 6);
		
		set_colors_on_shader(GetNode("GasLayers").material, cols1);
		set_colors_on_shader(GetNode("Ring").material, cols1);
		
		set_colors_on_shader(GetNode("GasLayers").material, cols2, "dark_colors");
		set_colors_on_shader(GetNode("Ring").material, cols2, "dark_colors");
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(6 + GD.Randi() % 4, randf_range(0.3,0.55), 1.4);
		Array cols = new Array(){};
		foreach(var i in 6)
		{
			var new_col = seed_colors[i].darkened(i/7.0);
			new_col = new_col.lightened((1.0 - (i/6.0)) * 0.3);
			cols.append(new_col);
	
		}
		set_colors(cols);
	
	
	}
	
	
	
}