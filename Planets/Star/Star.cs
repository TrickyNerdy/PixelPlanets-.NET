
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class \Users\Trick\OneDrive\Documents\GitHub\PixelPlanets-.NET\Planets\Star\Star : "res://Planets/Planet.gd"
{
	 
	public void set_pixels(__TYPE amount)
	{  
		GetNode("Blobs").material.set_shader_parameter("pixels", amount*relative_scale);
		GetNode("Star").material.set_shader_parameter("pixels", amount);
		GetNode("StarFlares").material.set_shader_parameter("pixels", amount*relative_scale);
	
		GetNode("Star").size = new Vector2(amount, amount);
		GetNode("StarFlares").size = new Vector2(amount, amount)*relative_scale;
		GetNode("Blobs").size = new Vector2(amount, amount)*relative_scale;
	
		GetNode("StarFlares").position = new Vector2(-amount, -amount) * 0.5;
		GetNode("Blobs").position = new Vector2(-amount, -amount) * 0.5;
	
	}
	
	public void set_light(__TYPE _pos)
	{  
	
	}
	
	public void set_seed(__TYPE sd)
	{  
		var converted_seed = sd%1000/100.0;
		GetNode("Blobs").material.set_shader_parameter("seed", converted_seed);
		GetNode("Star").material.set_shader_parameter("seed", converted_seed);
		GetNode("StarFlares").material.set_shader_parameter("seed", converted_seed);
	
	}
	
	public __TYPE starcolor1 = new Gradient()
	public __TYPE starcolor2 = new Gradient()
	public __TYPE starflarecolor1 = new Gradient()
	public __TYPE starflarecolor2 = new Gradient()
	
	public void _ready()
	{  
		starcolor1.offsets = new Array(){0, 0.33, 0.66, 1.0};
		starcolor2.offsets = new Array(){0, 0.33, 0.66, 1.0};
		starflarecolor1.offsets = new Array(){0.0, 1.0};
		starflarecolor2.offsets = new Array(){0.0, 1.0};
		
		starcolor1.colors = new Array(){new Color("f5ffe8"), new Color("ffd832"), new Color("ff823b"), new Color("7c191a")};
		starcolor2.colors = new Array(){new Color("f5ffe8"), new Color("77d6c1"), new Color("1c92a7"), new Color("033e5e")};
		
		starflarecolor1.colors = new Array(){new Color("ffd832"), new Color("f5ffe8")};
		starflarecolor2.colors = new Array(){new Color("77d6c1"), new Color("f5ffe8")};
	
	}
	
	public void _set_colors(__TYPE sd)
	{   // this is just a little extra function to show some different possible stars
		if((sd % 2 == 0))
		{
			GetNode("Star").material.get_shader_parameter("colorramp").gradient = starcolor1;
			GetNode("StarFlares").material.get_shader_parameter("colorramp").gradient = starflarecolor1;
		}
		else
		{
			GetNode("Star").material.get_shader_parameter("colorramp").gradient = starcolor2;
			GetNode("StarFlares").material.get_shader_parameter("colorramp").gradient = starflarecolor2;
	
		}
	}
	
	public void set_rotates(__TYPE r)
	{  
		GetNode("Blobs").material.set_shader_parameter("rotation", r);
		GetNode("Star").material.set_shader_parameter("rotation", r);
		GetNode("StarFlares").material.set_shader_parameter("rotation", r);
	
	}
	
	public void update_time(__TYPE t)
	{  
		GetNode("Blobs").material.set_shader_parameter("time", t * get_multiplier(GetNode("Blobs").material) * 0.01);
		GetNode("Star").material.set_shader_parameter("time", t * get_multiplier(GetNode("Star").material) * 0.005);
		GetNode("StarFlares").material.set_shader_parameter("time", t * get_multiplier(GetNode("StarFlares").material) * 0.015);
	
	}
	
	public void set_custom_time(__TYPE t)
	{  
		GetNode("Blobs").material.set_shader_parameter("time", t * get_multiplier(GetNode("Blobs").material));
		GetNode("Star").material.set_shader_parameter("time", t * (1.0 / GetNode("Star").material.get_shader_parameter("time_speed")));
		GetNode("StarFlares").material.set_shader_parameter("time", t * get_multiplier(GetNode("StarFlares").material));
	
	}
	
	public void set_dither(__TYPE d)
	{  
		GetNode("Star").material.set_shader_parameter("should_dither", d);
		GetNode("StarFlares").material.set_shader_parameter("should_dither", d);
	
	}
	
	public __TYPE get_dither()
	{  
		return GetNode("Star").material.get_shader_parameter("should_dither");
	
	}
	
	public __TYPE get_colors()
	{  
		return get_colors_from_shader(GetNode("Blobs").material) + get_colors_from_shader(GetNode("Star").material) + get_colors_from_shader(GetNode("StarFlares").material);
	
	}
	
	public void set_colors(__TYPE colors)
	{  
		set_colors_on_shader(GetNode("Blobs").material, colors.slice(0, 1));
		set_colors_on_shader(GetNode("Star").material, colors.slice(1, 6));
		set_colors_on_shader(GetNode("StarFlares").material, colors.slice(6, 10));
	
	
	}
	
	public void randomize_colors()
	{  
		var seed_colors = _generate_new_colorscheme(4, randf_range(0.2, 0.4), 2.0);
		Array cols = new Array(){};
		foreach(var i in 4)
		{
			var new_col = seed_colors[i].darkened((i/4.0) * 0.9);
			new_col = new_col.lightened((1.0 - (i/4.0)) * 0.8);
	
			cols.append(new_col);
		}
		cols[0] = cols[0].lightened(0.8);
	
		set_colors([cols[0]] + cols + [cols[1], cols[0]]);
	
	
	}
	
	
	
}