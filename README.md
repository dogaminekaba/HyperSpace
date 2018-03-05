# HyperSpace
-A 3D mobile game I designed and developed during my second internship-

## Design of the Game

### General
Hyperspace is an endless mobile game
#### Design Pillars:
-	Three different gameplay option at the same time on the same screen
-	Different obstacles and related moves for each view
-	Teleportation feeling
#### Gameplay:
-	The player controls a spaceship while trying to collect items and avoid obstacles
- Player has shields which act like lives
-	If there is no item left on the current view or more valuable items pop up in another view player can tap to that view and change current gameplay
-	Game continues until player has no shields left
-	Score is up to the items that collected by player

### Controls 
#### Movement:
-	View 1: swipe down to go under the walls
-	View 2: swipe left and right to avoid vertical walls
-	View 3: swipe up to jump over the walls below
-	For all views: there are 3 lanes on the road and player can change its current lane by swiping left and right to collect objects.
#### To Change Gameplay:
-	Simply tap on the screen where desired view is located

### Character
-	The ship moves automatically and accelerates by time (until it reaches the maximum speed)
-	If it hits an obstacle game ends
-	It can pick up objects by moving to the lane that object stands

### Pick Ups
- Alien Trees: player collect alien trees for points, and there is a countdown for trees. If player doesn't collect a tree during this countdown, ship loses a shield and countdown restarts. If there is no shield left and countdown ends the game is over. 
- Shields: player can collect shields to avoid exploding when they hit obstacles


## Development of the Game

Unity Engine is used to develop the game. This was my first experience with Unity. What I learned:
- Adding animations to the 3D models
- Adding different scenes to a game
- Creating obstacles and pick up objects are easier to manage if a factory class is used
- "State" enumeration is also easier to handle the user input and character movement
- To handle user input Lean.LeanTouch is used

## Screenshots from the Game:

<p>
  <img src="https://github.com/dogaminekaba/HyperSpace/blob/master/HyperSpace/1.png" height="600"/>
  <img src="https://github.com/dogaminekaba/HyperSpace/blob/master/HyperSpace/2.png" height="600"/>
  <img src="https://github.com/dogaminekaba/HyperSpace/blob/master/HyperSpace/3.png" height="600"/>
  <img src="https://github.com/dogaminekaba/HyperSpace/blob/master/HyperSpace/4.png" height="600"/>
</p>

