Autonomous service workshop
========

Session 2: Dockerise you API - Dev set up and Hands on
------

### Overview of the session

In this hands on session, we will

1. Set up a docker file for a web project
2. Set up a docker file for a worker project
3. Set up docker compose
4. Run and test them
5. Optimize the docker file for development set up

### Get started

To get started 

1. Clone this repo

   `git clone git@github.com:PageUpPeopleOrg/twitter-dotnetcore.git`

TODO: Instructions here

### How to test

1. Run the containers
   `docker-compose up -d`

2. Create a post

   `curl -i -H "Content-Type: application/json" -X POST -d '{ "content":"pk,mytweet" }' http://0.0.0.0:4000/api/twitter`


3. Expect a log from worker saying

   `Processed Message pk, mytweet`

4. Check the tweets

   `curl -i http://0.0.0.0:4000/api/twitter`