// Allows variables from the Global Ink file to be referenced.
INCLUDE Globals.ink

{ dayCounter == "1": -> day1 | -> null }

=== day1 ===
Welcome to the example dialogue screen prototype. I'll show you what's included. #speaker:Helpful Hannah
For example, here's some color text! #color: red
And now onto the choices. #color: white
Here's how the choice screen looks.
* [Choice 1]
* [Choice 2]
* [Choice 3]
- And then of course the text after choices.
-> continue
== continue
I'd like you to take notice of the name tag.
This can be changed at any time. What's another 'H' name...
Ah, here we go. #speaker:Helpful Hilda
You can also change the character like so. #chara:dad #speaker:Dad
And change the expression! #chara:dad #spr:happy
One last thing. #speaker:Helpful Hannah #chara:helpful
You can change the dialogue to be an internal thought. #mc:true
And then of course, switch back to speaking. #mc:false
Oh. Actually I should mention, choices don't have to have three options. That was just an example.
Why don't I show you one that has two choices?
Here. What's your favorite fruit out of these options?
* [Apples] -> choice1
* [Oranges] -> choice2
== choice1
Cool! Apples are great.
That's all!
-> DONE
== choice2
Awesome! I'm a fan of oranges too.
That's all!
-> DONE

=== null ===
Test unsuccessful.
-> DONE