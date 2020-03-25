EXCELSIOR ASSETS 
CUSTOMIZABLE SCIFI HOLO INTERFACE (CSFHI)

README - Setup and Documentation v.1.6.2

-----------------1.6.2-----------------
- Add: ForcedUnscaled time toggle on InterfaceAnimManagers Components (useful when changing timeScale for pause menus)
- Improve: remove various warnings with proper #UNITY tags in code
- Add: legacy postProcessBase and postProcessHelper scripts to avoid errors with users not using them

-----------------1.6.0-----------------
- Add: OnLevelWasLoaded replaced with delegate for Unity 5.4 and newest
- Add: New graphical elements (in progress)
- Add: Callbacks ExampleListener.cs with start/end appear/disappear events added
- Fix: "GetLayerCount / Animator is not playing a Playable" rare error happening
- Fix: AnmElement sometimes not triggering their 'appear' state at the end of animation
- Fix: Slight wait time if disappear called right after the whole IAM appeared
- Improve: various scripting syntax

-----------------1.5.2-----------------
- Fix: instantiating a prefab containing InterfaceAnimManager was triggering errors
- Add: The InterfaceAnimManager inspector can be now edited even when it's in a prefab in the resource browser
- Improve: AnmElements now gets a monobehaviour rather than a scriptableObject, any new IAM will follow that rule

------------------1.5------------------
- Change: speed delay multiplier redesigned -> becomes the time length multiplier (values inverted)
- Fix: call disappear, cut it with a direct disappear and start again won't make missing anim anymore
- Add: optional nested interface anim managers support
- Add: specific 'wait' mode triggerable for the nested IAM
- Add: new 'digital nest' holo interface example added
- InterfaceAnmElement.cs : simplified & more coherent animation states 
- InterfaceAnimManager.cs rewritten : disappear end more precisely detected
- InterfaceAnimManagerEditor.cs better handling of invert delays
- Interactivity added on 'Meticulous Lock' example
- Interactivity added on 'Archeologic Inventory' example
- Add: two new sounds added
- Improve: circle08.psd polished
- Improve: darkener wallRing.mat
- Improve: new hierarchy on some scripts that use the inspector(/CSFHI/ added)

------------------1.1------------------
- new circles graphs
- new lock graph
- new arrow block graph
- new associated animation for arrow blocks
- new holo interface example
- polish layer names in .psd files
- target set in color example scene

------------------1.0------------------
- original release


Thank you for purchasing Customizable Scifi Holo Interface Package from Excelsior Assets!

Following the new guidelines of the asset store, you need to manually import the Standard Assets IMAGE EFFECTS Package.
Import that package by going to Assets -> Import Package -> Effects
The folders that need to be imported are:
Editor -> ImageEffects and Standard Assets -> Effects

--------------------------------------------------

Once the Images Effects have been imported, add the scenes in build (CTRL+Shift+B) contained in:
Excelsior/CSFHI/Scenes
Inside CSFHI_Showroom.Unity, search for the 'ExampleCam' Object, in the inspector there should be 3 missing scripts that have been replaced by:
'Color Correction Curves', 'Bloom' and 'Screen Space Ambient Obscurance' 
If they're disabled. Enable them.

Now you can fully launch the CSHI_Showroom scene to test it.
Since the lighting is baked, you need to build the game to see the 'change environment' function working properly

--------------------------------------------------

You can directly copy holo interfaces contained in the scene to any other scene.

HOW THE INTERFACE ANIMATOR COMPONENT WORKS:
This component act as an animator interpreter:

// Step 1: 
It takes all the childs of the interface container (but not the ones nested inside those same child) and analyze their animators.

// Step 2: 
It lists all the childs and set them 'SetActive' to TRUE one by one in their delay order when calling the startAppear() function.

// Step 3: 
If the child has an appear animation in his animator, it will be triggered when the child is enabled.
There is example of holo interface childs in the showroom holo interfaces but also in : Excelsior/CSFHI/Prefabs/ExampleElements/
Select one of them with the Animator Panel open to look at how animations are handled.
The Entry is directly linked to the appear animation.

// Step 4: 
When calling startDisappear() function. The component will search for an animation with the word "disappear" in it. (main anim layer)
If it finds it, it will play it and wait for the animation to be finished. When its done, the object will be is 'SetActive' to FALSE.

Notes: 

- in the animation inspector, motion name must be the same as the animation
- high speed multiplier tend to break the animation system of unity, be careful with those
- nested IAM are automatically named
- nested InterfaceAnimManager (IAM) are implemented due to request, but are not especially recommended, the system is not intended to work this way in the first place
- when editing a holo interface with nested IAM inside, please get back to the root of the holo interface and click on the 'recompute nested IAM' button
- if you get an error 'adding system: data not available' after changing environement in example scene (playMode), it's a unity bug they've fixed in subsequent versions

--------------------------------------------------

For any inquiry or suggestion, please get in touch with me on the forum page : http://forum.unity3d.com/threads/customizable-scifi-holo-interface-released.427764/

You are not authorized to re-use the showroom to sell assets. It is exclusive to Excelsior Assets. Thanks for your understanding.

If you're satisfied with the product, please don't forget to give it a note on the asset store page : https://www.assetstore.unity3d.com/#!/content/69794
This is really helpful to support my work on the product and see more of it coming in the future. (buying it doesn't really tell me if you're satisfied with it ^^)

Thanks for your support,
Alexis F. (Immu)