
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class BlackHole: Planet
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("BlackHole").material.set_shader_parameter("pixels", amount);
		 // times 3 here because in this case ring is 3 times larger than planet
		GetNode("Disk").material.set_shader_parameter("pixels", amount*3.0);
		
		GetNode("BlackHole").size = new Vector2(amount, amount);
		GetNode("Disk").position = new Vector2(-amount, -amount);
		GetNode("Disk").size = new Vector2(amount, amount)*3.0;
	
	}
	
	public void set_light(__TYPE _pos)
	{  
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Disk").material.set_shader_parameter("seed", converted_seed);
	
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Disk").material.set_shader_parameter("rotation", r+0.7);
	
	}
	
	public void update_time(__TYPE t)
	{  
		GetNode("Disk").material.set_shader_parameter("time", t * 314.15 * 0.004 );
	
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("Disk").material.set_shader_parameter("time", t * 314.15 * GetNode("Disk").material.get_shader_parameter("time_speed") * 0.5);
	
	}
	
	public void set_dither(__TYPE d)
	{  
		GetNode("Disk").material.set_shader_parameter("should_dither", d);
	
	}
	
	public __TYPE get_dither()
	{  
		return GetNode("Disk").material.get_shader_parameter("should_dither");
	
	}
	
	public __TYPE get_colors()
	{  
		return get_colors_from_shader(GetNode("BlackHole").material) + get_colors_from_shader(GetNode("Disk").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		var cols1 = colors.slice(0, 3);
		var cols2 = colors.slice(3, 8);
		set_colors_on_shader(GetNode("BlackHole").material, cols1);
		set_colors_on_shader(GetNode("Disk").material, cols2);
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(5 + GD.Randi()%2, randf_range(0.3, 0.5), 2.0);
		Array cols= new Array(){};
		foreach(var i in 5)
		{
			var new_col = seed_colors[i].darkened((i/5.0) * 0.7);
			new_col = new_col.lightened((1.0 - (i/5.0)) * 0.9);
	
			cols.append(new_col);
	
		}
		set_colors(new Array(){new Color("272736")} + [cols[0], cols[3]] + cols);
	
	
	}
	
	
	
}