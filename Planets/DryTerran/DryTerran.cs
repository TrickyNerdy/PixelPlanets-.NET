
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class DryTerran
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("Land").material.set_shader_parameter("pixels", amount);
		GetNode("Land").size = new Vector2(amount, amount);
	}
	
	public void set_light(__TYPE pos)
	{  
		GetNode("Land").material.set_shader_parameter("light_origin", pos);
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Land").material.set_shader_parameter("seed", converted_seed);
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Land").material.set_shader_parameter("rotation", r);
	}
	
	public void update_time(__TYPE t)
	{  
		GetNode("Land").material.set_shader_parameter("time", t * get_multiplier(GetNode("Land").material) * 0.02);
	}
	
	public void set_custom_time(__TYPE t)
	{  
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
		return get_colors_from_shader(GetNode("Land").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		set_colors_on_shader(GetNode("Land").material, colors);
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(5 + GD.Randi()%3, randf_range(0.3, 0.65), 1.0);
		Array cols=new Array(){};
		foreach(var i in 5)
		{
			var new_col = seed_colors[i].darkened(i/5.0);
			new_col = new_col.lightened((1.0 - (i/5.0)) * 0.2);
	
			cols.append(new_col);
	
		}
		set_colors(cols);
	
	
	}
	
	
	
}