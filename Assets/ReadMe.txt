Encapsulation - the "building" block of the game
	keeps track of the Specification(s) assigned to it
		I know I want to allow the limitations of Specifications by "theme", but I don't know if I want a single encapsule to have multiple specs assigned at once.
	storage determines how many items this encapsulated pocket space can handle. 
		storage is assigned by the specifications
		the limitation could be based off "complexity", not certain if I want to do that as a vague value, or also allow there to be tiers of complexity that it can handle. Limiting "large" resources from being used with encapsules that can only handle "simple".
		resources might also be allowed to have an ability to be regestered as a single GameObject, with a value of how many of itself are there. Essentually having stacks.
	I might also allow the Encapsulation to have it's own Input and Output variables, with a minimum requirement being set by the Specifications. If not, then the Specifications could (sometimes) have a range for both of them.

Specification - the rules set for an Encapsulation
	Keeps track of what the input and outputs are, including what type of resource, and how much
	Keeps track of the (duration???), how long it takes to change the input into the output
	Keeps track of storageSize, which is enforced on the Encapsulation...
		I might just make Specification code part of Encapsulation... especially because storage is on the encapules, not the specs

PlayerController - data I want each individual player to track
	I want to do the interact idea I've been using for the Lv1 classes
		currently I'm using the Raycast check if clicking screen approach

GameController - handles all the data individual objects wouldn't track
	Currently I just have an ability to quit the game



KNOWN ISSUES:
Manual Production can sometimes make resources go to negative
Automation has a potential chance to consume a resource it isn't producing
Screen space regions are not auto-sized per aspect ratio
Automation cycle before 1s might be added for free instead of waiting for next cycle
Completed production doesn't show yellow text
The Manual Production can get stuck
There was one WebGL build where I didn't get a single "E", I only tried about ~200-500 times
Resource "B" is highlighted when DDL is open???