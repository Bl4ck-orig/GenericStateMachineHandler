# GernericStateMachineHandler

Simplifies the implementation of statemachines. Also a lot of behaviour can be set in the inspector.

Developed in unity but there is no actual unity project.
There are only the scripts with a simplified example. 

Uses a blackboard system to communicate between states. The blackboard creates instances for entries in the blackboard which are able to execute actions. The evaluations and entries can be set and linked from the inspector.

Steps on how to use:

  1. - Create an Enum containing identifiers for each state.
  2. - Create an Enum containing identifiers for entries in the blackboard.
  3. - Create a class which implements IStateFactory. The CreateState method should implement all states clarified in the enumeration containing the states.
  4. - Create a class which derives from BlackBoard.
  5. - Create a class which derives from the StateMachineHandler class and use the enumeration of the states as first type argument and the enumeration for the blackboard entries as the second. In this class create an instance of the StateFactory and Blackboard created in step 3 and 4 and link them to the appropriate  abstract properties.
  6. - Implement the states.
  7. - Create blackboard entries and evaluations from the inspector in the implementation of the StateMachineHandler.

<br><hr><br>
Here is a screenshot of the setting in the inspector of the example provided.

<center>
![Example](https://user-images.githubusercontent.com/38137603/166268922-dfb1b30d-e985-4960-b6f7-d7ad1ab288c0.png)
</center>
