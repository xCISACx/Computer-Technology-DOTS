# Computer-Technology-DOTS

Create a small space shooter game with the following features:

Simple movement:
> I use WASD to move the ship and it rotates in the direction you're moving.

Shooting:
> I use the Space bar to shoot projectiles.

Waves of enemies:
> Asteroids spawn at the start and, when destroyed, they get pooled and teleported outside of the game bounds, moving towards the center and being able to be destroyed again.

My loops are short and don't have a negative impact when employed at scale.
All my systems use the Burst Compiler for performance.
The game has 1000 asteroids at all times, staying above 200 FPS on an RTX 3060 Mobile + Ryzen 7 5800X configuration.

At first, I was using the Unity Physics package for DOTS.
This package made my performance drop considerably, so I opted to check positions instead in the end.

These are screenshots of the profiler when using the Physics package:

![image](https://github.com/xCISACx/Computer-Technology-DOTS/assets/37281623/fc3a0289-042c-4596-a281-0e963af491a1)

![image](https://github.com/xCISACx/Computer-Technology-DOTS/assets/37281623/9f9ac612-3119-404f-9a85-aa77e668fd49)

![image](https://github.com/xCISACx/Computer-Technology-DOTS/assets/37281623/e6492d31-3640-4566-844f-9a36c7e16a22)


And this is after switching away from the Physics package:

![image](https://github.com/xCISACx/Computer-Technology-DOTS/assets/37281623/92bcaf78-dd24-4166-b2ae-1243e9db7521)

