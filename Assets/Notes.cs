/*
29 OCT 2015 2:51pm

Previously learned how to use AudioSource.GetOutputData() to get amplitudes.
	-Used it for gameObject size fluctuation
	-Used it for gameObject sprite color redrawing

Also learned Audio.pitch can be used to increase pitch.
						
Now:
	-Need to represent it in an attractive way
	-Also need to be more interactive
	-Also more efficient

By:
	-Pancakes
	-Radially Increasing Sprites
	-Snake on Vinyl

Interact:
	-Explode to the beat
	-Draw direction

Share:
	-Export as a shareable
	-Save to play again
*/
 
/*
TRACE: 29 OCT 2015 2:51pm							
	Pancakes								
	Radially Increasing Sprites		29 OCT 2015		
	Snake on Vinyl							
	Explode to the beat				29 OCT 2015	
	Draw direction
	Export as a shareable
	Save to play

RESULT: 29 OCT 2015 4:37pm
	Radially Increasing Sprites
		-issue with origin (from corners instead of center of plane)
	Explode to the beat
		-didn't get to it
 */

/*
TRACE: 30 OCT 2015 2:00pm
	Radially Increasing Sprites		29 OCT 2015	
	Explode to the beat				29 OCT 2015	

RESULT: 30 OCT 2015 5:16pm
	Radially Increasing Sprites
		-Changed origin via material offset
		-Reset the radius to zero once the object's width has been exceeded
	Explode to the beat
		-Attempted to attach object's scale with beat, got dizzy
		-Attempted to increase presence of beat by making the radius static (nice effect)
NEXT:
	Maybe find some way to make the melody make better sense visually?
		-via detecting dominant pitch, and changing vertical placement maybe? (
 */

/*
TRACE:	02 NOV 2015 4:00pm
	Radially Increasing Sprites		29 OCT 2015
	Explode to the beat				29 OCT 2015
	Draw Direction					02 NOV 2015

RESULT:	02 NOV 2015	4:56pm
	Radially Increasing Sprites
		-No progress, watching it gives me a headache, need to use Math.Lerp properly
	Explode to the beat
		-No progress, waiting on Draw Direction to make sense of this one
	Draw Direction
		-Was able to use LineRenderer, have it grow, and spike it depending on amplitude

NEXT:
	Need to find a way to represent melody via LineRenderer
		-How about have it spiral, when the user taps to the beat, it creates a branch in front with a fruit that radiates
		-The main branch then continues onto a new radius (also increased dependent on the beat)
 */

/*
TRACE:	05 NOV 2015 12:30pm - 6:30pm

RESULT: 
	Used LineRenderer that spirals outwards to represent the waveform (the z being the amplitude.)
	LineRenderer speed slowed down by decreasing the angle according to the increasing radius (360 / (360*radius))  = angle per arc that represents a frequency
		The frequencies don't radially align, but that's fine. Better than being dizzy.
	Created a method that creates a new spiral and re-centers it at a random center via offsets.
 */

/*
TRACE:	08 NOV 2015 12:30pm - 5:30pm hours

RESULT:
	Got beat detection working! Using AudioProcessor from http://pastebin.com/myXiu97R via Reddit
 */

/*
TRACE:	10 NOV 2015 12:45pm - 8:30pm 
		Good job today!
RESULT:
	Beat detection was a complete bust! It wasn't accurate at all. 
	Decided to have the user tap the keys to the beat of their songs, then match the songs that way.
	Got it to find a matching user beat!
	Got it to transition at the start of the similar beat!
	Made the cylinder more vibrant by scrolling through a set of radii when at a certain amp.
NEED:
	To give the user a choice on which "matched set of user beats" to start with.
	Need to save the user beats of each song.
	Need to make the song access, start, stop, restart more organic. (Just hardcoded for now.)

	
 */