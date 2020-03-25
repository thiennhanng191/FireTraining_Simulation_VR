Thank you for purchasing this Complete Breakable Glass Solution!


--------------------------------------------------------------
IMPORTANT:
***-raycast script must be on, or assigned to, a camera.***

-if using a different raycast system – simply call 'BreakFunction()' on any object with a 'Breakable script attached.)


to listen to this instead of reading it, close this and open the 'listen-to-me' .ogg file alongside this ReadMe.
--------------------------------------------------------
/*
Inside these folders, you'll find quite a lot. First, lets delve into the prefabs folder.
Inside, you'll find whole, unbroken windows and props, and folders full of shattered replacements.
In the 'Windows' folder you'll find three more folders containing prefabs for hi-poly, low-poly, and 
mobile prefabs.. 
These are all identical - except for the shattered replacements that they call. When the break function is 
called on The "hi-poly" models The original window is instantly replaced with a random selection from 
one of 3 hi-poly shattered replacement prefabs composed of 50 shards.
When the break function is called on The "low-poly" models The original window is instantly replaced 
with a random selection from one of 3 low-poly shattered replacement prefabs composed of 25 shards.
For the mobile windows, the original prefab is instantly 
replaced with a extra-low poly replacement composed of only 12 shards. 

The Glassware folder contains the new props from the 1.1 update. These are all Low-Poly, and break into 16 shards each.

You should really never have to do anything with any of the shattered replacement prefabs, as they will 
instantiate at the same location, scale and rotation as the original. Also, Whatever material you drag 
onto the original window will be inherited by the shattered replacement immediately upon instantiation. Also, the shards' rigidbodies' mass and drag change automatically with the scale of the original model.
Because the original models and all of the shattered replacements share the same UV map, the textures 
placement on the original will not change for the replacement. you can easily 
make and apply your own textures and materials. The Textures for the bottles are designed to be re-designed to fit the need of the end user.
Speaking of materials, I have included several high-quality shaders for you to use To make your own diverse
prefabs. Several dirty, scratched and wet shaders, as well as a fully realized Physically-based stained 
glass shader that looks pretty sweet. All textures were produced in GIMP with the Insane-bump plugin, 
and are included in this package in their own folder. 
Also included are 19 sound effects, recorded by me in 16 bit, 44100 hz (cd quality). 10 sounds for 
cracking, and 9 sounds for shattering. These are played at random when the break function is called, 
based on certain parameters.

The Breakable script, located in the 'Scripts' folder, is a simple yet powerful tool that allows you to shape 
the behavior of the breaking glass to your specific need with just a few clicks. In the inspector in Unity,  
you can check or uncheck options such as whether the shards clean themselves up - the average shard 
lifetime before it is cleaned, wheather the glass repairs itself, and how long that will take. Whether the 
glass will respond to an impact with another gameobject, and 
what the velocity of the involved rigidbodies must be before the glass will break.  You can also choose to have the 
glass crack before it breaks. When a window cracks, it instantiates the shattered replacement, but all the 
shards stay in place instead of shattering and flying away. 

The RayCaster script, also in the Scrripts folder, has some options for instantiating explosions, and affecting nearby rigidbodies and breakables. this package DOESN'T COME WITH an explosion particle effect, but if you download one of the readily available (and free) particle effects from the asset store, all you have to do is assign it in the inspector. The functionality is already built in.

The demo scene has a lot of examples of different uses for the prefabs and scripts in this package, but the possibilities are many. Happy Glass Breaking

I have tried to make this as generally useful 
as possible, but inevitably, some of you will require functionality that I haven't thought of. For this 
purpose, I have included spaces in the (heavily commented) scripts for you to add your own code in (c#). 
These spaces will stay in the code for future updates, so you can download the update and easily copy 
and paste your own modifications back into the code without fear of them being lost.


Speaking of udates - this package will be regularly updated according to community response - that is 
to say, if you think something should be included in this package that was not - email me at 
mr.patrick.ball@gmail.com, and I'll most likely add your suggestions to the best of my ability. If you have any questions or need further support, please do not hesitate to contact me at MR.PATRICK.BALL@GMAIL.COM Again - 
thank you for purchasing this package. 
*/
