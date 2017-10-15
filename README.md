![logo](https://user-images.githubusercontent.com/2094592/31586173-e980b41e-b1cd-11e7-9ec5-d35d8298968f.png)
TextureReplacerReplaced
===============

* [Forum thread](http://forum.kerbalspaceprogram.com/index.php?/topic/161898-13texturereplacerreplaced-v01/)
* [GitHub page](https://github.com/HaArLiNsH/TextureReplacerReplaced)

TextureReplacerReplaced is a plugin for Kerbal Space Program that allows you to replace
stock textures and customise your Kerbals. 

This is the continuation of [TextureReplacer](https://github.com/ducakar/TextureReplacer) made by Davorin Učakar alias Shaw. 

More specifically, it can:

* replace stock textures with custom ones,
* assign personalised head and suit textures for each Kerbal,
* assign suits based on class and experience level,
* configure the elements the head and suits,
* toggle between 3 suits : Iva , Eva Ground, Eva Space
* add reflections to parts and helmet visors,
* generate missing mipmaps for PNG and JPEG model textures,
* compress uncompressed textures from `GameData/` to shrink textures in VRAM,
* unload textures from RAM after KSP finishes loading to reduce RAM usage and
* change bilinear texture filter to trilinear to improve mipmap quality.

Special thanks to:

* Davorin Učakar for making TextureReplacer in the first place, You rock man ! 
* RangeMachine who kept this mod alive,
* rbray89 who contributed a reflective visor shader and for Active Texture
  Management and Visual Enhancements where some code has been borrowed from,
* Tingle for Universe Replacer,
* taniwha for KerbalStats that was optionally used by this plugin for gender
  determination and role-based suit assignment,
* Razchek and Starwaster for Reflection Plugin,
* sarbian for fixing an issue with non-mupliple-of-4 texture dimensions,
* Ippo343 for contributing KSP-AVC configuration,
* JPLRepo for contributing DeepFreeze compatibility fixes,
* therealcrow999 for testing and benchmarking this plugin,
* Proot, Scart91, Green Skull and others for creating texture packs and
* Sylith and Scart91 for giving others permissions to make derivatives of their
  texture packs.
* Sigma88 for his contribution on the MM compatibility and the new folder system.
* Ger_space for his brilliant work on the shader system. 



Instructions
------------

You can find more informations on the custom head/suit set in the [TRR_Guide](https://github.com/HaArLiNsH/TRR_Guide)

### Important Stuf ###

TRR is provided with mandatory default textures. 

DON'T REMOVE THEM except for these 4 if you don't like them: 

* Suit_Iva_Veteran_Default : this the veteran version of the IVA suit
* Suit_EvaSpace_Veteran_Default : this the veteran version of the EVAground suit
* Suit_EvaGround_Veteran_Default : this the veteran version of the EVAspace suit
* logoFullRed_alt : this is the custom logo at the main menu

You can still replace these textures as you did before with Texture Replacer, but if you remove them , your game WILL crash.


### It is now recommended that you use a different folder than TRR for your custom textures ### 

You can find an empty premade [here](https://github.com/HaArLiNsH/TextureReplacerReplaced/releases/download/untagged-ef040eb633011347ae99/TRR_MyTextureMod.zip) 

You just have to make 3 steps to make it ready for you :

Change the name "MyTextureMod" by the one you want,

* Of the folder that goes in GameData/ 
* Of the file MyTextureMod.cfg 
* Inside this .cfg 


You can make this "MyTextureMod.cfg" also by yourself, 
make a MyTextureMod.txt file and rename it MyTextureMod.cfg and put this inside : 


	TextureReplacerReplaced
	{
	Folders
	{
		Default = MyTextureMod/Default/
		EnvMap = MyTextureMod/EnvMap/
		Heads = MyTextureMod/Heads/
		Suits = MyTextureMod/Suits/		
	}
	}

### General Suit settings ###


Your kerbals have now 3 suits that are used generally this way : 

* IVA : Used inside vehicle and outside of vehicle, on the ground, with atmosphere
* EVA Ground : Used outside of vehicle, on the ground, without atmosphere
* EVA Space : Used outside of Vehicle, in Space

You can assign one of these suits (and their elements, suit,helmet,visor and jetpack) for each of these situations : 

* EVAground Atmo : Out Of Vehicle, On the Ground, With Atmosphere,
* EVAground NoAtmo : Out Of Vehicle, On the Ground, Without Atmosphere,
* EVAspace : Out Of Vehicle, In Space,
* IVA Safe : In Vehicle, Safe (landed or in orbit),
* IVA Unsafe : In Vehicle, UnSafe (flying)

You can also configure the color and the reflection color of the 3 visors. This setting will affect how your visor custom texture will appear. 
TRR is  provided with 3 visor textures with different transparency level 

* IVA : grey 40% (clear but you still see the reflections)
* EVAground : grey 70% 
* EVAspace : grey 85% 

The EVAspace visor is colored in yellow by default. Don't forget to change the base color if you use a custom texture.


### General Textures ###

General replacement textures are of the form

    GameData/MyTextureMod/Default/<internalName>

where `<internalName>` is the texture's internal name in KSP or path of a
texture inside `GameData/` (plus .dds/.png/.jpg/.tga extension, of course).

.dds IS RECOMMENDED (especially for the normal maps)

Examples:

    GameData/MyTextureMod/
	
      Default/kerbalHead								// male and female teeth and male head
      Default/kerbalHeadNRM						// male and female teeth and male head normal map
      Default/kerbalGirl_06_BaseColor			// female head
      Default/kerbalGirl_06_BaseColorNRM	// female head normal map	  
	  
      Default/kerbalMain								// IVA veteran suit (veteran/orange)
      Default/kerbalMainGrey						// IVA suit (standard/grey)
      Default/kerbalMainNRM               			// IVA suit normal map	  
      Default/kerbalHelmetGrey           			// IVA helmet
      Default/kerbalHelmetNRM             		// IVA helmet normal map
      Default/kerbalVisor                 				// IVA helmet visor
	  Default/kerbalVisorNRM                 		// IVA helmet visor normal map
	  
	  Default/EVAgroundTexture                  	// EVA ground suit (standard/grey)
	  Default/EVAgroundTextureVeteran         // EVA ground suit (veteran/orange)
      Default/EVAgroundTextureNRM              // EVA ground suit normal map	  
      Default/EVAgroundHelmet                   	// EVA ground helmet
	  Default/EVAgroundHelmetNRM             	// EVA ground helmet normal map
      Default/EVAgroundVisor                    	// EVA ground helmet visor	 
	  Default/EVAgroundVisorNRM                 // EVA ground helmet visor	normal map 
      Default/EVAgroundJetpack                  	// EVA ground jetpack
      Default/EVAgroundJetpackNRM              // EVA ground jetpack normal map 
	  
      Default/EVAtexture                  			// EVA space suit (standard/grey)
	  Default/EVAtextureVeteran                  	// EVA space suit (veteran/orange)
      Default/EVAtextureNRM               			// EVA space suit normal map	  
      Default/EVAhelmet                   			// EVA space helmet
	  Default/EVAhelmetNRM               			// EVA space helmet normal map
      Default/EVAvisor                    				// EVA space helmet visor
	  Default/EVAvisorNRM              				// EVA space helmet visor normal map	  
      Default/EVAjetpack                  			// EVA space jetpack
      Default/EVAjetpackNRM              			// EVA space jetpack normal map     

      Default/GalaxyTex_PositiveX        		// skybox right face (if you don't put these in your custom folder, they wont work)
      Default/GalaxyTex_NegativeX         		// skybox left face
      Default/GalaxyTex_PositiveY         		// skybox bottom face rotated for 180°
      Default/GalaxyTex_NegativeY         		// skybox top face
      Default/GalaxyTex_PositiveZ        		// skybox front face
      Default/GalaxyTex_NegativeZ         		// skybox back face

      Default/moho00                      				// Moho
      Default/moho01                      				// Moho normal map
      Default/Eve2_00                     				// Eve
      Default/Eve2_01                     				// Eve normal map
      Default/evemoon100                 			// Gilly
      Default/evemoon101                  			// Gilly normal map
      Default/KerbinScaledSpace300        		// Kerbin
      Default/KerbinScaledSpace401        		// Kerbin normal map
      Default/NewMunSurfaceMapDiffuse     	// Mün
      Default/NewMunSurfaceMapNormals     	// Mün normal map
      Default/NewMunSurfaceMap00          	// Minmus
      Default/NewMunSurfaceMap01          	// Minmus normal map
      Default/Duna5_00                    			// Duna
      Default/Duna5_01                    			// Duna normal map
      Default/desertplanetmoon00         		// Ike
      Default/desertplanetmoon01          		// Ike normal map
      Default/dwarfplanet100              			// Dres
      Default/dwarfplanet101              			// Dres normal map
      Default/gas1_clouds                 			// Jool
      Default/cloud_normal                			// Jool normal map
      Default/newoceanmoon00              		// Laythe
      Default/newoceanmoon01              		// Laythe normal map
      Default/gp1icemoon00                			// Vall
      Default/gp1icemoon01                			// Vall normal map
      Default/rockyMoon00                 			// Tylo
      Default/rockyMoon01                 			// Tylo normal map
      Default/gp1minormoon100             		// Bop
      Default/gp1minormoon101             		// Bop normal map
      Default/gp1minormoon200             		// Pol
      Default/gp1minormoon201             		// Pol normal map
      Default/snowydwarfplanet00          		// Eeloo
      Default/snowydwarfplanet01          		// Eeloo normal map

It's also possible to replace textures from `GameData/` if one specifies
the full directory hierarchy:

    GameData/MyTextureMod/
      Default/Squad/Parts/Command/Mk1-2Pod/model000  // Mk1-2 pod texture
      Default/Squad/Parts/Command/Mk1-2Pod/model001  // Mk1-2 pod normal map

Note that all texture and directory names are case-sensitive!

### Reflections ###

Reflections are shown on visors of Kerbals' helmets and on parts that include
`TRReflection` module. There are two types of reflections: real and static.
Real reflections reflect the environment of a part while static reflections
reflect the skybox from `EnvMap/` directory:

    GameData/MyTextureMod/
      EnvMap/PositiveX         // fake skybox right face, vertically flipped
      EnvMap/NegativeX         // fake skybox left face, vertically flipped
      EnvMap/PositiveY         // fake skybox top face, vertically flipped
      EnvMap/NegativeY         // fake skybox bottom face, vertically flipped
      EnvMap/PositiveZ         // fake skybox front face, vertically flipped
      EnvMap/NegativeZ         // fake skybox back face, vertically flipped

Note that all textures must be quares and have the same dimensions that are
powers of two. Cube map textures are slow, so keep them as low-res as possible.

The static reflections textures don't work well with KSP 1.3.xxx. 
I recommend using only NO relfection or REAL reflections. 



`TRReflection` part module can be used as in the following example that adds
reflections onto the windows of Mk1-2 pod:

    MODULE
    {
      name = TRReflection
      shader = Reflective/Bumped Diffuse
      colour = 0.5 0.5 0.5
      interval = 1
      meshes = FrontWindow SideWindow
    }

There are several parameters, all optional:

* `shader`: Most shaders should be automatically mapped to their reflective
  counterparts. In some cases, however, thare are no reflective version of a
  shader, so you will have to manually specify appropriate shader.
* `colour`: Reflection is pre-multiplied by this RGB value before added to the
  material. "0.5 0.5 0.5" by default.
* `interval`: Once in how many steps the reflection is updated. "1" by default.
* `meshes`: Space- and/or comma-sparated list of mesh names where to apply
  reflections. Reflection is applied to whole part if this parameter is empty or
  non-existent. You may find `logReflectiveMeshes` configuration option very
  helpful as it prints names of all meshes for each part with `TRReflection`
  module into your log.

One face of one reflection cube texture is updated every `reflectionInterval`
frames (2 by default, it can be changed in a configuration file), so each
reflective part has to be updated six times to update all six texture faces.
More reflective parts there are on the scene less frequently they are updated.
`interval` field on TRReflection module can lessen the update rate for a part;
e.g. `interval = 2` makes the part update half less frequently.

### Personalised Kerbal Textures ###

Heads and suits are assigned either manually or automatically (configured in the
GUI while configuration files can provide initial settings). 

You have now different GUI that can be used either in the spacecenter scene and in the Flightscene.

"Random" assignment of heads try to give the head set the less used first, sometimes you wont
have a lot of possibilities because all of your head set must be assign first to one kerbal before 
it is randomly assigned to another kerbal (this don't account the exclusive heads)

Additionally, special per-class suit can be set for each class.


### Head Set ###

Head textures reside inside a directory inside either `Heads/Male/` or `Heads/Female/`
directory. Each head set must reside inside its own directory:

    GameData/MyTextureMod/Heads/Male/				// for the male heads
	GameData/MyTextureMod/Heads/Female/			// for the female heads
      MyTextureModMaleHead1/								// the custom folder for your head set
				HeadTexture0										//	The texture for the head at level 0 (mandatory)
				HeadTexture1										//	The texture for the head at level 1
				HeadTexture2										//	The texture for the head at level 2
				HeadTexture3										//	The texture for the head at level 3
				HeadTexture4										//	The texture for the head at level 4
				HeadTexture5										//	The texture for the head at level 5
				
				HeadTextureNRM0									//	The normal map for the head at level 0 (mandatory)
				HeadTextureNRM1									//	The normal map for the head at level 1
				HeadTextureNRM2									//	The normal map for the head at level 2
				HeadTextureNRM3									//	The normal map for the head at level 3
				HeadTextureNRM4									//	The normal map for the head at level 4
				HeadTextureNRM5									//	The normal map for the head at level 5
      

### Suit Set ###	  
	  
Suit textures reside inside a directory and their name are not the same as the 
ones you use to replace the default textures. You can make variants for the level, 
the gender and the veteran, badass, veteran badass status of each elements and their normal maps
Each suit set must reside inside its own directory:
	  

    GameData/MyTextureMod/Suits/
	  MyTextureModMaleHead1/
			

// -----------------------------------------------------
// Helmet
// -----------------------------------------------------

	Helmet_EvaGround_Badass_Female0
	Helmet_EvaGround_Badass_FemaleNRM0
	Helmet_EvaGround_Badass_Male0
	Helmet_EvaGround_Badass_MaleNRM0
	Helmet_EvaGround_Standard_Female0
	Helmet_EvaGround_Standard_FemaleNRM0
	Helmet_EvaGround_Standard_Male0
	Helmet_EvaGround_Standard_MaleNRM0
	Helmet_EvaGround_VetBad_Female0
	Helmet_EvaGround_VetBad_FemaleNRM0
	Helmet_EvaGround_VetBad_Male0
	Helmet_EvaGround_VetBad_MaleNRM0
	Helmet_EvaGround_Veteran_Female0
	Helmet_EvaGround_Veteran_FemaleNRM0
	Helmet_EvaGround_Veteran_Male0
	Helmet_EvaGround_Veteran_MaleNRM0

	Helmet_EvaSpace_Badass_Female0
	Helmet_EvaSpace_Badass_FemaleNRM0
	Helmet_EvaSpace_Badass_Male0
	Helmet_EvaSpace_Badass_MaleNRM0
	Helmet_EvaSpace_Standard_Female0
	Helmet_EvaSpace_Standard_FemaleNRM0
	Helmet_EvaSpace_Standard_Male0
	Helmet_EvaSpace_Standard_MaleNRM0
	Helmet_EvaSpace_VetBad_Female0
	Helmet_EvaSpace_VetBad_FemaleNRM0
	Helmet_EvaSpace_VetBad_Male0
	Helmet_EvaSpace_VetBad_MaleNRM0
	Helmet_EvaSpace_Veteran_Female0
	Helmet_EvaSpace_Veteran_FemaleNRM0
	Helmet_EvaSpace_Veteran_Male0
	Helmet_EvaSpace_Veteran_MaleNRM0
	
	Helmet_Iva_Badass_Female0
	Helmet_Iva_Badass_FemaleNRM0
	Helmet_Iva_Badass_Male0
	Helmet_Iva_Badass_MaleNRM0
	Helmet_Iva_Standard_Female0
	Helmet_Iva_Standard_FemaleNRM0
	Helmet_Iva_Standard_Male0
	Helmet_Iva_Standard_MaleNRM0
	Helmet_Iva_VetBad_Female0
	Helmet_Iva_VetBad_FemaleNRM0
	Helmet_Iva_VetBad_Male0
	Helmet_Iva_VetBad_MaleNRM0
	Helmet_Iva_Veteran_Female0
	Helmet_Iva_Veteran_FemaleNRM0
	Helmet_Iva_Veteran_Male0
	Helmet_Iva_Veteran_MaleNRM0

// -----------------------------------------------------
// Jetpack
// -----------------------------------------------------

	Jetpack_EvaGround_Badass_Female0
	Jetpack_EvaGround_Badass_FemaleNRM0
	Jetpack_EvaGround_Badass_Male0
	Jetpack_EvaGround_Badass_MaleNRM0
	Jetpack_EvaGround_Standard_Female0
	Jetpack_EvaGround_Standard_FemaleNRM0
	Jetpack_EvaGround_Standard_Male0
	Jetpack_EvaGround_Standard_MaleNRM0
	Jetpack_EvaGround_VetBad_Female0
	Jetpack_EvaGround_VetBad_FemaleNRM0
	Jetpack_EvaGround_VetBad_Male0
	Jetpack_EvaGround_VetBad_MaleNRM0
	Jetpack_EvaGround_Veteran_Female0
	Jetpack_EvaGround_Veteran_FemaleNRM0
	Jetpack_EvaGround_Veteran_Male0
	Jetpack_EvaGround_Veteran_MaleNRM0

	Jetpack_EvaSpace_Badass_Female0
	Jetpack_EvaSpace_Badass_FemaleNRM0
	Jetpack_EvaSpace_Badass_Male0
	Jetpack_EvaSpace_Badass_MaleNRM0
	Jetpack_EvaSpace_Standard_Female0
	Jetpack_EvaSpace_Standard_FemaleNRM0
	Jetpack_EvaSpace_Standard_Male0
	Jetpack_EvaSpace_Standard_MaleNRM0
	Jetpack_EvaSpace_VetBad_Female0
	Jetpack_EvaSpace_VetBad_FemaleNRM0
	Jetpack_EvaSpace_VetBad_Male0
	Jetpack_EvaSpace_VetBad_MaleNRM0
	Jetpack_EvaSpace_Veteran_Female0
	Jetpack_EvaSpace_Veteran_FemaleNRM0
	Jetpack_EvaSpace_Veteran_Male0
	Jetpack_EvaSpace_Veteran_MaleNRM0

// -----------------------------------------------------
// Suit
// -----------------------------------------------------

	Suit_EvaGround_Badass_Female0
	Suit_EvaGround_Badass_FemaleNRM0
	Suit_EvaGround_Badass_Male0
	Suit_EvaGround_Badass_MaleNRM0
	Suit_EvaGround_Standard_Female0
	Suit_EvaGround_Standard_FemaleNRM0
	Suit_EvaGround_Standard_Male0
	Suit_EvaGround_Standard_MaleNRM0
	Suit_EvaGround_VetBad_Female0
	Suit_EvaGround_VetBad_FemaleNRM0
	Suit_EvaGround_VetBad_Male0
	Suit_EvaGround_VetBad_MaleNRM0
	Suit_EvaGround_Veteran_Female0
	Suit_EvaGround_Veteran_FemaleNRM0
	Suit_EvaGround_Veteran_Male0
	Suit_EvaGround_Veteran_MaleNRM0

	Suit_EvaSpace_Badass_Female0
	Suit_EvaSpace_Badass_FemaleNRM0
	Suit_EvaSpace_Badass_Male0
	Suit_EvaSpace_Badass_MaleNRM0
	Suit_EvaSpace_Standard_Female0
	Suit_EvaSpace_Standard_FemaleNRM0
	Suit_EvaSpace_Standard_Male0
	Suit_EvaSpace_Standard_MaleNRM0
	Suit_EvaSpace_VetBad_Female0
	Suit_EvaSpace_VetBad_FemaleNRM0
	Suit_EvaSpace_VetBad_Male0
	Suit_EvaSpace_VetBad_MaleNRM0
	Suit_EvaSpace_Veteran_Female0
	Suit_EvaSpace_Veteran_FemaleNRM0
	Suit_EvaSpace_Veteran_Male0
	Suit_EvaSpace_Veteran_MaleNRM0

	Suit_Iva_Badass_Female0
	Suit_Iva_Badass_FemaleNRM0
	Suit_Iva_Badass_Male0
	Suit_Iva_Badass_MaleNRM0
	Suit_Iva_Standard_Female0
	Suit_Iva_Standard_FemaleNRM0
	Suit_Iva_Standard_Male0
	Suit_Iva_Standard_MaleNRM0
	Suit_Iva_VetBad_Female0
	Suit_Iva_VetBad_FemaleNRM0
	Suit_Iva_VetBad_Male0
	Suit_Iva_VetBad_MaleNRM0
	Suit_Iva_Veteran_Female0
	Suit_Iva_Veteran_FemaleNRM0
	Suit_Iva_Veteran_Male0
	Suit_Iva_Veteran_MaleNRM0

// -----------------------------------------------------
// Visor
// -----------------------------------------------------

	Visor_EvaGround_Badass_Female0
	Visor_EvaGround_Badass_FemaleNRM0
	Visor_EvaGround_Badass_Male0
	Visor_EvaGround_Badass_MaleNRM0
	Visor_EvaGround_Standard_Female0
	Visor_EvaGround_Standard_FemaleNRM0
	Visor_EvaGround_Standard_Male0
	Visor_EvaGround_Standard_MaleNRM0
	Visor_EvaGround_VetBad_Female0
	Visor_EvaGround_VetBad_FemaleNRM0
	Visor_EvaGround_VetBad_Male0
	Visor_EvaGround_VetBad_MaleNRM0
	Visor_EvaGround_Veteran_Female0
	Visor_EvaGround_Veteran_FemaleNRM0
	Visor_EvaGround_Veteran_Male0
	Visor_EvaGround_Veteran_MaleNRM0

	Visor_EvaSpace_Badass_Female0
	Visor_EvaSpace_Badass_FemaleNRM0
	Visor_EvaSpace_Badass_Male0
	Visor_EvaSpace_Badass_MaleNRM0
	Visor_EvaSpace_Standard_Female0
	Visor_EvaSpace_Standard_FemaleNRM0
	Visor_EvaSpace_Standard_Male0
	Visor_EvaSpace_Standard_MaleNRM0
	Visor_EvaSpace_VetBad_Female0
	Visor_EvaSpace_VetBad_FemaleNRM0
	Visor_EvaSpace_VetBad_Male0
	Visor_EvaSpace_VetBad_MaleNRM0
	Visor_EvaSpace_Veteran_Female0
	Visor_EvaSpace_Veteran_FemaleNRM0
	Visor_EvaSpace_Veteran_Male0
	Visor_EvaSpace_Veteran_MaleNRM0

	Visor_Iva_Badass_Female0
	Visor_Iva_Badass_FemaleNRM0
	Visor_Iva_Badass_Male0
	Visor_Iva_Badass_MaleNRM0
	Visor_Iva_Standard_Female0
	Visor_Iva_Standard_FemaleNRM0
	Visor_Iva_Standard_Male0
	Visor_Iva_Standard_MaleNRM0
	Visor_Iva_VetBad_Female0
	Visor_Iva_VetBad_FemaleNRM0
	Visor_Iva_VetBad_male0
	Visor_Iva_VetBad_MaleNRM0
	Visor_Iva_Veteran_Female0
	Visor_Iva_Veteran_FemaleNRM0
	Visor_Iva_Veteran_Male0
	Visor_Iva_Veteran_MaleNRM0


All these files goes from level 0 to level 5 as this : 

	Helmet_EvaGround_Badass_Female0
	Helmet_EvaGround_Badass_Female1
	Helmet_EvaGround_Badass_Female2
	Helmet_EvaGround_Badass_Female3
	Helmet_EvaGround_Badass_Female4
	Helmet_EvaGround_Badass_Female5
		

The level textures are optional. If a level texture is missing, the one from the
previous level is inherited.

### Configuration File ###

NOTE: All options that can be configured in the GUI are saved per-game and not
in the configuration files. Configuration files only provide initial settings
for those options.

Main/default configuration file:

    GameData/TextureReplacer/@Default.cfg

One can also use additional configuration files; configuration is merged from
all `*.cfg` files containing `TextureReplacer { ... }` as the root node. This
should prove useful to developers of texture packs so they can distribute
pack-specific head/suit assignment rules in a separate file. All `*.cfg` files
(including `@Default.cfg`) are processed in alphabetical order (the leading "@"
in `@Default.cfg` ensures it is processed first and overridden by subsequent
custom configuration files).




Notes
-----

* Use DDS format for optimal RAM usage and loading times since DDS textures are
  not shadowed in RAM and can be pre-compressed and can have pre-built mipmaps.
* Try to keep dimensions of all textures powers of two.
* The planet textures being replaced are the high-altitude textures, which are
  also used in the map mode and in the tracking station. When getting closer to
  the surface those textures are slowly interpolated into the high-resolution
  ones that cannot be replaced by this plugin.


Known Issues
------------
* STATIC reflections are not recommended.
* Reseting a suit in the visor menu or in the suit menu will reset it for both.
* You need to get out/get in the vehicle or reload the scene to see the change in the IVA_safe and IVA_unsafe situation.
* female teeth texture use only the one from the 'DEFAULT_MALE', the male use the head's texture one normaly.
* the 'lvlToHide_TeethUp' setting works for both teeth for the male and normaly for the female.
* the 'lvlToHide_TeethDown' setting don't work for the male and work normaly for the female.
* the 'lvlToHide_Ponytail' don't work for the male (obiously).


Licence
-------

Copyright © 2013-2017 Davorin Učakar, Ryan Bray, RangeMachine, HaArliNsH

Permission is hereby granted, free of charge, to any person obtaining a
copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.
