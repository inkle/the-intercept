VAR moneyInPocket = 0
VAR moneyInBank = 0

-> main

=== main === 
//  Intro
	- (newDay) It's a {&Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday}. You're {describeWealth(moneyInPocket)}.
		+ {newDay % 7 <= 5} [Go to Work]
			-> work.GoToWork
		+ [Do nothing.] You decide to watch TV all day.
			-> newDay
		{moneyInPocket > 0: 
		+ [Go to the bank.] 
			You decide to watch TV all day.
			-> goToBank
		}
		+ {goToBank >= 3} [Win!]
			You win!
			-> DONE
		
	
	- (goToBank)
		+ Deposit
			~ moneyInBank = moneyInBank + moneyInPocket
			~ moneyInPocket = 0
			-> newDay
=== work === 
	- (GoToWork)
		You're at work.
		+ Work hard
			-> workHard
		+ Doss
			-> dossAtWork
		~ moneyInPocket = moneyInPocket + 1
	- (dossAtWork)
		You spend the day watching the clock in the break room.
		-> finishWork
	- (workHard)
		You're hard at work. You barely notice when it's time to leave
		-> finishWork
	- (finishWork)
		~ moneyInPocket = moneyInPocket + 1
		You've earned a penny! You're {describeWealth(moneyInPocket)}.
		-> main.newDay

 // === function Bank(ref x)
 	// ~ x = x - 1

=== function describeWealth(x) ===
{
- x == 0:
    ~ return "penniless"
- x == 1:
    ~ return "not poor"
- x == 2:
    ~ return "comfortably wealthy"
- else:
    ~ return "in serious need of a bank"
}