# Farming-Game-Demo-C-Unity-

Youtube Page: https://youtu.be/SBhdLSRt6lM

This is a farming game demo by Jingbo Liu.

If you have any questions or anything wanna share or discuss with me, 
my email: boboliu0627@gmail.com


/////// THIS IS JUST A GAME DEMO WHICH I MADE FOR FUN ///////

What I've done in this demo:

Controller: Different controls and interactions based on what the character is holding. Generally, WASD for moving, B open/close the inventory bag, left mouse click for interaction with the ground.
    //My favourite part of this section is I assigned all grounds with unique id, use raycasting to check current ground all the time, and based on player's level, different tools with different levels can select different area of ground at once based on players position.

GroundSystem: Basically include everything of ground system except the ground manager which will mention later. 
    //I set four kinds of ground. Grass -> Dry -> Ready -> Wet, which each has different behaviour.
    //Ground is the base class which has the updatebydate virtual function can be override by its derived class, the Ground_Dry, Ground_Wet, Ground_Grass, Ground_Ready is derived from it.
    //SeedState should be attached on each seed, tracing its status, make change of its model by its status, also provide the harvest id when some one take it away.
    //My favourite part of this section is, the dry ground and grass ground will have different possibilities be planted by weeds and trees, also, based on the weather and time passed by, the dry ground is more possible changing to grass ground. Furthermore, for seed, I use a prefab which contains several models represent different status.

Inventory: I made a simple inventory bag ui and a quick access bar. You can drag between the bag and the quickaccess bar. Each time player click any item, it'll be equipped, and call the manager to detect which item is equipped in order to enable the right controller.

Manager: This is the CORE of this demo, which contains: 
EquipmentManager: Take equip item id, assign particular controller to player. 
GameManager: Control date, set weather based on season, everytime one day is passed, it'll reset everything.
GroundManager: Keep track of all the ground, when enable assign all ground an unique in order to quick access edit each ground. Also some features like swap between ground, get ground, select ground...
InventoryManager: Provide refresh ui feature so that everytime something change happen in inventory, it'll refresh the ui. Adding item by item id. Remove item by item id.
Warehouse: Collect all item and seed using dictionary. 

ScriptableObjects: the basic info for all objects and items. 

LeftCubeLevel: Don't be confused by the name, its just player's level of every tools.

PlayerInfo: Basic info of each player.

  
    
