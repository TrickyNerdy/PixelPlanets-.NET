
using System;
using Godot;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;


public class GUI: Control
{
		public Node viewport = GetNode("PlanetViewport");
		public Node viewport_planet = GetNode("PlanetViewport/PlanetHolder");
		public Node viewport_holder = GetNode("HBoxContainer/PlanetHolder");
		public Node viewport_tex = GetNode("HBoxContainer/PlanetHolder/ViewportTexture");
		public Node seedtext = GetNode("HBoxContainer/Settings/VBoxContainer/Seed/SeedText");
		public Node optionbutton = GetNode("HBoxContainer/Settings/VBoxContainer/OptionButton");
		public Node colorholder = GetNode("HBoxContainer/Settings/VBoxContainer/ColorButtonHolder");
		public Node picker = GetNode("Panel/ColorPicker");
		public Node random_colors = GetNode("HBoxContainer/Settings/VBoxContainer/HBoxContainer/RandomizeColors");
		public Node dither_button = GetNode("HBoxContainer/Settings/VBoxContainer/HBoxContainer2/ShouldDither");
		public Node layeroptions = GetNode("HBoxContainer/Settings/VBoxContainer/LayerOptions");
		public
		public Nodes colorbutton_scene = GD.Load("res://GUI/ColorPickerButton.tscn");
		public const var GIFExporter = GD.Load("res://addons/gdgifexporter/exporter.gd");
		public const var MedianCutQuantization = GD.Load("res://addons/gdgifexporter/quantization/median_cut.gd");


		public Dictionary planets = new Dictionary(){
		{"Terran Wet", GD.Load("res://Planets/Rivers/Rivers.tscn")},
		{"Terran Dry", GD.Load("res://Planets/DryTerran/DryTerran.tscn")},	
		{"Islands", GD.Load("res://Planets/LandMasses/LandMasses.tscn")},
		{"No atmosphere", GD.Load("res://Planets/NoAtmosphere/NoAtmosphere.tscn")},
		{"Gas giant 1", GD.Load("res://Planets/GasPlanet/GasPlanet.tscn")},
		{"Gas giant 2", GD.Load("res://Planets/GasPlanetLayers/GasPlanetLayers.tscn")},
		{"Ice World", GD.Load("res://Planets/IceWorld/IceWorld.tscn")},
		{"Lava World", GD.Load("res://Planets/LavaWorld/LavaWorld.tscn")},
		{"Asteroid", GD.Load("res://Planets/Asteroids/Asteroid.tscn")},
		{"Black Hole", GD.Load("res://Planets/BlackHole/BlackHole.tscn")},
		{"Galaxy", GD.Load("res://Planets/Galaxy/Galaxy.tscn")},
		{"Star", GD.Load("res://Planets/Star/Star.tscn")}
		}
	 
	
	public __TYPE pixels = 100.0;
	public __TYPE sd = 0;
	public __TYPE colors = new Array(){};
	public __TYPE should_dither = true;
	public __TYPE chosen_type = "Terran Wet";
	
	public void _ready()
	{  
		foreach(var k in planets.keys())
		{
			optionbutton.add_item(k);
		}
		layeroptions.get_popup().connect("id_pressed", Callable(this, "_on_layer_selected"));
		GetNode("ImportExportPopup").connect("set_colors", Callable(this, "_on_import_colors_set"));
	
		_seed_random();
		_create_new_planet(planets["Terran Wet"]);
	
	
	}
	
	public void _on_OptionButton_item_selected(__TYPE index)
	{  
		chosen_type = planets.keys()[index];
		var chosen_planet = planets[chosen_type];
		_create_new_planet(chosen_planet);
		_close_picker();
	
	}
	
	public void _on_SliderRotation_value_changed(__TYPE value)
	{  
		viewport_planet.get_child(0).set_rotates(value);
	
	}
	
	public void _on_LineEdit_text_changed(__TYPE new_text)
	{  
		call_deferred("_make_from_seed", (int)(new_text));
	
	}
	
	public void _make_from_seed(__TYPE new_seed)
	{  
		sd = new_seed;
		GD.Seed(sd);
		viewport_planet.get_child(0).set_seed(sd);
	
	}
	
	public async void _create_new_planet(__TYPE type)
	{  
		foreach(var c in viewport_planet.get_children())
		{
			c.queue_free();
		
		}
		var new_p = type.instantiate();
		viewport_planet.add_child(new_p);
		
		GD.Seed(sd);
		new_p.set_seed(sd)
		new_p.set_pixels(pixels)
		new_p.position = pixels * 0.5 * (new_p.relative_scale -1) * new Vector2(1,1);
		new_p.set_dither(should_dither)
		
		colors = new_p.get_colors();
		_make_color_buttons();
	
		_make_layer_selection(new_p);
	
		await get_tree().process_frame
		viewport.size = new Vector2(pixels, pixels) * new_p.relative_scale;
		
		// some hardcoded values that look good in the GUI
		switch( new_p.gui_zoom)
		{
			case 1.0:
				viewport_tex.position = new Vector2(50,50);
				viewport_tex.size = new Vector2(200,200);
				set_planet_holder_margin(46);
				break;
			case 2.0:
				viewport_tex.position = new Vector2(25,25);
				viewport_tex.size = new Vector2(250,250);
				set_planet_holder_margin(0);
				break;
			case 2.5:
				viewport_tex.position = new Vector2(0,0);
				viewport_tex.size = new Vector2(300,300);
				set_planet_holder_margin(0);
	
				break;
		}
	}
	
	public void set_planet_holder_margin(__TYPE margin_value)
	{  
		GetNode("HBoxContainer/PlanetHolder").add_theme_constant_override("margin_top", margin_value);
		GetNode("HBoxContainer/PlanetHolder").add_theme_constant_override("margin_bottom", margin_value);
		GetNode("HBoxContainer/PlanetHolder").add_theme_constant_override("margin_left", margin_value);
		GetNode("HBoxContainer/PlanetHolder").add_theme_constant_override("margin_right", margin_value);
	
	}
	
	public void _on_layer_selected(__TYPE id)
	{  
		viewport_planet.get_child(0).toggle_layer(id);
		_make_layer_selection(viewport_planet.get_child(0));
	
	}
	
	public void _make_layer_selection(__TYPE planet)
	{  
		var layers = planet.get_layers();
		layeroptions.get_popup().clear();
		int i = 0;
		foreach(var l in layers)
		{
			layeroptions.get_popup().add_check_item(l.name);
			layeroptions.get_popup().set_item_checked(i, l.visible);
			i+=1;
	
		}
	}
	
	public void _make_color_buttons()
	{  
		foreach(var b in colorholder.get_children())
		{
			b.queue_free();
		
		}
		foreach(var i in colors.size())
		{
			var b = colorbutton_scene.instantiate();
			b.set_color(colors[i]);
			b.set_index(i);
			b.connect("color_picked", Callable(this, "_on_colorbutton_color_picked"));
			b.connect("on_selected", Callable(this, "_on_colorbutton_pressed"));
			picker.connect("color_changed", Callable(b, "_on_picker_color_changed"));
			
			colorholder.add_child(b);
	
		}
	}
	
	public void _on_colorbutton_pressed(__TYPE button)
	{  
		foreach(var b in colorholder.get_children())
		{
			b.is_active = false;
		}
		button.is_active = true;
		GetNode("Panel").visible = true;
		picker.color = button.own_color;
	
	}
	
	public void _on_colorbutton_color_picked(__TYPE color, __TYPE index)
	{  
		colors[index] = color;
		viewport_planet.get_child(0).set_colors(colors);
	
	}
	
	public void _seed_random()
	{  
		GD.Randomize();
		sd = GD.Randi();
		GD.Seed(sd);
		seedtext.text = GD.Str(sd);
		viewport_planet.get_child(0).set_seed(sd);
	
	}
	
	public void _on_Button_pressed()
	{  
		_seed_random();
	
	}
	
	public void _on_ExportPNG_pressed()
	{  
		var planet = viewport_planet.get_child(0);
		var tex = viewport.get_texture().get_image();
		var image = Image.create(pixels * planet.relative_scale, pixels * planet.relative_scale, false, Image.FORMAT_RGBA8);
		int source_xy = 0;
		var source_size = pixels*planet.relative_scale;
		Rect2 source_rect = new Rect2(source_xy, source_xy,source_size,source_size);
		image.blit_rect(tex, source_rect, new Vector2(0,0));
		
		save_image(image, chosen_type + " - " + GD.Str(sd));
	
	}
	
	public async void export_spritesheet(__TYPE sheet_size, __TYPE progressbar, int pixel_margin = 0.0)
	{  
		var planet = viewport_planet.get_child(0);
		progressbar.max_value = sheet_size.x * sheet_size.y;
		var sheet = Image.create(pixels * sheet_size.x * planet.relative_scale + sheet_size.x*pixel_margin + pixel_margin,
					pixels * sheet_size.y * planet.relative_scale + sheet_size.y*pixel_margin + pixel_margin,
					false, Image.FORMAT_RGBA8);
		planet.override_time = true;
		
		int index = 0;
		foreach(var y in GD.Range(sheet_size.y))
		{
			foreach(var x in GD.Range(sheet_size.x + 1))
			{
				planet.set_custom_time(Mathf.Lerp(0.0, 1.0, (index)/float((sheet_size.x+1) * sheet_size.y)));
				await get_tree().process_frame
				
				if(index != 0)
				{
					var image = viewport.get_texture().get_image();
					int source_xy = 0;
					var source_size = pixels*planet.relative_scale;
					Rect2 source_rect = new Rect2(source_xy, source_xy,source_size,source_size);
					Vector2 destination = new Vector2(x - 1,y) * pixels * planet.relative_scale + new Vector2(x * pixel_margin, (y+1) * pixel_margin);
					sheet.blit_rect(image, source_rect, destination);
	
				}
				index +=1;
				progressbar.value = index;
		
		
			}
		}
		planet.override_time = false;
		save_image(sheet, chosen_type + " - " + GD.Str(sd) + " - spritesheet");
		GetNode("Popup").visible = false;
	
	}
	
	public void save_image(__TYPE img, __TYPE file_name)
	{  
		if(OS.has_feature("web"))
		{
			JavaScriptBridge.download_buffer(img.save_png_to_buffer(), file_name, "image/png");
		}
		else
		{
			if(OS.get_name() == "OSX")
			{
				img.save_png("user://%s.png"%file_name);
			}
			else
			{
				img.save_png("res://%s.png"%file_name);
	
			}
		}
	}
	
	public void _on_ExportSpriteSheet_pressed()
	{  
		GetNode("Panel").visible = false;
		GetNode("Popup").visible = true;
		GetNode("Popup").set_pixels(pixels * viewport_planet.get_child(0).relative_scale);
	
	}
	
	public void _on_PickerExit_pressed()
	{  
		_close_picker();
	
	}
	
	public void _close_picker()
	{  
		GetNode("Panel").visible = false;
		foreach(var b in colorholder.get_children())
		{
			b.is_active = false;
	
	
		}
	}
	
	public void _on_RandomizeColors_pressed()
	{  
		viewport_planet.get_child(0).randomize_colors();
		colors = viewport_planet.get_child(0).get_colors();
		foreach(var i in colorholder.get_child_count())
		{
			colorholder.get_child(i).set_color(colors[i]);
	
		}
	}
	
	public void _on_ResetColors_pressed()
	{  
		viewport_planet.get_child(0).set_colors(viewport_planet.get_child(0).original_colors);
		colors = viewport_planet.get_child(0).get_colors();
		foreach(var i in colorholder.get_child_count())
		{
			colorholder.get_child(i).set_color(colors[i]);
	
		}
	}
	
	public void _on_ShouldDither_pressed()
	{  
		should_dither = !should_dither;
		if(should_dither)
		{
			dither_button.text = "On";
		}
		else
		{
			dither_button.text = "Off";
		}
		viewport_planet.get_child(0).set_dither(should_dither);
	
	}
	
	public void _on_ExportGIF_pressed()
	{  
		GetNode("GifPopup").visible = true;
		cancel_gif = false
	
	}
	
	var cancel_gif = false
	func export_gif(frames, frame_delay, progressbar):
		public __TYPE planet = viewport_planet.get_child(0);
		public __TYPE exporter = GIFExporter.new(pixels*planet.relative_scale, pixels*planet.relative_scale)
		progressbar.max_value = frames;
		
		planet.override_time = true;
		planet.set_custom_time(0.0);
		await get_tree().process_frame
		
		foreach(var i in GD.Range(frames))
		{
			if cancel_gif:
				progressbar.value = 0;
				planet.override_time = false;
				break;
				return;
			
			planet.set_custom_time(Mathf.Lerp(0.0, 1.0, (float)(i)/float(frames)));
	
			await get_tree().process_frame
			
			public __TYPE tex = viewport.get_texture().get_image();
			public __TYPE image = Image.create(pixels * planet.relative_scale, pixels * planet.relative_scale, false, Image.FORMAT_RGBA8);
			
			public __TYPE source_xy = 0;
			public __TYPE source_size = pixels*planet.relative_scale;
			public __TYPE source_rect = new Rect2(source_xy, source_xy,source_size,source_size);
			image.blit_rect(tex, source_rect, new Vector2(0,0));
			exporter.add_frame(image, frame_delay, MedianCutQuantization);
			
			progressbar.value = i;
		
		}
		if cancel_gif:
			return;
		if(OS.has_feature("web"))
		{
			public __TYPE data = new Array(exporter.export_file_data());
			JavaScriptBridge.download_buffer(data, (chosen_type + " - " +str(sd))+".gif", "image/gif");
		}
		else
		{
			FileAccess file
			if(OS.get_name() == "OSX")
			{
				file = FileAccess.open("user://%s.gif"%(chosen_type + " - " +str(sd)), FileAccess.WRITE);
			}
			else
			{
				file = FileAccess.open("res://%s.gif"%(chosen_type + " - " +str(sd)), FileAccess.WRITE);
			}
			file.store_buffer(exporter.export_file_data());
			file.close();
	
		}
		planet.override_time = false;
		GetNode("GifPopup").visible = false;
		progressbar.visible = false;
	
	
	func _on_GifPopup_cancel_gif():
		cancel_gif = true
	
	public async void _on_InputPixels_text_changed(__TYPE text)
	{  
		pixels = (int)(text)
		pixels = Mathf.Clamp(pixels, 12, 5000);
		if(((int)(text) > 5000))
		{
			GetNode("HBoxContainer/Settings/VBoxContainer/InputPixels").text = GD.Str(pixels);
		
		}
		var p = viewport_planet.get_child(0);
		p.set_pixels(pixels);
		
		p.position = pixels * 0.5 * (p.relative_scale -1) * new Vector2(1,1);
	
		await get_tree().process_frame
		viewport.size = new Vector2(pixels, pixels) * p.relative_scale;
	
	}
	
	public void _on_ImportExportColors_pressed()
	{  
		colors = viewport_planet.get_child(0).get_colors();
		GetNode("ImportExportPopup").set_current_colors(colors);
		GetNode("ImportExportPopup").show_popup();
		
	}
	
	public void _on_import_colors_set(__TYPE i_colors)
	{  
		viewport_planet.get_child(0).set_colors(i_colors);
		foreach(var i in colorholder.get_child_count())
		{
			colorholder.get_child(i).set_color(i_colors[i]);
	
	
		}
	}
	
	public void _on_planet_holder_gui_input(__TYPE event)
	{  
		if((event is InputEventMouseMotion || event is InputEventScreenTouch) && Input.is_action_pressed("mouse"))
		{
			var normal = event.position / GetNode("HBoxContainer/PlanetHolder").size;
			viewport_planet.get_child(0).set_light(normal);
			
			if(GetNode("Panel").visible)
			{
				_close_picker();
	
	
			}
		}
	}
	
	
	
}