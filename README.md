# 7DRL_2017

2017 7 Day Roguelike Project

Using Unity 5.5.2f1 (Version freeze for duration of 7DRL challenge)

Overview:
Orb Temple (name subject to change) is going to use some of the ideas from my previous 7DRLs, Rogue Space Marine (2015) and Helix (2016).
Like my previous games, this will be a boiled down game with heavy emphasis on one hit-point tactical combat.
The basic concept is that the player is limited to two actions: move, or throw an orb. There are several types of orbs that can be thrown, each of which has varying effects, such as damage, stun, slow, repel, or confuse. The catch is that the player doesn't have control over which orb is thrown - the orbs will be queued up in a random order, sort of like the way blocks are queued up randomly in Tetris.

Here are some things I'd like to keep from my previous games:
- Bullet dodging
- Turn based mechanics with a real-time "feel"
- Cool-down timers on abilities
- Densely packed levels that thrust the player into immediate combat
- Punishingly high difficulty but hopefully a high level of replayability

Things that I'd like to improve upon from my previous games:
- More "readable" game state so that it is clear to the player if their attack will hit or miss
- More "readable" game state so that it is clear to the player if they will be hit on the next turn
- No crashes on level loading! (Helix was prone to this...)

Development TODO:
GameManager singleton
	Keeps track of state of game (start menu, playing, paused, game over screen)
	Keeps track of state of turns (PlayerTurnInput, PlayerTurnExecute, EnemyTurnInput, EnemyTurnExecute)
InputManager
	Feeds commands to player

Actor
	Any object that can move and execute actions
Player : Actor
	The player-controlled character
	Gets input from InputManager and executes turns on 
Enemy : Actor
	The AI-controlled non-player-characters
	Not necessarily hostile
	Can load different AI objects for different behavior
Projectile : Actor
	Any object generated by a player, enemy, or trap that can collide with other objects and actors to cause damage or other effects

	Architecture
	Anything that makes up a level
Floor : Architecture
	Passable
	Can see through
Wall : Architecture
	Unpassable
	Cannot see through
Obstacle : Architecture
	Can see through
	Passable for Projectiles
	Unpassable for other Actors

LevelGenerator
	Creates levels that are fun to play and populated with Enemies
	
Node
	Single tile in game world
Grid
	A 2D array of Nodes
Pathfinding
	An object used for pathfinding (A*) between Nodes in the game world