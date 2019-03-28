# JENPY
JENPY experimental networking protocol YAY

# Grammar
```
nc localhost <port> VERB | key:value| key:value | .

. termination
| line separators
```

# Milestones


### Milestone 5 [MEH]
- Deploy to cloud

### Milestone 7 [N/A]
- Create a RSTR verb to restore from the disk

### Milestone 8
- Implement LRU for in memory cache of key value stores


## Past Milestones
### Milestone 1 [DONE]
- Set up async server to serve one basic key value word

### Milestone 2 [DONE]
- Implement ECHO verb

### Milestone 3 [DONE]
- Implement GET

### Milestone 4 [DONE]
- Implement PUT

### Milestone 6 [DONE]
- Create a background worker that periodically backs up in memory key values to disc


### Resources:
Databases
Graph
https://github.com/cosh/fallen-8

KeyVal
https://github.com/hhblaze/DBreeze

Microsoft Networking:
https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example

### Sources:
http://www.mikeadev.net/2012/07/multi-threaded-tcp-server-in-csharp/
