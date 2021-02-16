# Unity Scripts

## Basic Player Controller

Adds basic physics controls to a player object. Uses the direction of a camera target object (that the camera is parented to) to determine the direction that force or velocity can be applied to the player object (see Camera Controller).

## Camera Controller

Follows a camera target that follows the position of the player object, but not the rotation. This is useful if the player object has rotations that you don't want to apply to the camera. It also separates the game player object from the object the camera is tracking to. 

## Extended Player Controller

Extends the basic player controller by adding the ability to jump and move only when the player object is grounded. Also adds the ability to shoot projectiles in the direction that the player object is facing.

## Track Object

Makes a rigid body object constantly turns to face a 'target' object (such as the player). Can also be used to make the rigid body move towards the position of the target object. 
