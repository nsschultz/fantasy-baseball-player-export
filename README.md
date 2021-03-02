## Player Export Service
* This source is used to convert the contents of the Player service into a CSV file that can be consumed by the legacy system.

---
### Endpoints:
* `GET api/player/export` - Gets all of the player data as a CSV file.

---
### Healthcheck:
* The service will fail if it cannot connect to the Player service. 