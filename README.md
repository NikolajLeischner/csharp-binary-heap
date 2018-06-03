This is a priority queue intended to be used within path-finding algorithms. I created it for a Unity-based RTS game where Unity's builtin navigation did not fit my needs.

For a binary heap implementation this should be about as efficient as it gets. For queues not larger than approximately 2^15 - 2^16 elements it is too my knowledge still the fastest algorithm for this use case.

In the context of path-finding in a video game, should this priority queue show up as a performance bottleneck, the most promosing optimisations would be:

* Prune your graph.
* Optimise the number of path queries.

# Implementing Dijkstra or A* without decreaseKey

Textbooks often show SPATH algorithms based upon a priority queue which also allows decreasing the key of priority queue elements. 

* You can use a hash set that stores visited node indices for this. If your graph is not too big, and nodes are identified by an int id in the range from 0 to N (as they should), you can also just use an array of bools.
* After dequeueing a node, check if it has already been visited. If yes, then skip it and dequeue the next node. If no, perform the next step of the algorithm, and mark the node as visited.

# Acknowledgement

This is mostly just a port of the C++ binary heap implementation found here: http://algo2.iti.kit.edu/sanders/programs/

The old but gold implementation tricks taken from there are:
* Use sentinel values instead of boundary checks.
* Assign everything you access in a method to a local variable to make the compiler's life as simple as possible.

# References

* A lot of good information on A* and path finding for games: https://www.redblobgames.com/
* Sequence Heaps: https://dl.acm.org/citation.cfm?id=384249
* Priority Queues with updates vs without updates: https://tutcris.tut.fi/portal/en/publications/priority-queue-classes-with-priority-update(7a05333c-64af-492f-a44b-186a1beb0cea).html
