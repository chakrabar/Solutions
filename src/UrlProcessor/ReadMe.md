This document explains the application architecture and code logic in brief.

### Application architecture

The basic architecture of the application is pretty simple. The API/Web layer (UrlProcessor) has 2 controllers 

1. `ProcessController` with the `POST` API
2. `QueueStatusController` with the `GET` API

This layer depends on the `AppContracts` layer (which is just an abstraction with interfaces) for all logic/processing. The `Core` layer (application folder) has the main domain logic, which implements the interfaces in `AppContracts`.

The wiring of `AppContracts` and `Core` is handled by IoC container. I have used the default IoC container of .NET Core.

The `Infrastructure` layer has some domain-agnostic implementation e.g. a web client.

The Models layer has 3 types of models,

1. `DomainModels` - domain specific models used in `Core`
2. `ViewModels` - models exposed by the `API`
3. `SharedModels` - some common types, shared between APIs and Core

```fs
                       GET  POST
                        +    +
                        |    |
+-------------+       +-+----+--+        +--------------+
|             |       |         |        |              |
| ViewModels  <---+---+   API   +--------> AppContracts |
|             |   |   |         |        |              |
+-------------+   |   +---------+        +-----^--------+
                  |                            |
                  |                            |
+--------------+  |                      +-----+--------+
|              <--+                      |              |
| SharedModels |                         |   App Core   |
|              <--+----------------------+              |
+--------------+  |                      +-----+--------+
                  |                            |
                  |                            |
+--------------+  |                     +------v----------+
|              |  |                     |                 |
| DomainModels <--+                     | Infrastructure  |
|              |                        |                 |
+--------------+                        +-----------------+
```

### Main componentas

The main logic is handled by 2 components (classes) in the `Core` layer:

1. **`ProcessQueue`** - it is a custom queue for our specific purpose. It has an internal `Queue` that actually stores the requests, and serves for later processing. It also has a `Dictionary` that stores the status of each item in queue. Each item in queue is a bunch of URLs with an ID. When items are added to the queue, their status is updated as 'QUEUED'. It also provides methods to mark specific items as 'COMPLETED' or 'FAILED', that is used by the next component. This status changes are internally updated in the dictionary. When status is requested for any queue item, they are read from the dictionary.
2. **`UrlBatchProcessor`** - this component runs in the background and actually processes the resources/URLs. It dequeues items one by one, and starts processing them. Initially it marks the items as 'INPROGRESS', and updates as 'COMPLETED' when all URLs in the batch are successfully read. If something goes wrong, the batch is marked as 'FAILED'. This process runs on separate (ThreadPool) thread, and handles concurrency. 

### Working Principle

On POST of resources, they are queued up in `ProcessQueue`. Also the `UrlBatchProcessor` is triggered. If the batch processing is not already running, it is invoked. The request queues the resource/URL batch and returns a batch ID for future reference, along with current status of the resources and a request status as success/failure.

The background process reads all the resources and updates the status accordingly.

On GET request, the staus is returned for the specific batch ID.

### Tests

Unit tests are written to test the application logic, for different test cases. Tests cover the API project (UrlProcessor) and the Core projects, as only these projects have real application logic.

There is total 16 tests cases, API - 5 & Core - 11.