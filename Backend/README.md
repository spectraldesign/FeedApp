# DAT250 - Assignment C

Group Project Dat250 - Group 3 Fall ´22

| Participants| 
| ------------|
| Jonas Schjøtt Olsaker | 
| Malin Iversen         |
| Petter Johansen       |
| Skjalg Eide Hodneland |


This repository contains a maven project that contains the first prototype of a domain model for a FeedApp. It uses JPA and Derby to define the entity classes in order to store information about user, polls, votes etc. in a database. The domain model can be found in [this repo](https://github.com/P1T1B0Y98/DAT250-AssignmentA) alongside the design document and other models for our application. 

## Diagram
We have three entities and the diagram displays that. One user can have many polls and many votes. A poll can have many votes, but only one creator. A vote can have one poll and one user.  
<img width="631" alt="Skjermbilde 2022-09-19 kl  10 44 27" src="https://user-images.githubusercontent.com/90247464/190984184-9c26aa58-21f8-4c58-be1b-dec64062f57d.png">

## Tables

### User Table
<img width="747" alt="UserTable" src="https://user-images.githubusercontent.com/90247464/190984256-4fab032a-f47a-45b6-b1ce-46d68b083287.png">

### Poll Table
<img width="1121" alt="PollTable" src="https://user-images.githubusercontent.com/90247464/190984287-badb90a4-d441-4dbb-9331-112585565025.png">

### Vote Table
<img width="597" alt="VoteTable" src="https://user-images.githubusercontent.com/90247464/190984339-0d9dbb9e-ea91-4922-ba98-5b48bb8b6ea3.png">
