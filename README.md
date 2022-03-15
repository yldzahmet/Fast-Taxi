# Fast Taxi
#### Description: Fast Taxi is game basic taxi game. In a straight road  we drive and looking for a customer. When we see it then stop in front of it and game ask us to are you accept customer.

### Game Rules
We have following attributes in game:
* **Durability**
* **Comfort**
* **Reputaion Points**
* **Cash**
#### **Durability** like a healh in games, it's active all times when we driving. If we hit other cars durability will decrease a bit. if it ran out we cant get a new customer, and game over if we had no money to such repair it.
#### **Comfort** is important while driving with a customer. It's draining if we exceed 130 km/h speed continiously and decrease a bit if we press breake button to long ,about 0.75 second. If Comfort ran out while driving our customer leave taxi immediately and we loose reputaion points which is our main score.
#### We acquire **Cash** and **Reputaion Points** if we succesfully carry customers to it's destination.

There is e menu button right up side of screen. In that menu there are buttons; repair durability, drop passenger, save game , quit game.
**repair durability** is using for refill durability value for a cash cost.
**drop passenger** button is using for leave customer any time while driving. if we drop customer we will lose some reputaion point
**save game** button save game with current reputaion and cash values
**quit games** quits the application.

Fast taxi is created on Unity

our scripts in "Fast Taxi\Assets\Scripts" folder
there are :

- **CameraFollow**
- **Destination** 
- **ManagePassenger**
- **Manager**
- **OtherCarControllers**
- **RoadGenerator**
- **SoundManager**
- **SpawnOtherCars**
- **TaxiCaller**
- **TaxiController**

All scripts written in C#.

CameraFollow > file moving camera continiously with our taxi.<br />
Destination > script manage destionation points behaves when taxi came in.<br />
ManagePassenger > is on our taxi object and generaly handle customers spawning and drop passenger functions<br />
Manager > is our global game manager script. Holds all global values. Controlling main game loop. Control many of UI transitions. calculating and printing to screen of durability, comfort, cash and reputaion point. Setting up where destination point will be and how visible is.<br />
OtherCarControllers > script control other cars behaviors.<br />
RoadGenerator > generate continiously road in forward way of taxi.<br />
SoundManager > manage sound files.<br />
SpawnOtherCars > controls spawn time and tranform point of other cars in scene.<br />
TaxiCaller > attached to customer and control of spawning time and location and allow to pop up UI elemens related accepting customer.<br />
TaxiController > contain controller for taxi.




