
//  === function lower(ref x)
//  	~ x = x - 1

//  === function raise(ref x)
//  	~ x = x + 1

=== test === 
//  Intro
	- (intro) 
		{not intro} The casino has mostly filled out. Stands of smoke hang in the air, waiting for the flow of money.
		* [Check out the tables.] Blackjack and poker have beaten you already, and you still hold a grudge.
			-> intro
		* [Check out the waitresses] The cocktail gowns flock to the sounds of fresh money.
			-> intro
		* (lookAround) [Check out your surroundings] A corner of your room grasps your attention. The lack of lighting and excitement attract you, likely for the same reason your eyes slid off it before.
			-> intro
		* {lookAround} Investigate
			You stroll towards a previously unnoticed corner of the room.
			-> towardsTheTable
	- (towardsTheTable)
		- "Engleesh!" That's as good as singling you out by name. 
		// * [Wander towards the back of the room.]
		* [Look around.] 
			3 men sit around a round table holding cards. One of them, red faced and smiling, waves and beckons you with his free hand.
			* * [Walk towards the men] You smell stale beer thick on the table, which is surrounded by worthless chips and empty bottles. Apparently the waiter hasn't been here in a while. The man gestures towards the empty seat.
			 	* * * [Pull up a chair] 
				As you move to sit, one of the men, tall in his seat, throws his cards on the table, scowls, and mutters under his breath. 
				* * * * [Sit down] 
					The man with the red face grins as you pull the seat in behind you, and throws some chips from the tall man's pile into the center.
					* * * * ["What are we playing?"] 
					Still smiling, the red faced man pours you a wide glass of dark liquid from a bottle that vanishes as quickly as it appeared.
					-> startGame
	- (startGame)
		- The third man gestures with his cigarette at the pile and deals 5 cards in front of the players, face down. 
		* [Look at the red faced man for an explaination]
			The red faced man gestures as his chips, and throws a low value one into the center of the table.
		* [Pick them up]
			As you move to touch the cards, the tall man man slaps your hand and spits something at you. It sounds Slavic - Slovakian, or Ukrainian, maybe.
		* [Place a chip on the table.]
			You take a low value chip from your pocket
		* Wait
		-> DONE