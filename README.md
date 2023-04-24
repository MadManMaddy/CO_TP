# CO_TP
Test repository for assignment provided by Crossover

Created a sample jenga metagame prototype

1.  Spawns jenga blocks as per the API response
2.  Spawns buttons on left top corner to select grades (bug: extra one button for separate stack but thats the error in API response)
3.  jenga blocks are divided into 3 colors : 
  stone => dark brown
  wood => dark yellowish
  glass => off white
4. spawns 2 buttons to 
  refresh the stacks again: destroys everything and respawns new stacks as per ai response
  TestMyStack : removes all glass stacks

Code Structure:
didn't use any specific optimized structure for this prototype. 
It uses singletons for managing game session.
But naming and structure is maintained in a way that it cna be converted to a service locator patttern using a single singleton
Most of the references are currently stored in a script called Global Context
For buttons shown both styles of assigning on inspector and through code,(later can be converted to be completely handled inside code)


