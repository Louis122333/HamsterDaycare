# HamsterDaycare
An Entity Framework and database project

SUT20 April 2021
Examination Advanced .NET

Instructions:
Build a simulated hamster daycare that handles 30 hamsters correctly based on the instructions given.
* All data and status updates is saved to a SQL-database that is handled through Entity Framework Core - Code First or Database First (I chose Code First)

* Create a structure for the application, it should be split into atleast two assemblies.

* Create a github repository and commit progress.

* During the day, the hamsters will:
  Arrive, Move to and from cages, Exercise and be sent home (departure).
  
* Upon departure we want to know what the hamsters have been doing and how many times they've exercised.
  Also, for how long they had to wait for their first exercise.
  
* The days are simulated by ticks, that is translated to a DateTime-based TimeStamp 
  100 ticks equals a day (07:00 - 17:00)
  1 tick equals 6 minutes
 
* Create an import function for the hamster data (read from file, seed through EF, or create another function to initialize)
  Make sure not to fill the database more than once.
  
* Create a UI to set how many days to simulate and how fast each tick should be printed.

* Create logic to relocate hamsters from cages to exercise.
  A cage can hold a maximum of 3 hamsters, and 6 hamsters can exercise at the same time (for atleast one hour).
  Cages and exercising has to be gender-segregated.
  
  A sample of the final output:
  ![image](https://user-images.githubusercontent.com/74004258/114287592-3f0c9e80-9a68-11eb-8f6c-8e5c7a61b488.png)

  
  
  
  

  
  





