# RabbitMQ consumer
Microservice written in Go to consume messages from RabbitMQ. 
If it consumes a poll_created messsage it sends the results to https://www.dweet.io. Latest result can be found
[here](https://dweet.io/follow/feed-app-poll-result).
If poll is closed it displays the poll to dweet and and stores the PollResult in a non-relational database(MongoDB).

# To run 
To run locally you have to go into the rabbit_consumer folder and install go on your machine

- Install Go on your computer by clicking [here](https://go.dev/doc/install) and choose the download file for your operating system.

- To run write the following in your project terminal window
    
        cd microservice //Go into the microservice directory
        
        go run main.go //Run the go file in the directory
