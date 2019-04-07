# Bowling game technical task

| 1 | 4 | 4 | 5 | 6 | 4 | 5 | 5 | 10 | 0 | 1 | 7 | 3 | 6 | 4 | 10 | 2 | 8 | 6 |
| ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ | ------ |
| 5 | 5 | 14 | 14 | 29 | 29 | 49 | 49 | 60 | 60 | 61 | 61 | 77 | 77 | 97 | 97 | 117 | 117 | 133 | 133 |

The game consists of 10 frames as shown above. In each frame the player has wto opportunities to knock down 10 pins. The score for the frame is the total number of pins knocked down, plus bonuses for strikes and spares.

A spare is when the player knocked down all 10 pins in two tries. The bonus for that frame is the number of pins knocked down by the next roll. So in frame 3 above, the score is 10 (the total number knocked down) plus a bonus of 5 (the number of pins knockeddown on the next roll.)

A strike is when the player knocked down all 10 pins on the first try. The bonus for that frame is the value of the next two balls rolled.

In the tenth frame a player who rolls a spare or strike is allowed to roll the extra balls to complete the frame. However no more than three balls can be rolled in tenth frame;

### The Requirements
| Game |
| ---- |
|+ roll(pins :int) |
|+Score() : int |

Write a class named "Games" that has two methods
  - roll(pins:int) is called each time the player rolls a ball. The argument is the number of pins knocked down
  - score() : int is called only at the very end of the game. It returns the total score for that game.