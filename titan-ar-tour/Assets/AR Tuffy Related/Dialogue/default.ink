-> main

=== main ===
Hello.
This is the default text. Please use or create an appropriate dialogue file for your prefab. Use Inky.
The following text will be an example:
You are here to choose a pokemon!
Are you ready???
Which pokemon do you choose?
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulbasaur")
    + [Squirtle]
        -> chosen("Squirtle")
        
=== chosen(pokemon) ===
You chose {pokemon}!
-> END